using Cinema.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure
{
    interface IWasCreated
    {
        event WasCreated Notify;
        void Unregister();
    }
}
