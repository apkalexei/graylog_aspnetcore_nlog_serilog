using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Events;
//using Serilog.Sinks.Graylog.Transport;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using GrayLogConfigValues;

namespace aspnetcore_graylog_sample_with_nlog.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;
        private readonly GrayLogConfig  _myValues;
       
        public HomeController(ILogger<HomeController> logger, IOptions<GrayLogConfig> myvalues)
        {
            _logger = logger;
            _myValues = myvalues.Value;
        }
        
        public IActionResult Index()
        {
            // EventId id = new EventId({ "Id":0,"Name":null})


            //*********************************  SERILOG ******************************************************/
            var loggerConfig = new LoggerConfiguration();

            loggerConfig.WriteTo.Graylog(new GraylogSinkOptions
            {
                ShortMessageMaxLength = 50,
                MinimumLogEventLevel = LogEventLevel.Information,
                Facility = _myValues.Facility,  // _configuration.GetValue<string>("GrayLog:Facility"),    //_configuration.GetSection("GrayLog").GetSection("Facility").
                HostnameOrAdress = _myValues.HostnameOrAdress,  // _configuration.GetValue<string>("GrayLog:HostnameOrAdress"),   //"logs.apedemonte.int",
                Port = _myValues.Port,  // Convert.ToInt32(_configuration.GetValue<Int32>("GrayLog:Port")),  // 12201,
                TransportType = _myValues.TransportType  // _configuration.GetValue<TransportType>("GrayLog:TransportType")
            });

            var test = new GrayLogConfig
            {
                Facility = "1",
                HostnameOrAdress = "1",
                Port = 3,
                TransportType = 0
            };


            var logger = loggerConfig.CreateLogger();

            try
            {
                try
                {
                    throw new InvalidOperationException("Level exception");
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException("Nested Exception", ex);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Test exception with {@test}", "SeriLog");
            }



            //*************************************************************************************************/
            // NLOG
            if (_logger.IsEnabled(LogLevel.Error))
            {
                _logger.LogError("Logging NLog", "test exception");
            }

       

            return View();
        }



        
    }
}