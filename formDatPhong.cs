using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using DevExpress.Data;
using DevExpress.Data.Mask.Internal;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraReports.ParameterPanel;
using THUEPHONG.Reports;
namespace THUEPHONG
{
    public partial class formDatPhong : DevExpress.XtraEditors.XtraForm
    {
        public formDatPhong()
        {
            InitializeComponent();
            DataTable tb = myFunctions.laydulieu("SELECT  A.IdPhong, A.TenPhong, C.DonGia,  B.TenTang FROM Phong A, Tang B, LoaiPhong C WHERE A.IDTANG = B.IDTANG AND A.IDLOAIPHONG = C.IDLOAIPHONG AND A.TrangThai = 0 AND A.IdLoaiPhong=C.IdLoaiPhong");

            gcPhong.DataSource = tb;          // Grid 1
            gcDatPhong.DataSource = tb.Clone();  // Grid 2: clone dữ liệu rỗng nhưng cùng schema


            FormatGridView(gvPhong);       // áp dụng format cho Grid 1
            FormatGridView(gvDatPhong);    // áp dụng format cho Grid 2

            gvSPDV.CellValueChanging += gvSPDV_CellValueChanging;
        }
        string _maCTY;
        string _maDVI;
        int _idPhong = 0;
        int _idDatPhong = 0;
        string _tenPhong;
        List<OBJ_DPSP> lstDPSP;

        SYSPARAM _sysparam;
        DATPHONG _datphong;
        PHONG _phong;
        TANG _tang;
        KHACHHANG _KH;
        SANPHAM _SP;
        LOAIPHONG _loaiphong;
        DATPHONG_SP _datphong_sp;
        DATPHONG_CHITIET _datphong_chitiet;


        GridHitInfo downHitInfor = null;
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        bool _them;

        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
            btnIn.Visible = t;
        }
        void _enabled(bool t)
        {
            cboKhachHang.Enabled = t;
            btnAddNew.Enabled = t;
            dtNgayDat.Enabled = t;
            dtNgayTra.Enabled = t;
            cboTrangThai.Enabled = t;
            spSoNguoiO.Enabled = t;
            txtGhiChu.Enabled = t;
        }

