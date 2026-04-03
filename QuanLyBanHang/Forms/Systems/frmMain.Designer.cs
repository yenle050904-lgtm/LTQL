using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Systems
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // ==============================
            // KHỞI TẠO CONTROLS
            // ==============================
            this.pnlSidebar = new Panel();
            this.pnlTopBar = new Panel();
            this.pnlContent = new Panel();
            this.lblAppName = new Label();
            this.lblTitle = new Label();

            // Sidebar buttons
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

            // ==============================
            // TOP BAR (thanh tiêu đề trên cùng)
            // ==============================
            this.pnlTopBar.BackColor = UIHelper.CardBg;
            this.pnlTopBar.Dock = DockStyle.Top;
            this.pnlTopBar.Height = 60;
            this.pnlTopBar.Padding = new Padding(20, 0, 0, 0);

            this.lblTitle.Text = "Trang chủ";
            this.lblTitle.Font = UIHelper.FontTitle;
            this.lblTitle.ForeColor = UIHelper.TextPrimary;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(20, 18);
            this.pnlTopBar.Controls.Add(this.lblTitle);

            // ==============================
            // CONTENT PANEL (vùng chính bên phải)
            // ==============================
            this.pnlContent.BackColor = UIHelper.ContentBg;
            this.pnlContent.Dock = DockStyle.Fill;
            this.pnlContent.Padding = new Padding(16);

            // ==============================
            // FORM CHÍNH
            // ==============================
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1100, 700);
            this.BackColor = UIHelper.ContentBg;
            this.Font = UIHelper.FontRegular;

            // Thứ tự Add quan trọng: Content → TopBar → Sidebar
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlTopBar);
            this.Controls.Add(this.pnlSidebar);

            this.Name = "frmMain";
            this.Text = "Hệ Thống Quản Lý Bán Nước Giải Khát";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);
        }

        // ==============================
        // KHAI BÁO FIELDS
        // ==============================
        private Panel pnlSidebar;
        private Panel pnlTopBar;
        private Panel pnlContent;
        private Label lblAppName;
        private Label lblTitle;
        private Button btnNhanVien;
        private Button btnSanPham;
        private Button btnLoaiSP;
        private Button btnHangSX;
        private Button btnKhachHang;
        private Button btnHoaDon;
        private Button btnBaoCao;
        private Button btnDangXuat;
    }
}
