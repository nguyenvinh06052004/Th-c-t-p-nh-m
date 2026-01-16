using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;

namespace THUEPHONG
{
    public partial class formBangLuong : DevExpress.XtraEditors.XtraForm
    {
        BANGLUONG _bangluong;
        NHANVIEN _nhanvien;
        CHAMCONG _chamcong;
        CAUHINHLUONG _cauhinhluong;
        bool _them;
        int _idbl;

        public formBangLuong()
        {
            InitializeComponent();
        }

        private void formBangLuong_Load(object sender, EventArgs e)
        {
            _bangluong = new BANGLUONG();
            _nhanvien = new NHANVIEN();
            _chamcong = new CHAMCONG();
            _cauhinhluong = new CAUHINHLUONG();

            // Set giá trị mặc định
            numThang.Value = DateTime.Now.Month;
            numNam.Value = DateTime.Now.Year;

            LoadNhanVien();
            ShowHideControl(true);
            EnableControl(false);
        }

        void LoadNhanVien()
        {
            var data = _nhanvien.GetAll().Where(nv => nv.TrangThai == "Đang làm").ToList();
            cboNhanVien.DataSource = data;
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "IdNV";
        }

        void LoadData(int thang, int nam)
        {
            var data = _bangluong.GetByThang(thang, nam)
                .Select(bl => new
                {
                    bl.IdBangLuong,
                    MaNV = bl.NhanVien != null ? bl.NhanVien.MaNV : "",
                    HoTen = bl.NhanVien != null ? bl.NhanVien.HoTen : "",
                    bl.Thang,
                    bl.Nam,
                    bl.SoNgayCong,
                    bl.LuongCoBan,
                    bl.PhuCap,
                    bl.Thuong,
                    bl.KhauTru,
                    bl.TongLuong,
                    bl.TrangThai,
                    bl.IdNV
                }).ToList();

            gcDanhSach.DataSource = data;
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        void ShowHideControl(bool t)
        {
            btnTaoBangLuong.Visible = t;
            btnSua.Visible = t;
            btnThanhToan.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void EnableControl(bool t)
        {
            txtThuong.Enabled = t;
            txtKhauTru.Enabled = t;
            txtGhiChu.Enabled = t;
        }

        void Reset()
        {
            if (cboNhanVien.Items.Count > 0)
                cboNhanVien.SelectedIndex = 0;
            txtSoNgayCong.Text = "0";
            txtSoGioLam.Text = "0";
            txtLuongCoBan.Text = "0";
            txtPhuCap.Text = "0";
            txtThuong.Text = "0";
            txtKhauTru.Text = "0";
            txtTongLuong.Text = "0";
            txtGhiChu.Clear();
        }

        private void btnXemBangLuong_Click(object sender, EventArgs e)
        {
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;
            LoadData(thang, nam);
        }

        private void btnTaoBangLuong_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("BANGLUONG", "ADD"))
            {
                XtraMessageBox.Show("Bạn không có quyền tạo bảng lương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;

            // Kiểm tra đã tạo bảng lương tháng này chưa
            var existing = _bangluong.GetByThang(thang, nam);
            if (existing.Count > 0)
            {
                if (XtraMessageBox.Show($"Bảng lương tháng {thang}/{nam} đã tồn tại!\nBạn có muốn tạo lại không?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                _bangluong.TaoBangLuongThang(thang, nam);
                XtraMessageBox.Show("Tạo bảng lương thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(thang, nam);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("BANGLUONG", "EDIT"))
            {
                XtraMessageBox.Show("Bạn không có quyền sửa bảng lương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gvDanhSach.GetFocusedRowCellValue("IdBangLuong") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThai = gvDanhSach.GetFocusedRowCellValue("TrangThai").ToString();
            if (trangThai == "Đã thanh toán")
            {
                XtraMessageBox.Show("Không thể sửa bảng lương đã thanh toán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = false;
            _idbl = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdBangLuong").ToString());
            ShowHideControl(false);
            EnableControl(true);

            var bl = _bangluong.GetItem(_idbl);
            if (bl != null)
            {
                cboNhanVien.SelectedValue = bl.IdNV;
                txtSoNgayCong.Text = bl.SoNgayCong.ToString();
                txtSoGioLam.Text = bl.SoGioLam.HasValue ? bl.SoGioLam.Value.ToString("N2") : "0";
                txtLuongCoBan.Text = bl.LuongCoBan.HasValue ? bl.LuongCoBan.Value.ToString("N0") : "0";
                txtPhuCap.Text = bl.PhuCap.HasValue ? bl.PhuCap.Value.ToString("N0") : "0";
                txtThuong.Text = bl.Thuong.HasValue ? bl.Thuong.Value.ToString("N0") : "0";
                txtKhauTru.Text = bl.KhauTru.HasValue ? bl.KhauTru.Value.ToString("N0") : "0";
                txtTongLuong.Text = bl.TongLuong.HasValue ? bl.TongLuong.Value.ToString("N0") : "0";
                txtGhiChu.Text = bl.GhiChu;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_idbl == 0)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi để cập nhật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                BangLuong bl = _bangluong.GetItem(_idbl);
                if (bl != null)
                {
                    // Chỉ cho phép sửa thưởng, khấu trừ và ghi chú
                    bl.Thuong = decimal.Parse(txtThuong.Text.Replace(",", ""));
                    bl.KhauTru = decimal.Parse(txtKhauTru.Text.Replace(",", ""));
                    bl.GhiChu = txtGhiChu.Text.Trim();

                    // Tính lại tổng lương
                    decimal luongCoBan = bl.LuongCoBan ?? 0;
                    decimal phuCap = bl.PhuCap ?? 0;
                    decimal thuong = bl.Thuong ?? 0;
                    decimal khauTru = bl.KhauTru ?? 0;

                    // Tính lương theo ngày công (26 ngày làm việc/tháng)
                    int ngayCong = bl.SoNgayCong ?? 0;
                    decimal luongTheoNgay = (luongCoBan / 26) * ngayCong;

                    bl.TongLuong = luongTheoNgay + phuCap + thuong - khauTru;

                    _bangluong.Update(bl);
                    XtraMessageBox.Show("Cập nhật thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadData((int)numThang.Value, (int)numNam.Value);
                    ShowHideControl(true);
                    EnableControl(false);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ShowHideControl(true);
            EnableControl(false);
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("BANGLUONG", "EDIT"))
            {
                XtraMessageBox.Show("Bạn không có quyền thanh toán lương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gvDanhSach.GetFocusedRowCellValue("IdBangLuong") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi cần thanh toán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string trangThai = gvDanhSach.GetFocusedRowCellValue("TrangThai").ToString();
            if (trangThai == "Đã thanh toán")
            {
                XtraMessageBox.Show("Bảng lương này đã được thanh toán!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idbl = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdBangLuong").ToString());
            string hoTen = gvDanhSach.GetFocusedRowCellValue("HoTen").ToString();
            string tongLuong = gvDanhSach.GetFocusedRowCellValue("TongLuong").ToString();

            if (XtraMessageBox.Show($"Xác nhận thanh toán lương cho: {hoTen}\nSố tiền: {tongLuong} VNĐ",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _bangluong.ThanhToan(idbl);
                    XtraMessageBox.Show("Thanh toán thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData((int)numThang.Value, (int)numNam.Value);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("BANGLUONG", "PRINT"))
            {
                XtraMessageBox.Show("Bạn không có quyền in bảng lương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gvDanhSach.GetFocusedRowCellValue("IdBangLuong") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi cần in!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idbl = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdBangLuong").ToString());
                // TODO: Implement print functionality
                XtraMessageBox.Show("Chức năng in đang được phát triển!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.GetFocusedRowCellValue("IdBangLuong") != null)
            {
                _idbl = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdBangLuong").ToString());
                var bl = _bangluong.GetItem(_idbl);
                if (bl != null)
                {
                    cboNhanVien.SelectedValue = bl.IdNV;
                    txtSoNgayCong.Text = bl.SoNgayCong.ToString();
                    txtSoGioLam.Text = bl.SoGioLam.HasValue ? bl.SoGioLam.Value.ToString("N2") : "0";
                    txtLuongCoBan.Text = bl.LuongCoBan.HasValue ? bl.LuongCoBan.Value.ToString("N0") : "0";
                    txtPhuCap.Text = bl.PhuCap.HasValue ? bl.PhuCap.Value.ToString("N0") : "0";
                    txtThuong.Text = bl.Thuong.HasValue ? bl.Thuong.Value.ToString("N0") : "0";
                    txtKhauTru.Text = bl.KhauTru.HasValue ? bl.KhauTru.Value.ToString("N0") : "0";
                    txtTongLuong.Text = bl.TongLuong.HasValue ? bl.TongLuong.Value.ToString("N0") : "0";
                    txtGhiChu.Text = bl.GhiChu;
                }
            }
        }

        private void TinhTongLuong(object sender, EventArgs e)
        {
            try
            {
                decimal luongCoBan = decimal.Parse(txtLuongCoBan.Text.Replace(",", ""));
                decimal phuCap = decimal.Parse(txtPhuCap.Text.Replace(",", ""));
                decimal thuong = decimal.Parse(txtThuong.Text.Replace(",", ""));
                decimal khauTru = decimal.Parse(txtKhauTru.Text.Replace(",", ""));
                int ngayCong = int.Parse(txtSoNgayCong.Text);

                // Tính lương theo ngày công (26 ngày làm việc/tháng)
                decimal luongTheoNgay = (luongCoBan / 26) * ngayCong;
                decimal tongLuong = luongTheoNgay + phuCap + thuong - khauTru;

                txtTongLuong.Text = tongLuong.ToString("N0");
            }
            catch
            {
                txtTongLuong.Text = "0";
            }
        }
    }
}