using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Inputs
{
    public class ReplyPublishedAckSentToReplyAuthorCmd
    {
        public int ReplyId { get; }
        public int QuestionId { get; }
        public string Reply { get; }
        public ReplyPublishedAckSentToReplyAuthorCmd(int replyId, int questionId, string reply)
        {
            ReplyId = replyId;
            QuestionId = questionId;
            Reply = reply;
        }
    }
}
