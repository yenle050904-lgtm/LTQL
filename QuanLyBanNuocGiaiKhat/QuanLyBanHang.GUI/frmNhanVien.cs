using System;
using System.Windows.Forms;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class frmNhanVien : Form
    {
        NhanVien_BUS nvBus = new NhanVien_BUS();

        public frmNhanVien()
        {
            InitializeComponent();
            this.Load += FrmNhanVien_Load;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
        }

        private void LoadDanhSachNhanVien()
        {
            try
            {
                dgvNhanVien.DataSource = nvBus.LayDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                dgvNhanVien.DataSource = nvBus.TimKiemNhanVien(tuKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private bool DocVaKiemTraDuLieu(out NhanVien_DTO nv)
        {
            nv = new NhanVien_DTO();

            string hoTen = txtHoTen.Text.Trim();
            string dangNhap = txtDangNhap.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(dangNhap))
            {
                MessageBox.Show("Vui lòng nhập đủ Họ tên và Tên đăng nhập!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            nv.HoVaTen = hoTen;
            nv.DienThoai = txtDienThoai.Text.Trim();
            nv.TenDangNhap = dangNhap;
            nv.MatKhau = matKhau;
            nv.QuyenHan = chkAdmin.Checked ? 1 : 0;
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!DocVaKiemTraDuLieu(out NhanVien_DTO nv)) return;

            try
            {
                // BUS đã kiểm tra trùng tên đăng nhập bên trong. Nếu trùng, trả về false.
                if (nvBus.KiemTraTonTaiTenDangNhap(nv.TenDangNhap))
                {
                    MessageBox.Show($"Tên đăng nhập '{nv.TenDangNhap}' đã tồn tại!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDangNhap.Focus();
                    txtDangNhap.SelectAll();
                    return;
                }

                if (nvBus.ThemNhanVien(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa trên lưới!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DocVaKiemTraDuLieu(out NhanVien_DTO nv)) return;
            nv.ID = int.Parse(txtID.Text);

            try
            {
                // Check trùng tên đăng nhập (bỏ qua chính bản ghi đang sửa)
                if (nvBus.KiemTraTonTaiTenDangNhap(nv.TenDangNhap, nv.ID))
                {
                    MessageBox.Show($"Tên đăng nhập '{nv.TenDangNhap}' đã được dùng bởi nhân viên khác!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDangNhap.Focus();
                    return;
                }

                if (nvBus.SuaNhanVien(nv))
                {
                    MessageBox.Show("Cập nhật thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(txtID.Text);

            // Không cho xóa chính mình
            if (id == UserSession.ID)
            {
                MessageBox.Show("Không thể xóa tài khoản đang đăng nhập!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                if (nvBus.XoaNhanVien(id))
                {
                    MessageBox.Show("Xóa thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì đã có dữ liệu giao dịch (hóa đơn/phiếu nhập) gắn với họ.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtHoTen.Text = "";
            txtDienThoai.Text = "";
            txtDangNhap.Text = "";
            txtMatKhau.Text = "";
            chkAdmin.Checked = false;
            txtTimKiem.Text = "";
            txtHoTen.Focus();
        }

        private void dgvNhanVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

            txtID.Text = row.Cells[0].Value?.ToString() ?? string.Empty;
            txtHoTen.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
            txtDienThoai.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
            txtDangNhap.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
            txtMatKhau.Text = row.Cells[4].Value?.ToString() ?? string.Empty;

            if (row.Cells[5].Value != null && row.Cells[5].Value != DBNull.Value)
                chkAdmin.Checked = Convert.ToBoolean(row.Cells[5].Value);
        }
    }
}
