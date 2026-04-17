using System;
using System.Windows.Forms;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class frmLoaiSanPham : Form
    {
        LoaiSanPham_BUS lspBus = new LoaiSanPham_BUS();

        public frmLoaiSanPham()
        {
            InitializeComponent();
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            try
            {
                dgvLoaiSP.DataSource = lspBus.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenLoai = txtTenLoai.Text.Trim();
            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Vui lòng nhập Tên loại sản phẩm!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoaiSanPham_DTO dto = new LoaiSanPham_DTO();
            dto.TenLoai = tenLoai;
            dto.MoTa = txtMoTa.Text.Trim();

            try
            {
                if (lspBus.Them(dto))
                {
                    MessageBox.Show("Thêm loại sản phẩm thành công!", "Thông báo");
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
            if (dgvLoaiSP.CurrentRow == null || dgvLoaiSP.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn một loại trên danh sách để sửa!");
                return;
            }

            string tenLoai = txtTenLoai.Text.Trim();
            if (string.IsNullOrEmpty(tenLoai))
            {
                MessageBox.Show("Tên loại không được để trống!");
                return;
            }

            LoaiSanPham_DTO dto = new LoaiSanPham_DTO();
            dto.ID = int.Parse(dgvLoaiSP.CurrentRow.Cells[0].Value.ToString());
            dto.TenLoai = tenLoai;
            dto.MoTa = txtMoTa.Text.Trim();

            try
            {
                if (lspBus.Sua(dto))
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
            if (dgvLoaiSP.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn loại cần xóa!");
                return;
            }

            int id = int.Parse(dgvLoaiSP.CurrentRow.Cells[0].Value.ToString());

            if (MessageBox.Show("Bạn chắc chắn muốn xóa loại sản phẩm này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                if (lspBus.Xoa(id))
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
                MessageBox.Show("Không thể xóa loại này vì đã có sản phẩm thuộc loại.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenLoai.Clear();
            txtMoTa.Clear();
            txtTimKiem.Clear();
            txtTenLoai.Focus();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvLoaiSP.DataSource = lspBus.TimKiem(txtTimKiem.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvLoaiSP.Rows[e.RowIndex];
            txtTenLoai.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
            txtMoTa.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
        }
    }
}
