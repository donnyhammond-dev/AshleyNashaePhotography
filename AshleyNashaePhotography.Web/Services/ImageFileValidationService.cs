using AshleyNashaePhotography.Web.Enums;

namespace AshleyNashaePhotography.Web.Services
{
    public static class ImageFileValidationService
    {
        private static readonly byte[] JpegSignature = { 255, 216, 255 };

        private static readonly byte[] PngSignature = { 137, 80, 78, 71, 13, 10, 26, 10 };

        private static readonly byte[] WebpRiffSignature = { 82, 73, 70, 70 };

        private static readonly byte[] WebpSignature = { 87, 69, 66, 80 };

        private const long MaxImageSize = 10485760; /*10mb*/

        public static string? ValidateFile(IFormFile? file)
        {
            if (file == null || file.Length == 0)
            {
                return "Please select an image file.";
            }

            if (!IsValidSize(file))
            {
                return "File must not exceed 10 MB";
            }

            if (!IsValidExtension(file))
            {
                return "Please select a JPG, PNG, or WEBP photo.";
            }

            var imageType = DetectImageType(file);

            if (imageType == null)
            {
                return "The file is not a supported image format. " +
                    "Please upload a JPG, PNG, or WebP image.";
            }

            if (!DoesExtensionMatchFileType(file, imageType))
            {
                return "The image file extension does not match its actual format. " +
                    "Please select a valid image file.";
            }

            return null;
        }

        private static string? DetectImageType(IFormFile file)
        {
            if (file.Length < 12)
            {
                return null;
            }

            byte[] header = new byte[12];

            using var stream = file.OpenReadStream();

            stream.ReadExactly(header);

            bool isJpeg = header.AsSpan(0,3).SequenceEqual(JpegSignature);
            bool isPng = header.AsSpan(0, 8).SequenceEqual(PngSignature);
            bool isWebp = header.AsSpan(0, 4).SequenceEqual(WebpRiffSignature)
                && header.AsSpan(8, 4).SequenceEqual(WebpSignature);

            if (isJpeg) return "Jpeg";
            else if (isPng) return "Png";
            else if (isWebp) return "Webp";
            else return null;
        }

        private static bool IsValidSize(IFormFile file) => 
            file.Length > 0 && file.Length <= MaxImageSize;

        private static bool IsValidExtension(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName);

            return extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".png", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".webp", StringComparison.OrdinalIgnoreCase);
        }

        private static bool DoesExtensionMatchFileType(IFormFile file, string detectedType)
        {
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return detectedType.ToLowerInvariant() switch
            {
                "jpeg" => extension is ".jpg" or ".jpeg",
                "png" => extension == ".png",
                "webp" => extension == ".webp",
                _ => false
            };
        }

    }
}
