using System;
using System.Data;
using System.Windows.Forms;
using QuanLyBanHang.BUS; // Gọi BUS
using QuanLyBanHang.DTO; // Gọi DTO

namespace QuanLyBanHang.GUI
{
    public partial class frmNhanVien : Form
    {
        // Khai báo lớp BUS xử lý nghiệp vụ
        NhanVien_BUS nvBus = new NhanVien_BUS();

        public frmNhanVien()
        {
            InitializeComponent();
            this.Load += FrmNhanVien_Load;
        }

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
        }

        private void LoadDanhSachNhanVien()
        {
            try
            {
                dgvNhanVien.DataSource = nvBus.LayDanhSachNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                dgvNhanVien.DataSource = nvBus.TimKiemNhanVien(tuKhoa);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Phát hiện lỗi ngầm!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string hoTen = txtHoTen.Text.Trim();
                string dangNhap = txtDangNhap.Text.Trim();

                if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(dangNhap))
                {
                    MessageBox.Show("Vui lòng nhập đủ Họ tên và Tên đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đóng gói dữ liệu
                NhanVien_DTO nv = new NhanVien_DTO();
                nv.HoVaTen = hoTen;
                nv.DienThoai = txtDienThoai.Text.Trim();
                nv.TenDangNhap = dangNhap;
                nv.MatKhau = txtMatKhau.Text.Trim();
                nv.QuyenHan = chkAdmin.Checked ? 1 : 0;

                // Gọi BUS
                if (nvBus.ThemNhanVien(nv))
                {
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                    btnLamMoi_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm (Có thể tên đăng nhập đã tồn tại): \n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa trên lưới!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Đóng gói dữ liệu
                NhanVien_DTO nv = new NhanVien_DTO();
                nv.ID = int.Parse(txtID.Text);
                nv.HoVaTen = txtHoTen.Text.Trim();
                nv.DienThoai = txtDienThoai.Text.Trim();
                nv.TenDangNhap = txtDangNhap.Text.Trim();
                nv.MatKhau = txtMatKhau.Text.Trim();
                nv.QuyenHan = chkAdmin.Checked ? 1 : 0;

                // Gọi BUS
                if (nvBus.SuaNhanVien(nv))
                {
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = int.Parse(txtID.Text);
                    nvBus.XoaNhanVien(id);

                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachNhanVien();
                    btnLamMoi_Click(sender, e);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa nhân viên này vì họ đã có dữ liệu giao dịch!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtHoTen.Text = "";
            txtDienThoai.Text = "";
            txtDangNhap.Text = "";
            txtMatKhau.Text = "";
            chkAdmin.Checked = false;
            txtTimKiem.Text = "";
            txtHoTen.Focus();
        }

        private void dgvNhanVien_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtID.Text = row.Cells[0].Value.ToString();
                txtHoTen.Text = row.Cells[1].Value.ToString();
                txtDienThoai.Text = row.Cells[2].Value.ToString();
                txtDangNhap.Text = row.Cells[3].Value.ToString();
                txtMatKhau.Text = row.Cells[4].Value.ToString();
                chkAdmin.Checked = Convert.ToBoolean(row.Cells[5].Value);
            }
        }
    }
}