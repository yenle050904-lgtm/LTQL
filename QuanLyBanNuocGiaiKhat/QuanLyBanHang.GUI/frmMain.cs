using System;
using System.Windows.Forms;

namespace QuanLyBanHang.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ApDungPhanQuyen();
            CheckCanhBaoHSD();  // Cảnh báo HSD khi khởi động
        }

        private void OpenForm(Form f)
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == f.GetType())
                {
                    form.Activate();
                    return;
                }
            }
            f.MdiParent = this;
            f.Show();
        }

        private void ApDungPhanQuyen()
        {
            bool isAdmin = UserSession.IsAdmin;
            string vaiTro = isAdmin ? "Quản trị viên" : "Nhân viên";
            string gioVao = UserSession.ThoiGianVao.ToString("HH:mm - dd/MM/yyyy");

            lblXinChao.Text = $"Xin chào: {UserSession.HoTen}  |  Vai trò: {vaiTro}  |  Giờ vào: {gioVao}";

            if (!isAdmin)
            {
                danhMụcToolStripMenuItem.Visible = false;
                báoCáoToolStripMenuItem.Visible = false;

                nhânViênToolStripMenuItem.Visible = false;
                kháchHàngToolStripMenuItem.Visible = false;
                sảnPhẩmToolStripMenuItem.Visible = false;
                nhậpHàngToolStripMenuItem.Visible = false;
                doanhThuToolStripMenuItem.Visible = false;
                hàngTồnKhoToolStripMenuItem.Visible = false;

                hệThốngToolStripMenuItem.Visible = true;
            }
        }

        /// <summary>
        /// Kiểm tra cảnh báo HSD, chỉ hiện cho admin khi có lô sắp/đã hết hạn.
        /// </summary>
        private void CheckCanhBaoHSD()
        {
            if (!UserSession.IsAdmin) return;

            try
            {
                BUS.HanSuDung_BUS hsdBus = new BUS.HanSuDung_BUS();
                hsdBus.DemCanhBao(30, out int soLoSapHet, out int soLoDaHet);

                if (soLoDaHet > 0 || soLoSapHet > 0)
                {
                    string msg = "";
                    if (soLoDaHet > 0)
                        msg += $"⚠ Có {soLoDaHet} lô hàng ĐÃ HẾT HẠN sử dụng.\n";
                    if (soLoSapHet > 0)
                        msg += $"⚠ Có {soLoSapHet} lô hàng SẮP HẾT HẠN (trong 30 ngày).\n";
                    msg += "\nBạn có muốn xem chi tiết không?";

                    if (MessageBox.Show(msg, "Cảnh báo HSD",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        OpenForm(new frmCanhBaoHSD());
                    }
                }
            }
            catch
            {
                // Nếu DB chưa có cột HanSuDung (chưa chạy patch Đợt 3), im lặng bỏ qua
            }
        }

        // ===================== DANH MỤC =====================

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmNhanVien());
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmKhachHang());
        }

        private void khuyếnMãiToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmKhuyenMai());
        }
        private void nhàCungCấpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmNhaCungCap());
        }
      
        private void khuyếnMãiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmKhuyenMai());
        }

        private void loạiSảnPhẩmToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmLoaiSanPham());
        }

        private void hãngSảnXuấtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmSanPham());
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmSanPham());
        }
        // ===================== NGHIỆP VỤ =====================

        private void bánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmBanHang());
        }

        private void nhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmNhapHang());
        }

        // MỚI: Trả hàng
        private void trảHàngToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmTraHang());
        }
        // ===================== BÁO CÁO =====================

        private void hàngTồnKhoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmThongKeTonKho());
        }

        private void doanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmThongKeDoanhThu());
        }

        private void lịchSửHóaĐơnToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmLichSuHoaDon());
        }

        private void lịchSửPhiếuNhậpToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmLichSuPhieuNhap());
        }
        private void cảnhBáoHSDToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmCanhBaoHSD());
        }

        private void topBánChạyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmTopBanChay());
        }

        private void báoCáoLãilỗToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenForm(new frmBaoCaoLaiLo());
        }

        // ===================== HỆ THỐNG =====================

        private void đổiMặtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDoiMatKhau f = new frmDoiMatKhau();
            f.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất khỏi tài khoản này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (kq == DialogResult.Yes)
            {
                UserSession.DangXuat();
                Application.Restart();
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn có thực sự muốn thoát chương trình?",
                "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (kq == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
