using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemMenu.Model;
using SystemMenu.Model.ViewModel;

namespace SystemMenu.Controllers
{
    public class LoginrecordController : Controller
    {
        public readonly SystemMenuDBContext _dbContext;

        public LoginrecordController(SystemMenuDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region 查  询

        /// <summary>
        /// 查询当前用户的登录记录
        /// </summary>
        /// <param name="pageHelper">分页帮助类</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<JsonResult> GetLoginrecord()
        {
            //获取登录的用户
            var mid = HttpContext.Session.GetInt32("mid");
            //pageHelper.Count = await _dbContext.loginrecords.CountAsync();
            var data = await (from n in _dbContext.loginrecords
                              join t in _dbContext.managers
                              on n.Mid equals t.Id
                              into n1
                              from t1 in n1.DefaultIfEmpty()
                              where t1.Id == mid
                              select new ManagerLogin
                              {
                                  Id = n.Id,
                                  Account = t1.Account,
                                  IPconfig = n.IPconfig,
                                  COMport = n.COMport,
                                  CreateTime = n.CreateTime
                              }).ToListAsync();

            return new JsonResult(new { code = 0, msg = "",  data });
        }

        #endregion
    }
}
