using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    /// <summary>
    /// Form quản lý Khách Hàng — CRUD + Search-as-you-type.
    /// </summary>
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
            UIHelper.StyleDataGridView(dgvKhachHang);
        }

        private void LoadData(string keyword = "")
        {
            using var db = new QLBHDbContext();
            var query = db.KhachHangs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(k =>
                    k.HoVaTen.Contains(keyword) ||
                    k.DienThoai.Contains(keyword) ||
                    k.DiaChi.Contains(keyword));
            }

            dgvKhachHang.DataSource = query.Select(k => new
            {
                k.ID, k.HoVaTen, k.DienThoai, k.DiaChi
            }).ToList();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvKhachHang.Rows[e.RowIndex];
                txtHoTen.Text = row.Cells["HoVaTen"].Value?.ToString() ?? "";
                txtDienThoai.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";
                txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? "";
            }
        }

        private bool ValidateInput()
        {
            errorProvider.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            { errorProvider.SetError(txtHoTen, "Họ tên không được để trống."); valid = false; }
            return valid;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            using var db = new QLBHDbContext();
            db.KhachHangs.Add(new KhachHang
            {
                HoVaTen = txtHoTen.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim()
            });
            db.SaveChanges();
            LoadData(); ClearFields();
            MessageBox.Show("Thêm khách hàng thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            if (dgvKhachHang.CurrentRow == null) return;
            int id = (int)dgvKhachHang.CurrentRow.Cells["ID"].Value;

            using var db = new QLBHDbContext();
            var kh = db.KhachHangs.Find(id);
            if (kh != null)
            {
                kh.HoVaTen = txtHoTen.Text.Trim();
                kh.DienThoai = txtDienThoai.Text.Trim();
                kh.DiaChi = txtDiaChi.Text.Trim();
                db.SaveChanges();
            }
            LoadData(); ClearFields();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null) return;
            if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            int id = (int)dgvKhachHang.CurrentRow.Cells["ID"].Value;
            try
            {
                using var db = new QLBHDbContext();
                var kh = db.KhachHangs.Find(id);
                if (kh != null) { db.KhachHangs.Remove(kh); db.SaveChanges(); }
                LoadData(); ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        { ClearFields(); LoadData(); }

        private void ClearFields()
        { txtHoTen.Clear(); txtDienThoai.Clear(); txtDiaChi.Clear(); errorProvider.Clear(); }
    }
}
