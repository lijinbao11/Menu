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


        #region 添加
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddManagers(Manager manager)
        {
            if (ModelState.IsValid)
            {
                manager.CreateTime = Convert.ToDateTime(DateTime.Now.ToString());
                manager.IsEnable = true;
                _dbContext.Add(manager);
                if ( _dbContext.SaveChanges() > 0)
                    return Json(new { success = true, msg = "新增成功" });
                return Json(new { success = false, msg = "提交失败" });
            }
            return Json(new { success = false, msg = "提交数据有误，请重新提交" });
        }
        #endregion

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var manager = await _dbContext.managers.FindAsync(id);
            if (manager == null)
            {
                return Json(new { sussess = false, msg = "数据不存在或已经删除" });
            }

            _dbContext.managers.Remove(manager);

                if ( _dbContext.SaveChanges() > 0)
                {
                    return Json(new { success = true, msg = "删除成功" });
                }

            return Json(new { success = false, msg = "提交数据有误，请重新提交" });
        }


        public IActionResult Edit()
        {
            return View();
        }
    }
}
