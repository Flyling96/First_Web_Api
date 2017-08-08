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
    public class RoadController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();
 
        // GET: api/Road/5
        /// <summary>
        /// 通过RoadID获取图片名
        /// </summary>
        /// <param name="RoadID"></param>
        /// <returns></returns>
        public ReturnImageNameByRoadID GetImageName(string RoadID)
        {
            ReturnImageNameByRoadID tmpModel = new ReturnImageNameByRoadID();
            List<string> tmpList = new List<string>();
            try
            {
                tmpList = myConnent.MySqlRead("SELECT * FROM 图片表 WHERE RoadID = '"+ RoadID + "'  AND IsDelete = '0' ORDER BY DateTime DESC", "ImageName");
                Debug.WriteLine(tmpList);
                tmpModel.returnImageName = tmpList;
                return tmpModel;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                tmpList.Add("error");
                tmpModel.returnImageName = tmpList;
                return tmpModel;
            }
        }

        /// <summary>
        /// 通过RoadID获取他人的图片名
        /// </summary>
        /// <param name="OtherRoadID"></param>
        /// <returns></returns>
        public ReturnImageNameByRoadID GetOthersImage(string OtherRoadID)
        {
            ReturnImageNameByRoadID tmpModel = new ReturnImageNameByRoadID();
            List<string> tmpList = new List<string>();
            try
            {
                tmpList = myConnent.MySqlRead("SELECT * FROM 图片表 WHERE RoadID = '" + OtherRoadID + "' AND IsDelete = '0' AND IsPublic = '1' AND IsCheck = '1' ORDER BY DateTime DESC", "ImageName");
                Debug.WriteLine(tmpList);
                tmpModel.returnImageName = tmpList;
                return tmpModel;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                tmpList.Add("error");
                tmpModel.returnImageName = tmpList;
                return tmpModel;
            }
        }


        /// <summary>
        /// 通过帐号获取行程信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ReturnRoad Get(string account)
        {
            ReturnRoad tmpReturnRoad = new ReturnRoad();
            List<Dictionary<string, string>> tmpList = new List<Dictionary<string, string>>();
            try
            {

                Dictionary<string, string> returnDictionary = new Dictionary<string, string>();
                returnDictionary.Add("RoadID", null);
                returnDictionary.Add("RoadName", null);
                returnDictionary.Add("Introduction", null);
                tmpList = myConnent.MySqlReadDictionary("SELECT * FROM 路径表 WHERE account ='" + account + "' ORDER BY EndTime DESC", returnDictionary);
                tmpReturnRoad.returnMessage = tmpList;
                return tmpReturnRoad;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                tmpReturnRoad.returnMessage = tmpList;
                return tmpReturnRoad;
            }

        }
        /// <summary>
        /// 通过账号获取最近一条行程信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ReturnFinalRoad GetFinalRoadName(string getAccount)
        {
            ReturnFinalRoad tmpRoad = new ReturnFinalRoad();
            try
            {
                tmpRoad.roadID = myConnent.MySqlReadReturn("SELECT * FROM 路径表 WHERE Account ='" + getAccount + "' ORDER BY EndTime DESC LIMIT 0,1","RoadID");
                return tmpRoad;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                tmpRoad.roadID = "error";
                return tmpRoad;


            }
        }

        // POST: api/Road
        /// <summary>
        /// 新增行程
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Post([FromBody]Road value)
        {
            try
            {
                string tmpID = myConnent.NewID();
                if (value.introduction == null)
                    value.introduction = "无";
                myConnent.MySqlWrite("INSERT INTO 路径表() VALUES('" + tmpID + "','" + value.roadName + "','" + value.account + "','" + value.introduction + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "')");
                return tmpID; 
            }
            catch(Exception e)
            {
                return "error";
            }
        }

 
    }
}
