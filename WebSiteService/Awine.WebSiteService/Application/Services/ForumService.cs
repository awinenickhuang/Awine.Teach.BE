using AutoMapper;
using Awine.Framework.Core.Collections;
using Awine.WebSite.Applicaton.Interface;
using Awine.WebSite.Applicaton.ServiceResult;
using Awine.WebSite.Applicaton.ViewModels;
using Awine.WebSite.Domain;
using Awine.WebSite.Domain.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Applicaton.Services
{
    /// <summary>
    /// 版块管理
    /// </summary>
    public class ForumService : IForumService
    {
        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<ForumService> _logger;

        /// <summary>
        /// IForumRepository
        /// </summary>
        private readonly IForumRepository _forumRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="forumRepository"></param>
        public ForumService(
            IMapper mapper,
            ILogger<ForumService> logger,
            IForumRepository forumRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _forumRepository = forumRepository;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        public async Task<IPagedList<ForumViewModel>> GetPageList(int page = 1, int limit = 15, int displayAttribute = 0, int contentAttribute = 0)
        {
            var entities = await _forumRepository.GetPageList(page, limit, displayAttribute, contentAttribute);

            return _mapper.Map<IPagedList<Forum>, IPagedList<ForumViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="displayAttribute"></param>
        /// <param name="contentAttribute"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ForumViewModel>> GetAll(int displayAttribute = 0, int contentAttribute = 0)
        {
            var entities = await _forumRepository.GetAll(displayAttribute, contentAttribute);

            return _mapper.Map<IEnumerable<Forum>, IEnumerable<ForumViewModel>>(entities);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ForumViewModel>> GetAllChilds(string parentId = "")
        {
            var entities = await _forumRepository.GetAllChilds(parentId);

            return _mapper.Map<IEnumerable<Forum>, IEnumerable<ForumViewModel>>(entities);
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ForumViewModel> GetModel(string id = "")
        {
            var entity = await _forumRepository.GetModel(id);

            return _mapper.Map<Forum, ForumViewModel>(entity);
        }

        /// <summary>
        /// 树型结构数据
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ForumTreeViewModel>> GetTreeList(string parentId = "")
        {
            var entities = await _forumRepository.GetAll();
            var result = _mapper.Map<IEnumerable<Forum>, IEnumerable<ForumTreeViewModel>>(entities);
            List<ForumTreeViewModel> rootNodes = new List<ForumTreeViewModel>();

            foreach (ForumTreeViewModel n in result)
            {
                if (!string.IsNullOrEmpty(parentId))
                {
                    if (n.Id.Equals(parentId))
                    {
                        n.Selected = true;
                    }
                }
                if (n.ParentId.Equals(Guid.Empty.ToString()))
                {
                    rootNodes.Add(n);
                    BuildChildNodes(n, result, parentId);
                }
            }

            return rootNodes;
        }

        #region 树型结构TreeSelect列表

        /// <summary>
        /// 获取父节点下所有的子节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private List<ForumTreeViewModel> GetChildNodes(ForumTreeViewModel parentNode, IEnumerable<ForumTreeViewModel> nodes)
        {
            List<ForumTreeViewModel> childNodes = new List<ForumTreeViewModel>();
            foreach (ForumTreeViewModel n in nodes)
            {
                if (parentNode.Id.Equals(n.ParentId))
                {
                    childNodes.Add(n);
                }
            }
            return childNodes;
        }

        /// <summary>
        /// 递归子节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="nodes"></param>
        /// <param name="moduleParentId"></param>
        private void BuildChildNodes(ForumTreeViewModel node, IEnumerable<ForumTreeViewModel> nodes, string moduleParentId = "")
        {
            List<ForumTreeViewModel> children = GetChildNodes(node, nodes);
            foreach (ForumTreeViewModel child in children)
            {
                if (!string.IsNullOrEmpty(moduleParentId))
                {
                    if (child.Id.Equals(moduleParentId))
                    {
                        child.Selected = true;
                    }
                }
                BuildChildNodes(child, nodes);
            }
            node.Children = children;
        }
        #endregion

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Add(ForumAddViewModel model)
        {
            var forum = _mapper.Map<ForumAddViewModel, Forum>(model);

            if (Guid.Empty.ToString().Equals(forum.ParentId) || null == forum.ParentId || string.Empty.Equals(forum.ParentId))
            {
                forum.ParentId = Guid.Empty.ToString();
            }

            if (await _forumRepository.Add(forum) > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> Update(ForumUpdateViewModel model)
        {
            var forum = _mapper.Map<ForumUpdateViewModel, Forum>(model);

            if (await _forumRepository.Update(forum) > 0)
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
            var childs = await _forumRepository.GetAllChilds(id);

            if (childs.Count() > 0)
            {
                return new Result { Success = false, Message = "操作失败：栏目下具有子栏目，请先删除子栏目！" };
            }

            var res = await _forumRepository.Delete(id);

            if (res > 0)
            {
                return new Result { Success = true, Message = "操作成功！" };
            }

            return new Result { Success = false, Message = "操作失败！" };
        }
    }
}
