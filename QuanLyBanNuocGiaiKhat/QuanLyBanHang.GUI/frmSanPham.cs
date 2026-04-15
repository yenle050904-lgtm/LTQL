using QuanLyBanHang.DAO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class frmSanPham : Form
    {
        private int maSanPhamDangChon = -1;

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadDanhSachSanPham();
        }

        private void LoadComboBox()
        {
            try
            {
                SqlConnection conn = DataProvider.MoKetNoi();
                // Lấy ID và Tên để người dùng chọn
                DataTable dtLoai = DataProvider.TruyVanLayDuLieu("SELECT ID, TenLoai FROM LoaiSanPhams", conn);
                cboLoaiSanPham.DataSource = dtLoai;
                cboLoaiSanPham.DisplayMember = "TenLoai";
                cboLoaiSanPham.ValueMember = "ID";

                DataTable dtHang = DataProvider.TruyVanLayDuLieu("SELECT ID, TenHang FROM HangSanXuats", conn);
                cboHangSanXuat.DataSource = dtHang;
                cboHangSanXuat.DisplayMember = "TenHang";
                cboHangSanXuat.ValueMember = "ID";

                DataProvider.DongKetNoi(conn);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi load ComboBox: " + ex.Message); }
        }

        private void LoadDanhSachSanPham()
        {
            try
            {
                SqlConnection conn = DataProvider.MoKetNoi();
                // SELECT thêm cả 2 cột ID ẩn để tí nữa CellClick dùng tới
                string sql = @"SELECT sp.ID AS [Mã SP], 
                                     sp.TenSanPham AS [Tên SP], 
                                     lsp.TenLoai AS [Loại SP], 
                                     sp.SoLuongTon AS [Số lượng], 
                                     sp.DonGia AS [Đơn giá], 
                                     hsx.TenHang AS [Hãng SX],
                                     sp.LoaiSanPhamID,
                                     sp.HangSanXuatID
                              FROM SanPhams sp
                              INNER JOIN LoaiSanPhams lsp ON sp.LoaiSanPhamID = lsp.ID
                              INNER JOIN HangSanXuats hsx ON sp.HangSanXuatID = hsx.ID";

                DataTable dt = DataProvider.TruyVanLayDuLieu(sql, conn);
                dgvSP.DataSource = dt;

                // Ẩn 2 cột ID đi cho đẹp giao diện
                dgvSP.Columns["LoaiSanPhamID"].Visible = false;
                dgvSP.Columns["HangSanXuatID"].Visible = false;

                // Định dạng tiền tệ cho cột Đơn giá
                if (dgvSP.Columns.Contains("Đơn giá"))
                {
                    dgvSP.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                    dgvSP.Columns["Đơn giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvSP.Columns.Contains("Số lượng"))
                {
                    dgvSP.Columns["Số lượng"].DefaultCellStyle.Format = "N0";
                    dgvSP.Columns["Số lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                DataProvider.DongKetNoi(conn);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị: " + ex.Message); }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSP.Rows[e.RowIndex];

                // Lấy ID từ cột đầu tiên (Mã SP)
                maSanPhamDangChon = Convert.ToInt32(row.Cells[0].Value);

                // Đổ dữ liệu lên các ô nhập
                txtTenSanPham.Text = row.Cells[1].Value.ToString();

                // Định dạng số nguyên cho Số lượng và Đơn giá khi hiển thị trong ô nhập
                if (row.Cells[3].Value != DBNull.Value)
                    txtSoLuongTon.Text = Convert.ToDecimal(row.Cells[3].Value).ToString("N0");

                if (row.Cells[4].Value != DBNull.Value)
                    txtDonGia.Text = Convert.ToDecimal(row.Cells[4].Value).ToString("N0");

                // Để ComboBox tự chọn đúng tên Loại/Hãng
                cboLoaiSanPham.Text = row.Cells[2].Value.ToString();
                cboHangSanXuat.Text = row.Cells[5].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenSanPham.Text)) { MessageBox.Show("Vui lòng nhập tên!"); return; }

            try
            {
                // Làm sạch dữ liệu số trước khi lưu (bỏ dấu chấm/phẩy phân cách hàng nghìn)
                decimal donGia = decimal.Parse(txtDonGia.Text.Replace(".", "").Replace(",", ""));
                int soLuong = int.Parse(txtSoLuongTon.Text.Replace(".", "").Replace(",", ""));

                SqlConnection conn = DataProvider.MoKetNoi();
                string sql = "INSERT INTO SanPhams (TenSanPham, DonGia, SoLuongTon, LoaiSanPhamID, HangSanXuatID) " +
                           "VALUES (@TenSP, @DonGia, @SoLuong, @LoaiID, @HangID)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSanPham.Text);
                cmd.Parameters.AddWithValue("@DonGia", donGia);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@LoaiID", cboLoaiSanPham.SelectedValue);
                cmd.Parameters.AddWithValue("@HangID", cboHangSanXuat.SelectedValue);

                cmd.ExecuteNonQuery();
                DataProvider.DongKetNoi(conn);

                MessageBox.Show("Thêm thành công!");
                LoadDanhSachSanPham();
                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi thêm: Vui lòng kiểm tra lại định dạng số!\n" + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maSanPhamDangChon == -1)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm trên lưới trước khi sửa!", "Thông báo");
                return;
            }

            try
            {
                // Làm sạch dữ liệu số trước khi lưu (bỏ dấu chấm/phẩy phân cách hàng nghìn)
                decimal donGia = decimal.Parse(txtDonGia.Text.Replace(".", "").Replace(",", ""));
                int soLuong = int.Parse(txtSoLuongTon.Text.Replace(".", "").Replace(",", ""));

                SqlConnection conn = DataProvider.MoKetNoi();
                string sql = "UPDATE SanPhams SET TenSanPham = @TenSP, DonGia = @DonGia, SoLuongTon = @SoLuong, " +
                           "LoaiSanPhamID = @LoaiID, HangSanXuatID = @HangID WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenSP", txtTenSanPham.Text.Trim());
                cmd.Parameters.AddWithValue("@DonGia", donGia);
                cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                cmd.Parameters.AddWithValue("@LoaiID", cboLoaiSanPham.SelectedValue);
                cmd.Parameters.AddWithValue("@HangID", cboHangSanXuat.SelectedValue);
                cmd.Parameters.AddWithValue("@ID", maSanPhamDangChon);

                cmd.ExecuteNonQuery();
                DataProvider.DongKetNoi(conn);

                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo");
                LoadDanhSachSanPham();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: Vui lòng kiểm tra lại định dạng số!\n" + ex.Message, "Lỗi SQL");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maSanPhamDangChon == -1) return;
            if (MessageBox.Show("Bạn chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    SqlConnection conn = DataProvider.MoKetNoi();
                    string sqlXoa = $"DELETE FROM SanPhams WHERE ID = {maSanPhamDangChon}";
                    SqlCommand cmd = new SqlCommand(sqlXoa, conn);
                    cmd.ExecuteNonQuery();
                    DataProvider.DongKetNoi(conn);
                    LoadDanhSachSanPham();
                    btnLamMoi_Click(sender, e);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi xóa: " + ex.Message); }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTenSanPham.Clear();
            txtSoLuongTon.Clear();
            txtDonGia.Clear();
            if (cboLoaiSanPham.Items.Count > 0) cboLoaiSanPham.SelectedIndex = 0;
            if (cboHangSanXuat.Items.Count > 0) cboHangSanXuat.SelectedIndex = 0;
            maSanPhamDangChon = -1;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                SqlConnection conn = DataProvider.MoKetNoi();
                string sql = $@"SELECT sp.ID AS [Mã SP], sp.TenSanPham AS [Tên SP], lsp.TenLoai AS [Loại SP], 
                                       sp.SoLuongTon AS [Số lượng], sp.DonGia AS [Đơn giá], hsx.TenHang AS [Hãng SX],
                                       sp.LoaiSanPhamID, sp.HangSanXuatID
                                FROM SanPhams sp
                                INNER JOIN LoaiSanPhams lsp ON sp.LoaiSanPhamID = lsp.ID
                                INNER JOIN HangSanXuats hsx ON sp.HangSanXuatID = hsx.ID
                                WHERE sp.TenSanPham LIKE N'{tuKhoa}%'";

                dgvSP.DataSource = DataProvider.TruyVanLayDuLieu(sql, conn);

                // Định dạng lại các cột sau khi tìm kiếm
                if (dgvSP.Columns.Contains("Đơn giá"))
                {
                    dgvSP.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0\" VNĐ\"";
                    dgvSP.Columns["Đơn giá"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvSP.Columns.Contains("Số lượng"))
                {
                    dgvSP.Columns["Số lượng"].DefaultCellStyle.Format = "N0";
                    dgvSP.Columns["Số lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                DataProvider.DongKetNoi(conn);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Phát hiện lỗi ngầm: " + ex.Message);
            }
        }
    }
}