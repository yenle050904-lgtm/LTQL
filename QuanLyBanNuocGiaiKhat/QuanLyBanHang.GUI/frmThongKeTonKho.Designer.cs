namespace QuanLyBanHang.GUI
{
    partial class frmThongKeTonKho
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gpbTimKiemVaLoc = new System.Windows.Forms.GroupBox();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.cboLocTonKho = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLoaiSanPham = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gpbDSHTK = new System.Windows.Forms.GroupBox();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.txtTongSoMH = new System.Windows.Forms.TextBox();
            this.txtGiaTriKho = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gpbTimKiemVaLoc.SuspendLayout();
            this.gpbDSHTK.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // gpbTimKiemVaLoc
            // 
            this.gpbTimKiemVaLoc.BackColor = System.Drawing.Color.AliceBlue;
            this.gpbTimKiemVaLoc.Controls.Add(this.btnXuatExcel);
            this.gpbTimKiemVaLoc.Controls.Add(this.btnTimKiem);
            this.gpbTimKiemVaLoc.Controls.Add(this.cboLocTonKho);
            this.gpbTimKiemVaLoc.Controls.Add(this.label3);
            this.gpbTimKiemVaLoc.Controls.Add(this.cboLoaiSanPham);
            this.gpbTimKiemVaLoc.Controls.Add(this.label1);
            this.gpbTimKiemVaLoc.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpbTimKiemVaLoc.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbTimKiemVaLoc.Location = new System.Drawing.Point(0, 0);
            this.gpbTimKiemVaLoc.Name = "gpbTimKiemVaLoc";
            this.gpbTimKiemVaLoc.Size = new System.Drawing.Size(800, 171);
            this.gpbTimKiemVaLoc.TabIndex = 0;
            this.gpbTimKiemVaLoc.TabStop = false;
            this.gpbTimKiemVaLoc.Text = "Tìm kiếm và lọc";
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.SteelBlue;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(504, 104);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(101, 37);
            this.btnXuatExcel.TabIndex = 7;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click_1);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.SteelBlue;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(657, 104);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(98, 39);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click_1);
            // 
            // cboLocTonKho
            // 
            this.cboLocTonKho.FormattingEnabled = true;
            this.cboLocTonKho.Location = new System.Drawing.Point(156, 112);
            this.cboLocTonKho.Name = "cboLocTonKho";
            this.cboLocTonKho.Size = new System.Drawing.Size(273, 31);
            this.cboLocTonKho.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Lọc tồn kho:";
            // 
            // cboLoaiSanPham
            // 
            this.cboLoaiSanPham.FormattingEnabled = true;
            this.cboLoaiSanPham.Location = new System.Drawing.Point(185, 43);
            this.cboLoaiSanPham.Name = "cboLoaiSanPham";
            this.cboLoaiSanPham.Size = new System.Drawing.Size(321, 31);
            this.cboLoaiSanPham.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Loại sản phẩm:";
            // 
            // gpbDSHTK
            // 
            this.gpbDSHTK.Controls.Add(this.dgvTonKho);
            this.gpbDSHTK.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpbDSHTK.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbDSHTK.Location = new System.Drawing.Point(0, 171);
            this.gpbDSHTK.Name = "gpbDSHTK";
            this.gpbDSHTK.Size = new System.Drawing.Size(800, 190);
            this.gpbDSHTK.TabIndex = 1;
            this.gpbDSHTK.TabStop = false;
            this.gpbDSHTK.Text = "Danh sách hàng tồn kho";
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTonKho.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTonKho.Location = new System.Drawing.Point(3, 26);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.RowHeadersWidth = 51;
            this.dgvTonKho.RowTemplate.Height = 24;
            this.dgvTonKho.Size = new System.Drawing.Size(794, 161);
            this.dgvTonKho.TabIndex = 0;
            // 
            // txtTongSoMH
            // 
            this.txtTongSoMH.BackColor = System.Drawing.Color.White;
            this.txtTongSoMH.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongSoMH.Location = new System.Drawing.Point(185, 397);
            this.txtTongSoMH.Name = "txtTongSoMH";
            this.txtTongSoMH.ReadOnly = true;
            this.txtTongSoMH.Size = new System.Drawing.Size(193, 30);
            this.txtTongSoMH.TabIndex = 10;
            this.txtTongSoMH.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGiaTriKho
            // 
            this.txtGiaTriKho.BackColor = System.Drawing.Color.LightGray;
            this.txtGiaTriKho.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGiaTriKho.Location = new System.Drawing.Point(543, 397);
            this.txtGiaTriKho.Name = "txtGiaTriKho";
            this.txtGiaTriKho.ReadOnly = true;
            this.txtGiaTriKho.Size = new System.Drawing.Size(222, 30);
            this.txtGiaTriKho.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(401, 404);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 23);
            this.label6.TabIndex = 8;
            this.label6.Text = "Tổng giá trị kho:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 404);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tổng số mặt hàng:";
            // 
            // frmThongKeTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtTongSoMH);
            this.Controls.Add(this.txtGiaTriKho);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gpbDSHTK);
            this.Controls.Add(this.gpbTimKiemVaLoc);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThongKeTonKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê tồn kho";
            this.Load += new System.EventHandler(this.frmThongKeTonKho_Load_1);
            this.gpbTimKiemVaLoc.ResumeLayout(false);
            this.gpbTimKiemVaLoc.PerformLayout();
            this.gpbDSHTK.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbTimKiemVaLoc;
        private System.Windows.Forms.GroupBox gpbDSHTK;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLoaiSanPham;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.ComboBox cboLocTonKho;
        private System.Windows.Forms.DataGridView dgvTonKho;
        private System.Windows.Forms.TextBox txtTongSoMH;
        private System.Windows.Forms.TextBox txtGiaTriKho;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
    }
}