using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministrativoImperial.Portal.Utils
{
    public static class UploadHelper
    {
        //public static string UploadBase64Image(string base64Image, string container)
        //{
        //    var fileName = Guid.NewGuid().ToString() + ".jpg";

        //    var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(base64Image, "");

        //    byte[] imageBytes = Convert.FromBase64String(data);

        //    var blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=cdngapcontabilidade;AccountKey=L76erpaINHsNFXh4/HHqKxSBnmtk80VJYMPYmQmvYJAXlfchr8dIETP06xaI2S30t3cxJUZ4puXhIi5sJ14CUQ==;EndpointSuffix=core.windows.net",
        //                                    "arquivos", fileName);

        //    using (var stream = new MemoryStream(imageBytes))
        //    {
        //        blobClient.Upload(stream);
        //    }

        //    return blobClient.Uri.AbsoluteUri;
        //}

        //public static string UploadFile(byte[] fileBase64, string extensao)
        //{
        //    var fileName = Guid.NewGuid().ToString() + extensao;

        //    var blobClient = new BlobClient("DefaultEndpointsProtocol=https;AccountName=cdngapcontabilidade;AccountKey=L76erpaINHsNFXh4/HHqKxSBnmtk80VJYMPYmQmvYJAXlfchr8dIETP06xaI2S30t3cxJUZ4puXhIi5sJ14CUQ==;EndpointSuffix=core.windows.net",
        //                                    "arquivos", fileName);

        //    using (var stream = new MemoryStream(fileBase64))
        //    {
        //        blobClient.Upload(stream);
        //    }

        //    return blobClient.Uri.AbsoluteUri;
        //}
    }
}
