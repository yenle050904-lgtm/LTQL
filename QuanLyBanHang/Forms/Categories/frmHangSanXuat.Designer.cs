using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    partial class frmHangSanXuat
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new ErrorProvider(this.components);
            this.dgvHangSX = new DataGridView();
            this.txtTenHang = new TextBox();
            this.lblTen = new Label();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvHangSX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            this.dgvHangSX.Location = new Point(16, 16);
            this.dgvHangSX.Size = new Size(500, 250);
            this.dgvHangSX.CellClick += new DataGridViewCellEventHandler(this.dgvHangSX_CellClick);

            this.lblTen.Text = "Tên hãng:"; this.lblTen.Font = UIHelper.FontSmall;
            this.lblTen.Location = new Point(16, 290); this.lblTen.AutoSize = true;
            this.txtTenHang.Location = new Point(90, 287); this.txtTenHang.Size = new Size(250, 26);
            UIHelper.StyleTextBox(this.txtTenHang);

            int btnY = 330;
            this.btnThem.Text = "✚ Thêm"; this.btnThem.Location = new Point(16, btnY);
            this.btnThem.Size = new Size(100, 36); UIHelper.StyleButtonPrimary(this.btnThem);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);

            this.btnSua.Text = "✏ Sửa"; this.btnSua.Location = new Point(126, btnY);
            this.btnSua.Size = new Size(100, 36); UIHelper.StyleButtonSuccess(this.btnSua);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);

            this.btnXoa.Text = "🗑 Xóa"; this.btnXoa.Location = new Point(236, btnY);
            this.btnXoa.Size = new Size(100, 36); UIHelper.StyleButtonDanger(this.btnXoa);
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);

            this.ClientSize = new Size(540, 390);
            this.Controls.Add(dgvHangSX); this.Controls.Add(lblTen); this.Controls.Add(txtTenHang);
            this.Controls.Add(btnThem); this.Controls.Add(btnSua); this.Controls.Add(btnXoa);
            this.BackColor = UIHelper.ContentBg;
            this.Name = "frmHangSanXuat"; this.Text = "Quản Lý Hãng Sản Xuất";
            this.Load += new System.EventHandler(this.frmHangSanXuat_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHangSX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }

        private ErrorProvider errorProvider;
        private DataGridView dgvHangSX;
        private TextBox txtTenHang;
        private Label lblTen;
        private Button btnThem, btnSua, btnXoa;
    }
}
