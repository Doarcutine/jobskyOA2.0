using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
///DBlink 的摘要说明
/// </summary>
namespace MyShop
{
    public class DBlink
    {
        public static SqlConnection GetConnection()
        {
            //数据库连接
            string mysql = ConfigurationManager.ConnectionStrings["myconnectionstrings"].ToString();
            SqlConnection cn = new SqlConnection(mysql);
            return cn;
        }
    }
}