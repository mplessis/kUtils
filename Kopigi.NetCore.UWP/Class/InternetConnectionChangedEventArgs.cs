using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopigi.Portable.Class
{
    public class InternetConnectionChangedEventArgs : EventArgs
    {
        private readonly bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
        }

        public InternetConnectionChangedEventArgs(bool isConnected)
        {
            this._isConnected = isConnected;
        }
    }
}
