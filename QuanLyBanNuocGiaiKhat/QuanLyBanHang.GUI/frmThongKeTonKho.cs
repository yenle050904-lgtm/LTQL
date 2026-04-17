using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmThongKeTonKho : Form
    {
        TonKho_BUS tkBus = new TonKho_BUS();

        public frmThongKeTonKho()
        {
            InitializeComponent();
        }

        private void frmThongKeTonKho_Load_1(object sender, EventArgs e)
        {
            // Combo Loại SP
            cboLoaiSanPham.DataSource = tkBus.LayDanhSachLoaiSP();
            cboLoaiSanPham.DisplayMember = "TenLoai";
            cboLoaiSanPham.ValueMember = "ID";
            cboLoaiSanPham.SelectedIndex = -1;

            // Combo lọc tồn kho
            cboLocTonKho.Items.Clear();
            cboLocTonKho.Items.Add("Tất cả");
            cboLocTonKho.Items.Add("Sắp hết hàng (< 10)");
            cboLocTonKho.Items.Add("Còn nhiều hàng (>= 10)");
            cboLocTonKho.SelectedIndex = 0;

            LoadDuLieuTonKho();
        }

        private void LoadDuLieuTonKho()
        {
            int maLoai = 0;
            if (cboLoaiSanPham.SelectedIndex > -1 && cboLoaiSanPham.SelectedValue != null)
            {
                int.TryParse(cboLoaiSanPham.SelectedValue.ToString(), out maLoai);
            }

            int loaiLoc = cboLocTonKho.SelectedIndex;
            if (loaiLoc < 0) loaiLoc = 0;

            DataTable dt = tkBus.LayDuLieuTonKho(maLoai, "", loaiLoc);
            dgvTonKho.DataSource = dt;

            // Định dạng
            if (dgvTonKho.Columns.Contains("DonGia"))
            {
                dgvTonKho.Columns["DonGia"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvTonKho.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTonKho.Columns["DonGia"].HeaderText = "Đơn giá";
            }
            if (dgvTonKho.Columns.Contains("SoLuongTon"))
            {
                dgvTonKho.Columns["SoLuongTon"].DefaultCellStyle.Format = "N0";
                dgvTonKho.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvTonKho.Columns["SoLuongTon"].HeaderText = "Số lượng tồn";
            }
            if (dgvTonKho.Columns.Contains("TenSanPham"))
                dgvTonKho.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            if (dgvTonKho.Columns.Contains("TenLoai"))
                dgvTonKho.Columns["TenLoai"].HeaderText = "Loại";
            if (dgvTonKho.Columns.Contains("ID"))
                dgvTonKho.Columns["ID"].HeaderText = "Mã SP";

            TinhTongThongKe();
        }

        private void TinhTongThongKe()
        {
            int tongSoLuongMatHang = 0;
            decimal tongGiaTriKho = 0;

            if (dgvTonKho.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int soLuong = Convert.ToInt32(row["SoLuongTon"]);
                    decimal donGia = Convert.ToDecimal(row["DonGia"]);

                    tongSoLuongMatHang += soLuong;
                    tongGiaTriKho += (soLuong * donGia);
                }
            }

            txtTongSoMH.Text = tongSoLuongMatHang.ToString("N0");
            txtGiaTriKho.Text = tongGiaTriKho.ToString("N0") + " VNĐ";
        }

        // Một handler duy nhất cho nút Tìm kiếm (đã bỏ btnTimKiem_Click cũ)
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            LoadDuLieuTonKho();
        }

        // Một handler duy nhất cho nút Xuất Excel
        private void btnXuatExcel_Click_1(object sender, EventArgs e)
        {
            if (dgvTonKho.Rows.Count == 0 || (dgvTonKho.Rows.Count == 1 && dgvTonKho.Rows[0].IsNewRow))
            {
                MessageBox.Show("Không có dữ liệu nào trong bảng để xuất ra Excel!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                worksheet.Name = "ThongKeTonKho";

                // Tiêu đề cột
                for (int i = 0; i < dgvTonKho.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = dgvTonKho.Columns[i].HeaderText;
                }

                // Dữ liệu
                int rowExcel = 2;
                for (int i = 0; i < dgvTonKho.Rows.Count; i++)
                {
                    if (dgvTonKho.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgvTonKho.Columns.Count; j++)
                    {
                        var val = dgvTonKho.Rows[i].Cells[j].Value;
                        worksheet.Cells[rowExcel, j + 1] = val?.ToString() ?? string.Empty;
                    }
                    rowExcel++;
                }

                // Tổng kết
                worksheet.Cells[rowExcel + 1, 1] = "Tổng số mặt hàng:";
                worksheet.Cells[rowExcel + 1, 2] = "'" + txtTongSoMH.Text;  // '  để Excel không đoán là số
                worksheet.Cells[rowExcel + 2, 1] = "Tổng giá trị kho:";
                worksheet.Cells[rowExcel + 2, 2] = txtGiaTriKho.Text;

                worksheet.Columns.AutoFit();
                app.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi khi xuất Excel. Hãy đảm bảo máy có cài Microsoft Excel.\n\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
