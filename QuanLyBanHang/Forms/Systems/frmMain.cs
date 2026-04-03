using System;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.Forms.Categories;
using QuanLyBanHang.Forms.Business;
using QuanLyBanHang.Forms.Reports;

namespace QuanLyBanHang.Forms.Systems
{
    /// <summary>
    /// Form chính của ứng dụng. Sử dụng mô hình Sidebar + Panel content
    /// thay cho MDI truyền thống để tránh lỗi Z-order và UI hiện đại hơn.
    /// </summary>
    public partial class frmMain : Form
    {
        private Form? currentChildForm;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Mặc định kích hoạt trang Nhân Viên
            btnNhanVien.PerformClick();
        }

        // ==============================
        // MỞ FORM CON VÀO PANEL CONTENT
        // ==============================
        private void OpenChildForm(Form childForm, Button activeBtn)
        {
            // Đóng form con hiện tại nếu có
            currentChildForm?.Close();
            currentChildForm?.Dispose();

            // Đánh dấu sidebar button đang active
            UIHelper.SetActiveSidebarButton(activeBtn, pnlSidebar);

            // Cập nhật tiêu đề
            lblTitle.Text = childForm.Text;

            // Embed form vào panel content
            UIHelper.OpenChildForm(childForm, pnlContent);
            currentChildForm = childForm;
        }

        // ==============================
        // CÁC SỰ KIỆN SIDEBAR
        // ==============================
        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhanVien(), btnNhanVien);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmSanPham(), btnSanPham);
        }

        private void btnLoaiSP_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLoaiSanPham(), btnLoaiSP);
        }

        private void btnHangSX_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHangSanXuat(), btnHangSX);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhachHang(), btnKhachHang);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmHoaDon(), btnHoaDon);
        }

        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmBaoCaoDoanhThu(), btnBaoCao);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
