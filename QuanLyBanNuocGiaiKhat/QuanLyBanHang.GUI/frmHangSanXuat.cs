using System;
using System.Windows.Forms;
using QuanLyBanHang.BUS;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.GUI
{
    public partial class frmHangSanXuat : Form
    {
        HangSanXuat_BUS hsxBus = new HangSanXuat_BUS();

        public frmHangSanXuat()
        {
            InitializeComponent();
        }

        private void frmHangSanXuat_Load(object sender, EventArgs e)
        {
            LoadDanhSach();
        }

        private void LoadDanhSach()
        {
            try
            {
                dgvHangSX.DataSource = hsxBus.LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string tenHang = txtTenHang.Text.Trim();
            if (string.IsNullOrEmpty(tenHang))
            {
                MessageBox.Show("Vui lòng nhập Tên hãng sản xuất!",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HangSanXuat_DTO dto = new HangSanXuat_DTO();
            dto.TenHang = tenHang;
            dto.QuocGia = txtQuocGia.Text.Trim();

            try
            {
                if (hsxBus.Them(dto))
                {
                    MessageBox.Show("Thêm hãng sản xuất thành công!", "Thông báo");
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
            if (dgvHangSX.CurrentRow == null || dgvHangSX.CurrentRow.Index < 0)
            {
                MessageBox.Show("Vui lòng chọn hãng trên danh sách để sửa!");
                return;
            }

            string tenHang = txtTenHang.Text.Trim();
            if (string.IsNullOrEmpty(tenHang))
            {
                MessageBox.Show("Tên hãng không được để trống!");
                return;
            }

            HangSanXuat_DTO dto = new HangSanXuat_DTO();
            dto.ID = int.Parse(dgvHangSX.CurrentRow.Cells[0].Value.ToString());
            dto.TenHang = tenHang;
            dto.QuocGia = txtQuocGia.Text.Trim();

            try
            {
                if (hsxBus.Sua(dto))
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
            if (dgvHangSX.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn hãng cần xóa!");
                return;
            }

            int id = int.Parse(dgvHangSX.CurrentRow.Cells[0].Value.ToString());

            if (MessageBox.Show("Bạn chắc chắn muốn xóa hãng sản xuất này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                if (hsxBus.Xoa(id))
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
                MessageBox.Show("Không thể xóa hãng này vì đã có sản phẩm thuộc hãng.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenHang.Clear();
            txtQuocGia.Clear();
            txtTimKiem.Clear();
            txtTenHang.Focus();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dgvHangSX.DataSource = hsxBus.TimKiem(txtTimKiem.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void dgvHangSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvHangSX.Rows[e.RowIndex];
            txtTenHang.Text = row.Cells[1].Value?.ToString() ?? string.Empty;
            txtQuocGia.Text = row.Cells[2].Value?.ToString() ?? string.Empty;
        }
    }
}
