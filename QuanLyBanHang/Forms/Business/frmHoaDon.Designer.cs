using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Business
{
    partial class frmHoaDon
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider = new ErrorProvider(this.components);
            this.cboNhanVien = new ComboBox();
            this.cboKhachHang = new ComboBox();
            this.dtpNgayLap = new DateTimePicker();
            this.txtGhiChu = new TextBox();
            this.cboSanPham = new ComboBox();
            this.txtSoLuongBan = new TextBox();
            this.btnThemChiTiet = new Button();
            this.btnXoaChiTiet = new Button();
            this.dgvChiTiet = new DataGridView();
            this.btnLuuHoaDon = new Button();
            this.lblTongTien = new Label();
            this.lblNV = new Label();
            this.lblKH = new Label();
            this.lblNgay = new Label();
            this.lblSP = new Label();
            this.lblSL = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();

            // ==============================
            // THÔNG TIN HÓA ĐƠN (hàng 1, 2)
            // ==============================
            int y1 = 16, y2 = 56;

            this.lblNV.Text = "Nhân viên:"; this.lblNV.Font = UIHelper.FontSmall;
            this.lblNV.Location = new Point(16, y1 + 3); this.lblNV.AutoSize = true;
            this.cboNhanVien.Location = new Point(100, y1); this.cboNhanVien.Size = new Size(180, 26);
            UIHelper.StyleComboBox(this.cboNhanVien);

            this.lblKH.Text = "Khách hàng:"; this.lblKH.Font = UIHelper.FontSmall;
            this.lblKH.Location = new Point(300, y1 + 3); this.lblKH.AutoSize = true;
            this.cboKhachHang.Location = new Point(400, y1); this.cboKhachHang.Size = new Size(180, 26);
            UIHelper.StyleComboBox(this.cboKhachHang);

            this.lblNgay.Text = "Ngày lập:"; this.lblNgay.Font = UIHelper.FontSmall;
            this.lblNgay.Location = new Point(600, y1 + 3); this.lblNgay.AutoSize = true;
            this.dtpNgayLap.Location = new Point(670, y1); this.dtpNgayLap.Size = new Size(150, 26);

            this.txtGhiChu.Location = new Point(16, y2); this.txtGhiChu.Size = new Size(804, 26);
            this.txtGhiChu.PlaceholderText = "Ghi chú hóa đơn (tùy chọn)";
            UIHelper.StyleTextBox(this.txtGhiChu);

            // ==============================
            // THÊM CHI TIẾT (hàng 3)
            // ==============================
            int y3 = 100;
            this.lblSP.Text = "Sản phẩm:"; this.lblSP.Font = UIHelper.FontSmall;
            this.lblSP.Location = new Point(16, y3 + 3); this.lblSP.AutoSize = true;
            this.cboSanPham.Location = new Point(100, y3); this.cboSanPham.Size = new Size(250, 26);
            UIHelper.StyleComboBox(this.cboSanPham);

            this.lblSL.Text = "Số lượng:"; this.lblSL.Font = UIHelper.FontSmall;
            this.lblSL.Location = new Point(370, y3 + 3); this.lblSL.AutoSize = true;
            this.txtSoLuongBan.Location = new Point(440, y3); this.txtSoLuongBan.Size = new Size(80, 26);
            UIHelper.StyleTextBox(this.txtSoLuongBan);

            this.btnThemChiTiet.Text = "✚ Thêm CT";
            this.btnThemChiTiet.Location = new Point(540, y3 - 2);
            this.btnThemChiTiet.Size = new Size(110, 32);
            UIHelper.StyleButtonPrimary(this.btnThemChiTiet);
            this.btnThemChiTiet.Click += new System.EventHandler(this.btnThemChiTiet_Click);

            this.btnXoaChiTiet.Text = "🗑 Xóa dòng";
            this.btnXoaChiTiet.Location = new Point(660, y3 - 2);
            this.btnXoaChiTiet.Size = new Size(110, 32);
            UIHelper.StyleButtonDanger(this.btnXoaChiTiet);
            this.btnXoaChiTiet.Click += new System.EventHandler(this.btnXoaChiTiet_Click);

            // ==============================
            // DATAGRIDVIEW CHI TIẾT
            // ==============================
            this.dgvChiTiet.Location = new Point(16, 145);
            this.dgvChiTiet.Size = new Size(804, 230);
            this.dgvChiTiet.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // ==============================
            // TỔNG TIỀN + LƯU
            // ==============================
            this.lblTongTien.Text = "💰 Tổng tiền: 0 VNĐ";
            this.lblTongTien.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.lblTongTien.ForeColor = UIHelper.AccentBlue;
            this.lblTongTien.Location = new Point(16, 390);
            this.lblTongTien.AutoSize = true;

            this.btnLuuHoaDon.Text = "💾 Lưu Hóa Đơn";
            this.btnLuuHoaDon.Location = new Point(640, 385);
            this.btnLuuHoaDon.Size = new Size(180, 40);
            UIHelper.StyleButtonSuccess(this.btnLuuHoaDon);
            this.btnLuuHoaDon.Click += new System.EventHandler(this.btnLuuHoaDon_Click);

            // ==============================
            // FORM
            // ==============================
            this.ClientSize = new Size(840, 445);
            this.Controls.Add(lblNV); this.Controls.Add(cboNhanVien);
            this.Controls.Add(lblKH); this.Controls.Add(cboKhachHang);
            this.Controls.Add(lblNgay); this.Controls.Add(dtpNgayLap);
            this.Controls.Add(txtGhiChu);
            this.Controls.Add(lblSP); this.Controls.Add(cboSanPham);
            this.Controls.Add(lblSL); this.Controls.Add(txtSoLuongBan);
            this.Controls.Add(btnThemChiTiet); this.Controls.Add(btnXoaChiTiet);
            this.Controls.Add(dgvChiTiet);
            this.Controls.Add(lblTongTien); this.Controls.Add(btnLuuHoaDon);
            this.BackColor = UIHelper.ContentBg;
            this.Name = "frmHoaDon"; this.Text = "Lập Hóa Đơn Bán Hàng";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false); this.PerformLayout();
        }

        private ErrorProvider errorProvider;
        private ComboBox cboNhanVien, cboKhachHang, cboSanPham;
        private DateTimePicker dtpNgayLap;
        private TextBox txtGhiChu, txtSoLuongBan;
        private Button btnThemChiTiet, btnXoaChiTiet, btnLuuHoaDon;
        private DataGridView dgvChiTiet;
        private Label lblTongTien, lblNV, lblKH, lblNgay, lblSP, lblSL;
    }
}
