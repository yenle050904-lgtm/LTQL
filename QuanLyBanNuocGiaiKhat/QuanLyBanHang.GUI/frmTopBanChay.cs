using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmTopBanChay : Form
    {
        BaoCao_BUS bcBus = new BaoCao_BUS();

        public frmTopBanChay()
        {
            InitializeComponent();
        }

        private void frmTopBanChay_Load(object sender, EventArgs e)
        {
            DateTime homNay = DateTime.Now;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDenNgay.Value = homNay;

            cboSapXep.Items.Clear();
            cboSapXep.Items.Add("Theo số lượng bán");
            cboSapXep.Items.Add("Theo doanh thu");
            cboSapXep.SelectedIndex = 0;

            nudTopN.Value = 10;

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

                int topN = (int)nudTopN.Value;
                string tuyTheo = cboSapXep.SelectedIndex == 1 ? "DoanhThu" : "SoLuong";

                DataTable dt = bcBus.TopSanPhamBanChay(tuNgay, denNgay, topN, tuyTheo);
                dgvTop.DataSource = dt;

                // Định dạng
                if (dgvTop.Columns.Contains("TongSoLuong"))
                {
                    dgvTop.Columns["TongSoLuong"].HeaderText = "Tổng SL";
                    dgvTop.Columns["TongSoLuong"].DefaultCellStyle.Format = "N0";
                    dgvTop.Columns["TongSoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvTop.Columns.Contains("DoanhThu"))
                {
                    dgvTop.Columns["DoanhThu"].HeaderText = "Doanh thu";
                    dgvTop.Columns["DoanhThu"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                    dgvTop.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo: " + ex.Message);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadBaoCao();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvTop.Rows.Count == 0)
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
                worksheet.Name = "TopBanChay";

                // Tiêu đề
                worksheet.Cells[1, 1] = "BÁO CÁO TOP SẢN PHẨM BÁN CHẠY";
                worksheet.Cells[2, 1] = $"Từ {dtpTuNgay.Value:dd/MM/yyyy} đến {dtpDenNgay.Value:dd/MM/yyyy}";

                // Header cột
                for (int i = 0; i < dgvTop.Columns.Count; i++)
                {
                    worksheet.Cells[4, i + 1] = dgvTop.Columns[i].HeaderText;
                }

                // Dữ liệu
                int rowExcel = 5;
                for (int i = 0; i < dgvTop.Rows.Count; i++)
                {
                    if (dgvTop.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dgvTop.Columns.Count; j++)
                    {
                        var val = dgvTop.Rows[i].Cells[j].Value;
                        worksheet.Cells[rowExcel, j + 1] = val?.ToString() ?? string.Empty;
                    }
                    rowExcel++;
                }

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
