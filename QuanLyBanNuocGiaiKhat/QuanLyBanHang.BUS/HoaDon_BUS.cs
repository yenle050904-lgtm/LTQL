using QuanLyBanHang.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.BUS
{
    public class HoaDon_BUS
    {
        HoaDon_DAO hdDAO = new HoaDon_DAO();
        public DataTable ThongKeDoanhThu(DateTime tuNgay, DateTime denNgay, string manv)
        {
            // Gọi hàm từ DAO (Nhớ khởi tạo biến hdDAO trước nhé)
            return hdDAO.ThongKeDoanhThu(tuNgay, denNgay, manv);
        }
    }
}
    
