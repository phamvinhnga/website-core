using System.ComponentModel;

namespace Website.Shared.Common
{
    public static class CoreEnum
    {
        public enum Folder
        {
            Post,
            Teacher,
            Parent,
            ClassRoom,
            Gallery,
            Facility
        }
        
        public enum Message
        {
            [Description("Something unexpected happened: {0}")]
            MessageWarning,

            [Description("An error occurred: {0}")]
            MessageError,

            [Description("Don't have permission")]
            NoPermission,

            [Description("EntityId {0} cannot found")]
            MessageEntityNotFound,

            [Description("The field {0} must be a valid image in base64 format.")]
            MessageValidImageBase64,

            [Description("File must be an image with a maximum size of {0}MB.")]
            MessageValidImageMaximumSize,

            Success
        }

        public enum Category
        {
            Gallery
        }
    }
}
