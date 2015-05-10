using System;
using System.Web;
using System.Web.SessionState;
using TNGames.Core.Domain;
using TNGames.Core.Helper;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace TNGames.Controls
{
    public class Captcha : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return false; }
        }

        int _width = 0;
        int _height = 0;
        Color _bgColor = Color.White;
        Color _color = Color.Black;

        public void ProcessRequest(HttpContext context)
        {
            GetQueryStringValue(context);

            context.Response.ClearHeaders();
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";

            ResponseStreamImage(context);
        }

        private void ResponseStreamImage(HttpContext context)
        {
            try
            {
                string text = CaptChaText();
                context.Session[TNHelper.CaptchaKey] = text;

                byte[] buffer = CreateBitmapImage(text);
                context.Response.BinaryWrite(buffer);
            }
            catch (Exception exp)
            {
                System.Diagnostics.Trace.Write(exp.ToString());
                // If any kind of error occurs return a 500 Internal Server error
                context.Response.StatusCode = 500;
                context.Response.Write("An error occured");
                context.Response.End();
            }
            finally
            {
                context.Response.End();
            }
        }

        private void GetQueryStringValue(HttpContext context)
        {
            if (context.Request.QueryString["w"] != null)
                int.TryParse(context.Request.QueryString["w"], out _width);

            if (_width <= 0 || _width > 500)
                _width = 90;

            if (context.Request.QueryString["h"] != null)
                int.TryParse(context.Request.QueryString["h"], out _height);

            if (_height <= 0 || _height > 400)
                _height = 30;

            #region bgcolor

            string tmpColor = string.Empty;
            if (context.Request.QueryString["bgcolor"] != null)
                tmpColor = context.Request.QueryString["bgcolor"];

            if (string.IsNullOrEmpty(tmpColor))
                tmpColor = "#FFFFFF";

            if (!tmpColor.StartsWith("#"))
                tmpColor = "#" + tmpColor;

            _bgColor = ColorTranslator.FromHtml(tmpColor);

            #endregion

            string color = string.Empty;
            if (context.Request.QueryString["color"] != null)
                color = context.Request.QueryString["color"];

            if (string.IsNullOrEmpty(color))
                color = "#000000";

            if (!color.StartsWith("#"))
                color = "#" + color;

            _color = ColorTranslator.FromHtml(color);

        }

        protected string CaptChaText()
        {
            string gui = Guid.NewGuid().ToString();
            return gui.Substring(0, 6);
        }

        private byte[] CreateBitmapImage(string sImageText)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create the Font object for the image text drawing.
            Font objFont = new Font("Arial", 20, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(sImageText, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(_width, _height));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);
            // objGraphics.FillRectangle(new System.Drawing.SolidBrush(_bgColor), new System.Drawing.Rectangle(0, 0, _width, _height));

            // Set Background color
            objGraphics.Clear(_bgColor);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(sImageText, objFont, new SolidBrush(_color), new System.Drawing.Rectangle(0, 0, _width, _height));
            objGraphics.Flush();

            MemoryStream ms = new MemoryStream();
            objBmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] buffer = ms.GetBuffer();
            if (ms != null)
                ms.Dispose();

            return ms.GetBuffer();
        }

        #endregion
    }
}
