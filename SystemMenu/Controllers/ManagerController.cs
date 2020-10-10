using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SystemMenu.Model;
using SystemMenu.Model.Entities.Permission;

namespace SystemMenu.Controllers
{
    public class ManagerController : Controller
    {
        private readonly SystemMenuDBContext _dbContext;

        public ManagerController(SystemMenuDBContext dbContext)
        {

            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetManagers()
        {
            var data = _dbContext.managers.ToList();
            return new JsonResult(new { code = 0, msg = "", count = data.Count, data });

        }
    }
}