        void _reset()
        {
            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);
            spSoNguoiO.Text = "1";
            cboTrangThai.SelectedValue = false;
            txtGhiChu.Text = "";
        }
        RepositoryItemButtonEdit btnThanhToan;

        void loadData()
        {
            // Lấy giá trị bộ lọc từ các control trên form (giả sử tên control là dtTuNgay, dtDenNgay, cboMaCTY, cboMaDVI)
            DateTime tuNgay = dtTuNgay.Value.Date;
            DateTime denNgay = dtDenNgay.Value.Date;
            string trangThaiTT = cboTrangThaiThanhToan.SelectedValue?.ToString();
            string tenPhong = cboTenPhong.SelectedValue?.ToString();
            string keyword = txtTimKiem.Text.Trim();

            // Lấy dữ liệu từ getAll với bộ lọc
            var gridData = _datphong.getAll(tuNgay,
            denNgay,
            _maCTY,
            _maDVI,
            trangThaiTT,
            tenPhong,
            keyword)
                            .Select(d => new
                            {
                                IdDatPhong = d.IDDP,
                                IdKH = d.IDKH,
                                TenKH = d.HOTEN, // Sử dụng HOTEN đã join sẵn từ getAll
                                NgayDatPhong = d.NGAYDATPHONG,
                                NgayTraPhong = d.NGAYTRAPHONG,
                                SoNguoiO = d.SONGUOIO,
                                Disabled = d.DISABLED,
                                MaCTY = d.MACTY,
                                Status = d.STATUS,
                                MaDVI = d.MADVI,
                                TheoDoan = d.THEODOAN,
                                GhiChu = d.GHICHU,
                                SoTien = d.SOTIEN,
                                ThanhToan = ""
                            }).ToList();

            // Gán DataSource
            gcDanhSach.DataSource = gridData;

            // Populate columns để GridView nhận các cột
            gvDanhSach.PopulateColumns();
            btnThanhToan = new RepositoryItemButtonEdit();
            btnThanhToan.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;

            btnThanhToan.Buttons[0].Caption = "Thanh toán";
            btnThanhToan.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;

            btnThanhToan.ButtonClick += BtnThanhToan_Click;

            gcDanhSach.RepositoryItems.Add(btnThanhToan);

            gvDanhSach.Columns["ThanhToan"].VisibleIndex = 13;
            gvDanhSach.Columns["ThanhToan"].Width = 120;
            gvDanhSach.Columns["ThanhToan"].Caption = "THANH TOÁN";
            //gvDanhSach.Columns["ThanhToan"].ColumnEdit = btnThanhToan;



            gvDanhSach.Appearance.HeaderPanel.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            gvDanhSach.Appearance.HeaderPanel.Options.UseFont = true;
            gvDanhSach.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            gvDanhSach.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gvDanhSach.Appearance.HeaderPanel.ForeColor = Color.Black;
            gvDanhSach.Appearance.HeaderPanel.Options.UseForeColor = true;
            gvDanhSach.OptionsView.ColumnAutoWidth = false;
            gvDanhSach.Columns["Disabled"].VisibleIndex = 0;
            gvDanhSach.Columns["Disabled"].Width = 50;
            gvDanhSach.Columns["Disabled"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["Disabled"].Caption = "DEL";
            gvDanhSach.Columns["IdDatPhong"].VisibleIndex = 1;
            gvDanhSach.Columns["IdDatPhong"].Width = 100;
            gvDanhSach.Columns["IdDatPhong"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["IdDatPhong"].Caption = "MÃ ĐẶT PHÒNG";
            gvDanhSach.Columns["NgayDatPhong"].VisibleIndex = 2;
            gvDanhSach.Columns["NgayDatPhong"].Width = 150;
            gvDanhSach.Columns["NgayDatPhong"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["NgayDatPhong"].Caption = "NGÀY ĐẶT";
            gvDanhSach.Columns["NgayTraPhong"].VisibleIndex = 3;
            gvDanhSach.Columns["NgayTraPhong"].Width = 150;
            gvDanhSach.Columns["NgayTraPhong"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["NgayTraPhong"].Caption = "NGÀY TRẢ";
            gvDanhSach.Columns["SoTien"].VisibleIndex = 4;
            gvDanhSach.Columns["SoTien"].Width = 100;
            gvDanhSach.Columns["SoTien"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["SoTien"].Caption = "SỐ TIỀN";
            gvDanhSach.Columns["SoNguoiO"].VisibleIndex = 5;
            gvDanhSach.Columns["SoNguoiO"].Width = 100;
            gvDanhSach.Columns["SoNguoiO"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["SoNguoiO"].Caption = "SỐ NGƯỜI Ở";
            gvDanhSach.Columns["MaCTY"].VisibleIndex = 6;
            gvDanhSach.Columns["MaCTY"].Width = 100;
            gvDanhSach.Columns["MaCTY"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["MaCTY"].Caption = "MÃ CÔNG TY";
            gvDanhSach.Columns["MaDVI"].VisibleIndex = 7;
            gvDanhSach.Columns["MaDVI"].Width = 100;
            gvDanhSach.Columns["MaDVI"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["MaDVI"].Caption = "MÃ ĐƠN VỊ";
            gvDanhSach.Columns["Status"].VisibleIndex = 8;
            gvDanhSach.Columns["Status"].Width = 100;
            gvDanhSach.Columns["Status"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["Status"].Caption = "TRẠNG THÁI";
            gvDanhSach.Columns["TheoDoan"].VisibleIndex = 9;
            gvDanhSach.Columns["TheoDoan"].Width = 100;
            gvDanhSach.Columns["TheoDoan"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["TheoDoan"].Caption = "ĐOÀN";
            gvDanhSach.Columns["IdKH"].VisibleIndex = 10;
            gvDanhSach.Columns["IdKH"].Width = 100;
            gvDanhSach.Columns["IdKH"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["IdKH"].Caption = "MÃ KHÁCH HÀNG";
            gvDanhSach.Columns["TenKH"].VisibleIndex = 11;
            gvDanhSach.Columns["TenKH"].Caption = "TÊN KHÁCH HÀNG";
            gvDanhSach.Columns["IdKH"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["TenKH"].Width = 150;
            gvDanhSach.Columns["GhiChu"].VisibleIndex = 12;
            gvDanhSach.Columns["GhiChu"].Width = 150;
            gvDanhSach.Columns["GhiChu"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["GhiChu"].Caption = "GHI CHÚ";


            gvDanhSach.OptionsBehavior.Editable = true;  // Bật edit cho grid để nút clickable

            // Lock tất cả cột trừ "ThanhToan"
            foreach (GridColumn col in gvDanhSach.Columns)
            {
                if (col.FieldName != "ThanhToan")
                {
                    col.OptionsColumn.AllowEdit = false;
                    col.OptionsColumn.ReadOnly = true;
                }
                else
                {
                    col.OptionsColumn.AllowEdit = true;  // Cho phép edit cho cột nút (để clickable)
                    col.OptionsColumn.ReadOnly = false;
                }
            }
            gvDanhSach.RefreshData();
        }

        void FormatGridView(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            gv.PopulateColumns();
            gv.Appearance.HeaderPanel.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            gv.Appearance.HeaderPanel.Options.UseFont = true;
            gv.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            gv.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gv.Appearance.HeaderPanel.ForeColor = Color.Black;
            gv.Appearance.HeaderPanel.Options.UseForeColor = true;
            gv.OptionsView.ColumnAutoWidth = false;

            gv.OptionsBehavior.Editable = false;

            if (gv.Columns["TenPhong"] != null)
            {
                gv.Columns["TenPhong"].VisibleIndex = 0;
                gv.Columns["TenPhong"].Width = 150;
                gv.Columns["TenPhong"].OptionsColumn.FixedWidth = true;
                gv.Columns["TenPhong"].Caption = "TÊN PHÒNG";
            }

            if (gv.Columns["TenTang"] != null)
            {
                gv.Columns["TenTang"].VisibleIndex = 1;
                gv.Columns["TenTang"].Width = 120;
                gv.Columns["TenTang"].OptionsColumn.FixedWidth = true;
                gv.Columns["TenTang"].Caption = "TẦNG";
            }

            if (gv.Columns["DonGia"] != null)
            {
                gv.Columns["DonGia"].VisibleIndex = 2;
                gv.Columns["DonGia"].Width = 150;
                gv.Columns["DonGia"].OptionsColumn.FixedWidth = true;
                gv.Columns["DonGia"].Caption = "ĐƠN GIÁ";
            }
            if (gv.Columns["IdPhong"] == null)
            {
                gv.Columns["IdPhong"].Visible = false; // ẩn cột khỏi GridView
            }

        }
        void loadData_SanPham()
        {
            var data = _SP.getAll();

            // 2. Nếu data là List<T>, loại bỏ property Users bằng Select
            var gridData = data.Select(x => new
            {
                x.TenSP,
                x.DonGia,
                x.IdSP,
            })
            .ToList();

            // 3. Gán DataSource
            gcSanPham.DataSource = gridData;

            // 4. Populate columns để GridView nhận các cột
            gvSanPham.PopulateColumns();


            // Reset về mặc định
            gvSanPham.Appearance.HeaderPanel.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            gvSanPham.Appearance.HeaderPanel.Options.UseFont = true;
            gvSanPham.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
            gvSanPham.Appearance.HeaderPanel.Options.UseTextOptions = true;
            gvSanPham.Appearance.HeaderPanel.ForeColor = Color.Black;
            gvSanPham.Appearance.HeaderPanel.Options.UseForeColor = true;
            gvSanPham.OptionsView.ColumnAutoWidth = false;

            gvSanPham.Columns["TenSP"].VisibleIndex = 1;
            gvSanPham.Columns["TenSP"].Width = 150;
            gvSanPham.Columns["TenSP"].OptionsColumn.FixedWidth = true;
            gvSanPham.Columns["TenSP"].Caption = "TÊN SẢN PHẨM";


            gvSanPham.Columns["DonGia"].Width = 100;
            gvSanPham.Columns["DonGia"].OptionsColumn.FixedWidth = true;
            gvSanPham.Columns["DonGia"].Caption = "ĐƠN GIÁ";
            gvSanPham.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvSanPham.Columns["DonGia"].DisplayFormat.FormatString = "0.000";

            gvSanPham.Columns["IdSP"].Visible = false; // ẩn cột khỏi GridView



            gvSanPham.OptionsBehavior.Editable = false;
        }

        void loadDP()
        {
            gcDatPhong.DataSource = myFunctions.laydulieu("SELECT  A.IdPhong, A.TenPhong, C.DonGia,  B.TenTang FROM Phong A, Tang B, LoaiPhong C, DatPhong_ChiTiet D WHERE" +
                " A.IDTANG = B.IDTANG AND A.IDLOAIPHONG = C.IDLOAIPHONG  AND A.IdPhong=D.IdPhong AND D.IdDatPhong = '" + _idDatPhong + "'");
        }

        void loadDPSP()
        {
            lstDPSP = _datphong_sp.getAllByDatPhong(_idDatPhong);
            gcSPDV.DataSource = lstDPSP;
            CreateSPDVColumns();
        }



        public void loadData_KhachHang()

        {
            _KH = new KHACHHANG();
            var data = _KH.getAll();
            cboKhachHang.DataSource = data;
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "IdKH";
        }

        void CreateSPDVColumns()
        {
            gvSPDV.Columns.Clear();

            gvSPDV.OptionsView.ColumnAutoWidth = false;
            gvSPDV.Appearance.HeaderPanel.Font = new Font("Tahoma", 8, FontStyle.Bold);
            gvSPDV.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gvSPDV.Appearance.HeaderPanel.ForeColor = Color.Black;

            gvSPDV.Columns.AddVisible("TenPhong", "PHÒNG").Width = 120;
            gvSPDV.Columns.AddVisible("TenSP", "TÊN SẢN PHẨM").Width = 180;
            gvSPDV.Columns.AddVisible("SoLuong", "SỐ LƯỢNG").Width = 150;
            gvSPDV.Columns.AddVisible("DonGia", "ĐƠN GIÁ").Width = 120;
            gvSPDV.Columns.AddVisible("ThanhTien", "THÀNH TIỀN").Width = 120;

            gvSPDV.OptionsView.ShowFooter = true;

            GridColumn colSL = gvSPDV.Columns["SoLuong"];

            colSL.Summary.Clear();
            colSL.Summary.Add(new DevExpress.XtraGrid.GridColumnSummaryItem(
                DevExpress.Data.SummaryItemType.Sum,
                "SoLuong",
                "{0:n0}"

            ));

            GridColumn colTT = gvSPDV.Columns["ThanhTien"];

            colTT.Summary.Clear();
            colTT.Summary.Add(new DevExpress.XtraGrid.GridColumnSummaryItem(
                DevExpress.Data.SummaryItemType.Sum,
                "ThanhTien",
                "{0:#,##0.000}"
            ));

            gvSPDV.OptionsBehavior.Editable = true; // Cho phép edit toàn grid
            gvSPDV.OptionsBehavior.ReadOnly = false;

            // Khóa các cột khác
            gvSPDV.Columns["TenPhong"].OptionsColumn.AllowEdit = false;
            gvSPDV.Columns["TenSP"].OptionsColumn.AllowEdit = false;
            gvSPDV.Columns["DonGia"].OptionsColumn.AllowEdit = false;
            gvSPDV.Columns["ThanhTien"].OptionsColumn.AllowEdit = false;

            // Chỉ cho SỐ LƯỢNG được sửa
            gvSPDV.Columns["SoLuong"].OptionsColumn.AllowEdit = true;
            gvSPDV.Columns["SoLuong"].OptionsColumn.ReadOnly = false;

            // Cột ẩn để lưu IdPhong và IdSP
            GridColumn colIdPhong = gvSPDV.Columns.AddField("IdPhong");
            colIdPhong.Visible = false;

            GridColumn colIdSP = gvSPDV.Columns.AddField("IdSP");
            colIdSP.Visible = false;



            // Cố định width
            foreach (GridColumn col in gvSPDV.Columns)
                col.OptionsColumn.FixedWidth = true;

            // ===== CỘT NÚT XOÁ =====
            RepositoryItemButtonEdit btnXoa = new RepositoryItemButtonEdit();
            btnXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;

            btnXoa.Buttons.Clear();
            btnXoa.Buttons.Add(new DevExpress.XtraEditors.Controls.EditorButton(
                DevExpress.XtraEditors.Controls.ButtonPredefines.Delete
            ));

            btnXoa.ButtonClick += BtnXoa_ButtonClick;

            // Thêm repository vào grid
            gvSPDV.GridControl.RepositoryItems.Add(btnXoa);

            // Tạo cột
            GridColumn colXoa = gvSPDV.Columns.AddField("Xoa");
            colXoa.Caption = "XOÁ";
            colXoa.ColumnEdit = btnXoa;
            colXoa.Visible = true;
            colXoa.VisibleIndex = gvSPDV.Columns.Count;
            colXoa.Width = 40;
            colXoa.OptionsColumn.FixedWidth = true;
            colXoa.OptionsColumn.AllowEdit = true;

        }

        private void BtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var view = gvSPDV;

            int rowHandle = view.FocusedRowHandle;
            if (rowHandle < 0) return;

            // Xoá dòng trên Grid
            view.DeleteRow(rowHandle);

        }


        private void formDatPhong_Load(object sender, EventArgs e)
        {
            _sysparam = new SYSPARAM();
            _datphong = new DATPHONG();
            _phong = new PHONG();
            _tang = new TANG();
            _KH = new KHACHHANG();
            _SP = new SANPHAM();
            _loaiphong = new LOAIPHONG();
            _datphong_sp = new DATPHONG_SP();
            _datphong_chitiet = new DATPHONG_CHITIET();

            lstDPSP = new List<OBJ_DPSP>();

            dtTuNgay.Value = myFunctions.GetFirstDayInMonth(DateTime.Now.Year, DateTime.Now.Month);
            dtDenNgay.Value = DateTime.Now;

            var pr = _sysparam.GetParam();
            _maCTY = pr.MaCTY;
            _maDVI = pr.MaDVI;

            // phần này là tạo summary cho tổng tiền phòng và tổng số phòng được đặt
            gvDatPhong.OptionsView.ShowFooter = true;

            GridColumn colDP = gvDatPhong.Columns["DonGia"];
            if (colDP != null)
            {
                colDP.Summary.Clear();
                colDP.Summary.Add(new GridColumnSummaryItem(
                    SummaryItemType.Sum,
                    "DonGia",
                    "{0:#,##0.000}"
                ));
            }

            GridColumn colTP = gvDatPhong.Columns["TenPhong"];
            if (colTP != null)
            {
                colTP.Summary.Clear();
                colTP.Summary.Add(new GridColumnSummaryItem(
                    SummaryItemType.Count,
                    "DonGia",
                    "{0:n0}"
                ));
            }

            loadData();
            loadData_SanPham();

            // load bảng sản phẩm - dịch vụ
            lstDPSP = new List<OBJ_DPSP>();

            gcSPDV.DataSource = lstDPSP;
            CreateSPDVColumns();


            // load phần tên khách hàng, trạng thái...
            loadData_KhachHang();
            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.ValueMember = "_value";

            showHideControl(true);
            gvDanhSach.OptionsView.ShowGroupPanel = false;
            _enabled(true);

            gvPhong.Columns["TenTang"].GroupIndex = 0;
            gvPhong.ExpandAllGroups();

            //gvDatPhong.Columns["TenTang"].GroupIndex = 0;
            //gvDatPhong.ExpandAllGroups();

            //gvSPDV.Columns["TenPhong"].GroupIndex = 0;
            //gvSPDV.ExpandAllGroups();

            xtraTabControl1.SelectedTabPage = pageDanhSach;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            xtraTabControl1.SelectedTabPage = pageChiTiet;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            showHideControl(false);
        }

        void saveData()
        {
            if (_them)
            {
                DatPhong dp = new DatPhong();
                DatPhong_ChiTiet dpct;
                DatPhong_SanPham dpsp;

                dp.NgayDatPhong = dtNgayDat.Value;
                dp.NgayTraPhong = dtNgayTra.Value;
                dp.SoNguoiO = int.Parse(spSoNguoiO.EditValue.ToString());
                dp.Status = bool.Parse(cboTrangThai.SelectedValue.ToString());
                //dp.TheoDoan = chkDoan.Checked;
                dp.IdKH = int.Parse(cboKhachHang.SelectedValue?.ToString());
                dp.Disabled = false;
                dp.SoTien = decimal.Parse(txtThanhTien.Text);
                dp.UId = 1;
                dp.GhiChu = txtGhiChu.Text;
                dp.MaCTY = _maCTY;
                dp.MaDVI = _maDVI;
                dp.PhuongThucThanhToan = cboPhuongThucThanhToan.Text;
                dp.Created_date = DateTime.Now;

                var _dp = _datphong.add(dp);
                _idDatPhong = _dp.IdDatPhong;
                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    dpct = new DatPhong_ChiTiet();
                    dpct.IdDatPhong = _dp.IdDatPhong;

                    var idPhong = gvDatPhong.GetRowCellValue(i, "IdPhong")?.ToString();
                    if (string.IsNullOrEmpty(idPhong))
                    {
                        // Xử lý lỗi: Ví dụ skip hàng này, hoặc throw exception
                        continue; // Hoặc MessageBox.Show($"Hàng {i} thiếu IdPhong!"); continue;
                    }
                    dpct.IdPhong = int.Parse(idPhong);
                    dpct.Ngay = DateTime.Now;
                    dpct.SoNgayO = (dtNgayTra.Value - dtNgayDat.Value).Days;

                    var donGiaValue = gvDatPhong.GetRowCellValue(i, "DonGia")?.ToString() ?? "0";
                    dpct.DonGia = decimal.Parse(donGiaValue);

                    dpct.ThanhTien = dpct.SoNgayO * dpct.DonGia;
                    var _dpct = _datphong_chitiet.add(dpct);
                    _phong.updateStatus(dpct.IdPhong, true); // Không cần Parse lại, vì dpct.IdPhong đã là int
                    MessageBox.Show(gvSPDV.GetRowCellValue(0, "IdPhong").ToString());
                    if (gvSPDV.RowCount > 0)
                    {
                        for (int j = 0; j < gvSPDV.RowCount; j++)
                        {
                            var idPhongValue = gvSPDV.GetRowCellValue(j, "IdPhong")?.ToString();
                            if (!string.IsNullOrEmpty(idPhongValue) && dpct.IdPhong == int.Parse(idPhongValue))
                            {
                                dpsp = new DatPhong_SanPham();
                                dpsp.IdDatPhong = _dp.IdDatPhong;
                                dpsp.IdDPCT = _dpct.IdDPCT; // Gán IdDPCT từ đối tượng dpct vừa thêm
                                dpsp.IdPhong = int.Parse(idPhongValue);  // Đã kiểm tra null
                                dpsp.IdSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IdSP")?.ToString() ?? "0");  // Thêm kiểm tra cho IdSP
                                dpsp.SoLuong = int.Parse(gvSPDV.GetRowCellValue(j, "SoLuong")?.ToString() ?? "0");
                                dpsp.DonGia = decimal.Parse(gvSPDV.GetRowCellValue(j, "DonGia")?.ToString() ?? "0");
                                dpsp.Ngay = DateTime.Now;
                                dpsp.ThanhTien = dpsp.SoLuong * dpsp.DonGia;
                                _datphong_sp.add(dpsp);
                            }
                        }
                    }

                }
            }
            else
            {
                // update
                DatPhong dp = _datphong.getItem(_idDatPhong);
                DatPhong_ChiTiet dpct;
                DatPhong_SanPham dpsp;

                dp.NgayDatPhong = dtNgayDat.Value;
                dp.NgayTraPhong = dtNgayTra.Value;
                dp.SoNguoiO = int.Parse(spSoNguoiO.EditValue.ToString());
                dp.Status = bool.Parse(cboTrangThai.SelectedValue.ToString());
                //dp.TheoDoan = chkDoan.Checked;
                dp.IdKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                dp.SoTien = decimal.Parse(txtThanhTien.Text);
                dp.GhiChu = txtGhiChu.Text;
                dp.UId = 1;
                dp.Update_by = 1;
                dp.Update_date = DateTime.Now;
                dp.PhuongThucThanhToan = cboPhuongThucThanhToan.Text;

                var _dp = _datphong.update(dp);
                _idDatPhong = _dp.IdDatPhong;
                _datphong_chitiet.deleteAll(_dp.IdDatPhong);
                _datphong_sp.deleteAll(_dp.IdDatPhong);

                for (int i = 0; i < gvDatPhong.RowCount; i++)
                {
                    dpct = new DatPhong_ChiTiet();
                    dpct.IdDatPhong = _dp.IdDatPhong;
                    dpct.IdPhong = int.Parse(gvDatPhong.GetRowCellValue(i, "IdPhong").ToString());
                    dpct.Ngay = DateTime.Now;
                    dpct.SoNgayO = (dtNgayTra.Value - dtNgayDat.Value).Days;
                    dpct.DonGia = decimal.Parse(gvDatPhong.GetRowCellValue(i, "DonGia").ToString());
                    dpct.ThanhTien = dpct.SoNgayO * dpct.DonGia;
                    var _dpct = _datphong_chitiet.add(dpct);
                    _phong.updateStatus(int.Parse(dpct.IdPhong.ToString()), true);
                    if (gvSPDV.RowCount > 0)
                    {
                        for (int j = 0; j < gvSPDV.RowCount; j++)
                        {
                            var idPhongValue = gvSPDV.GetRowCellValue(j, "IdPhong")?.ToString();
                            if (!string.IsNullOrEmpty(idPhongValue) && dpct.IdPhong == int.Parse(idPhongValue))
                            {
                                dpsp = new DatPhong_SanPham();
                                dpsp.IdDatPhong = _dp.IdDatPhong;
                                dpsp.IdDPCT = _dpct.IdDPCT; // Gán IdDPCT từ đối tượng dpct vừa thêm
                                dpsp.IdPhong = int.Parse(idPhongValue);  // Đã kiểm tra null
                                dpsp.IdSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IdSP")?.ToString() ?? "0");  // Thêm kiểm tra cho IdSP
                                dpsp.SoLuong = int.Parse(gvSPDV.GetRowCellValue(j, "SoLuong")?.ToString() ?? "0");
                                dpsp.Ngay = DateTime.Now;
                                dpsp.DonGia = decimal.Parse(gvSPDV.GetRowCellValue(j, "DonGia")?.ToString() ?? "0");
                                dpsp.ThanhTien = dpsp.SoLuong * dpsp.DonGia;
                                _datphong_sp.add(dpsp);
                            }
                        }
                    }
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chăn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _datphong.delete(_idDatPhong);
                var lstDPCT = _datphong_chitiet.GetAllByDatPhong(_idDatPhong);
                foreach (var item in lstDPCT)
                {
                    _phong.updateStatus(item.IdPhong, false);

                }
            }

            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            saveData();
            objMain.gControl.Gallery.Groups.Clear();
            objMain.showRoom();
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);


        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            xtraTabControl1.SelectedTabPage = pageChiTiet;
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void gvDatPhong_MouseDown(object sender, MouseEventArgs e)
        {
            if (gvDatPhong.GetFocusedRowCellValue("IdPhong") != null)
            {
                _idPhong = int.Parse(gvDatPhong.GetFocusedRowCellValue("IdPhong").ToString());
                _tenPhong = gvDatPhong.GetFocusedRowCellValue("TenPhong").ToString();
            }

            GridView view = sender as GridView;
            downHitInfor = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));

            if (Control.ModifierKeys != Keys.None) return;

            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
            {
                downHitInfor = hitInfo;
            }
        }

        private void gvDatPhong_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Button == MouseButtons.Left && downHitInfor != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfor.HitPoint.X - dragSize.Width / 2, downHitInfor.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    DataRow row = view.GetDataRow(downHitInfor.RowHandle);
                    view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                    downHitInfor = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void gvPhong_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfor = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));

            if (Control.ModifierKeys != Keys.None) return;

            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
            {
                downHitInfor = hitInfo;
            }
        }

        private void gvPhong_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            if (e.Button == MouseButtons.Left && downHitInfor != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfor.HitPoint.X - dragSize.Width / 2, downHitInfor.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    DataRow row = view.GetDataRow(downHitInfor.RowHandle);
                    view.GridControl.DoDragDrop(row, DragDropEffects.Move);
                    downHitInfor = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }

        private void gcDatPhong_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            DataTable table = grid.DataSource as DataTable;
            DataRow row = e.Data.GetData(typeof(DataRow)) as DataRow;
            if (row != null && table != null && row.Table != table)
            {
                table.ImportRow(row);
                row.Table.Rows.Remove(row);

                // Refresh grid sau drop
                gvDatPhong.RefreshData();

                // Tự động focus và select hàng mới thêm (hàng cuối)
                int newRowHandle = gvDatPhong.RowCount - 1;
                gvDatPhong.FocusedRowHandle = newRowHandle;
                gvDatPhong.SelectRow(newRowHandle);

                // Cập nhật tổng tiền hoặc các logic khác nếu cần
                gvDatPhong_RowCountChanged(null, null); // Gọi lại để tính tổng
            }
        }
        private void gcDatPhong_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRow)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void gvDatPhong_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0) // Chỉ xử lý nếu có hàng focused
            {
                var idPhongValue = gvDatPhong.GetRowCellValue(e.FocusedRowHandle, "IdPhong")?.ToString();
                if (!string.IsNullOrEmpty(idPhongValue))
                {
                    _idPhong = int.Parse(idPhongValue);
                    _tenPhong = gvDatPhong.GetRowCellValue(e.FocusedRowHandle, "TenPhong")?.ToString() ?? string.Empty;
                }
                else
                {
                    _idPhong = 0; // Reset nếu null
                }
            }
        }
        private void gcPhong_DragDrop(object sender, DragEventArgs e)
        {
            GridControl grid = sender as GridControl;
            DataTable table = grid.DataSource as DataTable;
            DataRow row = e.Data.GetData(typeof(DataRow)) as DataRow;
            if (row != null && table != null && row.Table != table)
            {
                // Lấy IdPhong từ row bị remove (giả sử cột "IdPhong" tồn tại trong row)
                int idPhongToRemove = 0;
                if (row["IdPhong"] != null && int.TryParse(row["IdPhong"].ToString(), out int parsedId))
                {
                    idPhongToRemove = parsedId;
                }

                // Di chuyển row như cũ
                table.ImportRow(row);
                row.Table.Rows.Remove(row);

                // Xóa dữ liệu liên quan trong lstDPSP nếu IdPhong hợp lệ
                if (idPhongToRemove != 0)
                {
                    // Xóa tất cả item có IdPhong khớp (sử dụng RemoveAll để xóa)
                    lstDPSP.RemoveAll(item => item.IdPhong == idPhongToRemove);
                }

                // Refresh grid gvPhong
                gvPhong.RefreshData();

                // Refresh gvSPDV để dữ liệu biến mất
                LoadSPDV();  // Gọi lại phương thức load của bạn để refresh gvSPDV từ lstDPSP

                // Cập nhật tổng tiền nếu cần (gọi lại sự kiện tính tổng)
                gvDatPhong_RowCountChanged(null, null);
            }
        }

        private void gcPhong_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataRow)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        bool Cal(Int32 Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < Width ? Width : _View.IndicatorWidth;
            return true;
        }

        private void gvPhong_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvPhong.IsGroupRow(e.RowHandle)) // Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) // Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; // Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Số thứ tự tăng dần
                    }

                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); // Lấy kích thước của vùng hiển thị Text
                    int _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvPhong); })); // Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); // Nhân -1 để đánh lại số thứ tự tăng dần

                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                int _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvPhong); }));
            }
        }
        private void gvPhong_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            string caption = info.Column.Caption;

            if (info.Column.Caption == string.Empty)
            {
                caption = info.Column.ToString();
            }

            info.GroupText = string.Format("{0}: {1} ({2} phòng trống)", caption, info.GroupValueText, view.GetChildRowCount(e.RowHandle));
        }

        private void gvDatPhong_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvDatPhong.IsGroupRow(e.RowHandle)) // Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) // Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; // Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Số thứ tự tăng dần
                    }

                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); // Lấy kích thước của vùng hiển thị Text
                    int _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvDatPhong); })); // Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); // Nhân -1 để đánh lại số thứ tự tăng dần

                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                int _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvDatPhong); }));
            }
        }
        private void gvSPDV_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvSPDV.IsGroupRow(e.RowHandle)) // Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) // Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; // Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Số thứ tự tăng dần
                    }

                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); // Lấy kích thước của vùng hiển thị Text
                    int _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvSPDV); })); // Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); // Nhân -1 để đánh lại số thứ tự tăng dần

                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                int _width = Convert.ToInt32(_size.Width) + 20;

                BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvSPDV); }));
            }
        }
        private void gvSanPham_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvSanPham.IsGroupRow(e.RowHandle)) // Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) // Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; // Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Số thứ tự tăng dần
                    }

                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); // Lấy kích thước của vùng hiển thị Text
                    int _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvSanPham); })); // Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); // Nhân -1 để đánh lại số thứ tự tăng dần

                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                int _width = Convert.ToInt32(_size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvSanPham); }));
            }
        }

        void TinhTongTien()
        {
            gvSPDV.RefreshData();
            gvDatPhong.RefreshData();

            decimal tongSPDV = Convert.ToDecimal(
                gvSPDV.Columns["ThanhTien"].SummaryItem.SummaryValue ?? 0
            );

            decimal tongPhong = Convert.ToDecimal(
                gvDatPhong.Columns["DonGia"].SummaryItem.SummaryValue ?? 0
            );

            txtThanhTien.Text = (tongSPDV + tongPhong).ToString("N3");
        }

        private void gcSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (_idPhong == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng!");
                return;
            }

            if (gvSanPham.GetFocusedRowCellValue("IdSP") == null) return;

            int idSP = Convert.ToInt32(gvSanPham.GetFocusedRowCellValue("IdSP"));

            // ===== 1. KIỂM TRA + TRỪ KHO TRONG DATABASE =====
            using (var db = Entities.CreateEntities())
            {
                var spDB = db.SanPham.Find(idSP);
                if (spDB == null) return;

                if (spDB.SL <= 0)
                {
                    XtraMessageBox.Show(
                        "Sản phẩm đã hết hàng!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // Trừ kho 1 sản phẩm
                spDB.SL -= 1;
                db.SaveChanges();
            }

            // ===== 2. XỬ LÝ DANH SÁCH SẢN PHẨM ĐẶT PHÒNG =====
            var spTonTai = lstDPSP
                .FirstOrDefault(x => x.IdSP == idSP && x.IdPhong == _idPhong);

            if (spTonTai != null)
            {
                spTonTai.SoLuong++;
                spTonTai.ThanhTien = spTonTai.DonGia * spTonTai.SoLuong;
            }
            else
            {
                OBJ_DPSP sp = new OBJ_DPSP
                {
                    IdSP = idSP,
                    TenSP = gvSanPham.GetFocusedRowCellValue("TenSP").ToString(),
                    IdPhong = _idPhong,
                    TenPhong = _tenPhong,
                    DonGia = Convert.ToDecimal(gvSanPham.GetFocusedRowCellValue("DonGia")),
                    SoLuong = 1
                };
                sp.ThanhTien = sp.DonGia;
                lstDPSP.Add(sp);
            }

            // ===== 3. CẬP NHẬT GIAO DIỆN =====
            LoadSPDV();
            TinhTongTien();
        }

        void LoadSPDV()
        {
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gvSPDV.RefreshData();
            gvSPDV.Columns["TenPhong"].GroupIndex = 0;
            gvSPDV.ExpandAllGroups();
        }

        int _soLuongCu = 0;
        private void gvSPDV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong")
            {
                _soLuongCu = Convert.ToInt32(
                    gvSPDV.GetRowCellValue(e.RowHandle, "SoLuong") ?? 0
                );
            }
        }

        private void gvSPDV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "SoLuong") return;

            int soLuongMoi = Convert.ToInt32(e.Value ?? 0);

            // ❌ Không cho nhập 0
            if (soLuongMoi <= 0)
            {
                XtraMessageBox.Show(
                    "Số lượng phải lớn hơn 0",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                gvSPDV.SetRowCellValue(e.RowHandle, "SoLuong", _soLuongCu);
                return;
            }

            int idSP = Convert.ToInt32(
                gvSPDV.GetRowCellValue(e.RowHandle, "IdSP")
            );

            using (var db = Entities.CreateEntities()) // đổi đúng DbContext của bạn
            {
                var sp = db.SanPham.FirstOrDefault(x => x.IdSP == idSP);
                if (sp == null) return;

                int chenhlech = soLuongMoi - _soLuongCu;

                // ❌ Vượt quá tồn kho
                if (chenhlech > sp.SL)
                {
                    XtraMessageBox.Show(
                        $"Sản phẩm chỉ còn {sp.SL} trong kho",
                        "Không đủ hàng",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    gvSPDV.SetRowCellValue(e.RowHandle, "SoLuong", _soLuongCu);
                    return;
                }

                // ✅ Trừ / cộng tồn kho
                sp.SL -= chenhlech;
                db.SaveChanges();

                // ===== TÍNH THÀNH TIỀN =====
                decimal gia = Convert.ToDecimal(
                    gvSPDV.GetRowCellValue(e.RowHandle, "DonGia") ?? 0m
                );

                gvSPDV.SetRowCellValue(
                    e.RowHandle,
                    "ThanhTien",
                    soLuongMoi * gia
                );
            }

            // ===== CẬP NHẬT TỔNG =====
            gvSPDV.UpdateTotalSummary();

            decimal tongSPDV = Convert.ToDecimal(
                gvSPDV.Columns["ThanhTien"].SummaryItem?.SummaryValue ?? 0m
            );

            decimal tongPhong = Convert.ToDecimal(
                gvDatPhong.Columns["DonGia"].SummaryItem?.SummaryValue ?? 0m
            );

            txtThanhTien.Text = (tongSPDV + tongPhong).ToString("#,##0.000");
      
        }
    
        private void gvDatPhong_RowCountChanged(object sender, EventArgs e)
        {
            decimal tongSPDV = 0m;
            decimal tongPhong = 0m;

            // ===== Tổng dịch vụ =====
            var colThanhTien = gvSPDV.Columns["ThanhTien"];
            if (colThanhTien?.SummaryItem?.SummaryValue != null)
            {
                tongSPDV = Convert.ToDecimal(colThanhTien.SummaryItem.SummaryValue);
            }

            // ===== Tổng phòng =====
            var colDonGia = gvDatPhong.Columns["DonGia"];
            if (colDonGia?.SummaryItem?.SummaryValue != null)
            {
                tongPhong = Convert.ToDecimal(colDonGia.SummaryItem.SummaryValue);
            }

            txtThanhTien.Text = (tongPhong + tongSPDV).ToString("#,##0.000");
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            formKhachHang frm = new formKhachHang();
            frm.ShowDialog();
        }
        public void GetKhachHang(int idkh)
        {
            var _kh = _KH.getItem(idkh);
            cboKhachHang.SelectedValue = _kh.IdKH;
            cboKhachHang.Text = _kh.HoTen;
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {



        }

        private void dtTuNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                loadData();
        }

        private void dtTuNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                loadData();
        }

        private void dtDenNgay_ValueChanged(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                loadData();
        }

        private void dtDenNgay_Leave(object sender, EventArgs e)
        {
            if (dtTuNgay.Value > dtDenNgay.Value)
            {
                MessageBox.Show("Ngày không hợp lệ.", "Thông bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                loadData();
        }

        private void gcDanhSach_Click(object sender, EventArgs e)
        {

        }

        private void gcDanhSach_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDatPhong = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdDatPhong").ToString());
                var dp = _datphong.getItem(_idDatPhong);
                cboKhachHang.SelectedValue = dp.IdKH;
                dtNgayDat.Value = dp.NgayDatPhong.Value;
                dtNgayTra.Value = dp.NgayTraPhong.Value;
                spSoNguoiO.Text = dp.SoNguoiO.ToString();
                cboTrangThai.SelectedValue = dp.Status;
                txtGhiChu.Text = dp.GhiChu;
                txtThanhTien.Text = dp.SoTien.Value.ToString("N0");
                cboPhuongThucThanhToan.Text = dp.PhuongThucThanhToan;
                loadDP();
                loadDPSP();
            }
            xtraTabControl1.SelectedTabPage = pageChiTiet;
        }

        private void gvDanhSach_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvDanhSach.IsGroupRow(e.RowHandle)) // Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) // Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; // Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); // Số thứ tự tăng dần
                    }

                    SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); // Lấy kích thước của vùng hiển thị Text
                    int _width = Convert.ToInt32(_size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvDanhSach); })); // Tăng kích thước nếu Text vượt quá
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); // Nhân -1 để đánh lại số thứ tự tăng dần

                SizeF _size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                int _width = Convert.ToInt32(_size.Width) + 20;

                BeginInvoke(new MethodInvoker(delegate { Cal(_width, gvDanhSach); }));
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void gvDanhSach_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column.FieldName == "ThanhToan")
            {
                try
                {
                    var status = gvDanhSach.GetRowCellValue(e.RowHandle, "Status");
                    var disabled = gvDanhSach.GetRowCellValue(e.RowHandle, "Disabled");
                    // Debug: Hiển thị giá trị để kiểm tra (xóa sau khi test xong)
                    //MessageBox.Show($"Row {e.RowHandle}: Status = {status?.ToString() ?? "null"} (Type: {status?.GetType()?.Name ?? "null"}), Disabled = {disabled?.ToString() ?? "null"} (Type: {disabled?.GetType()?.Name ?? "null"})");
                    bool isStatusFalse = status != null && (status is bool ? !(bool)status : (status is int ? (int)status == 0 : false));
                    bool isDisabledFalse = disabled != null && (disabled is bool ? !(bool)disabled : (disabled is int ? (int)disabled == 0 : false));
                    if (isStatusFalse && isDisabledFalse)
                    {
                        // Chưa thanh toán và chưa xóa → hiện nút
                        e.RepositoryItem = btnThanhToan;
                    }
                    else
                    {
                        // Đã thanh toán hoặc đã xóa → không hiện gì
                        e.RepositoryItem = null;
                    }
                }
                catch (Exception ex)
                {
                    // Debug: Hiển thị lỗi nếu có (xóa sau khi test xong)
                    // MessageBox.Show("Lỗi trong CustomRowCellEdit: " + ex.Message);
                }
            }
        }
        private void BtnThanhToan_Click(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idDatPhong = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdDatPhong").ToString());
                var dp = _datphong.getItem(_idDatPhong);
                cboKhachHang.SelectedValue = dp.IdKH;
                dtNgayDat.Value = dp.NgayDatPhong.Value;
                dtNgayTra.Value = dp.NgayTraPhong.Value;
                spSoNguoiO.Text = dp.SoNguoiO.ToString();
                cboTrangThai.SelectedValue = dp.Status;
                txtGhiChu.Text = dp.GhiChu;
                txtThanhTien.Text = dp.SoTien.Value.ToString("N0");
                cboPhuongThucThanhToan.Text = dp.PhuongThucThanhToan;
                loadDP();
                loadDPSP();
                cboTrangThai.SelectedIndex = 1;
            }
            showHideControl(false);
            _enabled(true);
            xtraTabControl1.SelectedTabPage = pageChiTiet;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void btnIn_Click_1(object sender, EventArgs e)
        {

            try
            {
                int idDP = 0;

                // Kiểm tra tab hiện tại
                if (xtraTabControl1.SelectedTabPage == pageChiTiet)
                {
                    idDP = _idDatPhong;
                }
                else if (xtraTabControl1.SelectedTabPage == pageDanhSach)
                {
                    if (gvDanhSach.GetFocusedRowCellValue("IdDatPhong") != null)
                    {
                        idDP = Convert.ToInt32(gvDanhSach.GetFocusedRowCellValue("IdDatPhong"));
                    }
                }

                if (idDP == 0)
                {
                    MessageBox.Show("Vui lòng chọn phiếu đặt phòng cần in!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Gọi phương thức in
                bool trangThai = false;
                if (cboTrangThai.SelectedValue != null)
                    trangThai = bool.Parse(cboTrangThai.SelectedValue.ToString());

                try
                {
                    if (trangThai == true)
                    {
                        // ✅ ĐÃ TRẢ PHÒNG → IN HÓA ĐƠN
                        DirectPrintHelper.PrintHoaDon(_idDatPhong);
                    }
                    else
                    {
                        // ✅ CHƯA TRẢ → IN PHIẾU ĐẶT PHÒNG
                        DirectPrintHelper.PrintPhieuDatPhong(_idDatPhong);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi in phiếu: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show("Lỗi khi in phiếu: " + ex.Message,
                //    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTKSP_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTKSP.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                gvSanPham.ActiveFilter.Clear();
                return;
            }

            string[] words = keyword.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> conditions = new List<string>();

            foreach (string w in words)
            {
                string word = w.Replace("'", "''");
                conditions.Add($"Upper([TenSP]) LIKE '%{word.ToUpper()}%'");
            }

            gvSanPham.ActiveFilterString = string.Join(" AND ", conditions);
        }

        private void txtTKP_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTKP.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                gvPhong.ActiveFilter.Clear();
                return;
            }

            // Tách từng từ (space)
            string[] words = keyword.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Tạo điều kiện AND cho từng từ
            List<string> conditions = new List<string>();

            foreach (string w in words)
            {
                string word = w.Replace("'", "''"); // chống lỗi SQL-like
                conditions.Add(
                    $"(Upper([TenPhong]) LIKE '%{word.ToUpper()}%' OR Upper([TenTang]) LIKE '%{word.ToUpper()}%')"
                );
            }

            gvPhong.ActiveFilterString = string.Join(" AND ", conditions);
        }

        private void groupControl6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
