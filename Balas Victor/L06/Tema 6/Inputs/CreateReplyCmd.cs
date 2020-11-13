using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Inputs
{
    public class CreateReplyCmd
    {
        public string Reply { get; }
        public int QuestionId { get; }
        public int AuthorId { get; }

        public CreateReplyCmd(int authorId, int questionId, string reply)
        {
            QuestionId = questionId;
            Reply = reply;
            AuthorId = authorId;
        }
    }
}
