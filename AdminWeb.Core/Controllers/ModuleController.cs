﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminWeb.Core.BasicData;
using AdminWeb.Core.IServices;
using AdminWeb.Core.Model;
using AdminWeb.Core.Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminWeb.Core.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [Route("api/Module")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class ModuleController : Controller
    {
        IModuleServices moduleServices;
        IsysUserInfoServices IsysUserInfoServices;
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="moduleServices"></param>
        /// <param name="IsysUserInfoServices"></param>
        public ModuleController(IModuleServices moduleServices, IsysUserInfoServices IsysUserInfoServices)
        {
            this.moduleServices = moduleServices;
            this.IsysUserInfoServices = IsysUserInfoServices;
        }

        /// <summary>
        /// 获取单个菜单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("GetModule")]
        [HttpGet]
        public async Task<object> GetModule(int Id)
        {
            var model = await moduleServices.GetModule(Id);
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = true,
                Data = model
            });
        }

        /// <summary>
        /// 获取菜单分页
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        [Route("ListPage")]
        [HttpPost]
        public IActionResult ListPage([FromBody] ModuleViewModels moduleViewModels)
        {
            var models = moduleServices.ListPageModules(moduleViewModels);
            return Ok(models);
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        [Route("AddModule")]
        [HttpPost]
        public async Task<IActionResult> AddModule([FromBody] ModuleViewModels moduleViewModels)
        {
            moduleViewModels.CreateId = BasicDataUser.UserId;
            moduleViewModels.CreateBy = BasicDataUser.UserName;
            moduleViewModels.CreateTime = DateTime.Now;
            var result = await moduleServices.AddModule(moduleViewModels);
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = result,
                Msg = result ? "菜单添加成功" : "菜单更新失败"
            });
        }


        /// <summary>
        /// 更新菜单信息
        /// </summary>
        /// <param name="moduleViewModels"></param>
        /// <returns></returns>
        [Route("UpdateModule")]
        [HttpPost]
        public async Task<IActionResult> UpdateModule([FromBody] ModuleViewModels moduleViewModels)
        {
            moduleViewModels.ModifyId = BasicDataUser.UserId;
            moduleViewModels.ModifyBy = BasicDataUser.UserName;
            moduleViewModels.ModifyTime = DateTime.Now;
            var result = await moduleServices.UpdateModule(moduleViewModels);
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = result,
                Msg = result?"菜单更新成功": "菜单更新失败"
            });
        }

        /// <summary>
        /// 删除菜单信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("DeleteModule")]
        [HttpGet]
        public async Task<IActionResult> DeleteModule(int Id)
        {
            var result = await moduleServices.DeleteModule(Id);
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = result,
                Msg = result? "菜单删除成功": "菜单删除失败"
            });
        }

        /// <summary>
        /// 获取当前的路由菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ListModule")]
        public IActionResult ListModule()
        {
            var result = moduleServices.ListClientModules();
            return Ok(new MessageModel<ModuleViewModels>()
            {
                Success = result.Count > 0 ? true : false,
                Datas = result
            });
        }
    }
}