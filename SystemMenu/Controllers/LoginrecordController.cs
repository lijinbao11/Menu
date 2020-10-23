using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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



        [HttpGet]
        public async Task<JsonResult> GetLoginrecord(PageHelper pageHelper)
        {
            pageHelper.Count = await _dbContext.loginrecords.CountAsync();
            var data =await (from n in _dbContext.loginrecords
                        join t in _dbContext.managers
                        on n.Mid equals t.Id
                        select new ManagerLogin
                        {
                            Id = n.Id,
                            Account = t.Account,
                            IPconfig = n.IPconfig,
                            COMport = n.COMport,
                            CreateTime = n.CreateTime
                        }).Skip((pageHelper.Page - 1) * pageHelper.Limit).Take(pageHelper.Limit).ToListAsync();

            return new JsonResult(new { code = 0, msg = "", count = pageHelper.Count, data });
        }
    }
}
