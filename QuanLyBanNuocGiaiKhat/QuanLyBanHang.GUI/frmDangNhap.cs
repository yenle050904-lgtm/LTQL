using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmDangNhap : Form
    {
        NhanVien_BUS nvBus = new NhanVien_BUS();

        public frmDangNhap()
        {
            InitializeComponent();

            // Căn card vào giữa khi resize
            this.Resize += (s, e) =>
            {
                pnlCard.Location = new Point(
                    (this.ClientSize.Width - pnlCard.Width) / 2,
                    (this.ClientSize.Height - pnlCard.Height) / 2
                );
            };

            // Nhấn Enter ở ô mật khẩu = bấm Đăng nhập
            this.txtMatKhau.KeyDown += txtPassword_KeyDown;
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnDangNhap.PerformClick();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tendangnhap = txtTenDangNhap.Text.Trim();
            string matkhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(tendangnhap) || string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable dt = nvBus.KiemTraDangNhap(tendangnhap, matkhau);

                if (dt.Rows.Count > 0)
                {
                    int id = Convert.ToInt32(dt.Rows[0]["ID"]);
                    string hoTen = dt.Rows[0]["HoVaTen"].ToString();
                    string tenDN = dt.Rows[0]["TenDangNhap"].ToString();
                    bool isAdmin = Convert.ToInt32(dt.Rows[0]["QuyenHan"]) == 1;

                    // Tự động set cả ThoiGianVao
                    UserSession.DangNhap(id, hoTen, tenDN, isAdmin);

                    MessageBox.Show($"Chào mừng {hoTen} đăng nhập hệ thống!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    frmMain mainForm = new frmMain();
                    mainForm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!",
                        "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.SelectAll();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
