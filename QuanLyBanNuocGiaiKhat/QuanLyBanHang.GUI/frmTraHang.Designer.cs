namespace QuanLyBanHang.GUI
{
    partial class frmTraHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.grpHoaDonGoc = new System.Windows.Forms.GroupBox();
            this.btnTimHoaDon = new System.Windows.Forms.Button();
            this.txtTongHD = new System.Windows.Forms.TextBox();
            this.lblTongHD = new System.Windows.Forms.Label();
            this.txtKhachHD = new System.Windows.Forms.TextBox();
            this.lblKhachHD = new System.Windows.Forms.Label();
            this.txtNgayHD = new System.Windows.Forms.TextBox();
            this.lblNgayHD = new System.Windows.Forms.Label();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.lblMaHD = new System.Windows.Forms.Label();
            this.lblChiTiet = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.grpThongTinTra = new System.Windows.Forms.GroupBox();
            this.txtTongHoan = new System.Windows.Forms.TextBox();
            this.lblTongHoan = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.btnLuuPhieu = new System.Windows.Forms.Button();
            this.lblLichSu = new System.Windows.Forms.Label();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.grpHoaDonGoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.grpThongTinTra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(14, 16);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(297, 32);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TRẢ HÀNG / HOÀN TIỀN";
            // 
            // grpHoaDonGoc
            // 
            this.grpHoaDonGoc.Controls.Add(this.btnTimHoaDon);
            this.grpHoaDonGoc.Controls.Add(this.txtTongHD);
            this.grpHoaDonGoc.Controls.Add(this.lblTongHD);
            this.grpHoaDonGoc.Controls.Add(this.txtKhachHD);
            this.grpHoaDonGoc.Controls.Add(this.lblKhachHD);
            this.grpHoaDonGoc.Controls.Add(this.txtNgayHD);
            this.grpHoaDonGoc.Controls.Add(this.lblNgayHD);
            this.grpHoaDonGoc.Controls.Add(this.txtMaHD);
            this.grpHoaDonGoc.Controls.Add(this.lblMaHD);
            this.grpHoaDonGoc.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.grpHoaDonGoc.Location = new System.Drawing.Point(14, 59);
            this.grpHoaDonGoc.Name = "grpHoaDonGoc";
            this.grpHoaDonGoc.Size = new System.Drawing.Size(1120, 107);
            this.grpHoaDonGoc.TabIndex = 1;
            this.grpHoaDonGoc.TabStop = false;
            this.grpHoaDonGoc.Text = "Hóa đơn gốc";
            // 
            // btnTimHoaDon
            // 
            this.btnTimHoaDon.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTimHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTimHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnTimHoaDon.Location = new System.Drawing.Point(229, 62);
            this.btnTimHoaDon.Name = "btnTimHoaDon";
            this.btnTimHoaDon.Size = new System.Drawing.Size(114, 30);
            this.btnTimHoaDon.TabIndex = 2;
            this.btnTimHoaDon.Text = "Tìm hóa đơn";
            this.btnTimHoaDon.UseVisualStyleBackColor = false;
            this.btnTimHoaDon.Click += new System.EventHandler(this.btnTimHoaDon_Click);
            // 
            // txtTongHD
            // 
            this.txtTongHD.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.txtTongHD.ForeColor = System.Drawing.Color.Red;
            this.txtTongHD.Location = new System.Drawing.Point(903, 62);
            this.txtTongHD.Name = "txtTongHD";
            this.txtTongHD.ReadOnly = true;
            this.txtTongHD.Size = new System.Drawing.Size(194, 27);
            this.txtTongHD.TabIndex = 8;
            this.txtTongHD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongHD
            // 
            this.lblTongHD.AutoSize = true;
            this.lblTongHD.Location = new System.Drawing.Point(794, 67);
            this.lblTongHD.Name = "lblTongHD";
            this.lblTongHD.Size = new System.Drawing.Size(103, 20);
            this.lblTongHD.TabIndex = 7;
            this.lblTongHD.Text = "Tổng HĐ gốc:";
            // 
            // txtKhachHD
            // 
            this.txtKhachHD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtKhachHD.Location = new System.Drawing.Point(583, 62);
            this.txtKhachHD.Name = "txtKhachHD";
            this.txtKhachHD.ReadOnly = true;
            this.txtKhachHD.Size = new System.Drawing.Size(182, 27);
            this.txtKhachHD.TabIndex = 6;
            // 
            // lblKhachHD
            // 
            this.lblKhachHD.AutoSize = true;
            this.lblKhachHD.Location = new System.Drawing.Point(512, 67);
            this.lblKhachHD.Name = "lblKhachHD";
            this.lblKhachHD.Size = new System.Drawing.Size(55, 20);
            this.lblKhachHD.TabIndex = 5;
            this.lblKhachHD.Text = "Khách:";
            // 
            // txtNgayHD
            // 
            this.txtNgayHD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNgayHD.Location = new System.Drawing.Point(354, 62);
            this.txtNgayHD.Name = "txtNgayHD";
            this.txtNgayHD.ReadOnly = true;
            this.txtNgayHD.Size = new System.Drawing.Size(131, 27);
            this.txtNgayHD.TabIndex = 4;
            // 
            // lblNgayHD
            // 
            this.lblNgayHD.AutoSize = true;
            this.lblNgayHD.Location = new System.Drawing.Point(229, 30);
            this.lblNgayHD.Name = "lblNgayHD";
            this.lblNgayHD.Size = new System.Drawing.Size(50, 20);
            this.lblNgayHD.TabIndex = 3;
            this.lblNgayHD.Text = "Ngày:";
            // 
            // txtMaHD
            // 
            this.txtMaHD.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.txtMaHD.Location = new System.Drawing.Point(97, 27);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(114, 30);
            this.txtMaHD.TabIndex = 1;
            // 
            // lblMaHD
            // 
            this.lblMaHD.AutoSize = true;
            this.lblMaHD.Location = new System.Drawing.Point(17, 30);
            this.lblMaHD.Name = "lblMaHD";
            this.lblMaHD.Size = new System.Drawing.Size(61, 20);
            this.lblMaHD.TabIndex = 0;
            this.lblMaHD.Text = "Mã HĐ:";
            // 
            // lblChiTiet
            // 
            this.lblChiTiet.AutoSize = true;
            this.lblChiTiet.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblChiTiet.Location = new System.Drawing.Point(14, 181);
            this.lblChiTiet.Name = "lblChiTiet";
            this.lblChiTiet.Size = new System.Drawing.Size(377, 20);
            this.lblChiTiet.TabIndex = 2;
            this.lblChiTiet.Text = "Chi tiết HĐ (nhập số lượng vào cột \'SL Trả\' màu vàng):";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AllowUserToDeleteRows = false;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvChiTiet.Location = new System.Drawing.Point(14, 203);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 25;
            this.dgvChiTiet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTiet.Size = new System.Drawing.Size(1120, 213);
            this.dgvChiTiet.TabIndex = 3;
            this.dgvChiTiet.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellEndEdit);
            this.dgvChiTiet.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvChiTiet_CellValueChanged);
            // 
            // grpThongTinTra
            // 
            this.grpThongTinTra.BackColor = System.Drawing.Color.AliceBlue;
            this.grpThongTinTra.Controls.Add(this.txtTongHoan);
            this.grpThongTinTra.Controls.Add(this.lblTongHoan);
            this.grpThongTinTra.Controls.Add(this.txtLyDo);
            this.grpThongTinTra.Controls.Add(this.lblLyDo);
            this.grpThongTinTra.Controls.Add(this.btnLuuPhieu);
            this.grpThongTinTra.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.grpThongTinTra.Location = new System.Drawing.Point(14, 427);
            this.grpThongTinTra.Name = "grpThongTinTra";
            this.grpThongTinTra.Size = new System.Drawing.Size(1120, 128);
            this.grpThongTinTra.TabIndex = 4;
            this.grpThongTinTra.TabStop = false;
            this.grpThongTinTra.Text = "Thông tin phiếu trả";
            // 
            // txtTongHoan
            // 
            this.txtTongHoan.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.txtTongHoan.ForeColor = System.Drawing.Color.Red;
            this.txtTongHoan.Location = new System.Drawing.Point(800, 43);
            this.txtTongHoan.Name = "txtTongHoan";
            this.txtTongHoan.ReadOnly = true;
            this.txtTongHoan.Size = new System.Drawing.Size(297, 32);
            this.txtTongHoan.TabIndex = 3;
            this.txtTongHoan.Text = "0 VNĐ";
            this.txtTongHoan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongHoan
            // 
            this.lblTongHoan.AutoSize = true;
            this.lblTongHoan.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongHoan.Location = new System.Drawing.Point(674, 48);
            this.lblTongHoan.Name = "lblTongHoan";
            this.lblTongHoan.Size = new System.Drawing.Size(116, 23);
            this.lblTongHoan.TabIndex = 2;
            this.lblTongHoan.Text = "TỔNG HOÀN:";
            // 
            // txtLyDo
            // 
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLyDo.Location = new System.Drawing.Point(103, 37);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(537, 69);
            this.txtLyDo.TabIndex = 1;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(17, 43);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(50, 20);
            this.lblLyDo.TabIndex = 0;
            this.lblLyDo.Text = "Lý do:";
            // 
            // btnLuuPhieu
            // 
            this.btnLuuPhieu.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLuuPhieu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuPhieu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Location = new System.Drawing.Point(800, 80);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(297, 37);
            this.btnLuuPhieu.TabIndex = 4;
            this.btnLuuPhieu.Text = "Lưu phiếu trả hàng";
            this.btnLuuPhieu.UseVisualStyleBackColor = false;
            this.btnLuuPhieu.Click += new System.EventHandler(this.btnLuuPhieu_Click);
            // 
            // lblLichSu
            // 
            this.lblLichSu.AutoSize = true;
            this.lblLichSu.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLichSu.Location = new System.Drawing.Point(14, 571);
            this.lblLichSu.Name = "lblLichSu";
            this.lblLichSu.Size = new System.Drawing.Size(248, 20);
            this.lblLichSu.TabIndex = 5;
            this.lblLichSu.Text = "Lịch sử trả hàng (30 ngày gần đây):";
            // 
            // dgvLichSu
            // 
            this.dgvLichSu.AllowUserToAddRows = false;
            this.dgvLichSu.AllowUserToDeleteRows = false;
            this.dgvLichSu.BackgroundColor = System.Drawing.Color.White;
            this.dgvLichSu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLichSu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvLichSu.Location = new System.Drawing.Point(14, 592);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.ReadOnly = true;
            this.dgvLichSu.RowHeadersWidth = 51;
            this.dgvLichSu.RowTemplate.Height = 25;
            this.dgvLichSu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLichSu.Size = new System.Drawing.Size(1120, 160);
            this.dgvLichSu.TabIndex = 6;
            // 
            // frmTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1154, 768);
            this.Controls.Add(this.dgvLichSu);
            this.Controls.Add(this.lblLichSu);
            this.Controls.Add(this.grpThongTinTra);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.lblChiTiet);
            this.Controls.Add(this.grpHoaDonGoc);
            this.Controls.Add(this.lblTieuDe);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTraHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trả hàng";
            this.Load += new System.EventHandler(this.frmTraHang_Load);
            this.grpHoaDonGoc.ResumeLayout(false);
            this.grpHoaDonGoc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.grpThongTinTra.ResumeLayout(false);
            this.grpThongTinTra.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.GroupBox grpHoaDonGoc;
        private System.Windows.Forms.Button btnTimHoaDon;
        private System.Windows.Forms.TextBox txtTongHD;
        private System.Windows.Forms.Label lblTongHD;
        private System.Windows.Forms.TextBox txtKhachHD;
        private System.Windows.Forms.Label lblKhachHD;
        private System.Windows.Forms.TextBox txtNgayHD;
        private System.Windows.Forms.Label lblNgayHD;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label lblMaHD;
        private System.Windows.Forms.Label lblChiTiet;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.GroupBox grpThongTinTra;
        private System.Windows.Forms.TextBox txtTongHoan;
        private System.Windows.Forms.Label lblTongHoan;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Label lblLyDo;
        private System.Windows.Forms.Button btnLuuPhieu;
        private System.Windows.Forms.Label lblLichSu;
        private System.Windows.Forms.DataGridView dgvLichSu;
    }
}
