using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Web_Api.Models
{
    /// <summary>
    /// 上传图片Model
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// 图片名
        /// </summary>
        public string imageName { get; set; }
        /// <summary>
        /// 拍照日期
        /// </summary>
        public string dateTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 对应路径ID
        /// </summary>
        public string roadID { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public string isPublic { get; set; }
    }



    /// <summary>
    /// 对图片进行假删除操作
    /// </summary>
    public class DeleteImageModel
    {
        /// <summary>
        /// 假删除图片名
        /// </summary>
        public string imageName { get; set; }
    }


    /// <summary>
    /// 图片上传结果
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 返回结果 0: 失败，1: 成功。
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 操作信息，成功将返回空。
        /// </summary>
        public string Message { get; set; }

    }


    /// <summary>
    /// 返回图片名列表
    /// </summary>
    public class ReturnImageNameModel
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 返回图片名
        /// </summary>
        public List<string> imageName { get; set; }
    }

    /// <summary>
    /// 返回头像图片名
    /// </summary>
    public class ReturnHeadImageNameModel
    {
        /// <summary>
        /// 返回结果
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 返回头像图片名
        /// </summary>
        public string headimageName { get; set; }
    }


    /// <summary>
    /// 返回图片信息
    /// </summary>
    public class ReturnImageMessageModel
    {
        /// <summary>
        /// 返回的图片名
        /// </summary>
        public string imageName { get; set; }

        /// <summary>
        /// 拍照日期
        /// </summary>
        public string dateTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 图片简介
        /// </summary>
        public string introduction{get; set;}
    }

    /// <summary>
    /// 评论
    /// </summary>
    public class CommentModel
    {
        /// <summary>
        /// 评论账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 评价图片ID
        /// </summary>
        public string imageName { get; set; }
    }

    /// <summary>
    /// 返回评论
    /// </summary>
    public class CommentBackModel
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public string commentID { get; set; }

        /// <summary>
        /// 评论账号
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string comment { get; set; }

        /// <summary>
        /// 评价图片名
        /// </summary>
        public string imageName { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        public string dateTime { get; set; }

      
    }

    /// <summary>
    /// 返回评论列表
    /// </summary>
    public class CommentBackListModel
    {
        /// <summary>
        /// 结果
        /// </summary>
        public string result { get; set; }

        /// <summary>
        /// 返回用户列表
        /// </summary>
        public List<CommentBackModel> commentBackList { get; set; }



    }

    public class ReturnLikeCount
    {
        public string count { get; set; }
    }

}