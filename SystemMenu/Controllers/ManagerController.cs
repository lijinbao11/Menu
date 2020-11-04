using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SystemMenu.Helper;
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

        //[HttpGet]
        //public IActionResult GetManagers()
        //{
        //    var data = _dbContext.managers.ToList();
        //    return new JsonResult(new { code = 0, msg = "", count = data.Count, data });
        //}


        [HttpGet]
        public async Task<JsonResult> GetManagers(PageHelper pageHelper)
        {
            pageHelper.Count = await _dbContext.managers.CountAsync();
            if (pageHelper.Order == "asc")//升序
            {
                var data = await _dbContext.managers.OrderBy(c => c.Id).Skip((pageHelper.Page - 1) * pageHelper.Limit).Take(pageHelper.Limit).ToListAsync();

                return new JsonResult(new { code = 0, msg = "", data, count = pageHelper.Count });
            }
            //降序
            else {
                var data = await _dbContext.managers.OrderByDescending(c => c.Id).Skip((pageHelper.Page - 1) * pageHelper.Limit).Take(pageHelper.Limit).ToListAsync();

                return new JsonResult(new { code = 0, msg = "", data, count = pageHelper.Count });
            }
                
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
                //加密
                manager.Password = DesEncryptHelper.DesEncrypt(manager.Password);
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

        #region 删除 /批量删除
        [HttpGet]
        public IActionResult Delete(string id)
        {

            List<int> Ids = new List<int>();//创建list<int> 保存选中信息的Id 简化操作
            var ls = id.Split(',');  //根据 , 完成对数组的分组
            foreach (var item in ls)//foreach 循环遍历添加选中信息的Id
            {
                Ids.Add(Convert.ToInt32(item));//返回选中信息的Id
            }
            var vm = _dbContext.managers.Where(m => Ids.Contains(m.Id));//获取所要操作的行
            if (vm == null)
            {
                return Json(new { sussess = false, msg = "数据不存在或已经删除" });
            }
            vm.ToList().ForEach(t => _dbContext.Entry(t).State = EntityState.Deleted);//利用 Foreach() 方法 循环遍历删除选中行
            _dbContext.managers.RemoveRange(vm);//完成操作
            if (_dbContext.SaveChanges() > 0)//返回数据
            {
                return Json(new { success = true, msg = "删除成功" });
            }
            return Json(new { success = false, msg = "提交数据有误，请重新提交" });
            
            /*var manager = await _dbContext.managers.FindAsync(Ids);
            if (manager == null)
            {
                return Json(new { sussess = false, msg = "数据不存在或已经删除" });
            }

            _dbContext.managers.Remove(manager);

            if (_dbContext.SaveChanges() > 0)
            {
                return Json(new { success = true, msg = "删除成功" });
            }

            return Json(new { success = false, msg = "提交数据有误，请重新提交" });*/
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改 查询ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public IActionResult Edit(int id)
        //{
        //    //var manager = _dbContext.managers.FindAsync(id);
        //    var manager = _dbContext.managers.Where(c => c.Id == id).FirstOrDefault();
        //    if (manager == null)
        //    {
        //        return Redirect("/Home/Index");
        //    }
        //    return View(manager);
        //}
        
        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var manager = await _dbContext.managers.FindAsync(id);
            if (manager == null)
                return Redirect("/Home/Index");
            return View(manager);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="manger"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(Manager manger)
        {
            if (ModelState.IsValid)
            {
                manger.Password = DesEncryptHelper.DesEncrypt(manger.Password);
                _dbContext.Update(manger);
                if (await _dbContext.SaveChangesAsync() > 0)
                    return Json(new { success = true, msg = "修改成功" });
                return Json(new { success = false, msg = "修改失败" });
            }
            return Json(new { success = false, msg = "提交数据有误，请重新提交" });

        }


        #endregion
    }
}
