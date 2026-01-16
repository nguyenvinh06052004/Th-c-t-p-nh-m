using BusinessLayer;
using DataLayer;
using DevExpress.Export.Xl;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;


namespace THUEPHONG
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private DevExpress.XtraGrid.GridControl gridControl1;
        TANG _tang = new TANG();
        PHONG _phong = new PHONG();
        DATPHONG _datphong = new DATPHONG();

        c_FUNC _func = new c_FUNC();
        GalleryItem item = null;
        int _idPhongDangChon = 0;
        string _tenPhongDangChon = "";

        public frmMain()
        {
            InitializeComponent();
            // Initialize gridControl1 if not done in designer
            gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.Controls.Add(gridControl1);
            gridControl1.Dock = DockStyle.Fill;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = $"Quản lý khách sạn - Xin chào: {UserSession.FullName} ({UserSession.RoleName})";
            leftMenu();
            showRoom();
            ApplyPermissions();
        }
        private void ApplyPermissions()
        {
            if (!UserSession.IsAdmin())
            {
                // Ví dụ: Ẩn chức năng quản lý user
                 btnHeThong.Visible = false;
            }
        }
        public void leftMenu()
        {
            int i = 0;
            var _lsParent = _func.getParent();
            foreach (var parent in _lsParent)
            {
                NavBarGroup group = new NavBarGroup(parent.DESCRIPTION);
                group.Tag = parent.FUNC_CODE;
                group.Name = parent.FUNC_CODE;
                group.ImageOptions.LargeImageIndex = i;
                i++;
                navMain.Groups.Add(group);

                var _lsChild = _func.getChild(parent.FUNC_CODE);
                int j = 0;
                foreach (var child in _lsChild)
                {
                    NavBarItem item = new NavBarItem(child.DESCRIPTION);
                    item.Tag = child.FUNC_CODE;
                    item.Name = child.FUNC_CODE;
                    item.ImageOptions.SmallImageIndex = j;
                    j++;
                    group.ItemLinks.Add(item);
                }
                navMain.Groups[group.Name].Expanded = true;

            }
        }

        private void gridLookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridLookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void navBarControl1_Click(object sender, EventArgs e)
        {

        }

        private void navMain_KeyDown(object sender, KeyEventArgs e)
        {

        }
        public void showRoom()
        {
            _phong = new PHONG();   // 🔥 BẮT BUỘC
            _tang = new TANG();     // 🔥 NÊN LÀM
            gControl.Gallery.Groups.Clear();
            var lsTang = _tang.getAll();
            gControl.Gallery.ItemImageLayout = DevExpress.Utils.Drawing.ImageLayoutMode.ZoomInside;
            gControl.Gallery.ImageSize = new Size(64, 64);
            gControl.Gallery.ShowItemText = true;
            gControl.Gallery.ShowGroupCaption = true;
            foreach (var t in lsTang)
            {
                var item = new GalleryItemGroup();
                item.Caption = t.TenTang;
                item.CaptionAlignment = GalleryItemGroupCaptionAlignment.Stretch;

                List<Phong> lsPhong = _phong.getByTang(t.IdTang);

                foreach (var p in lsPhong)
                {
                    DevExpress.XtraBars.Ribbon.GalleryItem sub_item = new DevExpress.XtraBars.Ribbon.GalleryItem();
                    sub_item.Caption = p.TenPhong;
                    sub_item.Value = p.IdPhong;
                    if (p.TrangThai == "1")
                        sub_item.ImageOptions.Image = imageList3.Images[1];
                    else
                        sub_item.ImageOptions.Image = imageList3.Images[0];
                    item.Items.Add(sub_item);
                }
                gControl.Gallery.Groups.Add(item);
            }

        }
        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string func_code = e.Link.Item.Tag.ToString();
            func_code = func_code.Trim();
           // Kiểm tra quyền xem
            if (!UserSession.HasPermission(func_code, "VIEW"))
            {
                XtraMessageBox.Show("Bạn không có quyền truy cập chức năng này!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (func_code)
            {
                case "CONGTY":
                    {
                        formCongTy frm = new formCongTy();
                        frm.ShowDialog();
                        break;
                    }
                case "DONVI":
                    {
                        formDonVi frm = new formDonVi();
                        frm.ShowDialog();
                        break;
                    }
                case "PHONG":
                    {
                        formPhong frm = new formPhong();
                        frm.ShowDialog();
                       // showRoom();
                        break;
                    }
                case "TANG":
                    {
                        formTang frm = new formTang();
                        frm.ShowDialog();
                        break;
                    }
                case "LOAIPHONG":
                    {
                        formLoaiPhong frm = new formLoaiPhong();
                        frm.ShowDialog();
                        break;
                    }
                case "THIETBI":
                    {
                        formThietBi frm = new formThietBi();
                        frm.ShowDialog();
                        break;
                    }
                case "PHONG_THIETBI":
                    {
                        formThietBi_Phong frm = new formThietBi_Phong();
                        frm.ShowDialog();
                        break;
                    }
                case "SANPHAM":
                    {
                        formSanPham frm = new formSanPham();
                        frm.ShowDialog();
                        break;
                    }
                case "KHACHHANG":
                    {
                        formKhachHang frm = new formKhachHang();
                        frm.ShowDialog();
                        break;
                    }
                case "DATPHONG":
                    {
                        formDatPhong frm = new formDatPhong();
                        frm.ShowDialog();
                        break;
                    }
                //
                // ... các case khác ...
                case "USERS":
                    {
                        formUsers frm = new formUsers();
                        frm.ShowDialog();
                        break;
                    }
                case "NHANVIEN":
                    {
                        formNhanVien frm = new formNhanVien();
                        frm.ShowDialog();
                        break;
                    }
                case "CHAMCONG":
                    {
                        formChamCong frm = new formChamCong();
                        frm.ShowDialog();
                        break;
                    }
                case "CAUHINHLUONG":
                    {
                        formCauHinhLuong frm = new formCauHinhLuong();
                        frm.ShowDialog();
                        break;
                    }
                case "BANGLUONG":
                    {
                        formBangLuong frm = new formBangLuong();
                        frm.ShowDialog();
                        break;
                    }

                default:
                    {
                        XtraMessageBox.Show("Chức năng đang phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
            }
        }

        private void popupMenu1_Popup(object sender, EventArgs e)
        {
            Point point = gControl.PointToClient(Control.MousePosition);
            RibbonHitInfo hitInfo = gControl.CalcHitInfo(point);
            if (hitInfo.InGalleryItem || hitInfo.HitTest == RibbonHitTest.GalleryImage)
            {
                item = hitInfo.GalleryItem;
                _idPhongDangChon = Convert.ToInt32(item.Value);
                _tenPhongDangChon = item.Caption;
            }
        }

        private void btnDatPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var phong = _phong.getItem(_idPhongDangChon);
            if (phong.TrangThai == "1")
            {
                XtraMessageBox.Show("Phòng " + _tenPhongDangChon + " đã được đặt. Vui lòng chọn phòng khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DatPhongDon formDatPhongDon = new DatPhongDon();
            formDatPhongDon._idPhong = int.Parse(item.Value.ToString());
            formDatPhongDon.ShowDialog();

        }

        private void btnSPDV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var phong = _phong.getItem(_idPhongDangChon);
            if (phong.TrangThai != "1")
            {
                XtraMessageBox.Show("Phòng " + _tenPhongDangChon + " chưa được đặt. Vui lòng đặt phòng trước khi cập nhật dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var dp = _datphong.getDatPhongDangHoatDongByPhong(_idPhongDangChon);

                if (dp == null)
                {
                    MessageBox.Show("Phòng chưa có phiếu đặt!");
                    return;
                }

                DatPhongDon f = new DatPhongDon();
                f._idPhong = _idPhongDangChon;
                f._idDatPhong = dp.IdDatPhong;   // ✅ QUAN TRỌNG
                f.ShowDialog();

            }
        }

        private void btnChuyenPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var phong = _phong.getItem(_idPhongDangChon);
            if (phong.TrangThai != "1")
            {
                MessageBox.Show("Phòng chưa được đặt nên không được phép chuyển. Vui lòng chọn phòng đã được đặt.", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            frmChuyenPhong frm = new frmChuyenPhong();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm.ShowDialog();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var phong = _phong.getItem(_idPhongDangChon);
            if (phong.TrangThai != "1")
            {
                XtraMessageBox.Show("Phòng " + _tenPhongDangChon + " chưa được đặt. Vui lòng đặt phòng để tiếp tục thao tác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                var dp = _datphong.getDatPhongDangHoatDongByPhong(_idPhongDangChon);

                if (dp == null)
                {
                    MessageBox.Show("Phòng chưa có phiếu đặt!");
                    return;
                }

                DatPhongDon f = new DatPhongDon();
                f._idPhong = _idPhongDangChon;
                f._idDatPhong = dp.IdDatPhong;   // ✅ QUAN TRỌNG
                f.ShowDialog();

            }
        }

        private void frmMain_Activated_1(object sender, EventArgs e)
        {
            showRoom();
        }
    }
}