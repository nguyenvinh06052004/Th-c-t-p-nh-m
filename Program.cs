using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using DataLayer;
namespace THUEPHONG
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // --- KIỂM TRA VÀ TẠO FILE KẾT NỐI ---
            connect cn = new connect();
            if (!System.IO.File.Exists("connectdb.dba"))
            {
                // Tạo file kết nối lần đầu (giá trị mẫu)
                //cn.ConnectData("localhost", "KHACHSAN", "sa", "123");
                cn.Servername = @"INVVV\SQLEXPRESS";
                cn.Database = "QUAN LI KHACH SAN";
                cn.ConnectData();

            }
            // ------------------------------------

           // Application.Run(new frmMain());

            // Hiển thị form đăng nhập
            frmLogin loginForm = new frmLogin();

            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Đăng nhập thành công, mở form chính
                Application.Run(new frmMain());
            }
            else
            {
                // Đăng nhập thất bại hoặc thoát
                Application.Exit();
            }
        }
    }
}
