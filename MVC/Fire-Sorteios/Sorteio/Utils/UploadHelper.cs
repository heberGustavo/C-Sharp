using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sorteio.Portal.Utils
{
    public static class UploadHelper
    {
        //Img, pdf ...
        public static string UploadFile(byte[] fileBase64, string extensao)
        {
            var fileName = Guid.NewGuid().ToString() + extensao;

            var blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=cdnfiresorteios;AccountKey=B8Ksf9VqoXL0cUyHPoxH034cyu0zMQY3pnR5ne/Tim2VfNAFfMOqx0mHX0pvs595w0M2U8E4HZOfKNvWueYAIg==;EndpointSuffix=core.windows.net",
                                            "imagens", fileName);

            using (var stream = new MemoryStream(fileBase64))
            {
                blobClient.Upload(stream);
            }

            return blobClient.Uri.AbsoluteUri;
        }

        //public static string UploadBase64Image(string base64Image, string container)
        //{
        //    var fileName = Guid.NewGuid().ToString() + ".jpg";

        //    var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

        //    byte[] imageBytes = Convert.FromBase64String(data);

        //    var blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=cdnfiresorteios;AccountKey=B8Ksf9VqoXL0cUyHPoxH034cyu0zMQY3pnR5ne/Tim2VfNAFfMOqx0mHX0pvs595w0M2U8E4HZOfKNvWueYAIg==;EndpointSuffix=core.windows.net",
        //                                    "imagens", fileName);

        //    using (var stream = new MemoryStream(imageBytes))
        //    {
        //        blobClient.Upload(stream);
        //    }

        //    return blobClient.Uri.AbsoluteUri;
        //}
    }
}
