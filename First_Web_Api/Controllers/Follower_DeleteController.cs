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
    public class Follower_DeleteController : ApiController
    {
        MysqlConnent MyConnent = new MysqlConnent();


        // POST: api/Follower_Delete
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Post([FromBody]Follower value)
        {
            try
            {
                MyConnent.MySqlWrite("DELETE FROM 关注表 WHERE FollowerAccount ='" + value.FollowerAccount + "' AND WasFollowederAccount = '" + value.WasFollowederAccount + "'");
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
