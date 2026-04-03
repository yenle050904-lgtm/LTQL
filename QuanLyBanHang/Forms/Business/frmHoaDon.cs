using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using QuanLyBanHang.Data;
using QuanLyBanHang.Helpers;

namespace QuanLyBanHang.Forms.Business
{
    /// <summary>
    /// DTO hiển thị trên DataGridView — tách biệt khỏi EF Entity
    /// để tránh lỗi tracking khi gán navigation property.
    /// </summary>
    public class ChiTietDTO
    {
        public int SanPhamID { get; set; }
        public string TenSanPham { get; set; } = "";
        public short SoLuongBan { get; set; }
        public int DonGiaBan { get; set; }
        public long ThanhTien => SoLuongBan * DonGiaBan;
    }

    /// <summary>
    /// Form lập Hóa Đơn — logic nghiệp vụ chính.
    /// - Sử dụng DTO cho BindingList (không dùng Entity trực tiếp).
    /// - DbContext mới trong mỗi thao tác.
    /// - Transaction: lưu hóa đơn + trừ kho trong 1 đơn vị công việc.
    /// - Kiểm tra tồn kho trước khi thêm chi tiết.
    /// </summary>
    public partial class frmHoaDon : Form
    {
        private BindingList<ChiTietDTO> chiTietList;

        public frmHoaDon()
        {
            InitializeComponent();
            chiTietList = new BindingList<ChiTietDTO>();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            using var db = new QLBHDbContext();

            cboNhanVien.DataSource = db.NhanViens.ToList();
            cboNhanVien.DisplayMember = "HoVaTen";
            cboNhanVien.ValueMember = "ID";

            cboKhachHang.DataSource = db.KhachHangs.ToList();
            cboKhachHang.DisplayMember = "HoVaTen";
            cboKhachHang.ValueMember = "ID";

            cboSanPham.DataSource = db.SanPhams.ToList();
            cboSanPham.DisplayMember = "TenSanPham";
            cboSanPham.ValueMember = "ID";

            dgvChiTiet.DataSource = chiTietList;
            UIHelper.StyleDataGridView(dgvChiTiet);
            UpdateTongTien();
        }

        // ==============================
        // THÊM CHI TIẾT VÀO GIỎ (DTO)
        // ==============================
        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();

            if (cboSanPham.SelectedValue == null)
            {
                errorProvider.SetError(cboSanPham, "Chọn sản phẩm.");
                return;
            }

            if (!short.TryParse(txtSoLuongBan.Text, out short soLuongBan) || soLuongBan <= 0)
            {
                errorProvider.SetError(txtSoLuongBan, "Số lượng phải là số > 0.");
                return;
            }

            int spId = (int)cboSanPham.SelectedValue;

            // Kiểm tra tồn kho — dùng DbContext mới để có dữ liệu fresh
            using var db = new QLBHDbContext();
            var sanPham = db.SanPhams.Find(spId);
            if (sanPham == null) return;

            // Tính tổng đã thêm vào giỏ cho cùng SP
            int daDatTrongGio = chiTietList
                .Where(c => c.SanPhamID == spId)
                .Sum(c => c.SoLuongBan);

            if (daDatTrongGio + soLuongBan > sanPham.SoLuong)
            {
                MessageBox.Show(
                    $"Vượt quá tồn kho!\n" +
                    $"Tồn kho: {sanPham.SoLuong}\n" +
                    $"Đã đặt trong giỏ: {daDatTrongGio}\n" +
                    $"Yêu cầu thêm: {soLuongBan}",
                    "Cảnh báo tồn kho", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            chiTietList.Add(new ChiTietDTO
            {
                SanPhamID = spId,
                TenSanPham = sanPham.TenSanPham,
                SoLuongBan = soLuongBan,
                DonGiaBan = sanPham.DonGia
            });

            txtSoLuongBan.Clear();
            UpdateTongTien();
        }

        // ==============================
        // XÓA DÒNG CHI TIẾT
        // ==============================
        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            if (dgvChiTiet.CurrentRow != null && dgvChiTiet.CurrentRow.Index >= 0)
            {
                chiTietList.RemoveAt(dgvChiTiet.CurrentRow.Index);
                UpdateTongTien();
            }
        }

        // ==============================
        // CẬP NHẬT TỔNG TIỀN REAL-TIME
        // ==============================
        private void UpdateTongTien()
        {
            long tong = chiTietList.Sum(c => c.ThanhTien);
            lblTongTien.Text = $"💰 Tổng tiền: {tong:N0} VNĐ";
        }

        // ==============================
        // LƯU HÓA ĐƠN (TRANSACTION)
        // ==============================
        private void btnLuuHoaDon_Click(object sender, EventArgs e)
        {
            if (chiTietList.Count == 0)
            {
                MessageBox.Show("Phải có ít nhất 1 sản phẩm trong hóa đơn!",
                    "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo DbContext MỚI cho transaction — tránh stale data
            using var db = new QLBHDbContext();
            using var transaction = db.Database.BeginTransaction();

            try
            {
                // 1. Tạo hóa đơn
                if (cboNhanVien.SelectedValue == null || cboKhachHang.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên và khách hàng!", "Thiếu dữ liệu",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var hoaDon = new HoaDon
                {
                    NgayLap = dtpNgayLap.Value,
                    GhiChu = txtGhiChu.Text.Trim(),
                    NhanVienID = (int)cboNhanVien.SelectedValue!,
                    KhachHangID = (int)cboKhachHang.SelectedValue!
                };
                db.HoaDons.Add(hoaDon);
                db.SaveChanges(); // Lấy ID hóa đơn

                // 2. Duyệt chi tiết: thêm + trừ kho
                foreach (var ct in chiTietList)
                {
                    // Kiểm tra tồn kho lần cuối (trong transaction)
                    var sp = db.SanPhams.Find(ct.SanPhamID);
                    if (sp == null || sp.SoLuong < ct.SoLuongBan)
                    {
                        throw new Exception(
                            $"Sản phẩm '{ct.TenSanPham}' không đủ tồn kho " +
                            $"(Còn: {sp?.SoLuong ?? 0}, Cần: {ct.SoLuongBan}).");
                    }

                    // Thêm chi tiết hóa đơn
                    db.HoaDon_ChiTiets.Add(new HoaDon_ChiTiet
                    {
                        HoaDonID = hoaDon.ID,
                        SanPhamID = ct.SanPhamID,
                        SoLuongBan = ct.SoLuongBan,
                        DonGiaBan = ct.DonGiaBan
                    });

                    // Trừ kho
                    sp.SoLuong -= ct.SoLuongBan;
                }

                db.SaveChanges();
                transaction.Commit();

                MessageBox.Show("Lưu hóa đơn và trừ kho thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                chiTietList.Clear();
                UpdateTongTien();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show(
                    "Giao dịch thất bại — đã Rollback.\n\nLỗi: " + ex.Message,
                    "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
