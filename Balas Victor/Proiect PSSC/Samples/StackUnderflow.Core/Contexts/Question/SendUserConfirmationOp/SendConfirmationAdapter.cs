using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp.SendConfirmationResult;
namespace StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp
{
    public partial class SendConfirmationAdapter : Adapter<SendConfirmationCmd, ISendConfirmationResult, QuestionWriteContext, QuestionDependencies>
    {
        public SendConfirmationAdapter()
        {
        }

        public override async Task<ISendConfirmationResult> Work(SendConfirmationCmd command, QuestionWriteContext state, QuestionDependencies dependencies)
        {
            var wf = from isValid in command.TryValidate()
                     from user in command.AdminUser.ToTryAsync()
                     let letter = GenerateConfirmationLetter(user)
                     from invitationAck in dependencies.SendConfirmationEmail(letter)
                     select (user, invitationAck);

            return await wf.Match(
                Succ: r => new ConfirmationSent(r.user, r.invitationAck.Receipt),
                Fail: ex => (ISendConfirmationResult)new InvalidRequest(ex.ToString()));
        }

        private ConfirmationLetter GenerateConfirmationLetter(User user)
        {
            var link = $"https://stackunderflow/question";
            var letter = @$"Dear {user.DisplayName}Please click on {link}";
            return new ConfirmationLetter(user.Email, letter, new Uri(link));
        }

        public override Task PostConditions(SendConfirmationCmd cmd, ISendConfirmationResult result, QuestionWriteContext state)
        {
            return Task.CompletedTask;
        }
    }
}
