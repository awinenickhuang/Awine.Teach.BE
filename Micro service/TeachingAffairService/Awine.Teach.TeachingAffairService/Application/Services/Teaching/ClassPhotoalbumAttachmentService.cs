using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.Framework.Identity;
using Awine.Teach.TeachingAffairService.Application.Interfaces;
using Awine.Teach.TeachingAffairService.Application.ServiceResult;
using Awine.Teach.TeachingAffairService.Application.ViewModels;
using Awine.Teach.TeachingAffairService.Domain;
using Awine.Teach.TeachingAffairService.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awine.Teach.TeachingAffairService.Application.Services
{
    /// <summary>
    /// 相册管理-> 相片管理
    /// </summary>
    public class ClassPhotoalbumAttachmentService : IClassPhotoalbumAttachmentService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ClassPhotoalbumAttachmentService> _logger;

        /// <summary>
        /// 用户信息
        /// </summary>
        private readonly ICurrentUser _user;

        /// <summary>
        /// IClassPhotoalbumAttachmentRepository
        /// </summary>
        private readonly IClassPhotoalbumAttachmentRepository _classPhotoalbumAttachmentRepository;

        /// <summary>
        /// 构造 -> 注入需要的资源
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="user"></param>
        /// <param name="classPhotoalbumAttachmentRepository"></param>
        public ClassPhotoalbumAttachmentService(
            IMapper mapper,
            ILogger<ClassPhotoalbumAttachmentService> logger,
            ICurrentUser user,
            IClassPhotoalbumAttachmentRepository classPhotoalbumAttachmentRepository
            )
        {
            _mapper = mapper;
            _logger = logger;
            _user = user;
            _classPhotoalbumAttachmentRepository = classPhotoalbumAttachmentRepository;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <remarks>
        /// TODO：记录图片大小
        /// </remarks>
        public async Task<Result> Add(ClassPhotoalbumAttachmentAddViewModel model)
        {
            var existPhotos = await _classPhotoalbumAttachmentRepository.GetAll(tenantId: _user.TenantId, photoalbumId: model.PhotoalbumId);
            if (existPhotos != null)
            {
                if (existPhotos.Count() > 100)
                {
                    return new Result { Success = false, Message = "每个相册的相片张数最大为 100 张！" };
                }
            }

            //return new Result { Success = false, Message = "单个相册的最大可使用空间为 1G ！" };

            var entity = _mapper.Map<ClassPhotoalbumAttachmentAddViewModel, ClassPhotoalbumAttachment>(model);
            entity.TenantId = _user.TenantId;

            if (await _classPhotoalbumAttachmentRepository.Add(entity) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> Delete(string id)
        {
            if (await _classPhotoalbumAttachmentRepository.Delete(id) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }
            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ClassPhotoalbumAttachmentViewModel> GetModel(string id)
        {
            var entities = await _classPhotoalbumAttachmentRepository.GetModel(id);

            return _mapper.Map<ClassPhotoalbumAttachment, ClassPhotoalbumAttachmentViewModel>(entities);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="photoalbumId"></param>
        /// <returns></returns>
        public async Task<IPagedList<ClassPhotoalbumAttachmentViewModel>> GetPageList(int page = 1, int limit = 15, string photoalbumId = "")
        {
            var entities = await _classPhotoalbumAttachmentRepository.GetPageList(page, limit, _user.TenantId, photoalbumId);

            return _mapper.Map<IPagedList<ClassPhotoalbumAttachment>, IPagedList<ClassPhotoalbumAttachmentViewModel>>(entities);
        }
    }
}
