using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    partial class frmLoaiSanPham
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            errorProvider = new ErrorProvider(components);
            dgvLoaiSP = new DataGridView();
            txtTenLoai = new TextBox();
            lblTen = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSP).BeginInit();
            SuspendLayout();
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // dgvLoaiSP
            // 
            dgvLoaiSP.ColumnHeadersHeight = 29;
            dgvLoaiSP.Location = new Point(16, 16);
            dgvLoaiSP.Name = "dgvLoaiSP";
            dgvLoaiSP.RowHeadersWidth = 51;
            dgvLoaiSP.Size = new Size(500, 250);
            dgvLoaiSP.TabIndex = 0;
            dgvLoaiSP.CellClick += dgvLoaiSP_CellClick;
            // 
            // txtTenLoai
            // 
            txtTenLoai.Location = new Point(90, 287);
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(250, 27);
            txtTenLoai.TabIndex = 2;
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Segoe UI", 9F);
            lblTen.Location = new Point(16, 290);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(64, 20);
            lblTen.TabIndex = 1;
            lblTen.Text = "Tên loại:";
            // 
            // btnThem
            // 
            btnThem.Location = new Point(16, 330);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(100, 36);
            btnThem.TabIndex = 3;
            btnThem.Text = "✚ Thêm";
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(126, 330);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(100, 36);
            btnSua.TabIndex = 4;
            btnSua.Text = "✏ Sửa";
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(236, 330);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(100, 36);
            btnXoa.TabIndex = 5;
            btnXoa.Text = "🗑 Xóa";
            btnXoa.Click += btnXoa_Click;
            // 
            // frmLoaiSanPham
            // 
            BackColor = Color.FromArgb(245, 245, 245);
            ClientSize = new Size(540, 390);
            Controls.Add(dgvLoaiSP);
            Controls.Add(lblTen);
            Controls.Add(txtTenLoai);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Name = "frmLoaiSanPham";
            Text = "Quản Lý Loại Sản Phẩm";
            Load += frmLoaiSanPham_Load;
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLoaiSP).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private ErrorProvider errorProvider;
        private DataGridView dgvLoaiSP;
        private TextBox txtTenLoai;
        private Label lblTen;
        private Button btnThem, btnSua, btnXoa;
    }
}
