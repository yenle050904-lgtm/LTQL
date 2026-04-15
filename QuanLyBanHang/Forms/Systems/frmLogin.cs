using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Systems
{
    /// <summary>
    /// Form đăng nhập hiển thị dạng Dialog trước khi vào hệ thống.
    /// Sử dụng BCrypt để xác thực mật khẩu từ bảng NhanVien.
    /// </summary>
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            // Căn giữa card khi form load
            this.Resize += (s, e) =>
            {
                pnlCard.Location = new Point(
                    (this.ClientSize.Width - pnlCard.Width) / 2,
                    (this.ClientSize.Height - pnlCard.Height) / 2
                );
            };
            pnlCard.Location = new Point(
                (this.ClientSize.Width - pnlCard.Width) / 2,
                (this.ClientSize.Height - pnlCard.Height) / 2
            );

            
        
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;


            // 1. Khởi tạo form Main
            frmMain formMain = new frmMain();

            // 2. Hiển thị form Main
            formMain.Show();

            // 3. Ẩn form Login đi
            this.Hide();
            // Validation cơ bản
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblError.Text = "⚠ Vui lòng nhập đầy đủ thông tin.";
                lblError.Visible = true;
                return;
            }

            try
            {
                using (var db = new QLBHDbContext())
                {
                    var user = db.NhanViens.FirstOrDefault(u => u.TenDangNhap == username);

                    if (user != null && BCrypt.Net.BCrypt.Verify(password, user.MatKhau))
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        lblError.Text = "⚠ Tên đăng nhập hoặc mật khẩu không đúng.";
                        lblError.Visible = true;
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "⚠ Lỗi kết nối CSDL: " + ex.Message;
                lblError.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
