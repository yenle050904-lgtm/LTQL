using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Systems
{
    partial class frmLogin
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
            this.pnlCard = new Panel();
            this.lblLoginTitle = new Label();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnCancel = new Button();
            this.lblError = new Label();

            this.SuspendLayout();

            // ==============================
            // CARD PANEL (giữa màn hình)
            // ==============================
            this.pnlCard.Size = new Size(380, 340);
            this.pnlCard.BackColor = UIHelper.CardBg;
            this.pnlCard.Padding = new Padding(30);
            // Sẽ căn giữa trong Resize event

            // Title
            this.lblLoginTitle.Text = "🥤 Đăng Nhập Hệ Thống";
            this.lblLoginTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            this.lblLoginTitle.ForeColor = UIHelper.TextPrimary;
            this.lblLoginTitle.Location = new Point(30, 28);
            this.lblLoginTitle.AutoSize = true;

            // Username label
            this.lblUsername.Text = "Tên đăng nhập";
            this.lblUsername.Font = UIHelper.FontSmall;
            this.lblUsername.ForeColor = Color.Gray;
            this.lblUsername.Location = new Point(30, 80);
            this.lblUsername.AutoSize = true;

            // Username textbox
            this.txtUsername.Location = new Point(30, 100);
            this.txtUsername.Size = new Size(320, 30);
            this.txtUsername.Font = UIHelper.FontRegular;
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;

            // Password label
            this.lblPassword.Text = "Mật khẩu";
            this.lblPassword.Font = UIHelper.FontSmall;
            this.lblPassword.ForeColor = Color.Gray;
            this.lblPassword.Location = new Point(30, 140);
            this.lblPassword.AutoSize = true;

            // Password textbox
            this.txtPassword.Location = new Point(30, 160);
            this.txtPassword.Size = new Size(320, 30);
            this.txtPassword.Font = UIHelper.FontRegular;
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.KeyDown += new KeyEventHandler(this.txtPassword_KeyDown);

            // Error label (ẩn mặc định)
            this.lblError.Text = "";
            this.lblError.Font = UIHelper.FontSmall;
            this.lblError.ForeColor = UIHelper.AccentRed;
            this.lblError.Location = new Point(30, 200);
            this.lblError.Size = new Size(320, 20);
            this.lblError.Visible = false;

            // Login button
            this.btnLogin.Text = "Đăng Nhập";
            this.btnLogin.Location = new Point(30, 230);
            this.btnLogin.Size = new Size(155, 40);
            UIHelper.StyleButtonPrimary(this.btnLogin);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Cancel button
            this.btnCancel.Text = "Thoát";
            this.btnCancel.Location = new Point(195, 230);
            this.btnCancel.Size = new Size(155, 40);
            UIHelper.StyleButtonSecondary(this.btnCancel);
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Thêm controls vào card
            this.pnlCard.Controls.Add(this.lblLoginTitle);
            this.pnlCard.Controls.Add(this.lblUsername);
            this.pnlCard.Controls.Add(this.txtUsername);
            this.pnlCard.Controls.Add(this.lblPassword);
            this.pnlCard.Controls.Add(this.txtPassword);
            this.pnlCard.Controls.Add(this.lblError);
            this.pnlCard.Controls.Add(this.btnLogin);
            this.pnlCard.Controls.Add(this.btnCancel);

            // ==============================
            // FORM LOGIN
            // ==============================
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(600, 450);
            this.BackColor = UIHelper.SidebarBg;
            this.Controls.Add(this.pnlCard);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Đăng Nhập — Quản Lý Bán Nước Giải Khát";

        
            this.ResumeLayout(false);
        }

        private Panel pnlCard;
        private Label lblLoginTitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnCancel;
        private Label lblError;
    }
}
