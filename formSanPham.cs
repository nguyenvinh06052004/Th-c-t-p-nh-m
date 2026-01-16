using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DataLayer;
using BusinessLayer;
using System.IO;

namespace THUEPHONG
{
    public partial class formSanPham : DevExpress.XtraEditors.XtraForm
    {
        public formSanPham()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.None;
        }
         SANPHAM _SP;
        bool _them;
        int _MaSP;
        string _tenAnh = "";
        private void formSanPham_Load(object sender, EventArgs e)
        {
            _SP = new SANPHAM();
            loadData();
            gvDanhSach.OptionsView.ShowGroupPanel = false;

            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
            picSanPham.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picSanPham.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Never;
            picSanPham.Properties.ReadOnly = true;
        }
        void loadData()
        {
            // 1. Lấy dữ liệu
            var data = _SP.getAll();

            // 2. Nếu data là List<T>, loại bỏ property Users bằng Select
            var gridData = data.Select(x => new
            {
                x.IdSP,
                x.TenSP,
                x.DonGia,
                x.Disabled,
                x.HinhAnh   // 🔥 THÊM DÒNG NÀY


            })
            .ToList();

            // 3. Gán DataSource
            gcDanhSach.DataSource = gridData;

            // 4. Populate columns để GridView nhận các cột
            gvDanhSach.PopulateColumns();
            gvDanhSach.Columns["HinhAnh"].Visible = false;

            // Reset về mặc định
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

            gvDanhSach.Columns["IdSP"].Width = 100;
            gvDanhSach.Columns["IdSP"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["IdSP"].Caption = "MÃ SẢN PHẨM";

            gvDanhSach.Columns["TenSP"].Width = 200;
            gvDanhSach.Columns["TenSP"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["TenSP"].Caption = "TÊN SẢN PHẨM";

            gvDanhSach.Columns["DonGia"].Width = 100;
            gvDanhSach.Columns["DonGia"].OptionsColumn.FixedWidth = true;
            gvDanhSach.Columns["DonGia"].Caption = "ĐƠN GIÁ";
            gvDanhSach.Columns["DonGia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gvDanhSach.Columns["DonGia"].DisplayFormat.FormatString = "0.000";




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
            txtDonGia.Enabled = t;
            chkDisabled.Enabled = t;
        }

        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDonGia.Text = "";
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
            btnChonAnh.Visible = true;
            picSanPham.Visible = false;
            showHideControl(false);
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Hình ảnh (*.jpg;*.png)|*.jpg;*.png";
            dlg.Title = "Chọn hình sản phẩm";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // 🔥 LƯU FULL PATH
                _tenAnh = dlg.FileName;   // VD: D:\HinhAnh\sp1.jpg

                // Load ảnh lên PictureEdit
                picSanPham.Image = Image.FromFile(_tenAnh);
                picSanPham.Visible = true;
                btnChonAnh.Visible = false;
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                SanPham SP = new SanPham();
                SP.TenSP = txtTen.Text;

                if (!decimal.TryParse(
                    txtDonGia.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SP.DonGia = donGia;
                SP.Disabled = chkDisabled.Checked;

                // 🔥 LƯU TÊN ẢNH
                SP.HinhAnh = _tenAnh;

                _SP.add(SP);
            }
            else
            {
                SanPham SP = _SP.getItem(_MaSP);
                SP.TenSP = txtTen.Text;

                if (!decimal.TryParse(
                    txtDonGia.Text,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out decimal donGia))
                {
                    MessageBox.Show("Đơn giá không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SP.DonGia = donGia;
                SP.Disabled = chkDisabled.Checked;

                // 🔥 CHỈ UPDATE ẢNH KHI CÓ CHỌN ẢNH MỚI
                if (!string.IsNullOrEmpty(_tenAnh))
                    SP.HinhAnh = _tenAnh;

                _SP.Update(SP);
            }

            // reset trạng thái
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
            btnChonAnh.Visible = false;
            picSanPham.Visible = true;
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
            _tenAnh = "";
            picSanPham.Image = null;
            picSanPham.Visible = false;
            btnChonAnh.Visible = true;
            showHideControl(false);
            _enabled(true);
            _reset();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chăn xóa SPông?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _SP.Delete(_MaSP);

            }
            loadData();
        }

        private void gcDanhSach_Click(object sender, EventArgs e)
        {

        }
        private void gcDanhSach_Load(object sender, EventArgs e)
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


        void LoadHinhSanPham(string hinhAnh)
        {
            if (string.IsNullOrEmpty(hinhAnh))
            {
                picSanPham.Image = null;
                return;
            }

            if (File.Exists(hinhAnh))
                picSanPham.LoadAsync(hinhAnh);
            else
                picSanPham.Image = null;
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                object cell = gvDanhSach.GetFocusedRowCellValue("IdSP");
                _MaSP = Convert.ToInt32(cell ?? 0);
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("IdSP").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TenSP").ToString();
                // Khi hiển thị dữ liệu vào textbox
                decimal donGia = Convert.ToDecimal(gvDanhSach.GetFocusedRowCellValue("DonGia"));
                txtDonGia.Text = donGia.ToString("0.000", System.Globalization.CultureInfo.InvariantCulture);

                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("Disabled").ToString());

                // ===== LOAD ẢNH SẢN PHẨM =====
                string hinhAnh = gvDanhSach.GetFocusedRowCellValue("HinhAnh")?.ToString();
                LoadHinhSanPham(hinhAnh);
            }
        }
        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (txtDonGia.Text == "") return;

            txtDonGia.TextChanged -= txtDonGia_TextChanged; // tránh loop

            string raw = txtDonGia.Text.Replace(".", "").Replace(",", "");
            if (double.TryParse(raw, out double val))
            {
                txtDonGia.Text = val.ToString("#,0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
                txtDonGia.SelectionStart = txtDonGia.Text.Length;
            }

            txtDonGia.TextChanged += txtDonGia_TextChanged;
        }
    }
}