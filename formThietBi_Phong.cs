using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THUEPHONG
{
    public partial class formThietBi_Phong : Form
    {
        public formThietBi_Phong()
        {
            InitializeComponent();
        }
        THIETBI_PHONG _PTB;
        PHONG _Phong;
        THIETBI _ThietBi;

        bool isAdding = false;
        bool isEditing = false;
        private void formThietBi_Phong_Load(object sender, EventArgs e)
        {
            _PTB = new THIETBI_PHONG();
            _Phong = new PHONG();
            _ThietBi = new THIETBI();

            loadCombo();
            loadData();
            enableControls(false);
        }
        void loadCombo()
        {
            cbIdPhong.DataSource = _Phong.getAll();
            cbIdPhong.DisplayMember = "TenPhong";
            cbIdPhong.ValueMember = "IdPhong";

            cbIdThietBi.DataSource = _ThietBi.getAll();
            cbIdThietBi.DisplayMember = "TenThietBi";
            cbIdThietBi.ValueMember = "IdTB";
        }

        void enableControls(bool t)
        {
            cbIdPhong.Enabled = t && isAdding; // chỉ cho đổi phòng khi thêm
            cbIdThietBi.Enabled = t && isAdding; // không cho đổi khi sửa
            txtSoLuong.Enabled = t;
            checkBox1.Enabled = t;
        }

        void reset()
        {
            txtSoLuong.Text = "";
            checkBox1.Checked = false;
        }
        void loadData()
        {
            var list = _PTB.getAll();
            dgvDS.DataSource = BuildDataTable(list);

            dgvDS.Columns["Id Phong"].Visible = false;
            dgvDS.Columns["Id Thiết Bị"].Visible = false;
        }
        DataTable BuildDataTable(List<Phong_ThietBi> list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Id Phong", typeof(int));
            dt.Columns.Add("Id Thiết Bị", typeof(int));
            dt.Columns.Add("Số lượng", typeof(int));
            dt.Columns.Add("Disabled", typeof(bool));
            dt.Columns.Add("Tên phòng", typeof(string));
            dt.Columns.Add("Tên thiết bị", typeof(string));

            foreach (var item in list)
            {
                dt.Rows.Add(item.IdPhong,
                            item.IdThietBi,
                            item.SoLuong ?? 0,
                            item.Disabled ?? false,
                            item.Phong?.TenPhong ?? "",
                            item.ThietBi?.TenTB ?? "");
            }

            return dt;
        }

        void loadData1()
        {
            //dgvDS.DataSource = _PTB.getAll();
            dgvDS.AutoGenerateColumns = true;
            dgvDS.ReadOnly = true;
            var list = _PTB.getAll();

            // Tạo DataTable tạm
            DataTable dt = new DataTable();
            dt.Columns.Add("Id Phong", typeof(int));
            dt.Columns.Add("Id Thiết Bị", typeof(int));
            dt.Columns.Add("Số lượng", typeof(int));
            dt.Columns.Add("Disabled", typeof(bool));
            dt.Columns.Add("Tên phòng", typeof(string));
            dt.Columns.Add("Tên thiết bị", typeof(string));

            foreach (var item in list)
            {
                dt.Rows.Add(item.IdPhong, item.IdThietBi, item.SoLuong ?? 0,
                            item.Disabled ?? false,
                            item.Phong?.TenPhong ?? "",
                            item.ThietBi?.TenTB ?? "");
            }

            dgvDS.DataSource = dt;

            dgvDS.ReadOnly = true;
            dgvDS.Columns["Id Phong"].Visible = false;
            dgvDS.Columns["Id Thiết Bị"].Visible = false;
        }

        private void dgvDS_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idPhong = Convert.ToInt32(dgvDS.Rows[e.RowIndex].Cells["Id Phong"].Value);
                int idTB = Convert.ToInt32(dgvDS.Rows[e.RowIndex].Cells["Id Thiết Bị"].Value);

                Phong_ThietBi ptb = _PTB.getItem(idPhong, idTB);

                if (ptb != null)
                {
                    cbIdPhong.SelectedValue = ptb.IdPhong;
                    cbIdThietBi.SelectedValue = ptb.IdThietBi;
                    txtSoLuong.Text = ptb.SoLuong.ToString();
                    checkBox1.Checked = ptb.Disabled ?? false;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            reset();
            enableControls(true);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Chọn dữ liệu để sửa!");
                return;
            }

            isAdding = false;
            isEditing = true;

            enableControls(true);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int idPhong = (int)cbIdPhong.SelectedValue;
            int idTB = (int)cbIdThietBi.SelectedValue;

            if (MessageBox.Show("Xóa thiết bị khỏi phòng?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _PTB.delete(idPhong, idTB);
                loadData();
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            Phong_ThietBi ptb = new Phong_ThietBi
            {
                IdPhong = (int)cbIdPhong.SelectedValue,
                IdThietBi = (int)cbIdThietBi.SelectedValue,
                SoLuong = int.Parse(txtSoLuong.Text),
                Disabled = checkBox1.Checked
            };

            if (isAdding)
                _PTB.add(ptb);
            else if (isEditing)
                _PTB.update(ptb);
            loadCombo();
            loadData();
            enableControls(false);
            reset();
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            enableControls(false);
            reset();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                loadData();
            }
            else
            {
                var list = _PTB.search(txtTimKiem.Text.Trim());
                dgvDS.DataSource = BuildDataTable(list);

                dgvDS.Columns["Id Phong"].Visible = false;
                dgvDS.Columns["Id Thiết Bị"].Visible = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
