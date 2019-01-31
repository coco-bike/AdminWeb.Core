﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AdminWeb.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Core.Controllers
{
    /// <summary>
    /// 用户权限控制器所有接口
    /// </summary>
    [Authorize(Policy = "Admin")]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserRoleController : Controller
    {
        IsysUserInfoServices sysUserInfoServices;
        IUserRoleServices userRoleServices;
        IRoleServices roleServices;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sysUserInfoServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="roleServices"></param>
        public UserRoleController(IsysUserInfoServices sysUserInfoServices, IUserRoleServices userRoleServices, IRoleServices roleServices)
        {
            this.sysUserInfoServices = sysUserInfoServices;
            this.userRoleServices = userRoleServices;
            this.roleServices = roleServices;
        }



        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPWD"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddUser(string loginName, string loginPWD)
        {
            var model = await sysUserInfoServices.SaveUserInfo(loginName, loginPWD);
            return Ok(new
            {
                success = true,
                data = model
            });
        }

        /// <summary>
        /// 新建Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddRole(string roleName)
        {
            var model = await roleServices.SaveRole(roleName);
            return Ok(new
            {
                success = true,
                data = model
            });
        }

        /// <summary>
        /// 新建用户角色关系
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> AddUserRole(int uid, int rid)
        {
            var model = await userRoleServices.SaveUserRole(uid, rid);
            return Ok(new
            {
                success = true,
                data = model
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetRoles(string token)
        {
            return Ok(new
            {
                success = true,
                token= token,
                roles =new List<string>(){ "admins"}
            });
        }



    }
}
