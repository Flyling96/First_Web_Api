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
    public class FollowerController : ApiController
    {
        MysqlConnent MyConnent = new MysqlConnent();

        /// <summary>
        /// 获取已关注的人的列表
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ReturnMyFollow Get(string account)
        {

            MysqlConnent myConnent = new MysqlConnent();

            ReturnMyFollow returnModel = new ReturnMyFollow();
            try
            {
                List<User> tmpUserList = new List<Models.User>();
                //select * from info where name regexp 'ic'
                //like "%2%"

                List<string> accountList = myConnent.MySqlRead("SELECT * FROM 关注表 WHERE FollowerAccount ='" + account + "'", "WasFollowederAccount");

                if (accountList[0] != "error")
                {
                    foreach (string wasFollowederAccount in accountList)
                    {
                        User tmpUser = new User();
                        tmpUser.account = wasFollowederAccount;
                        tmpUser.email = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + wasFollowederAccount + "'", "email");
                        tmpUser.sex = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + wasFollowederAccount + "'", "sex");
                        tmpUser.userName = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + wasFollowederAccount + "'", "userName");
                        tmpUser.introduction = myConnent.MySqlReadReturn("SELECT * FROM 账号表 WHERE Account ='" + wasFollowederAccount + "'", "introduction");
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

        // POST: api/Follower
        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Post([FromBody]Follower value)
        {
            try
            {
                MyConnent.MySqlWrite("INSERT INTO 关注表() VALUES('" + value.FollowerAccount + "','" + value.WasFollowederAccount + "')");
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
