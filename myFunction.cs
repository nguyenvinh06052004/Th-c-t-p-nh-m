using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THUEPHONG
{
    public class myFunctions
    {
        public static string _srv;
        public static string _db;
        static SqlConnection con = new SqlConnection();
        public static void taoketnoi()
        {
            _srv = @"INVVV\SQLEXPRESS";
            _db = "QUAN LI KHACH SAN";
            con.ConnectionString = $"Server={_srv};Database={_db};Integrated Security=True;";
            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối SQL Server: " + ex.Message);
            }
        }

        public static void dongketnoi()
        {
            con.Close();
        }

        public static DataTable laydulieu(string qr)
        {
            taoketnoi();
            DataTable datatbl = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand(qr, con);
            dap.Fill(datatbl);
            dongketnoi();
            return datatbl;
        }

        public static DateTime GetFirstDayInMonth(int year, int month)
        {
            return new DateTime(year, month, 1);
        }
        //public static DataTable laydulieu(string sql)
        //{
        //    DataTable dt = new DataTable();
        //    using (SqlConnection conn = new SqlConnection(DataLayer.Entities.GetConnectionString()))
        //    {
        //        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        //        da.Fill(dt);
        //    }
        //    return dt;
        //}

        //public static DateTime GetFirstDayInMonth(int year, int month)
        //{
        //    return new DateTime(year, month, 1);
        //}
        private static string DocBaSo(int so, string[] chuSo)
        {
            int tram = so / 100;
            int chuc = (so % 100) / 10;
            int donvi = so % 10;

            string ketQua = "";

            // Trăm
            if (tram > 0)
            {
                ketQua += chuSo[tram] + " trăm";
            }

            // Chục
            if (chuc > 1)
            {
                ketQua += " " + chuSo[chuc] + " mươi";

                if (donvi == 1)
                    ketQua += " mốt";
                else if (donvi == 5)
                    ketQua += " lăm";
                else if (donvi > 0)
                    ketQua += " " + chuSo[donvi];
            }
            else if (chuc == 1)
            {
                ketQua += " mười";

                if (donvi == 5)
                    ketQua += " lăm";
                else if (donvi > 0)
                    ketQua += " " + chuSo[donvi];
            }
            else if (chuc == 0 && donvi > 0)
            {
                if (tram > 0)
                    ketQua += " lẻ";

                ketQua += " " + chuSo[donvi];
            }

            return ketQua.Trim();
        }
        public static string ChuyenSoThanhChu(decimal so)
        {
            if (so == 0)
                return "Không đồng";

            string[] chuSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string[] donViNhom = { "", "nghìn", "triệu", "tỷ", "nghìn tỷ", "triệu tỷ" };

            long soNguyen = (long)Math.Floor(so); // ❗ KHÔNG Round
            int nhom = 0;
            string ketQua = "";

            while (soNguyen > 0)
            {
                int baSo = (int)(soNguyen % 1000);

                if (baSo > 0)
                {
                    string docBaSo = DocBaSo(baSo, chuSo);
                    ketQua = docBaSo + " " + donViNhom[nhom] + " " + ketQua;
                }

                soNguyen /= 1000;
                nhom++;
            }

            ketQua = ketQua.Trim();
            ketQua = char.ToUpper(ketQua[0]) + ketQua.Substring(1);

            return ketQua + " đồng";
        }
        public static decimal NormalizeMoney(decimal? value)
        {
            if (value == null) return 0;

            // Nếu < 1000 thì coi là đang lưu dạng xxx.500 → nhân 1000
            if (value < 1000)
                return Math.Round(value.Value * 1000, 0);

            return Math.Round(value.Value, 0);
        }


    }
}