using Awine.Framework.Identity;
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
        /// <summary>
        /// 
        /// </summary>
        private readonly FileUploadOptions _fileUploadOptions;

        /// <summary>
        /// 
        /// </summary>
        private readonly CosUploadOptions _cosUploadOptions;

        /// <summary>
        /// 
        /// </summary>
        private readonly ITencentCosHandler _cosHandler;

        /// <summary>
        /// 
        /// </summary>
        private readonly ICurrentUser _currentUser;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileUploadOptionsAccessor"></param>
        /// <param name="cosUploadOptionsAccessor"></param>
        /// <param name="cosHandler"></param>
        /// <param name="currentUser"></param>
        public TencentCosController(
            IOptions<FileUploadOptions> fileUploadOptionsAccessor,
            IOptions<CosUploadOptions> cosUploadOptionsAccessor,
            ITencentCosHandler cosHandler,
            ICurrentUser currentUser)
        {
            _fileUploadOptions = fileUploadOptionsAccessor?.Value ?? throw new ArgumentNullException(nameof(fileUploadOptionsAccessor));
            _cosUploadOptions = cosUploadOptionsAccessor?.Value ?? throw new ArgumentNullException(nameof(cosUploadOptionsAccessor));
            _cosHandler = cosHandler ?? throw new ArgumentNullException(nameof(cosHandler));
            _currentUser = currentUser;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private StatusCodeResult NotValidUpload() => BadRequest();

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        /// <remarks>
        /// TODO:每次完成上传后返回文件大小，以限制每个租户的存储空间
        /// </remarks>
        [HttpPost("tencentcosupload")]
        public async Task<IActionResult> UploadCloudObject(IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            var uploadOptions = _cosUploadOptions;

            if (file.Length > uploadOptions.MaxLength)
            {
                return Response(success: false, message: "文件大小超限，文件大小最大为：2MB");
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
                return Response(success: false, message: "未找到指定存储位置");
            }

            //如果企业目录不存在，则创建
            //var tenantStorageUri = storageUri + $"/{_currentUser.TenantId}";
            //var tenantContainerExists = await _cosHandler.ExistsAsync(tenantStorageUri);
            //if (!tenantContainerExists)
            //{
            //    await _cosHandler.PutBucketAsync(_currentUser.TenantId, "ap-chengdu");
            //}

            var filePath = new Uri(new Uri(storageUri), file.FileName);
            var fileExists = await _cosHandler.ExistsAsync(filePath.ToString());

            if (fileExists && !uploadOptions.IsOverrideEnabled)
            {
                return Response(success: false, message: "未找到指定文件");
            }

            var uploadedUri = await _cosHandler.PutObjectAsync(filePath.ToString(), file.OpenReadStream());

            return Response(success: true, data: new UploadResult { UploadedUri = uploadedUri.OriginalString, FileName = file.FileName });
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="type">响应类型</param>
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
                        FileStreamResult fileStreamResult = null;
                        await _cosHandler.GetObjectAsync(fileUri.ToString(), stream =>
                        {
                            fileStreamResult = new FileStreamResult(stream, contentType);
                        });
                        return fileStreamResult;
                    }
                case "4":
                default:
                    {
                        FileContentResult fileStreamResult = null;
                        await _cosHandler.GetObjectAsync(fileUri.ToString(), stream =>
                        {
                            var len = stream.Length;
                            var bytes = new byte[len];
                            stream.Read(bytes, 0, (int)len);
                            fileStreamResult = new FileContentResult(bytes, contentType);
                        });
                        return fileStreamResult;
                    }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        [HttpPost("tencentcosdelete")]
        public async Task<IActionResult> DeleteCloudObject(string fileName)
        {
            var uploadOptions = _cosUploadOptions;

            var storageUri = uploadOptions.CosStorageUri;
            var containerExists = await _cosHandler.ExistsAsync(storageUri);

            if (!containerExists)
            {
                return Response(success: false, message: "未找到指定文件");
            }

            var filePath = new Uri(new Uri(storageUri), fileName);
            var fileExists = await _cosHandler.ExistsAsync(filePath.ToString());

            if (fileExists && !uploadOptions.IsOverrideEnabled)
            {
                return Response(success: false, message: "未找到指定文件");
            }

            if (await _cosHandler.DeleteObjectAsync(filePath.OriginalString))
            {
                return Response(success: true, message: "操作成功");
            }

            return Response(success: false, message: "操作失败");
        }
    }
}
