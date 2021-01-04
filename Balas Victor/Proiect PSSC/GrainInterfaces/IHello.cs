using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
