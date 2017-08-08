using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using First_Web_Api.Models;

namespace First_Web_Api.Controllers
{
    public class SignController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();


        // GET: api/sign?account=233
        /// <summary>
        /// 查看帐号是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string Get(string userName)
        {
            try
            {
                int tmp = myConnent.MySqlReadCount("SELECT COUNT(*) FROM 账号表 WHERE UserName ='" + userName + "'");
                if(tmp==1)
                {
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "false";
            }
        }

        // POST: api/Sign
        /// <summary>
        /// 注册帐号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Post([FromBody]Sign value)
        {
            try
            {
                myConnent.MySqlWrite("INSERT INTO 账号表() VALUES('" + value.account + "','" + value.password + "','" + value.username + "','" + "暂无" + "','" + "这个人很懒，什么都没留下" + "','" + "1" + "','" + "head" + "')");
                return "true";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "false";
            }

        }

  
    }
}
