using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    partial class frmSanPham
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
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new ErrorProvider(this.components);
            this.dgvSanPham = new DataGridView();
            this.txtTimKiem = new TextBox();
            this.txtTenSP = new TextBox();
            this.txtDonGia = new TextBox();
            this.txtSoLuong = new TextBox();
            this.cboLoaiSP = new ComboBox();
            this.cboHangSX = new ComboBox();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.lblSearch = new Label();
            this.lblTenSP = new Label();
            this.lblDonGia = new Label();
            this.lblSoLuong = new Label();
            this.lblLoai = new Label();
            this.lblHang = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            // Search
            this.lblSearch.Text = "🔍 Tìm kiếm:";
            this.lblSearch.Font = UIHelper.FontRegular;
            this.lblSearch.Location = new Point(16, 16);
            this.lblSearch.AutoSize = true;

            this.txtTimKiem.Location = new Point(110, 14);
            this.txtTimKiem.Size = new Size(300, 26);
            this.txtTimKiem.PlaceholderText = "Nhập tên sản phẩm...";
            UIHelper.StyleTextBox(this.txtTimKiem);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // DataGridView
            this.dgvSanPham.Location = new Point(16, 50);
            this.dgvSanPham.Size = new Size(750, 240);
            this.dgvSanPham.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvSanPham.CellClick += new DataGridViewCellEventHandler(this.dgvSanPham_CellClick);

            // Input fields
            int y1 = 310, y2 = 355;

            this.lblTenSP.Text = "Tên SP:"; this.lblTenSP.Font = UIHelper.FontSmall;
            this.lblTenSP.Location = new Point(16, y1 + 3); this.lblTenSP.AutoSize = true;
            this.txtTenSP.Location = new Point(90, y1); this.txtTenSP.Size = new Size(200, 26);
            UIHelper.StyleTextBox(this.txtTenSP);

            this.lblDonGia.Text = "Đơn giá:"; this.lblDonGia.Font = UIHelper.FontSmall;
            this.lblDonGia.Location = new Point(310, y1 + 3); this.lblDonGia.AutoSize = true;
            this.txtDonGia.Location = new Point(385, y1); this.txtDonGia.Size = new Size(120, 26);
            UIHelper.StyleTextBox(this.txtDonGia);

            this.lblSoLuong.Text = "Số lượng:"; this.lblSoLuong.Font = UIHelper.FontSmall;
            this.lblSoLuong.Location = new Point(530, y1 + 3); this.lblSoLuong.AutoSize = true;
            this.txtSoLuong.Location = new Point(610, y1); this.txtSoLuong.Size = new Size(100, 26);
            UIHelper.StyleTextBox(this.txtSoLuong);

            this.lblLoai.Text = "Loại SP:"; this.lblLoai.Font = UIHelper.FontSmall;
            this.lblLoai.Location = new Point(16, y2 + 3); this.lblLoai.AutoSize = true;
            this.cboLoaiSP.Location = new Point(90, y2); this.cboLoaiSP.Size = new Size(200, 26);
            UIHelper.StyleComboBox(this.cboLoaiSP);

            this.lblHang.Text = "Hãng SX:"; this.lblHang.Font = UIHelper.FontSmall;
            this.lblHang.Location = new Point(310, y2 + 3); this.lblHang.AutoSize = true;
            this.cboHangSX.Location = new Point(385, y2); this.cboHangSX.Size = new Size(200, 26);
            UIHelper.StyleComboBox(this.cboHangSX);

            // Buttons
            int btnY = 400;
            this.btnThem.Text = "✚ Thêm"; this.btnThem.Location = new Point(16, btnY);
            this.btnThem.Size = new Size(110, 36); UIHelper.StyleButtonPrimary(this.btnThem);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Text = "✏ Sửa"; this.btnSua.Location = new Point(136, btnY);
            this.btnSua.Size = new Size(110, 36); UIHelper.StyleButtonSuccess(this.btnSua);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Text = "🗑 Xóa"; this.btnXoa.Location = new Point(256, btnY);
            this.btnXoa.Size = new Size(110, 36); UIHelper.StyleButtonDanger(this.btnXoa);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Text = "↻ Làm mới"; this.btnLamMoi.Location = new Point(376, btnY);
            this.btnLamMoi.Size = new Size(110, 36); UIHelper.StyleButtonSecondary(this.btnLamMoi);
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // Form
            this.ClientSize = new Size(790, 460);
            this.Controls.Add(this.lblSearch); this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.dgvSanPham);
            this.Controls.Add(this.lblTenSP); this.Controls.Add(this.txtTenSP);
            this.Controls.Add(this.lblDonGia); this.Controls.Add(this.txtDonGia);
            this.Controls.Add(this.lblSoLuong); this.Controls.Add(this.txtSoLuong);
            this.Controls.Add(this.lblLoai); this.Controls.Add(this.cboLoaiSP);
            this.Controls.Add(this.lblHang); this.Controls.Add(this.cboHangSX);
            this.Controls.Add(this.btnThem); this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa); this.Controls.Add(this.btnLamMoi);
            this.BackColor = UIHelper.ContentBg;
            this.Name = "frmSanPham";
            this.Text = "Quản Lý Sản Phẩm";
            this.Load += new System.EventHandler(this.frmSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private ErrorProvider errorProvider;
        private DataGridView dgvSanPham;
        private TextBox txtTimKiem, txtTenSP, txtDonGia, txtSoLuong;
        private ComboBox cboLoaiSP, cboHangSX;
        private Button btnThem, btnSua, btnXoa, btnLamMoi;
        private Label lblSearch, lblTenSP, lblDonGia, lblSoLuong, lblLoai, lblHang;
    }
}
