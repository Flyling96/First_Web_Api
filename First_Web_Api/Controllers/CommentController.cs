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
    public class CommentController : ApiController
    {
        MysqlConnent myConnent = new MysqlConnent();
        // GET: api/Comment
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 通过图片id获取评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CommentBackListModel Get(string imageName)
        {
            CommentBackListModel returnModel = new CommentBackListModel();
            try
            {
                List<CommentBackModel> tmpCommentList = new List<CommentBackModel>();
                //select * from info where name regexp 'ic'
                //like "%2%"

                List<string> commentIDList = myConnent.MySqlRead("SELECT * FROM 评论表 WHERE ImageName ='"+ imageName + "'", "CommentID");

                if (commentIDList[0] != "error")
                {
                    foreach (string commentID in commentIDList)
                    {
                        CommentBackModel tmpComment = new CommentBackModel();
                        tmpComment.account = myConnent.MySqlReadReturn("SELECT * FROM 评论表 WHERE CommentID ='" + commentID + "' AND (IsCheck = '1' OR IsCheck = '0')", "Account");
                        tmpComment.comment= myConnent.MySqlReadReturn("SELECT * FROM 评论表 WHERE CommentID ='" + commentID + "' AND (IsCheck = '1' OR IsCheck = '0')", "Comment");
                        tmpComment.dateTime = myConnent.MySqlReadReturn("SELECT * FROM 评论表 WHERE CommentID ='" + commentID + "' AND (IsCheck = '1' OR IsCheck = '0')", "DateTime");
                        tmpComment.imageName = imageName;
                        tmpComment.commentID = commentID;
                        tmpCommentList.Add(tmpComment);
                    }

                    returnModel.result = "true";
                    returnModel.commentBackList = tmpCommentList;
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

        // POST: api/Comment
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="value"></param>
        public string Post([FromBody]CommentModel value)
        {
            try
            {
                string NewID = myConnent.NewID();
                myConnent.MySqlWrite("INSERT INTO 评论表() VALUES('" + NewID + "','" + value.account + "','" + value.comment + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + value.imageName + "','" + "0" + "')");
                Debug.WriteLine(value.comment);
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
