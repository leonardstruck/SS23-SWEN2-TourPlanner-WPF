using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.Log4Net
{
    public static class LoggerFactory
    {
        public static ILoggerWrapper GetLogger(string name)
        {
            return Log4NetWrapper.CreateLogger("./log4net.config", name);
        }
    }

}
