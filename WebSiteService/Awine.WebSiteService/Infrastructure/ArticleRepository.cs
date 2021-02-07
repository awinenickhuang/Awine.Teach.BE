using Awine.Framework.Core.Collections;
using Awine.Framework.Dapper.Extensions.Options;
using Awine.WebSite.Domain;
using Awine.WebSite.Domain.Interface;
using Dapper;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Awine.WebSite.Infrastructure.Repository
{
    /// <summary>
    /// 文章管理
    /// </summary>
    public class ArticleRepository : IArticleRepository
    {
        /// <summary>
        /// MySQLProviderOptions
        /// </summary>
        private MySQLProviderOptions _mySQLProviderOptions;

        /// <summary>
        /// ILogger
        /// </summary>
        private readonly ILogger<ArticleRepository> _logger;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mySQLProviderOptions"></param>
        /// <param name="logger"></param>
        public ArticleRepository(MySQLProviderOptions mySQLProviderOptions, ILogger<ArticleRepository> logger)
        {
            this._mySQLProviderOptions = mySQLProviderOptions ?? throw new ArgumentNullException(nameof(mySQLProviderOptions));
            _logger = logger;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="forumId"></param>
        /// <param name="title"></param>
        /// <param name="author"></param>
        /// <param name="contentSource"></param>
        /// <returns></returns>
        public async Task<IPagedList<Article>> GetPageList(int page = 1, int limit = 15, string forumId = "", string title = "", string author = "", string contentSource = "")
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                DynamicParameters parameters = new DynamicParameters();

                sqlStr.Append(" SELECT * FROM t_article WHERE 1=1 ");

                if (!string.IsNullOrEmpty(forumId))
                {
                    sqlStr.Append(" AND ForumId=@ForumId ");
                    parameters.Add("ForumId", forumId);
                }

                if (!string.IsNullOrEmpty(title))
                {
                    sqlStr.Append(" and Title Like @Title");
                    parameters.Add("@Title", "%" + title + "%");
                }

                if (!string.IsNullOrEmpty(author))
                {
                    sqlStr.Append(" and Author Like @Author");
                    parameters.Add("@Author", "%" + author + "%");
                }

                if (!string.IsNullOrEmpty(contentSource))
                {
                    sqlStr.Append(" and ContentSource Like @ContentSource");
                    parameters.Add("@ContentSource", "%" + contentSource + "%");
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var list = await connection.QueryAsync<Article>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);

                return list.ToPagedList(page, limit);
            }
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <param name="forumId"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Article>> GetAll(string forumId, int number = 0)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                StringBuilder sqlStr = new StringBuilder();

                sqlStr.Append(" SELECT * FROM t_article WHERE 1=1 ");

                if (!string.IsNullOrEmpty(forumId))
                {
                    sqlStr.Append(" AND ForumId=@ForumId ");
                    parameters.Add("ForumId", forumId);
                }

                sqlStr.Append(" ORDER BY CreateTime DESC ");

                var enumerable = await connection.QueryAsync<Article>(sqlStr.ToString(), parameters, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
                if (number > 0)
                {
                    return enumerable.Take(number);
                }
                else
                {
                    return enumerable;
                }
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Article> GetModel(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_article WHERE Id=@Id ");
                return await connection.QueryFirstOrDefaultAsync<Article>(sqlStr.ToString(), new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 取一条数据
        /// </summary>
        /// <param name="forumId"></param>
        /// <returns></returns>
        public async Task<Article> GetModelForumAccording(string forumId)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append(" SELECT * FROM t_article WHERE ForumId=@ForumId ");
                return await connection.QueryFirstOrDefaultAsync<Article>(sqlStr.ToString(), new { ForumId = forumId }, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Add(Article model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("INSERT INTO t_article (Id,`Title`,`Content`,`Author`,ContentSource,ViewCount,ForumId,CoverPicture,CreateTime) VALUES (@Id,@Title,@Content,@Author,@ContentSource,@ViewCount,@ForumId,@CoverPicture,@CreateTime) ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(Article model)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                StringBuilder sqlStr = new StringBuilder();
                sqlStr.Append("UPDATE t_article SET `Title`=@Title,`Content`=@Content,`Author`=@Author,ContentSource=@ContentSource,ForumId=@ForumId,CoverPicture=@CoverPicture WHERE Id=@Id ");
                return await connection.ExecuteAsync(sqlStr.ToString(), model, commandTimeout: _mySQLProviderOptions.CommandTimeOut,
                    commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> Delete(string id)
        {
            using (var connection = new MySqlConnection(_mySQLProviderOptions.ConnectionString))
            {
                string sqlStr = "DELETE FROM t_article WHERE Id=@Id";
                return await connection.ExecuteAsync(sqlStr, new { Id = id }, commandTimeout: _mySQLProviderOptions.CommandTimeOut, commandType: CommandType.Text);
            }
        }
    }
}
