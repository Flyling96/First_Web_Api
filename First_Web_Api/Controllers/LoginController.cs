using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using First_Web_Api.Models;

namespace First_Web_Api.Controllers
{
    public class LoginController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();


 

        // POST: api/Login
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string PostLogin([FromBody]Login value)
        {
            try
            {
                string isTrue = myConnent.MySqlHasRows("SELECT * FROM 账号表 WHERE (Account = '" + value.account + "' OR UserName ='"+ value.account + "') AND Password ='" + value.password + "'");
                return isTrue;
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            

        }

    }
}
