using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using First_Web_Api.Models;
using System.IO;
using System.Text;


namespace First_Web_Api.Controllers
{
    public class ClickLikeController : ApiController
    {
        MysqlConnent myConnent = new MysqlConnent();

        // GET: api/ClickLike/5
        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public string Get(string imageName)
        {
            try
            {
                string ClickLikeCount = myConnent.MySqlReadReturn("SELECT * FROM 图片表 WHERE ImageName ='"+ imageName + "'","ClickLikeCount");
                int NewCount = int.Parse(ClickLikeCount);
                NewCount++;
                myConnent.MySqlWrite("UPDATE 图片表 SET ClickLikeCount ='" + NewCount + "' WHERE ImageName ='" + imageName + "'");
                return "true";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "false";
            }
        }

        /// <summary>
        /// 通过图片名返回赞数
        /// </summary>
        /// <param name="name">图片名</param>
        /// <returns></returns>
        public string GetLikeCount(string name)
        {
           
            try
            {           
                string count = myConnent.MySqlReadReturn("SELECT * FROM 图片表 WHERE ImageName ='" + name + "'", "ClickLikeCount");
                return count;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "error";
            }
        }
    }
}
