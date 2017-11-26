using dtso.core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace dtso.core.Utilties
{
    public class Error
    {
        public ErrorCode ErrorCode { get; set; }
        public string Message { get; set; }

        public Error()
        {
            ErrorCode = ErrorCode.OKAY;
        }
    }
}
