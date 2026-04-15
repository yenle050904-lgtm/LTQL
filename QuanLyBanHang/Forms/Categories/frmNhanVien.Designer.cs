using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    partial class frmNhanVien
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
            components = new System.ComponentModel.Container();
            errorProvider = new ErrorProvider(components);
            dgvNhanVien = new DataGridView();
            txtTimKiem = new TextBox();
            txtHoTen = new TextBox();
            txtDienThoai = new TextBox();
            txtTenDangNhap = new TextBox();
            txtMatKhau = new TextBox();
            chkAdmin = new CheckBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblSearch = new Label();
            lblHoTen = new Label();
            lblDT = new Label();
            lblTDN = new Label();
            lblMK = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).BeginInit();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // dgvNhanVien
            // 
            dgvNhanVien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvNhanVien.ColumnHeadersHeight = 29;
            dgvNhanVien.Location = new Point(16, 50);
            dgvNhanVien.Name = "dgvNhanVien";
            dgvNhanVien.RowHeadersWidth = 51;
            dgvNhanVien.Size = new Size(750, 260);
            dgvNhanVien.TabIndex = 2;
            dgvNhanVien.CellClick += dgvNhanVien_CellClick;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(110, 14);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập tên, SĐT hoặc tên đăng nhập...";
            txtTimKiem.Size = new Size(300, 27);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(16, 330);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(180, 27);
            txtHoTen.TabIndex = 4;
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(400, 330);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(150, 27);
            txtDienThoai.TabIndex = 6;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Location = new Point(16, 375);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(180, 27);
            txtTenDangNhap.TabIndex = 8;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Location = new Point(400, 375);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '●';
            txtMatKhau.Size = new Size(150, 27);
            txtMatKhau.TabIndex = 10;
            // 
            // chkAdmin
            // 
            chkAdmin.AutoSize = true;
            chkAdmin.Font = new Font("Segoe UI", 10F);
            chkAdmin.Location = new Point(580, 375);
            chkAdmin.Name = "chkAdmin";
            chkAdmin.Size = new Size(82, 27);
            chkAdmin.TabIndex = 11;
            chkAdmin.Text = "Admin";
            // 
            // btnThem
            // 
            btnThem.Location = new Point(16, 420);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(110, 36);
            btnThem.TabIndex = 12;
            btnThem.Text = "✚ Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(136, 420);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(110, 36);
            btnSua.TabIndex = 13;
            btnSua.Text = "✏ Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(256, 420);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(110, 36);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "🗑 Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(376, 420);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 36);
            btnLamMoi.TabIndex = 15;
            btnLamMoi.Text = "↻ Làm mới";
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F);
            lblSearch.Location = new Point(16, 16);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(111, 23);
            lblSearch.TabIndex = 0;
            lblSearch.Text = "🔍 Tìm kiếm:";
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Font = new Font("Segoe UI", 9F);
            lblHoTen.Location = new Point(16, 330);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(78, 20);
            lblHoTen.TabIndex = 3;
            lblHoTen.Text = "Họ và Tên:";
            // 
            // lblDT
            // 
            lblDT.AutoSize = true;
            lblDT.Font = new Font("Segoe UI", 9F);
            lblDT.Location = new Point(310, 330);
            lblDT.Name = "lblDT";
            lblDT.Size = new Size(81, 20);
            lblDT.TabIndex = 5;
            lblDT.Text = "Điện thoại:";
            // 
            // lblTDN
            // 
            lblTDN.AutoSize = true;
            lblTDN.Font = new Font("Segoe UI", 9F);
            lblTDN.Location = new Point(16, 375);
            lblTDN.Name = "lblTDN";
            lblTDN.Size = new Size(85, 20);
            lblTDN.TabIndex = 7;
            lblTDN.Text = "Đăng nhập:";
            // 
            // lblMK
            // 
            lblMK.AutoSize = true;
            lblMK.Font = new Font("Segoe UI", 9F);
            lblMK.Location = new Point(310, 375);
            lblMK.Name = "lblMK";
            lblMK.Size = new Size(73, 20);
            lblMK.TabIndex = 9;
            lblMK.Text = "Mật khẩu:";
            // 
            // frmNhanVien
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(790, 480);
            Controls.Add(lblSearch);
            Controls.Add(txtTimKiem);
            Controls.Add(dgvNhanVien);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblDT);
            Controls.Add(txtDienThoai);
            Controls.Add(lblTDN);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblMK);
            Controls.Add(txtMatKhau);
            Controls.Add(chkAdmin);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnLamMoi);
            Name = "frmNhanVien";
            Text = "Quản Lý Nhân Viên";
            Load += frmNhanVien_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvNhanVien).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ErrorProvider errorProvider;
        private DataGridView dgvNhanVien;
        private TextBox txtTimKiem;
        private TextBox txtHoTen;
        private TextBox txtDienThoai;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private CheckBox chkAdmin;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Label lblSearch;
        private Label lblHoTen;
        private Label lblDT;
        private Label lblTDN;
        private Label lblMK;
    }
}
