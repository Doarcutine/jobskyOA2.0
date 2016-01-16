using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

/// <summary>
///Person 的摘要说明
/// </summary>
namespace MyShop
{
    public class Person
    {
        public static DataRow GetPersonByAccount(string account)
        {
            //返回含有相应账号的所有信息的一条记录
            SqlConnection cnn = DBlink.GetConnection();
            string sqlstr = "SELECT * FROM Person where Person_Account='" + account + "'";
            SqlCommand cmm = new SqlCommand(sqlstr, cnn);
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmm);
            DataSet ds = new DataSet();
            da.Fill(ds);
            cmm.Dispose();
            cnn.Close();
            return ds.Tables[0].Rows[0];       
        }
        public static int GetCountByAccount(string account)
        {
            //返回相应账号的记录条数（即是否存在该账户）
            int count=0;
            SqlConnection cnn = DBlink.GetConnection();
            string sqlstr = "Select Count(*) FROM Person where Person_Account='" + account + "'";
            SqlCommand cmm = new SqlCommand(sqlstr, cnn);
            cnn.Open();
            count=int.Parse(cmm.ExecuteScalar().ToString());
            cmm.Dispose();
            cnn.Close();
            return count;
        }
        #region 加密、解密函数
        public static string EnCrypt(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                int itchar = (int)(str.Substring(i,1)[0]);
                itchar = itchar + (i + 1);
                char chr = (char)itchar;
                result += chr.ToString();
            }
            return result;
        }
        public static string DeCrypt(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                int itchar = (int)(str.Substring(i, 1)[0]);
                itchar = itchar - (i + 1);
                char chr = (char)itchar;
                result += chr.ToString();
            }
            return result;
        }
        #endregion
        public static string UpLoadPicture(FileUpload UpFile,DataRow dr)
        {
            if (UpFile.HasFile)     //如果已经上传了文件
            {
                //加入允许上传的文件格式
                List<string> FileType = new List<string>();
                FileType.Add("jpg");
                FileType.Add("gif");
                FileType.Add("png");
                FileType.Add("bmp");
                FileType.Add("jpeg");
                //判断上传文件格式是否正确
                if (FileType.Contains(UpFile.FileName.Substring(UpFile.FileName.LastIndexOf(".") + 1)))
                {
                    string name = dr["Person_Name"].ToString().Trim();  //获取登陆者的名字，准备用于上传图片的名称
                    string FilePath = "";
                    //获取当前时间，并将格式改为yyyymmdd，准备用于上传图片的名称
                    string FileTime = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("-", "").Replace("/", "");
                    //获取完整的Image文件夹物理路径(F://..../Image/)
                    string FoderPath = System.Web.HttpContext.Current.Server.MapPath("~/Image/");  
                    try
                    {
                        FilePath = FoderPath + name+FileTime + UpFile.FileName;
                        UpFile.SaveAs(FilePath);       //文件上传成功，存储文件到目录下                    
                    }
                    catch
                    {
                        return "上传文件失败！";
                    }
                    return name + FileTime + UpFile.FileName;
                }
                else
                {
                    return "不支持上传此类图片文件！";
                }
            }
            return "未选中文件！";
        }
    }
}