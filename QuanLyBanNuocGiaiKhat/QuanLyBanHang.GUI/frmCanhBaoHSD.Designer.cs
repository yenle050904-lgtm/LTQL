namespace QuanLyBanHang.GUI
{
    partial class frmCanhBaoHSD
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
            this.lblLoc = new System.Windows.Forms.Label();
            this.cboLoc = new System.Windows.Forms.ComboBox();
            this.lblSoNgay = new System.Windows.Forms.Label();
            this.nudSoNgay = new System.Windows.Forms.NumericUpDown();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.dgvHSD = new System.Windows.Forms.DataGridView();
            this.lblThongKe = new System.Windows.Forms.Label();
            this.lblChuThich = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHSD)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(14, 16);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(318, 32);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CẢNH BÁO HẠN SỬ DỤNG";
            // 
            // lblLoc
            // 
            this.lblLoc.AutoSize = true;
            this.lblLoc.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoc.Location = new System.Drawing.Point(14, 64);
            this.lblLoc.Name = "lblLoc";
            this.lblLoc.Size = new System.Drawing.Size(71, 20);
            this.lblLoc.TabIndex = 1;
            this.lblLoc.Text = "Lọc theo:";
            // 
            // cboLoc
            // 
            this.cboLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLoc.Location = new System.Drawing.Point(97, 61);
            this.cboLoc.Name = "cboLoc";
            this.cboLoc.Size = new System.Drawing.Size(205, 28);
            this.cboLoc.TabIndex = 2;
            this.cboLoc.SelectedIndexChanged += new System.EventHandler(this.cboLoc_SelectedIndexChanged);
            // 
            // lblSoNgay
            // 
            this.lblSoNgay.AutoSize = true;
            this.lblSoNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoNgay.Location = new System.Drawing.Point(326, 64);
            this.lblSoNgay.Name = "lblSoNgay";
            this.lblSoNgay.Size = new System.Drawing.Size(168, 20);
            this.lblSoNgay.TabIndex = 3;
            this.lblSoNgay.Text = "Cảnh báo trong (ngày):";
            // 
            // nudSoNgay
            // 
            this.nudSoNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.nudSoNgay.Location = new System.Drawing.Point(520, 61);
            this.nudSoNgay.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.nudSoNgay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoNgay.Name = "nudSoNgay";
            this.nudSoNgay.Size = new System.Drawing.Size(91, 27);
            this.nudSoNgay.TabIndex = 4;
            this.nudSoNgay.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudSoNgay.ValueChanged += new System.EventHandler(this.nudSoNgay_ValueChanged);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.SteelBlue;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(634, 59);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(103, 30);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // dgvHSD
            // 
            this.dgvHSD.AllowUserToAddRows = false;
            this.dgvHSD.AllowUserToDeleteRows = false;
            this.dgvHSD.BackgroundColor = System.Drawing.Color.White;
            this.dgvHSD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHSD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvHSD.Location = new System.Drawing.Point(14, 107);
            this.dgvHSD.Name = "dgvHSD";
            this.dgvHSD.ReadOnly = true;
            this.dgvHSD.RowHeadersWidth = 51;
            this.dgvHSD.RowTemplate.Height = 25;
            this.dgvHSD.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHSD.Size = new System.Drawing.Size(1086, 427);
            this.dgvHSD.TabIndex = 6;
            // 
            // lblThongKe
            // 
            this.lblThongKe.AutoSize = true;
            this.lblThongKe.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblThongKe.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblThongKe.Location = new System.Drawing.Point(14, 555);
            this.lblThongKe.Name = "lblThongKe";
            this.lblThongKe.Size = new System.Drawing.Size(86, 23);
            this.lblThongKe.TabIndex = 7;
            this.lblThongKe.Text = "Tổng: 0 lô";
            // 
            // lblChuThich
            // 
            this.lblChuThich.AutoSize = true;
            this.lblChuThich.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblChuThich.ForeColor = System.Drawing.Color.Gray;
            this.lblChuThich.Location = new System.Drawing.Point(14, 581);
            this.lblChuThich.Name = "lblChuThich";
            this.lblChuThich.Size = new System.Drawing.Size(419, 20);
            this.lblChuThich.TabIndex = 8;
            this.lblChuThich.Text = "Chú thích: Đỏ = đã hết hạn, Cam = ≤ 7 ngày, Vàng = ≤ 30 ngày";
            // 
            // frmCanhBaoHSD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1120, 619);
            this.Controls.Add(this.lblChuThich);
            this.Controls.Add(this.lblThongKe);
            this.Controls.Add(this.dgvHSD);
            this.Controls.Add(this.btnLamMoi);
            this.Controls.Add(this.nudSoNgay);
            this.Controls.Add(this.lblSoNgay);
            this.Controls.Add(this.cboLoc);
            this.Controls.Add(this.lblLoc);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCanhBaoHSD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cảnh báo hạn sử dụng";
            this.Load += new System.EventHandler(this.frmCanhBaoHSD_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSoNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHSD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblLoc;
        private System.Windows.Forms.ComboBox cboLoc;
        private System.Windows.Forms.Label lblSoNgay;
        private System.Windows.Forms.NumericUpDown nudSoNgay;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.DataGridView dgvHSD;
        private System.Windows.Forms.Label lblThongKe;
        private System.Windows.Forms.Label lblChuThich;
    }
}
