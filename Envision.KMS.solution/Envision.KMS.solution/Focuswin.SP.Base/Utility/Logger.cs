using log4net;
using log4net.Config;
//* ===================================
//* 类名称：Logger
//* 类描述：
//* 创建人：Ryan Ren
//* 创建时间：2015/10/1 11:14:08
//* 修改人：
//* 修改时间：
//* 修改备注：
//* @version 1.0
//* ===================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Focuswin.SP.Base.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class Logger
    {
        Type _objType;
        ILog _log;

        protected Logger(Type type)
        {
            _objType = type;
            _log = LogManager.GetLogger(type);
        }

        public static Logger GetLogger(Type type)
        {
            LoggerConfiger lc = LoggerConfiger.Default;
            return new Logger(type);
        }

        private IEnumerable<string> toStringArray(object[] objects)
        {
            foreach (var o in objects)
            {
                yield return GeneralHelper.ToString(o);
            }
        }

        public void DebugFormat(string message, params object[] objects)
        {
            if (_log.IsDebugEnabled)
            {
                Debug(string.Format(message, toStringArray(objects).ToArray()));
            }
        }

        public void DebugFormat(Exception ex, string message, params object[] objects)
        {
            if (_log.IsDebugEnabled)
            {
                Debug(string.Format(message, toStringArray(objects).ToArray()), ex);
            }
        }

        public void Debug(string message)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(message);
            }
        }

        public void Debug(object obj, Exception ex)
        {
            if (_log.IsDebugEnabled)
            {
                _log.Debug(FormatMessage(obj), ex);
            }
        }

        public void WarnFormat(string message, params object[] objects)
        {
            if (_log.IsWarnEnabled)
            {
                Warn(string.Format(message, toStringArray(objects).ToArray()));
            }
        }

        public void WarnFormat(Exception ex, string message, params object[] objects)
        {
            if (_log.IsWarnEnabled)
            {
                Warn(string.Format(message, toStringArray(objects).ToArray()), ex);
            }
        }

        public void Warn(string message)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(FormatMessage(message));
            }
        }

        public void Warn(object obj, Exception ex)
        {
            if (_log.IsWarnEnabled)
            {
                _log.Warn(FormatMessage(obj), ex);
            }
        }

        public void Error(string message)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(FormatMessage(message));
            }
        }

        public void ErrorFormat(string message, params object[] objects)
        {
            if (_log.IsErrorEnabled)
            {
                Error(string.Format(message, toStringArray(objects).ToArray()));
            }
        }

        public void ErrorFormat(Exception ex, string message, params object[] objects)
        {
            if (_log.IsErrorEnabled)
            {
                Error(string.Format(message, toStringArray(objects).ToArray()), ex);
            }
        }

        public void Error(object obj, Exception ex)
        {
            if (_log.IsErrorEnabled)
            {
                _log.Error(FormatMessage(obj), ex);
            }
        }

        private string FormatMessage(object o)
        {
            try
            {
                return FormatMessage(GeneralHelper.ToString(o));

            }
            catch (Exception ex)
            {
                _log.Error("format message error:" + o, ex);
                return o + "";
            }
        }

        private string FormatMessage(string message)
        {
            StringBuilder formattedMessage = new StringBuilder();
            formattedMessage.AppendLine(message);
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                formattedMessage.AppendFormat("Current HttpContext User:{1}, Request:{0}, Raw:{2}\r\n", HttpContext.Current.Request.Url, HttpContext.Current.User, HttpContext.Current.Request.RawUrl);
            }
            return formattedMessage.ToString();
        }

    }

    internal class LoggerConfiger
    {
        public const string Element_File = "file";
        public const string Attribute_Value = "value";
        private int defaultTimerInterval = 1000 * 60 * 5; //5 mins as default
        private Timer _timer;
        private string log4netConfigFilePath = "";
        private static Logger log = Logger.GetLogger(typeof(LoggerConfiger));

        private static LoggerConfiger defaultInstance = new LoggerConfiger();

        private string initErrorMsg = "";
        private long lastUpdateTicks = 0;

        private LoggerConfiger()
        {
            log4netConfigFilePath = getConfigFilePath();
            if (string.IsNullOrEmpty(log4netConfigFilePath))
                throw new Exception(string.Format("Cannot load log4net configuration file."));
            Configure();
            TimerCallback tcb = this.Configure;
            _timer = new Timer(tcb, null, defaultTimerInterval, defaultTimerInterval);
        }

        private void Configure()
        {
            Configure(null);
        }

        private string getConfigFilePath()
        {
            string configPath = null;

            //get log path from web config

            if (string.IsNullOrEmpty(configPath))
            {
                var f = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config");
                initErrorMsg += string.Format("Domain BaseDir:{0}", f);
                if (File.Exists(f))
                {
                    configPath = f;
                }
            }

            if (string.IsNullOrEmpty(configPath))
            {
                var f = @"C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\15\CONFIG\Focuswin\log4net.config";
                if (File.Exists(f))
                {
                    configPath = f;
                }
            }

            return configPath;
        }


        private void Configure(Object stateInfo)
        {
            lock (this)
            {
                FileInfo f = new FileInfo(log4netConfigFilePath);

                if (f.LastWriteTime.Ticks > lastUpdateTicks)
                {
                    using (Stream s = f.OpenRead())
                    {
                        XElement xml = XElement.Load(new StreamReader(s));
                        foreach (var e in xml.Descendants(Element_File))
                        {
                            var attr = e.Attributes(Attribute_Value).LastOrDefault();
                            if (attr != null)
                            {
                                string process = string.Format("{0}({1})", Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id);
                                attr.Value = string.Format(attr.Value, process);
                            }
                        }
                        XmlDocument doc = new XmlDocument();
                        doc.InnerXml = xml.ToString();
                        XmlConfigurator.Configure(doc.DocumentElement);
                        lastUpdateTicks = f.LastWriteTime.Ticks;
                    }
                }
                log.DebugFormat("log4net loaded successfully from {0}", log4netConfigFilePath);
            }
        }

        public static LoggerConfiger Default
        {
            get { return defaultInstance; }
        }
    }
}
