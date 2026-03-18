using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsEntry;

internal class Program
{
    /// <summary>
    /// Application Entry Point.
    /// </summary>
    [System.STAThreadAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "10.0.3.0")]
    public static void Main(string[] args)
    {
        ComicsEntry.App app = new ComicsEntry.App(args);
        app.InitializeComponent();
        app.Run();
    }
}
