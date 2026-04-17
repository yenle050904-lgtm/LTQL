using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmLichSuPhieuNhap : Form
    {
        LichSuPhieuNhap_BUS lsBus = new LichSuPhieuNhap_BUS();

        public frmLichSuPhieuNhap()
        {
            InitializeComponent();
        }

        private void frmLichSuPhieuNhap_Load(object sender, EventArgs e)
        {
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

                DataTable dt = lsBus.LayDanhSachPhieuNhap(tuNgay, denNgay);
                dgvPhieuNhap.DataSource = dt;
                DinhDangLuoiPhieu();

                dgvChiTiet.DataSource = null;
                txtTongTienPN.Text = "0 VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void DinhDangLuoiPhieu()
        {
            if (dgvPhieuNhap.Columns.Contains("Tổng Tiền"))
            {
                dgvPhieuNhap.Columns["Tổng Tiền"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvPhieuNhap.Columns["Tổng Tiền"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvPhieuNhap.Columns.Contains("Ngày Nhập"))
            {
                dgvPhieuNhap.Columns["Ngày Nhập"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private void DinhDangLuoiChiTiet()
        {
            if (dgvChiTiet.Columns.Contains("Giá Nhập"))
            {
                dgvChiTiet.Columns["Giá Nhập"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvChiTiet.Columns["Giá Nhập"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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
            string strMa = txtTimMaPN.Text.Trim();
            if (string.IsNullOrEmpty(strMa))
            {
                LoadDanhSach();
                return;
            }

            if (!int.TryParse(strMa, out int maPN))
            {
                MessageBox.Show("Mã phiếu nhập phải là số!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                dgvPhieuNhap.DataSource = lsBus.TimKiemTheoMa(maPN);
                DinhDangLuoiPhieu();
                dgvChiTiet.DataSource = null;
                txtTongTienPN.Text = "0 VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvPhieuNhap.Rows[e.RowIndex];
            if (row.Cells[0].Value == null) return;

            int maPN = Convert.ToInt32(row.Cells[0].Value);

            try
            {
                dgvChiTiet.DataSource = lsBus.LayChiTietPhieuNhap(maPN);
                DinhDangLuoiChiTiet();

                if (row.Cells["Tổng Tiền"]?.Value != null)
                {
                    decimal tongTien = Convert.ToDecimal(row.Cells["Tổng Tiền"].Value);
                    txtTongTienPN.Text = tongTien.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message);
            }
        }
    }
}
