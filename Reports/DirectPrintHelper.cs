using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;

namespace THUEPHONG.Reports
{
    public class DirectPrintHelper
    {
        private int _idDatPhong;
        private DatPhong _datPhong;
        private KhachHang _khachHang;
        private Param _param;
        private decimal _tongTienPhong;
        private decimal _tongTienDV;
        private decimal _tongCong;

        private Font fontTitle = new Font("Arial", 16, FontStyle.Bold);
        private Font fontHeader = new Font("Arial", 12, FontStyle.Bold);
        private Font fontNormal = new Font("Arial", 10, FontStyle.Regular);
        private Font fontBold = new Font("Arial", 10, FontStyle.Bold);
        private Font fontSmall = new Font("Arial", 9, FontStyle.Regular);

        private Brush brushBlack = Brushes.Black;
        private Pen penBlack = new Pen(Color.Black, 1);

        private int currentY = 0;
        private const int marginLeft = 50;
        private const int marginRight = 50;
        private const int lineHeight = 25;

        public static void Print(int idDatPhong)
        {
            DirectPrintHelper printer = new DirectPrintHelper();
            printer._idDatPhong = idDatPhong;
            printer.LoadData();
            printer.ShowPrintPreview();
        }

        private void LoadData()
        {
            DATPHONG _datphongBL = new DATPHONG();
            KHACHHANG _khBL = new KHACHHANG();
            SYSPARAM _sysparamBL = new SYSPARAM();
            DATPHONG_CHITIET _datphong_chitiet = new DATPHONG_CHITIET();
            DATPHONG_SP _datphong_sp = new DATPHONG_SP();

            _datPhong = _datphongBL.getItem(_idDatPhong);
            if (_datPhong == null)
                throw new Exception("Không tìm thấy thông tin đặt phòng!");

            _khachHang = _khBL.getItem(_datPhong.IdKH );
            _param = _sysparamBL.GetParam();

            // Tính tổng tiền
            var chiTietList = _datphong_chitiet.GetAllByDatPhong(_idDatPhong);
            _tongTienPhong = chiTietList.Sum(x => x.ThanhTien ?? 0);

            var sanPhamList = _datphong_sp.GetAll()
                .Where(x => x.IdDatPhong == _idDatPhong)
                .ToList();
            _tongTienDV = sanPhamList.Sum(x => x.ThanhTien ?? 0);

            _tongCong = _tongTienPhong + _tongTienDV;
        }

        private void ShowPrintPreview()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);

            PrintPreviewDialog ppd = new PrintPreviewDialog();
            ppd.Document = pd;
            ppd.Width = 900;
            ppd.Height = 700;
            ppd.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            currentY = 50;

            // ===== HEADER - Thông tin khách sạn =====
            DrawHeader(g, e.PageBounds.Width);

            // ===== Tiêu đề phiếu =====
            DrawTitle(g, e.PageBounds.Width);

            // ===== Thông tin khách hàng =====
            DrawCustomerInfo(g, e.PageBounds.Width);

            currentY += 10;

            // ===== Bảng chi tiết phòng =====
            DrawRoomDetails(g, e.PageBounds.Width);

            currentY += 10;

            // ===== Bảng sản phẩm - dịch vụ =====
            DrawServiceDetails(g, e.PageBounds.Width);

            currentY += 10;

            // ===== Tổng tiền =====
            DrawTotal(g, e.PageBounds.Width);

            currentY += 20;

