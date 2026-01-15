using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using DataLayer;

public partial class Entities : DbContext
{
    private Entities(DbConnection connection, bool contextOwnsConnection = true)
        : base(new EntityConnection(connection.ConnectionString), contextOwnsConnection)
    { }
    // Mỹ sửa property bảng từ object sang DbSet<CongTy>
    public DbSet<CongTy> CongTy { get; set; }
    public DbSet<DonVi> DonVi { get; set; }

    public DbSet<KhachHang> KhachHang { get; set; }
    public DbSet<SanPham> SanPham { get; set; }
    public DbSet<Phong>   Phong { get; set; }
    public DbSet<LoaiPhong> LoaiPhong { get; set; }
    public DbSet<ThietBi> ThietBi { get; set; }
    public DbSet<Phong_ThietBi> Phong_ThietBi { get; set; }
    public DbSet<Tang> Tang { get; set; }
    public DbSet<DatPhong> DatPhong { get; set; }

    public DbSet<DatPhong_SanPham> DatPhong_SanPham { get; set; }

    public DbSet<Param> Param { get; set; }

    public DbSet<DatPhong_ChiTiet> DatPhong_ChiTiet { get; set; }

    public static Entities CreateEntities(bool contextOwnsConnection = true)
    {
        // CHUỖI KẾT NỐI TỰ ĐIỀN VÀO ĐÂY
        string servername = @"ADMIN-PC\SQLEXPRESS";
        string database = "QUAN LI KHACH SAN";

        // Build SqlConnectionString
        SqlConnectionStringBuilder sql = new SqlConnectionStringBuilder
        {
            DataSource = servername,
            InitialCatalog = database,
            IntegratedSecurity = true,   // ← QUAN TRỌNG: Cho phép Windows Authentication
            MultipleActiveResultSets = true
        };


        // Build EntityConnectionString
        EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder
        {
            Provider = "System.Data.SqlClient",
            ProviderConnectionString = sql.ConnectionString,
            Metadata = @"res://*/KHACHSAN.csdl|res://*/KHACHSAN.ssdl|res://*/KHACHSAN.msl"
        };

        EntityConnection connection = new EntityConnection(entityBuilder.ConnectionString);
        return new Entities(connection);
    }
}
