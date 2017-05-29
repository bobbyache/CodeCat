using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AvalonDockExamples
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Unhandled 'System.ComponentModel.Win32Exception' when using AvalonDock 2.0
        // One way to handle this is to simply do as below:
        //  http://stackoverflow.com/questions/37834945/unhandled-system-componentmodel-win32exception-when-using-avalondock-2-0
        // However, this prevents the application from shutting down so would need to be reset just before compiling the release.
        //  Or: Tools –> Options –> Debugging –> General –> Enable UI Debugging Tools for XAML
        // make it unchecked. https://avalondock.codeplex.com/workitem/17653
        
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //this.DispatcherUnhandledException += AppGlobalDispatcherUnhandledException;
        }

        //private void AppGlobalDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        //{
        //    //e.Handled = true;
        //}
    }
}
