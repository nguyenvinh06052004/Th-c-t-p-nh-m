using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public static class UserSession
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string FullName { get; set; }
        public static int RoleId { get; set; }
        public static string RoleName { get; set; }
        public static string MaCTY { get; set; }
        public static string MaDVI { get; set; }

    // Kiểm tra quyền
    public static bool HasPermission(string funcCode, string action)
    {
        // action: "VIEW", "ADD", "EDIT", "DELETE", "PRINT"

        BusinessLayer.ROLEPERMISSION _perm = new BusinessLayer.ROLEPERMISSION();
        var permission = _perm.GetPermission(RoleId, funcCode);

        if (permission == null) return false;

        switch (action.ToUpper())
        {
            case "VIEW":
                return permission.CanView ?? false;
            case "ADD":
                return permission.CanAdd ?? false;
            case "EDIT":
                return permission.CanEdit ?? false;
            case "DELETE":
                return permission.CanDelete ?? false;
            case "PRINT":
                return permission.CanPrint ?? false;
            default:
                return false;
        }
    }
    // Kiểm tra có phải Admin không
    public static bool IsAdmin()
    {
        return RoleName == "Admin";
    }

    // Clear session khi đăng xuất
    public static void Clear()
    {
        UserId = 0;
        Username = "";
        FullName = "";
        RoleId = 0;
        RoleName = "";
        MaCTY = "";
        MaDVI = "";
    }
}