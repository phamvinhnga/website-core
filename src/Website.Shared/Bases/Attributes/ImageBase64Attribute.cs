using HeyRed.Mime;
using Serilog;
using System;
using System.ComponentModel.DataAnnotations;
using Website.Shared.Common;
using Website.Shared.Extensions;

namespace Website.Shared.Bases.Attributes
{
    public class ImageBase64Attribute : ValidationAttribute
    {
        public int MaxSizeMb { get; set; }

        public ImageBase64Attribute()
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(string.Format(CoreEnum.Message.MessageValidImageBase64.GetEnumDescription(), validationContext.DisplayName));
            }

            string stringValue = value.ToString();

            // Check if the value is a valid URL
            if (IsUrlValid(stringValue))
            {
                return ValidationResult.Success;
            }

            // Check if the value is a valid base64-encoded image data
            if (IsImageBase64(stringValue))
            {
                // Check if the base64 string has a valid size
                if (!IsValidImageSize(stringValue))
                {
                    return new ValidationResult(string.Format(CoreEnum.Message.MessageValidImageMaximumSize.GetEnumDescription(), validationContext.DisplayName, MaxSizeMb));
                }
                return ValidationResult.Success;
            }

            return new ValidationResult(string.Format(CoreEnum.Message.MessageValidImageBase64.GetEnumDescription(), validationContext.DisplayName));
        }

        private bool IsUrlValid(string value)
        {
            return Uri.TryCreate(value, UriKind.Absolute, out Uri uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private bool IsImageBase64(string base64String)
        {
            try
            {
                var mimeType = MimeGuesser.GuessMimeType(base64String);
                return mimeType.StartsWith("image/");
            }
            catch (Exception ex)
            {
                Log.Error(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return false;
            }
        }

        private bool IsValidImageSize(string base64String)
        {
            try
            {
                var imageData = Convert.FromBase64String(base64String);

                // Convert MaxSizeMb from MB to bytes
                int maxSizeBytes = MaxSizeMb * 1024 * 1024;
                return imageData.Length <= maxSizeBytes;
            }
            catch (Exception ex)
            {
                Log.Error(ex, CoreEnum.Message.MessageError.GetEnumDescription(), ex.Message);
                return false;
            }
        }
    }
}
