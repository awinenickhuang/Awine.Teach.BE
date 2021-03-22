using AutoMapper;
using Awine.Framework.Core.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Awine.WebSite.Applicaton.AutoMappr
{
    /// <summary>
    /// AutoMapper自定义PagedList集合映射
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class PagedListConverter<T1, T2> : ITypeConverter<IPagedList<T1>, IPagedList<T2>>
    {
        /// <summary>
        /// Init Mapper
        /// </summary>
        private IMapper _mapper { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mapper"></param>
        public PagedListConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Customer Mappings
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public IPagedList<T2> Convert(IPagedList<T1> source, IPagedList<T2> destination, ResolutionContext context)
        {
            var result = source.Items.Select(_mapper.Map<T1, T2>);
            return new PagedListMapping<T2>
            {
                Limit = source.Limit,
                Page = source.Page,
                TotalCount = source.TotalCount,
                HasPreviousPage = source.HasPreviousPage,
                HasNextPage = source.HasNextPage,
                Items = result.ToList()
            };
        }
    }
}
