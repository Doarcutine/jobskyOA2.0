using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
///Goods 的摘要说明
/// </summary>
namespace MyShop
{
    public class Goods
    {
        public static DataRow GetGoodsByGoodsID(string goodsid)
        {
            SqlConnection cn = DBlink.GetConnection();
            string sqlstr = "SELECT * FROM Goods where Goods_ID='" + goodsid + "'";
            SqlCommand cmm = new SqlCommand(sqlstr,cn);
            SqlDataAdapter da = new SqlDataAdapter(cmm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds.Tables[0].Rows[0];
        }
        public static DataSet GetDsGoodsByGoodsID(string goodsid)
        {
            SqlConnection cn = DBlink.GetConnection();
            string sqlstr = "SELECT Goods_ID,Goods_Name,Goods_Price,Goods_Count,convert(nvarchar(10),Goods_UpTime,120)as Goods_UpTime,Goods_Image FROM Goods where Goods_ID='" + goodsid + "'";
            SqlCommand cmm = new SqlCommand(sqlstr, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}