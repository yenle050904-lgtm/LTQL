using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    public partial class frmLoaiSanPham : Form
    {
        public frmLoaiSanPham()
        {
            InitializeComponent();
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
            UIHelper.StyleDataGridView(dgvLoaiSP);
        }

        private void LoadData()
        {
            using var db = new QLBHDbContext();
            dgvLoaiSP.DataSource = db.LoaiSanPhams
                .Select(l => new { l.ID, l.TenLoai })
                .ToList();
        }

        private void dgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                txtTenLoai.Text = dgvLoaiSP.Rows[e.RowIndex].Cells["TenLoai"].Value?.ToString() ?? "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            { errorProvider.SetError(txtTenLoai, "Không được để trống."); return; }
            errorProvider.Clear();

            using var db = new QLBHDbContext();
            db.LoaiSanPhams.Add(new LoaiSanPham { TenLoai = txtTenLoai.Text.Trim() });
            db.SaveChanges();
            LoadData(); txtTenLoai.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLoaiSP.CurrentRow == null) return;
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            { errorProvider.SetError(txtTenLoai, "Không được để trống."); return; }
            errorProvider.Clear();

            int id = (int)dgvLoaiSP.CurrentRow.Cells["ID"].Value;
            using var db = new QLBHDbContext();
            var item = db.LoaiSanPhams.Find(id);
            if (item != null) { item.TenLoai = txtTenLoai.Text.Trim(); db.SaveChanges(); }
            LoadData(); txtTenLoai.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLoaiSP.CurrentRow == null) return;
            if (MessageBox.Show("Xóa loại sản phẩm này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            int id = (int)dgvLoaiSP.CurrentRow.Cells["ID"].Value;
            try
            {
                using var db = new QLBHDbContext();
                var item = db.LoaiSanPhams.Find(id);
                if (item != null) { db.LoaiSanPhams.Remove(item); db.SaveChanges(); }
                LoadData(); txtTenLoai.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa (có sản phẩm liên quan): " +
                    (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
