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
    public class AccountController : ApiController
    {
        MysqlConnent MyConnent = new MysqlConnent();
        bool IsSuccess = true;

        // POST: api/Account
        /// <summary>
        /// 注册帐号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string PostAccount([FromBody]Sign value)
        {
            try
            {
                MyConnent.MySqlWrite("INSERT INTO 账号表() VALUES('" + value.account + "','" + value.password + "')");
                return value.account + "," +value.password;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }
            
        }

      
    }
}
