using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMenu.Helper;
using SystemMenu.Model;

namespace SystemMenu.Filter
{
    public class LogErrorFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //写入异常日志
            NLogHelper.WriteErrorFileLog($"\r\n【异常类型】:{context.Exception.GetType().Name};\r\n【异常信息】:{context.Exception.Message};\r\n【堆栈调用】:{context.Exception.StackTrace};\r\n==============================================================================================");
            //context.Result = new JsonResult(new { code = 500, msg = "服务器内部错误，请联系管理员" });
            context.Result = new JsonResult(new MessageModel<string> { Count = 0, success = false, msg = "服务器内部错误，请联系管理员", status = 500 });
        }
    }
}
