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
    public partial class formTang : Form
    { 
        public formTang()
        {
            InitializeComponent();
        }
        TANG _Tang;
        bool isAdding = false;
        bool isEditing = false;

        private void txtIdTang_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dgvTang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void formTang_Load(object sender, EventArgs e)
        {
            _Tang = new TANG();
            loadData();
        }
        void loadData()
        {
            dgvTang.DataSource = _Tang.getAll().Where(x => x.Disabled != true).ToList();
            dgvTang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvTang.ReadOnly = true;
            dgvTang.AutoGenerateColumns = false;
            dgvTang.Columns.Clear();

            dgvTang.Columns.Add("IdTang", "ID Tầng");
            dgvTang.Columns["IdTang"].DataPropertyName = "IdTang";

            dgvTang.Columns.Add("TenTang", "Tên Tầng");
            dgvTang.Columns["TenTang"].DataPropertyName = "TenTang";
        }

        private void dgvTang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idTang = Convert.ToInt32(dgvTang.Rows[e.RowIndex].Cells["IdTang"].Value);
                Tang t = _Tang.getItem(idTang);
                if (t != null)
                {
                    txtIdTang.Text = t.IdTang.ToString();
                    txtTenTang.Text = t.TenTang;
                    checkBoxDisabled.Checked = t.Disabled ?? false;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            txtIdTang.Text = _Tang.getNewId().ToString();
            txtIdTang.Enabled = false;

            txtTenTang.Text = "";
            txtTenTang.Enabled = true;
            checkBoxDisabled.Enabled = true;
            txtTenTang.Focus();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdTang.Text))
            {
                MessageBox.Show("Hãy chọn tầng cần sửa!", "Thông báo");
                return;
            }

            isAdding = false;
            isEditing = true;

            txtIdTang.Enabled = false;
            txtTenTang.Enabled = true;
            checkBoxDisabled.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIdTang.Text))
            {
                MessageBox.Show("Hãy chọn tầng cần xóa!", "Thông báo");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa tầng này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idTang = int.Parse(txtIdTang.Text);
                _Tang.delete(idTang);
                MessageBox.Show("Đã xóa (Disabled = true)!");
                loadData();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenTang.Text))
            {
                MessageBox.Show("Tên tầng không được để trống!", "Thông báo");
                txtTenTang.Focus();
                return;
            }

            Tang t = new Tang
            {
                IdTang = int.Parse(txtIdTang.Text),
                TenTang = txtTenTang.Text.Trim(),
                Disabled = checkBoxDisabled.Checked
            };

            if (isAdding)
            {
                _Tang.add(t);
                MessageBox.Show("Thêm tầng thành công!");
            }
            else if (isEditing)
            {
                _Tang.update(t);
                MessageBox.Show("Cập nhật tầng thành công!");
            }

            isAdding = false;
            isEditing = false;

            txtIdTang.Text = "";
            txtTenTang.Text = "";
            checkBoxDisabled.Checked = false;

            loadData();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            isAdding = false;
            isEditing = false;

            txtIdTang.Text = "";
            txtTenTang.Text = "";
            checkBoxDisabled.Checked = false;

            txtIdTang.Enabled = false;
            txtTenTang.Enabled = false;
            checkBoxDisabled.Enabled = false;

            loadData();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
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

            dgvTang.DataSource = _Tang.search(keyword);
            dgvTang.ClearSelection();
        }
    }
}
