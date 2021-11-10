using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Restaurant_Management
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public bool IsShuttingDown { get; private set; }

        public new void Shutdown(int exitCode = 0)
        {
            this.IsShuttingDown = true;
            base.Shutdown(exitCode);
        }

    }
}
