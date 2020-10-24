using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //登录成功,执行将用户名存储到session  每登录一次记录一次
            //将用户名存储到session
            HttpContext.Session.SetString("username", username);
            //将用户ID传给添加登录日志的方法
            AddLoginrecord(managers.FirstOrDefault().Id);
            return Json(new { success = true, msg = "登录成功" });
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
        }
        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="id">用户ID</param>
        private void AddLoginrecord(int id)
        {
            Loginrecord loginrecord = new Loginrecord();
            if (ModelState.IsValid)
            {
                //Request.HttpContext.Connection.RemoteIpAddress.ToString()  IPV6
                //Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                loginrecord.IPconfig = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
                loginrecord.COMport = Request.HttpContext.Connection.RemotePort;
                loginrecord.CreateTime = Convert.ToDateTime(DateTime.Now.ToString());
                loginrecord.Mid =id ;
                _dbContext.Add(loginrecord);
                _dbContext.SaveChanges();
            }
            
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
                id = 0,
                icon = "",
                href = "",
                title = "根目录",
            };
            var systemMenuEntities = _dbContext.systemMenus.Where(s => s.Id > 0).ToList();

            //将rootNode的Child 赋值返回给 MenusInfoResultDTO.MenuInfo 返回给前端就行


            MenusInfoResultDTO menusInfoResultDTO = new MenusInfoResultDTO()
            {
                menuInfo = GetTreeNodeListByNoLockedDTOArray(systemMenuEntities, rootNode),
                //rootNode.Child,
                logoInfo = new LogoInfo(),
                homeInfo = new HomeInfo()
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
            var childreDataList = systemMenuEntities.Where(p => p.Pid == rootNode.id);
            if (childreDataList != null && childreDataList.Count() > 0)
            {
                rootNode.Child = new List<SysTemMenus>();

                foreach (var item in childreDataList)
                {
                    SysTemMenus treeNode = new SysTemMenus()
                    {
                        id = item.Id,
                        icon = item.Icon,
                        href = item.Href,
                        title = item.Title,
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
