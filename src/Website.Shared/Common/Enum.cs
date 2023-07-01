using System.ComponentModel;

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
