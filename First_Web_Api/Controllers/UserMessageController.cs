using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using First_Web_Api.Models;

namespace First_Web_Api.Controllers
{

    /// <summary>
    /// 个人信息管理
    /// </summary>
    public class UserMessageController : ApiController
    {
        MysqlConnent myConnent = new MysqlConnent();


        // GET: api/UserMessage
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public User Get(string account)
        {

            User tmp = new User();
            tmp.account = account;
            tmp.userName = myConnent.MySqlReadReturn("SELECT userName FROM 账号表 WHERE account ='" + account + "'", "userName");
            tmp.sex = myConnent.MySqlReadReturn("SELECT sex FROM 账号表 WHERE account ='" + account + "'", "sex");
            tmp.introduction = myConnent.MySqlReadReturn("SELECT introduction FROM 账号表 WHERE account ='" + account + "'", "introduction");
            tmp.email = myConnent.MySqlReadReturn("SELECT email FROM 账号表 WHERE account ='" + account + "'", "email");
            return tmp;
        }

    

        // POST: api/UserMessage
        /// <summary>
        /// 更改个人信息
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string PostPeopleMessage([FromBody]User value)
        {
            try
            {
                myConnent.MySqlWrite("UPDATE 账号表 SET userName = '" + value.userName + "', sex = '" + value.sex + "', introduction = '" + value.introduction + "', email = '" + value.email + "' WHERE account = '" + value.account + "'");
                return "true";
            }
            catch(Exception e)
            {
                return "false";
            }
        }

  
    }
}
