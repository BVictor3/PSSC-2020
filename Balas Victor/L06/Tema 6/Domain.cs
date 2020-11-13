using System;
using System.Collections.Generic;
using System.Text;
using Access.Primitives.IO;
using LanguageExt;

using Tema6.Inputs;
using Tema6.Outputs;
using static PortExt;


namespace Tema6
{
    public static class Domain
    {
        public static Port<CreateReplyResult.IPublishReplyResult> ValidateReply(int questionId, int authorId, string answer)
            => NewPort<CreateReplyCmd, CreateReplyResult.IPublishReplyResult>(new CreateReplyCmd(authorId, questionId, answer));


        public static Port<CheckLanguageResult.ICheckLanguageResult> CheckLanguage(string text)
            => NewPort<CheckLanguageCmd, CheckLanguageResult.ICheckLanguageResult>(new CheckLanguageCmd(text));


        public static Port<ReplyReceivedAckSentToQuestionOwnerResult.IReplyReceivedAckSentToQuestionOwnerResult> SendAckToQuestionOwner(int authorid, int questionid, string reply)
         => NewPort<ReplyReceivedAckSentToQuestionOwnerCmd, ReplyReceivedAckSentToQuestionOwnerResult.IReplyReceivedAckSentToQuestionOwnerResult>(new ReplyReceivedAckSentToQuestionOwnerCmd(authorid, questionid, reply));


        public static Port<ReplyPublishedAckSentToReplyAuthorResult.IReplyPublishedAckSentToReplyAuthorResult> SendAckToReplyAuthor(int replyid, int questionid, string reply)
        => NewPort<ReplyPublishedAckSentToReplyAuthorCmd, ReplyPublishedAckSentToReplyAuthorResult.IReplyPublishedAckSentToReplyAuthorResult>(new ReplyPublishedAckSentToReplyAuthorCmd(replyid, questionid, reply));

    } 
    
}

