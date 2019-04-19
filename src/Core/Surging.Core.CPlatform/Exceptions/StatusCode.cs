using System;
using System.Collections.Generic;
using System.Text;

namespace Surging.Core.CPlatform.Exceptions
{
    public enum StatusCode
    {
        Ok = 200,

        CommunicationError = 501,

        CPlatformError = 502,

        BusinessError = 503,

        ValidateError = 504,

        DataAccessError = 505,

        UnAuthentication = 401,

        UnAthorized = 402,

        RequestError = 506,

        UnKnownError = 0,


    }
}
