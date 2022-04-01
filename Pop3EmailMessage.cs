using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPClientServer
{
    class Pop3EmailMessage
    {
        public long msgNumber;
        public long msgSize;
        public bool msgReceived;
        public string msgContent;
    }
}
