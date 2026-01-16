using BusinessLayer;
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

namespace THUEPHONG
{
    public partial class formLoaiPhong : Form
    {
        public formLoaiPhong()
        {
            InitializeComponent();
        }
        bool isAdding = false;
        bool isEditing = false;
        LOAIPHONG _LoaiPhong;
        private void formLoaiPhong_Load(object sender, EventArgs e)
        {
            _LoaiPhong = new LOAIPHONG();
            loadData();
            enableControls(false);
        }
        void enableControls(bool e)
        {
            txtIdPhong.Enabled = false;
            txtTen.Enabled = e;
            txtSoGiuong.Enabled = e;
            txtSoNguoi.Enabled = e;
            txtDonGia.Enabled = e;
            checkBox1.Enabled = e;
        }
        void reset()
        {
            txtIdPhong.Text = "";
            txtTen.Text = "";
            txtSoGiuong.Text = "";
            txtSoNguoi.Text = "";
            txtDonGia.Text = "";
            checkBox1.Checked = false;
        }
        void loadData()
        {
            dgvDS.DataSource = _LoaiPhong.getAll();

            dgvDS.ReadOnly = true;
            dgvDS.AutoGenerateColumns = false;
            dgvDS.Columns.Clear();

            dgvDS.Columns.Add("IdLoaiPhong", "ID Loại phòng");
            dgvDS.Columns["IdLoaiPhong"].DataPropertyName = "IdLoaiPhong";

            dgvDS.Columns.Add("TenLoaiPhong", "Tên loại phòng");
            dgvDS.Columns["TenLoaiPhong"].DataPropertyName = "TenLoaiPhong";

            dgvDS.Columns.Add("SoGiuong", "Số giường");
            dgvDS.Columns["SoGiuong"].DataPropertyName = "SoGiuong";

            dgvDS.Columns.Add("SoNguoi", "Số người");
            dgvDS.Columns["SoNguoi"].DataPropertyName = "SoNguoi";

            dgvDS.Columns.Add("DonGia", "Đơn giá");
            dgvDS.Columns["DonGia"].DataPropertyName = "DonGia";
        }

        private void dgvDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dgvDS.Rows[e.RowIndex].Cells["IdLoaiPhong"].Value);
                LoaiPhong lp = _LoaiPhong.getItem(id);

                if (lp != null)
                {
                    txtIdPhong.Text = lp.IdLoaiPhong.ToString();
                    txtTen.Text = lp.TenLoaiPhong;
                    txtSoGiuong.Text = lp.SoGiuong.ToString();
                    txtSoNguoi.Text = lp.SoNguoi.ToString();
                    txtDonGia.Text = lp.DonGia.ToString();
                    checkBox1.Checked = lp.Disabled ?? false;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            reset();
            txtIdPhong.Text = _LoaiPhong.getNewId().ToString();

            enableControls(true);
            txtTen.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtIdPhong.Text == "")
            {
                MessageBox.Show("Chọn loại phòng để sửa!");
                return;
            }

            isAdding = false;
            isEditing = true;

            enableControls(true);
            txtTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtIdPhong.Text == "")
            {
                MessageBox.Show("Chọn loại phòng để xóa!");
                return;
            }

            if (MessageBox.Show("Xóa loại phòng này?", "Xác nhận", MessageBoxButtons.YesNo)
                == DialogResult.Yes)
            {
                _LoaiPhong.delete(int.Parse(txtIdPhong.Text));
                MessageBox.Show("Đã xóa!");

                loadData();
                reset();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Tên loại phòng không được trống!");
                return;
            }

            LoaiPhong lp = new LoaiPhong
            {
                IdLoaiPhong = int.Parse(txtIdPhong.Text),
                TenLoaiPhong = txtTen.Text.Trim(),
                SoGiuong = int.Parse(txtSoGiuong.Text),
                SoNguoi = int.Parse(txtSoNguoi.Text),
                DonGia = int.Parse(txtDonGia.Text),
                Disabled = checkBox1.Checked 
            };

            if (isAdding)
            {
                _LoaiPhong.add(lp);
                MessageBox.Show("Thêm thành công!");
            }
            else if (isEditing)
            {
                _LoaiPhong.update(lp);
                MessageBox.Show("Sửa thành công!");
            }

            isAdding = false;
            isEditing = false;

            enableControls(false);
            loadData();
            reset();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            isAdding = false;
            isEditing = false;

            enableControls(false);
            reset();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim() == "")
            {
                loadData();
                return;
            }

            dgvDS.DataSource = _LoaiPhong.search(txtTimKiem.Text.Trim());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
