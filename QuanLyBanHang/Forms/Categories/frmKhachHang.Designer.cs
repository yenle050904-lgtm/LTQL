using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    partial class frmKhachHang
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            errorProvider = new ErrorProvider(components);
            dgvKhachHang = new DataGridView();
            txtTimKiem = new TextBox();
            txtHoTen = new TextBox();
            txtDienThoai = new TextBox();
            txtDiaChi = new TextBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            lblSearch = new Label();
            lblHoTen = new Label();
            lblDT = new Label();
            lblDC = new Label();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).BeginInit();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // dgvKhachHang
            // 
            dgvKhachHang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvKhachHang.ColumnHeadersHeight = 29;
            dgvKhachHang.Location = new Point(16, 50);
            dgvKhachHang.Name = "dgvKhachHang";
            dgvKhachHang.RowHeadersWidth = 51;
            dgvKhachHang.Size = new Size(700, 250);
            dgvKhachHang.TabIndex = 2;
            dgvKhachHang.CellClick += dgvKhachHang_CellClick;
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(110, 14);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập tên, SĐT hoặc địa chỉ...";
            txtTimKiem.Size = new Size(300, 27);
            txtTimKiem.TabIndex = 1;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(90, 320);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(200, 27);
            txtHoTen.TabIndex = 4;
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(360, 320);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(150, 27);
            txtDienThoai.TabIndex = 6;
            // 
            // txtDiaChi
            // 
            txtDiaChi.Location = new Point(90, 360);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(420, 27);
            txtDiaChi.TabIndex = 8;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(16, 405);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(110, 36);
            btnThem.TabIndex = 9;
            btnThem.Text = "✚ Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(136, 405);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(110, 36);
            btnSua.TabIndex = 10;
            btnSua.Text = "✏ Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(256, 405);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(110, 36);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "🗑 Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(376, 405);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(110, 36);
            btnLamMoi.TabIndex = 12;
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
            lblHoTen.Location = new Point(16, 320);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(57, 20);
            lblHoTen.TabIndex = 3;
            lblHoTen.Text = "Họ tên:";
            // 
            // lblDT
            // 
            lblDT.AutoSize = true;
            lblDT.Font = new Font("Segoe UI", 9F);
            lblDT.Location = new Point(310, 320);
            lblDT.Name = "lblDT";
            lblDT.Size = new Size(39, 20);
            lblDT.TabIndex = 5;
            lblDT.Text = "SĐT:";
            // 
            // lblDC
            // 
            lblDC.AutoSize = true;
            lblDC.Font = new Font("Segoe UI", 9F);
            lblDC.Location = new Point(16, 360);
            lblDC.Name = "lblDC";
            lblDC.Size = new Size(58, 20);
            lblDC.TabIndex = 7;
            lblDC.Text = "Địa chỉ:";
            // 
            // frmKhachHang
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(740, 465);
            Controls.Add(lblSearch);
            Controls.Add(txtTimKiem);
            Controls.Add(dgvKhachHang);
            Controls.Add(lblHoTen);
            Controls.Add(txtHoTen);
            Controls.Add(lblDT);
            Controls.Add(txtDienThoai);
            Controls.Add(lblDC);
            Controls.Add(txtDiaChi);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnLamMoi);
            Name = "frmKhachHang";
            Text = "Quản Lý Khách Hàng";
            Load += frmKhachHang_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvKhachHang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ErrorProvider errorProvider;
        private DataGridView dgvKhachHang;
        private TextBox txtTimKiem, txtHoTen, txtDienThoai, txtDiaChi;
        private Button btnThem, btnSua, btnXoa, btnLamMoi;
        private Label lblSearch, lblHoTen, lblDT, lblDC;
    }
}
