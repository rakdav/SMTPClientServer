using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTPClientServer
{
    class PopMailException : ApplicationException
    {
        public PopMailException(string message) : base(message)
        {
        }
    }
}
