using Microsoft.AspNetCore.Http;
using RestWithNETUdemy.Data.VO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithNETUdemy.Business.Implementations
{
    public class FileBusinessImplamentation : IFileBusiness
    {
        private readonly IHttpContextAccessor _context;
        private readonly string _basePath;

        public FileBusinessImplamentation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public byte[] GetFile(string filename)
        {
            var filePath = _basePath + filename;
            return File.ReadAllBytes(filePath);
        }
        
        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            var fileDetail = new FileDetailVO();

            if (file != null && file.Length > 0)
            {
                var fileType = Path.GetExtension(file.FileName);
                var baseUrl = _context.HttpContext.Request.Host;

                if (fileType.ToLower().Equals(".pdf") || fileType.ToLower().Equals(".jpg") ||
                    fileType.ToLower().Equals(".png") || fileType.ToLower().Equals(".jpeg"))
                {
                    var docNome = Path.GetFileName(file.FileName);
                    var destination = Path.Combine(_basePath, "", docNome);

                    fileDetail.DocName = docNome;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }

            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(List<IFormFile> files)
        {
            var list = new List<FileDetailVO>();

            foreach (var file in files)
            {
                list.Add(await SaveFileToDisk(file));
            }

            return list;
        }

    }
}
