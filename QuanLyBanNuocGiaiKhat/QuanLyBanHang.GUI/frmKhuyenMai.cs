using System;
using System.Globalization;
using System.Windows.Forms;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class frmKhuyenMai : Form
    {
        KhuyenMai_BUS kmBus = new KhuyenMai_BUS();
        private int maKMDangChon = -1;

        public frmKhuyenMai()
        {
            InitializeComponent();
        }

        private void frmKhuyenMai_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
            // Mặc định ngày bắt đầu = hôm nay, kết thúc = 30 ngày sau
            dtpNgayBatDau.Value = DateTime.Now.Date;
            dtpNgayKetThuc.Value = DateTime.Now.Date.AddDays(30);
        }

        private void LoadDanhSach()
        {
            try
            {
                dgvKhuyenMai.DataSource = kmBus.LayDanhSach();
                DinhDangLuoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void DinhDangLuoi()
        {
            if (dgvKhuyenMai.Columns.Contains("Giảm Tối Đa"))
                dgvKhuyenMai.Columns["Giảm Tối Đa"].DefaultCellStyle.Format = "#,##0";
            if (dgvKhuyenMai.Columns.Contains("Đơn Tối Thiểu"))
                dgvKhuyenMai.Columns["Đơn Tối Thiểu"].DefaultCellStyle.Format = "#,##0";
            if (dgvKhuyenMai.Columns.Contains("Ngày BĐ"))
                dgvKhuyenMai.Columns["Ngày BĐ"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgvKhuyenMai.Columns.Contains("Ngày KT"))
                dgvKhuyenMai.Columns["Ngày KT"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private bool TryParseTien(string s, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;
            string clean = s.Replace(".", "").Replace(",", "").Trim();
            return decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        }

        private bool DocDuLieuForm(out KhuyenMai_DTO dto)
        {
            dto = new KhuyenMai_DTO();

            if (string.IsNullOrWhiteSpace(txtMaCode.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã code!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTenKM.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên khuyến mãi!");
                return false;
            }
            if (!decimal.TryParse(txtPhanTram.Text.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal phanTram)
                || phanTram < 0 || phanTram > 100)
            {
                MessageBox.Show("Phần trăm giảm phải từ 0 đến 100!");
                return false;
            }

            TryParseTien(txtGiamToiDa.Text, out decimal giamMax);
            TryParseTien(txtDonToiThieu.Text, out decimal donMin);

            if (dtpNgayBatDau.Value > dtpNgayKetThuc.Value)
            {
                MessageBox.Show("Ngày bắt đầu phải trước Ngày kết thúc!");
                return false;
            }

            dto.MaCode = txtMaCode.Text.Trim().ToUpper();
            dto.TenKhuyenMai = txtTenKM.Text.Trim();
            dto.PhanTramGiam = phanTram;
            dto.GiamToiDa = giamMax > 0 ? (decimal?)giamMax : null;
            dto.DonToiThieu = donMin;
            dto.NgayBatDau = dtpNgayBatDau.Value.Date;
            dto.NgayKetThuc = dtpNgayKetThuc.Value.Date;
            dto.TrangThai = chkHoatDong.Checked;
            dto.MoTa = txtMoTa.Text.Trim();
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!DocDuLieuForm(out KhuyenMai_DTO dto)) return;

            try
            {
                if (kmBus.Them(dto))
                {
                    MessageBox.Show("Thêm khuyến mãi thành công!", "Thông báo");
                    LoadDanhSach();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm (có thể mã code đã tồn tại): " + ex.Message, "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maKMDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn 1 khuyến mãi trên lưới để sửa!");
                return;
            }

            if (!DocDuLieuForm(out KhuyenMai_DTO dto)) return;
            dto.ID = maKMDangChon;

            try
            {
                if (kmBus.Sua(dto))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    LoadDanhSach();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maKMDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn khuyến mãi cần xóa!");
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa khuyến mãi này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                if (kmBus.Xoa(maKMDangChon))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDanhSach();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa khuyến mãi này vì đã có hóa đơn sử dụng.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCode.Clear();
            txtTenKM.Clear();
            txtPhanTram.Clear();
            txtGiamToiDa.Clear();
            txtDonToiThieu.Clear();
            txtMoTa.Clear();
            txtTimKiem.Clear();
            chkHoatDong.Checked = true;
            dtpNgayBatDau.Value = DateTime.Now.Date;
            dtpNgayKetThuc.Value = DateTime.Now.Date.AddDays(30);
            maKMDangChon = -1;
            txtMaCode.Focus();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvKhuyenMai.DataSource = kmBus.TimKiem(txtTimKiem.Text.Trim());
                DinhDangLuoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvKhuyenMai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvKhuyenMai.Rows[e.RowIndex];
            if (row.Cells[0].Value == null) return;

            maKMDangChon = Convert.ToInt32(row.Cells[0].Value);
            txtMaCode.Text = row.Cells[1].Value?.ToString() ?? "";
            txtTenKM.Text = row.Cells[2].Value?.ToString() ?? "";
            txtPhanTram.Text = row.Cells[3].Value?.ToString() ?? "0";
            txtGiamToiDa.Text = row.Cells[4].Value != DBNull.Value && row.Cells[4].Value != null
                ? Convert.ToDecimal(row.Cells[4].Value).ToString("N0") : "";
            txtDonToiThieu.Text = row.Cells[5].Value != DBNull.Value && row.Cells[5].Value != null
                ? Convert.ToDecimal(row.Cells[5].Value).ToString("N0") : "0";

            if (row.Cells[6].Value != DBNull.Value && row.Cells[6].Value != null)
                dtpNgayBatDau.Value = Convert.ToDateTime(row.Cells[6].Value);
            if (row.Cells[7].Value != DBNull.Value && row.Cells[7].Value != null)
                dtpNgayKetThuc.Value = Convert.ToDateTime(row.Cells[7].Value);

            chkHoatDong.Checked = row.Cells[8].Value != DBNull.Value && Convert.ToBoolean(row.Cells[8].Value);
            txtMoTa.Text = row.Cells[9].Value?.ToString() ?? "";
        }
    }
}
