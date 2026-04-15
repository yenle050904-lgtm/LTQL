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

            int y = 90; // Vị trí Y bắt đầu sau logo
            int gap = 4;
            this.btnNhanVien = UIHelper.CreateSidebarButton("Nhân Viên", "👤", y); y += 45 + gap;
            this.btnSanPham = UIHelper.CreateSidebarButton("Sản Phẩm", "📦", y); y += 45 + gap;
            this.btnLoaiSP = UIHelper.CreateSidebarButton("Loại SP", "📂", y); y += 45 + gap;
            this.btnHangSX = UIHelper.CreateSidebarButton("Hãng SX", "🏭", y); y += 45 + gap;
            this.btnKhachHang = UIHelper.CreateSidebarButton("Khách Hàng", "🧑‍💼", y); y += 45 + gap;
            this.btnHoaDon = UIHelper.CreateSidebarButton("Hóa Đơn", "🧾", y); y += 45 + gap;
            this.btnBaoCao = UIHelper.CreateSidebarButton("Báo Cáo", "📊", y); y += 45 + gap + 20;
            this.btnDangXuat = UIHelper.CreateSidebarButton("Đăng Xuất", "🚪", y);

            this.SuspendLayout();

            // ==============================
            // SIDEBAR PANEL (trái, 220px)
            // ==============================
            this.pnlSidebar.BackColor = UIHelper.SidebarBg;
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 220;
            this.pnlSidebar.Padding = new Padding(0);

            // Logo / Tên ứng dụng trên sidebar
            this.lblAppName.Text = "🥤 QLBH";
            this.lblAppName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblAppName.ForeColor = UIHelper.AccentBlue;
            this.lblAppName.BackColor = Color.Transparent;
            this.lblAppName.Location = new Point(16, 20);
            this.lblAppName.AutoSize = true;

            // Thêm buttons vào sidebar
            this.pnlSidebar.Controls.Add(this.lblAppName);
            this.pnlSidebar.Controls.Add(this.btnNhanVien);
            this.pnlSidebar.Controls.Add(this.btnSanPham);
            this.pnlSidebar.Controls.Add(this.btnLoaiSP);
            this.pnlSidebar.Controls.Add(this.btnHangSX);
            this.pnlSidebar.Controls.Add(this.btnKhachHang);
            this.pnlSidebar.Controls.Add(this.btnHoaDon);
            this.pnlSidebar.Controls.Add(this.btnBaoCao);
            this.pnlSidebar.Controls.Add(this.btnDangXuat);

            // Button events
            this.btnNhanVien.Click += new System.EventHandler(this.btnNhanVien_Click);
            this.btnSanPham.Click += new System.EventHandler(this.btnSanPham_Click);
            this.btnLoaiSP.Click += new System.EventHandler(this.btnLoaiSP_Click);
            this.btnHangSX.Click += new System.EventHandler(this.btnHangSX_Click);
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            this.btnHoaDon.Click += new System.EventHandler(this.btnHoaDon_Click);
            this.btnBaoCao.Click += new System.EventHandler(this.btnBaoCao_Click);
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);

            // Đổi màu nút Đăng Xuất thành đỏ nhạt
            this.btnDangXuat.ForeColor = UIHelper.AccentRed;
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
