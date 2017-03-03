using System;
using Kopigi.Portable.Class;

namespace Kopigi.Portable.Interfaces
{
    public interface INetworkService
    {
        event EventHandler<InternetConnectionChangedEventArgs> InternetConnectionChanged;

        bool IsConnected { get; }
    }
}
