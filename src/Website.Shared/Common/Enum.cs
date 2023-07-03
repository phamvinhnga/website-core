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
            [Description("Something unexpected happened: {0}")]
            MessageWarning,

            [Description("An error occurred: {0}")]
            MessageError,

            [Description("Don't have permission")]
            NoPermission,

            Success
        }
    }
}
