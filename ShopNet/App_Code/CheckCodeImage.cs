using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
/// <summary>
///CheckCodeImage 的摘要说明
/// </summary>
public class CheckCodeImage
{
    /// <summary>
    /// 生成随机验证码，四位数
    /// </summary>
    private HttpResponse Response;
    public CheckCodeImage(HttpResponse response)
    {
        this.Response = response;
    }


    public string GetCheckCode()
    {
        int num;
        char code;
        string checkCode = string.Empty;
        System.Random random = new Random();


        for (int i = 0; i < 4; i++)
        {
            num = random.Next();
            code = (char)('0' + (char)(num % 10));
            checkCode += code.ToString();
        }
        return checkCode;
    }


    public void CreatCheckCodeImage(string checkCode)
    {
        //不允许验证码为空
        if (checkCode == null || checkCode.Trim() == string.Empty)
        { return; }
        System.Drawing.Bitmap image = new Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);//验证码图片的高度和宽度
        Graphics g = Graphics.FromImage(image);


        Random random = new Random();
        //清空图片背景色
        g.Clear(Color.White);


        for (int i = 0; i < 44; i++)
        {
            int x1 = random.Next(image.Width - i);
            int x2 = random.Next(image.Width);
            int y1 = random.Next(image.Height);
            int y2 = random.Next(image.Height);
            g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
        }


        Font font = new Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
        System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);


        g.DrawString(checkCode, font, brush, 2, 2);
        //画前景噪音点
        for (int i = 0; i < 36; i++)
        {
            int x = random.Next(image.Width);
            int y = random.Next(image.Height);


            image.SetPixel(x, y, Color.FromArgb(random.Next()));
        }


        //画图片的的边框线
        g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        Response.ClearContent();
        Response.ContentType = "image/Gif";
        Response.BinaryWrite(ms.ToArray());

    }
}