using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace QuanLyBanHang.Helpers
{
    /// <summary>
    /// Lớp tiện ích tĩnh chứa các phương thức style cho Flat Design UI.
    /// Sử dụng bảng màu Catppuccin Mocha làm theme chính.
    /// </summary>
    public static class UIHelper
    {
        // ==============================
        // BẢNG MÀU FLAT DESIGN (Catppuccin)
        // ==============================
        public static readonly Color SidebarBg     = Color.FromArgb(30, 30, 46);    // #1E1E2E
        public static readonly Color SidebarHover  = Color.FromArgb(49, 50, 68);    // #313244
        public static readonly Color SidebarActive = Color.FromArgb(137, 180, 250);  // #89B4FA
        public static readonly Color ContentBg     = Color.FromArgb(245, 245, 245);  // #F5F5F5
        public static readonly Color CardBg        = Color.FromArgb(255, 255, 255);  // #FFFFFF
        public static readonly Color TextPrimary   = Color.FromArgb(30, 30, 46);     // #1E1E2E
        public static readonly Color TextLight     = Color.FromArgb(205, 214, 244);  // #CDD6F4
        public static readonly Color AccentBlue    = Color.FromArgb(137, 180, 250);  // #89B4FA
        public static readonly Color AccentGreen   = Color.FromArgb(166, 227, 161);  // #A6E3A1
        public static readonly Color AccentRed     = Color.FromArgb(243, 139, 168);  // #F38BA8
        public static readonly Color AccentYellow  = Color.FromArgb(249, 226, 175);  // #F9E2AF
        public static readonly Color BorderColor   = Color.FromArgb(220, 220, 220);  // #DCDCDC
        public static readonly Color RowAlt        = Color.FromArgb(248, 249, 252);  // #F8F9FC
        public static readonly Color HeaderBg      = Color.FromArgb(30, 30, 46);     // #1E1E2E

        public static readonly Font FontRegular    = new Font("Segoe UI", 10F, FontStyle.Regular);
        public static readonly Font FontBold       = new Font("Segoe UI", 10F, FontStyle.Bold);
        public static readonly Font FontTitle      = new Font("Segoe UI", 14F, FontStyle.Bold);
        public static readonly Font FontSmall      = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static readonly Font FontSidebar    = new Font("Segoe UI", 10.5F, FontStyle.Regular);

        // ==============================
        // STYLE: DataGridView
        // ==============================
        public static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.EnableHeadersVisualStyles = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AllowUserToResizeRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;
            dgv.BackgroundColor = CardBg;
            dgv.GridColor = BorderColor;
            dgv.Font = FontRegular;

            // Header style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = HeaderBg;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = FontBold;
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 0, 0, 0);
            dgv.ColumnHeadersHeight = 40;

            // Row style
            dgv.DefaultCellStyle.BackColor = CardBg;
            dgv.DefaultCellStyle.ForeColor = TextPrimary;
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 225, 250);
            dgv.DefaultCellStyle.SelectionForeColor = TextPrimary;
            dgv.DefaultCellStyle.Padding = new Padding(8, 4, 0, 4);
            dgv.RowTemplate.Height = 36;

            // Alternating row
            dgv.AlternatingRowsDefaultCellStyle.BackColor = RowAlt;
        }

        // ==============================
        // STYLE: Button — Primary (Accent Blue)
        // ==============================
        public static void StyleButtonPrimary(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = AccentBlue;
            btn.ForeColor = Color.White;
            btn.Font = FontBold;
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(btn.Width < 100 ? 100 : btn.Width, 36);

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(116, 160, 230);
            btn.MouseLeave += (s, e) => btn.BackColor = AccentBlue;
        }

        // ==============================
        // STYLE: Button — Success (Green)
        // ==============================
        public static void StyleButtonSuccess(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = AccentGreen;
            btn.ForeColor = TextPrimary;
            btn.Font = FontBold;
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(btn.Width < 100 ? 100 : btn.Width, 36);

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(140, 200, 135);
            btn.MouseLeave += (s, e) => btn.BackColor = AccentGreen;
        }

        // ==============================
        // STYLE: Button — Danger (Red)
        // ==============================
        public static void StyleButtonDanger(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = AccentRed;
            btn.ForeColor = Color.White;
            btn.Font = FontBold;
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(btn.Width < 100 ? 100 : btn.Width, 36);

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(220, 110, 140);
            btn.MouseLeave += (s, e) => btn.BackColor = AccentRed;
        }

        // ==============================
        // STYLE: Button — Secondary (Outline)
        // ==============================
        public static void StyleButtonSecondary(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = BorderColor;
            btn.FlatAppearance.BorderSize = 1;
            btn.BackColor = CardBg;
            btn.ForeColor = TextPrimary;
            btn.Font = FontRegular;
            btn.Cursor = Cursors.Hand;
            btn.Size = new Size(btn.Width < 100 ? 100 : btn.Width, 36);

            btn.MouseEnter += (s, e) => btn.BackColor = ContentBg;
            btn.MouseLeave += (s, e) => btn.BackColor = CardBg;
        }

        // ==============================
        // STYLE: TextBox — Flat với border dưới
        // ==============================
        public static void StyleTextBox(TextBox txt)
        {
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = FontRegular;
            txt.BackColor = CardBg;
            txt.ForeColor = TextPrimary;
        }

        // ==============================
        // STYLE: ComboBox — Flat
        // ==============================
        public static void StyleComboBox(ComboBox cbo)
        {
            cbo.FlatStyle = FlatStyle.Flat;
            cbo.Font = FontRegular;
            cbo.BackColor = CardBg;
            cbo.ForeColor = TextPrimary;
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ==============================
        // STYLE: Label — Header
        // ==============================
        public static void StyleLabelHeader(Label lbl)
        {
            lbl.Font = FontTitle;
            lbl.ForeColor = TextPrimary;
            lbl.AutoSize = true;
        }

        // ==============================
        // STYLE: Panel — Card với shadow giả
        // ==============================
        public static void StyleCardPanel(Panel pnl)
        {
            pnl.BackColor = CardBg;
            pnl.Padding = new Padding(16);
        }

        // ==============================
        // SIDEBAR: Tạo button điều hướng
        // ==============================
        public static Button CreateSidebarButton(string text, string icon, int yPosition)
        {
            var btn = new Button();
            btn.Text = $"  {icon}  {text}";
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = SidebarBg;
            btn.ForeColor = TextLight;
            btn.Font = FontSidebar;
            btn.Size = new Size(220, 45);
            btn.Location = new Point(0, yPosition);
            btn.Cursor = Cursors.Hand;
            btn.Dock = DockStyle.None;
            btn.Padding = new Padding(12, 0, 0, 0);

            btn.MouseEnter += (s, e) =>
            {
                if (btn.Tag?.ToString() != "active")
                    btn.BackColor = SidebarHover;
            };
            btn.MouseLeave += (s, e) =>
            {
                if (btn.Tag?.ToString() != "active")
                    btn.BackColor = SidebarBg;
            };

            return btn;
        }

        // ==============================
        // SIDEBAR: Đánh dấu button đang active
        // ==============================
        public static void SetActiveSidebarButton(Button activeBtn, Panel sidebar)
        {
            foreach (Control ctrl in sidebar.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = SidebarBg;
                    btn.ForeColor = TextLight;
                    btn.Tag = null;
                }
            }
            activeBtn.BackColor = SidebarActive;
            activeBtn.ForeColor = Color.White;
            activeBtn.Tag = "active";
        }

        // ==============================
        // FORM CHILD: Embed form vào Panel
        // ==============================
        public static void OpenChildForm(Form childForm, Panel container)
        {
            container.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.BackColor = ContentBg;
            container.Controls.Add(childForm);
            childForm.Show();
        }
    }
}
