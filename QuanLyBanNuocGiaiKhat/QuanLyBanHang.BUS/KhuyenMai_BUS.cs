using System.Data;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.BUS
{
    public class KhuyenMai_BUS
    {
        KhuyenMai_DAO kmDAO = new KhuyenMai_DAO();

        public DataTable LayDanhSach()
        {
            return kmDAO.LayDanhSach();
        }

        public DataTable TimKiem(string tuKhoa)
        {
            return kmDAO.TimKiem(tuKhoa);
        }

        public bool Them(KhuyenMai_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaCode)) return false;
            if (string.IsNullOrWhiteSpace(dto.TenKhuyenMai)) return false;
            if (dto.PhanTramGiam < 0 || dto.PhanTramGiam > 100) return false;
            if (dto.NgayBatDau > dto.NgayKetThuc) return false;
            return kmDAO.Them(dto);
        }

        public bool Sua(KhuyenMai_DTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaCode)) return false;
            if (string.IsNullOrWhiteSpace(dto.TenKhuyenMai)) return false;
            if (dto.PhanTramGiam < 0 || dto.PhanTramGiam > 100) return false;
            if (dto.NgayBatDau > dto.NgayKetThuc) return false;
            return kmDAO.Sua(dto);
        }

        public bool Xoa(int id)
        {
            return kmDAO.Xoa(id);
        }

        /// <summary>
        /// Kiểm tra mã và tính ra số tiền được giảm trên đơn hàng.
        /// </summary>
        public KhuyenMai_DTO KiemTraApDung(string maCode, decimal tongDonHang)
        {
            return kmDAO.KiemTraApDung(maCode, tongDonHang);
        }

        /// <summary>
        /// Tính tiền giảm thực tế dựa trên đơn hàng và mã KM (đã verify).
        /// </summary>
        public decimal TinhTienGiam(KhuyenMai_DTO km, decimal tongDon)
        {
            if (km == null) return 0;

            decimal tienGiam = tongDon * km.PhanTramGiam / 100m;

            // Áp trần nếu có
            if (km.GiamToiDa.HasValue && tienGiam > km.GiamToiDa.Value)
                tienGiam = km.GiamToiDa.Value;

            return tienGiam;
        }
    }
}
