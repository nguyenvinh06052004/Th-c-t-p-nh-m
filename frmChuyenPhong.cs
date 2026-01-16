using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using DataLayer;

namespace THUEPHONG
{
    public partial class frmChuyenPhong : Form
    {
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];
        public frmChuyenPhong()
        {
            InitializeComponent();
        }
        public int _idPhong;
        PHONG _phong;
        DATPHONG_CHITIET _datphongct;
        DATPHONG_SP _datphongsp;
        DATPHONG _datphong;
        private void frmChuyenPhong_Load(object sender, EventArgs e)
        {
            _phong = new PHONG();
            _datphongct = new DATPHONG_CHITIET();
            _datphongsp = new DATPHONG_SP();
            _datphong = new DATPHONG();
            var p = _phong.getItemFull(_idPhong);
            lblPhong.Text = p.TENPHONG + "- Đơn giá: " + p.DONGIA.ToString("N0");
            loadPhongTrong();
        }
        void loadPhongTrong()
        {
            searchPhong.Properties.DataSource = _phong.getPhongTrongFull();
            searchPhong.Properties.ValueMember = "IDPHONG";
            searchPhong.Properties.DisplayMember = "TENPHONG";
            searchPhong.Properties.PopulateViewColumns();

           
        }


        private void btnChuyenPhong_Click(object sender, EventArgs e)
        {
            if (searchPhong.EditValue == null || searchPhong.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn phòng muốn chuyển đến.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            decimal tongtien1 = 0;
            decimal tongtien2 = 0;
            var phonghientai = _datphongct.getIDDPByPhong(_idPhong);
            var phongchuyenden = _phong.getItemFull(int.Parse(searchPhong.EditValue.ToString()));

            List<DatPhong_SanPham> lstDPSP = _datphongsp.getAllByPhong(phonghientai.IdDatPhong, phonghientai.IdDPCT);
            foreach (var item in lstDPSP)
            {
                item.IdPhong = int.Parse(searchPhong.EditValue.ToString());
                tongtien1 += decimal.Parse(item.DonGia.ToString()) * int.Parse(item.SoLuong.ToString());
                _datphongsp.update(item);
            }
            var dpct = _datphongct.getItem(phonghientai.IdDatPhong, _idPhong);
            dpct.IdPhong = phongchuyenden.IDPHONG;
            dpct.DonGia = (decimal)phongchuyenden.DONGIA;

            dpct.ThanhTien = dpct.SoNgayO * decimal.Parse(phongchuyenden.DONGIA.ToString());
            tongtien2 = decimal.Parse(dpct.ThanhTien.ToString());
            _datphongct.update(dpct);

            _phong.updateStatus(_idPhong, false);
            _phong.updateStatus(phongchuyenden.IDPHONG, true);

            var dp = _datphong.getItem(phonghientai.IdDatPhong);
            if (dp == null)
            {
                MessageBox.Show("Không tìm thấy thông tin đặt phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dp.SoTien = tongtien1 + tongtien2;
            _datphong.update(dp);

            objMain.gControl.Gallery.Groups.Clear();
            objMain.showRoom();

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
