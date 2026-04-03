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
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new ErrorProvider(this.components);
            this.dgvKhachHang = new DataGridView();
            this.txtTimKiem = new TextBox();
            this.txtHoTen = new TextBox();
            this.txtDienThoai = new TextBox();
            this.txtDiaChi = new TextBox();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.lblSearch = new Label();
            this.lblHoTen = new Label();
            this.lblDT = new Label();
            this.lblDC = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            // Search
            this.lblSearch.Text = "🔍 Tìm kiếm:"; this.lblSearch.Font = UIHelper.FontRegular;
            this.lblSearch.Location = new Point(16, 16); this.lblSearch.AutoSize = true;
            this.txtTimKiem.Location = new Point(110, 14); this.txtTimKiem.Size = new Size(300, 26);
            this.txtTimKiem.PlaceholderText = "Nhập tên, SĐT hoặc địa chỉ...";
            UIHelper.StyleTextBox(this.txtTimKiem);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // Grid
            this.dgvKhachHang.Location = new Point(16, 50);
            this.dgvKhachHang.Size = new Size(700, 250);
            this.dgvKhachHang.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvKhachHang.CellClick += new DataGridViewCellEventHandler(this.dgvKhachHang_CellClick);

            // Inputs
            int y1 = 320, y2 = 360;
            this.lblHoTen.Text = "Họ tên:"; this.lblHoTen.Font = UIHelper.FontSmall;
            this.lblHoTen.Location = new Point(16, y1 + 3); this.lblHoTen.AutoSize = true;
            this.txtHoTen.Location = new Point(90, y1); this.txtHoTen.Size = new Size(200, 26);
            UIHelper.StyleTextBox(this.txtHoTen);

            this.lblDT.Text = "SĐT:"; this.lblDT.Font = UIHelper.FontSmall;
            this.lblDT.Location = new Point(310, y1 + 3); this.lblDT.AutoSize = true;
            this.txtDienThoai.Location = new Point(360, y1); this.txtDienThoai.Size = new Size(150, 26);
            UIHelper.StyleTextBox(this.txtDienThoai);

            this.lblDC.Text = "Địa chỉ:"; this.lblDC.Font = UIHelper.FontSmall;
            this.lblDC.Location = new Point(16, y2 + 3); this.lblDC.AutoSize = true;
            this.txtDiaChi.Location = new Point(90, y2); this.txtDiaChi.Size = new Size(420, 26);
            UIHelper.StyleTextBox(this.txtDiaChi);

            // Buttons
            int btnY = 405;
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
            this.ClientSize = new Size(740, 465);
            this.Controls.Add(lblSearch); this.Controls.Add(txtTimKiem);
            this.Controls.Add(dgvKhachHang);
            this.Controls.Add(lblHoTen); this.Controls.Add(txtHoTen);
            this.Controls.Add(lblDT); this.Controls.Add(txtDienThoai);
            this.Controls.Add(lblDC); this.Controls.Add(txtDiaChi);
            this.Controls.Add(btnThem); this.Controls.Add(btnSua);
            this.Controls.Add(btnXoa); this.Controls.Add(btnLamMoi);
            this.BackColor = UIHelper.ContentBg;
            this.Name = "frmKhachHang"; this.Text = "Quản Lý Khách Hàng";
            this.Load += new System.EventHandler(this.frmKhachHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }

        private ErrorProvider errorProvider;
        private DataGridView dgvKhachHang;
        private TextBox txtTimKiem, txtHoTen, txtDienThoai, txtDiaChi;
        private Button btnThem, btnSua, btnXoa, btnLamMoi;
        private Label lblSearch, lblHoTen, lblDT, lblDC;
    }
}