            // ===== Chữ ký =====
            DrawSignature(g, e.PageBounds.Width);
        }


        private void DrawHeader(Graphics g, int pageWidth)
        {
            // Lấy thông tin từ CongTy thay vì Param
            CONGTY _congtyBL = new CONGTY();
            var congTy = _congtyBL.getItem(_param?.MaCTY);

            string tenKS = congTy?.TenCTY ?? _param?.TenCTY ?? "KHÁCH SẠN";
            string diaChi = congTy?.DiaChi ?? "";
            string dienThoai = congTy?.DienThoai ?? "";

            g.DrawString(tenKS, fontHeader, brushBlack, marginLeft, currentY);
            currentY += lineHeight;

            if (!string.IsNullOrEmpty(diaChi))
            {
                g.DrawString("Địa chỉ: " + diaChi, fontSmall, brushBlack, marginLeft, currentY);
                currentY += 20;
            }

            if (!string.IsNullOrEmpty(dienThoai))
            {
                g.DrawString("Điện thoại: " + dienThoai, fontSmall, brushBlack, marginLeft, currentY);
                currentY += 20;
            }

            currentY += 10;
        }

        private void DrawTitle(Graphics g, int pageWidth)
        {
        //    string title = "PHIẾU ĐẶT PHÒNG";
            string title = _isHoaDon ? "HÓA ĐƠN THANH TOÁN" : "PHIẾU ĐẶT PHÒNG";
            SizeF titleSize = g.MeasureString(title, fontTitle);
            float titleX = (pageWidth - titleSize.Width) / 2;

            g.DrawString(title, fontTitle, brushBlack, titleX, currentY);
            currentY += lineHeight + 10;

            string soPhieu = "Số: DP" + _idDatPhong.ToString("D6");
            string ngayLap = "Ngày: " + DateTime.Now.ToString("dd/MM/yyyy");

            SizeF soPhieuSize = g.MeasureString(soPhieu, fontNormal);
            float soPhieuX = (pageWidth - soPhieuSize.Width) / 2;

            g.DrawString(soPhieu, fontNormal, brushBlack, soPhieuX, currentY);     
            currentY += lineHeight;

            SizeF ngaySize = g.MeasureString(ngayLap, fontNormal);
            float ngayX = (pageWidth - ngaySize.Width) / 2;

            g.DrawString(ngayLap, fontNormal, brushBlack, ngayX, currentY);
            currentY += 30;
        }

        private void DrawCustomerInfo(Graphics g, int pageWidth)
        {
            g.DrawString("Họ tên khách hàng: " + (_khachHang?.HoTen ?? ""), fontNormal, brushBlack, marginLeft, currentY);
            currentY += lineHeight;

            g.DrawString("Điện thoại: " + (_khachHang?.DienThoai ?? ""), fontNormal, brushBlack, marginLeft, currentY);
            g.DrawString("CCCD: " + (_khachHang?.CCCD ?? ""), fontNormal, brushBlack, 400, currentY);
            currentY += lineHeight;

            g.DrawString("Địa chỉ: " + (_khachHang?.DiaChi ?? ""), fontNormal, brushBlack, marginLeft, currentY);
            currentY += lineHeight;

            g.DrawString("Ngày đặt: " + _datPhong.NgayDatPhong?.ToString("dd/MM/yyyy"), fontNormal, brushBlack, marginLeft, currentY);
            g.DrawString("Ngày trả: " + _datPhong.NgayTraPhong?.ToString("dd/MM/yyyy"), fontNormal, brushBlack, 400, currentY);
            currentY += lineHeight;

            g.DrawString("Số người: " + _datPhong.SoNguoiO, fontNormal, brushBlack, marginLeft, currentY);
            currentY += lineHeight;

            if (!string.IsNullOrEmpty(_datPhong.GhiChu))
            {
                g.DrawString("Ghi chú: " + _datPhong.GhiChu, fontNormal, brushBlack, marginLeft, currentY);
                currentY += lineHeight;
            }
        }

        private void DrawRoomDetails(Graphics g, int pageWidth)
        {
            DATPHONG_CHITIET _datphong_chitiet = new DATPHONG_CHITIET();
            PHONG _phongBL = new PHONG();
            LOAIPHONG _loaiphongBL = new LOAIPHONG();

            var chiTietList = _datphong_chitiet.GetAllByDatPhong(_idDatPhong);
            if (chiTietList == null || chiTietList.Count == 0)
                return;

            // Tiêu đề bảng
            g.DrawString("CHI TIẾT PHÒNG:", fontBold, brushBlack, marginLeft, currentY);
            currentY += lineHeight;

            // Header bảng
            int col1 = marginLeft;
            int col2 = marginLeft + 50;
            int col3 = marginLeft + 200;
            int col4 = marginLeft + 400;
            int col5 = marginLeft + 500;
            int col6 = marginLeft + 600;

            // Vẽ header
            g.DrawRectangle(penBlack, col1, currentY, pageWidth - marginLeft - marginRight, 25);
            g.DrawString("STT", fontBold, brushBlack, col1 + 5, currentY + 5);
            g.DrawString("Tên phòng", fontBold, brushBlack, col2 + 5, currentY + 5);
            g.DrawString("Loại phòng", fontBold, brushBlack, col3 + 5, currentY + 5);
            g.DrawString("Số ngày", fontBold, brushBlack, col4 + 5, currentY + 5);
            g.DrawString("Đơn giá", fontBold, brushBlack, col5 + 5, currentY + 5);
            g.DrawString("Thành tiền", fontBold, brushBlack, col6 + 5, currentY + 5);
            currentY += 25;

            // Vẽ chi tiết
            int stt = 1;
            foreach (var ct in chiTietList)
            {
                var phong = _phongBL.getItem(ct.IdPhong );
                var loaiPhong = _loaiphongBL.getItem(phong?.IdLoaiPhong ?? 0);

                g.DrawRectangle(penBlack, col1, currentY, pageWidth - marginLeft - marginRight, 25);
                g.DrawString(stt.ToString(), fontNormal, brushBlack, col1 + 5, currentY + 5);
                g.DrawString(phong?.TenPhong ?? "", fontNormal, brushBlack, col2 + 5, currentY + 5);
                g.DrawString(loaiPhong?.TenLoaiPhong ?? "", fontNormal, brushBlack, col3 + 5, currentY + 5);
                g.DrawString((ct.SoNgayO ?? 0).ToString(), fontNormal, brushBlack, col4 + 5, currentY + 5);
                //g.DrawString((ct.DonGia ?? 0).ToString("#,##0"), fontNormal, brushBlack, col5 + 5, currentY + 5);
              //  g.DrawString((ct.ThanhTien ?? 0).ToString("#,##0"), fontNormal, brushBlack, col6 + 5, currentY + 5);

                g.DrawString(myFunctions.NormalizeMoney(ct.DonGia).ToString("N0")
                , fontNormal, brushBlack, col5 + 5, currentY + 5);
                g.DrawString(myFunctions.NormalizeMoney(ct.ThanhTien).ToString("N0")
                    , fontNormal, brushBlack, col6 + 5, currentY + 5);

                currentY += 25;
                stt++;

            }

            //    phòng
            g.DrawString("Tổng tiền phòng:     ", fontBold, brushBlack, col5 -10, currentY +5);
            g.DrawString(myFunctions.NormalizeMoney(_tongTienPhong).ToString("N0"), fontBold, brushBlack, col6 + 15, currentY + 5);
            currentY += 25;
        }

        private void DrawServiceDetails(Graphics g, int pageWidth)
        {
            DATPHONG_SP _datphong_sp = new DATPHONG_SP();
            PHONG _phongBL = new PHONG();
            SANPHAM _spBL = new SANPHAM();

            var sanPhamList = _datphong_sp.GetAll()
                .Where(x => x.IdDatPhong == _idDatPhong && x.IdSP != null)
                .ToList();

            if (sanPhamList == null || sanPhamList.Count == 0)
                return;

            // Tiêu đề
            g.DrawString("SẢN PHẨM - DỊCH VỤ:", fontBold, brushBlack, marginLeft, currentY);
            currentY += lineHeight;

            // Header
            int col1 = marginLeft;
            int col2 = marginLeft + 50;
            int col3 = marginLeft + 200;
            int col4 = marginLeft + 400;
            int col5 = marginLeft + 500;
            int col6 = marginLeft + 600;

            g.DrawRectangle(penBlack, col1, currentY, pageWidth - marginLeft - marginRight, 25);
            g.DrawString("STT", fontBold, brushBlack, col1 + 5, currentY + 5);
            g.DrawString("Phòng", fontBold, brushBlack, col2 + 5, currentY + 5);
            g.DrawString("Tên SP/DV", fontBold, brushBlack, col3 + 5, currentY + 5);
            g.DrawString("Số lượng", fontBold, brushBlack, col4 + 5, currentY + 5);
            g.DrawString("Đơn giá", fontBold, brushBlack, col5 + 5, currentY + 5);
            g.DrawString("Thành tiền", fontBold, brushBlack, col6 + 5, currentY + 5);
            currentY += 25;

            int stt = 1;
            foreach (var item in sanPhamList)
            {
                var phong = _phongBL.getItem(item.IdPhong ?? 0);
                var sp = _spBL.getItem(item.IdSP.Value);

                g.DrawRectangle(penBlack, col1, currentY, pageWidth - marginLeft - marginRight, 25);
                g.DrawString(stt.ToString(), fontNormal, brushBlack, col1 + 5, currentY + 5);
                g.DrawString(phong?.TenPhong ?? "", fontNormal, brushBlack, col2 + 5, currentY + 5);
                g.DrawString(sp?.TenSP ?? "", fontNormal, brushBlack, col3 + 5, currentY + 5);
                g.DrawString((item.SoLuong ?? 0).ToString(), fontNormal, brushBlack, col4 + 5, currentY + 5);
                g.DrawString(myFunctions.NormalizeMoney(item.DonGia).ToString("N0"), fontNormal, brushBlack, col5 + 5, currentY + 5);
                g.DrawString(myFunctions.NormalizeMoney(item.ThanhTien).ToString("N0"), fontNormal, brushBlack, col6 + 5, currentY + 5);

                currentY += 25;
                stt++;
            }

            // Tổng tiền DV
            g.DrawString("Tổng tiền dịch vụ:      ", fontBold, brushBlack, col5 - 10, currentY + 5);
            g.DrawString(myFunctions.NormalizeMoney(_tongTienDV).ToString("N0"), fontBold, brushBlack, col6 + 15, currentY + 5);
            currentY += 25;
        }

        private void DrawTotal(Graphics g, int pageWidth)
        {
            g.DrawString("TỔNG CỘNG: " + myFunctions.NormalizeMoney(_tongCong).ToString("N0") + " VNĐ", fontHeader, brushBlack, marginLeft, currentY);
            currentY += lineHeight + 5;

            string bangChu = "Bằng chữ: " + myFunctions.ChuyenSoThanhChu(_tongCong);
            g.DrawString(bangChu, fontBold, brushBlack, marginLeft, currentY);
            currentY += lineHeight;
        }

        private void DrawSignature(Graphics g, int pageWidth)
        {
            int col1 = marginLeft + 100;
            int col2 = pageWidth - marginRight - 200;

            g.DrawString("Khách hàng", fontBold, brushBlack, col1, currentY);
            g.DrawString("Nhân viên lập phiếu", fontBold, brushBlack, col2, currentY);
            currentY += lineHeight;

            g.DrawString("(Ký, ghi rõ họ tên)", fontSmall, brushBlack, col1, currentY);
            g.DrawString("(Ký, ghi rõ họ tên)", fontSmall, brushBlack, col2, currentY);
        }

        private bool _isHoaDon = false;

        public static void PrintPhieuDatPhong(int idDatPhong)
        {
            DirectPrintHelper printer = new DirectPrintHelper();
            printer._idDatPhong = idDatPhong;
            printer._isHoaDon = false;
            printer.LoadData();
            printer.ShowPrintPreview();
        }
        public static void PrintHoaDon(int idDatPhong)
        {
            DirectPrintHelper printer = new DirectPrintHelper();
            printer._idDatPhong = idDatPhong;
            printer._isHoaDon = true;
            printer.LoadData();
            printer.ShowPrintPreview();
        }

    }
}