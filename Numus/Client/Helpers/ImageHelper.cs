using Microsoft.AspNetCore.Components.Forms;
using Numus.Shared.Dtoes.Invoice;
using Numus.Shared.Dtoes.User;

namespace Numus.Client.Helpers
{
    public class ImageHelper
    {
        public static async Task ImageToByteArray(InputFileChangeEventArgs e, UserDto user)
        {
            var file = e.File;
            if (file != null)
            {
                using var stream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(stream);
                user.Image = stream.ToArray();
            }
            else
            {
                user.Image = null;
            }
        }
        public static async Task<byte[]?> ImageToByteArray(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                using var stream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(stream);
                return stream.ToArray();
            }
            else
            {
                return null;
            }
        }
        public static string ByteArrayToImage(byte[] imgData)
        {
            var imagesrc = Convert.ToBase64String(imgData);
            return string.Format("data:image/png;base64,{0}", imagesrc);
        }
    }
}
