namespace QuanLyBanHang.GUI
{
    partial class frmBanHang
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.cboKhachHang = new System.Windows.Forms.ComboBox();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.btnThemVaoHoaDon = new System.Windows.Forms.Button();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnXoaMon = new System.Windows.Forms.Button();
            this.btnXoaTatCa = new System.Windows.Forms.Button();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.txtTongCong = new System.Windows.Forms.TextBox();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTienGiam = new System.Windows.Forms.TextBox();
            this.txtTamTinh = new System.Windows.Forms.TextBox();
            this.lblKMApDung = new System.Windows.Forms.Label();
            this.btnHuyKM = new System.Windows.Forms.Button();
            this.btnApDungKM = new System.Windows.Forms.Button();
            this.txtMaKM = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvSanPham);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(443, 246);
            this.panel1.TabIndex = 0;
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSanPham.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSanPham.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dgvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanPham.EnableHeadersVisualStyles = false;
            this.dgvSanPham.Location = new System.Drawing.Point(0, 0);
            this.dgvSanPham.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.RowHeadersWidth = 51;
            this.dgvSanPham.RowTemplate.Height = 24;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(443, 246);
            this.dgvSanPham.TabIndex = 0;
            this.dgvSanPham.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSanPham_CellClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvGioHang);
            this.panel2.Location = new System.Drawing.Point(525, 52);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 207);
            this.panel2.TabIndex = 0;
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGioHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvGioHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGioHang.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGioHang.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioHang.Location = new System.Drawing.Point(0, 0);
            this.dgvGioHang.Margin = new System.Windows.Forms.Padding(4);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowHeadersWidth = 51;
            this.dgvGioHang.RowTemplate.Height = 24;
            this.dgvGioHang.Size = new System.Drawing.Size(460, 207);
            this.dgvGioHang.TabIndex = 0;
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.Location = new System.Drawing.Point(596, 13);
            this.cboKhachHang.Margin = new System.Windows.Forms.Padding(4);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(389, 31);
            this.cboKhachHang.TabIndex = 1;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhachHang.Location = new System.Drawing.Point(483, 21);
            this.lblKhachHang.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(105, 23);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Khách hàng:";
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSoLuong.Location = new System.Drawing.Point(13, 276);
            this.lblSoLuong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(83, 23);
            this.lblSoLuong.TabIndex = 0;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // btnThemVaoHoaDon
            // 
            this.btnThemVaoHoaDon.BackColor = System.Drawing.Color.SteelBlue;
            this.btnThemVaoHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemVaoHoaDon.FlatAppearance.BorderSize = 0;
            this.btnThemVaoHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVaoHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnThemVaoHoaDon.Location = new System.Drawing.Point(274, 354);
            this.btnThemVaoHoaDon.Margin = new System.Windows.Forms.Padding(4);
            this.btnThemVaoHoaDon.Name = "btnThemVaoHoaDon";
            this.btnThemVaoHoaDon.Size = new System.Drawing.Size(182, 44);
            this.btnThemVaoHoaDon.TabIndex = 3;
            this.btnThemVaoHoaDon.Text = "Thêm vào hóa đơn";
            this.btnThemVaoHoaDon.UseVisualStyleBackColor = false;
            this.btnThemVaoHoaDon.Click += new System.EventHandler(this.btnThemVaoHoaDon_Click);
            // 
            // nudSoLuong
            // 
            this.nudSoLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.nudSoLuong.Location = new System.Drawing.Point(116, 276);
            this.nudSoLuong.Margin = new System.Windows.Forms.Padding(4);
            this.nudSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(150, 30);
            this.nudSoLuong.TabIndex = 2;
            this.nudSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnXoaMon
            // 
            this.btnXoaMon.BackColor = System.Drawing.Color.LightGray;
            this.btnXoaMon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaMon.FlatAppearance.BorderSize = 0;
            this.btnXoaMon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaMon.ForeColor = System.Drawing.Color.Black;
            this.btnXoaMon.Location = new System.Drawing.Point(17, 354);
            this.btnXoaMon.Margin = new System.Windows.Forms.Padding(4);
            this.btnXoaMon.Name = "btnXoaMon";
            this.btnXoaMon.Size = new System.Drawing.Size(220, 44);
            this.btnXoaMon.TabIndex = 4;
            this.btnXoaMon.Text = "Xóa món đang chọn";
            this.btnXoaMon.UseVisualStyleBackColor = false;
            this.btnXoaMon.Click += new System.EventHandler(this.btnXoaMon_Click);
            // 
            // btnXoaTatCa
            // 
            this.btnXoaTatCa.BackColor = System.Drawing.Color.LightGray;
            this.btnXoaTatCa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaTatCa.FlatAppearance.BorderSize = 0;
            this.btnXoaTatCa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaTatCa.Location = new System.Drawing.Point(17, 445);
            this.btnXoaTatCa.Margin = new System.Windows.Forms.Padding(4);
            this.btnXoaTatCa.Name = "btnXoaTatCa";
            this.btnXoaTatCa.Size = new System.Drawing.Size(220, 46);
            this.btnXoaTatCa.TabIndex = 5;
            this.btnXoaTatCa.Text = "Xóa tất cả";
            this.btnXoaTatCa.UseVisualStyleBackColor = false;
            this.btnXoaTatCa.Click += new System.EventHandler(this.btnXoaTatCa_Click);
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongCong.Location = new System.Drawing.Point(24, 536);
            this.lblTongCong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(199, 31);
            this.lblTongCong.TabIndex = 0;
            this.lblTongCong.Text = "Tổng thanh toán:";
            // 
            // txtTongCong
            // 
            this.txtTongCong.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongCong.ForeColor = System.Drawing.Color.DarkRed;
            this.txtTongCong.Location = new System.Drawing.Point(231, 536);
            this.txtTongCong.Margin = new System.Windows.Forms.Padding(4);
            this.txtTongCong.Name = "txtTongCong";
            this.txtTongCong.ReadOnly = true;
            this.txtTongCong.Size = new System.Drawing.Size(417, 30);
            this.txtTongCong.TabIndex = 6;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.SteelBlue;
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(274, 435);
            this.btnThanhToan.Margin = new System.Windows.Forms.Padding(4);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(182, 56);
            this.btnThanhToan.TabIndex = 7;
            this.btnThanhToan.Text = "Thanh toán và In hóa đơn";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTienGiam);
            this.groupBox1.Controls.Add(this.txtTamTinh);
            this.groupBox1.Controls.Add(this.lblKMApDung);
            this.groupBox1.Controls.Add(this.btnHuyKM);
            this.groupBox1.Controls.Add(this.btnApDungKM);
            this.groupBox1.Controls.Add(this.txtMaKM);
            this.groupBox1.Location = new System.Drawing.Point(487, 276);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 243);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Khuyến mãi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Giảm giá:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Tạm tính:";
            // 
            // txtTienGiam
            // 
            this.txtTienGiam.ForeColor = System.Drawing.Color.Green;
            this.txtTienGiam.Location = new System.Drawing.Point(109, 178);
            this.txtTienGiam.Name = "txtTienGiam";
            this.txtTienGiam.ReadOnly = true;
            this.txtTienGiam.Size = new System.Drawing.Size(116, 30);
            this.txtTienGiam.TabIndex = 5;
            this.txtTienGiam.Text = "0 VNĐ";
            this.txtTienGiam.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTamTinh
            // 
            this.txtTamTinh.Location = new System.Drawing.Point(365, 178);
            this.txtTamTinh.Name = "txtTamTinh";
            this.txtTamTinh.ReadOnly = true;
            this.txtTamTinh.Size = new System.Drawing.Size(116, 30);
            this.txtTamTinh.TabIndex = 4;
            this.txtTamTinh.Text = "0 VNĐ";
            this.txtTamTinh.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblKMApDung
            // 
            this.lblKMApDung.AutoSize = true;
            this.lblKMApDung.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKMApDung.ForeColor = System.Drawing.Color.Green;
            this.lblKMApDung.Location = new System.Drawing.Point(19, 136);
            this.lblKMApDung.Name = "lblKMApDung";
            this.lblKMApDung.Size = new System.Drawing.Size(100, 20);
            this.lblKMApDung.TabIndex = 3;
            this.lblKMApDung.Text = "Chưa áp dụng";
            this.lblKMApDung.Click += new System.EventHandler(this.lblKMApDung_Click);
            // 
            // btnHuyKM
            // 
            this.btnHuyKM.BackColor = System.Drawing.Color.Gray;
            this.btnHuyKM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuyKM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyKM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuyKM.ForeColor = System.Drawing.Color.White;
            this.btnHuyKM.Location = new System.Drawing.Point(335, 64);
            this.btnHuyKM.Name = "btnHuyKM";
            this.btnHuyKM.Size = new System.Drawing.Size(146, 37);
            this.btnHuyKM.TabIndex = 2;
            this.btnHuyKM.Text = "Hủy KM";
            this.btnHuyKM.UseVisualStyleBackColor = false;
            this.btnHuyKM.Click += new System.EventHandler(this.btnHuyKM_Click_1);
            // 
            // btnApDungKM
            // 
            this.btnApDungKM.BackColor = System.Drawing.Color.SteelBlue;
            this.btnApDungKM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApDungKM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApDungKM.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApDungKM.ForeColor = System.Drawing.Color.White;
            this.btnApDungKM.Location = new System.Drawing.Point(173, 62);
            this.btnApDungKM.Name = "btnApDungKM";
            this.btnApDungKM.Size = new System.Drawing.Size(146, 39);
            this.btnApDungKM.TabIndex = 1;
            this.btnApDungKM.Text = "Áp dụng";
            this.btnApDungKM.UseVisualStyleBackColor = false;
            this.btnApDungKM.Click += new System.EventHandler(this.btnApDungKM_Click_1);
            // 
            // txtMaKM
            // 
            this.txtMaKM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMaKM.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaKM.Location = new System.Drawing.Point(23, 71);
            this.txtMaKM.Name = "txtMaKM";
            this.txtMaKM.Size = new System.Drawing.Size(111, 30);
            this.txtMaKM.TabIndex = 0;
            this.txtMaKM.Enter += new System.EventHandler(this.txtMaKM_Enter);
            this.txtMaKM.Leave += new System.EventHandler(this.txtMaKM_Leave);
            // 
            // frmBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(998, 597);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboKhachHang);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.txtTongCong);
            this.Controls.Add(this.lblKhachHang);
            this.Controls.Add(this.lblTongCong);
            this.Controls.Add(this.btnXoaTatCa);
            this.Controls.Add(this.btnXoaMon);
            this.Controls.Add(this.nudSoLuong);
            this.Controls.Add(this.btnThemVaoHoaDon);
            this.Controls.Add(this.lblSoLuong);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bán hàng";
            this.Load += new System.EventHandler(this.frmBanHang_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Button btnThemVaoHoaDon;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.ComboBox cboKhachHang;
        private System.Windows.Forms.Label lblKhachHang;
        private System.Windows.Forms.Button btnXoaMon;
        private System.Windows.Forms.Button btnXoaTatCa;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.TextBox txtTongCong;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApDungKM;
        private System.Windows.Forms.TextBox txtMaKM;
        private System.Windows.Forms.Label lblKMApDung;
        private System.Windows.Forms.Button btnHuyKM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTienGiam;
        private System.Windows.Forms.TextBox txtTamTinh;
    }
}