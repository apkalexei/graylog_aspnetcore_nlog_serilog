using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog.Sinks.Graylog.Transport;

namespace GrayLogConfigValues
{
    public class GrayLogConfig
    {
    public string Facility { get; set; }
    public string HostnameOrAdress { get; set; }
    public int Port { get; set; }
    public TransportType TransportType { get; set; }


}
}
