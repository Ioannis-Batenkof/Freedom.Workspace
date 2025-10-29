using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freedom.Workspace
{
    internal static class Validate
    {
        public static FailedToCreateUserReasons User(User user)
        {
            return FailedToCreateUserReasons.Success;
        }
    }
}
