namespace QuanLyBanHang.GUI
{
    partial class frmBaoCaoLaiLo
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
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnXem = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.dgvLaiLo = new System.Windows.Forms.DataGridView();
            this.grpTongQuan = new System.Windows.Forms.GroupBox();
            this.txtSoHD = new System.Windows.Forms.TextBox();
            this.lblSoHD = new System.Windows.Forms.Label();
            this.txtTongLoiNhuan = new System.Windows.Forms.TextBox();
            this.lblTongLoiNhuan = new System.Windows.Forms.Label();
            this.txtTongChiPhi = new System.Windows.Forms.TextBox();
            this.lblTongChiPhi = new System.Windows.Forms.Label();
            this.txtTongDoanhThu = new System.Windows.Forms.TextBox();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaiLo)).BeginInit();
            this.grpTongQuan.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(14, 16);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(206, 32);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "BÁO CÁO LÃI/LỖ";
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblTuNgay.Location = new System.Drawing.Point(14, 64);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(68, 20);
            this.lblTuNgay.TabIndex = 1;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(97, 61);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(148, 27);
            this.dtpTuNgay.TabIndex = 2;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.lblDenNgay.Location = new System.Drawing.Point(263, 64);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(79, 20);
            this.lblDenNgay.TabIndex = 3;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(354, 61);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(148, 27);
            this.dtpDenNgay.TabIndex = 4;
            // 
            // btnXem
            // 
            this.btnXem.BackColor = System.Drawing.Color.SteelBlue;
            this.btnXem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXem.ForeColor = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(534, 58);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(114, 30);
            this.btnXem.TabIndex = 5;
            this.btnXem.Text = "Xem";
            this.btnXem.UseVisualStyleBackColor = false;
            this.btnXem.Click += new System.EventHandler(this.btnXem_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnXuatExcel.FlatAppearance.BorderSize = 0;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(682, 59);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(114, 30);
            this.btnXuatExcel.TabIndex = 6;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // dgvLaiLo
            // 
            this.dgvLaiLo.AllowUserToAddRows = false;
            this.dgvLaiLo.AllowUserToDeleteRows = false;
            this.dgvLaiLo.BackgroundColor = System.Drawing.Color.White;
            this.dgvLaiLo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLaiLo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvLaiLo.Location = new System.Drawing.Point(14, 107);
            this.dgvLaiLo.Name = "dgvLaiLo";
            this.dgvLaiLo.ReadOnly = true;
            this.dgvLaiLo.RowHeadersWidth = 51;
            this.dgvLaiLo.RowTemplate.Height = 25;
            this.dgvLaiLo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLaiLo.Size = new System.Drawing.Size(1154, 363);
            this.dgvLaiLo.TabIndex = 7;
            // 
            // grpTongQuan
            // 
            this.grpTongQuan.Controls.Add(this.txtSoHD);
            this.grpTongQuan.Controls.Add(this.lblSoHD);
            this.grpTongQuan.Controls.Add(this.txtTongLoiNhuan);
            this.grpTongQuan.Controls.Add(this.lblTongLoiNhuan);
            this.grpTongQuan.Controls.Add(this.txtTongChiPhi);
            this.grpTongQuan.Controls.Add(this.lblTongChiPhi);
            this.grpTongQuan.Controls.Add(this.txtTongDoanhThu);
            this.grpTongQuan.Controls.Add(this.lblTongDoanhThu);
            this.grpTongQuan.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.grpTongQuan.Location = new System.Drawing.Point(14, 485);
            this.grpTongQuan.Name = "grpTongQuan";
            this.grpTongQuan.Size = new System.Drawing.Size(1154, 128);
            this.grpTongQuan.TabIndex = 8;
            this.grpTongQuan.TabStop = false;
            this.grpTongQuan.Text = "Tổng quan";
            // 
            // txtSoHD
            // 
            this.txtSoHD.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.txtSoHD.Location = new System.Drawing.Point(731, 43);
            this.txtSoHD.Name = "txtSoHD";
            this.txtSoHD.ReadOnly = true;
            this.txtSoHD.Size = new System.Drawing.Size(228, 30);
            this.txtSoHD.TabIndex = 7;
            this.txtSoHD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSoHD
            // 
            this.lblSoHD.AutoSize = true;
            this.lblSoHD.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoHD.Location = new System.Drawing.Point(600, 48);
            this.lblSoHD.Name = "lblSoHD";
            this.lblSoHD.Size = new System.Drawing.Size(102, 23);
            this.lblSoHD.TabIndex = 6;
            this.lblSoHD.Text = "Số hóa đơn:";
            // 
            // txtTongLoiNhuan
            // 
            this.txtTongLoiNhuan.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.txtTongLoiNhuan.ForeColor = System.Drawing.Color.Green;
            this.txtTongLoiNhuan.Location = new System.Drawing.Point(731, 85);
            this.txtTongLoiNhuan.Name = "txtTongLoiNhuan";
            this.txtTongLoiNhuan.ReadOnly = true;
            this.txtTongLoiNhuan.Size = new System.Drawing.Size(228, 32);
            this.txtTongLoiNhuan.TabIndex = 5;
            this.txtTongLoiNhuan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongLoiNhuan
            // 
            this.lblTongLoiNhuan.AutoSize = true;
            this.lblTongLoiNhuan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongLoiNhuan.Location = new System.Drawing.Point(594, 91);
            this.lblTongLoiNhuan.Name = "lblTongLoiNhuan";
            this.lblTongLoiNhuan.Size = new System.Drawing.Size(110, 23);
            this.lblTongLoiNhuan.TabIndex = 4;
            this.lblTongLoiNhuan.Text = "LỢI NHUẬN:";
            // 
            // txtTongChiPhi
            // 
            this.txtTongChiPhi.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongChiPhi.ForeColor = System.Drawing.Color.OrangeRed;
            this.txtTongChiPhi.Location = new System.Drawing.Point(171, 85);
            this.txtTongChiPhi.Name = "txtTongChiPhi";
            this.txtTongChiPhi.ReadOnly = true;
            this.txtTongChiPhi.Size = new System.Drawing.Size(228, 30);
            this.txtTongChiPhi.TabIndex = 3;
            this.txtTongChiPhi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongChiPhi
            // 
            this.lblTongChiPhi.AutoSize = true;
            this.lblTongChiPhi.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongChiPhi.Location = new System.Drawing.Point(17, 91);
            this.lblTongChiPhi.Name = "lblTongChiPhi";
            this.lblTongChiPhi.Size = new System.Drawing.Size(109, 23);
            this.lblTongChiPhi.TabIndex = 2;
            this.lblTongChiPhi.Text = "Tổng chi phí:";
            // 
            // txtTongDoanhThu
            // 
            this.txtTongDoanhThu.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongDoanhThu.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtTongDoanhThu.Location = new System.Drawing.Point(171, 43);
            this.txtTongDoanhThu.Name = "txtTongDoanhThu";
            this.txtTongDoanhThu.ReadOnly = true;
            this.txtTongDoanhThu.Size = new System.Drawing.Size(228, 30);
            this.txtTongDoanhThu.TabIndex = 1;
            this.txtTongDoanhThu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongDoanhThu.Location = new System.Drawing.Point(17, 48);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(138, 23);
            this.lblTongDoanhThu.TabIndex = 0;
            this.lblTongDoanhThu.Text = "Tổng doanh thu:";
            // 
            // frmBaoCaoLaiLo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1189, 640);
            this.Controls.Add(this.grpTongQuan);
            this.Controls.Add(this.dgvLaiLo);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.lblDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.lblTuNgay);
            this.Controls.Add(this.lblTieuDe);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBaoCaoLaiLo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo Lãi/Lỗ";
            this.Load += new System.EventHandler(this.frmBaoCaoLaiLo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLaiLo)).EndInit();
            this.grpTongQuan.ResumeLayout(false);
            this.grpTongQuan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnXem;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.DataGridView dgvLaiLo;
        private System.Windows.Forms.GroupBox grpTongQuan;
        private System.Windows.Forms.TextBox txtSoHD;
        private System.Windows.Forms.Label lblSoHD;
        private System.Windows.Forms.TextBox txtTongLoiNhuan;
        private System.Windows.Forms.Label lblTongLoiNhuan;
        private System.Windows.Forms.TextBox txtTongChiPhi;
        private System.Windows.Forms.Label lblTongChiPhi;
        private System.Windows.Forms.TextBox txtTongDoanhThu;
        private System.Windows.Forms.Label lblTongDoanhThu;
    }
}
