using Awine.Teach.DocumentService.TencentCos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Controllers
{
    /// <summary>
    /// 腾讯云对象存储 - 存储桶
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TencentCosBucketsController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ITencentCosHandler _cosHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cosHandler"></param>
        public TencentCosBucketsController(ITencentCosHandler cosHandler)
        {
            _cosHandler = cosHandler;
        }

        /// <summary>
        /// 存储桶列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("/buckets")]
        public async Task<IActionResult> AllBuckets()
        {
            var buckets = await _cosHandler.AllBucketsAsync();
            return Response(success: true, data: buckets);
        }

        /// <summary>
        /// 查询存储桶
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("/buckets/{url}")]
        public async Task<IActionResult> AllObjects(string url)
        {
            url = Uri.UnescapeDataString(url);

            var cloudObjects = await _cosHandler.AllObjectsAsync(url, "", "");

            return Response(success: true, data: cloudObjects);
        }
    }
}
