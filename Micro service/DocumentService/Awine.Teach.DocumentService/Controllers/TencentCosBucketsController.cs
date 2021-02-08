﻿using Awine.Teach.DocumentService.TencentCos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.DocumentService.Controllers
{
    /// <summary>
    /// 腾讯云对象存储存储桶
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TencentCosBucketsController : ControllerBase
    {
        private readonly ITencentCosHandler _cosHandler;

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
            return Ok();
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
            // return Ok();

            var cloudObjects = await _cosHandler.AllObjectsAsync(url, "", "");
            return Ok();
        }
    }
}
