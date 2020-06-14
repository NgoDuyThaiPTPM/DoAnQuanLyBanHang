using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DoAnMon_QuanLyBanHang
{
    class QL_DangNhap
    {
        public int Check_Config()
        {
            if (Properties.Settings.Default.ConnectDB == string.Empty)
                return 1;// Chuỗi cấu hình không tồn tại 
            SqlConnection _Sqlconn = new SqlConnection(Properties.Settings.Default.ConnectDB);
            try
            {
                if (_Sqlconn.State == System.Data.ConnectionState.Closed)
                    _Sqlconn.Open();
                return 0;
                // Kết nối thành công chuỗi cấu hình hợp lệ 
            }
            catch
            {
                return 2;// Chuỗi cấu hình không phù hợp. 
            }
        }
        public int Check_User(string pUser, string pPass)
        {
            SqlDataAdapter daUser = new SqlDataAdapter("select * from QL_NguoiDung where TenDangNhap='" + pUser + "' and MatKhau ='" + pPass + "'", Properties.Settings.Default.ConnectDB);
            DataTable dt = new DataTable();
            daUser.Fill(dt);
            if (dt.Rows.Count == 0)
                return 1000;
            else if (dt.Rows[0][2] == null || dt.Rows[0][2].ToString() == "False")
                return 2000;
            return 3000;
        }
        public DataTable GetServerName()
        {
            DataTable dt = new DataTable();
            dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            return dt;
        }
        public DataTable GetDBName(string pServer, string pUser, string pPass)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select name from sys.Databases", "Data Source =" + pServer + ";Initial Catalog =master; User ID =" + pUser + "; pwd =" + pPass);
            da.Fill(dt);
            return dt;
        }
        public void SaveConfig(string pServer, string pUser, string pPass, string pDataBase)
        {
            _DoAnMon_QuanLyBanHang.Properties.Settings.Default.ConnectDB = "Data Source=" + pServer + ";Initial Catalog=" + pDataBase + ";User ID=" + pUser + ";pwd = " + pPass + "";
            _DoAnMon_QuanLyBanHang.Properties.Settings.Default.Save();
        }
    }
}
