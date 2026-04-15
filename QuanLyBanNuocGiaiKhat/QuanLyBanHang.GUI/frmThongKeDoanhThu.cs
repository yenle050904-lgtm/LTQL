using QuanLyBanHang.BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class frmThongKeDoanhThu : Form
    {
        public frmThongKeDoanhThu()
        {
            InitializeComponent();
        }
        NhanVien_BUS nvBUS = new NhanVien_BUS();
        HoaDon_BUS hdBUS = new HoaDon_BUS();
        private void frmThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            DataTable dtNV = nvBUS.LayDanhSachNhanVien();

            // Thêm dòng "Tất cả" vào ComboBox để có thể xem doanh thu của toàn bộ cửa hàng
            DataRow row = dtNV.NewRow();
            row["Mã NV"] = 0;
            row["Họ và Tên"] = "--- Tất cả nhân viên ---";
            dtNV.Rows.InsertAt(row, 0);

            cboNhanVien.DataSource = dtNV;
            cboNhanVien.DisplayMember = "Họ và Tên";
            cboNhanVien.ValueMember = "Mã NV";
            cboNhanVien.SelectedIndex = 0;

            // Set ngày mặc định (Ví dụ: Từ đầu tháng đến ngày hiện tại)
            dtpTuNgay.Value = new DateTime(2024, 1, 1);
            dtpDenNgay.Value = DateTime.Now;
        }
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvDanhSach.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Khai báo thư viện (Nhớ thêm tham chiếu Microsoft.Office.Interop.Excel vào Project)
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "DoanhThu";

            // Xuất Tiêu đề cột
            for (int i = 1; i < dgvDanhSach.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgvDanhSach.Columns[i - 1].HeaderText;
            }

            // Xuất nội dung các dòng
            for (int i = 0; i < dgvDanhSach.Rows.Count - 1; i++) // Bỏ dòng trắng cuối cùng
            {
                for (int j = 0; j < dgvDanhSach.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dgvDanhSach.Rows[i].Cells[j].Value.ToString();
                }
            }

            app.Visible = true; // Mở file Excel lên sau khi xuất xong
        }

        private void btnXemBaoCao_Click_1(object sender, EventArgs e)
        {
            // 1. LẤY NGÀY THÁNG ĐÚNG TỪ DATETIMEPICKER (đã sửa: không còn ép cứng)
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1); // Lấy hết ngày cuối

            // 2. LẤY MÃ NHÂN VIÊN AN TOÀN CHỐNG LỖI DATAROWVIEW
            string maNV = "0";
            if (cboNhanVien.SelectedValue != null)
            {
                if (cboNhanVien.SelectedValue is DataRowView)
                {
                    DataRowView drv = (DataRowView)cboNhanVien.SelectedValue;
                    maNV = drv[cboNhanVien.ValueMember].ToString();
                }
                else
                {
                    maNV = cboNhanVien.SelectedValue.ToString();
                }
            }

            // 3. Ràng buộc ngày tháng hợp lệ
            if (tuNgay > denNgay)
            {
                MessageBox.Show("Từ ngày không được lớn hơn Đến ngày!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 4. Lấy dữ liệu từ SQL thông qua BUS
            DataTable dtDoanhThu = hdBUS.ThongKeDoanhThu(tuNgay, denNgay, maNV);

            // 5. Bật tự động vẽ cột và Đổ dữ liệu vào lưới DataGridView
            dgvDanhSach.AutoGenerateColumns = true;
            dgvDanhSach.DataSource = dtDoanhThu;

            // Định dạng cột Tổng Tiền trong lưới
            if (dgvDanhSach.Columns.Contains("Tổng Tiền"))
            {
                dgvDanhSach.Columns["Tổng Tiền"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvDanhSach.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // 6. Tính tổng số hóa đơn và tổng doanh thu
            int tongSoHD = dtDoanhThu.Rows.Count;
            decimal tongTien = 0;

            foreach (DataRow dr in dtDoanhThu.Rows)
            {
                // Kiểm tra an toàn: Nếu cột thành tiền bị trống (NULL) thì bỏ qua
                if (dr["Tổng Tiền"] != DBNull.Value)
                {
                    tongTien += Convert.ToDecimal(dr["Tổng Tiền"]);
                }
            }

            // 7. Hiển thị ra 2 ô TextBox/Label phía dưới cùng
            txtTongSoHD.Text = tongSoHD.ToString("N0");
            txtTongDoanhThu.Text = tongTien.ToString("N0") + " VNĐ";

            // 8. Thông báo nếu bảng trống
            if (tongSoHD == 0)
            {
                MessageBox.Show($"Đã chạy thành công nhưng không tìm thấy Hóa đơn nào của nhân viên mã [{maNV}] trong khoảng thời gian này!\nHãy kiểm tra lại dữ liệu trong SQL Server.", "Kết quả truy vấn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
