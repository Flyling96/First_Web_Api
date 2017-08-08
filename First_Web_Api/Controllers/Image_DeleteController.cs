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
    public class Image_DeleteController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();


        // GET: api/Image_Delete/5
        /// <summary>
        /// 返回已删除的图片名
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ReturnImageNameModel Get(string account)
        {
            ReturnImageNameModel returnModel = new ReturnImageNameModel();
            try
            {
                List<string> imageNameList = myConnent.MySqlRead("SELECT * FROM 图片表 WHERE Account ='" + account + "' AND IsDelete ='"+ 1 +"' ORDER BY DateTime DESC", "imageName");
                if (imageNameList[0] != "error")
                {
                    returnModel.result = "true";
                    returnModel.imageName = imageNameList;
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


        // POST: api/Image_Delete
        /// <summary>
        /// 对图片进行假删除
        /// </summary>
        /// <param name="value"></param>
        public string Post([FromBody]DeleteImageModel value)
        {
            try
            {
                myConnent.MySqlWrite("UPDATE 图片表 SET IsDelete = '" + 1 + "' WHERE ImageName = '" + value.imageName + "'");
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
