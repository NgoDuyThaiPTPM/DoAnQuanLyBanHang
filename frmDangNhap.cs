using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoAnMon_QuanLyBanHang
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        QL_DangNhap cauHinh = new QL_DangNhap();
        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text.Trim()))
            {
                MessageBox.Show("Không được bỏ trống" + lblTenDangNhap.Text.ToLower());
                this.txtTenDangNhap.Focus(); return;
            }
            if (string.IsNullOrEmpty(this.txtMatKhau.Text))
            {
                MessageBox.Show("Không được bỏ trống" + lblMatKhau.Text.ToLower());
                this.txtMatKhau.Focus();
                return;
            }
            int kq = cauHinh.Check_Config(); //hàm Check_Config() thuộc Class QL_NguoiDung 
            if (kq == 0)
            {
                ProcessLogin();// Cấu hình phù hợp xử lý đăng nhập 
            }
            if (kq == 1)
            {
                MessageBox.Show("Chuỗi cấu hình không tồn tại");// Xử lý cấu hình 
                
            }
            if (kq == 2)
            {
                MessageBox.Show("Chuỗi cấu hình không phù hợp");// Xử lý cấu hình 
                
            }

        }
        public void ProcessLogin()
        {
            int result;
            result = cauHinh.Check_User(txtTenDangNhap.Text, txtMatKhau.Text);
            if (result == 1000)
            {
                MessageBox.Show("Sai " + lblTenDangNhap.Text + " or " + lblMatKhau.Text);
                return;
            }
            else if (result == 2000)
            {
                MessageBox.Show("Tai khoan bi khoa !!");
                return;
            }
            if (Program.mainForm == null)
            {
                Program.mainForm = new frmMain();
            }
            this.Visible = false;
            Program.mainForm.Show();
        }
    }
}
