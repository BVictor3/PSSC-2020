using EarlyPay.Primitives.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.GetQuestionRepyOp
{
    public class GetQuestionReplyCmd
    {
        public GetQuestionReplyCmd(int questionId, int userId, Guid postedby, string postText, DateTime dateCreated)
        {
            PostId = questionId;
            TenantId = userId;
            PostedBy = postedby;
            PostText = postText;

            DateCreated = dateCreated;

        }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [GuidNotEmpty]
        public Guid PostedBy { get; set; }

        [GuidNotEmpty]
        public DateTime DateCreated { get; set; }


    }
}

