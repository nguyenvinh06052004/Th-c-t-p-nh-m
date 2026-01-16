using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THUEPHONG
{
    public partial class frmKetNoi : DevExpress.XtraEditors.XtraForm
    {
        public frmKetNoi()
        {
            InitializeComponent();
        }
        SqlConnection GetCon(string server, string user, string pass, string database)
        {
            SqlConnection con = new SqlConnection("Data Source=" + server + ";Initial Catalog=" + database + ";User ID=" + user + ";Password=" + pass + "");
            return con;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnKiemTra_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetCon(txtServer.Text, txtUsername.Text, txtPassword.Text, cboDatabase.Text);
            try
            {
                con.Open();
                MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                //   btnLuu.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kết nối thất bại!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cboDatabase_MouseClick(object sender, MouseEventArgs e)
        {
            cboDatabase.Items.Clear();
            string conn = "Data Source=" + txtServer.Text + ";User ID=" + txtUsername.Text + ";Password=" + txtPassword.Text + "";
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            string sql = "SELECT name FROM sys.databases";
            SqlCommand cmd = new SqlCommand(sql, con);
            IDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cboDatabase.Items.Add(dr[0].ToString());

            }
        }
    }
}