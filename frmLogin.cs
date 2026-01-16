using System;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;
using System.Configuration;
namespace THUEPHONG
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        USERS _user;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _user = new USERS();

            // Đọc thông tin đăng nhập đã lưu (nếu có)
            if (Properties.Settings.Default.Properties["RememberMe"] != null &&
    (bool)(Properties.Settings.Default["RememberMe"] ?? false))
            {
                txtUsername.Text = Properties.Settings.Default["Username"] as string ?? "";
                chkRemember.Checked = true;
            }

            // Example fix in PerformLogin:
            if (chkRemember.Checked)
            {
                Properties.Settings.Default["Username"] = txtUsername.Text;
                Properties.Settings.Default["RememberMe"] = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default["Username"] = "";
                Properties.Settings.Default["RememberMe"] = false;
                Properties.Settings.Default.Save();
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            // Validate
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                // Đăng nhập
                var user = _user.Login(txtUsername.Text.Trim(), txtPassword.Text);

                if (user != null)
                {
                    // Lưu thông tin đăng nhập nếu chọn "Ghi nhớ"
                    if (chkRemember.Checked)
                    {
                        Properties.Settings.Default.Username = txtUsername.Text;
                        Properties.Settings.Default.RememberMe = true;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.Username = "";
                        Properties.Settings.Default.RememberMe = false;
                        Properties.Settings.Default.Save();
                    }

                    // Lưu thông tin user vào session
                    UserSession.UserId = user.UId;
                    UserSession.Username = user.Username;
                    UserSession.FullName = user.FullName;
                    UserSession.RoleId = user.IdRole ?? 0;
                    UserSession.MaCTY = user.MaCTY;
                    UserSession.MaDVI = user.MaDVI;

                    // Lấy tên role
                    if (user.IdRole.HasValue)
                    {
                        ROLE _role = new ROLE();
                        var role = _role.GetItem(user.IdRole.Value);
                        UserSession.RoleName = role?.RoleName ?? "";
                    }

                    MessageBox.Show("Đăng nhập thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}