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
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new ErrorProvider(this.components);
            this.dgvNhanVien = new DataGridView();
            this.txtTimKiem = new TextBox();
            this.txtHoTen = new TextBox();
            this.txtDienThoai = new TextBox();
            this.txtTenDangNhap = new TextBox();
            this.txtMatKhau = new TextBox();
            this.chkAdmin = new CheckBox();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnLamMoi = new Button();
            this.lblSearch = new Label();
            this.lblHoTen = new Label();
            this.lblDT = new Label();
            this.lblTDN = new Label();
            this.lblMK = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            // ==============================
            // SEARCH BAR
            // ==============================
            this.lblSearch.Text = "🔍 Tìm kiếm:";
            this.lblSearch.Font = UIHelper.FontRegular;
            this.lblSearch.Location = new Point(16, 16);
            this.lblSearch.AutoSize = true;

            this.txtTimKiem.Location = new Point(110, 14);
            this.txtTimKiem.Size = new Size(300, 26);
            this.txtTimKiem.PlaceholderText = "Nhập tên, SĐT hoặc tên đăng nhập...";
            UIHelper.StyleTextBox(this.txtTimKiem);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);

            // ==============================
            // DATAGRIDVIEW
            // ==============================
            this.dgvNhanVien.Location = new Point(16, 50);
            this.dgvNhanVien.Size = new Size(750, 260);
            this.dgvNhanVien.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.dgvNhanVien.CellClick += new DataGridViewCellEventHandler(this.dgvNhanVien_CellClick);

            // ==============================
            // INPUT FIELDS (2 hàng)
            // ==============================
            int inputY1 = 330;
            int inputY2 = 375;
            int lblW = 95;

            this.lblHoTen.Text = "Họ và Tên:";
            this.lblHoTen.Font = UIHelper.FontSmall;
            this.lblHoTen.Location = new Point(16, inputY1 + 3);
            this.lblHoTen.AutoSize = true;

            this.txtHoTen.Location = new Point(16 + lblW, inputY1);
            this.txtHoTen.Size = new Size(180, 26);
            UIHelper.StyleTextBox(this.txtHoTen);

            this.lblDT.Text = "Điện thoại:";
            this.lblDT.Font = UIHelper.FontSmall;
            this.lblDT.Location = new Point(310, inputY1 + 3);
            this.lblDT.AutoSize = true;

            this.txtDienThoai.Location = new Point(400, inputY1);
            this.txtDienThoai.Size = new Size(150, 26);
            UIHelper.StyleTextBox(this.txtDienThoai);

            this.lblTDN.Text = "Đăng nhập:";
            this.lblTDN.Font = UIHelper.FontSmall;
            this.lblTDN.Location = new Point(16, inputY2 + 3);
            this.lblTDN.AutoSize = true;

            this.txtTenDangNhap.Location = new Point(16 + lblW, inputY2);
            this.txtTenDangNhap.Size = new Size(180, 26);
            UIHelper.StyleTextBox(this.txtTenDangNhap);

            this.lblMK.Text = "Mật khẩu:";
            this.lblMK.Font = UIHelper.FontSmall;
            this.lblMK.Location = new Point(310, inputY2 + 3);
            this.lblMK.AutoSize = true;

            this.txtMatKhau.Location = new Point(400, inputY2);
            this.txtMatKhau.Size = new Size(150, 26);
            this.txtMatKhau.PasswordChar = '●';
            UIHelper.StyleTextBox(this.txtMatKhau);

            this.chkAdmin.Text = "Admin";
            this.chkAdmin.Font = UIHelper.FontRegular;
            this.chkAdmin.Location = new Point(580, inputY2 + 2);
            this.chkAdmin.AutoSize = true;

            // ==============================
            // BUTTONS
            // ==============================
            int btnY = 420;
            this.btnThem.Text = "✚ Thêm";
            this.btnThem.Location = new Point(16, btnY);
            this.btnThem.Size = new Size(110, 36);
            UIHelper.StyleButtonPrimary(this.btnThem);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Text = "✏ Sửa";
            this.btnSua.Location = new Point(136, btnY);
            this.btnSua.Size = new Size(110, 36);
            UIHelper.StyleButtonSuccess(this.btnSua);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Text = "🗑 Xóa";
            this.btnXoa.Location = new Point(256, btnY);
            this.btnXoa.Size = new Size(110, 36);
            UIHelper.StyleButtonDanger(this.btnXoa);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.btnLamMoi.Text = "↻ Làm mới";
            this.btnLamMoi.Location = new Point(376, btnY);
            this.btnLamMoi.Size = new Size(110, 36);
            UIHelper.StyleButtonSecondary(this.btnLamMoi);
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);

            // ==============================
            // FORM
            // ==============================
            this.ClientSize = new Size(790, 480);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.dgvNhanVien);
            this.Controls.Add(this.lblHoTen); this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblDT); this.Controls.Add(this.txtDienThoai);
            this.Controls.Add(this.lblTDN); this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lblMK); this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.chkAdmin);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnLamMoi);
            this.BackColor = UIHelper.ContentBg;
            this.Name = "frmNhanVien";
            this.Text = "Quản Lý Nhân Viên";
            this.Load += new System.EventHandler(this.frmNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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
