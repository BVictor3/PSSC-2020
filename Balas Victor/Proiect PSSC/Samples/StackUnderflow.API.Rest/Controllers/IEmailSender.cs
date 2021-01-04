using System.Threading.Tasks;

namespace StackUnderflow.API.Rest.Controllers
{
    internal interface IEmailSender
    {
        Task SendEmailAsync(string letter);
    }
}