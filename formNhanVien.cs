
using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;

namespace THUEPHONG
{
    public partial class formNhanVien : DevExpress.XtraEditors.XtraForm
    {
        NHANVIEN _nhanvien;
        bool _them;
        int _idnv;

        public formNhanVien()
        {
            InitializeComponent();
        }

        private void formNhanVien_Load(object sender, EventArgs e)
        {
            _nhanvien = new NHANVIEN();

            LoadData();
            ShowHideControl(true);
            EnableControl(false);

            // Set giá trị mặc định
            cboTrangThai.SelectedIndex = 0;
            dtNgaySinh.Value = DateTime.Now.AddYears(-25);
            dtNgayVaoLam.Value = DateTime.Now;
        }

        void LoadData()
        {
            gcDanhSach.DataSource = _nhanvien.GetAll();
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
            txtHoTen.Enabled = t;
            dtNgaySinh.Enabled = t;
            txtGioiTinh.Enabled = t;
            txtCCCD.Enabled = t;
            txtDiaChi.Enabled = t;
            txtDienThoai.Enabled = t;
            txtEmail.Enabled = t;
            txtChucVu.Enabled = t;
            txtPhongBan.Enabled = t;
            dtNgayVaoLam.Enabled = t;
            cboTrangThai.Enabled = t;
        }

        void Reset()
        {
            txtMaNV.Clear();
            txtHoTen.Clear();
            txtGioiTinh.Clear();
            txtCCCD.Clear();
            txtDiaChi.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtChucVu.Clear();
            txtPhongBan.Clear();
            dtNgaySinh.Value = DateTime.Now.AddYears(-25);
            dtNgayVaoLam.Value = DateTime.Now;
            cboTrangThai.SelectedIndex = 0;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("NHANVIEN", "ADD"))
            {
                XtraMessageBox.Show("Bạn không có quyền thêm nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = true;
            ShowHideControl(false);
            EnableControl(true);
            Reset();

            // Tạo mã nhân viên tự động
            txtMaNV.Text = _nhanvien.GenerateMaNV();
            txtHoTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("NHANVIEN", "EDIT"))
            {
                XtraMessageBox.Show("Bạn không có quyền sửa nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gvDanhSach.GetFocusedRowCellValue("IdNV") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = false;
            _idnv = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdNV").ToString());
            ShowHideControl(false);
            EnableControl(true);

            var nv = _nhanvien.GetItem(_idnv);
            if (nv != null)
            {
                txtMaNV.Text = nv.MaNV;
                txtHoTen.Text = nv.HoTen;
                dtNgaySinh.Value = nv.NgaySinh ?? DateTime.Now;
                txtGioiTinh.Text = nv.GioiTinh;
                txtCCCD.Text = nv.CCCD;
                txtDiaChi.Text = nv.DiaChi;
                txtDienThoai.Text = nv.DienThoai;
                txtEmail.Text = nv.Email;
                txtChucVu.Text = nv.ChucVu;
                txtPhongBan.Text = nv.PhongBan;
                dtNgayVaoLam.Value = nv.NgayVaoLam ?? DateTime.Now;
                cboTrangThai.Text = nv.TrangThai;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("NHANVIEN", "DELETE"))
            {
                XtraMessageBox.Show("Bạn không có quyền xóa nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gvDanhSach.GetFocusedRowCellValue("IdNV") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idnv = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdNV").ToString());

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _nhanvien.Delete(idnv);
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
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập họ tên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return;
            }

            try
            {
                if (_them)
                {
                    NhanVien nv = new NhanVien
                    {
                        MaNV = txtMaNV.Text.Trim(),
                        HoTen = txtHoTen.Text.Trim(),
                        NgaySinh = dtNgaySinh.Value,
                        GioiTinh = txtGioiTinh.Text.Trim(),
                        CCCD = txtCCCD.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim(),
                        DienThoai = txtDienThoai.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        ChucVu = txtChucVu.Text.Trim(),
                        PhongBan = txtPhongBan.Text.Trim(),
                        NgayVaoLam = dtNgayVaoLam.Value,
                        TrangThai = cboTrangThai.Text,
                        MaCTY = UserSession.MaCTY,
                        MaDVI = UserSession.MaDVI
                    };

                    _nhanvien.Add(nv);
                    XtraMessageBox.Show("Thêm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    NhanVien nv = _nhanvien.GetItem(_idnv);
                    if (nv != null)
                    {
                        nv.HoTen = txtHoTen.Text.Trim();
                        nv.NgaySinh = dtNgaySinh.Value;
                        nv.GioiTinh = txtGioiTinh.Text.Trim();
                        nv.CCCD = txtCCCD.Text.Trim();
                        nv.DiaChi = txtDiaChi.Text.Trim();
                        nv.DienThoai = txtDienThoai.Text.Trim();
                        nv.Email = txtEmail.Text.Trim();
                        nv.ChucVu = txtChucVu.Text.Trim();
                        nv.PhongBan = txtPhongBan.Text.Trim();
                        nv.TrangThai = cboTrangThai.Text;

                        _nhanvien.Update(nv);
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
            if (gvDanhSach.GetFocusedRowCellValue("IdNV") != null)
            {
                _idnv = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdNV").ToString());
                var nv = _nhanvien.GetItem(_idnv);
                if (nv != null)
                {
                    txtMaNV.Text = nv.MaNV;
                    txtHoTen.Text = nv.HoTen;
                    dtNgaySinh.Value = nv.NgaySinh ?? DateTime.Now;
                    txtGioiTinh.Text = nv.GioiTinh;
                    txtCCCD.Text = nv.CCCD;
                    txtDiaChi.Text = nv.DiaChi;
                    txtDienThoai.Text = nv.DienThoai;
                    txtEmail.Text = nv.Email;
                    txtChucVu.Text = nv.ChucVu;
                    txtPhongBan.Text = nv.PhongBan;
                    dtNgayVaoLam.Value = nv.NgayVaoLam ?? DateTime.Now;
                    cboTrangThai.Text = nv.TrangThai;
                }
            }
        }
    }
}
