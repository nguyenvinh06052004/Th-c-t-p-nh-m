using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;

namespace THUEPHONG
{
    public partial class formKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public formKhachHang()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;

        }


        KHACHHANG _KH;
        bool _them;
        int _MaKH;
        private void formKhachHang_Load(object sender, EventArgs e)
        {
            _KH = new KHACHHANG();
            loadData();
            showHideControl(true);
            gvDanhSach.OptionsView.ShowGroupPanel = false;
            _enabled(false);
            txtMa.Enabled = false;
        }
        void loadData()
        {
            // 1. Lấy dữ liệu
            var data = _KH.getAll();

            // 2. Nếu data là List<T>, loại bỏ property Users bằng Select
            var gridData = data.Select(x => new
            {
                x.IdKH,
                x.HoTen,
                x.DienThoai,
                x.CCCD,
                x.Email,
                x.DiaChi,
                x.Disabled
            }).ToList();

            // 3. Gán DataSource
            gcDanhSach.DataSource = gridData;

            // 4. Populate columns để GridView nhận các cột
            gvDanhSach.PopulateColumns();

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

            gvDanhSach.Columns["IdKH"].Width = 50;
            gvDanhSach.Columns["IdKH"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["IdKH"].Caption = "MÃ KHÁCH HÀNG";

            gvDanhSach.Columns["HoTen"].Width = 120;
            gvDanhSach.Columns["HoTen"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["HoTen"].Caption = "HỌ TÊN";

            gvDanhSach.Columns["DienThoai"].Width = 70;
            gvDanhSach.Columns["DienThoai"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["DienThoai"].Caption = "ĐIỆN THOẠI";

            gvDanhSach.Columns["CCCD"].Width = 80;
            gvDanhSach.Columns["CCCD"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["CCCD"].Caption = "CCCD";

            gvDanhSach.Columns["Email"].Width = 120;
            gvDanhSach.Columns["Email"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["Email"].Caption = "EMAIL";

            gvDanhSach.Columns["DiaChi"].Width = 150;
            gvDanhSach.Columns["DiaChi"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["DiaChi"].Caption = "ĐỊA CHỈ";


            gvDanhSach.OptionsBehavior.Editable = false;


        }



        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }
        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            txtDienThoai.Enabled = t;
            txtCCCD.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
        }

        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtCCCD.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            txtMa.Enabled = false;
            showHideControl(false);
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                KhachHang KH = new KhachHang();
                KH.HoTen = txtTen.Text;
                KH.DiaChi = txtDiaChi.Text;
                KH.DienThoai = txtDienThoai.Text;
                KH.CCCD = txtCCCD.Text;
                KH.Email = txtEmail.Text;
                KH.Disabled = chkDisabled.Checked;
                _KH.add(KH);
            }
            else
            {
                KhachHang KH = _KH.getItem(_MaKH);
                KH.HoTen = txtTen.Text;
                KH.DiaChi = txtDiaChi.Text;
                KH.DienThoai = txtDienThoai.Text;
                KH.CCCD = txtCCCD.Text;
                KH.Email = txtEmail.Text;
                KH.Disabled = chkDisabled.Checked;
                _KH.Update(KH);

            }
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
            txtMa.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chăn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _KH.Delete(_MaKH);

            }
            loadData();
        }

        private void gcDanhSach_Click(object sender, EventArgs e)
        {

        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.delete_3221897;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }

        }

        private void gcDanhSach_Load(object sender, EventArgs e)
        {

        }
        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                object cell = gvDanhSach.GetFocusedRowCellValue("IdKH");
                _MaKH = Convert.ToInt32(cell ?? 0);
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IdKH").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("HoTen").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DienThoai").ToString();
                txtCCCD.Text = gvDanhSach.GetFocusedRowCellValue("CCCD").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("Email").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DiaChi").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());

            }
        }
    }
}