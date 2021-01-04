using CSharp.Choices;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp
{
    [AsChoice]
    static partial class SendConfirmationResult 
    {
        public interface ISendConfirmationResult { }

        public class ConfirmationSent : ISendConfirmationResult
        {
            public User AdminUser { get; }

            public string ConfirmationAcknowlwedgement { get; set; }

            public ConfirmationSent(User adminUser, string confirmationAcknowledgement)
            {
                AdminUser = adminUser;
                ConfirmationAcknowlwedgement = confirmationAcknowledgement;
            }
            ///TODO
        }

        public class ConfirmationNotSent : ISendConfirmationResult
        {
            ///TODO
        }

        public class InvalidRequest : ISendConfirmationResult
        {
            public string Message { get; }

            public InvalidRequest(string message)
            {
                Message = message;
            }

        }
    }
}
