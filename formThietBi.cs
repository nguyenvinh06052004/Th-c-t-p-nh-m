using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using BusinessLayer;

namespace THUEPHONG
{
    public partial class formThietBi : Form
    {
        THIETBI _TB;
        bool isAdding = false;
        bool isEditing = false;
        public formThietBi()
        {
            InitializeComponent();
        }

        private void formThietBi_Load(object sender, EventArgs e)
        {
            _TB = new THIETBI();
            loadData();
        }
        // ================== LOAD GRID ==================
        void loadData()
        {
            dgvDS.DataSource = _TB.getAll().Where(x => x.Disabled != true).ToList();
            dgvDS.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDS.ReadOnly = true;
            dgvDS.AutoGenerateColumns = false;
            dgvDS.Columns.Clear();

            dgvDS.Columns.Add("IdTB", "Id Thiết Bị");
            dgvDS.Columns["IdTB"].DataPropertyName = "IdTB";

            dgvDS.Columns.Add("TenTB", "Tên Thiết Bị");
            dgvDS.Columns["TenTB"].DataPropertyName = "TenTB";

            dgvDS.Columns.Add("DonGia", "Đơn Giá");
            dgvDS.Columns["DonGia"].DataPropertyName = "DonGia";

            // Không hiển thị Disabled
        }

        private void dgvDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dgvDS.Rows[e.RowIndex].Cells["IdTB"].Value);
                var item = _TB.getItem(id);

                if (item != null)
                {
                    txtIdTB.Text = item.IdTB.ToString();
                    txtTen.Text = item.TenTB;
                    textBox1.Text = item.DonGia.ToString();
                    checkBox1.Checked = item.Disabled ?? false;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            txtIdTB.Enabled = false;

            txtIdTB.Text = _TB.getNewId().ToString();
            txtTen.Text = "";
            textBox1.Text = "";
            checkBox1.Checked = false;

            txtTen.Enabled = true;
            textBox1.Enabled = true;
            checkBox1.Enabled = true;

            txtTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdTB.Text))
            {
                MessageBox.Show("Hãy chọn thiết bị cần sửa!");
                return;
            }

            isAdding = false;
            isEditing = true;

            txtIdTB.Enabled = false;

            txtTen.Enabled = true;
            textBox1.Enabled = true;
            checkBox1.Enabled = true;

            txtTen.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show("Tên thiết bị không được trống!");
                    txtTen.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Đơn giá không được trống!");
                    textBox1.Focus();
                    return;
                }

                ThietBi t = new ThietBi
                {
                    IdTB = int.Parse(txtIdTB.Text),
                    TenTB = txtTen.Text.Trim(),
                    DonGia = decimal.Parse(textBox1.Text),
                    Disabled = checkBox1.Checked
                };

                if (isAdding)
                {
                    _TB.add(t);
                    MessageBox.Show("Thêm thiết bị thành công!");
                }
                else if (isEditing)
                {
                    _TB.update(t);
                    MessageBox.Show("Cập nhật thiết bị thành công!");
                }

                isAdding = false;
                isEditing = false;

                resetForm();
                loadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdTB.Text))
            {
                MessageBox.Show("Hãy chọn thiết bị cần xoá!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xoá thiết bị này?",
                "Xác nhận", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _TB.delete(int.Parse(txtIdTB.Text));
                MessageBox.Show("Đã xoá (Disabled = true)");
                loadData();
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            isAdding = false;
            isEditing = false;

            resetForm();
            loadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                loadData();
                return;
            }

            dgvDS.DataSource = _TB.search(keyword);
        }
        void resetForm()
        {
            txtIdTB.Enabled = false;
            txtTen.Enabled = false;
            textBox1.Enabled = false;
            checkBox1.Enabled = false;

            txtIdTB.Text = "";
            txtTen.Text = "";
            textBox1.Text = "";
            checkBox1.Checked = false;
        }
    }
}
