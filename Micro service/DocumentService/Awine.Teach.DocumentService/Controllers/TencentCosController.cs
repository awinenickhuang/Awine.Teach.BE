using Awine.Teach.DocumentService.Extensions;
using Awine.Teach.DocumentService.Models;
using Awine.Teach.DocumentService.TencentCos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Controllers
{
    /// <summary>
    /// 腾讯云对象存储
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TencentCosController : ApiController
    {
        private readonly FileUploadOptions _fileUploadOptions;

        private readonly CosUploadOptions _cosUploadOptions;

        private readonly ITencentCosHandler _cosHandler;

        public TencentCosController(
            IOptions<FileUploadOptions> fileUploadOptionsAccessor,
            IOptions<CosUploadOptions> cosUploadOptionsAccessor,
            ITencentCosHandler cosHandler)
        {
            _fileUploadOptions = fileUploadOptionsAccessor?.Value ?? throw new ArgumentNullException(nameof(fileUploadOptionsAccessor));
            _cosUploadOptions = cosUploadOptionsAccessor?.Value ?? throw new ArgumentNullException(nameof(cosUploadOptionsAccessor));
            _cosHandler = cosHandler ?? throw new ArgumentNullException(nameof(cosHandler));
        }

        private StatusCodeResult NotValidUpload() => BadRequest();

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("tencentcosupload")]
        public async Task<IActionResult> CosUpload(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var uploadOptions = _cosUploadOptions;

            if (file.Length > uploadOptions.MaxLength)
            {
                return Response(success: false, message: "文件大小超限");
            }

            string extension = Path.GetExtension(file.FileName);
            if (extension == null)
            {
                return Response(success: false, message: "文件类型不明确");
            }

            extension = extension.ToLowerInvariant();
            if (!uploadOptions.SupportedExtensions.Contains(extension))
            {
                return Response(success: false, message: "不被支持的文件类型");
            }

            var storageUri = uploadOptions.CosStorageUri;
            var containerExists = await _cosHandler.ExistsAsync(storageUri);

            if (!containerExists)
            {
                return Response(success: false, message: "未找到指定文件");
            }

            var filePath = new Uri(new Uri(storageUri), file.FileName);
            var fileExists = await _cosHandler.ExistsAsync(filePath.ToString());

            if (fileExists && !uploadOptions.IsOverrideEnabled)
            {
                return Response(success: false, message: "未找到指定文件");
            }

            var uploadedUri = await _cosHandler.PutObjectAsync(filePath.ToString(), file.OpenReadStream());

            return Response(success: true, data: uploadedUri);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpGet("tencentcos/{fileName}/{type?}")]
        public async Task<IActionResult> GetCloudObject(string fileName, string type)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var uploadOptions = _cosUploadOptions;

            var storageUri = uploadOptions.CosStorageUri;
            var fileUri = new Uri(new Uri(storageUri), fileName);

            string extension = Path.GetExtension(fileName);
            if (extension == null)
                return NotFound();

            extension = extension.ToLowerInvariant();
            if (!uploadOptions.SupportedExtensions.Contains(extension))
                return NotFound();

            var exists = await _cosHandler.ExistsAsync(fileUri.ToString());
            if (!exists)
                return NotFound();

            var contentType = ContentTypeHelper.GetContentType(extension);

            if (string.IsNullOrWhiteSpace(type)) type = "1";
            switch (type)
            {
                case "1":
                    {
                        var downloaded = await _cosHandler.GetObjectAsync(fileUri.ToString(), stream =>
                        {
                        });
                        return File(downloaded, contentType);
                    }
                case "2":
                    {
                        var downloaded = await _cosHandler.GetObjectAsync(fileUri.ToString(), stream =>
                        {
                        });

                        var len = downloaded.Length;
                        var bytes = new byte[len];
                        downloaded.Read(bytes, 0, (int)len);
                        return new FileContentResult(bytes, contentType);
                    }
                case "3":
                    {
                        FileStreamResult fs = null;
                        await _cosHandler.GetObjectAsync(fileUri.ToString(), stream =>
                        {
                            fs = new FileStreamResult(stream, contentType);
                        });
                        return fs;
                    }
                case "4":
                default:
                    {
                        FileContentResult fs = null;
                        await _cosHandler.GetObjectAsync(fileUri.ToString(), stream =>
                        {
                            var len = stream.Length;
                            var bytes = new byte[len];
                            stream.Read(bytes, 0, (int)len);
                            fs = new FileContentResult(bytes, contentType);
                        });
                        return fs;
                    }
            }
        }
    }
}
