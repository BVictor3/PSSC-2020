using Access.Primitives.EFCore;
using Access.Primitives.IO;
using GrainInterfaces;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using StackUnderflow.Domain.Core.Contexts.Question;
using StackUnderflow.Domain.Core.Contexts.Question.CreateQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Question.GetQuestionRepyOp;
using StackUnderflow.Domain.Core.Contexts.Question.SendUserConfirmationOp;
using StackUnderflow.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackUnderflow.API.AspNetCore.Controllers
{

    [ApiController]
    [Route("question")]
    public class QuestionController : Controller
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly StackUnderflowContext _dbContext;
        private readonly IClusterClient _client; 

        public QuestionController(IInterpreterAsync interpreter, StackUnderflowContext dbContext, IClusterClient client)
        {

            _interpreter = interpreter;
            _dbContext = dbContext;
            _client = client;
        }

      

        [HttpPost("postquestion")]
        public async Task<IActionResult> CreateQuestionAsyncAndSendEmail([FromBody] CreateQuestionCmd createQuestionCmd)
        {
            QuestionWriteContext ctx = new QuestionWriteContext(
                new EFList<Post>(_dbContext.Post),
                new EFList<User>(_dbContext.User));

            var expr = from createQuestionResult in QuestionDomain.CreateQuestion(createQuestionCmd)
                       select createQuestionResult;

            var r = await _interpreter.Interpret(expr, ctx, dependencies);
            _dbContext.SaveChanges();
            return r.Match(
                created => Ok(created.Question.PostId),
                 notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created."),//todo return 500 (),
            invalidRequest => BadRequest("Invalid request."));
        }

        private TryAsync<ConfirmationAcknowledgement> SendEmail(ConfirmationLetter letter)
        => async () =>
        {
            var emailSender = _client.GetGrain<IEmailQuestionSender>(Guid.NewGuid());
            await emailSender.SendConfirmationEmailAsync(letter.Letter);

            var guid = Guid.Empty;
            var streamProvider = _client.GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<string>(guid, "LETTER");
            await stream.OnNextAsync("Hello event");
            return new ConfirmationAcknowledgement(Guid.NewGuid().ToString());
        };

        [HttpPost("question/{questionId}")]
        public async Task<IActionResult> CreateReply(int questionId, [FromBody] GetQuestionReplyCmd getQuestionReplyCmd)
        {
         


            QuestionWriteContext ctx = new QuestionWriteContext(
                new EFList<Post>(_dbContext.Post),
                new EFList<User>(_dbContext.User));


            var dependencies = new QuestionDependencies();
            var expr = from createQuestionResult in QuestionDomain.GetQuestionReply(getQuestionReplyCmd)
                       select createQuestionResult;


            var r = await _interpreter.Interpret(expr, ctx, dependencies);
            _dbContext.SaveChanges();
            return r.Match(
                created => Ok(created.Replies.PostId),
                 notCreated => StatusCode(StatusCodes.Status500InternalServerError, "Question could not be created."),//todo return 500 (),
            invalidRequest => BadRequest("Invalid request."));
        }
    }
}

