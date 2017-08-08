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
    public class FileController : ApiController
    {
        /// <summary>
        /// 返回原图
        /// </summary>
        /// <param name="imagename"></param>
        /// <returns></returns>
        //[System.Web.Mvc.Route("api/image/tmp")]
        public HttpResponseMessage GetImage()
        {
            try
            {
                string root = HttpContext.Current.Server.MapPath("~/File/lvtu.apk");
                //从图片中读取byte
                var imgByte = File.ReadAllBytes(root);
                //从图片中读取流
                var imgStream = new MemoryStream(File.ReadAllBytes(root));
                var resp = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(imgByte)
                    //或者
                    //Content = new StreamContent(stream)
                };
                resp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "lvtu.apk"
                };
                return resp;
            }

            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return null;
            }
        }




        /// <summary>
        /// 接收图片
        /// </summary>
        /// <returns></returns>
        public async Task<string> Post()
        {
            MysqlConnent myConnent = new MysqlConnent();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string root = HttpContext.Current.Server.MapPath("~/File");//指定要将文件存入的服务器物理位置
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
                }//TODO:这样做直接就将文件存到了指定目录下，暂时不知道如何实现只接收文件数据流但并不保存至服务器的目录下，由开发自行指定如何存储，比如通过服务存到图片服务器


            }
            catch
            {
                throw;
            }

            return "true";
        }

    }
}
