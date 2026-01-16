using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THUEPHONG
{
    public partial class formPhong : DevExpress.XtraEditors.XtraForm
    {
        public formPhong()
        {
            InitializeComponent();
        }
        string _tenAnh = "";

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            btnThem.Visible = true;
            btnXoa.Visible = true;
            btnSua.Visible = true;
            // Hủy trạng thái Thêm/Sửa
            isAdding = false;
            isEditing = false;

            // Reset form: xóa các textbox và combobox
            txtIdPhong.Text = "";
            txtTen.Text = "";
            cbTrangThai.SelectedIndex = 0;
            cbIdTang.SelectedIndex = 0;
            cbLoaiPhong.SelectedIndex = 0;
            checkBox1.Checked = false;

            // Khóa các ô nhập liệu, chỉ xem danh sách
            txtIdPhong.Enabled = false;
            txtTen.Enabled = false;
            cbTrangThai.Enabled = false;
            cbIdTang.Enabled = false;
            cbLoaiPhong.Enabled = false;
            checkBox1.Enabled = false;

            // Reload DataGridView
            loadData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtIdPhong_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        PHONG _Phong ;
        TANG _Tang;
        LOAIPHONG _LoaiPhong;
        private void formPhong_Load(object sender, EventArgs e)
        {
            _Phong = new PHONG();
            _Tang = new TANG();
            _LoaiPhong = new LOAIPHONG();
            loadData();
            loadCombobox();

            picPhong.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            picPhong.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Never;
            picPhong.Properties.ReadOnly = true;
            picPhong.Image = null;

        }

        void loadCombobox()
        {
            // Load tầng
            cbIdTang.DataSource = _Tang.getAll();
            cbIdTang.DisplayMember = "TenTang";   // cột hiển thị
            cbIdTang.ValueMember = "IdTang";      // cột giá trị

            // Load loại phòng
            cbLoaiPhong.DataSource = _LoaiPhong.getAll();
            cbLoaiPhong.DisplayMember = "TenLoaiPhong";
            cbLoaiPhong.ValueMember = "IdLoaiPhong";

            // Trạng thái (nếu trong DB là bit / bool)
            cbTrangThai.DataSource = new[]
            {
                new { Text = "Trống", Value = "0" },
                new { Text = "Đang sử dụng", Value = "1" }
            };
                        cbTrangThai.DisplayMember = "Text";
                        cbTrangThai.ValueMember = "Value";


        }

        void loadData()
        {
            dgvDS.DataSource = _Phong.getAll().Where(x => x.Disabled!=true).ToList(); //disable la true thi phong k hoat dong, hieu la da bi xoa va khong hien thi len danh sach
            //dgvDS.DataSource = _Phong.getAll(); //disable la true thi phong k hoat dong, hieu la da bi xoa va khong hien thi len danh sach
            dgvDS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            dgvDS.ReadOnly = true;
            dgvDS.AutoGenerateColumns = false;
            dgvDS.Columns.Clear();

            dgvDS.Columns.Add("IdPhong", "Id Phòng");
            dgvDS.Columns["IdPhong"].DataPropertyName = "IdPhong";

            dgvDS.Columns.Add("TenPhong", "Tên Phòng");
            dgvDS.Columns["TenPhong"].DataPropertyName = "TenPhong";

            dgvDS.Columns.Add("TrangThai", "Trạng Thái");
            dgvDS.Columns["TrangThai"].DataPropertyName = "TrangThai";

            dgvDS.Columns.Add("IdTang", "Id Tầng");
            dgvDS.Columns["IdTang"].DataPropertyName = "IdTang";

            dgvDS.Columns.Add("IdLoaiPhong", "Id Loại Phòng");
            dgvDS.Columns["IdLoaiPhong"].DataPropertyName = "IdLoaiPhong";

            //dgvDS.Columns.Add("Disabled", "Disabled");
            //dgvDS.Columns["Disabled"].DataPropertyName = "Disabled";
        }


        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idPhong = Convert.ToInt32(dgvDS.Rows[e.RowIndex].Cells["IdPhong"].Value);
                Phong p = _Phong.getItem(idPhong); // Lấy trực tiếp từ DB

                if (p != null)
                {
                    txtIdPhong.Text = p.IdPhong.ToString();
                    txtTen.Text = p.TenPhong;

                    // Chuyển string sang bool trước khi gán
                    cbTrangThai.SelectedValue = (p.TrangThai) ?? "0";  // hoặc (p.TrangThai == "1")

                    cbIdTang.SelectedValue = p.Tang.IdTang;
                    cbLoaiPhong.SelectedValue = p.LoaiPhong.IdLoaiPhong;
                    checkBox1.Checked = p.Disabled ?? false ;

                    // 🔥 LOAD ẢNH
                    LoadHinhPhong(p.HinhAnh);
                    _tenAnh = ""; // reset tránh ghi đè
                }
            }
        }

        private void cbTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
        
        }
        bool isAdding = false;
        bool isEditing = false;

        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnThem.Visible = true;
            btnXoa.Visible = true;
            btnSua.Visible = true;
            try
            {
                // Kiểm tra dữ liệu hợp lệ
                if (string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show("Tên phòng không được để trống!", "Thông báo");
                    txtTen.Focus();
                    return;
                }

                if (cbTrangThai.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn trạng thái!", "Thông báo");
                    cbTrangThai.Focus();
                    return;
                }

                if (cbIdTang.SelectedValue == null || cbLoaiPhong.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn tầng và loại phòng!", "Thông báo");
                    return;
                }

                // Tạo đối tượng Phong mới
                Phong p = new Phong
                {
                    IdPhong = int.Parse(txtIdPhong.Text),
                    TenPhong = txtTen.Text.Trim(),
                    TrangThai = cbTrangThai.SelectedValue.ToString(),
                    IdTang = Convert.ToInt32(cbIdTang.SelectedValue),
                    IdLoaiPhong = Convert.ToInt32(cbLoaiPhong.SelectedValue),
                    Disabled = checkBox1.Checked, // luôn true hoặc false
                    HinhAnh = _tenAnh
                };

                // Thêm mới hoặc cập nhật
                if (isAdding)
                {
                    _Phong.add(p);
                    MessageBox.Show("Thêm phòng thành công!");
                }
                else if (isEditing)
                {
                    _Phong.update(p);
                    MessageBox.Show("Cập nhật phòng thành công!");
                }

                isAdding = false;
                isEditing = false;

                // Reset form
                txtIdPhong.Text = "";
                txtTen.Text = "";
                cbTrangThai.SelectedIndex = 0;
                cbIdTang.SelectedIndex = 0;
                cbLoaiPhong.SelectedIndex = 0;
                checkBox1.Checked = false;

                // Load lại dữ liệu
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi");
            }
            btnChonAnh.Visible = false;
            picPhong.Visible = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnChonAnh.Visible = true;
            picPhong.Visible = false;
            btnThem.Visible = false;
            btnXoa.Visible = false;
            btnSua.Visible = false;

            isAdding = true;
            isEditing = false;

            // Reset form
            txtIdPhong.Text = _Phong.getNewId().ToString();
            txtTen.Text = "";
            cbTrangThai.SelectedIndex = 0;
            cbIdTang.SelectedIndex = 0;
            cbLoaiPhong.SelectedIndex = 0;
            checkBox1.Checked = false;

            // Khóa ID
            txtIdPhong.Enabled = false;

            // Mở nhập liệu
            txtTen.Enabled = true;
            cbTrangThai.Enabled = true;
            cbIdTang.Enabled = true;
            cbLoaiPhong.Enabled = true;
            checkBox1.Enabled = true;

            txtTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnThem.Visible = false;
            btnXoa.Visible = false;
            btnSua.Visible = false;
            if (string.IsNullOrEmpty(txtIdPhong.Text))
            {
                MessageBox.Show("Hãy chọn phòng cần sửa!", "Thông báo");
                return;
            }

            isAdding = false;
            isEditing = true;

            // Không cho sửa ID
            txtIdPhong.Enabled = false;

            // Cho phép chỉnh sửa các trường
            txtTen.Enabled = true;
            cbTrangThai.Enabled = true;
            cbIdTang.Enabled = true;
            cbLoaiPhong.Enabled = true;
            checkBox1.Enabled = true;

            txtTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdPhong.Text))
            {
                MessageBox.Show("Hãy chọn phòng cần xóa!", "Thông báo");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa phòng này?",
                                "Xác nhận",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idPhong = int.Parse(txtIdPhong.Text);
                _Phong.delete(idPhong);

                MessageBox.Show("Đã xóa (đánh dấu Disabled)!");
                loadData();
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            // Nếu textbox trống, hiển thị tất cả dữ liệu
            if (string.IsNullOrEmpty(keyword))
            {
                loadData();
                return;
            }

            // Gọi hàm tìm kiếm nâng cao
            var result = _Phong.search(keyword);

            // Hiển thị kết quả vào DataGridView
            dgvDS.DataSource = result;

            // Xóa selection nếu muốn
            dgvDS.ClearSelection();
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Hình ảnh (*.jpg;*.png)|*.jpg;*.png";
            dlg.Title = "Chọn hình phòng";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // 🔥 LƯU NGUYÊN ĐƯỜNG DẪN Ổ ĐĨA
                _tenAnh = dlg.FileName;

                // Load ảnh lên PictureEdit
                picPhong.Image = Image.FromFile(_tenAnh);
                picPhong.Visible = true;
                btnChonAnh.Visible = false;
            }
        }

        void LoadHinhPhong(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                picPhong.Image = null;
                return;
            }

            if (File.Exists(path))
                picPhong.LoadAsync(path);
            else
                picPhong.Image = null;
        }

    }
}