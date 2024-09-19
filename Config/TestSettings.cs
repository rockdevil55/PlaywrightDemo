using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightDemo.Config
{
    public class TestSettings
    {

        public bool Headless { get; set; }

        public bool DevTools { get; set; }

        public string Channel { get; set; }

        public int SlowMo { get; set; }

        public DriverType DriverType { get; set; }
    }


    public enum DriverType
    {
        Chromium,
        Firefox,
        Edge,
        Chrome,
        WebKit
    }
}
