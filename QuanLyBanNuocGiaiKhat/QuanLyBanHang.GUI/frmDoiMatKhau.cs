using System;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmDoiMatKhau : Form
    {
        public frmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void frmDoiMatKhau_Load(object sender, EventArgs e)
        {
            // Giấu mật khẩu
            txtMatKhauCu.UseSystemPasswordChar = true;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtNhapLai.UseSystemPasswordChar = true;

            // Tự động điền tên đăng nhập đang login, không cho sửa
            txtTaiKhoan.Text = UserSession.TenDangNhap ?? string.Empty;
            txtTaiKhoan.ReadOnly = true;
            txtTaiKhoan.TabStop = false;

            txtMatKhauCu.Focus();
        }

        private void chkHienMatKhauCu_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauCu.UseSystemPasswordChar = !chkHienMatKhauCu.Checked;
        }

        private void chkHienMatKhauMoi_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhauMoi.UseSystemPasswordChar = !chkHienMatKhauMoi.Checked;
        }

        private void chkHienNhapLai_CheckedChanged(object sender, EventArgs e)
        {
            txtNhapLai.UseSystemPasswordChar = !chkHienNhapLai.Checked;
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            string mkCu = txtMatKhauCu.Text.Trim();
            string mkMoi = txtMatKhauMoi.Text.Trim();
            string nhapLai = txtNhapLai.Text.Trim();

            if (string.IsNullOrEmpty(mkCu) || string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(nhapLai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (mkMoi.Length < 4)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 4 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            if (mkMoi != nhapLai)
            {
                MessageBox.Show("Mật khẩu mới và Nhập lại không khớp nhau. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapLai.Focus();
                return;
            }

            if (mkCu == mkMoi)
            {
                MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMatKhauMoi.Focus();
                return;
            }

            try
            {
                NhanVien_BUS nvBUS = new NhanVien_BUS();
                bool ketQua = nvBUS.DoiMatKhau(UserSession.TenDangNhap, mkCu, mkMoi);

                if (ketQua)
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhauCu.Focus();
                    txtMatKhauCu.SelectAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
