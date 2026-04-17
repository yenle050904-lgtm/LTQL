using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyBanHang.BUS;

namespace QuanLyBanHang.GUI
{
    public partial class frmCanhBaoHSD : Form
    {
        HanSuDung_BUS hsdBus = new HanSuDung_BUS();

        public frmCanhBaoHSD()
        {
            InitializeComponent();
        }

        private void frmCanhBaoHSD_Load(object sender, EventArgs e)
        {
            cboLoc.Items.Clear();
            cboLoc.Items.Add("Tất cả");
            cboLoc.Items.Add("Sắp hết hạn");
            cboLoc.Items.Add("Đã hết hạn");
            cboLoc.SelectedIndex = 1;  // Mặc định xem sắp hết hạn

            nudSoNgay.Value = 30;
            LoadDuLieu();
        }

        private void LoadDuLieu()
        {
            try
            {
                int loaiLoc = cboLoc.SelectedIndex;
                if (loaiLoc < 0) loaiLoc = 0;

                int soNgay = (int)nudSoNgay.Value;

                DataTable dt = hsdBus.LayDanhSachTheoHSD(loaiLoc, soNgay);
                dgvHSD.DataSource = dt;

                // Định dạng cột
                if (dgvHSD.Columns.Contains("Giá Nhập"))
                {
                    dgvHSD.Columns["Giá Nhập"].DefaultCellStyle.Format = "#,##0";
                    dgvHSD.Columns["Giá Nhập"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgvHSD.Columns.Contains("Ngày Nhập"))
                    dgvHSD.Columns["Ngày Nhập"].DefaultCellStyle.Format = "dd/MM/yyyy";
                if (dgvHSD.Columns.Contains("Hạn Sử Dụng"))
                    dgvHSD.Columns["Hạn Sử Dụng"].DefaultCellStyle.Format = "dd/MM/yyyy";

                // Tô màu các dòng theo trạng thái HSD
                ToMauDong();

                // Cập nhật đếm
                hsdBus.DemCanhBao(soNgay, out int soLoSapHet, out int soLoDaHet);
                lblThongKe.Text = $"Tổng: {soLoSapHet} lô sắp hết hạn   |   {soLoDaHet} lô đã hết hạn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        private void ToMauDong()
        {
            foreach (DataGridViewRow row in dgvHSD.Rows)
            {
                if (row.Cells["Còn Lại (ngày)"]?.Value == null) continue;
                if (row.Cells["Còn Lại (ngày)"].Value == DBNull.Value) continue;

                int conLai = Convert.ToInt32(row.Cells["Còn Lại (ngày)"].Value);

                if (conLai < 0)
                {
                    // Đã hết hạn - đỏ
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
                else if (conLai <= 7)
                {
                    // Cực kỳ gấp - cam đậm
                    row.DefaultCellStyle.BackColor = Color.LightSalmon;
                }
                else if (conLai <= 30)
                {
                    // Sắp hết - vàng
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDuLieu();
        }

        private void cboLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDuLieu();
        }

        private void nudSoNgay_ValueChanged(object sender, EventArgs e)
        {
            LoadDuLieu();
        }
    }
}
