using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Website.Shared.Common
{

    public static class CoreEnum
    {
        public enum Folder
        {
            Post,
            Teacher,
            Parent
        }
        
        public enum Message
        {
            [Description("Don't have permission")]
            NoPermission,
            Success
        }
    }
}
