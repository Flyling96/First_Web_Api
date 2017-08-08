using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using First_Web_Api.Models;

namespace First_Web_Api.Controllers
{
    public class ImageMessageController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();


        // GET: api/ImageMessage
        /// <summary>
        /// 返回图片信息
        /// </summary>
        /// <param name="imageName"></param>
        /// <returns></returns>
        public ReturnImageMessageModel GetMessage(string imageName)
        {
            ReturnImageMessageModel tmpReturnModel = new ReturnImageMessageModel();
            try
            {
               
                tmpReturnModel.imageName = imageName;
                tmpReturnModel.dateTime = myConnent.MySqlReadReturn("SELECT dateTime FROM 图片表 WHERE ImageName ='" + imageName + "'", "dateTime");
                tmpReturnModel.introduction = myConnent.MySqlReadReturn("SELECT introduce FROM 图片表 WHERE ImageName ='" + imageName + "'", "introduce");
                tmpReturnModel.longitude = myConnent.MySqlReadReturn("SELECT longitude FROM 图片表 WHERE ImageName ='" + imageName + "'", "longitude");
                tmpReturnModel.latitude = myConnent.MySqlReadReturn("SELECT latitude FROM 图片表 WHERE ImageName ='" + imageName + "'", "latitude");
                return tmpReturnModel;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return tmpReturnModel;
            }

        }




        // GET: api/ImageMessage/5
        /// <summary>
        /// 返回自己的图片名
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ReturnImageNameModel Get(string account)
        {
            ReturnImageNameModel returnModel = new ReturnImageNameModel();
            try
            {
                List<string> imageNameList = myConnent.MySqlRead("SELECT * FROM 图片表 WHERE Account ='" + account + "' AND IsDelete = '0'  ORDER BY DateTime DESC", "imageName");
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
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                returnModel.result = "error";
                return returnModel;
            }
        }


        // GET: api/ImageMessage/5
        /// <summary>
        /// 返回他人的图片名
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ReturnImageNameModel GetOthers(string othersAccount)
        {
            ReturnImageNameModel returnModel = new ReturnImageNameModel();
            try
            {
                List<string> imageNameList = myConnent.MySqlRead("SELECT * FROM 图片表 WHERE Account ='" + othersAccount + "' AND IsDelete = '0' AND IsPublic = '1' AND IsCheck = '1' ORDER BY DateTime DESC", "imageName");
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


        // POST: api/ImageMessage
        /// <summary>
        /// 添加图片信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Post([FromBody]ImageModel value)
        {
            try
            {
                myConnent.MySqlWrite("UPDATE 图片表 SET DateTime = '" + value.dateTime + "', Longitude = '" + value.longitude + "', Introduce = '" + value.introduction + "', Latitude = '" + value.latitude + "', RoadID = '" + value.roadID + "', isPublic = '" + value.isPublic + "' WHERE ImageName = '" + value.imageName + "'");
                return "true";
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "error";
            }
        }

  
    }
}
