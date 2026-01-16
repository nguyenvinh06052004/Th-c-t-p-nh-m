namespace THUEPHONG
{
    partial class formCauHinhLuong
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
            this.btnThem = new System.Windows.Forms.ToolStripButton();
            this.btnSua = new System.Windows.Forms.ToolStripButton();
            this.btnXoa = new System.Windows.Forms.ToolStripButton();
            this.btnLuu = new System.Windows.Forms.ToolStripButton();
            this.btnBoQua = new System.Windows.Forms.ToolStripButton();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtPhuCapXangXe = new System.Windows.Forms.TextBox();
            this.txtPhuCapAnTrua = new System.Windows.Forms.TextBox();
            this.txtPhuCapChucVu = new System.Windows.Forms.TextBox();
            this.txtLuongCoBan = new System.Windows.Forms.TextBox();
            this.txtChucVu = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcDanhSach = new DevExpress.XtraGrid.GridControl();
            this.gvDanhSach = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IdCauHinh = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ChucVu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.LuongCoBan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PhuCapChucVu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PhuCapAnTrua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PhuCapXangXe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
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
            this.btnThem,
            this.btnSua,
            this.btnXoa,
            this.btnLuu,
            this.btnBoQua,
            this.btnThoat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1000, 47);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnThem
            // 
            this.btnThem.Image = global::THUEPHONG.Properties.Resources.add;
            this.btnThem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(50, 44);
            this.btnThem.Text = "Thêm";
            this.btnThem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
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
            // btnXoa
            // 
            this.btnXoa.Image = global::THUEPHONG.Properties.Resources.delete_3221897;
            this.btnXoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(39, 44);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
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
            this.splitContainerControl1.Size = new System.Drawing.Size(1000, 553);
            this.splitContainerControl1.SplitterPosition = 250;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtGhiChu);
            this.groupControl1.Controls.Add(this.txtPhuCapXangXe);
            this.groupControl1.Controls.Add(this.txtPhuCapAnTrua);
            this.groupControl1.Controls.Add(this.txtPhuCapChucVu);
            this.groupControl1.Controls.Add(this.txtLuongCoBan);
            this.groupControl1.Controls.Add(this.txtChucVu);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label4);
            this.groupControl1.Controls.Add(this.label3);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(1000, 250);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Thông tin cấu hình lương";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(200, 175);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(400, 40);
            this.txtGhiChu.TabIndex = 11;
            // 
            // txtPhuCapXangXe
            // 
            this.txtPhuCapXangXe.Location = new System.Drawing.Point(600, 118);
            this.txtPhuCapXangXe.Name = "txtPhuCapXangXe";
            this.txtPhuCapXangXe.Size = new System.Drawing.Size(200, 23);
            this.txtPhuCapXangXe.TabIndex = 10;
            this.txtPhuCapXangXe.Text = "0";
            this.txtPhuCapXangXe.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPhuCapAnTrua
            // 
            this.txtPhuCapAnTrua.Location = new System.Drawing.Point(600, 88);
            this.txtPhuCapAnTrua.Name = "txtPhuCapAnTrua";
            this.txtPhuCapAnTrua.Size = new System.Drawing.Size(200, 23);
            this.txtPhuCapAnTrua.TabIndex = 9;
            this.txtPhuCapAnTrua.Text = "0";
            this.txtPhuCapAnTrua.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPhuCapChucVu
            // 
            this.txtPhuCapChucVu.Location = new System.Drawing.Point(600, 58);
            this.txtPhuCapChucVu.Name = "txtPhuCapChucVu";
            this.txtPhuCapChucVu.Size = new System.Drawing.Size(200, 23);
            this.txtPhuCapChucVu.TabIndex = 8;
            this.txtPhuCapChucVu.Text = "0";
            this.txtPhuCapChucVu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLuongCoBan
            // 
            this.txtLuongCoBan.Location = new System.Drawing.Point(200, 116);
            this.txtLuongCoBan.Name = "txtLuongCoBan";
            this.txtLuongCoBan.Size = new System.Drawing.Size(200, 23);
            this.txtLuongCoBan.TabIndex = 7;
            this.txtLuongCoBan.Text = "0";
            this.txtLuongCoBan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtChucVu
            // 
            this.txtChucVu.Location = new System.Drawing.Point(200, 60);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Size = new System.Drawing.Size(200, 23);
            this.txtChucVu.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(100, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ghi chú:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(450, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phụ cấp xăng xe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(450, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Phụ cấp ăn trua:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Phụ cấp chức vụ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lương cơ bản:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chức vụ:";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gcDanhSach);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1000, 291);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "Danh sách cấu hình lương";
            // 
            // gcDanhSach
            // 
            this.gcDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcDanhSach.Location = new System.Drawing.Point(2, 28);
            this.gcDanhSach.MainView = this.gvDanhSach;
            this.gcDanhSach.Name = "gcDanhSach";
            this.gcDanhSach.Size = new System.Drawing.Size(996, 261);
            this.gcDanhSach.TabIndex = 0;
            this.gcDanhSach.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDanhSach});
            // 
            // gvDanhSach
            // 
            this.gvDanhSach.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IdCauHinh,
            this.ChucVu,
            this.LuongCoBan,
            this.PhuCapChucVu,
            this.PhuCapAnTrua,
            this.PhuCapXangXe});
            this.gvDanhSach.GridControl = this.gcDanhSach;
            this.gvDanhSach.Name = "gvDanhSach";
            this.gvDanhSach.OptionsBehavior.Editable = false;
            this.gvDanhSach.OptionsView.ShowGroupPanel = false;
            this.gvDanhSach.Click += new System.EventHandler(this.gvDanhSach_Click);
            // 
            // IdCauHinh
            // 
            this.IdCauHinh.Caption = "ID";
            this.IdCauHinh.FieldName = "IdCauHinh";
            this.IdCauHinh.Name = "IdCauHinh";
            // 
            // ChucVu
            // 
            this.ChucVu.Caption = "CHỨC VỤ";
            this.ChucVu.FieldName = "ChucVu";
            this.ChucVu.Name = "ChucVu";
            this.ChucVu.Visible = true;
            this.ChucVu.VisibleIndex = 0;
            this.ChucVu.Width = 150;
            // 
            // LuongCoBan
            // 
            this.LuongCoBan.Caption = "LƯƠNG CƠ BẢN";
            this.LuongCoBan.DisplayFormat.FormatString = "N0";
            this.LuongCoBan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.LuongCoBan.FieldName = "LuongCoBan";
            this.LuongCoBan.Name = "LuongCoBan";
            this.LuongCoBan.Visible = true;
            this.LuongCoBan.VisibleIndex = 1;
            this.LuongCoBan.Width = 150;
            // 
            // PhuCapChucVu
            // 
            this.PhuCapChucVu.Caption = "PHỤ CẤP CHỨC VỤ";
            this.PhuCapChucVu.DisplayFormat.FormatString = "N0";
            this.PhuCapChucVu.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PhuCapChucVu.FieldName = "PhuCapChucVu";
            this.PhuCapChucVu.Name = "PhuCapChucVu";
            this.PhuCapChucVu.Visible = true;
            this.PhuCapChucVu.VisibleIndex = 2;
            this.PhuCapChucVu.Width = 150;
            // 
            // PhuCapAnTrua
            // 
            this.PhuCapAnTrua.Caption = "PHỤ CẤP ĂN TRUA";
            this.PhuCapAnTrua.DisplayFormat.FormatString = "N0";
            this.PhuCapAnTrua.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PhuCapAnTrua.FieldName = "PhuCapAnTrua";
            this.PhuCapAnTrua.Name = "PhuCapAnTrua";
            this.PhuCapAnTrua.Visible = true;
            this.PhuCapAnTrua.VisibleIndex = 3;
            this.PhuCapAnTrua.Width = 150;
            // 
            // PhuCapXangXe
            // 
            this.PhuCapXangXe.Caption = "PHỤ CẤP XĂNG XE";
            this.PhuCapXangXe.DisplayFormat.FormatString = "N0";
            this.PhuCapXangXe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.PhuCapXangXe.FieldName = "PhuCapXangXe";
            this.PhuCapXangXe.Name = "PhuCapXangXe";
            this.PhuCapXangXe.Visible = true;
            this.PhuCapXangXe.VisibleIndex = 4;
            this.PhuCapXangXe.Width = 150;
            // 
            // formCauHinhLuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "formCauHinhLuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu hình lương theo chức vụ";
            this.Load += new System.EventHandler(this.formCauHinhLuong_Load);
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
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcDanhSach)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDanhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnThem;
        private System.Windows.Forms.ToolStripButton btnSua;
        private System.Windows.Forms.ToolStripButton btnXoa;
        private System.Windows.Forms.ToolStripButton btnLuu;
        private System.Windows.Forms.ToolStripButton btnBoQua;
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
        private System.Windows.Forms.TextBox txtChucVu;
        private System.Windows.Forms.TextBox txtLuongCoBan;
        private System.Windows.Forms.TextBox txtPhuCapChucVu;
        private System.Windows.Forms.TextBox txtPhuCapAnTrua;
        private System.Windows.Forms.TextBox txtPhuCapXangXe;
        private System.Windows.Forms.TextBox txtGhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn IdCauHinh;
        private DevExpress.XtraGrid.Columns.GridColumn ChucVu;
        private DevExpress.XtraGrid.Columns.GridColumn LuongCoBan;
        private DevExpress.XtraGrid.Columns.GridColumn PhuCapChucVu;
        private DevExpress.XtraGrid.Columns.GridColumn PhuCapAnTrua;
        private DevExpress.XtraGrid.Columns.GridColumn PhuCapXangXe;
    }
}