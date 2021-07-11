using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsLibrary.Helpers
{
    public class DebugState
    {
        public string DatabaseFileName ()
        {
            var fileName = System.Configuration.ConfigurationManager.AppSettings["ComicStore"];
            return fileName;
        }
    }
}
