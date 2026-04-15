using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmBanHang : Form
    {
        // Sử dụng đúng tên class: BanHang_BUS (không phải BanHangBUS)
        private BanHang_BUS banHangBus = new BanHang_BUS();

        // Giỏ hàng dùng DataTable (không dùng class SanPham/HoaDon_ChiTiet vì không tồn tại trong project)
        private DataTable _gioHang;

        // Lưu thông tin sản phẩm đang chọn
        private int _maSPChon = -1;
        private string _tenSPChon = "";
        private int _soLuongTonChon = 0;
        private decimal _donGiaChon = 0;

        public frmBanHang()
        {
            InitializeComponent();
        }

        private void frmBanHang_Load(object sender, EventArgs e)
        {
            // Khởi tạo giỏ hàng DataTable
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

        // ==========================================
        // 1. CÁC HÀM TẢI DỮ LIỆU TỪ BUS
        // ==========================================
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
                cboKhachHang.DisplayMember = "Họ Tên"; // Đúng với alias trong SQL: HoTen AS [Họ Tên]
                cboKhachHang.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách khách hàng: " + ex.Message);
            }
        }

        // Hàm tính lại Tổng cộng từ DataTable giỏ hàng
        private void CapNhatTongCong()
        {
            decimal tongTien = 0;
            foreach (DataRow row in _gioHang.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtTongCong.Text = tongTien.ToString("N0") + " VNĐ";
        }

        // ==========================================
        // 2. XỬ LÝ SỰ KIỆN CHỌN SẢN PHẨM
        // ==========================================
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                _maSPChon = Convert.ToInt32(row.Cells[0].Value);      // Mã SP
                _tenSPChon = row.Cells[1].Value.ToString();            // Tên SP
                _soLuongTonChon = Convert.ToInt32(row.Cells[2].Value); // Tồn kho
                _donGiaChon = Convert.ToDecimal(row.Cells[3].Value);   // Đơn giá
            }
        }

        // ==========================================
        // 3. CÁC NÚT THÊM, XÓA, THANH TOÁN
        // ==========================================
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

            // Kiểm tra xem sản phẩm đã có trong giỏ chưa
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
                // Cộng dồn số lượng
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
            CapNhatTongCong();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_gioHang.Rows.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống, không thể thanh toán!");
                return;
            }

            // Lấy ID Khách hàng từ ComboBox
            int maKhachHang = (cboKhachHang.SelectedValue != null) ? Convert.ToInt32(cboKhachHang.SelectedValue) : 0;

            // Nhân viên đang đăng nhập
            int maNhanVien = UserSession.ID;


            // Tính tổng tiền
            decimal tongTien = 0;
            foreach (DataRow row in _gioHang.Rows)
                tongTien += Convert.ToDecimal(row["ThanhTien"]);

            try
            {
                bool kq = banHangBus.ThanhToan(maNhanVien, maKhachHang, _gioHang, tongTien);

                if (kq)
                {
                    MessageBox.Show("Đã chốt đơn thành công! Tồn kho đã được trừ.");
                    _gioHang.Rows.Clear();
                    _maSPChon = -1;
                    nudSoLuong.Value = 1;
                    txtTongCong.Text = "0 VNĐ";
                    LoadDanhSachSanPham();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu vào cơ sở dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nghiêm trọng khi thanh toán:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}