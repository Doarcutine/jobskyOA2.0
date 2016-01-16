using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using MyShop;

public partial class JudgeShow : System.Web.UI.UserControl
{
    #region 页面参数
    private int pagesize = 8;       //每页显示多少记录
    private int currentpageindex = 0;       //当前是第几页，第一页为0
    public int PageSize
    {
        get { return pagesize; }
        set
        {
            if (value < 1) return;      //至少一条记录
            else pagesize = value;
        }
    }
    public int CurrentPageIndex
    {
        get { return currentpageindex; }
        set
        {
            if (value < 0) return;
            else currentpageindex = value;
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //获取商品的ID，即页面的第一个参数
        string goodsid = Request.QueryString["Goods_ID"].ToString();
        if (!IsPostBack)
        {
            if (Request.QueryString["currentpageindex"] != null) CurrentPageIndex = int.Parse(Request.QueryString["currentpageindex"].ToString());
            ShowPageRecord();
            int recordcount = GetRecordCount();        //总数据数量
            int pagecount = (recordcount % PageSize == 0 && recordcount >= PageSize) ? (recordcount / PageSize) : ((recordcount / PageSize) + 1);
            lblCurrentPage.Text = (CurrentPageIndex + 1).ToString();
            lblTotalPage.Text = pagecount.ToString();
            if (recordcount == 0)       //没有数据时
            {
                lblMessage.Text = "暂无评论";
                JudgeGuide.Visible = false;
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
            //给下拉框添加内容，先清空原有的
            dpPageJump.Items.Clear();
            for (int i = 1; i <= pagecount; i++)
            {
                dpPageJump.Items.Add(new ListItem((i).ToString(), (i - 1).ToString()));     //第一页的value为0
            }
            //上下页的逻辑
            if (CurrentPageIndex == 0) hlPrevious.Visible = false;    //第一页时，上一页不可见
            if (CurrentPageIndex == pagecount - 1) hlNext.Visible = false;  //最后一页时，下一页不可见
            if (pagecount == 1)     //只有一页时，上下页都不可见
            {
                hlPrevious.Visible = false;
                hlNext.Visible = false;
            }
            //获取页面URL，不包含参数
            string url = Request.Url.ToString().Split('?')[0];
            hlFirst.NavigateUrl = url + "?Goods_ID=" + goodsid + "&currentpageindex=" + (0).ToString();
            hlPrevious.NavigateUrl = url + "?Goods_ID=" + goodsid + "&currentpageindex=" + (CurrentPageIndex-1).ToString();
            hlNext.NavigateUrl = url + "?Goods_ID=" + goodsid + "&currentpageindex=" + (CurrentPageIndex+1).ToString();
            hlLast.NavigateUrl = url + "?Goods_ID=" + goodsid + "&currentpageindex=" + (pagecount-1).ToString();
        }
    }
    public void ShowPageRecord()
    {
        string goodsid = Request.QueryString["Goods_ID"].ToString();    //获取所查看货物的ID
        SqlConnection cn = DBlink.GetConnection();
        //查询所查看货物的相关评论（根据Account获取Name）
        string sqlstr = "SELECT TOP (@pagesize) Judge_Content,convert(nvarchar(10),Judge_Time,120)as Judge_Time,(SELECT Person_Name from Person where Person.Person_Account=Judge.Person_Account)as Person_Name FROM Judge where Judge_ID not in (SELECT TOP (@pagesize*@pageindex) Judge_ID from Judge where Goods_ID='"+goodsid+"') and Goods_ID='" + goodsid + "'";
        SqlCommand cmm = new SqlCommand(sqlstr, cn);
        cmm.Parameters.Add(new SqlParameter("@pagesize", PageSize));
        cmm.Parameters.Add(new SqlParameter("@pageindex", CurrentPageIndex));
        SqlDataAdapter da = new SqlDataAdapter(cmm);
        DataSet ds = new DataSet();
        try
        {
            cn.Open();
            da.Fill(ds);
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            cn.Close();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    public int GetRecordCount()
    {
        string goodsid = Request.QueryString["Goods_ID"].ToString();
        int count;
        SqlConnection cn = DBlink.GetConnection();
        string sqlstr="SELECT Count(*) from Judge where Goods_ID='"+goodsid+"'";
        SqlCommand cmm = new SqlCommand(sqlstr,cn);
        cn.Open();
        count = int.Parse(cmm.ExecuteScalar().ToString());
        return count;
    }
    protected void btGo_Click(object sender, EventArgs e)
    {
        string goodsid = Request.QueryString["Goods_ID"].ToString();
        string url = Request.Url.ToString().Split('?')[0];
        url = url + "?Goods_ID=" + goodsid + "&currentpageindex=" + dpPageJump.SelectedValue;
        Response.Redirect(url);
    }
}