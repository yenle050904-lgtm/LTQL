using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS;  // Gọi lớp BUS
using QuanLyBanHang.DTO;  // Gọi lớp DTO

namespace QuanLyBanHang.GUI
{
    public partial class frmKhachHang : Form
    {
        // Khai báo anh Phục vụ (BUS) ở đây
        KhachHang_BUS khBus = new KhachHang_BUS();

        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            LoadDanhSachKhachHang();
        }

        private void LoadDanhSachKhachHang()
        {
            dgvKhachHang.DataSource = khBus.LayDanhSachKhachHang();
            dgvKhachHang.Columns["ID"].Visible = false; // Ẩn cột ID
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập Họ và Tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Đóng gói dữ liệu vào DTO
            KhachHang_DTO kh = new KhachHang_DTO();
            kh.HoTen = hoTen;
            kh.SoDienThoai = txtDienThoai.Text.Trim();
            kh.DiaChi = txtDiaChi.Text.Trim();

            // Nhờ BUS đi thêm mới
            if (khBus.ThemKhachHang(kh))
            {
                MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                LoadDanhSachKhachHang();
                txtHoTen.Text = ""; txtDienThoai.Text = ""; txtDiaChi.Text = "";
                txtHoTen.Focus();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null || dgvKhachHang.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một khách hàng trên danh sách để sửa!");
                return;
            }

            string hoTenMoi = txtHoTen.Text.Trim();
            if (string.IsNullOrEmpty(hoTenMoi))
            {
                MessageBox.Show("Họ tên không được để trống!");
                return;
            }

            // Lấy ID và đóng gói dữ liệu vào DTO
            KhachHang_DTO kh = new KhachHang_DTO();
            kh.ID = int.Parse(dgvKhachHang.CurrentRow.Cells[0].Value.ToString());
            kh.HoTen = hoTenMoi;
            kh.SoDienThoai = txtDienThoai.Text.Trim();
            kh.DiaChi = txtDiaChi.Text.Trim();

            // Nhờ BUS cập nhật
            if (khBus.SuaKhachHang(kh))
            {
                MessageBox.Show("Cập nhật thông tin khách hàng thành công!");
                LoadDanhSachKhachHang();
            }
            else
            {
                MessageBox.Show("Sửa thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!");
                return;
            }

            int idKhachHang = int.Parse(dgvKhachHang.CurrentRow.Cells[0].Value.ToString());

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (khBus.XoaKhachHang(idKhachHang))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadDanhSachKhachHang();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            dgvKhachHang.DataSource = khBus.TimKiemKhachHang(tuKhoa);
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvKhachHang.Rows[e.RowIndex];
                txtHoTen.Text = row.Cells[1].Value.ToString();
                txtDienThoai.Text = row.Cells[2].Value.ToString();
                txtDiaChi.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}