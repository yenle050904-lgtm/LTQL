namespace QuanLyBanHang.GUI
{
    partial class frmInHoaDon
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
            this.rtbHoaDon = new System.Windows.Forms.RichTextBox();
            this.btnIn = new System.Windows.Forms.Button();
            this.btnXemTruoc = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.AutoSize = true;
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblTieuDe.Location = new System.Drawing.Point(14, 16);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(164, 32);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "IN HÓA ĐƠN";
            // 
            // rtbHoaDon
            // 
            this.rtbHoaDon.BackColor = System.Drawing.Color.White;
            this.rtbHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbHoaDon.Font = new System.Drawing.Font("Consolas", 10F);
            this.rtbHoaDon.Location = new System.Drawing.Point(14, 59);
            this.rtbHoaDon.Name = "rtbHoaDon";
            this.rtbHoaDon.ReadOnly = true;
            this.rtbHoaDon.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbHoaDon.Size = new System.Drawing.Size(765, 533);
            this.rtbHoaDon.TabIndex = 1;
            this.rtbHoaDon.Text = "";
            // 
            // btnIn
            // 
            this.btnIn.BackColor = System.Drawing.Color.SteelBlue;
            this.btnIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnIn.ForeColor = System.Drawing.Color.White;
            this.btnIn.Location = new System.Drawing.Point(14, 608);
            this.btnIn.Name = "btnIn";
            this.btnIn.Size = new System.Drawing.Size(114, 34);
            this.btnIn.TabIndex = 2;
            this.btnIn.Text = "In";
            this.btnIn.UseVisualStyleBackColor = false;
            this.btnIn.Click += new System.EventHandler(this.btnIn_Click);
            // 
            // btnXemTruoc
            // 
            this.btnXemTruoc.BackColor = System.Drawing.Color.SteelBlue;
            this.btnXemTruoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemTruoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemTruoc.ForeColor = System.Drawing.Color.White;
            this.btnXemTruoc.Location = new System.Drawing.Point(137, 608);
            this.btnXemTruoc.Name = "btnXemTruoc";
            this.btnXemTruoc.Size = new System.Drawing.Size(137, 34);
            this.btnXemTruoc.TabIndex = 3;
            this.btnXemTruoc.Text = "Xem trước";
            this.btnXemTruoc.UseVisualStyleBackColor = false;
            this.btnXemTruoc.Click += new System.EventHandler(this.btnXemTruoc_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.LightGray;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.Black;
            this.btnDong.Location = new System.Drawing.Point(665, 608);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(114, 34);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmInHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(800, 661);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXemTruoc);
            this.Controls.Add(this.btnIn);
            this.Controls.Add(this.rtbHoaDon);
            this.Controls.Add(this.lblTieuDe);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmInHoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "In hóa đơn";
            this.Load += new System.EventHandler(this.frmInHoaDon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.RichTextBox rtbHoaDon;
        private System.Windows.Forms.Button btnIn;
        private System.Windows.Forms.Button btnXemTruoc;
        private System.Windows.Forms.Button btnDong;
    }
}
