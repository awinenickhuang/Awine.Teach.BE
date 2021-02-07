using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Awine.WebSiteService.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Awine.WebSiteService.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public class FileController : ApiController
    {
        /// <summary>
        /// IWebHostEnvironment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// IConfiguration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// FileController
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        /// <param name="configuration"></param>
        public FileController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="file">上传文件地址</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {
            if (file.Length > 1024 * 1024 * 20)
            {
                return Response(success: false, message: "文件大小不能超过20M");
            }
            string fileName = Path.GetFileName(file.FileName);//原始文件名称
            string fileExtension = Path.GetExtension(fileName);//文件扩展名
            string saveName = Guid.NewGuid().ToString("N") + fileExtension; //保存文件名称

            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string dir = DateTime.Now.ToString("yyyyMMdd");
            if (!System.IO.Directory.Exists(contentRootPath + $@"\wwwroot\uploadfiles\{dir}"))
            {
                System.IO.Directory.CreateDirectory(contentRootPath + $@"\wwwroot\uploadfiles\{dir}");
            }
            string filePath = Path.Combine(contentRootPath + $@"\wwwroot\uploadfiles\{dir}", saveName);

            FileInfo fileInfo = new FileInfo(filePath);
            try
            {
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                    fileInfo = new FileInfo(Path.Combine(contentRootPath, saveName));
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Flush();
                }
                return Response(success: true, message: $"上传成功", data: $"/uploadfiles/{dir}/{saveName}");
            }
            catch (Exception ex)
            {
                return Response(success: false, message: $"上传文件发生了错误:{ex.Message}", data: "");
            }
        }

        /// <summary>
        /// 文件上传 - layedit 有返回格式要求
        /// </summary>
        /// <param name="file">上传文件地址</param>
        /// <returns></returns>
        [HttpPost]
        [Route("layeditupload")]
        public async Task<LayeditUploadResult> LayeditFileUpload(IFormFile file)
        {
            if (file.Length > 1024 * 1024 * 20)
            {
                return new LayeditUploadResult() { Code = 400, Msg = "文件大小不能超过20M" };
            }
            string fileName = Path.GetFileName(file.FileName);//原始文件名称
            string fileExtension = Path.GetExtension(fileName);//文件扩展名
            string saveName = Guid.NewGuid().ToString("N") + fileExtension; //保存文件名称

            string contentRootPath = _webHostEnvironment.ContentRootPath;
            string dir = DateTime.Now.ToString("yyyyMMdd");
            if (!System.IO.Directory.Exists(contentRootPath + $@"\wwwroot\uploadfiles\{dir}"))
            {
                System.IO.Directory.CreateDirectory(contentRootPath + $@"\wwwroot\uploadfiles\{dir}");
            }
            string filePath = Path.Combine(contentRootPath + $@"\wwwroot\uploadfiles\{dir}", saveName);

            FileInfo fileInfo = new FileInfo(filePath);
            try
            {
                if (fileInfo.Exists)
                {
                    fileInfo.Delete();
                    fileInfo = new FileInfo(Path.Combine(contentRootPath, saveName));
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Flush();
                }
                return new LayeditUploadResult()
                {
                    Code = 0,
                    Msg = $"上传成功",
                    Data = new Data()
                    {
                        //layedit需要返回一个完整的路径
                        Src = $"{_configuration["FileServiceAddress"]}/uploadfiles/{dir}/{saveName}"
                    }
                };
            }
            catch (Exception ex)
            {
                return new LayeditUploadResult()
                {
                    Code = 500,
                    Msg = $"上传文件发生了错误:{ex.Message}",
                    Data = new Data()
                    {
                        Src = ""
                    }
                };
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public void DeleteImage(string filepath)
        {
            if (!string.IsNullOrEmpty(filepath))
            {
                string contentRootPath = _webHostEnvironment.ContentRootPath;
                string filePath = Path.Combine(contentRootPath + filepath);
                FileInfo file = new FileInfo(filePath);
                try
                {
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                catch (Exception ex)
                {
                    //
                }
            }
        }
    }
}