using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmNhapHang : Form
    {
        NhapHang_BUS nhBus = new NhapHang_BUS();
        DataTable dtChiTietNhap = new DataTable();

        public frmNhapHang()
        {
            InitializeComponent();
        }

        private void frmNhapHang_Load_1(object sender, EventArgs e)
        {
            dtpNgayLap.Value = DateTime.Now;

            // Cột giỏ hàng ảo - CÓ THÊM HanSuDung
            dtChiTietNhap.Columns.Add("MaSP", typeof(int));
            dtChiTietNhap.Columns.Add("TenSP", typeof(string));
            dtChiTietNhap.Columns.Add("SoLuong", typeof(int));
            dtChiTietNhap.Columns.Add("GiaNhap", typeof(decimal));
            dtChiTietNhap.Columns.Add("ThanhTien", typeof(decimal));
            dtChiTietNhap.Columns.Add("HanSuDung", typeof(DateTime));

            dgvChiTietNhap.DataSource = dtChiTietNhap;

            try
            {
                cboNCC.DataSource = nhBus.LayDanhSachNhaCungCap();
                cboNCC.DisplayMember = "TenNhaCungCap";
                cboNCC.ValueMember = "ID";

                cboChonSP.DataSource = nhBus.LayDanhSachSanPham();
                cboChonSP.DisplayMember = "TenSanPham";
                cboChonSP.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ComboBox: " + ex.Message);
            }

            // Mặc định HSD = 6 tháng sau, nhưng disabled cho tới khi check
            if (dtpHanSuDung != null)
            {
                dtpHanSuDung.Value = DateTime.Now.Date.AddMonths(6);
                dtpHanSuDung.Enabled = chkCoHSD.Checked;
            }
        }

        private bool TryParseTien(string s, out decimal value)
        {
            value = 0;
            if (string.IsNullOrWhiteSpace(s)) return false;
            string clean = s.Replace(".", "").Replace(",", "").Replace("VNĐ", "").Trim();
            return decimal.TryParse(clean, NumberStyles.Any, CultureInfo.InvariantCulture, out value);
        }

        private void TinhThanhTien()
        {
            if (TryParseTien(txtGiaNhap.Text, out decimal giaNhap))
            {
                decimal soLuong = nudSoLuongNhap.Value;
                txtThanhTienSP.Text = (giaNhap * soLuong).ToString("N0") + " VNĐ";
            }
            else
            {
                txtThanhTienSP.Text = "0";
            }
        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e) => TinhThanhTien();
        private void nudSoLuongNhap_ValueChanged(object sender, EventArgs e) => TinhThanhTien();

        private void chkCoHSD_CheckedChanged(object sender, EventArgs e)
        {
            dtpHanSuDung.Enabled = chkCoHSD.Checked;
        }

        private void TinhTongTienPhieu()
        {
            decimal tongTien = 0;
            foreach (DataRow row in dtChiTietNhap.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtTongTienPN.Text = tongTien.ToString("N0") + " VNĐ";
        }

        private void btnThemVaoPhieu_Click(object sender, EventArgs e)
        {
            if (cboChonSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryParseTien(txtGiaNhap.Text, out decimal giaNhap) || giaNhap <= 0)
            {
                MessageBox.Show("Giá nhập không hợp lệ!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaNhap.Focus();
                return;
            }

            int maSP = Convert.ToInt32(cboChonSP.SelectedValue);
            string tenSP = cboChonSP.Text;
            int soLuong = Convert.ToInt32(nudSoLuongNhap.Value);

            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal thanhTien = soLuong * giaNhap;

            // HSD
            object hsdValue = DBNull.Value;
            if (chkCoHSD.Checked)
            {
                hsdValue = dtpHanSuDung.Value.Date;
                if (dtpHanSuDung.Value.Date < DateTime.Now.Date)
                {
                    if (MessageBox.Show("Hạn sử dụng đã qua, bạn vẫn muốn thêm?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;
                }
            }

            // Nếu đã có SP đó trong giỏ (+cùng HSD) thì cộng dồn
            bool daTonTai = false;
            foreach (DataRow row in dtChiTietNhap.Rows)
            {
                bool cungHSD = (row["HanSuDung"] == DBNull.Value && hsdValue == DBNull.Value)
                    || (row["HanSuDung"] != DBNull.Value && hsdValue != DBNull.Value 
                        && Convert.ToDateTime(row["HanSuDung"]).Date == Convert.ToDateTime(hsdValue).Date);

                if (Convert.ToInt32(row["MaSP"]) == maSP && cungHSD)
                {
                    int slMoi = Convert.ToInt32(row["SoLuong"]) + soLuong;
                    row["SoLuong"] = slMoi;
                    row["GiaNhap"] = giaNhap;
                    row["ThanhTien"] = slMoi * giaNhap;
                    daTonTai = true;
                    break;
                }
            }

            if (!daTonTai)
            {
                DataRow newRow = dtChiTietNhap.NewRow();
                newRow["MaSP"] = maSP;
                newRow["TenSP"] = tenSP;
                newRow["SoLuong"] = soLuong;
                newRow["GiaNhap"] = giaNhap;
                newRow["ThanhTien"] = thanhTien;
                newRow["HanSuDung"] = hsdValue;
                dtChiTietNhap.Rows.Add(newRow);
            }

            TinhTongTienPhieu();
        }

        private void btnLuuPhieu_Click_1(object sender, EventArgs e)
        {
            if (dtChiTietNhap.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong phiếu để lưu!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboNCC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DateTime ngayLap = dtpNgayLap.Value;
                int maNCC = Convert.ToInt32(cboNCC.SelectedValue);
                int maNhanVien = UserSession.ID;
                string ghiChu = txtGhiChu.Text.Trim();

                decimal tongTien = 0;
                foreach (DataRow row in dtChiTietNhap.Rows)
                    tongTien += Convert.ToDecimal(row["ThanhTien"]);

                bool ketQua = nhBus.LuuPhieuNhap(ngayLap, maNCC, maNhanVien, ghiChu, tongTien, dtChiTietNhap);

                if (ketQua)
                {
                    MessageBox.Show("Đã lưu Phiếu Nhập và cộng kho thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dtChiTietNhap.Rows.Clear();
                    txtTongTienPN.Text = "0 VNĐ";
                    txtGhiChu.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Quá trình lưu thất bại, dữ liệu đã được hoàn tác.\n\nChi tiết: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyPhieu_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy toàn bộ phiếu nhập này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dtChiTietNhap.Rows.Clear();
                TinhTongTienPhieu();
                txtGhiChu.Clear();
            }
        }
    }
}
