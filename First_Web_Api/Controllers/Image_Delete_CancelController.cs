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
    /// <summary>
    /// 取消假删除
    /// </summary>
    public class Image_Delete_CancelController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();
        // POST: api/Image_Delete_Cancel
        /// <summary>
        /// 取消假删除
        /// </summary>
        /// <param name="value"></param>
        public string Post([FromBody]DeleteImageModel value)
        {
            try
            {
                myConnent.MySqlWrite("UPDATE 图片表 SET IsDelete = '" + 0 + "' WHERE ImageName = '" + value.imageName + "'");
                return "true";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "error";
            }

        }

    }
}
