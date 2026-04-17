using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmLichSuHoaDon : Form
    {
        LichSuHoaDon_BUS lsBus = new LichSuHoaDon_BUS();

        public frmLichSuHoaDon()
        {
            InitializeComponent();
        }

        private void frmLichSuHoaDon_Load(object sender, EventArgs e)
        {
            // Mặc định: đầu tháng hiện tại đến hôm nay
            DateTime homNay = DateTime.Now;
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDenNgay.Value = homNay;

            LoadDanhSach();
        }

        private void LoadDanhSach()
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

                DataTable dt = lsBus.LayDanhSachHoaDon(tuNgay, denNgay);
                dgvHoaDon.DataSource = dt;
                DinhDangLuoiHoaDon();

                // Xóa chi tiết cũ
                dgvChiTiet.DataSource = null;
                txtTongTienHD.Text = "0 VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void DinhDangLuoiHoaDon()
        {
            if (dgvHoaDon.Columns.Contains("Tổng Tiền"))
            {
                dgvHoaDon.Columns["Tổng Tiền"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvHoaDon.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvHoaDon.Columns.Contains("Ngày Lập"))
            {
                dgvHoaDon.Columns["Ngày Lập"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private void DinhDangLuoiChiTiet()
        {
            if (dgvChiTiet.Columns.Contains("Đơn Giá"))
            {
                dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvChiTiet.Columns["Đơn Giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvChiTiet.Columns.Contains("Thành Tiền"))
            {
                dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvChiTiet.Columns["Thành Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void btnTimTheoMa_Click(object sender, EventArgs e)
        {
            string strMa = txtTimMaHD.Text.Trim();
            if (string.IsNullOrEmpty(strMa))
            {
                LoadDanhSach();
                return;
            }

            if (!int.TryParse(strMa, out int maHD))
            {
                MessageBox.Show("Mã hóa đơn phải là số!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dgvHoaDon.DataSource = lsBus.TimKiemTheoMa(maHD);
                DinhDangLuoiHoaDon();
                dgvChiTiet.DataSource = null;
                txtTongTienHD.Text = "0 VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvHoaDon.Rows[e.RowIndex];
            if (row.Cells[0].Value == null) return;

            int maHD = Convert.ToInt32(row.Cells[0].Value);

            try
            {
                dgvChiTiet.DataSource = lsBus.LayChiTietHoaDon(maHD);
                DinhDangLuoiChiTiet();

                // Hiển thị tổng tiền của hóa đơn đang chọn
                if (row.Cells["Tổng Tiền"]?.Value != null)
                {
                    decimal tongTien = Convert.ToDecimal(row.Cells["Tổng Tiền"].Value);
                    txtTongTienHD.Text = tongTien.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maHD = Convert.ToInt32(dgvHoaDon.CurrentRow.Cells[0].Value);

            // Mở form in hóa đơn (modal)
            frmInHoaDon frm = new frmInHoaDon(maHD);
            frm.ShowDialog();
        }
    }
}
