using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Web_Api.Models
{
    /// <summary>
    /// 注册
    /// </summary>
    public class Sign
    {
        /// <summary>
        /// 注册时的帐号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 注册时的密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// 注册时的用户名
        /// </summary>
        public string username { get; set; }

    }

    /// <summary>
    /// 登录
    /// </summary>
    public class Login
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string password { get; set; }

    }
    /// <summary>
    /// 更改密码
    /// </summary>
    public class ChangePassword
    {

        /// <summary>
        /// 帐号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 旧密码
        /// </summary>
        public string oldPassword { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string newPassword { get; set; }

    }

    /// <summary>
    /// 忘记密码
    /// </summary>
    public class ForgetPassword
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        public string newPassword { get; set; }

    }

    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
    }

    /// <summary>
    /// 寻找用户(用帐号或者用户名)
    /// </summary>
    public class FindUser
    {
        /// <summary>
        /// 帐号或者用户名
        /// </summary>
        public string account { get; set; }
    }

    /// <summary>
    /// 返回所寻找的用户信息
    /// </summary>
    public class ReturnFindUser
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