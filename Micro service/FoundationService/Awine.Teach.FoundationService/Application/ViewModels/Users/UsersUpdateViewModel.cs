﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Awine.Teach.FoundationService.Application.ViewModels
{
    /// <summary>
    /// 系统账号 -> 更新 -> 视图模型
    /// </summary>
    public class UsersUpdateViewModel
    {
        /// <summary>
        /// 主键标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "主键标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "主键标识不正确")]
        public string Id { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Required]
        [StringLength(32, ErrorMessage = "邮箱长度为6-32", MinimumLength = 6)]
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号码必填")]
        [RegularExpression(@"^1[34578]\d{9}$", ErrorMessage = "手机号码无效")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 角色标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "角色标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "角色标识不正确")]
        public string RoleId { get; set; }

        /// <summary>
        /// 部门标识
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "部门标识必填")]
        [RegularExpression(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$", ErrorMessage = "部门标识不正确")]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required(ErrorMessage = "姓名必填"), MaxLength(6, ErrorMessage = "姓名太长啦"), MinLength(2, ErrorMessage = "姓名太啦")]
        [RegularExpression(@"^[\u4e00-\u9fa5]+$", ErrorMessage = "姓名必须是中文")]
        public string UserName { get; set; }

        /// <summary>
        /// 性别 1-男 2-女
        /// </summary>
        [Required(ErrorMessage = "性别必填")]
        [RegularExpression(@"^\+?[1-9][0-9]*$", ErrorMessage = "性别只能输入正整数")]
        public int Gender { get; set; }
    }
}
