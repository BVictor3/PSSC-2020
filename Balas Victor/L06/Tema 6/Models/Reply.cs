using System;
using System.Collections.Generic;
using System.Text;

namespace Tema6.Models
{
    public class Question
    {

        public string[] Tags { get; }
        public string Body { get; }
        public string Title { get; }


        public Question(string title, string body, string[] tags)
        {
            Body = body;
            Tags = tags;
            Title = title;
        }
    }

    public class Reply
    {
        public int QuestionId { get; }
        public string Answer { get; }
        public string AuthorId { get; }
 

        public Reply(int questionId, string authorId, string answer)
        {
            AuthorId = authorId;
            Answer = answer;
            QuestionId = questionId;
        }
    }
}
