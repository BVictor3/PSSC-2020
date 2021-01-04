using Access.Primitives.IO;
using EarlyPay.Primitives.ValidationAttributes;
using LanguageExt;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp
{
    public struct SendConfirmationCmd
    {
        [OptionValidator(typeof(RequiredAttribute))]
        public Option<User> AdminUser { get; }

        public SendConfirmationCmd(Option<User> adminUser)
        {
            AdminUser = adminUser;
        }
    }

    public enum SendConfirmationCmdInput
    {
        Valid,
        UserIsNone
    }

    public class SendConfirmationCmdInputGen : InputGenerator<SendConfirmationCmd, SendConfirmationCmdInput>
    {
        public SendConfirmationCmdInputGen()
        {
            mappings.Add(SendConfirmationCmdInput.Valid, () =>
                new SendConfirmationCmd(
                    Option<User>.Some(new User()
                    {
                        DisplayName = Guid.NewGuid().ToString(),
                        Email = $"{Guid.NewGuid()}@mailinator.com"
                    }))
            );

            mappings.Add(SendConfirmationCmdInput.UserIsNone, () =>
                new SendConfirmationCmd(
                    Option<User>.None
                    )
            );
        }
    }
}
