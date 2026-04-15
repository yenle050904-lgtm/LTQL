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
            pnlSidebar = new Panel();
            pnlTopBar = new Panel();
            lblTitle = new Label();
            pnlContent = new Panel();
            lblAppName = new Label();
            pnlTopBar.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(200, 100);
            pnlSidebar.TabIndex = 2;
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.FromArgb(255, 255, 255);
            pnlTopBar.Controls.Add(lblTitle);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Padding = new Padding(20, 0, 0, 0);
            pnlTopBar.Size = new Size(1026, 60);
            pnlTopBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 46);
            lblTitle.Location = new Point(20, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(127, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Trang chủ";
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.FromArgb(245, 245, 245);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(0, 60);
            pnlContent.Name = "pnlContent";
            pnlContent.Padding = new Padding(16);
            pnlContent.Size = new Size(1026, 419);
            pnlContent.TabIndex = 0;
            // 
            // lblAppName
            // 
            lblAppName.Location = new Point(0, 0);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(100, 23);
            lblAppName.TabIndex = 0;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(1026, 479);
            Controls.Add(pnlContent);
            Controls.Add(pnlTopBar);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 10F);
            Name = "frmMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Quản Lý Bán Nước Giải Khát";
            WindowState = FormWindowState.Maximized;
            Load += frmMain_Load;
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            ResumeLayout(false);
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
