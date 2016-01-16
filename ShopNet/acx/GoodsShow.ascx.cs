using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MyShop;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class GoodsShow : System.Web.UI.UserControl
{
    #region 分页参数
    private int pagesize=8;     //每页显示的记录数
    private int currentpageindex = 0;       //当前第几页，第一页为0
    public int PageSize
    {
        get { return pagesize; }
        set
        {
            if(value<1)return;      //每页显示数至少为1
            else pagesize=value;
        }
    }
    public int CurrentPageIndex
    {
        get { return currentpageindex; }
        set
        {
            if (value < 0) return;      //至少第一页
            else currentpageindex = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        int recordcount;
        if (!IsPostBack)        //第一次载入时
        {
            
            if(Request.QueryString["currentpageindex"]!=null)   CurrentPageIndex=int.Parse(Request.QueryString["currentpageindex"].Trim());
            if (Request.QueryString["Value"] != null)
            {
                string value = Request.QueryString["value"].ToString();
                string searchinfo = Request.QueryString["SearchInfo"].ToString();
                switch (value)
                {
                    case "0": ShowRecordByID(searchinfo);
                        recordcount = GetRecordCountByID(searchinfo);
                        break;
                    case "1": ShowRecordByName(searchinfo);
                        recordcount = GetRecordCountByName(searchinfo);
                        break;
                    case "2": ShowRecordByTime(searchinfo);
                        recordcount = GetRecordCountByTime(searchinfo);
                        break;
                    default:
                        recordcount = GetRecordCountByID(searchinfo);
                        break;
                }
            }
            else
            {
                ShowPageRecord();
                recordcount = GetRecordCount();     //获取记录总数
            }
            int pagecount = (recordcount % PageSize == 0 && recordcount >= PageSize) ? (recordcount / PageSize) : ((recordcount / PageSize) + 1);//总页数
            lblTotalPage.Text = pagecount.ToString();    //共?页
            lblNowPage.Text = (CurrentPageIndex + 1).ToString();    //当前第？页
            if (recordcount == 0)
            {
                lblMessage.Text = "当前无记录";
                Guide.Visible = false;   //导航区域全部不可见
            }
            if (pagecount == 1)     //只有一页时
            {
                hlFirst.Visible = false;
                hlPrevious.Visible = false;
                hlLast.Visible = false;         //首页，上一页，下一页，尾页，页面跳转都不可见
                hlNext.Visible = false;         //实际就是让导航栏里面只有“第几页和共几页可见”     
                dpPageJump.Visible = false;
                btGo.Visible = false;
            }
            //给下拉框添加内容
            dpPageJump.Items.Clear();       //先清空原有的
            for (int i = 1; i <= pagecount; i++)
            {
                dpPageJump.Items.Add(new ListItem((i).ToString(),(i-1).ToString()));        //显示为1，value为0
            }
            //上下页的逻辑
            if (CurrentPageIndex == 0) hlPrevious.Visible = false;  //第一页时，上一页按钮不可见
            if (CurrentPageIndex == pagecount - 1) hlNext.Visible = false;  //最后一页时，下一页按钮不可见
            if (pagecount == 0)     //只有一页时，上一页和下一页都不可见
            {
                hlPrevious.Visible = false;
                hlNext.Visible = false;
            }
            //获取当前页面的URL，不包含参数
            string url = Request.Url.ToString().Split('?')[0];
            if (Request.QueryString["Value"] != null)
            {
                string value = Request.QueryString["value"].ToString();
                string searchinfo = Request.QueryString["SearchInfo"].ToString();
                hlFirst.NavigateUrl = url +"?SearchInfo="+searchinfo+"&Value="+value+"&currentpageindex=" + "0";
                hlLast.NavigateUrl = url + "?SearchInfo=" + searchinfo + "&Value=" + value + "&currentpageindex=" + (pagecount - 1).ToString();
                hlPrevious.NavigateUrl = url + "?SearchInfo=" + searchinfo + "&Value=" + value + "&currentpageindex=" + (CurrentPageIndex - 1).ToString();
                hlNext.NavigateUrl = url + "?SearchInfo=" + searchinfo + "&Value=" + value + "&currentpageindex=" + (CurrentPageIndex + 1).ToString();
            }
            else
            {
                hlFirst.NavigateUrl = url + "?currentpageindex=" + "0";
                hlLast.NavigateUrl = url + "?currentpageindex=" + (pagecount - 1).ToString();
                hlPrevious.NavigateUrl = url + "?currentpageindex=" + (CurrentPageIndex - 1).ToString();
                hlNext.NavigateUrl = url + "?currentpageindex=" + (CurrentPageIndex + 1).ToString();
            }
        }   
    }
    public void ShowPageRecord()
    {       
            //获取商品表的前8条数据，同时把时间改为yyyy-mm-dd
            SqlConnection cn = DBlink.GetConnection();
            string sqlstr = "SELECT TOP (@pagesize) Goods_ID,Goods_Name,Goods_Price,Goods_Count,convert(nvarchar(10),Goods_UpTime,120)as Goods_UpTime,Goods_Image from Goods where Goods_ID not in(SELECT TOP (@pagesize*@pageindex) Goods_ID from Goods)";
            SqlCommand cmm = new SqlCommand(sqlstr, cn);
            cmm.Parameters.Add(new SqlParameter("@pagesize", PageSize));
            cmm.Parameters.Add(new SqlParameter("@pageindex", CurrentPageIndex));
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            cn.Close();
    }
    public int GetRecordCount()
    {
        //获取商品总数
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "SELECT Count(*) FROM Goods";
        SqlCommand cmm = new SqlCommand(sqlstr,cn);
        cn.Open();
        int totalcount = int.Parse(cmm.ExecuteScalar().ToString());
        cmm.Dispose();
        cn.Close();
        return totalcount;
    }
    #region 3中获取记录总数的方式
    public int GetRecordCountByID(string searchinfo)
    {
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "SELECT Count(*) FROM Goods where Goods_ID='"+searchinfo+"'";
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cn.Open();
        int count = int.Parse(cmm.ExecuteScalar().ToString());
        cmm.Dispose();
        cn.Close();
        return count;
    }
    public int GetRecordCountByName(string searchinfo)
    {
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "SELECT Count(*) FROM Goods where Goods_Name='" + searchinfo + "'";
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cn.Open();
        int count = int.Parse(cmm.ExecuteScalar().ToString());
        cmm.Dispose();
        cn.Close();
        return count;
    }
    public int GetRecordCountByTime(string searchinfo)
    {
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "SELECT Count(*) FROM Goods where convert(nvarchar(10),Goods_UpTime,120)='" + searchinfo + "'";
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cn.Open();
        int count = int.Parse(cmm.ExecuteScalar().ToString());
        cmm.Dispose();
        cn.Close();
        return count;
    }
#endregion
    #region 页面跳转
    protected void btGo_Click(object sender, EventArgs e)
    {
        //跳转到页面
        string url = Request.Url.ToString().Split('?')[0];
        if (Request.QueryString["Value"] != null)
        {
            string value = Request.QueryString["value"].ToString();
            string searchinfo = Request.QueryString["SearchInfo"].ToString();
            url = url + "?SearchInfo=" + searchinfo + "&Value=" + value + "&currentpageindex=" + dpPageJump.SelectedValue;
        }
        else
        {
            url += "?currentpageindex=" + dpPageJump.SelectedValue;
            Response.Redirect(url);
        }
    }
    #endregion
    #region 3种查找商品的方式
    public void ShowRecordByID(string searchinfo)
    {
        //根据商品ID查找后
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "SELECT TOP (@pagesize) Goods_ID,Goods_Name,Goods_Price,Goods_Count,convert(nvarchar(10),Goods_UpTime,120)as Goods_UpTime,Goods_Image from Goods where Goods_ID not in(SELECT TOP (@pagesize*@pageindex) Goods_ID from Goods where Goods_ID='" + searchinfo + "') and Goods_ID='" + searchinfo + "'";
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cmm.Parameters.Add(new SqlParameter("@pagesize", PageSize));
        cmm.Parameters.Add(new SqlParameter("@pageindex", CurrentPageIndex));
        cn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmm);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
        cn.Close();
    }
    public void ShowRecordByName(string searchinfo)
    {
        //根据商品名查找
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "SELECT TOP (@pagesize) Goods_ID,Goods_Name,Goods_Price,Goods_Count,convert(nvarchar(10),Goods_UpTime,120)as Goods_UpTime,Goods_Image from Goods where Goods_ID not in(SELECT TOP (@pagesize*@pageindex) Goods_ID from Goods where Goods_Name='" + searchinfo + "') and Goods_Name='" + searchinfo + "'";
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cmm.Parameters.Add(new SqlParameter("@pagesize", PageSize));
        cmm.Parameters.Add(new SqlParameter("@pageindex", CurrentPageIndex));
        cn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmm);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
        cn.Close();
    }
    public void ShowRecordByTime(string searchinfo)
    {
        //根据上架时间查找
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr = "SELECT TOP (@pagesize) Goods_ID,Goods_Name,Goods_Price,Goods_Count,convert(nvarchar(10),Goods_UpTime,120)as Goods_UpTime,Goods_Image from Goods where Goods_ID not in(SELECT TOP (@pagesize*@pageindex) Goods_ID from Goods where convert(nvarchar(10),Goods_UpTime,120)='" + searchinfo + "') and convert(nvarchar(10),Goods_UpTime,120)='" + searchinfo + "'";
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cmm.Parameters.Add(new SqlParameter("@pagesize", PageSize));
        cmm.Parameters.Add(new SqlParameter("@pageindex", CurrentPageIndex));
        cn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmm);
        DataSet ds = new DataSet();
        da.Fill(ds);
        Repeater1.DataSource = ds;
        Repeater1.DataBind();
        cn.Close();
    }
    #endregion
}