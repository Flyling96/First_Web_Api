using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Web_Api.Models
{
    /// <summary>
    /// 关注
    /// </summary>
    public class Follower
    {
        /// <summary>
        /// 关注者
        /// </summary>
        public string FollowerAccount { get; set; }

        /// <summary>
        /// 被关注者
        /// </summary>
        public string WasFollowederAccount { get; set; }
    }


    /// <summary>
    /// 返回我的关注
    /// </summary>
    public class ReturnMyFollow
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 返回用户列表
        /// </summary>
        public List<User> userList { get; set; }
    }

}