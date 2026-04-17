using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmThongKeDoanhThu : Form
    {
        NhanVien_BUS nvBUS = new NhanVien_BUS();
        HoaDon_BUS hdBUS = new HoaDon_BUS();

        public frmThongKeDoanhThu()
        {
            InitializeComponent();
        }

        private void frmThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            // Load danh sách nhân viên + thêm dòng "Tất cả"
            DataTable dtNV = nvBUS.LayDanhSachNhanVien();
            DataRow row = dtNV.NewRow();
            row["Mã NV"] = 0;
            row["Họ và Tên"] = "--- Tất cả nhân viên ---";
            dtNV.Rows.InsertAt(row, 0);

            cboNhanVien.DataSource = dtNV;
            cboNhanVien.DisplayMember = "Họ và Tên";
            cboNhanVien.ValueMember = "Mã NV";
            cboNhanVien.SelectedIndex = 0;

            // Mặc định: từ đầu tháng hiện tại đến hôm nay (không hard-code 2024)
            DateTime homNay = DateTime.Now;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDenNgay.Value = homNay;
        }

        private void btnXemBaoCao_Click_1(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);

            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy mã nhân viên an toàn
            string maNV = "0";
            if (cboNhanVien.SelectedValue != null)
            {
                if (cboNhanVien.SelectedValue is DataRowView drv)
                    maNV = drv[cboNhanVien.ValueMember].ToString();
                else
                    maNV = cboNhanVien.SelectedValue.ToString();
            }

            DataTable dtDoanhThu = hdBUS.ThongKeDoanhThu(tuNgay, denNgay, maNV);

            dgvDanhSach.AutoGenerateColumns = true;
            dgvDanhSach.DataSource = dtDoanhThu;

            if (dgvDanhSach.Columns.Contains("Tổng Tiền"))
            {
                dgvDanhSach.Columns["Tổng Tiền"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvDanhSach.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvDanhSach.Columns.Contains("Ngày Lập"))
            {
                dgvDanhSach.Columns["Ngày Lập"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }

            // Tổng kết
            int tongSoHD = dtDoanhThu.Rows.Count;
            decimal tongTien = 0;
            foreach (DataRow dr in dtDoanhThu.Rows)
            {
                if (dr["Tổng Tiền"] != DBNull.Value)
                    tongTien += Convert.ToDecimal(dr["Tổng Tiền"]);
            }

            txtTongSoHD.Text = tongSoHD.ToString("N0");
            txtTongDoanhThu.Text = tongTien.ToString("N0") + " VNĐ";

            if (tongSoHD == 0)
            {
                MessageBox.Show("Không có hóa đơn nào trong khoảng thời gian đã chọn.",
                    "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Microsoft.Office.Interop.Excel._Application app = null;
            Microsoft.Office.Interop.Excel._Workbook workbook = null;
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            try
            {
                app = new Microsoft.Office.Interop.Excel.Application();
                workbook = app.Workbooks.Add(Type.Missing);
                worksheet = workbook.Sheets[1];
                worksheet.Name = "DoanhThu";

                // Tiêu đề cột
                for (int i = 0; i < dgvDanhSach.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvDanhSach.Columns[i].HeaderText;
                }

                // Dữ liệu - duyệt hết TẤT CẢ các dòng, có check IsNewRow
                int rowExcel = 2;
                for (int i = 0; i < dgvDanhSach.Rows.Count; i++)
                {
                    if (dgvDanhSach.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgvDanhSach.Columns.Count; j++)
                    {
                        var val = dgvDanhSach.Rows[i].Cells[j].Value;
                        worksheet.Cells[rowExcel, j + 1] = val?.ToString() ?? string.Empty;
                    }
                    rowExcel++;
                }

                // Tổng kết
                worksheet.Cells[rowExcel + 1, 1] = "Tổng số hóa đơn:";
                worksheet.Cells[rowExcel + 1, 2] = txtTongSoHD.Text;
                worksheet.Cells[rowExcel + 2, 1] = "Tổng doanh thu:";
                worksheet.Cells[rowExcel + 2, 2] = txtTongDoanhThu.Text;

                worksheet.Columns.AutoFit();
                app.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
