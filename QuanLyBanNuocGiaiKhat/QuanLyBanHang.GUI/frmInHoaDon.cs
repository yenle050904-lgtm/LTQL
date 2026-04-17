using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmInHoaDon : Form
    {
        LichSuHoaDon_BUS lsBus = new LichSuHoaDon_BUS();
        private int _maHoaDon;

        // Dữ liệu dùng lại khi in
        private DataRow _thongTinHD;
        private DataTable _chiTietHD;

        // Constructor mặc định (để Designer không kêu lỗi)
        public frmInHoaDon()
        {
            InitializeComponent();
        }

        public frmInHoaDon(int maHoaDon) : this()
        {
            _maHoaDon = maHoaDon;
        }

        private void frmInHoaDon_Load(object sender, EventArgs e)
        {
            if (_maHoaDon <= 0) return;
            LoadHoaDon();
        }

        private void LoadHoaDon()
        {
            try
            {
                _thongTinHD = lsBus.LayThongTinHoaDon(_maHoaDon);
                _chiTietHD = lsBus.LayChiTietHoaDon(_maHoaDon);

                if (_thongTinHD == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                RenderHoaDonLenRichTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hóa đơn: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ghép text hóa đơn lên RichTextBox để xem trước trước khi in.
        /// </summary>
        private void RenderHoaDonLenRichTextBox()
        {
            rtbHoaDon.Clear();
            rtbHoaDon.SelectionAlignment = HorizontalAlignment.Center;

            // Tiêu đề cửa hàng (bạn có thể đổi tên thật trong Designer/resource)
            rtbHoaDon.SelectionFont = new Font("Consolas", 13, FontStyle.Bold);
            rtbHoaDon.AppendText("CỬA HÀNG NƯỚC GIẢI KHÁT\n");

            rtbHoaDon.SelectionFont = new Font("Consolas", 9, FontStyle.Regular);
            rtbHoaDon.AppendText("Địa chỉ: ....................................\n");
            rtbHoaDon.AppendText("Điện thoại: ................................\n\n");

            rtbHoaDon.SelectionFont = new Font("Consolas", 14, FontStyle.Bold);
            rtbHoaDon.AppendText("HÓA ĐƠN BÁN HÀNG\n\n");

            // Thông tin hóa đơn
            rtbHoaDon.SelectionAlignment = HorizontalAlignment.Left;
            rtbHoaDon.SelectionFont = new Font("Consolas", 10, FontStyle.Regular);

            DateTime ngayLap = Convert.ToDateTime(_thongTinHD["NgayLap"]);
            rtbHoaDon.AppendText($"Số HĐ     : {_thongTinHD["ID"]}\n");
            rtbHoaDon.AppendText($"Ngày lập  : {ngayLap:dd/MM/yyyy HH:mm}\n");
            rtbHoaDon.AppendText($"Nhân viên : {_thongTinHD["TenNhanVien"]}\n");
            rtbHoaDon.AppendText($"Khách hàng: {_thongTinHD["TenKhachHang"]}\n");

            string sdt = _thongTinHD["SDTKhachHang"]?.ToString() ?? string.Empty;
            if (!string.IsNullOrEmpty(sdt))
                rtbHoaDon.AppendText($"SĐT       : {sdt}\n");

            rtbHoaDon.AppendText("\n");
            rtbHoaDon.AppendText(new string('=', 68) + "\n");

            // Header bảng chi tiết
            rtbHoaDon.SelectionFont = new Font("Consolas", 10, FontStyle.Bold);
            rtbHoaDon.AppendText(string.Format("{0,-30}{1,6}{2,14}{3,16}\n",
                "Tên sản phẩm", "SL", "Đơn giá", "Thành tiền"));
            rtbHoaDon.SelectionFont = new Font("Consolas", 10, FontStyle.Regular);
            rtbHoaDon.AppendText(new string('-', 68) + "\n");

            // Các dòng chi tiết
            decimal tongTien = 0;
            foreach (DataRow row in _chiTietHD.Rows)
            {
                string tenSP = row["Tên SP"].ToString();
                int soLuong = Convert.ToInt32(row["Số Lượng"]);
                decimal donGia = Convert.ToDecimal(row["Đơn Giá"]);
                decimal thanhTien = Convert.ToDecimal(row["Thành Tiền"]);
                tongTien += thanhTien;

                // Tên dài quá thì cắt
                if (tenSP.Length > 30) tenSP = tenSP.Substring(0, 27) + "...";

                rtbHoaDon.AppendText(string.Format("{0,-30}{1,6}{2,14:N0}{3,16:N0}\n",
                    tenSP, soLuong, donGia, thanhTien));
            }

            rtbHoaDon.AppendText(new string('=', 68) + "\n");

            // Tổng cộng
            rtbHoaDon.SelectionFont = new Font("Consolas", 11, FontStyle.Bold);
            rtbHoaDon.AppendText(string.Format("{0,52}: {1,14:N0} VNĐ\n", "TỔNG CỘNG", tongTien));

            rtbHoaDon.AppendText("\n\n");

            // Lời cảm ơn
            rtbHoaDon.SelectionAlignment = HorizontalAlignment.Center;
            rtbHoaDon.SelectionFont = new Font("Consolas", 10, FontStyle.Italic);
            rtbHoaDon.AppendText("Cảm ơn quý khách. Hẹn gặp lại!\n");

            rtbHoaDon.SelectionStart = 0;
            rtbHoaDon.ScrollToCaret();
        }

        // ============== IN ==============

        private int _vitriInHienTai = 0;

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += new PrintPageEventHandler(Doc_PrintPage);

                PrintDialog dialog = new PrintDialog();
                dialog.Document = doc;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _vitriInHienTai = 0;
                    doc.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // In nguyên nội dung RichTextBox. Cách đơn giản: dùng MeasureString + DrawString
            string noiDung = rtbHoaDon.Text;
            if (string.IsNullOrEmpty(noiDung)) return;

            Font font = new Font("Consolas", 10);
            float lineHeight = font.GetHeight(e.Graphics);

            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            string conLai = noiDung.Substring(_vitriInHienTai);
            string[] dong = conLai.Split('\n');

            int i;
            for (i = 0; i < dong.Length; i++)
            {
                if (y + lineHeight > e.MarginBounds.Bottom) break;

                e.Graphics.DrawString(dong[i], font, Brushes.Black, x, y);
                y += lineHeight;
            }

            // Tính vị trí đã in để nếu quá 1 trang thì in tiếp
            int daIn = 0;
            for (int j = 0; j < i; j++)
                daIn += dong[j].Length + 1; // +1 cho \n
            _vitriInHienTai += daIn;

            e.HasMorePages = (_vitriInHienTai < noiDung.Length);
        }

        // ============== XEM TRƯỚC ==============

        private void btnXemTruoc_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument doc = new PrintDocument();
                doc.PrintPage += new PrintPageEventHandler(Doc_PrintPage);

                PrintPreviewDialog preview = new PrintPreviewDialog();
                preview.Document = doc;
                preview.Width = 900;
                preview.Height = 700;

                _vitriInHienTai = 0;
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xem trước: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
