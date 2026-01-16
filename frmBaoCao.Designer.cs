namespace THUEPHONG
{
    partial class frmBaoCao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title5 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title6 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.tabControlBaoCao = new System.Windows.Forms.TabControl();
            this.tabDoanhThu = new System.Windows.Forms.TabPage();
            this.tabTiLeDatPhong = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadDoanhThu = new System.Windows.Forms.Button();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDoanhThu = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.chartChiemCho = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnLoadChiemCho = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvPhong = new System.Windows.Forms.DataGridView();
            this.chartXuHuongNam = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblPhongNhieuNhat = new System.Windows.Forms.Label();
            this.lblPhongItNhat = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.tabControlBaoCao.SuspendLayout();
            this.tabDoanhThu.SuspendLayout();
            this.tabTiLeDatPhong.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartChiemCho)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartXuHuongNam)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlBaoCao
            // 
            this.tabControlBaoCao.Controls.Add(this.tabDoanhThu);
            this.tabControlBaoCao.Controls.Add(this.tabTiLeDatPhong);
            this.tabControlBaoCao.Location = new System.Drawing.Point(0, 50);
            this.tabControlBaoCao.Name = "tabControlBaoCao";
            this.tabControlBaoCao.SelectedIndex = 0;
            this.tabControlBaoCao.Size = new System.Drawing.Size(1953, 1028);
            this.tabControlBaoCao.TabIndex = 0;
            this.tabControlBaoCao.SelectedIndexChanged += new System.EventHandler(this.tabControlBaoCao_SelectedIndexChanged);
            this.tabControlBaoCao.TabIndexChanged += new System.EventHandler(this.tabControlBaoCao_TabIndexChanged);
            // 
            // tabDoanhThu
            // 
            this.tabDoanhThu.Controls.Add(this.chartXuHuongNam);
            this.tabDoanhThu.Controls.Add(this.label3);
            this.tabDoanhThu.Controls.Add(this.dgvDoanhThu);
            this.tabDoanhThu.Controls.Add(this.chartDoanhThu);
            this.tabDoanhThu.Controls.Add(this.groupBox1);
            this.tabDoanhThu.Location = new System.Drawing.Point(4, 25);
            this.tabDoanhThu.Name = "tabDoanhThu";
            this.tabDoanhThu.Padding = new System.Windows.Forms.Padding(3);
            this.tabDoanhThu.Size = new System.Drawing.Size(1345, 999);
            this.tabDoanhThu.TabIndex = 0;
            this.tabDoanhThu.Text = "Doanh Thu";
            this.tabDoanhThu.UseVisualStyleBackColor = true;
            this.tabDoanhThu.Click += new System.EventHandler(this.tabDoanhThu_Click);
            // 
            // tabTiLeDatPhong
            // 
            this.tabTiLeDatPhong.Controls.Add(this.groupBox4);
            this.tabTiLeDatPhong.Controls.Add(this.groupBox3);
            this.tabTiLeDatPhong.Controls.Add(this.label6);
            this.tabTiLeDatPhong.Controls.Add(this.dgvPhong);
            this.tabTiLeDatPhong.Controls.Add(this.groupBox2);
            this.tabTiLeDatPhong.Controls.Add(this.chartChiemCho);
            this.tabTiLeDatPhong.Location = new System.Drawing.Point(4, 25);
            this.tabTiLeDatPhong.Name = "tabTiLeDatPhong";
            this.tabTiLeDatPhong.Padding = new System.Windows.Forms.Padding(3);
            this.tabTiLeDatPhong.Size = new System.Drawing.Size(1945, 999);
            this.tabTiLeDatPhong.TabIndex = 1;
            this.tabTiLeDatPhong.Text = "Tỉ lệ đặt phòng";
            this.tabTiLeDatPhong.UseVisualStyleBackColor = true;
            this.tabTiLeDatPhong.Click += new System.EventHandler(this.tabTiLeDatPhong_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnLoadDoanhThu);
            this.groupBox1.Controls.Add(this.dtpDenNgay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpTuNgay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1339, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bộ lọc";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.CustomFormat = "dd/MM/yy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(1058, 19);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(166, 22);
            this.dtpDenNgay.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(969, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Đến ngày";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.CustomFormat = "dd/MM/yy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(783, 21);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(142, 22);
            this.dtpTuNgay.TabIndex = 5;
            this.dtpTuNgay.Value = new System.DateTime(2026, 1, 1, 0, 0, 0, 0);
            this.dtpTuNgay.ValueChanged += new System.EventHandler(this.dtpTuNgay_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(668, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Từ ngày";
            // 
            // btnLoadDoanhThu
            // 
            this.btnLoadDoanhThu.Location = new System.Drawing.Point(870, 72);
            this.btnLoadDoanhThu.Name = "btnLoadDoanhThu";
            this.btnLoadDoanhThu.Size = new System.Drawing.Size(110, 32);
            this.btnLoadDoanhThu.TabIndex = 6;
            this.btnLoadDoanhThu.Text = "Tải dữ liệu";
            this.btnLoadDoanhThu.UseVisualStyleBackColor = true;
            this.btnLoadDoanhThu.Click += new System.EventHandler(this.btnLoadDoanhThu_Click);
            // 
            // chartDoanhThu
            // 
            this.chartDoanhThu.BackColor = System.Drawing.Color.LightBlue;
            chartArea5.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend5);
            this.chartDoanhThu.Location = new System.Drawing.Point(111, 212);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartDoanhThu.Series.Add(series5);
            this.chartDoanhThu.Size = new System.Drawing.Size(795, 324);
            this.chartDoanhThu.TabIndex = 1;
            this.chartDoanhThu.Text = "Biểu đồ doanh thu";
            title5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title5.Name = "Title1";
            title5.Text = "Doanh thu theo ngày";
            this.chartDoanhThu.Titles.Add(title5);
            // 
            // dgvDoanhThu
            // 
            this.dgvDoanhThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoanhThu.Location = new System.Drawing.Point(111, 663);
            this.dgvDoanhThu.Name = "dgvDoanhThu";
            this.dgvDoanhThu.RowHeadersWidth = 51;
            this.dgvDoanhThu.RowTemplate.Height = 24;
            this.dgvDoanhThu.Size = new System.Drawing.Size(1700, 149);
            this.dgvDoanhThu.TabIndex = 2;
            this.dgvDoanhThu.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDoanhThu_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(107, 640);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Danh sách hoá đơn ";
            // 
            // chartChiemCho
            // 
            chartArea4.Name = "ChartArea1";
            this.chartChiemCho.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartChiemCho.Legends.Add(legend4);
            this.chartChiemCho.Location = new System.Drawing.Point(620, 217);
            this.chartChiemCho.Name = "chartChiemCho";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            this.chartChiemCho.Series.Add(series4);
            this.chartChiemCho.Size = new System.Drawing.Size(1000, 262);
            this.chartChiemCho.TabIndex = 2;
            this.chartChiemCho.Text = "chart1";
            title4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title4.Name = "Title1";
            title4.Text = "Tỉ lệ lấp đầy";
            this.chartChiemCho.Titles.Add(title4);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLoadChiemCho);
            this.groupBox2.Controls.Add(this.dateTimePicker1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.dateTimePicker2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1939, 131);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bộ lọc";
            // 
            // btnLoadChiemCho
            // 
            this.btnLoadChiemCho.Location = new System.Drawing.Point(870, 72);
            this.btnLoadChiemCho.Name = "btnLoadChiemCho";
            this.btnLoadChiemCho.Size = new System.Drawing.Size(110, 32);
            this.btnLoadChiemCho.TabIndex = 6;
            this.btnLoadChiemCho.Text = "Tải dữ liệu";
            this.btnLoadChiemCho.UseVisualStyleBackColor = true;
            this.btnLoadChiemCho.Click += new System.EventHandler(this.btnLoadChiemCho_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "dd/MM/yy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(1058, 19);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(166, 22);
            this.dateTimePicker1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(969, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Đến ngày";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "dd/MM/yy";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(783, 21);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(142, 22);
            this.dateTimePicker2.TabIndex = 5;
            this.dateTimePicker2.Value = new System.DateTime(2026, 1, 1, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(668, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Từ ngày";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(225, 583);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Danh sách phòng";
            // 
            // dgvPhong
            // 
            this.dgvPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhong.Location = new System.Drawing.Point(234, 609);
            this.dgvPhong.Name = "dgvPhong";
            this.dgvPhong.RowHeadersWidth = 51;
            this.dgvPhong.RowTemplate.Height = 24;
            this.dgvPhong.Size = new System.Drawing.Size(1400, 144);
            this.dgvPhong.TabIndex = 4;
            // 
            // chartXuHuongNam
            // 
            chartArea6.Name = "ChartArea1";
            this.chartXuHuongNam.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartXuHuongNam.Legends.Add(legend6);
            this.chartXuHuongNam.Location = new System.Drawing.Point(976, 212);
            this.chartXuHuongNam.Name = "chartXuHuongNam";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chartXuHuongNam.Series.Add(series6);
            this.chartXuHuongNam.Size = new System.Drawing.Size(831, 324);
            this.chartXuHuongNam.TabIndex = 4;
            this.chartXuHuongNam.Text = "chart1";
            title6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            title6.Name = "Title1";
            title6.Text = "Xu hướng doanh thu trong năm ";
            this.chartXuHuongNam.Titles.Add(title6);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox3.Controls.Add(this.lblPhongNhieuNhat);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(230, 217);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(308, 106);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // lblPhongNhieuNhat
            // 
            this.lblPhongNhieuNhat.AutoSize = true;
            this.lblPhongNhieuNhat.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhongNhieuNhat.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblPhongNhieuNhat.Location = new System.Drawing.Point(19, 28);
            this.lblPhongNhieuNhat.Name = "lblPhongNhieuNhat";
            this.lblPhongNhieuNhat.Size = new System.Drawing.Size(204, 20);
            this.lblPhongNhieuNhat.TabIndex = 0;
            this.lblPhongNhieuNhat.Text = "Phòng được đặt nhiều nhất";
            this.lblPhongNhieuNhat.Click += new System.EventHandler(this.lblPhongNhieuNhat_Click);
            // 
            // lblPhongItNhat
            // 
            this.lblPhongItNhat.AutoSize = true;
            this.lblPhongItNhat.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhongItNhat.ForeColor = System.Drawing.Color.Red;
            this.lblPhongItNhat.Location = new System.Drawing.Point(19, 31);
            this.lblPhongItNhat.Name = "lblPhongItNhat";
            this.lblPhongItNhat.Size = new System.Drawing.Size(175, 20);
            this.lblPhongItNhat.TabIndex = 0;
            this.lblPhongItNhat.Text = "Phòng được đặt ít nhất";
            this.lblPhongItNhat.Click += new System.EventHandler(this.lblPhongItNhat_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Gainsboro;
            this.groupBox4.Controls.Add(this.lblPhongItNhat);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(230, 362);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(311, 117);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnThoat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1353, 47);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
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
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 1028);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControlBaoCao);
            this.Name = "frmBaoCao";
            this.Text = "frmBaoCao";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            this.tabControlBaoCao.ResumeLayout(false);
            this.tabDoanhThu.ResumeLayout(false);
            this.tabDoanhThu.PerformLayout();
            this.tabTiLeDatPhong.ResumeLayout(false);
            this.tabTiLeDatPhong.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartChiemCho)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartXuHuongNam)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlBaoCao;
        private System.Windows.Forms.TabPage tabDoanhThu;
        private System.Windows.Forms.TabPage tabTiLeDatPhong;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.Button btnLoadDoanhThu;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvDoanhThu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartChiemCho;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnLoadChiemCho;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvPhong;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartXuHuongNam;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblPhongItNhat;
        private System.Windows.Forms.Label lblPhongNhieuNhat;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnThoat;
    }
}