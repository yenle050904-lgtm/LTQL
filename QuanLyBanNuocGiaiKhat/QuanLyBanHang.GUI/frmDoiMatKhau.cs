using QuanLyBanHang.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // Giấu mật khẩu bằng dấu chấm mặc định của hệ thống
            txtMatKhauCu.UseSystemPasswordChar = true;
            txtMatKhauMoi.UseSystemPasswordChar = true;
            txtNhapLai.UseSystemPasswordChar = true;


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

            // 1. Kiểm tra không được để trống
            if (string.IsNullOrEmpty(mkCu) || string.IsNullOrEmpty(mkMoi) || string.IsNullOrEmpty(nhapLai))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kiểm tra mật khẩu mới và nhập lại có khớp nhau không
            if (mkMoi != nhapLai)
            {
                MessageBox.Show("Mật khẩu mới và Nhập lại không khớp nhau. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNhapLai.Focus();
                return;
            }

            // 3. Xử lý đổi mật khẩu qua lớp BUS (Bạn điều chỉnh lại tên hàm cho đúng với code BUS của bạn nhé)
            // 3. Xử lý đổi mật khẩu qua lớp BUS
            NhanVien_BUS nvBUS = new NhanVien_BUS(); // Phải khởi tạo đối tượng BUS trước

            // Gọi hàm qua biến nvBUS
            bool ketQua = nvBUS.DoiMatKhau(txtTaiKhoan.Text, mkCu, mkMoi);

            if (ketQua == true)
            {
                MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Hoặc Application.Restart() để bắt đăng nhập lại
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không chính xác!", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhauCu.Focus();
            }

        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
