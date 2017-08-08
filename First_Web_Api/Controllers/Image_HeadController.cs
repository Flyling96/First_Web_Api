using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.Mvc;
using First_Web_Api.Models;
using System.Web.Hosting;
using System.Text;

namespace First_Web_Api.Controllers
{
    public class Image_HeadController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();
        // GET: api/Image_Head/5
        /// <summary>
        /// 获取Account对应的头像名
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ReturnHeadImageNameModel Get(string account)
        {


            ReturnHeadImageNameModel returnModel = new ReturnHeadImageNameModel();
            try
            {
                string imageNameList = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + account + "'", "HeadImageName");
                if (imageNameList!= "error")
                {
                    returnModel.result = "true";
                    returnModel.headimageName = imageNameList;
                    return returnModel;
                }
                else
                {
                    returnModel.result = "error";
                    return returnModel;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                returnModel.result = "error";
                return returnModel;
            }
        }


        /// <summary>
        /// 上传头像
        /// </summary>
        public async Task<string> Post()
        {
            string imageName;
            MysqlConnent myConnent = new MysqlConnent();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string root = HttpContext.Current.Server.MapPath("~/Image");//指定要将文件存入的服务器物理位置
            Debug.WriteLine(root);
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                List<string> fileName = new List<string>();

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {//接收文件
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);//获取上传文件实际的文件名
                    Trace.WriteLine("Server file path: " + file.LocalFileName);//获取上传文件在服务上默认的文件名
                    await Task.Delay(1000);
                    Save(file.LocalFileName, 77, 77);
                    fileName.Add(file.LocalFileName);
                }//TODO:这样做直接就将文件存到了指定目录下，暂时不知道如何实现只接收文件数据流但并不保存至服务器的目录下，由开发自行指定如何存储，比如通过服务存到图片服务器
                foreach (var key in provider.FormData.AllKeys)
                {//接收FormData
                    dic.Add(key, provider.FormData[key]);
                }
                imageName = fileName[0].Substring(fileName[0].Length - 45, 45);
                Debug.WriteLine(imageName);
                string account = dic["Account"];
                myConnent.MySqlWrite("INSERT INTO 图片表() VALUES('" + imageName + "','" + account + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + "0" + "','" + "0" + "','" + "暂无" + "','" + "0" + "','" + "暂无" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "')");
                // myConnent.MySqlHasRows("SELECT * FROM 路径表() ")
                myConnent.MySqlWrite("UPDATE 账号表 SET HeadImageName = '" + imageName + "' WHERE Account = '" + account + "'");


            }
            catch
            {
                throw;
            }

            return imageName;
        }



        /// <summary>
        /// 对图片进行压缩
        /// </summary>
        /// <param name="sourcefullname"></param>
        /// <param name="dispMaxWidth"></param>
        /// <param name="dispMaxHeight"></param>
        public async void Save(string sourcefullname, int dispMaxWidth, int dispMaxHeight)
        {
            try
            {
                Bitmap mg = new Bitmap(sourcefullname);
                Size newSize = new Size(dispMaxWidth, dispMaxHeight);
                Bitmap bp = ResizeImage(mg, newSize);
                if (bp != null)
                    bp.Save(sourcefullname + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            //Path.Combine(output, Path.GetFileName(sourcefullname))
        }

        Bitmap ResizeImage(Bitmap mg, Size newSize)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            int x = 0;
            int y = 0;

            Bitmap bp = null;
            try
            {
                if ((mg.Width / Convert.ToDouble(newSize.Width)) > (mg.Height /
                Convert.ToDouble(newSize.Height)))
                    ratio = Convert.ToDouble(mg.Width) / Convert.ToDouble(newSize.Width);
                else
                    ratio = Convert.ToDouble(mg.Height) / Convert.ToDouble(newSize.Height);
                myThumbHeight = Math.Ceiling(mg.Height / ratio);
                myThumbWidth = Math.Ceiling(mg.Width / ratio);

                Size thumbSize = new Size((int)newSize.Width, (int)newSize.Height);
                bp = new Bitmap(newSize.Width, newSize.Height);
                x = (newSize.Width - thumbSize.Width) / 2;
                y = (newSize.Height - thumbSize.Height);
                System.Drawing.Graphics g = Graphics.FromImage(bp);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Rectangle rect = new Rectangle(x, y, thumbSize.Width, thumbSize.Height);
                g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, GraphicsUnit.Pixel);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return bp;

        }

    }
}
