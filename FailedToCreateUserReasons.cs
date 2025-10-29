using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freedom.Workspace
{
    internal enum FailedToCreateUserReasons
    {
        Success,
        UserIsNull,
        UsernameIsMissing,
        PasswordIsMissing,
    }
}
