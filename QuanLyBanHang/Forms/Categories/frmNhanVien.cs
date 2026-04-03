using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    /// <summary>
    /// Form quản lý Nhân Viên — CRUD đầy đủ.
    /// - Validation bằng ErrorProvider (không dùng MessageBox).
    /// - BCrypt: hash khi thêm mới, bỏ qua khi sửa nếu để trống.
    /// - Search-as-you-type bằng LINQ.
    /// </summary>
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }

        // ==============================
        // LOAD DỮ LIỆU
        // ==============================
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
            UIHelper.StyleDataGridView(dgvNhanVien);
        }

        private void LoadData(string keyword = "")
        {
            using var db = new QLBHDbContext();
            var query = db.NhanViens.AsQueryable();

            // Tìm kiếm theo từ khóa
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(n =>
                    n.HoVaTen.Contains(keyword) ||
                    n.TenDangNhap.Contains(keyword) ||
                    n.DienThoai.Contains(keyword));
            }

            dgvNhanVien.DataSource = query.Select(n => new
            {
                n.ID,
                n.HoVaTen,
                n.DienThoai,
                n.TenDangNhap,
                QuyenHan = n.QuyenHan ? "Admin" : "User"
            }).ToList();
        }

        // ==============================
        // SEARCH-AS-YOU-TYPE
        // ==============================
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text.Trim());
        }

        // ==============================
        // CLICK VÀO DÒNG → FILL TEXTBOX
        // ==============================
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                txtHoTen.Text = row.Cells["HoVaTen"].Value?.ToString() ?? "";
                txtDienThoai.Text = row.Cells["DienThoai"].Value?.ToString() ?? "";
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value?.ToString() ?? "";
                txtMatKhau.Clear(); // Không hiển thị mật khẩu cũ
                chkAdmin.Checked = row.Cells["QuyenHan"].Value?.ToString() == "Admin";
            }
        }

        // ==============================
        // VALIDATION BẰNG ERRORPROVIDER
        // ==============================
        private bool ValidateInput(bool isNew)
        {
            errorProvider.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                errorProvider.SetError(txtHoTen, "Họ tên không được để trống.");
                valid = false;
            }
            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                errorProvider.SetError(txtTenDangNhap, "Tên đăng nhập không được để trống.");
                valid = false;
            }
            if (isNew && string.IsNullOrWhiteSpace(txtMatKhau.Text))
            {
                errorProvider.SetError(txtMatKhau, "Mật khẩu bắt buộc khi thêm mới.");
                valid = false;
            }
            if (!string.IsNullOrWhiteSpace(txtDienThoai.Text) &&
                !txtDienThoai.Text.All(c => char.IsDigit(c) || c == '+' || c == '-'))
            {
                errorProvider.SetError(txtDienThoai, "Số điện thoại không hợp lệ.");
                valid = false;
            }
            return valid;
        }

        // ==============================
        // THÊM NHÂN VIÊN
        // ==============================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(isNew: true)) return;

            try
            {
                using var db = new QLBHDbContext();
                var nv = new NhanVien
                {
                    HoVaTen = txtHoTen.Text.Trim(),
                    DienThoai = txtDienThoai.Text.Trim(),
                    TenDangNhap = txtTenDangNhap.Text.Trim(),
                    MatKhau = BCrypt.Net.BCrypt.HashPassword(txtMatKhau.Text),
                    QuyenHan = chkAdmin.Checked
                };

                db.NhanViens.Add(nv);
                db.SaveChanges();
                LoadData();
                ClearFields();
                MessageBox.Show("Thêm nhân viên thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.InnerException?.Message ?? ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==============================
        // SỬA NHÂN VIÊN (BCrypt thông minh)
        // ==============================
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(isNew: false)) return;
            if (dgvNhanVien.CurrentRow == null) return;

            int id = (int)dgvNhanVien.CurrentRow.Cells["ID"].Value;

            try
            {
                using var db = new QLBHDbContext();
                var nv = db.NhanViens.Find(id);
                if (nv == null) return;

                nv.HoVaTen = txtHoTen.Text.Trim();
                nv.DienThoai = txtDienThoai.Text.Trim();
                nv.TenDangNhap = txtTenDangNhap.Text.Trim();
                nv.QuyenHan = chkAdmin.Checked;

                // Chỉ băm lại khi người dùng nhập mật khẩu mới
                if (!string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    nv.MatKhau = BCrypt.Net.BCrypt.HashPassword(txtMatKhau.Text);
                }
                // Nếu để trống → giữ nguyên mật khẩu cũ (không cần làm gì)

                db.SaveChanges();
                LoadData();
                ClearFields();
                MessageBox.Show("Cập nhật thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.InnerException?.Message ?? ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==============================
        // XÓA NHÂN VIÊN
        // ==============================
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null) return;
            int id = (int)dgvNhanVien.CurrentRow.Cells["ID"].Value;

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using var db = new QLBHDbContext();
                var nv = db.NhanViens.Find(id);
                if (nv != null)
                {
                    db.NhanViens.Remove(nv);
                    db.SaveChanges();
                    LoadData();
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa: " + ex.InnerException?.Message ?? ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==============================
        // LÀM MỚI
        // ==============================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearFields();
            LoadData();
        }

        private void ClearFields()
        {
            txtHoTen.Clear();
            txtDienThoai.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            chkAdmin.Checked = false;
            errorProvider.Clear();
        }
    }
}
