using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemMenu.Model;

namespace SystemMenu.Controllers
{
    public class MenuController : Controller
    {
        private readonly SystemMenuDBContext _dbContext;

        public MenuController(SystemMenuDBContext dbContext)
        {

            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetMenuList() 
        {
            var menu = _dbContext.systemMenus.ToList();
            return new JsonResult(new { code = 0, msg = "", count = menu.Count, menu });
        }
    }
}
