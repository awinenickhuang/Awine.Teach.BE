using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Awine.Teach.DocumentService.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Awine.Teach.DocumentService.Controllers
{
    /**
     * 
     * 先实现简单的统一文件管理功能
     * 
     * 1-文件上传
     * 2-文件删除
     * 
     * TO DO
     * 
     * 1-文件查看  文件权限  文件夹操作  文件操作
     * 
     * 2-流式大文件上传支持
     * 
     * **/

    /// <summary>
    /// 文件管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors(PolicyName = "ccar142fileservice")]
    //[Authorize]
    public class FileManagementController : ControllerBase
    {
        /// <summary>
        /// Host Environment
        /// </summary>
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// 文件配置
        /// </summary>
        private readonly AwineFileOptions _fileOptions;

        /// <summary>
        /// Configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="fileOptions"></param>
        /// <param name="configuration"></param>
        public FileManagementController(IWebHostEnvironment environment, IOptions<AwineFileOptions> fileOptions, IConfiguration configuration)
        {
            _environment = environment;
            _fileOptions = fileOptions.Value;
            _configuration = configuration;
        }

        /// <summary>
        /// 文件上传 -> 单个文件上传 -> 返回文件绝对路径
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("singlefileupload")]
        public async Task<IActionResult> UploadFilesAsync(IFormFile file)
        {
            if (null == file)
            {
                return BadRequest(new { success = false, message = "上传文件不能为空" });
            }

            if (file.Length <= 0)
            {
                return BadRequest(new { success = false, message = "上传文件不能为空" });
            }

            if (file.Length >= this._fileOptions.MaxSize)
            {
                return BadRequest(new { success = false, message = $"文件大小不能超过{this._fileOptions.MaxSize / (1024f * 1024f)}M" });
            }

            try
            {
                var fileExtension = Path.GetExtension(file.FileName);

                if (this._fileOptions.FileTypes.IndexOf(fileExtension) >= 0)
                {
                    string newfileName = System.Guid.NewGuid().ToString("N") + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _fileOptions.FileBaseUrl, newfileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                        stream.Flush();
                    }
                    return Ok(new { code = 200, success = true, message = "上传成功", filepath = _configuration["ServerAddress"] + "/" + newfileName });
                }
                else
                {
                    return BadRequest(new { code = 400, success = false, message = "不被支持的文件类型" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 500, success = false, message = $"出错了: {ex.Message}" });
            }
        }
    }
}