using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Reports
{
    partial class frmBaoCaoDoanhThu
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            ChartArea chartArea1 = new ChartArea();
            Legend legend1 = new Legend();

            this.chartDoanhThu = new Chart();
            this.cboCheDo = new ComboBox();
            this.btnRefresh = new Button();
            this.lblCheDo = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.SuspendLayout();

            // Chế độ xem
            this.lblCheDo.Text = "📊 Chế độ xem:";
            this.lblCheDo.Font = UIHelper.FontRegular;
            this.lblCheDo.Location = new Point(16, 18);
            this.lblCheDo.AutoSize = true;

            this.cboCheDo.Location = new Point(140, 15);
            this.cboCheDo.Size = new Size(200, 26);
            this.cboCheDo.Items.AddRange(new object[] { "Doanh thu theo Tháng", "Doanh thu theo Nhân viên" });
            UIHelper.StyleComboBox(this.cboCheDo);
            this.cboCheDo.SelectedIndexChanged += new System.EventHandler(this.cboCheDo_SelectedIndexChanged);

            this.btnRefresh.Text = "↻ Làm mới";
            this.btnRefresh.Location = new Point(360, 12);
            this.btnRefresh.Size = new Size(110, 32);
            UIHelper.StyleButtonSecondary(this.btnRefresh);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // Chart
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new Point(16, 55);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new Size(800, 400);
            this.chartDoanhThu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // Form
            this.ClientSize = new Size(840, 475);
            this.Controls.Add(lblCheDo);
            this.Controls.Add(cboCheDo);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(chartDoanhThu);
            this.BackColor = UIHelper.ContentBg;
            this.Name = "frmBaoCaoDoanhThu";
            this.Text = "Báo Cáo Doanh Thu";
            this.Load += new System.EventHandler(this.frmBaoCaoDoanhThu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Chart chartDoanhThu;
        private ComboBox cboCheDo;
        private Button btnRefresh;
        private Label lblCheDo;
    }
}
