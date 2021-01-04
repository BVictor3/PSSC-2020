using EarlyPay.Primitives.ValidationAttributes;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp
{
    public struct CreateQuestionCmd
    {
        public CreateQuestionCmd(int questionId, int userId) 
        {
            PostId = questionId;
            TenantId = userId;

        }

        [Required]
        public int PostId { get; set; }

        [Required]
        public int TenantId { get; set; }

    }
}

