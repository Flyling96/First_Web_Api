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
    public class ClickLike_DeleteController : ApiController
    {
        MysqlConnent MyConnent = new MysqlConnent();

        // GET: api/ClickLike/5
        /// <summary>
        /// 取消点赞
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public string Get(string imageName)
        {
            try
            {
                string ClickLikeCount = MyConnent.MySqlReadReturn("SELECT * FROM 图片表 WHERE ImageName ='" + imageName + "'", "ClickLikeCount");
                int NewCount = int.Parse(ClickLikeCount);
                NewCount--;
                MyConnent.MySqlWrite("UPDATE 图片表 SET ClickLikeCount ='" + NewCount + "' WHERE ImageName ='" + imageName + "'");
                return "true";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "false";
            }
        }

    }
}
