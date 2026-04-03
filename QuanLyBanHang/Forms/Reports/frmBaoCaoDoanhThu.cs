using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Reports
{
    /// <summary>
    /// Form báo cáo doanh thu — Column Chart.
    /// Hỗ trợ 2 chế độ xem: Theo Tháng / Theo Nhân Viên.
    /// </summary>
    public partial class frmBaoCaoDoanhThu : Form
    {
        public frmBaoCaoDoanhThu()
        {
            InitializeComponent();
        }

        private void frmBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            cboCheDo.SelectedIndex = 0;
        }

        private void cboCheDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCheDo.SelectedIndex == 0)
                LoadChartTheoThang();
            else
                LoadChartTheoNhanVien();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cboCheDo_SelectedIndexChanged(sender, e);
        }

        // ==============================
        // BIỂU ĐỒ DOANH THU THEO THÁNG
        // ==============================
        private void LoadChartTheoThang()
        {
            using var db = new QLBHDbContext();

            var data = db.HoaDon_ChiTiets
                .Select(ct => new
                {
                    Thang = ct.HoaDon.NgayLap.Month,
                    DoanhThu = ct.SoLuongBan * ct.DonGiaBan
                })
                .AsEnumerable()  // Client-side evaluation for GroupBy
                .GroupBy(x => x.Thang)
                .Select(g => new { Thang = g.Key, Tong = g.Sum(x => x.DoanhThu) })
                .OrderBy(x => x.Thang)
                .ToList();

            chartDoanhThu.Series.Clear();
            var series = chartDoanhThu.Series.Add("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.Color = UIHelper.AccentBlue;
            series.BorderWidth = 0;

            chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
            chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";

            foreach (var item in data)
            {
                series.Points.AddXY($"T{item.Thang}", item.Tong);
            }

            StyleChart();
        }

        // ==============================
        // BIỂU ĐỒ DOANH THU THEO NHÂN VIÊN
        // ==============================
        private void LoadChartTheoNhanVien()
        {
            using var db = new QLBHDbContext();

            var data = db.HoaDon_ChiTiets
                .Select(ct => new
                {
                    NhanVien = ct.HoaDon.NhanVien != null ? ct.HoaDon.NhanVien.HoVaTen : "N/A",
                    DoanhThu = ct.SoLuongBan * ct.DonGiaBan
                })
                .AsEnumerable()
                .GroupBy(x => x.NhanVien)
                .Select(g => new { NhanVien = g.Key, Tong = g.Sum(x => x.DoanhThu) })
                .OrderByDescending(x => x.Tong)
                .ToList();

            chartDoanhThu.Series.Clear();
            var series = chartDoanhThu.Series.Add("Doanh Thu");
            series.ChartType = SeriesChartType.Column;
            series.Color = UIHelper.AccentGreen;
            series.BorderWidth = 0;

            chartDoanhThu.ChartAreas[0].AxisX.Title = "Nhân viên";
            chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";

            foreach (var item in data)
            {
                series.Points.AddXY(item.NhanVien, item.Tong);
            }

            StyleChart();
        }

        // ==============================
        // STYLE CHART
        // ==============================
        private void StyleChart()
        {
            var area = chartDoanhThu.ChartAreas[0];
            area.BackColor = UIHelper.CardBg;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = UIHelper.BorderColor;
            area.AxisX.LabelStyle.Font = UIHelper.FontSmall;
            area.AxisY.LabelStyle.Font = UIHelper.FontSmall;
            area.AxisX.TitleFont = UIHelper.FontBold;
            area.AxisY.TitleFont = UIHelper.FontBold;
            chartDoanhThu.BackColor = UIHelper.ContentBg;
        }
    }
}
