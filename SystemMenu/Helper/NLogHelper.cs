﻿using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SystemMenu.Helper
{
    public class NLogHelper
    {
        public static Logger dbLogger = LogManager.GetLogger("logdb");
        public static Logger fileLogger = LogManager.GetLogger("logfile");
        public static Logger errorlogger = LogManager.GetLogger("errorflie");
        public static Logger warnlogger = LogManager.GetLogger("warnflie");



        /// <summary>
        /// 写日志到数据库
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logType">日志类型</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void WriteDBLog(string message, Exception exception = null)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Debug, dbLogger.Name, message);
            //theEvent.Properties["LogType"] = logType.ToString();
            theEvent.Exception = exception;
            dbLogger.Log(theEvent);
        }

        /// <summary>
        /// 写日志到文件
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logType">日志类型</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void WriteFileLog(string message, Exception exception = null)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, fileLogger.Name, message);
            //theEvent.Properties["LogType"] = logType.ToString();
            theEvent.Exception = exception;
            fileLogger.Log(theEvent);
        }

        /// <summary>
        /// 异常写日志到文件
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logType">日志类型</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void WriteErrorFileLog(string message, Exception exception = null)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Error, errorlogger.Name, message);
            //theEvent.Properties["LogType"] = logType.ToString();
            theEvent.Exception = exception;
            errorlogger.Log(theEvent);
        }

        /// <summary>
        /// 重要写日志到文件
        /// </summary>
        /// <param name="logLevel">日志等级</param>
        /// <param name="logType">日志类型</param>
        /// <param name="message">信息</param>
        /// <param name="exception">异常</param>
        public static void WriteWarnFileLog(string message, Exception exception = null)
        {
            LogEventInfo theEvent = new LogEventInfo(LogLevel.Info, errorlogger.Name, message);
            //theEvent.Properties["LogType"] = logType.ToString();
            theEvent.Exception = exception;
            warnlogger.Log(theEvent);
        }

        /// <summary>
        /// 确保NLog配置文件sql连接字符串正确
        /// </summary>
        /// <param name="nlogPath"></param>
        /// <param name="sqlConnectionStr"></param>
        public static void EnsureNlogConfig(string nlogPath, string sqlConnectionStr)
        {
            XDocument xd = XDocument.Load(nlogPath);
            if (xd.Root.Elements().FirstOrDefault(a => a.Name.LocalName == "targets")
                is XElement targetsNode && targetsNode != null &&
                targetsNode.Elements().FirstOrDefault(a => a.Name.LocalName == "target" && a.Attribute("name").Value == "log_database")
                is XElement targetNode && targetNode != null)
            {
                if (!targetNode.Attribute("connectionString").Value.Equals(sqlConnectionStr))//不一致则修改
                {
                    //这里暂时没有考虑dbProvider的变动
                    targetNode.Attribute("connectionString").Value = sqlConnectionStr;
                    xd.Save(nlogPath);
                    //编辑后重新载入配置文件（不依靠NLog自己的autoReload，有延迟）
                    LogManager.Configuration = new XmlLoggingConfiguration(nlogPath);
                }
            }
        }

    }
}
