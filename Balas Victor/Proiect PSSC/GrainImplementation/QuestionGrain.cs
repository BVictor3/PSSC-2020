using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GrainInterfaces;
using Orleans.Streams;
using StackUnderflow.EF.Models;


namespace GrainImplementation
{
    public class QuestionGrain: Orleans.Grain, IEmailQuestionSender
    {
        private StackUnderflowContext _dbContext;
        private QuestionGrain state;
        public QuestionGrain(StackUnderflowContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task OnActivateAsync()
        {
            //read state from Db where postid = or parentid=
            //subscribe to reply states
            var streamProvider = GetStreamProvider("SMSProvider");
            var stream = streamProvider.GetStream<string>(Guid.Empty, "LETTER");
            await stream.SubscribeAsync((IAsyncObserver<string>)this);
            await base.OnActivateAsync();
        }

        public Task<string> SendConfirmationEmailAsync(string message)
        {
            return Task.FromResult(message);
        }

        //public GetQuestionWithReplies()
        //{

        //}
    }
}
