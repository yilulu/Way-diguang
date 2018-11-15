using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Web.SessionState;
using DTcms.Common;

namespace DTcms.Web.tools
{
    /// <summary>
    /// 驗證碼生成類
    /// </summary>
    public class verify_code : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int codeW = 80;
            int codeH = 22;
            int fontSize = 16;
            string chkCode = string.Empty;
            //顏色列表，用於驗證碼、噪線、噪點 
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
            //字體清單，用於驗證碼 
            string[] font = { "Times New Roman", "Verdana", "Arial", "Gungsuh", "Impact" };
            //驗證碼的字元集，去掉了一些容易混淆的字元 
            char[] character = { '2', '3', '4', '5', '6', '8', '9', 'a', 'b', 'd', 'e', 'f', 'h', 'k', 'm', 'n', 'r', 'x', 'y', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'W', 'X', 'Y' };
            Random rnd = new Random();
            //生成驗證碼字串 
            for (int i = 0; i < 4; i++)
            {
                chkCode += character[rnd.Next(character.Length)];
            }
            //寫入Session
            context.Session[DTKeys.SESSION_CODE] = chkCode;
            //創建畫布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            //畫噪線 
            for (int i = 0; i < 1; i++)
            {
                int x1 = rnd.Next(codeW);
                int y1 = rnd.Next(codeH);
                int x2 = rnd.Next(codeW);
                int y2 = rnd.Next(codeH);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawLine(new Pen(clr), x1, y1, x2, y2);
            }
            //畫驗證碼字串 
            for (int i = 0; i < chkCode.Length; i++)
            {
                string fnt = font[rnd.Next(font.Length)];
                Font ft = new Font(fnt, fontSize);
                Color clr = color[rnd.Next(color.Length)];
                g.DrawString(chkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 18 + 2, (float)0);
            }
            //畫噪點 
            for (int i = 0; i < 100; i++)
            {
                int x = rnd.Next(bmp.Width);
                int y = rnd.Next(bmp.Height);
                Color clr = color[rnd.Next(color.Length)];
                bmp.SetPixel(x, y, clr);
            }
            //清除該頁輸出緩存，設置該頁無緩存 
            context.Response.Buffer = true;
            context.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            context.Response.Expires = 0;
            context.Response.CacheControl = "no-cache";
            context.Response.AppendHeader("Pragma", "No-Cache");
            //將驗證碼圖片寫入記憶體流，並將其以 "image/Png" 格式輸出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bmp.Save(ms, ImageFormat.Png);
                context.Response.ClearContent();
                context.Response.ContentType = "image/Png";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //顯式釋放資源 
                bmp.Dispose();
                g.Dispose();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
} 
