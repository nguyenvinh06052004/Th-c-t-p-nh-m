using System;
using System.Linq;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;

namespace THUEPHONG
{
    public partial class formChamCong : DevExpress.XtraEditors.XtraForm
    {
        CHAMCONG _chamcong;
        NHANVIEN _nhanvien;
        bool _them;
        int _idcc;
        int _idnv;

        public formChamCong()
        {
            InitializeComponent();
        }

        private void formChamCong_Load(object sender, EventArgs e)
        {
            _chamcong = new CHAMCONG();
            _nhanvien = new NHANVIEN();

            // Set giá trị mặc định
            numThang.Value = DateTime.Now.Month;
            numNam.Value = DateTime.Now.Year;
            dtNgay.Value = DateTime.Now;
            dtGioVao.Value = DateTime.Today.AddHours(8); // 8:00
            dtGioRa.Value = DateTime.Today.AddHours(17); // 17:00

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
            DateTime from = new DateTime(nam, thang, 1);
            DateTime to = from.AddMonths(1);

            var data = _chamcong.GetAll()
                .Where(cc => cc.Ngay >= from && cc.Ngay < to)
                .ToList() // ⚠️ tách DB & UI
                .Select(cc => new
                {
                    cc.IdChamCong,
                    MaNV = cc.NhanVien != null ? cc.NhanVien.MaNV : "",
                    HoTen = cc.NhanVien != null ? cc.NhanVien.HoTen : "",
                    Ngay = cc.Ngay.ToString("dd/MM/yyyy"),
                    GioVao = cc.GioVao.HasValue ? cc.GioVao.Value.ToString(@"hh\:mm") : "",
                    GioRa = cc.GioRa.HasValue ? cc.GioRa.Value.ToString(@"hh\:mm") : "",
                    SoGioLam = cc.SoGioLam.HasValue ? cc.SoGioLam.Value.ToString("N2") : "0",
                    cc.GhiChu,
                    cc.IdNV
                })
                .ToList();

            gcDanhSach.DataSource = data;
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
            cboNhanVien.Enabled = t;
            dtNgay.Enabled = t;
            dtGioVao.Enabled = t;
            dtGioRa.Enabled = t;
            txtGhiChu.Enabled = t;
        }

        void Reset()
        {
            if (cboNhanVien.Items.Count > 0)
                cboNhanVien.SelectedIndex = 0;
            dtNgay.Value = DateTime.Now;
            dtGioVao.Value = DateTime.Today.AddHours(8);
            dtGioRa.Value = DateTime.Today.AddHours(17);
            txtGhiChu.Clear();
        }

        //private void btnXemChamCong_Click(object sender, EventArgs e)
        //{
        //    int thang = (int)numThang.Value;
        //    int nam = (int)numNam.Value;
        //    LoadData(thang, nam);
        //}

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CHAMCONG", "ADD"))
            {
                XtraMessageBox.Show("Bạn không có quyền thêm chấm công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = true;
            ShowHideControl(false);
            EnableControl(true);
            Reset();
            cboNhanVien.Focus();
        }


        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.GetFocusedRowCellValue("IdChamCong") != null)
            {
                _idcc = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdChamCong").ToString());
                var cc = _chamcong.GetAll().FirstOrDefault(c => c.IdChamCong == _idcc);
                if (cc != null)
                {
                    cboNhanVien.SelectedValue = cc.IdNV;
                    dtNgay.Value = cc.Ngay;

                    if (cc.GioVao.HasValue)
                        dtGioVao.Value = DateTime.Today.Add(cc.GioVao.Value);

                    if (cc.GioRa.HasValue)
                        dtGioRa.Value = DateTime.Today.Add(cc.GioRa.Value);

                    txtGhiChu.Text = cc.GhiChu;
                }
            }
        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            // Validate
            if (cboNhanVien.SelectedValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNhanVien.Focus();
                return;
            }

            if (dtGioRa.Value <= dtGioVao.Value)
            {
                XtraMessageBox.Show("Giờ ra phải sau giờ vào!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtGioRa.Focus();
                return;
            }

            try
            {
                if (_them)
                {
                    // Kiểm tra đã chấm công ngày này chưa
                    int idnv = int.Parse(cboNhanVien.SelectedValue.ToString());
                    var existing = _chamcong.GetByDate(idnv, dtNgay.Value.Date);

                    if (existing != null)
                    {
                        XtraMessageBox.Show("Nhân viên này đã được chấm công trong ngày này!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    ChamCong cc = new ChamCong
                    {
                        IdNV = idnv,
                        Ngay = dtNgay.Value.Date,
                        GioVao = dtGioVao.Value.TimeOfDay,
                        GioRa = dtGioRa.Value.TimeOfDay,
                        GhiChu = txtGhiChu.Text.Trim()
                    };

                    _chamcong.Add(cc);
                    XtraMessageBox.Show("Thêm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ChamCong cc = _chamcong.GetAll().FirstOrDefault(c => c.IdChamCong == _idcc);
                    if (cc != null)
                    {
                        cc.GioVao = dtGioVao.Value.TimeOfDay;
                        cc.GioRa = dtGioRa.Value.TimeOfDay;
                        cc.GhiChu = txtGhiChu.Text.Trim();

                        _chamcong.Update(cc);
                        XtraMessageBox.Show("Cập nhật thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                LoadData((int)numThang.Value, (int)numNam.Value);
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

        private void btnSua_Click_1(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CHAMCONG", "EDIT"))
            {
                XtraMessageBox.Show("Bạn không có quyền sửa chấm công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gvDanhSach.GetFocusedRowCellValue("IdChamCong") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = false;
            _idcc = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdChamCong").ToString());
            ShowHideControl(false);
            EnableControl(true);

            var cc = _chamcong.GetAll().FirstOrDefault(c => c.IdChamCong == _idcc);
            if (cc != null)
            {
                cboNhanVien.SelectedValue = cc.IdNV;
                dtNgay.Value = cc.Ngay;
                dtGioVao.Value = DateTime.Today.Add(cc.GioVao ?? TimeSpan.Zero);
                dtGioRa.Value = DateTime.Today.Add(cc.GioRa ?? TimeSpan.Zero);
                txtGhiChu.Text = cc.GhiChu;
            }
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            if (!UserSession.HasPermission("CHAMCONG", "DELETE"))
            {
                XtraMessageBox.Show("Bạn không có quyền xóa chấm công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (gvDanhSach.GetFocusedRowCellValue("IdChamCong") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn bản ghi cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idcc = int.Parse(gvDanhSach.GetFocusedRowCellValue("IdChamCong").ToString());

            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _chamcong.Delete(idcc);
                    XtraMessageBox.Show("Xóa thành công!", "Thông báo",
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

        private void btnBoQua_Click_1(object sender, EventArgs e)
        {
            _them = false;
            ShowHideControl(true);
            EnableControl(false);
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemChamCong_Click_1(object sender, EventArgs e)
        {
            int thang = (int)numThang.Value;
            int nam = (int)numNam.Value;
            LoadData(thang, nam);
        }
    }
}