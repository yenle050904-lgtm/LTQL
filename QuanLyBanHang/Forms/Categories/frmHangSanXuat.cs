using System;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Categories
{
    public partial class frmHangSanXuat : Form
    {
        public frmHangSanXuat()
        {
            InitializeComponent();
        }

        private void frmHangSanXuat_Load(object sender, EventArgs e)
        {
            LoadData();
            UIHelper.StyleDataGridView(dgvHangSX);
        }

        private void LoadData()
        {
            using var db = new QLBHDbContext();
            dgvHangSX.DataSource = db.HangSanXuats
                .Select(h => new { h.ID, h.TenHangSanXuat })
                .ToList();
        }

        private void dgvHangSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                txtTenHang.Text = dgvHangSX.Rows[e.RowIndex].Cells["TenHangSanXuat"].Value?.ToString() ?? "";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenHang.Text))
            { errorProvider.SetError(txtTenHang, "Không được để trống."); return; }
            errorProvider.Clear();

            using var db = new QLBHDbContext();
            db.HangSanXuats.Add(new HangSanXuat { TenHangSanXuat = txtTenHang.Text.Trim() });
            db.SaveChanges();
            LoadData(); txtTenHang.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvHangSX.CurrentRow == null) return;
            if (string.IsNullOrWhiteSpace(txtTenHang.Text))
            { errorProvider.SetError(txtTenHang, "Không được để trống."); return; }
            errorProvider.Clear();

            int id = (int)dgvHangSX.CurrentRow.Cells["ID"].Value;
            using var db = new QLBHDbContext();
            var item = db.HangSanXuats.Find(id);
            if (item != null) { item.TenHangSanXuat = txtTenHang.Text.Trim(); db.SaveChanges(); }
            LoadData(); txtTenHang.Clear();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvHangSX.CurrentRow == null) return;
            if (MessageBox.Show("Xóa hãng sản xuất này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            int id = (int)dgvHangSX.CurrentRow.Cells["ID"].Value;
            try
            {
                using var db = new QLBHDbContext();
                var item = db.HangSanXuats.Find(id);
                if (item != null) { db.HangSanXuats.Remove(item); db.SaveChanges(); }
                LoadData(); txtTenHang.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể xóa: " + (ex.InnerException?.Message ?? ex.Message));
            }
        }
    }
}
