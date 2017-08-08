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
    public class User_FindController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();


        // POST: api/User_Find
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ReturnFindUser Post([FromBody]FindUser value)
        {
            ReturnFindUser returnModel = new ReturnFindUser();
            try
            {
                List<User> tmpUserList = new List<Models.User>();
                //select * from info where name regexp 'ic'
                //like "%2%"
          
                List<string> accountList = myConnent.MySqlRead("SELECT * FROM 账号表 WHERE Account Like '%" + value.account + "%' OR UserName Like '%" + value.account + "%'", "Account");
          
                if (accountList[0] != "error")
                {
                    foreach(string account in accountList)
                    {
                        User tmpUser = new User();
                        tmpUser.account = account;
                        tmpUser.email = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + account + "'", "email");
                        tmpUser.sex = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + account + "'", "sex");
                        tmpUser.userName = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + account + "'", "userName");
                        tmpUser.introduction = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + account + "'", "introduction");
                        tmpUserList.Add(tmpUser);
                    }

                    returnModel.result = "true";
                    returnModel.userList = tmpUserList;
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


    }
}
