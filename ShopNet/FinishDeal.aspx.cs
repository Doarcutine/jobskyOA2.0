using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyShop;
using System.Data;
using System.Data.SqlClient;

public partial class FinishDeal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Account"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            //绑定数据源
            string account = Session["Account"].ToString();
            SqlConnection cn = DBlink.GetConnection();
            string sqlstr = "SELECT TP_ID,TPGoods_ID,TP_Name,TP_Count,TP_Price,TP_EachTotal,(SELECT Person_Name from Person where Person_Account=TP_Account) as TP_AcName from TP where TP_Account='" + account + "'";
            SqlCommand cmm = new SqlCommand(sqlstr, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
        }
    }
    protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            //如果按了删除，就删除该条记录
            SqlConnection cn = DBlink.GetConnection();
            string sqlstr = "DELETE FROM TP WHERE TP_ID='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmm = new SqlCommand(sqlstr, cn);
            cn.Open();
            cmm.ExecuteNonQuery();
            cn.Close();
            Response.Redirect("FinishDeal.aspx");
        }
    }
    protected void btnPay_Click(object sender, EventArgs e)
    {
        SqlConnection cn = DBlink.GetConnection();
        SqlCommand cmm = new SqlCommand();
        cmm.Connection = cn;
        cmm.CommandType = CommandType.StoredProcedure;
        cmm.CommandText = "AddBuyGoods";            //添加到采购记录
        string account = Session["Account"].ToString();         
        string sqlstr = "DELETE FROM TP WHERE TP_Account='" + account + "'";
        string sqlstr1 = "SELECT * FROM TP WHERE TP_Account='" + account + "'";
        SqlCommand cmm1 = new SqlCommand(sqlstr1, cn);      //查询TP中的数据，准备减少库存量
        SqlCommand cmm2 = new SqlCommand(sqlstr, cn);       //清空购物车
        try
        {
            cn.Open();
            cmm.ExecuteNonQuery();      //添加到采购表
            SqlDataAdapter da1 = new SqlDataAdapter(cmm1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);      //ds1存储的是TP里面的信息，即之前购物车的信息
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                string buycount = ds1.Tables[0].Rows[i]["TP_Count"].ToString();     //购买的数量
                string goodsid = ds1.Tables[0].Rows[i]["TPGoods_ID"].ToString();    //购买商品的编号
                string sqlstr2 = "SELECT Goods_Count from Goods where Goods_ID='" + goodsid + "'";  
                SqlCommand cmm3 = new SqlCommand(sqlstr2, cn);
                string origincount = cmm3.ExecuteScalar().ToString();   //获取原始数量
                string nowcount = (int.Parse(origincount) - int.Parse(buycount)).ToString();
                string sqlstr3 = "Update Goods SET Goods_Count='" + nowcount + "' where Goods_ID='"+goodsid+"'";
                //更新库存量
                cmm3.CommandText = sqlstr3;
                cmm3.ExecuteNonQuery();
            }
            cmm2.ExecuteNonQuery();
            cn.Close();            
            Response.Write("<script>alert('购买成功！');location='Default.aspx'</script>");
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        string account = Session["Account"].ToString();
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "DELETE FROM TP WHERE TP_Account='" + account + "'";    //删除现有记录
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cn.Open();
        cmm.ExecuteNonQuery();
        cn.Close();
        Response.Redirect("FinishDeal.aspx");
    }
    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}