using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using First_Web_Api.Models;

namespace First_Web_Api.Controllers
{
    public class PasswordController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();

        // POST: api/Password
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string PostChangePassword([FromBody]ChangePassword value)
        {
            try
            {

                string isTrue = myConnent.MySqlHasRows("SELECT * FROM 账号表 WHERE Account = '" + value.account + "' AND Password ='" + value.oldPassword + "'");
                if(isTrue=="true")
                {
                    myConnent.MySqlWrite("UPDATE 账号表 SET Password = '" + value.newPassword + "' WHERE Account = '" + value.account  +"'");
                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch(Exception e)
            {
                return "wrong";
            }
        }

    }
}
