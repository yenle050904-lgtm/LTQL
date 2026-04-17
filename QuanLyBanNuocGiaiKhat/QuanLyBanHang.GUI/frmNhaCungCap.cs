using System;
using System.Windows.Forms;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class frmNhaCungCap : Form
    {
        NhaCungCap_BUS nccBus = new NhaCungCap_BUS();

        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            try
            {
                dgvNCC.DataSource = nccBus.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string ten = txtTenNCC.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập Tên nhà cung cấp!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NhaCungCap_DTO dto = new NhaCungCap_DTO();
            dto.TenNhaCungCap = ten;
            dto.SoDienThoai = txtSDT.Text.Trim();
            dto.DiaChi = txtDiaChi.Text.Trim();
            dto.GhiChu = txtGhiChu.Text.Trim();

            try
            {
                if (nccBus.Them(dto))
                {
                    MessageBox.Show("Thêm nhà cung cấp thành công!", "Thông báo");
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
                MessageBox.Show("Lỗi khi thêm: " + ex.Message, "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvNCC.CurrentRow == null || dgvNCC.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp trên danh sách để sửa!");
                return;
            }

            string ten = txtTenNCC.Text.Trim();
            if (string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Tên nhà cung cấp không được để trống!");
                return;
            }

            NhaCungCap_DTO dto = new NhaCungCap_DTO();
            dto.ID = int.Parse(dgvNCC.CurrentRow.Cells[0].Value.ToString());
            dto.TenNhaCungCap = ten;
            dto.SoDienThoai = txtSDT.Text.Trim();
            dto.DiaChi = txtDiaChi.Text.Trim();
            dto.GhiChu = txtGhiChu.Text.Trim();

            try
            {
                if (nccBus.Sua(dto))
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
            if (dgvNCC.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp cần xóa!");
                return;
            }

            int id = int.Parse(dgvNCC.CurrentRow.Cells[0].Value.ToString());

            if (MessageBox.Show("Bạn chắc chắn muốn xóa nhà cung cấp này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                if (nccBus.Xoa(id))
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
                MessageBox.Show("Không thể xóa nhà cung cấp này vì đã có phiếu nhập liên quan.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenNCC.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            txtGhiChu.Clear();
            txtTimKiem.Clear();
            txtTenNCC.Focus();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvNCC.DataSource = nccBus.TimKiem(txtTimKiem.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvNCC.Rows[e.RowIndex];
            txtTenNCC.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
            txtSDT.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
            txtDiaChi.Text = row.Cells[3].Value?.ToString() ?? string.Empty;
            txtGhiChu.Text = row.Cells[4].Value?.ToString() ?? string.Empty;
        }
    }
}
