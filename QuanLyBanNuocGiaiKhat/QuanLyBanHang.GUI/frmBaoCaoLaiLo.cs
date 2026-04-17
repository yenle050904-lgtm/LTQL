using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmBaoCaoLaiLo : Form
    {
        BaoCao_BUS bcBus = new BaoCao_BUS();

        public frmBaoCaoLaiLo()
        {
            InitializeComponent();
        }

        private void frmBaoCaoLaiLo_Load(object sender, EventArgs e)
        {
            DateTime homNay = DateTime.Now;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDenNgay.Value = homNay;
            LoadBaoCao();
        }

        private void LoadBaoCao()
        {
            try
            {
                DateTime tuNgay = dtpTuNgay.Value.Date;
                DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chi tiết
                DataTable dt = bcBus.BaoCaoLaiLo(tuNgay, denNgay);
                dgvLaiLo.DataSource = dt;
                DinhDangLuoi();
                ToMauDong();

                // Tổng quan
                DataRow rowTong = bcBus.TongQuanLaiLo(tuNgay, denNgay);
                if (rowTong != null)
                {
                    decimal doanhThu = Convert.ToDecimal(rowTong["TongDoanhThu"]);
                    decimal chiPhi = Convert.ToDecimal(rowTong["TongChiPhi"]);
                    decimal loiNhuan = Convert.ToDecimal(rowTong["TongLoiNhuan"]);
                    int soHD = Convert.ToInt32(rowTong["SoHoaDon"]);

                    txtTongDoanhThu.Text = doanhThu.ToString("N0") + " VNĐ";
                    txtTongChiPhi.Text = chiPhi.ToString("N0") + " VNĐ";
                    txtTongLoiNhuan.Text = loiNhuan.ToString("N0") + " VNĐ";
                    txtSoHD.Text = soHD.ToString("N0");

                    // Tô màu lợi nhuận: xanh nếu dương, đỏ nếu âm
                    txtTongLoiNhuan.ForeColor = loiNhuan >= 0 ? Color.Green : Color.Red;
                }
                else
                {
                    txtTongDoanhThu.Text = "0 VNĐ";
                    txtTongChiPhi.Text = "0 VNĐ";
                    txtTongLoiNhuan.Text = "0 VNĐ";
                    txtSoHD.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message);
            }
        }

        private void DinhDangLuoi()
        {
            string[] cotTien = { "Doanh Thu", "Giá Vốn TB", "Chi Phí", "Lợi Nhuận" };
            foreach (string c in cotTien)
            {
                if (dgvLaiLo.Columns.Contains(c))
                {
                    dgvLaiLo.Columns[c].DefaultCellStyle.Format = "#,##0";
                    dgvLaiLo.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            if (dgvLaiLo.Columns.Contains("Số Lượng Bán"))
            {
                dgvLaiLo.Columns["Số Lượng Bán"].DefaultCellStyle.Format = "N0";
                dgvLaiLo.Columns["Số Lượng Bán"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void ToMauDong()
        {
            if (!dgvLaiLo.Columns.Contains("Lợi Nhuận")) return;

            foreach (DataGridViewRow row in dgvLaiLo.Rows)
            {
                if (row.Cells["Lợi Nhuận"]?.Value == null) continue;
                if (row.Cells["Lợi Nhuận"].Value == DBNull.Value) continue;

                decimal loiNhuan = Convert.ToDecimal(row.Cells["Lợi Nhuận"].Value);
                if (loiNhuan < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (loiNhuan == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadBaoCao();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvLaiLo.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Sheets[1];
                worksheet.Name = "LaiLo";

                worksheet.Cells[1, 1] = "BÁO CÁO LÃI/LỖ";
                worksheet.Cells[2, 1] = $"Từ {dtpTuNgay.Value:dd/MM/yyyy} đến {dtpDenNgay.Value:dd/MM/yyyy}";

                // Header cột
                for (int i = 0; i < dgvLaiLo.Columns.Count; i++)
                {
                    worksheet.Cells[4, i + 1] = dgvLaiLo.Columns[i].HeaderText;
                }

                int rowExcel = 5;
                for (int i = 0; i < dgvLaiLo.Rows.Count; i++)
                {
                    if (dgvLaiLo.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvLaiLo.Columns.Count; j++)
                    {
                        var val = dgvLaiLo.Rows[i].Cells[j].Value;
                        worksheet.Cells[rowExcel, j + 1] = val?.ToString() ?? string.Empty;
                    }
                    rowExcel++;
                }

                // Tổng kết
                rowExcel += 2;
                worksheet.Cells[rowExcel, 1] = "Tổng số hóa đơn:";
                worksheet.Cells[rowExcel, 2] = txtSoHD.Text;
                worksheet.Cells[rowExcel + 1, 1] = "Tổng doanh thu:";
                worksheet.Cells[rowExcel + 1, 2] = txtTongDoanhThu.Text;
                worksheet.Cells[rowExcel + 2, 1] = "Tổng chi phí:";
                worksheet.Cells[rowExcel + 2, 2] = txtTongChiPhi.Text;
                worksheet.Cells[rowExcel + 3, 1] = "Tổng lợi nhuận:";
                worksheet.Cells[rowExcel + 3, 2] = txtTongLoiNhuan.Text;

                worksheet.Columns.AutoFit();
                app.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }
    }
}
