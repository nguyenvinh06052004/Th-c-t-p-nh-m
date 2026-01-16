using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using THUEPHONG.Reports;
using THUEPHONG.Reports;

namespace THUEPHONG
{
    public partial class DatPhongDon : Form
    {
        public DatPhongDon()
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
            InitializeComponent();
            gvSPDV.CellValueChanging += gvSPDV_CellValueChanging;


        }
        private void gvSPDV_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SoLuong")
            {
                _soLuongCu = Convert.ToInt32(
                    gvSPDV.GetRowCellValue(e.RowHandle, "SoLuong") ?? 0
                );
            }
        }

        // ===== BIẾN TOÀN CỤC =====
        bool _them;
        public int _idPhong;
        public int _idDatPhong ;
        string _maCTY;
        string _maDVI;
        string _tenPhong;
        List<OBJ_DPSP> lstDPSP;

        SYSPARAM _sysparam;
        DATPHONG _datphong;
        DATPHONG_CHITIET _datphongchitiet;
        DATPHONG_SP _datphongsp;
        KHACHHANG _kh;  
        SANPHAM _sp;
        PHONG _phong;

        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];

        // ===== FORM LOAD =====
        private void DatPhongDon_Load(object sender, EventArgs e)
        {
            this.Width = 1494;
            this.Height = 989;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Khởi tạo Business Layer
            _sysparam = new SYSPARAM();
            _datphong = new DATPHONG();
            _datphongchitiet = new DATPHONG_CHITIET();
            _datphongsp = new DATPHONG_SP();
            _kh = new KHACHHANG();
            _sp = new SANPHAM();
            _phong = new PHONG();

            lstDPSP = new List<OBJ_DPSP>();

            // Lấy thông tin hệ thống
            var pr = _sysparam.GetParam();
            _maCTY = pr.MaCTY;
            _maDVI = pr.MaDVI;

            // Load dữ liệu
            LoadPhongInfo();
            LoadKhachHang();
            LoadTrangThai();
            LoadSanPham();

            TinhTongTien();
            CreateSPDVColumns();
            {
                showHideControl(true);
                _enabled(false);
                //_enabled(true);
                var phong = _phong.getItem(_idPhong);

                // 👉 TRƯỜNG HỢP MỞ FORM TỪ "CẬP NHẬT DỊCH VỤ"
                if (_idDatPhong > 0)
                {
                    var dp = _datphong.getItem(_idDatPhong);
                    if (dp != null)
                    {
                        LoadThongTinDatPhong(dp);
                        btnThem.Enabled = false;
                    }
                }
                // 👉 TRƯỜNG HỢP ĐẶT PHÒNG MỚI
                else if (phong.TrangThai == "1")
                {
                    var dp = _datphong.getDatPhongDangHoatDongByPhong(_idPhong);
                    if (dp != null)
                    {
                        _idDatPhong = dp.IdDatPhong;
                        LoadThongTinDatPhong(dp);
                        btnThem.Enabled = false;
                    }
                }
                else
                {
                    btnThem.Enabled = true;
                    dtNgayDat.Value = DateTime.Now;
                    dtNgayTra.Value = DateTime.Now.AddDays(1);
                }
                TinhTongTien();

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
        }
        private void BtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            var view = gvSPDV;

            int rowHandle = view.FocusedRowHandle;
            if (rowHandle < 0) return;

            // Xoá dòng trên Grid
            view.DeleteRow(rowHandle);

        }
        void LoadThongTinDatPhong(DatPhong dp)
        {
            cboKhachHang.SelectedValue = dp.IdKH;
            dtNgayDat.Value = dp.NgayDatPhong.Value;
            dtNgayTra.Value = dp.NgayTraPhong.Value;

            cboTrangThai.SelectedValue = dp.Status;
            spSoNguoiO.EditValue = dp.SoNguoiO;
            txtGhiChu.Text = dp.GhiChu;
            textBox1.Text = (dp.SoTien ?? 0).ToString("N0");


            LoadSPDVTheoDatPhong(dp.IdDatPhong);
        }

        void LoadSPDVTheoDatPhong(int idDatPhong)
        {
            lstDPSP.Clear();

            var data = _datphongsp.getByDatPhong(idDatPhong);

            foreach (var item in data)
            {
                OBJ_DPSP sp = new OBJ_DPSP();

                sp.IdSP = item.IdSP ?? 0;
                sp.SoLuong = item.SoLuong ?? 0;
                sp.DonGia = item.DonGia ?? 0m;
                sp.ThanhTien = item.ThanhTien ?? 0m;
                sp.IdPhong = item.IdPhong ?? 0;

                if (item.IdSP.HasValue)
                {
                    var spInfo = _sp.getItem(item.IdSP.Value);
                    if (spInfo != null)
                        sp.TenSP = spInfo.TenSP;
                }

                lstDPSP.Add(sp);
            }

            LoadSPDV();
            TinhTongTien();
        }

        // ===== LOAD DỮ LIỆU =====
        void LoadPhongInfo()
        {
            var phong = _phong.getItem(_idPhong);
            if (phong != null)
            {
                lblPhong.Text = "Phòng: " + phong.TenPhong;
                _tenPhong = phong.TenPhong;
            }
        }

        void LoadKhachHang()
        {
            var data = _kh.getAll();
            cboKhachHang.DataSource = data;
            cboKhachHang.DisplayMember = "HoTen";
            cboKhachHang.ValueMember = "IdKH";
        }

        void LoadTrangThai()
        {
            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.DisplayMember = "_display";
            cboTrangThai.ValueMember = "_value";
        }

        void LoadSanPham()
        {
            var data = _sp.getAll();
            var gridData = data.Select(x => new
            {
                x.TenSP,
                x.DonGia,
                x.IdSP,
            }).ToList();

            gcSanPham.DataSource = gridData;
            gvSanPham.PopulateColumns();

            gvSanPham.Appearance.HeaderPanel.Font = new Font("Tahoma", 8F, FontStyle.Bold);
            gvSanPham.Appearance.HeaderPanel.Options.UseFont = true;
            gvSanPham.Appearance.HeaderPanel.ForeColor = Color.Black;
            gvSanPham.OptionsView.ColumnAutoWidth = false;

            gvSanPham.Columns["TenSP"].VisibleIndex = 0;
            gvSanPham.Columns["TenSP"].Width = 150;
            gvSanPham.Columns["TenSP"].Caption = "TÊN SẢN PHẨM";

            gvSanPham.Columns["DonGia"].VisibleIndex = 1;
            gvSanPham.Columns["DonGia"].Width = 100;
            gvSanPham.Columns["DonGia"].Caption = "ĐƠN GIÁ";

            gvSanPham.Columns["IdSP"].Visible = false;
            gvSanPham.OptionsBehavior.Editable = false;

            // Thêm event double click
            gcSanPham.DoubleClick += gcSanPham_DoubleClick;
        }

        void CreateSPDVColumns()
        {
            gvSPDV.Columns.Clear();
            gvSPDV.OptionsView.ColumnAutoWidth = false;
            gvSPDV.Appearance.HeaderPanel.Font = new Font("Tahoma", 8, FontStyle.Bold);
            gvSPDV.Appearance.HeaderPanel.ForeColor = Color.Black;

            gvSPDV.Columns.AddVisible("TenSP", "TÊN SẢN PHẨM").Width = 180;
            gvSPDV.Columns.AddVisible("SoLuong", "SỐ LƯỢNG").Width = 100;
            gvSPDV.Columns.AddVisible("DonGia", "ĐƠN GIÁ").Width = 120;
            gvSPDV.Columns.AddVisible("ThanhTien", "THÀNH TIỀN").Width = 120;

            // Summary
            gvSPDV.OptionsView.ShowFooter = true;
            gvSPDV.Columns["ThanhTien"].Summary.Add(
                DevExpress.Data.SummaryItemType.Sum, "ThanhTien", "{0:#,##0}"
            );

            // Cho phép edit số lượng
            gvSPDV.OptionsBehavior.Editable = true;
            gvSPDV.Columns["TenSP"].OptionsColumn.AllowEdit = false;
            gvSPDV.Columns["DonGia"].OptionsColumn.AllowEdit = false;
            gvSPDV.Columns["ThanhTien"].OptionsColumn.AllowEdit = false;
            gvSPDV.Columns["SoLuong"].OptionsColumn.AllowEdit = true;

            // Cột ẩn
            GridColumn colIdSP = gvSPDV.Columns.AddField("IdSP");
            colIdSP.Visible = false;

            // Event
            gvSPDV.CellValueChanged += gvSPDV_CellValueChanged;

            foreach (GridColumn col in gvSPDV.Columns)
                col.OptionsColumn.FixedWidth = true;
        }
        int _soLuongCu = 0;

        // ===== HIỂN THỊ/ẨN CONTROLS =====
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
            gcSanPham.Enabled = t;
        }

        void _reset()
        {
            //_idDatPhong = 0;
            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);
            spSoNguoiO.EditValue = 1;
            cboTrangThai.SelectedValue = false;
            txtGhiChu.Text = "";
            textBox1.Text = "0";

            lstDPSP.Clear();
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gvSPDV.RefreshData();
        }

        // ===== SỰ KIỆN THÊM SẢN PHẨM =====
        private void gcSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (gvSanPham.GetFocusedRowCellValue("IdSP") == null) return;

            int idSP = Convert.ToInt32(gvSanPham.GetFocusedRowCellValue("IdSP"));

            using (var db = Entities.CreateEntities())
            {
                var spDB = db.SanPham.Find(idSP);
                if (spDB == null) return;

                // ❌ Hết hàng
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

                // ✅ Trừ kho 1 sản phẩm
                spDB.SL -= 1;
                db.SaveChanges();
            }

            // ===== Thêm vào danh sách đặt phòng =====
            OBJ_DPSP sp = new OBJ_DPSP();
            sp.IdSP = idSP;
            sp.TenSP = gvSanPham.GetFocusedRowCellValue("TenSP").ToString();
            sp.IdPhong = _idPhong;
            sp.TenPhong = _tenPhong;
            sp.DonGia = Convert.ToDecimal(gvSanPham.GetFocusedRowCellValue("DonGia"));
            sp.SoLuong = 1;
            sp.ThanhTien = sp.DonGia;

            // ===== Kiểm tra trùng =====
            foreach (var item in lstDPSP)
            {
                if (item.IdSP == sp.IdSP)
                {
                    item.SoLuong += 1;
                    item.ThanhTien = item.DonGia * item.SoLuong;

                    LoadSPDV();
                    TinhTongTien();
                    return;
                }
            }

            lstDPSP.Add(sp);
            LoadSPDV();
            TinhTongTien();
        }


        void LoadSPDV()
        {
            gcSPDV.DataSource = null;
            gcSPDV.DataSource = lstDPSP;
            gvSPDV.RefreshData();
        }

        private void gvSPDV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName != "SoLuong") return;

            int soLuongMoi = Convert.ToInt32(e.Value ?? 0);

            // ❌ Không cho nhập 0
            if (soLuongMoi <= 0)
            {
                XtraMessageBox.Show("Số lượng phải lớn hơn 0!");
                gvSPDV.SetRowCellValue(e.RowHandle, "SoLuong", _soLuongCu);
                return;
            }

            int idSP = Convert.ToInt32(
                gvSPDV.GetRowCellValue(e.RowHandle, "IdSP")
            );

            using (var db = Entities.CreateEntities())
            {
                var sp = db.SanPham.Find(idSP);
                if (sp == null) return;

                int chenhLech = soLuongMoi - _soLuongCu;

                // ❌ Nhập vượt quá tồn kho
                if (chenhLech > 0 && sp.SL < chenhLech)
                {
                    XtraMessageBox.Show(
                        $"Sản phẩm chỉ còn {sp.SL} trong kho!",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    gvSPDV.SetRowCellValue(e.RowHandle, "SoLuong", _soLuongCu);
                    return;
                }

                // ✅ Cập nhật tồn kho
                sp.SL -= chenhLech;
                db.SaveChanges();
            }

            // ✅ Tính lại thành tiền
            decimal gia = Convert.ToDecimal(
                gvSPDV.GetRowCellValue(e.RowHandle, "DonGia") ?? 0m
            );

            gvSPDV.SetRowCellValue(
                e.RowHandle,
                "ThanhTien",
                soLuongMoi * gia
            );

            gvSPDV.UpdateTotalSummary();
            TinhTongTien();
        }


        void TinhTongTien()
        {
            decimal tongSPDV = Convert.ToDecimal(
                gvSPDV.Columns["ThanhTien"]?.SummaryItem?.SummaryValue ?? 0m
            );

            // Tính tiền phòng
            var phong = _phong.getItem(_idPhong);
            decimal donGiaPhong = 0;
            if (phong != null && phong.IdLoaiPhong > 0)
            {
                LOAIPHONG _loaiphong = new LOAIPHONG();
                var loai = _loaiphong.getItem(phong.IdLoaiPhong);
                donGiaPhong = loai?.DonGia ?? 0;
            }

            int soNgay = (dtNgayTra.Value - dtNgayDat.Value).Days;
            if (soNgay < 1) soNgay = 1;

            decimal tienPhong = donGiaPhong * soNgay;
            decimal tongTien = tongSPDV + tienPhong;

            //textBox1.Text = myFunctions.NormalizeMoney(tongTien).ToString("N0");
        }

        // ===== NÚT LƯU =====
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate
            if (cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }

            if (dtNgayTra.Value <= dtNgayDat.Value)
            {
                MessageBox.Show("Ngày trả phải sau ngày đặt!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtNgayTra.Focus();
                return;
            }

            try
            {
                SaveData();

                MessageBox.Show("Lưu thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh main form
                if (objMain != null)
                {
                    objMain.gControl.Gallery.Groups.Clear();
                    objMain.showRoom();
                }

                _them = false;
                _enabled(false);
                showHideControl(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void SaveData()
        {
            if (_them)
            {
                // THÊM MỚI
                DatPhong dp = new DatPhong();
                dp.NgayDatPhong = dtNgayDat.Value;
                dp.NgayTraPhong = dtNgayTra.Value;
                dp.SoNguoiO = int.Parse(spSoNguoiO.EditValue.ToString());
                dp.Status = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.IdKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                dp.GhiChu = txtGhiChu.Text;
                dp.Disabled = false;
                dp.SoTien = decimal.Parse(textBox1.Text.Replace(",", ""));
                dp.UId = 1;
                dp.MaCTY = _maCTY;
                dp.MaDVI = _maDVI;
                dp.TheoDoan = false;

                var _dp = _datphong.add(dp);
                _idDatPhong = _dp.IdDatPhong;

                // Thêm chi tiết phòng
                DatPhong_ChiTiet dpct = new DatPhong_ChiTiet();
                dpct.IdDatPhong = _dp.IdDatPhong;
                dpct.IdPhong = _idPhong;
                dpct.SoNgayO = (dtNgayTra.Value - dtNgayDat.Value).Days;

                // Lấy đơn giá phòng
                var phong = _phong.getItem(_idPhong);
                if (phong != null && phong.IdLoaiPhong > 0)
                {
                    LOAIPHONG _loaiphong = new LOAIPHONG();
                    var loai = _loaiphong.getItem(phong.IdLoaiPhong);
                    dpct.DonGia = loai?.DonGia ?? 0;
                }

                dpct.ThanhTien = dpct.SoNgayO * dpct.DonGia;
                var _dpct = _datphongchitiet.add(dpct);

                // Cập nhật trạng thái phòng
                if (dp.Status == true)
                { _phong.updateStatus(_idPhong, false); }
                else
                { _phong.updateStatus(_idPhong, true); }

                // Thêm sản phẩm - dịch vụ
                if (lstDPSP.Count > 0)
                {
                    foreach (var item in lstDPSP)
                    {
                        DatPhong_SanPham dpsp = new DatPhong_SanPham();
                        dpsp.IdDatPhong = _dp.IdDatPhong;
                        dpsp.IdDPCT = _dpct.IdDPCT;
                        dpsp.IdPhong = _idPhong;
                        dpsp.IdSP = item.IdSP;
                        dpsp.SoLuong = item.SoLuong;
                        dpsp.DonGia = item.DonGia;
                        dpsp.ThanhTien = item.ThanhTien;
                        _datphongsp.add(dpsp);
                    }
                }
            

                var phong1 = _phong.getItem(_idPhong);
                if (phong1.TrangThai == "1")
                {
                    btnThem.Enabled = false;
                }

            }
            else
            {
                // CẬP NHẬT
                DatPhong dp = _datphong.getItem(_idDatPhong);
                //DatPhong dp = _datphong.getItem(_idDatPhong);
                if (dp == null)
                {
                    MessageBox.Show("Không tìm thấy phiếu đặt phòng để cập nhật!");
                    return;
                }

                dp.NgayDatPhong = dtNgayDat.Value;
                dp.NgayTraPhong = dtNgayTra.Value;
                dp.SoNguoiO = int.Parse(spSoNguoiO.EditValue.ToString());
                dp.Status = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.IdKH = int.Parse(cboKhachHang.SelectedValue.ToString());
                dp.GhiChu = txtGhiChu.Text;
                dp.SoTien = decimal.Parse(textBox1.Text.Replace(",", ""));
                dp.UId = 1;

                var _dp = _datphong.update(dp);

                // Xóa chi tiết cũ
                _datphongchitiet.deleteAll(_dp.IdDatPhong);
                _datphongsp.deleteAll(_dp.IdDatPhong);

                // Thêm lại chi tiết mới (tương tự phần thêm)
                DatPhong_ChiTiet dpct = new DatPhong_ChiTiet();
                dpct.IdDatPhong = _dp.IdDatPhong;
                dpct.IdPhong = _idPhong;
                dpct.SoNgayO = (dtNgayTra.Value - dtNgayDat.Value).Days;

                var phong = _phong.getItem(_idPhong);
                if (phong != null && phong.IdLoaiPhong > 0)
                {
                    LOAIPHONG _loaiphong = new LOAIPHONG();
                    var loai = _loaiphong.getItem(phong.IdLoaiPhong);
                    dpct.DonGia = loai?.DonGia ?? 0;
                }

                dpct.ThanhTien = dpct.SoNgayO * dpct.DonGia;
                var _dpct = _datphongchitiet.add(dpct);

                // Cập nhật trạng thái phòng
                if (dp.Status == true)
                { _phong.updateStatus(_idPhong, false); }
                else
                { _phong.updateStatus(_idPhong, true); }

                if (lstDPSP.Count > 0)
                {
                    foreach (var item in lstDPSP)
                    {
                        DatPhong_SanPham dpsp = new DatPhong_SanPham();
                        dpsp.IdDatPhong = _dp.IdDatPhong;
                        dpsp.IdDPCT = _dpct.IdDPCT;
                        dpsp.IdPhong = _idPhong;
                        dpsp.IdSP = item.IdSP;
                        dpsp.SoLuong = item.SoLuong;
                        dpsp.DonGia = item.DonGia;
                        dpsp.ThanhTien = item.ThanhTien;
                        _datphongsp.add(dpsp);
                    }
                }
                else
                {
                    DatPhong_SanPham dpsp = new DatPhong_SanPham();
                    dpsp.IdDatPhong = _dp.IdDatPhong;
                    dpsp.IdDPCT = _dpct.IdDPCT;
                    dpsp.IdPhong = _idPhong;
                    _datphongsp.add(dpsp);
                }

                var phong3 = _phong.getItem(_idPhong);
                if (phong3.TrangThai == "1")
                {
                    btnThem.Enabled = false;
                }

            }
        }
        public void GetKhachHang(int idkh)
        {
            var _khach = _kh.getItem(idkh);
            cboKhachHang.SelectedValue = _khach.IdKH;
            cboKhachHang.Text = _khach.HoTen;
        }

        // ===== NÚT IN =====
        //private void btnIn_Click(object sender, EventArgs e)
        //{
        //    if (_idDatPhong == 0)
        //    {
        //        MessageBox.Show("Chưa có phiếu đặt phòng để in!", "Thông báo",
        //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    bool trangThai = false;
        //    if (cboTrangThai.SelectedValue != null)
        //        trangThai = bool.Parse(cboTrangThai.SelectedValue.ToString());

        //    try
        //    {
        //        if (trangThai == true)
        //        {
        //            // ✅ ĐÃ TRẢ PHÒNG → IN HÓA ĐƠN
        //            DirectPrintHelper.PrintHoaDon(_idDatPhong);
        //        }
        //        else
        //        {
        //            // ✅ CHƯA TRẢ → IN PHIẾU ĐẶT PHÒNG
        //            DirectPrintHelper.PrintPhieuDatPhong(_idDatPhong);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi khi in phiếu: " + ex.Message, "Lỗi",
        //            MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        // ===== NÚT THOÁT =====
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("btnThem_Click");
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("idDatPhong: " + _idDatPhong);
            if (_idDatPhong == 0)
            {
                MessageBox.Show("Chưa có phiếu đặt phòng để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = false;
             _enabled(true);
            showHideControl(false);
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (_idDatPhong == 0)
            {
                MessageBox.Show("Chưa có phiếu đặt phòng để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn xóa phiếu đặt phòng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _datphong.delete(_idDatPhong);
                    _phong.updateStatus(_idPhong, false); // Set phòng về trống

                    MessageBox.Show("Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Refresh main form
                    if (objMain != null)
                    {
                        objMain.gControl.Gallery.Groups.Clear();
                        objMain.showRoom();
                    }

                    _reset();
                    _enabled(false);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBoQua_Click_1(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (_idDatPhong == 0)
            {
                MessageBox.Show("Chưa có phiếu đặt phòng để in!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
    }
}