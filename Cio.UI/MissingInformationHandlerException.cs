using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cio.UI
{
    public class MissingInformationHandlerException : CioException
    {
        public MissingInformationHandlerException()
            : base()
        {
        }

        public MissingInformationHandlerException(string message)
            : base(message)
        {
        }

        public MissingInformationHandlerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
