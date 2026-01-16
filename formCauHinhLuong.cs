using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;

namespace THUEPHONG
{
    public partial class formCauHinhLuong : DevExpress.XtraEditors.XtraForm
    {
        CAUHINHLUONG _cauhinhluong;
        bool _them;
        int _idch;

        public formCauHinhLuong()
        {
            InitializeComponent();
        }

        private void formCauHinhLuong_Load(object sender, EventArgs e)
        {
            _cauhinhluong = new CAUHINHLUONG();

            LoadData();
            ShowHideControl(true);
            EnableControl(false);
        }

        void LoadData()
        {
            gcDanhSach.DataSource = _cauhinhluong.GetAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void ShowHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void EnableControl(bool t)
        {
            txtChucVu.Enabled = t;
            txtLuongCoBan.Enabled = t;
            txtPhuCapChucVu.Enabled = t;
            txtPhuCapAnTrua.Enabled = t;
            txtPhuCapXangXe.Enabled = t;
            txtGhiChu.Enabled = t;
        }

        void Reset()
        {
            txtChucVu.Clear();
            txtLuongCoBan.Text = "0";
            txtPhuCapChucVu.Text = "0";
            txtPhuCapAnTrua.Text = "0";
            txtPhuCapXangXe.Text = "0";
            txtGhiChu.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (!UserSession.HasPermission("CAUHINHLUONG", "ADD"))
            //{
            //    XtraMessageBox.Show("Bạn không có quyền thêm cấu hình lương!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            _them = true;
            ShowHideControl(false);
            EnableControl(true);
            Reset();
            txtChucVu.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //if (!UserSession.HasPermission("CAUHINHLUONG", "EDIT"))
            //{
            //    XtraMessageBox.Show("Bạn không có quyền sửa cấu hình lương!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (gvDanhSach.GetFocusedRowCellValue("IdCauHinh") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = false;
            _idch = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdCauHinh").ToString());
            ShowHideControl(false);
            EnableControl(true);

            var ch = _cauhinhluong.GetItem(_idch);
            if (ch != null)
            {
                txtChucVu.Text = ch.ChucVu;
                txtLuongCoBan.Text = ch.LuongCoBan.HasValue ? ch.LuongCoBan.Value.ToString("N0") : "0";
                txtPhuCapChucVu.Text = ch.PhuCapChucVu.HasValue ? ch.PhuCapChucVu.Value.ToString("N0") : "0";
                txtPhuCapAnTrua.Text = ch.PhuCapAnTrua.HasValue ? ch.PhuCapAnTrua.Value.ToString("N0") : "0";
                txtPhuCapXangXe.Text = ch.PhuCapXangXe.HasValue ? ch.PhuCapXangXe.Value.ToString("N0") : "0";
                txtGhiChu.Text = ch.GhiChu;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //if (!UserSession.HasPermission("CAUHINHLUONG", "DELETE"))
            //{
            //    XtraMessageBox.Show("Bạn không có quyền xóa cấu hình lương!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (gvDanhSach.GetFocusedRowCellValue("IdCauHinh") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idch = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdCauHinh").ToString());

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa cấu hình này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _cauhinhluong.Delete(idch);
                    XtraMessageBox.Show("Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtChucVu.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập chức vụ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChucVu.Focus();
                return;
            }

            try
            {
                if (_them)
                {
                    CauHinhLuong ch = new CauHinhLuong
                    {
                        ChucVu = txtChucVu.Text.Trim(),
                        LuongCoBan = decimal.Parse(txtLuongCoBan.Text.Replace(",", "")),
                        PhuCapChucVu = decimal.Parse(txtPhuCapChucVu.Text.Replace(",", "")),
                        PhuCapAnTrua = decimal.Parse(txtPhuCapAnTrua.Text.Replace(",", "")),
                        PhuCapXangXe = decimal.Parse(txtPhuCapXangXe.Text.Replace(",", "")),
                        GhiChu = txtGhiChu.Text.Trim()
                    };

                    _cauhinhluong.Add(ch);
                    XtraMessageBox.Show("Thêm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    CauHinhLuong ch = _cauhinhluong.GetItem(_idch);
                    if (ch != null)
                    {
                        ch.ChucVu = txtChucVu.Text.Trim();
                        ch.LuongCoBan = decimal.Parse(txtLuongCoBan.Text.Replace(",", ""));
                        ch.PhuCapChucVu = decimal.Parse(txtPhuCapChucVu.Text.Replace(",", ""));
                        ch.PhuCapAnTrua = decimal.Parse(txtPhuCapAnTrua.Text.Replace(",", ""));
                        ch.PhuCapXangXe = decimal.Parse(txtPhuCapXangXe.Text.Replace(",", ""));
                        ch.GhiChu = txtGhiChu.Text.Trim();

                        _cauhinhluong.Update(ch);
                        XtraMessageBox.Show("Cập nhật thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadData();
                ShowHideControl(true);
                EnableControl(false);
                _them = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            ShowHideControl(true);
            EnableControl(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.GetFocusedRowCellValue("IdCauHinh") != null)
            {
                _idch = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdCauHinh").ToString());
                var ch = _cauhinhluong.GetItem(_idch);
                if (ch != null)
                {
                    txtChucVu.Text = ch.ChucVu;
                    txtLuongCoBan.Text = ch.LuongCoBan.HasValue ? ch.LuongCoBan.Value.ToString("N0") : "0";
                    txtPhuCapChucVu.Text = ch.PhuCapChucVu.HasValue ? ch.PhuCapChucVu.Value.ToString("N0") : "0";
                    txtPhuCapAnTrua.Text = ch.PhuCapAnTrua.HasValue ? ch.PhuCapAnTrua.Value.ToString("N0") : "0";
                    txtPhuCapXangXe.Text = ch.PhuCapXangXe.HasValue ? ch.PhuCapXangXe.Value.ToString("N0") : "0";
                    txtGhiChu.Text = ch.GhiChu;
                }
            }
        }
    }
}