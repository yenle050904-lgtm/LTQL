using QuanLyBanHang.BUS;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class frmNhapHang : Form
    {

        NhapHang_BUS nhBus = new NhapHang_BUS();
        // Tạo một DataTable để làm "Giỏ hàng ảo" chứa các SP chuẩn bị nhập
        DataTable dtChiTietNhap = new DataTable();

        public frmNhapHang()
        {
            InitializeComponent();


        }

        private void frmNhapHang_Load_1(object sender, EventArgs e)
        {
            // 1. Gán ngày hiện tại cho Ngày lập
            dtpNgayLap.Value = DateTime.Now;

            // 2. Tạo các cột cho Giỏ hàng ảo
            dtChiTietNhap.Columns.Add("MaSP", typeof(int));
            dtChiTietNhap.Columns.Add("TenSP", typeof(string));
            dtChiTietNhap.Columns.Add("SoLuong", typeof(int));
            dtChiTietNhap.Columns.Add("GiaNhap", typeof(decimal));
            dtChiTietNhap.Columns.Add("ThanhTien", typeof(decimal));

            // Đổ giỏ hàng ảo lên DataGridView
            dgvChiTietNhap.DataSource = dtChiTietNhap;

            try
            {
                cboNCC.DataSource = nhBus.LayDanhSachNhaCungCap();
                cboNCC.DisplayMember = "TenNhaCungCap"; // Tên cột muốn hiển thị lên màn hình
                cboNCC.ValueMember = "ID";              // Tên cột chứa Mã (chìm bên dưới)

                cboChonSP.DataSource = nhBus.LayDanhSachSanPham();
                cboChonSP.DisplayMember = "TenSanPham";       // Tên cột tên SP trong SQL
                cboChonSP.ValueMember = "ID";                 // Tên cột Mã SP trong SQL
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ComboBox: " + ex.Message);
            }
        }

        // --- XỬ LÝ TỰ ĐỘNG TÍNH THÀNH TIỀN ---
        private void TinhThanhTien()
        {
            // Nếu Giá nhập là số và Số lượng > 0 thì tính Thành tiền
            if (decimal.TryParse(txtGiaNhap.Text, out decimal giaNhap))
            {
                decimal soLuong = nudSoLuongNhap.Value;
                txtThanhTienSP.Text = (giaNhap * soLuong).ToString("N0") + " VNĐ"; // N0 để có dấu chấm ngàn
            }
            else
            {
                txtThanhTienSP.Text = "0";
            }
        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e) => TinhThanhTien();
        private void nudSoLuongNhap_ValueChanged(object sender, EventArgs e) => TinhThanhTien();

        // --- TÍNH TỔNG TIỀN PHIẾU NHẬP DƯỚI CÙNG ---
        private void TinhTongTienPhieu()
        {
            decimal tongTien = 0;
            foreach (DataRow row in dtChiTietNhap.Rows)
            {
                tongTien += Convert.ToDecimal(row["ThanhTien"]);
            }
            txtTongTienPN.Text = tongTien.ToString("N0") + " VNĐ";
        }


        // --- NÚT LƯU PHIẾU (LƯU XUỐNG DATABASE) ---

        private void btnLuuPhieu_Click_1(object sender, EventArgs e)
        {
            if (dtChiTietNhap.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có sản phẩm nào trong phiếu để lưu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboNCC.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Lấy các dữ liệu cần thiết từ giao diện
                DateTime ngayLap = dtpNgayLap.Value;
                int maNCC = Convert.ToInt32(cboNCC.SelectedValue);
                string ghiChu = txtGhiChu.Text.Trim();

                // Tính lại tổng tiền cho chắc chắn
                decimal tongTien = 0;
                foreach (DataRow row in dtChiTietNhap.Rows)
                {
                    tongTien += Convert.ToDecimal(row["ThanhTien"]);
                }

                // 3. GỌI BUS ĐỂ THỰC HIỆN LƯU GIAO DỊCH
                int maNhanVien = UserSession.ID;
                bool ketQua = nhBus.LuuPhieuNhap(ngayLap, maNCC, maNhanVien, ghiChu, tongTien, dtChiTietNhap);


                if (ketQua)
                {
                    MessageBox.Show("Đã lưu Phiếu Nhập và cộng kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xóa giỏ hàng và reset giao diện chuẩn bị cho phiếu tiếp theo
                    dtChiTietNhap.Rows.Clear();
                    txtTongTienPN.Text = "0 VNĐ";
                    txtGhiChu.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Quá trình lưu thất bại, dữ liệu đã được hoàn tác. Lỗi chi tiết: \n" + ex.Message, "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemVaoPhieu_Click(object sender, EventArgs e)
        {
            if (cboChonSP.SelectedValue == null || string.IsNullOrEmpty(txtGiaNhap.Text))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm và nhập giá!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maSP = Convert.ToInt32(cboChonSP.SelectedValue);
            string tenSP = cboChonSP.Text;
            int soLuong = Convert.ToInt32(nudSoLuongNhap.Value);
            decimal giaNhap = Convert.ToDecimal(txtGiaNhap.Text);
            decimal thanhTien = soLuong * giaNhap;

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng ảo chưa
            bool daTonTai = false;
            foreach (DataRow row in dtChiTietNhap.Rows)
            {
                if (Convert.ToInt32(row["MaSP"]) == maSP)
                {
                    // Nếu có rồi thì cộng dồn số lượng và tính lại tiền
                    row["SoLuong"] = Convert.ToInt32(row["SoLuong"]) + soLuong;
                    row["ThanhTien"] = Convert.ToInt32(row["SoLuong"]) * giaNhap;
                    daTonTai = true;
                    break;
                }
            }

            // Nếu chưa có thì thêm dòng mới
            if (!daTonTai)
            {
                dtChiTietNhap.Rows.Add(maSP, tenSP, soLuong, giaNhap, thanhTien);
            }

            TinhTongTienPhieu();
        }

        private void btnHuyPhieu_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn hủy toàn bộ phiếu nhập này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                dtChiTietNhap.Rows.Clear(); // Xóa sạch giỏ hàng
                TinhTongTienPhieu();
                txtGhiChu.Clear();
            }
        }
    }
}