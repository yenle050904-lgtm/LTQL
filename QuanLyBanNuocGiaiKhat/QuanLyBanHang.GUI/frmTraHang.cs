using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmTraHang : Form
    {
        TraHang_BUS thBus = new TraHang_BUS();
        private int _maHDGoc = -1;
        private DataTable _dtChiTiet;  // Chi tiết HĐ để chỉnh cột "Trả SL"

        public frmTraHang()
        {
            InitializeComponent();
        }

        private void frmTraHang_Load(object sender, EventArgs e)
        {
            LoadLichSu();
        }

        private void btnTimHoaDon_Click(object sender, EventArgs e)
        {
            string strMa = txtMaHD.Text.Trim();
            if (!int.TryParse(strMa, out int maHD))
            {
                MessageBox.Show("Mã hóa đơn phải là số!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRow rowHD = thBus.KiemTraHoaDon(maHD);
                if (rowHD == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn có mã này!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNgayHD.Clear();
                    txtKhachHD.Clear();
                    txtTongHD.Clear();
                    dgvChiTiet.DataSource = null;
                    _maHDGoc = -1;
                    return;
                }

                _maHDGoc = maHD;
                txtNgayHD.Text = Convert.ToDateTime(rowHD["NgayLap"]).ToString("dd/MM/yyyy HH:mm");
                txtKhachHD.Text = rowHD["TenKhachHang"].ToString();
                txtTongHD.Text = Convert.ToDecimal(rowHD["TongTien"]).ToString("N0") + " VNĐ";

                // Load chi tiết + thêm cột "SL Trả" editable
                _dtChiTiet = thBus.LayChiTietHoaDon(maHD);
                if (!_dtChiTiet.Columns.Contains("SL Trả"))
                    _dtChiTiet.Columns.Add("SL Trả", typeof(int));

                // Set giá trị mặc định = 0
                foreach (DataRow r in _dtChiTiet.Rows)
                    r["SL Trả"] = 0;

                dgvChiTiet.DataSource = _dtChiTiet;
                DinhDangLuoiChiTiet();

                // Reset tổng hoàn
                txtTongHoan.Text = "0 VNĐ";
                txtLyDo.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void DinhDangLuoiChiTiet()
        {
            foreach (DataGridViewColumn col in dgvChiTiet.Columns)
            {
                // Chỉ cho phép sửa cột "SL Trả"
                col.ReadOnly = (col.Name != "SL Trả");
            }

            if (dgvChiTiet.Columns.Contains("Đơn Giá"))
            {
                dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Format = "#,##0";
                dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvChiTiet.Columns.Contains("Mã SP"))
                dgvChiTiet.Columns["Mã SP"].Width = 70;
            if (dgvChiTiet.Columns.Contains("Tên SP"))
                dgvChiTiet.Columns["Tên SP"].Width = 250;
            if (dgvChiTiet.Columns.Contains("SL Trả"))
            {
                dgvChiTiet.Columns["SL Trả"].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                dgvChiTiet.Columns["SL Trả"].Width = 80;
            }
        }

        private void dgvChiTiet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            TinhTongHoan();
        }

        private void dgvChiTiet_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvChiTiet.Rows[e.RowIndex];

            // Validate: SL trả không được > Còn lại
            if (row.Cells["SL Trả"]?.Value == null || row.Cells["SL Trả"].Value == DBNull.Value)
                return;

            int slTra = Convert.ToInt32(row.Cells["SL Trả"].Value);
            int conLai = Convert.ToInt32(row.Cells["Còn Lại"].Value);

            if (slTra < 0)
            {
                row.Cells["SL Trả"].Value = 0;
            }
            else if (slTra > conLai)
            {
                MessageBox.Show($"Số lượng trả không được vượt quá số còn lại ({conLai})!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                row.Cells["SL Trả"].Value = conLai;
            }

            TinhTongHoan();
        }

        private void TinhTongHoan()
        {
            if (_dtChiTiet == null) return;

            decimal tong = 0;
            foreach (DataRow row in _dtChiTiet.Rows)
            {
                if (row["SL Trả"] == DBNull.Value) continue;
                int sl = Convert.ToInt32(row["SL Trả"]);
                decimal dg = Convert.ToDecimal(row["Đơn Giá"]);
                tong += sl * dg;
            }
            txtTongHoan.Text = tong.ToString("N0") + " VNĐ";
        }

        private void btnLuuPhieu_Click(object sender, EventArgs e)
        {
            if (_maHDGoc == -1 || _dtChiTiet == null)
            {
                MessageBox.Show("Vui lòng tìm hóa đơn trước!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuẩn bị DataTable cho DAO (cột MaSP, TenSP, SoLuongTra, DonGia, ThanhTien)
            DataTable dtTra = new DataTable();
            dtTra.Columns.Add("MaSP", typeof(int));
            dtTra.Columns.Add("TenSP", typeof(string));
            dtTra.Columns.Add("SoLuongTra", typeof(int));
            dtTra.Columns.Add("DonGia", typeof(decimal));
            dtTra.Columns.Add("ThanhTien", typeof(decimal));

            decimal tongHoan = 0;
            foreach (DataRow row in _dtChiTiet.Rows)
            {
                if (row["SL Trả"] == DBNull.Value) continue;
                int sl = Convert.ToInt32(row["SL Trả"]);
                if (sl <= 0) continue;

                int maSP = Convert.ToInt32(row["Mã SP"]);
                string tenSP = row["Tên SP"].ToString();
                decimal dg = Convert.ToDecimal(row["Đơn Giá"]);
                decimal tt = sl * dg;
                tongHoan += tt;

                dtTra.Rows.Add(maSP, tenSP, sl, dg, tt);
            }

            if (dtTra.Rows.Count == 0)
            {
                MessageBox.Show("Chưa chọn sản phẩm nào để trả! Vui lòng nhập số lượng vào cột 'SL Trả'.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lyDo = txtLyDo.Text.Trim();
            if (string.IsNullOrEmpty(lyDo))
            {
                MessageBox.Show("Vui lòng nhập lý do trả hàng!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLyDo.Focus();
                return;
            }

            if (MessageBox.Show($"Xác nhận lưu phiếu trả hàng?\nTổng hoàn: {tongHoan:N0} VNĐ",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                int maPhieuTra = thBus.LuuPhieuTraHang(_maHDGoc, UserSession.ID, lyDo, tongHoan, dtTra);
                MessageBox.Show($"Đã lưu phiếu trả hàng (Mã: {maPhieuTra}). Tồn kho đã được cộng lại.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset
                _maHDGoc = -1;
                _dtChiTiet = null;
                txtMaHD.Clear();
                txtNgayHD.Clear();
                txtKhachHD.Clear();
                txtTongHD.Clear();
                txtTongHoan.Text = "0 VNĐ";
                txtLyDo.Clear();
                dgvChiTiet.DataSource = null;

                LoadLichSu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu phiếu trả: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadLichSu()
        {
            try
            {
                DateTime tuNgay = DateTime.Now.Date.AddMonths(-1);
                DateTime denNgay = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
                DataTable dt = thBus.LayLichSuTraHang(tuNgay, denNgay);
                dgvLichSu.DataSource = dt;

                if (dgvLichSu.Columns.Contains("Tổng Hoàn"))
                {
                    dgvLichSu.Columns["Tổng Hoàn"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                    dgvLichSu.Columns["Tổng Hoàn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvLichSu.Columns.Contains("Ngày Trả"))
                    dgvLichSu.Columns["Ngày Trả"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }
    }
}
