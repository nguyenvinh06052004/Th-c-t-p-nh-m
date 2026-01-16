namespace THUEPHONG
{
    partial class formBangLuong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnTaoBangLuong = new System.Windows.Forms.ToolStripButton();
            this.btnSua = new System.Windows.Forms.ToolStripButton();
            this.btnLuu = new System.Windows.Forms.ToolStripButton();
            this.btnBoQua = new System.Windows.Forms.ToolStripButton();
            this.btnThanhToan = new System.Windows.Forms.ToolStripButton();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnXemBangLuong = new DevExpress.XtraEditors.SimpleButton();
            this.txtTongLuong = new System.Windows.Forms.TextBox();
            this.txtKhauTru = new System.Windows.Forms.TextBox();
            this.txtThuong = new System.Windows.Forms.TextBox();
            this.txtPhuCap = new System.Windows.Forms.TextBox();
            this.txtLuongCoBan = new System.Windows.Forms.TextBox();
            this.txtSoGioLam = new System.Windows.Forms.TextBox();
            this.txtSoNgayCong = new System.Windows.Forms.TextBox();
            this.cboNhanVien = new System.Windows.Forms.ComboBox();
            this.numThang = new System.Windows.Forms.NumericUpDown();
            this.numNam = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcDanhSach = new DevExpress.XtraGrid.GridControl();
            this.gvDanhSach = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IdBangLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.MaNV = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HoTen = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Thang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Nam = new DevExpress.XtraGrid.Columns.GridColumn();
            this.SoNgayCong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LuongCoBan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PhuCap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Thuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.KhauTru = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TongLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSach)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTaoBangLuong,
            this.btnSua,
            this.btnLuu,
            this.btnBoQua,
            this.btnThanhToan,
            this.btnThoat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1400, 47);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnTaoBangLuong
            // 
            this.btnTaoBangLuong.Image = global::THUEPHONG.Properties.Resources.add;
            this.btnTaoBangLuong.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTaoBangLuong.Name = "btnTaoBangLuong";
            this.btnTaoBangLuong.Size = new System.Drawing.Size(42, 44);
            this.btnTaoBangLuong.Text = "Tạo ";
            this.btnTaoBangLuong.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTaoBangLuong.Click += new System.EventHandler(this.btnTaoBangLuong_Click);
            // 
            // btnSua
            // 
            this.btnSua.Image = global::THUEPHONG.Properties.Resources.update;
            this.btnSua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(38, 44);
            this.btnSua.Text = "Sửa";
            this.btnSua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Image = global::THUEPHONG.Properties.Resources.save;
            this.btnLuu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(37, 44);
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnBoQua
            // 
            this.btnBoQua.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBoQua.Name = "btnBoQua";
            this.btnBoQua.Size = new System.Drawing.Size(60, 44);
            this.btnBoQua.Text = "Bỏ qua";
            this.btnBoQua.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnBoQua.Click += new System.EventHandler(this.btnBoQua_Click);
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(87, 44);
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Image = global::THUEPHONG.Properties.Resources.exit;
            this.btnThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(51, 44);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 47);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1400, 703);
            this.splitContainerControl1.SplitterPosition = 320;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtGhiChu);
            this.groupControl1.Controls.Add(this.label12);
            this.groupControl1.Controls.Add(this.btnXemBangLuong);
            this.groupControl1.Controls.Add(this.txtTongLuong);
            this.groupControl1.Controls.Add(this.txtKhauTru);
            this.groupControl1.Controls.Add(this.txtThuong);
            this.groupControl1.Controls.Add(this.txtPhuCap);
            this.groupControl1.Controls.Add(this.txtLuongCoBan);
            this.groupControl1.Controls.Add(this.txtSoGioLam);
            this.groupControl1.Controls.Add(this.txtSoNgayCong);
            this.groupControl1.Controls.Add(this.cboNhanVien);
            this.groupControl1.Controls.Add(this.numThang);
            this.groupControl1.Controls.Add(this.numNam);
            this.groupControl1.Controls.Add(this.label11);
            this.groupControl1.Controls.Add(this.label10);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1400, 320);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Thông tin bảng lương";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(250, 221);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(300, 24);
            this.txtGhiChu.TabIndex = 24;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(150, 224);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(54, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "Ghi chú:";
            // 
            // btnXemBangLuong
            // 
            this.btnXemBangLuong.Location = new System.Drawing.Point(615, 31);
            this.btnXemBangLuong.Name = "btnXemBangLuong";
            this.btnXemBangLuong.Size = new System.Drawing.Size(140, 30);
            this.btnXemBangLuong.TabIndex = 22;
            this.btnXemBangLuong.Text = "Xem bảng lương";
            this.btnXemBangLuong.Click += new System.EventHandler(this.btnXemBangLuong_Click);
            // 
            // txtTongLuong
            // 
            this.txtTongLuong.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.txtTongLuong.ForeColor = System.Drawing.Color.Red;
            this.txtTongLuong.Location = new System.Drawing.Point(920, 220);
            this.txtTongLuong.Name = "txtTongLuong";
            this.txtTongLuong.ReadOnly = true;
            this.txtTongLuong.Size = new System.Drawing.Size(200, 28);
            this.txtTongLuong.TabIndex = 21;
            this.txtTongLuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtKhauTru
            // 
            this.txtKhauTru.Location = new System.Drawing.Point(920, 190);
            this.txtKhauTru.Name = "txtKhauTru";
            this.txtKhauTru.Size = new System.Drawing.Size(200, 23);
            this.txtKhauTru.TabIndex = 20;
            this.txtKhauTru.Text = "0";
            this.txtKhauTru.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtKhauTru.TextChanged += new System.EventHandler(this.TinhTongLuong);
            // 
            // txtThuong
            // 
            this.txtThuong.Location = new System.Drawing.Point(920, 160);
            this.txtThuong.Name = "txtThuong";
            this.txtThuong.Size = new System.Drawing.Size(200, 23);
            this.txtThuong.TabIndex = 19;
            this.txtThuong.Text = "0";
            this.txtThuong.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtThuong.TextChanged += new System.EventHandler(this.TinhTongLuong);
            // 
            // txtPhuCap
            // 
            this.txtPhuCap.Location = new System.Drawing.Point(920, 130);
            this.txtPhuCap.Name = "txtPhuCap";
            this.txtPhuCap.ReadOnly = true;
            this.txtPhuCap.Size = new System.Drawing.Size(200, 23);
            this.txtPhuCap.TabIndex = 18;
            this.txtPhuCap.Text = "0";
            this.txtPhuCap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLuongCoBan
            // 
            this.txtLuongCoBan.Location = new System.Drawing.Point(920, 100);
            this.txtLuongCoBan.Name = "txtLuongCoBan";
            this.txtLuongCoBan.ReadOnly = true;
            this.txtLuongCoBan.Size = new System.Drawing.Size(200, 23);
            this.txtLuongCoBan.TabIndex = 17;
            this.txtLuongCoBan.Text = "0";
            this.txtLuongCoBan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSoGioLam
            // 
            this.txtSoGioLam.Location = new System.Drawing.Point(250, 191);
            this.txtSoGioLam.Name = "txtSoGioLam";
            this.txtSoGioLam.ReadOnly = true;
            this.txtSoGioLam.Size = new System.Drawing.Size(300, 23);
            this.txtSoGioLam.TabIndex = 16;
            this.txtSoGioLam.Text = "0";
            // 
            // txtSoNgayCong
            // 
            this.txtSoNgayCong.Location = new System.Drawing.Point(250, 161);
            this.txtSoNgayCong.Name = "txtSoNgayCong";
            this.txtSoNgayCong.ReadOnly = true;
            this.txtSoNgayCong.Size = new System.Drawing.Size(300, 23);
            this.txtSoNgayCong.TabIndex = 15;
            this.txtSoNgayCong.Text = "0";
            // 
            // cboNhanVien
            // 
            this.cboNhanVien.FormattingEnabled = true;
            this.cboNhanVien.Location = new System.Drawing.Point(250, 131);
            this.cboNhanVien.Name = "cboNhanVien";
            this.cboNhanVien.Size = new System.Drawing.Size(300, 24);
            this.cboNhanVien.TabIndex = 14;
            // 
            // numThang
            // 
            this.numThang.Location = new System.Drawing.Point(250, 91);
            this.numThang.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numThang.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numThang.Name = "numThang";
            this.numThang.Size = new System.Drawing.Size(80, 23);
            this.numThang.TabIndex = 13;
            this.numThang.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numNam
            // 
            this.numNam.Location = new System.Drawing.Point(449, 91);
            this.numNam.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numNam.Minimum = new decimal(new int[] {
            2020,
            0,
            0,
            0});
            this.numNam.Name = "numNam";
            this.numNam.Size = new System.Drawing.Size(100, 23);
            this.numNam.TabIndex = 12;
            this.numNam.Value = new decimal(new int[] {
            2025,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.Red;
            this.label11.Location = new System.Drawing.Point(800, 223);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 18);
            this.label11.TabIndex = 11;
            this.label11.Text = "Tổng lương:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(800, 193);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 16);
            this.label10.TabIndex = 10;
            this.label10.Text = "Khấu trừ:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(800, 163);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 16);
            this.label9.TabIndex = 9;
            this.label9.Text = "Thưởng:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(800, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "Phụ cấp:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(800, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 16);
            this.label7.TabIndex = 7;
            this.label7.Text = "Lương cơ bản:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(150, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Số giờ làm:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(150, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Số ngày công:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Nhân viên:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(150, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(420, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Chọn tháng/năm và nhấn \"Xem bảng lương\" để hiển thị dữ liệu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(400, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Năm:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tháng:";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcDanhSach);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1400, 371);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Danh sách bảng lương";
            // 
            // gcDanhSach
            // 
            this.gcDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhSach.Location = new System.Drawing.Point(2, 28);
            this.gcDanhSach.MainView = this.gvDanhSach;
            this.gcDanhSach.Name = "gcDanhSach";
            this.gcDanhSach.Size = new System.Drawing.Size(1396, 341);
            this.gcDanhSach.TabIndex = 0;
            this.gcDanhSach.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhSach});
            // 
            // gvDanhSach
            // 
            this.gvDanhSach.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IdBangLuong,
            this.MaNV,
            this.HoTen,
            this.Thang,
            this.Nam,
            this.SoNgayCong,
            this.LuongCoBan,
            this.PhuCap,
            this.Thuong,
            this.KhauTru,
            this.TongLuong,
            this.TrangThai});
            this.gvDanhSach.GridControl = this.gcDanhSach;
            this.gvDanhSach.Name = "gvDanhSach";
            this.gvDanhSach.OptionsBehavior.Editable = false;
            this.gvDanhSach.OptionsView.ShowGroupPanel = false;
            this.gvDanhSach.Click += new System.EventHandler(this.gvDanhSach_Click);
            // 
            // IdBangLuong
            // 
            this.IdBangLuong.Caption = "ID";
            this.IdBangLuong.FieldName = "IdBangLuong";
            this.IdBangLuong.Name = "IdBangLuong";
            // 
            // MaNV
            // 
            this.MaNV.Caption = "MÃ NV";
            this.MaNV.FieldName = "MaNV";
            this.MaNV.Name = "MaNV";
            this.MaNV.Visible = true;
            this.MaNV.VisibleIndex = 0;
            this.MaNV.Width = 80;
            // 
            // HoTen
            // 
            this.HoTen.Caption = "HỌ TÊN";
            this.HoTen.FieldName = "HoTen";
            this.HoTen.Name = "HoTen";
            this.HoTen.Visible = true;
            this.HoTen.VisibleIndex = 1;
            this.HoTen.Width = 180;
            // 
            // Thang
            // 
            this.Thang.Caption = "THÁNG";
            this.Thang.FieldName = "Thang";
            this.Thang.Name = "Thang";
            this.Thang.Visible = true;
            this.Thang.VisibleIndex = 2;
            this.Thang.Width = 60;
            // 
            // Nam
            // 
            this.Nam.Caption = "NĂM";
            this.Nam.FieldName = "Nam";
            this.Nam.Name = "Nam";
            this.Nam.Visible = true;
            this.Nam.VisibleIndex = 3;
            this.Nam.Width = 60;
            // 
            // SoNgayCong
            // 
            this.SoNgayCong.Caption = "NGÀY CÔNG";
            this.SoNgayCong.FieldName = "SoNgayCong";
            this.SoNgayCong.Name = "SoNgayCong";
            this.SoNgayCong.Visible = true;
            this.SoNgayCong.VisibleIndex = 4;
            this.SoNgayCong.Width = 90;
            // 
            // LuongCoBan
            // 
            this.LuongCoBan.Caption = "LƯƠNG CƠ BẢN";
            this.LuongCoBan.DisplayFormat.FormatString = "N0";
            this.LuongCoBan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.LuongCoBan.FieldName = "LuongCoBan";
            this.LuongCoBan.Name = "LuongCoBan";
            this.LuongCoBan.Visible = true;
            this.LuongCoBan.VisibleIndex = 5;
            this.LuongCoBan.Width = 120;
            // 
            // PhuCap
            // 
            this.PhuCap.Caption = "PHỤ CẤP";
            this.PhuCap.DisplayFormat.FormatString = "N0";
            this.PhuCap.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PhuCap.FieldName = "PhuCap";
            this.PhuCap.Name = "PhuCap";
            this.PhuCap.Visible = true;
            this.PhuCap.VisibleIndex = 6;
            this.PhuCap.Width = 100;
            // 
            // Thuong
            // 
            this.Thuong.Caption = "THƯỞNG";
            this.Thuong.DisplayFormat.FormatString = "N0";
            this.Thuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Thuong.FieldName = "Thuong";
            this.Thuong.Name = "Thuong";
            this.Thuong.Visible = true;
            this.Thuong.VisibleIndex = 7;
            this.Thuong.Width = 100;
            // 
            // KhauTru
            // 
            this.KhauTru.Caption = "KHẤU TRỪ";
            this.KhauTru.DisplayFormat.FormatString = "N0";
            this.KhauTru.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.KhauTru.FieldName = "KhauTru";
            this.KhauTru.Name = "KhauTru";
            this.KhauTru.Visible = true;
            this.KhauTru.VisibleIndex = 8;
            this.KhauTru.Width = 100;
            // 
            // TongLuong
            // 
            this.TongLuong.Caption = "TỔNG LƯƠNG";
            this.TongLuong.DisplayFormat.FormatString = "N0";
            this.TongLuong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.TongLuong.FieldName = "TongLuong";
            this.TongLuong.Name = "TongLuong";
            this.TongLuong.Visible = true;
            this.TongLuong.VisibleIndex = 9;
            this.TongLuong.Width = 120;
            // 
            // TrangThai
            // 
            this.TrangThai.Caption = "TRẠNG THÁI";
            this.TrangThai.FieldName = "TrangThai";
            this.TrangThai.Name = "TrangThai";
            this.TrangThai.Visible = true;
            this.TrangThai.VisibleIndex = 10;
            this.TrangThai.Width = 120;
            // 
            // formBangLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 750);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "formBangLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý bảng lương";
            this.Load += new System.EventHandler(this.formBangLuong_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numThang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnTaoBangLuong;
        private System.Windows.Forms.ToolStripButton btnSua;
        private System.Windows.Forms.ToolStripButton btnLuu;
        private System.Windows.Forms.ToolStripButton btnBoQua;
        private System.Windows.Forms.ToolStripButton btnThanhToan;
        private System.Windows.Forms.ToolStripButton btnThoat;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcDanhSach;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDanhSach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numNam;
        private System.Windows.Forms.NumericUpDown numThang;
        private System.Windows.Forms.ComboBox cboNhanVien;
        private System.Windows.Forms.TextBox txtSoNgayCong;
        private System.Windows.Forms.TextBox txtSoGioLam;
        private System.Windows.Forms.TextBox txtLuongCoBan;
        private System.Windows.Forms.TextBox txtPhuCap;
        private System.Windows.Forms.TextBox txtThuong;
        private System.Windows.Forms.TextBox txtKhauTru;
        private System.Windows.Forms.TextBox txtTongLuong;
        private DevExpress.XtraEditors.SimpleButton btnXemBangLuong;
        private DevExpress.XtraGrid.Columns.GridColumn IdBangLuong;
        private DevExpress.XtraGrid.Columns.GridColumn MaNV;
        private DevExpress.XtraGrid.Columns.GridColumn HoTen;
        private DevExpress.XtraGrid.Columns.GridColumn Thang;
        private DevExpress.XtraGrid.Columns.GridColumn Nam;
        private DevExpress.XtraGrid.Columns.GridColumn SoNgayCong;
        private DevExpress.XtraGrid.Columns.GridColumn LuongCoBan;
        private DevExpress.XtraGrid.Columns.GridColumn PhuCap;
        private DevExpress.XtraGrid.Columns.GridColumn Thuong;
        private DevExpress.XtraGrid.Columns.GridColumn KhauTru;
        private DevExpress.XtraGrid.Columns.GridColumn TongLuong;
        private DevExpress.XtraGrid.Columns.GridColumn TrangThai;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label12;
    }
}
