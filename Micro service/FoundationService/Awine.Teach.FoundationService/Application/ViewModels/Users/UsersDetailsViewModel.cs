using System;
using System.Collections.Generic;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 获取当前登录用户的详细信息
    /// </summary>
    public class UsersDetailsViewModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 机构
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 租户类型 1-免费 2-试用 3-付费（VIP）8-代理商 9-平台运营
        /// </summary>
        public int ClassiFication { get; set; }

        /// <summary>
        /// VIP过期时间
        /// </summary>
        public DateTime VIPExpirationTime { get; set; }

        /// <summary>
        /// 行业类型名称
        /// </summary>
        public string IndustryName { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
