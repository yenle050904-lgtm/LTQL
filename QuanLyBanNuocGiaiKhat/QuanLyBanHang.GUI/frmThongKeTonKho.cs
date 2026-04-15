using QuanLyBanHang.BUS;
using System;
using System.Data;
using System.Windows.Forms;
// Thêm thư viện này nếu bạn dùng chức năng Xuất Excel (Cần Add Reference Microsoft.Office.Interop.Excel)
using Excel = Microsoft.Office.Interop.Excel;

namespace QuanLyBanHang.GUI
{
    public partial class frmThongKeTonKho : Form
    {
        // Khai báo lớp BUS (Bạn tự tạo file TonKho_BUS nhé)
        TonKho_BUS tkBus = new TonKho_BUS();

        public frmThongKeTonKho()
        {
            InitializeComponent();
        }
        private void LoadDuLieuTonKho()
        {
            int maLoai = 0;

            // Kiểm tra xem người dùng có đang chọn Loại SP nào không
            if (cboLoaiSanPham.SelectedIndex > -1 && cboLoaiSanPham.SelectedValue != null)
            {
                // Ép kiểu an toàn (tránh lỗi khi gán ValueMember)
                int.TryParse(cboLoaiSanPham.SelectedValue.ToString(), out maLoai);
            }

            // Lấy tình trạng tồn kho (0: Tất cả, 1: Sắp hết, 2: Còn nhiều)
            int loaiLoc = cboLocTonKho.SelectedIndex;
            if (loaiLoc < 0) loaiLoc = 0;

            // Gọi hàm từ BUS truyền đủ 3 điều kiện vào (đã bỏ tên sản phẩm)
            DataTable dt = tkBus.LayDuLieuTonKho(maLoai, "", loaiLoc);
            dgvTonKho.DataSource = dt;

            // Định dạng các cột số trong lưới
            if (dgvTonKho.Columns.Contains("DonGia"))
            {
                dgvTonKho.Columns["DonGia"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvTonKho.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvTonKho.Columns.Contains("SoLuongTon"))
            {
                dgvTonKho.Columns["SoLuongTon"].DefaultCellStyle.Format = "N0";
                dgvTonKho.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Gọi lại hàm tính tổng tiền mà chúng ta đã làm ở bước trước
            TinhTongThongKe();
        }

        private void TinhTongThongKe()
        {
            int tongSoLuongMatHang = 0;
            decimal tongGiaTriKho = 0;

            // Kiểm tra xem bảng có dữ liệu chưa
            if (dgvTonKho.DataSource != null)
            {
                // Lấy lại cái bảng dữ liệu gốc (DataTable) đang được gắn vào DataGridView
                DataTable dt = (DataTable)dgvTonKho.DataSource;

                // Duyệt qua từng dòng dữ liệu thật
                foreach (DataRow row in dt.Rows)
                {
                    // Lấy thẳng tên cột chuẩn từ Database của bạn
                    int soLuong = Convert.ToInt32(row["SoLuongTon"]);
                    decimal donGia = Convert.ToDecimal(row["DonGia"]); // Đã đổi chuẩn thành DonGia

                    // Cộng dồn toàn bộ số lượng tồn của tất cả sản phẩm
                    tongSoLuongMatHang += soLuong;

                    // Tính tổng giá trị = Số lượng * Đơn giá
                    tongGiaTriKho += (soLuong * donGia);
                }
            }

            // Hiển thị ra màn hình (Định dạng "N0" để có dấu phẩy phân cách hàng nghìn cho đẹp)
            txtTongSoMH.Text = tongSoLuongMatHang.ToString("N0");
            txtGiaTriKho.Text = tongGiaTriKho.ToString("N0") + " VNĐ";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Chỉ cần gọi lại hàm Load, nó sẽ tự động lấy các giá trị trên ô nhập để lọc
            LoadDuLieuTonKho();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvTonKho.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Đoạn code này cần thư viện Microsoft.Office.Interop.Excel
                /*
                Excel.Application exApp = new Excel.Application();
                Excel.Workbook exBook = exApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                Excel.Worksheet exSheet = (Excel.Worksheet)exBook.Worksheets[1];

                // In tiêu đề cột
                for (int i = 0; i < dgvTonKho.Columns.Count; i++)
                {
                    exSheet.Cells[1, i + 1] = dgvTonKho.Columns[i].HeaderText;
                }

                // In dữ liệu các dòng
                for (int i = 0; i < dgvTonKho.Rows.Count; i++)
                {
                    for (int j = 0; j < dgvTonKho.Columns.Count; j++)
                    {
                        if (dgvTonKho.Rows[i].Cells[j].Value != null)
                        {
                            exSheet.Cells[i + 2, j + 1] = dgvTonKho.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }
                exApp.Visible = true;
                */
                MessageBox.Show("Chức năng đang được hoàn thiện. Vui lòng cài đặt thư viện Excel Interop để bỏ comment đoạn code trên.", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message);
            }
        }

        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            LoadDuLieuTonKho();
        }

