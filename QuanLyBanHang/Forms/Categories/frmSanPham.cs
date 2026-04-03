using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    /// <summary>
    /// Form quản lý Sản Phẩm — CRUD đầy đủ.
    /// - Fix NullRef khi FK nullable (LoaiSanPham, HangSanXuat).
    /// - ErrorProvider validation, search-as-you-type.
    /// - Validate: DonGia > 0, SoLuong >= 0.
    /// </summary>
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadData();
            UIHelper.StyleDataGridView(dgvSanPham);
        }

        private void LoadComboBoxes()
        {
            using var db = new QLBHDbContext();

            cboLoaiSP.DataSource = db.LoaiSanPhams.ToList();
            cboLoaiSP.DisplayMember = "TenLoai";
            cboLoaiSP.ValueMember = "ID";
            cboLoaiSP.SelectedIndex = cboLoaiSP.Items.Count > 0 ? 0 : -1;

            cboHangSX.DataSource = db.HangSanXuats.ToList();
            cboHangSX.DisplayMember = "TenHangSanXuat";
            cboHangSX.ValueMember = "ID";
            cboHangSX.SelectedIndex = cboHangSX.Items.Count > 0 ? 0 : -1;
        }

        private void LoadData(string keyword = "")
        {
            using var db = new QLBHDbContext();
            var query = db.SanPhams.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(s => s.TenSanPham.Contains(keyword));
            }

            // Fix NullRef: dùng null-conditional cho FK nullable
            dgvSanPham.DataSource = query.Select(s => new
            {
                s.ID,
                s.TenSanPham,
                s.DonGia,
                s.SoLuong,
                Loai = s.LoaiSanPham != null ? s.LoaiSanPham.TenLoai : "",
                Hang = s.HangSanXuat != null ? s.HangSanXuat.TenHangSanXuat : ""
            }).ToList();
        }

        // ==============================
        // SEARCH-AS-YOU-TYPE
        // ==============================
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSanPham.Rows[e.RowIndex];
                txtTenSP.Text = row.Cells["TenSanPham"].Value?.ToString() ?? "";
                txtDonGia.Text = row.Cells["DonGia"].Value?.ToString() ?? "";
                txtSoLuong.Text = row.Cells["SoLuong"].Value?.ToString() ?? "";
            }
        }

        // ==============================
        // VALIDATION
        // ==============================
        private bool ValidateInput()
        {
            errorProvider.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                errorProvider.SetError(txtTenSP, "Tên sản phẩm không được để trống.");
                valid = false;
            }
            if (!int.TryParse(txtDonGia.Text, out int donGia) || donGia <= 0)
            {
                errorProvider.SetError(txtDonGia, "Đơn giá phải là số nguyên > 0.");
                valid = false;
            }
            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong < 0)
            {
                errorProvider.SetError(txtSoLuong, "Số lượng phải là số nguyên >= 0.");
                valid = false;
            }
            return valid;
        }

        // ==============================
        // THÊM
        // ==============================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using var db = new QLBHDbContext();
                var sp = new SanPham
                {
                    TenSanPham = txtTenSP.Text.Trim(),
                    DonGia = int.Parse(txtDonGia.Text),
                    SoLuong = int.Parse(txtSoLuong.Text),
                    LoaiSanPhamID = (int?)cboLoaiSP.SelectedValue,
                    HangSanXuatID = (int?)cboHangSX.SelectedValue
                };

                db.SanPhams.Add(sp);
                db.SaveChanges();
                LoadData();
                ClearFields();
                MessageBox.Show("Thêm sản phẩm thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + (ex.InnerException?.Message ?? ex.Message),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==============================
        // SỬA
        // ==============================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            if (dgvSanPham.CurrentRow == null) return;

            int id = (int)dgvSanPham.CurrentRow.Cells["ID"].Value;

            try
            {
                using var db = new QLBHDbContext();
                var sp = db.SanPhams.Find(id);
                if (sp == null) return;

                sp.TenSanPham = txtTenSP.Text.Trim();
                sp.DonGia = int.Parse(txtDonGia.Text);
                sp.SoLuong = int.Parse(txtSoLuong.Text);
                sp.LoaiSanPhamID = (int?)cboLoaiSP.SelectedValue;
                sp.HangSanXuatID = (int?)cboHangSX.SelectedValue;

                db.SaveChanges();
                LoadData();
                ClearFields();
                MessageBox.Show("Cập nhật thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + (ex.InnerException?.Message ?? ex.Message),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==============================
        // XÓA
        // ==============================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;
            int id = (int)dgvSanPham.CurrentRow.Cells["ID"].Value;

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var db = new QLBHDbContext();
                var sp = db.SanPhams.Find(id);
                if (sp != null)
                {
                    db.SanPhams.Remove(sp);
                    db.SaveChanges();
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa: " + (ex.InnerException?.Message ?? ex.Message),
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadData();
        }

        private void ClearFields()
        {
            txtTenSP.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            errorProvider.Clear();
        }
    }
}
