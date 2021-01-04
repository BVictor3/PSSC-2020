using GrainInterfaces;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldGrain
{
    public class HelloGrain : Orleans.Grain, IHello, IAsyncObserver<string>
    {
        private ILogger _logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            _logger = logger;
        }
        public Task<string> SayHello(string greeting)
        {
            _logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
        }

        public override async Task OnActivateAsync()
        {
            //Get one of the providers which we defined in config
            var streamProvider = GetStreamProvider("SMSProvider");
            //Get the reference to a stream
            var stream = streamProvider.GetStream<string>(Guid.Empty, "CHAT");
            //Set our OnNext method to the lambda which simply prints the data, this doesn't make new subscriptions because we are using implicit subscriptions via [ImplicitStreamSubscription].
            await stream.SubscribeAsync(this);

            await base.OnActivateAsync();
        }

        public Task OnNextAsync(string item, StreamSequenceToken token = null)
        {
            Console.WriteLine($"{this.GetPrimaryKeyString()} - {item}");
            return Task.CompletedTask;
        }

        public Task OnCompletedAsync()
        {
            throw new NotImplementedException();
        }

        public Task OnErrorAsync(Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
