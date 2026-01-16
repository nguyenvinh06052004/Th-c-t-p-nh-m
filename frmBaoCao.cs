using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace THUEPHONG
{
    public partial class frmBaoCao : Form
    {
        private Entities db;
        public frmBaoCao()
        {
            InitializeComponent();

            // Khởi tạo DbContext ngay khi form mở
            db = Entities.CreateEntities();  // Sử dụng phương thức static của bạn

            // Tùy chỉnh giao diện chung
            this.Text = "BÁO CÁO QUẢN LÝ KHÁCH SẠN";
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            tabControlBaoCao.Font = new System.Drawing.Font("Segoe UI", 10F);
            tabControlBaoCao.Appearance = TabAppearance.FlatButtons;

            // Load dữ liệu khi chuyển tab
            tabControlBaoCao.SelectedIndexChanged += tabControlBaoCao_SelectedIndexChanged;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            db?.Dispose(); // Giải phóng DbContext khi đóng form
            base.OnFormClosing(e);
        }

        private void LoadDoanhThu()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1); // Bao gồm hết ngày

            try
            {
                var doanhThuList = db.DatPhong
                    .Where(dp => dp.NgayDatPhong >= tuNgay && dp.NgayDatPhong <= denNgay
                              && dp.Status == true && dp.Disabled != true)
                    .Select(dp => new
                    {
                        Id = dp.IdDatPhong,
                        KhachHang = dp.KhachHang != null ? dp.KhachHang.HoTen : "Khách vãng lai",  // Sửa dòng này: Sử dụng navigation property trực tiếp
                        NgayDat = dp.NgayDatPhong,
                        TongTienPhong = dp.DatPhong_ChiTiet.Sum(ct => ct.ThanhTien ?? 0),
                        TongTienSanPham = dp.DatPhong_SanPham.Sum(sp => sp.ThanhTien ?? 0),
                        TongTien = (dp.SoTien ?? 0) + dp.DatPhong_ChiTiet.Sum(ct => ct.ThanhTien ?? 0)
                                 + dp.DatPhong_SanPham.Sum(sp => sp.ThanhTien ?? 0)
                    })
                    .OrderByDescending(x => x.NgayDat)
                    .ToList();

                dgvDoanhThu.DataSource = doanhThuList;


                dgvDoanhThu.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Định dạng tiền Việt Nam
                dgvDoanhThu.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgvDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDoanhThu.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                dgvDoanhThu.AllowUserToResizeColumns = true;
                dgvDoanhThu.RowHeadersVisible = false;
                // Biểu đồ doanh thu theo ngày
                chartDoanhThu.Series.Clear();
                var series = new Series("Doanh Thu") { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.DodgerBlue };

                var doanhThuTheoNgay = doanhThuList
                    .GroupBy(d => d.NgayDat?.Date)
                    .Select(g => new { Ngay = g.Key, Tong = g.Sum(d => d.TongTien) })
                    .OrderBy(x => x.Ngay);

                foreach (var item in doanhThuTheoNgay)
                {
                    series.Points.AddXY(item.Ngay?.ToString("dd/MM"), item.Tong);
                }
                chartDoanhThu.Series.Add(series);
                chartDoanhThu.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (nghìn VNĐ)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // ================= BIỂU ĐỒ XU HƯỚNG DOANH THU TRONG NĂM =================
            chartXuHuongNam.Series.Clear();
            chartXuHuongNam.ChartAreas.Clear();

            // ChartArea
            ChartArea area = new ChartArea("AreaXuHuong");
            area.AxisX.Title = "Tháng";
            area.AxisY.Title = "Doanh thu (nghìn VNĐ)";
            area.AxisX.Interval = 1;
            chartXuHuongNam.ChartAreas.Add(area);

            // Series Line
            Series lineSeries = new Series("Xu hướng doanh thu trong năm")
            {
                ChartType = SeriesChartType.Line,
                BorderWidth = 3,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 7,
                Color = Color.OrangeRed
            };

            // Năm đang chọn (theo dtpTuNgay)
            int nam = dtpTuNgay.Value.Year;

            // Gom doanh thu theo tháng
            var doanhThuTheoThang = db.DatPhong
                .Where(dp => dp.NgayDatPhong.HasValue
                          && dp.NgayDatPhong.Value.Year == nam
                          && dp.Status == true
                          && dp.Disabled != true)
                .GroupBy(dp => dp.NgayDatPhong.Value.Month)
                .Select(g => new
                {
                    Thang = g.Key,
                    TongTien = g.Sum(dp =>
                        (dp.SoTien ?? 0)
                      + dp.DatPhong_ChiTiet.Sum(ct => ct.ThanhTien ?? 0)
                      + dp.DatPhong_SanPham.Sum(sp => sp.ThanhTien ?? 0))
                })
                .OrderBy(x => x.Thang)
                .ToList();

            // Đổ dữ liệu vào chart
            foreach (var item in doanhThuTheoThang)
            {
                lineSeries.Points.AddXY("Tháng " + item.Thang, item.TongTien);
            }

            chartXuHuongNam.Series.Add(lineSeries);

            // Format
            chartXuHuongNam.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            chartXuHuongNam.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chartXuHuongNam.Legends.Clear();
            chartXuHuongNam.Legends.Add(new Legend());

        }

        private void tabControlBaoCao_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControlBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlBaoCao.SelectedTab == tabTiLeDatPhong)
            {
                LoadChiemCho();
            }
        }

        private void btnLoadDoanhThu_Click(object sender, EventArgs e)
        {
            LoadDoanhThu();
        }

        private void dtpTuNgay_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabDoanhThu_Click(object sender, EventArgs e)
        {
            LoadDoanhThu();
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            LoadDoanhThu();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dgvDoanhThu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnLoadChiemCho_Click(object sender, EventArgs e)
        {
            LoadChiemCho();

        }
        private void LoadChiemCho()
        {
            DateTime tuNgay = dtpTuNgay.Value.Date;
            DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddTicks(-1);

            try
            {
                // Tổng số phòng hoạt động
                int tongPhong = db.Phong.Count(p => p.Disabled != true);

                // Số phòng đang được đặt trong khoảng thời gian
                var phongDangChiem = db.DatPhong_ChiTiet
                    .Where(ct => ct.Ngay >= tuNgay && ct.Ngay <= denNgay
                              && ct.DatPhong.Status == true && ct.DatPhong.Disabled != true)
                    .Select(ct => ct.IdPhong)
                    .Distinct()
                    .Count();

                double tyLeChiem = tongPhong > 0 ? (double)phongDangChiem / tongPhong * 100 : 0;

                // Hiển thị danh sách phòng
                dgvPhong.DataSource = db.Phong
                    .Where(p => p.Disabled != true)
                    .Select(p => new
                    {
                        Tên_Phòng = p.TenPhong,
                        Tầng = p.Tang.TenTang,
                        Loại_Phòng = p.LoaiPhong.TenLoaiPhong,
                        Trạng_Thái = p.TrangThai
                    })
                    .ToList();

                dgvPhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Biểu đồ Pie
                chartChiemCho.Series.Clear();
                var series = new Series("Tỷ Lệ Chiếm Chỗ") { ChartType = SeriesChartType.Pie };
                series.Points.AddXY($"Chiếm chỗ ({phongDangChiem} phòng)", tyLeChiem);
                series.Points.AddXY($"Trống ({tongPhong - phongDangChiem} phòng)", 100 - tyLeChiem);

                series.Points[0].Color = System.Drawing.Color.ForestGreen;
                series.Points[1].Color = System.Drawing.Color.IndianRed;

                chartChiemCho.Series.Add(series);
                chartChiemCho.Legends[0].Docking = Docking.Bottom;

                // 4. THỐNG KÊ PHÒNG ĐẶT NHIỀU / ÍT NHẤT
                var thongKePhong = db.DatPhong_ChiTiet
                    .Where(ct => ct.Ngay >= tuNgay && ct.Ngay <= denNgay
                              && ct.DatPhong.Status == true && ct.DatPhong.Disabled != true)
                    .GroupBy(ct => ct.IdPhong)
                    .Select(g => new
                    {
                        IdPhong = g.Key,
                        SoLanDat = g.Count()
                    })
                    .ToList();

                if (thongKePhong.Any())
                {
                    var phongNhieuNhat = thongKePhong
                        .OrderByDescending(x => x.SoLanDat)
                        .First();

                    var phongItNhat = thongKePhong
                        .OrderBy(x => x.SoLanDat)
                        .First();

                    var pNhieu = db.Phong.First(p => p.IdPhong == phongNhieuNhat.IdPhong);
                    var pIt = db.Phong.First(p => p.IdPhong == phongItNhat.IdPhong);

                    lblPhongNhieuNhat.Text =
                        $"🏆 Phòng đặt nhiều nhất: {pNhieu.TenPhong}\n\n" +
                        $"Số lần đặt: {phongNhieuNhat.SoLanDat}";

                    lblPhongItNhat.Text =
                        $"📉 Phòng đặt ít nhất: {pIt.TenPhong}\n\n" +
                        $"Số lần đặt: {phongItNhat.SoLanDat}";
                }
                else
                {
                    lblPhongNhieuNhat.Text = "Không có dữ liệu";
                    lblPhongItNhat.Text = "Không có dữ liệu";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo chiếm chỗ: " + ex.Message);
            }
        }

        private void lblPhongItNhat_Click(object sender, EventArgs e)
        {

        }

        private void lblPhongNhieuNhat_Click(object sender, EventArgs e)
        {

        }

        private void tabTiLeDatPhong_Click(object sender, EventArgs e)
        {
            LoadChiemCho();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
