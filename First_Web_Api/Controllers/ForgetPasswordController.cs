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
    public class ForgetPasswordController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();



        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string PostForgetPassword([FromBody]ForgetPassword value)
        {
            try
            {
                string isTrue = myConnent.MySqlHasRows("SELECT * FROM 账号表 WHERE Account = '" + value.account +  "'");
                if (isTrue=="true")
                {
                    Debug.WriteLine(value.newPassword);
                    myConnent.MySqlWrite("UPDATE 账号表 SET Password = '" + value.newPassword + "' WHERE Account = '" + value.account + "'");
                    return "true";
                }
                else
                {
                    return "false";
                }
                

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "error";
            }
        }

    }
}
