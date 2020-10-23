using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemMenu.Helper;

namespace SystemMenu.Filter
{
    /// <summary>
    /// 日志过滤器
    /// </summary>
    public class LogActionFilter: ActionFilterAttribute
    {
        /// <summary>
        /// 方法执行之前
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
        /// <summary>
        /// 方法执行之后
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        /// <summary>
        /// 方法返回之前
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }


        /// <summary>
        /// 方法返回之后
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            //请求方法
            var RequestMethod = context.HttpContext.Request.Method;
            //请求参数
            var RequestQuerystring = context.HttpContext.Request.QueryString.Value.ToString();
            //请求ip
            var RequestIp = context.HttpContext.Request.Host;
            //请求路径
            var RequestPath = context.HttpContext.Request.Path;
            //返回结果
            //var Result = (context.Result as JsonResult)?.Value.ToString();
            //返回状态
            var StatusCode = context.HttpContext.Response.StatusCode;

            NLogHelper.WriteFileLog($"\r\n 【请求方法】:{RequestMethod};\r\n 【请求参数】:{RequestQuerystring};\r\n 【请求者IP】:{RequestIp};\r\n 【请求路径】:{RequestPath};\r\n 【返回状态】:{StatusCode};\r\n=====================================================================");
            base.OnResultExecuted(context);
        }
    }
}
