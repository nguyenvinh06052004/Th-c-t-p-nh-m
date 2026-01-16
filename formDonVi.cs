using BusinessLayer;
using DevExpress.XtraEditors;
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

namespace THUEPHONG
{
    public partial class formDonVi : DevExpress.XtraEditors.XtraForm
    {
        public formDonVi()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }
        DONVI _donvi;
        CONGTY _congty;
        bool _them;
        string _madvi;

        private void formDonVi_Load(object sender, EventArgs e)
        {
            _donvi = new DONVI();
            _congty = new CONGTY();
            loadCongty();
            loadData();
            showHideControl(true);
            gvDanhSach.OptionsView.ShowGroupPanel = false;
            _enabled(false);
            txtMa.Enabled = false;
            cboCty.SelectedIndexChanged += cboCty_SelectedIndexChanged;
        }

        private void cboCty_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDviByCty();
        }

        void loadCongty()
        {
            cboCty.DataSource = _congty.getAll();   
            cboCty.DisplayMember = "TenCTY";    
            cboCty.ValueMember = "MaCTY";
        }
        void loadData()
        {
            // 1. Lấy dữ liệu
            var data = _donvi.GetAll();

            // 2. Nếu data là List<T>, loại bỏ property Users bằng Select
            var gridData = data.Select(x => new
            {
                x.MaCTY,
                x.MaDVI,
                x.TenDVI,
                x.DienThoai,
                x.Fax,
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

            gvDanhSach.Columns["MaCTY"].Width = 70;
            gvDanhSach.Columns["MaCTY"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["MaCTY"].Caption = "MÃ CÔNG TY";

            gvDanhSach.Columns["MaDVI"].Width = 70;
            gvDanhSach.Columns["MaDVI"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["MaDVI"].Caption = "MÃ ĐƠN VỊ";

            gvDanhSach.Columns["TenDVI"].Width = 100;
            gvDanhSach.Columns["TenDVI"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["TenDVI"].Caption = "TÊN ĐƠN VỊ";

            gvDanhSach.Columns["DienThoai"].Width = 100;
            gvDanhSach.Columns["DienThoai"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["DienThoai"].Caption = "ĐIỆN THOẠI";

            gvDanhSach.Columns["Fax"].Width = 70;
            gvDanhSach.Columns["Fax"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["Fax"].Caption = "FAX";

            gvDanhSach.Columns["Email"].Width = 100;
            gvDanhSach.Columns["Email"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["Email"].Caption = "EMAIL";

            gvDanhSach.Columns["DiaChi"].Width = 150;
            gvDanhSach.Columns["DiaChi"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["DiaChi"].Caption = "ĐỊA CHỈ";


            gvDanhSach.OptionsBehavior.Editable = false;


        }

        void loadDviByCty()
        {
            // 1. Lấy dữ liệu
            var data = _donvi.GetAll(cboCty.SelectedValue.ToString());

            // 2. Nếu data là List<T>, loại bỏ property Users bằng Select
            var gridData = data.Select(x => new
            {
                x.MaCTY,
                x.MaDVI,
                x.TenDVI,
                x.DienThoai,
                x.Fax,
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

            gvDanhSach.Columns["MaCTY"].Width = 70;
            gvDanhSach.Columns["MaCTY"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["MaCTY"].Caption = "MÃ CÔNG TY";

            gvDanhSach.Columns["MaDVI"].Width = 70;
            gvDanhSach.Columns["MaDVI"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["MaDVI"].Caption = "MÃ ĐƠN VỊ";

            gvDanhSach.Columns["TenDVI"].Width = 100;
            gvDanhSach.Columns["TenDVI"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["TenDVI"].Caption = "TÊN ĐƠN VỊ";

            gvDanhSach.Columns["DienThoai"].Width = 100;
            gvDanhSach.Columns["DienThoai"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["DienThoai"].Caption = "ĐIỆN THOẠI";

            gvDanhSach.Columns["Fax"].Width = 70;
            gvDanhSach.Columns["Fax"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["Fax"].Caption = "FAX";

            gvDanhSach.Columns["Email"].Width = 100;
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
            txtFax.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
        }

        void _reset()
        {
            cboCty.SelectedValue = "";
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            txtMa.Enabled = true;
            _enabled(true);
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            txtMa.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chăn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _donvi.Delete(_madvi);

            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                DonVi dvi = new DonVi();
                dvi.MaCTY = cboCty.SelectedValue.ToString();
                dvi.MaDVI = txtMa.Text;
                dvi.TenDVI = txtTen.Text;
                dvi.DiaChi = txtDiaChi.Text;
                dvi.DienThoai = txtDienThoai.Text;
                dvi.Fax = txtFax.Text;
                dvi.Email = txtEmail.Text;
                dvi.Disabled = chkDisabled.Checked;
                _donvi.Add(dvi);
            }
            else
            {
                DonVi dvi = _donvi.GetItem(_madvi);
                dvi.MaCTY = cboCty.SelectedValue.ToString();
                dvi.MaDVI = txtMa.Text;
                dvi.TenDVI = txtTen.Text;
                dvi.DiaChi = txtDiaChi.Text;
                dvi.DienThoai = txtDienThoai.Text;
                dvi.Fax = txtFax.Text;
                dvi.Email = txtEmail.Text;
                dvi.Disabled = chkDisabled.Checked;
                _donvi.Update(dvi);

            }
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _madvi = gvDanhSach.GetFocusedRowCellValue("MaDVI").ToString();
                cboCty.SelectedValue = gvDanhSach.GetFocusedRowCellValue("MaCTY").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MaDVI").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenDVI").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DienThoai").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("Fax").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("Email").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DiaChi").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());

            }
        }
    }
}