        private void btnXuatExcel_Click_1(object sender, EventArgs e)
        {
            // Kiểm tra xem bảng có dữ liệu không, nếu trống thì báo lỗi không cho xuất
            if (dgvTonKho.Rows.Count == 0 || (dgvTonKho.Rows.Count == 1 && dgvTonKho.Rows[0].IsNewRow))
            {
                MessageBox.Show("Không có dữ liệu nào trong bảng để xuất ra Excel!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Khởi tạo ứng dụng Excel
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

                // Thiết lập sheet hiện tại
                worksheet = workbook.Sheets["Sheet1"];
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "ThongKeTonKho";

                // 1. In tiêu đề các cột từ DataGridView lên dòng đầu tiên của Excel
                for (int i = 1; i < dgvTonKho.Columns.Count + 1; i++)
                {
                    worksheet.Cells[1, i] = dgvTonKho.Columns[i - 1].HeaderText;
                }

                // 2. In toàn bộ dữ liệu các dòng lên Excel
                for (int i = 0; i < dgvTonKho.Rows.Count; i++)
                {
                    // Bỏ qua dòng trắng tự động thêm ở cuối (nếu có)
                    if (dgvTonKho.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dgvTonKho.Columns.Count; j++)
                    {
                        if (dgvTonKho.Rows[i].Cells[j].Value != null)
                        {
                            worksheet.Cells[i + 2, j + 1] = dgvTonKho.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                }

                // 3. In thêm phần TỔNG KẾT ở bên dưới cùng của bảng Excel
                int rowCuoi = dgvTonKho.Rows.Count + 3; // Cách ra một chút cho đẹp
                worksheet.Cells[rowCuoi, 1] = "Tổng số mặt hàng:";
                worksheet.Cells[rowCuoi, 2] = "'" + txtTongSoMH.Text;

                worksheet.Cells[rowCuoi + 1, 1] = "Tổng giá trị kho:";
                worksheet.Cells[rowCuoi + 1, 2] = txtGiaTriKho.Text;

                // Tự động giãn cách độ rộng các cột Excel cho chữ không bị che
                worksheet.Columns.AutoFit();

                // 4. Mở file Excel lên cho người dùng xem
                app.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi khi xuất Excel. Xin đảm bảo máy tính bạn có cài đặt Microsoft Excel. Lỗi chi tiết: \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmThongKeTonKho_Load_1(object sender, EventArgs e)
        {

            // Đổ dữ liệu vào ComboBox Loại Sản Phẩm
            cboLoaiSanPham.DataSource = tkBus.LayDanhSachLoaiSP();
            cboLoaiSanPham.DisplayMember = "TenLoai";
            cboLoaiSanPham.ValueMember = "ID";
            cboLoaiSanPham.SelectedIndex = -1; // Để trống ô combo lúc mới mở

            // 2. Thêm các lựa chọn bằng tay cho ComboBox Lọc Tồn Kho
            cboLocTonKho.Items.Clear();
            cboLocTonKho.Items.Add("Tất cả");
            cboLocTonKho.Items.Add("Sắp hết hàng (< 10)");
            cboLocTonKho.Items.Add("Còn nhiều hàng (>= 10)");
            cboLocTonKho.SelectedIndex = 0; // Mặc định chọn "Tất cả"
            // Đổ dữ liệu vào DataGridView
            LoadDuLieuTonKho();

        }
    }
}