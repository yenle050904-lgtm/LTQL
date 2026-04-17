using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class frmBanHang : Form
    {
        private BanHang_BUS banHangBus = new BanHang_BUS();
        private KhuyenMai_BUS kmBus = new KhuyenMai_BUS();
        private DataTable _gioHang;

        // Sản phẩm đang chọn
        private int _maSPChon = -1;
        private string _tenSPChon = "";
        private int _soLuongTonChon = 0;
        private decimal _donGiaChon = 0;

        // Khuyến mãi đang áp dụng
        private KhuyenMai_DTO _kmApDung = null;
        private decimal _tienGiam = 0;

        public frmBanHang()
        {
            InitializeComponent();
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            _gioHang = new DataTable();
            _gioHang.Columns.Add("MaSP", typeof(int));
            _gioHang.Columns.Add("TenSP", typeof(string));
            _gioHang.Columns.Add("SoLuong", typeof(int));
            _gioHang.Columns.Add("DonGia", typeof(decimal));
            _gioHang.Columns.Add("ThanhTien", typeof(decimal));

            dgvGioHang.DataSource = _gioHang;

            LoadDanhSachSanPham();
            LoadKhachHang();
        }

        private void LoadDanhSachSanPham()
        {
            try
            {
                dgvSanPham.DataSource = banHangBus.LayDanhSachSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        private void LoadKhachHang()
        {
            try
            {
                cboKhachHang.DataSource = banHangBus.LayDanhSachKhachHang();
                cboKhachHang.DisplayMember = "HoTen";
                cboKhachHang.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message);
            }
        }

        // Tính tổng cộng: hiển thị cả tổng trước giảm, tiền giảm, và tổng sau giảm
        private void CapNhatTongCong()
        {
            decimal tongGoc = 0;
            foreach (DataRow row in _gioHang.Rows)
            {
                tongGoc += Convert.ToDecimal(row["ThanhTien"]);
            }

            // Nếu đã có KM áp dụng, tính lại tiền giảm theo tổng mới
            if (_kmApDung != null)
            {
                _tienGiam = kmBus.TinhTienGiam(_kmApDung, tongGoc);

                // Nếu tổng đơn giờ không còn đủ điều kiện tối thiểu -> hủy áp dụng
                if (tongGoc < _kmApDung.DonToiThieu)
                {
                    HuyKhuyenMai();
                    _tienGiam = 0;
                }
            }

            decimal tongSauGiam = tongGoc - _tienGiam;
            if (tongSauGiam < 0) tongSauGiam = 0;

            txtTongCong.Text = tongSauGiam.ToString("N0") + " VNĐ";

            // Các textbox hiển thị thêm (nếu Designer có)
            if (txtTamTinh != null) txtTamTinh.Text = tongGoc.ToString("N0") + " VNĐ";
            if (txtTienGiam != null) txtTienGiam.Text = _tienGiam.ToString("N0") + " VNĐ";
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
            _maSPChon = Convert.ToInt32(row.Cells[0].Value);
            _tenSPChon = row.Cells[1].Value.ToString();
            _soLuongTonChon = Convert.ToInt32(row.Cells[2].Value);
            _donGiaChon = Convert.ToDecimal(row.Cells[3].Value);
        }

        private void btnThemVaoHoaDon_Click(object sender, EventArgs e)
        {
            if (_maSPChon == -1)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm từ danh sách bên trái!");
                return;
            }

            int soLuongMua = (int)nudSoLuong.Value;
            if (soLuongMua <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
                return;
            }

            DataRow monDaCo = null;
            foreach (DataRow row in _gioHang.Rows)
            {
                if (Convert.ToInt32(row["MaSP"]) == _maSPChon)
                {
                    monDaCo = row;
                    break;
                }
            }

            if (monDaCo != null)
            {
                int slMoi = Convert.ToInt32(monDaCo["SoLuong"]) + soLuongMua;
                if (slMoi > _soLuongTonChon)
                {
                    MessageBox.Show($"Tổng số lượng mua ({slMoi}) vượt quá tồn kho ({_soLuongTonChon})!");
                    return;
                }
                monDaCo["SoLuong"] = slMoi;
                monDaCo["ThanhTien"] = slMoi * _donGiaChon;
            }
            else
            {
                if (soLuongMua > _soLuongTonChon)
                {
                    MessageBox.Show($"Số lượng mua ({soLuongMua}) vượt quá tồn kho ({_soLuongTonChon})!");
                    return;
                }
                _gioHang.Rows.Add(_maSPChon, _tenSPChon, soLuongMua, _donGiaChon, soLuongMua * _donGiaChon);
            }

            CapNhatTongCong();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow != null && dgvGioHang.CurrentRow.Index >= 0)
            {
                _gioHang.Rows.RemoveAt(dgvGioHang.CurrentRow.Index);
                CapNhatTongCong();
            }
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            _gioHang.Rows.Clear();
            HuyKhuyenMai();
            CapNhatTongCong();
        }

        // =============== KHUYẾN MÃI ===============

        private void btnApDungKM_Click(object sender, EventArgs e)
        {
            if (_gioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng trống, không thể áp dụng khuyến mãi!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string maCode = txtMaKM.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(maCode))
            {
                MessageBox.Show("Vui lòng nhập mã khuyến mãi!");
                return;
            }

            // Tính tổng gốc
            decimal tongGoc = 0;
            foreach (DataRow row in _gioHang.Rows)
                tongGoc += Convert.ToDecimal(row["ThanhTien"]);

            try
            {
                KhuyenMai_DTO km = kmBus.KiemTraApDung(maCode, tongGoc);
                if (km == null)
                {
                    MessageBox.Show("Mã khuyến mãi không hợp lệ!\n\nCó thể do:\n- Mã không tồn tại\n- Mã đã bị tạm dừng\n- Đã hết hạn\n- Đơn hàng chưa đủ giá trị tối thiểu",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    HuyKhuyenMai();
                    return;
                }

                _kmApDung = km;
                _tienGiam = kmBus.TinhTienGiam(km, tongGoc);

                if (lblKMApDung != null)
                    lblKMApDung.Text = $"✓ {km.TenKhuyenMai} (-{_tienGiam:N0} VNĐ)";

                CapNhatTongCong();

                MessageBox.Show($"Áp dụng mã '{km.MaCode}' thành công!\nGiảm {_tienGiam:N0} VNĐ",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra mã KM: " + ex.Message);
            }
        }

        private void btnHuyKM_Click(object sender, EventArgs e)
        {
            HuyKhuyenMai();
            CapNhatTongCong();
        }

        private void HuyKhuyenMai()
        {
            _kmApDung = null;
            _tienGiam = 0;
            if (txtMaKM != null) txtMaKM.Clear();
            if (lblKMApDung != null) lblKMApDung.Text = "(Chưa áp dụng)";
        }

        // =============== THANH TOÁN ===============

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_gioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống, không thể thanh toán!");
                return;
            }

            int maKhachHang = (cboKhachHang.SelectedValue != null)
                ? Convert.ToInt32(cboKhachHang.SelectedValue)
                : 0;
            int maNhanVien = UserSession.ID;

            decimal tongGoc = 0;
            foreach (DataRow row in _gioHang.Rows)
                tongGoc += Convert.ToDecimal(row["ThanhTien"]);

            decimal tongThanhToan = tongGoc - _tienGiam;
            if (tongThanhToan < 0) tongThanhToan = 0;

            try
            {
                // Lưu ý: phiên bản hiện tại BanHang_BUS.ThanhToan chưa nhận tham số khuyến mãi
                // nên ta lưu tongThanhToan (đã trừ KM) vào TongTien của HĐ.
                // Nếu muốn lưu riêng KhuyenMaiID và TienGiam vào HoaDons, sửa thêm DAO/BUS.
                int maHoaDon = banHangBus.ThanhToan(maNhanVien, maKhachHang, _gioHang, tongThanhToan);

                if (maHoaDon > 0)
                {
                    string thongBao = $"Đã chốt đơn thành công! Mã hóa đơn: {maHoaDon}";
                    if (_tienGiam > 0)
                        thongBao += $"\nKhuyến mãi đã giảm: {_tienGiam:N0} VNĐ";
                    thongBao += "\n\nBạn có muốn in hóa đơn không?";

                    DialogResult kq = MessageBox.Show(thongBao,
                        "Thanh toán thành công",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (kq == DialogResult.Yes)
                    {
                        frmInHoaDon frm = new frmInHoaDon(maHoaDon);
                        frm.ShowDialog();
                    }

                    // Reset
                    _gioHang.Rows.Clear();
                    _maSPChon = -1;
                    nudSoLuong.Value = 1;
                    HuyKhuyenMai();
                    CapNhatTongCong();
                    LoadDanhSachSanPham();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu vào cơ sở dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nghiêm trọng khi thanh toán:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApDungKM_Click_1(object sender, EventArgs e)
        {

        }

        private void btnHuyKM_Click_1(object sender, EventArgs e)
        {

        }

        private void lblKMApDung_Click(object sender, EventArgs e)
        {

        }

        private void txtMaKM_Enter(object sender, EventArgs e)
        {
            if (txtMaKM.Text == "Nhập mã KM...")
            {
                txtMaKM.Text = ""; // Xóa chữ gợi ý đi
                txtMaKM.ForeColor = Color.Black; // Đổi màu chữ sang đen để người dùng nhập
            }
        }

        private void txtMaKM_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKM.Text))
            {
                txtMaKM.Text = "Nhập mã KM..."; // Nếu để trống thì hiện lại chữ gợi ý
                txtMaKM.ForeColor = Color.Gray; // Đổi lại màu xám
            }
        }
    }
}
