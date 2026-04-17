using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class frmSanPham : Form
    {
        private SanPham_BUS spBus = new SanPham_BUS();
        private int maSanPhamDangChon = -1;

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDanhSachSanPham();
        }

        // ===================== LOAD =====================

        private void LoadComboBox()
        {
            try
            {
                cboLoaiSanPham.DataSource = spBus.LayDanhSachLoaiSP();
                cboLoaiSanPham.DisplayMember = "TenLoai";
                cboLoaiSanPham.ValueMember = "ID";

                cboHangSanXuat.DataSource = spBus.LayDanhSachHangSX();
                cboHangSanXuat.DisplayMember = "TenHang";
                cboHangSanXuat.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load ComboBox: " + ex.Message);
            }
        }

        private void LoadDanhSachSanPham()
        {
            try
            {
                DataTable dt = spBus.LayDanhSachDayDu();
                dgvSP.DataSource = dt;
                DinhDangLuoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị: " + ex.Message);
            }
        }

        private void DinhDangLuoi()
        {
            if (dgvSP.Columns.Contains("LoaiSanPhamID"))
                dgvSP.Columns["LoaiSanPhamID"].Visible = false;
            if (dgvSP.Columns.Contains("HangSanXuatID"))
                dgvSP.Columns["HangSanXuatID"].Visible = false;

            if (dgvSP.Columns.Contains("Đơn giá"))
            {
                dgvSP.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                dgvSP.Columns["Đơn giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dgvSP.Columns.Contains("Số lượng"))
            {
                dgvSP.Columns["Số lượng"].DefaultCellStyle.Format = "N0";
                dgvSP.Columns["Số lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        // ===================== SỰ KIỆN =====================

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSP.Rows[e.RowIndex];

            if (row.Cells[0].Value == null) return;

            maSanPhamDangChon = Convert.ToInt32(row.Cells[0].Value);
            txtTenSanPham.Text = row.Cells[1].Value?.ToString() ?? string.Empty;

            if (row.Cells[3].Value != null && row.Cells[3].Value != DBNull.Value)
                txtSoLuongTon.Text = Convert.ToDecimal(row.Cells[3].Value).ToString("N0");

            if (row.Cells[4].Value != null && row.Cells[4].Value != DBNull.Value)
                txtDonGia.Text = Convert.ToDecimal(row.Cells[4].Value).ToString("N0");

            // Dùng giá trị ID để chọn combo (chính xác hơn so với Text)
            if (row.Cells["LoaiSanPhamID"] != null && row.Cells["LoaiSanPhamID"].Value != DBNull.Value)
                cboLoaiSanPham.SelectedValue = Convert.ToInt32(row.Cells["LoaiSanPhamID"].Value);
            if (row.Cells["HangSanXuatID"] != null && row.Cells["HangSanXuatID"].Value != DBNull.Value)
                cboHangSanXuat.SelectedValue = Convert.ToInt32(row.Cells["HangSanXuatID"].Value);
        }

        // Parse số an toàn, chấp nhận cả "10.000" và "10,000"
        private bool TryParseSoNguyen(string s, out int value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;
            string clean = s.Replace(".", "").Replace(",", "").Trim();
            return int.TryParse(clean, out value);
        }

        private bool TryParseDecimal(string s, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;
            string clean = s.Replace(".", "").Replace(",", "").Trim();
            return decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        }

        private bool DocDuLieuForm(out SanPham_DTO sp)
        {
            sp = new SanPham_DTO();

            if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Thông báo");
                return false;
            }

            if (!TryParseDecimal(txtDonGia.Text, out decimal donGia) || donGia < 0)
            {
                MessageBox.Show("Đơn giá không hợp lệ!", "Thông báo");
                return false;
            }

            if (!TryParseSoNguyen(txtSoLuongTon.Text, out int soLuong) || soLuong < 0)
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo");
                return false;
            }

            if (cboLoaiSanPham.SelectedValue == null || cboHangSanXuat.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Loại sản phẩm và Hãng sản xuất!", "Thông báo");
                return false;
            }

            sp.TenSanPham = txtTenSanPham.Text.Trim();
            sp.DonGia = donGia;
            sp.SoLuongTon = soLuong;
            sp.LoaiSanPhamID = Convert.ToInt32(cboLoaiSanPham.SelectedValue);
            sp.HangSanXuatID = Convert.ToInt32(cboHangSanXuat.SelectedValue);
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!DocDuLieuForm(out SanPham_DTO sp)) return;

            try
            {
                if (spBus.ThemSanPham(sp))
                {
                    MessageBox.Show("Thêm thành công!");
                    LoadDanhSachSanPham();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Thêm thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maSanPhamDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm trên lưới trước khi sửa!", "Thông báo");
                return;
            }

            if (!DocDuLieuForm(out SanPham_DTO sp)) return;
            sp.ID = maSanPhamDangChon;

            try
            {
                if (spBus.SuaSanPham(sp))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    LoadDanhSachSanPham();
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
            if (maSanPhamDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo");
                return;
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                if (spBus.XoaSanPham(maSanPhamDangChon))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadDanhSachSanPham();
                    btnLamMoi_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
            catch (Exception ex)
            {
                // SQL thường báo lỗi FK constraint khi sản phẩm đã nằm trong hóa đơn/phiếu nhập
                MessageBox.Show("Không thể xóa sản phẩm này. Có thể sản phẩm đã có trong hóa đơn hoặc phiếu nhập.\n\nChi tiết: " + ex.Message, "Lỗi");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenSanPham.Clear();
            txtSoLuongTon.Clear();
            txtDonGia.Clear();
            if (cboLoaiSanPham.Items.Count > 0) cboLoaiSanPham.SelectedIndex = 0;
            if (cboHangSanXuat.Items.Count > 0) cboHangSanXuat.SelectedIndex = 0;
            maSanPhamDangChon = -1;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                dgvSP.DataSource = spBus.TimKiemDayDu(tuKhoa);
                DinhDangLuoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }
    }
}
