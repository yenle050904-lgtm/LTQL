using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Load += frmMain_Load;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ApDungPhanQuyen();
            lblXinChao.Text = "Xin chào: " + UserSession.HoTen;
            lblXinChao.Text = $"Xin chào: {UserSession.HoTen} | Giờ vào: {UserSession.ThoiGianVao.ToString("HH:mm - dd/MM/yyyy")}";
        }

        /// <summary>
        /// Phân quyền menu dựa trên vai trò người dùng đang đăng nhập.
        /// Admin: toàn bộ menu.
        /// Nhân viên: chỉ Bán hàng + Hệ thống (Đổi mật khẩu, Đăng xuất, Thoát).
        /// </summary>
        private void ApDungPhanQuyen()
        {
            bool isAdmin = UserSession.IsAdmin;

            // Hiển thị tên người dùng ở thanh trạng thái
            lblXinChao.Text = $"Xin chào: {UserSession.HoTen}  |  Vai trò: {(isAdmin ? "Quản trị viên" : "Nhân viên")}";

            // ===== ADMIN: thấy tất cả =====
            // ===== NHÂN VIÊN: ẩn các menu sau =====
            danhMụcToolStripMenuItem.Visible = isAdmin; // Nhân viên, Khách hàng, Sản phẩm
            nhậpHàngToolStripMenuItem.Visible = isAdmin; // Nhập hàng
            báoCáoToolStripMenuItem.Visible = isAdmin; // Doanh thu, Tồn kho

            // Nhân viên chỉ còn: Bán hàng (nghiệpVụ vẫn Visible nhưng ẩn Nhập hàng)
            // Nếu ẩn hết sub-item thì ẩn luôn menu cha
            bool conSubItemNghiepVu = bánHàngToolStripMenuItem.Visible || nhậpHàngToolStripMenuItem.Visible;
            nghiệpVụToolStripMenuItem.Visible = true; // Luôn hiện vì có Bán hàng
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem form Nhân viên đã được mở sẵn chưa
            foreach (Form form in this.MdiChildren)
            {
                if (form.Name == "frmNhanVien")
                {
                    form.Activate(); // Nếu mở rồi thì chỉ cần đưa nó lên trên cùng
                    return;
                }
            }

            // 2. Nếu chưa mở thì tạo mới và cho hiển thị bên trong form Main
            frmNhanVien fNV = new frmNhanVien();
            fNV.MdiParent = this; // Khai báo form Main là form Mẹ
            fNV.Show();
        }

        private void hệThốngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Khởi tạo Form Khách Hàng (Lưu ý: Thay frmKhachHang bằng đúng tên Form của bạn)
            frmKhachHang f = new frmKhachHang();
            f.ShowDialog();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSanPham f = new frmSanPham();
            f.ShowDialog();
        }

        private void bánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBanHang f = new frmBanHang();
            f.ShowDialog();
        }

        private void nhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNhapHang f = new frmNhapHang();
            f.ShowDialog();
        }

        private void hàngTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongKeTonKho f = new frmThongKeTonKho();
            f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hiện hộp thoại hỏi xác nhận đăng xuất
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            // Nếu chọn Yes thì khởi động lại app để văng ra màn hình Đăng Nhập
            if (kq == DialogResult.Yes)
            {
                Application.Restart();
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hiện hộp thoại hỏi người dùng có chắc chắn muốn thoát không
            DialogResult kq = MessageBox.Show("Bạn có thực sự muốn thoát chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Nếu người dùng bấm Yes thì mới thoát
            if (kq == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void đổiMặtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau f = new frmDoiMatKhau();
            f.ShowDialog();
        }

        private void doanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongKeDoanhThu f = new frmThongKeDoanhThu();
            f.ShowDialog();
        }
    }
}
