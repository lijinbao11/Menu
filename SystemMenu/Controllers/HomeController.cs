﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemMenu.Model;
using SystemMenu.Model.Entities.Permission;
using SystemMenu.Model.ViewModel;

namespace SystemMenu.Controllers
{
    public class HomeController : Controller
    {
        private readonly SystemMenuDBContext _dbContext;

        public HomeController(SystemMenuDBContext dbContext)
        {

            _dbContext = dbContext;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Index()
        {
            //获取登录的用户
            var username = HttpContext.Session.GetString("username");
            ViewBag.Account = username;
            //如果session是否为空 如果是空则返回到主页面
            if (string.IsNullOrWhiteSpace(username))
            {
                ViewBag.IsLogin = false;
                return Redirect("/Home/Login");
            }
            else
            {
                ViewBag.IsLogin = true;
            }
            return View();
        }
        public IActionResult Default()
        {
            return View();
        }

        #region 登录
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var managers = _dbContext.managers.Where(c => c.Account == username && c.IsEnable == true).ToList();
            if (managers.Count() == 0)
                return Json(new { success = false, msg = "用户不存在，请联系管理员" });
            if (!managers.FirstOrDefault().Password.Equals(password))
                return Json(new { success = false, msg = "密码错误，请重新输入" });
            //#region 生成cookie
            //var claimIdentity = new ClaimsIdentity("Cookie", JwtClaimTypes.Name, JwtClaimTypes.Role);
            //claimIdentity.AddClaims(new List<Claim>()
            //    {
            //        new Claim(JwtClaimTypes.Id,managers.FirstOrDefault().Id.ToString()),
            //        new Claim(JwtClaimTypes.Name,managers.FirstOrDefault().Account),
            //        //new Claim(JwtClaimTypes.Email,user.Email)
            //    });
            //#endregion
            //var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            //HttpContext.SignInAsync(claimPrincipal);
            //将用户名存储到session
            HttpContext.Session.SetString("username", username);
            return Json(new { success = true, msg = "登录成功" });
        }
        #endregion
        #region 登出
        [HttpGet]
        public  IActionResult LoginOut()
        {
            HttpContext.Session.Clear();
            return Json(new { success = true, msg = "登出成功" });
        }


        #endregion


        #region 菜单权限
        [HttpGet]
        public IActionResult GetMenuList()
        {
            SysTemMenus rootNode = new SysTemMenus()
            {
                Id = 0,
                Icon = "",
                Href = "",
                Title = "根目录",
            };
            var systemMenuEntities = _dbContext.systemMenus.Where(s => s.id > 0).ToList();

            //将rootNode的Child 赋值返回给 MenusInfoResultDTO.MenuInfo 返回给前端就行


            MenusInfoResultDTO menusInfoResultDTO = new MenusInfoResultDTO()
            {
                MenuInfo = GetTreeNodeListByNoLockedDTOArray(systemMenuEntities, rootNode),
                //rootNode.Child,
                LogoInfo = new LogoInfo(),
                HomeInfo = new HomeInfo()
            };


            return Json(menusInfoResultDTO);
        }
        /// <summary>
        /// 递归处理数据
        /// </summary>
        /// <param name="systemMenuEntities"></param>
        /// <param name="rootNode"></param>
        public static List<SysTemMenus> GetTreeNodeListByNoLockedDTOArray(List<SystemMenuEntity> systemMenuEntities, SysTemMenus rootNode)
        {
            if (systemMenuEntities == null || systemMenuEntities.Count() <= 0)
            {
                return null;
            }

            var childreDataList = systemMenuEntities.Where(p => p.pid == rootNode.Id);
            if (childreDataList != null && childreDataList.Count() > 0)
            {
                rootNode.Child = new List<SysTemMenus>();

                foreach (var item in childreDataList)
                {
                    SysTemMenus treeNode = new SysTemMenus()
                    {
                        Id = item.id,
                        Icon = item.icon,
                        Href = item.href,
                        Title = item.title,
                    };
                    rootNode.Child.Add(treeNode);
                }

                foreach (var item in rootNode.Child)
                {
                    GetTreeNodeListByNoLockedDTOArray(systemMenuEntities, item);
                }
            }
            return rootNode.Child;
        }
        #endregion
    }
}
