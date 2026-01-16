using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Linq;
using System.Windows.Forms;
//using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace THUEPHONG
{
    public partial class formUsers : DevExpress.XtraEditors.XtraForm
    {
        USERS _user;
        ROLE _role;
        bool _them;
        int _uid;

        public formUsers()
        {
            InitializeComponent();
        }

        private void formUsers_Load(object sender, EventArgs e)
        {
            _user = new USERS();
            _role = new ROLE();

            LoadRole();
            LoadData();
            ShowHideControl(true);
            EnableControl(false);
        }

        void LoadRole()
        {
            var roles = _role.GetAll();
            cboRole.DataSource = roles;
            cboRole.DisplayMember = "RoleName";
            cboRole.ValueMember = "IdRole";
        }

        void LoadData()
        {
            var data = from u in _user.GetAll()
                       join r in _role.GetAll()
                       on u.IdRole equals r.IdRole into ur
                       from r in ur.DefaultIfEmpty()
                       select new
                       {
                           u.UId,
                           u.Username,
                           u.FullName,
                           u.Email,
                           u.Phone,
                           RoleName = r != null ? r.RoleName : "",
                           u.IsActive,
                           u.IdRole
                       };

            gcDanhSach.DataSource = data.ToList();
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
            txtUsername.Enabled = t;
            txtPassword.Enabled = t;
            txtFullName.Enabled = t;
            txtEmail.Enabled = t;
            txtPhone.Enabled = t;
            cboRole.Enabled = t;
            chkIsActive.Enabled = t;
        }

        void Reset()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            chkIsActive.Checked = true;
            if (cboRole.Items.Count > 0)
                cboRole.SelectedIndex = 0;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //if (!UserSession.HasPermission("USERS", "ADD"))
            //{
            //    XtraMessageBox.Show("Bạn không có quyền thêm user!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            _them = true;
            ShowHideControl(false);
            EnableControl(true);
            Reset();
            txtUsername.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //if (!UserSession.HasPermission("USERS", "EDIT"))
            //{
            //    XtraMessageBox.Show("Bạn không có quyền sửa user!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (gvDanhSach.GetFocusedRowCellValue("UId") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn user cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _them = false;
            _uid = int.Parse(gvDanhSach.GetFocusedRowCellValue("UId").ToString());
            ShowHideControl(false);
            EnableControl(true);
            txtUsername.Enabled = false; // Không cho sửa username

            var user = _user.GetItem(_uid);
            if (user != null)
            {
                txtUsername.Text = user.Username;
                txtPassword.Clear(); // Để trống, người dùng nhập nếu muốn đổi
                txtFullName.Text = user.FullName;
                txtEmail.Text = user.Email;
                txtPhone.Text = user.Phone;
               // cboRole.SelectedValue = user.IdRole;
                chkIsActive.Checked = user.IsActive ?? true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //if (!UserSession.HasPermission("USERS", "DELETE"))
            //{
            //    XtraMessageBox.Show("Bạn không có quyền xóa user!", "Thông báo",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            if (gvDanhSach.GetFocusedRowCellValue("UId") == null)
            {
                XtraMessageBox.Show("Vui lòng chọn user cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int uid = int.Parse(gvDanhSach.GetFocusedRowCellValue("UId").ToString());

            // Không cho xóa chinh minh
            if (uid == UserSession.UserId)
            {
                XtraMessageBox.Show("Bạn không thể xóa chính mình!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (XtraMessageBox.Show("Bạn có chắc chắn muốn xóa user này?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    _user.Delete(uid);
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
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (_them && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                XtraMessageBox.Show("Vui lòng nhập họ tên!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (cboRole.SelectedValue == null)
            {
                XtraMessageBox.Show("Vui lòng chọn vai trò!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboRole.Focus();
                return;
            }

            try
            {
                if (_them)
                {
                    // Kiểm tra username đã tồn tại
                    if (_user.IsUsernameExists(txtUsername.Text))
                    {
                        XtraMessageBox.Show("Tên đăng nhập đã tồn tại!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtUsername.Focus();
                        return;
                    }

                    User user = new User
                    {
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Text,
                        FullName = txtFullName.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        IdRole = int.Parse(cboRole.SelectedValue.ToString()),
                        IsActive = chkIsActive.Checked,
                        MaCTY = UserSession.MaCTY,
                        MaDVI = UserSession.MaDVI
                    };

                    _user.Add(user);
                    XtraMessageBox.Show("Thêm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    User user = _user.GetItem(_uid);
                    if (user != null)
                    {
                        user.FullName = txtFullName.Text.Trim();
                        user.Email = txtEmail.Text.Trim();
                        user.Phone = txtPhone.Text.Trim();
                        user.IdRole = int.Parse(cboRole.SelectedValue.ToString());
                        user.IsActive = chkIsActive.Checked;

                        // Chỉ cập nhật mật khẩu nếu có nhập
                        if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                        {
                            user.Password = txtPassword.Text;
                        }

                        _user.Update(user);
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
            if (gvDanhSach.GetFocusedRowCellValue("UId") != null)
            {
                _uid = int.Parse(gvDanhSach.GetFocusedRowCellValue("UId").ToString());
                var user = _user.GetItem(_uid);
                if (user != null)
                {
                    txtUsername.Text = user.Username;
                    txtPassword.Clear();
                    txtFullName.Text = user.FullName;
                    txtEmail.Text = user.Email;
                    txtPhone.Text = user.Phone;
                    //cboRole.SelectedValue = user.IdRole;
                    //if (user.IdRole != null)
                    //{
                    //    cboRole.SelectedValue = user.IdRole;
                    //}
                    //else
                    //{
                    //    cboRole.SelectedIndex = -1; // hoặc chọn role mặc định
                    //}

                    chkIsActive.Checked = user.IsActive ?? true;
                }
            }
        }
    }
}