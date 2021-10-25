namespace ComicsLibrary.Helpers
{
    public class DebugState
    {
        public static string DatabaseFileName ()
        {
            var fileName = System.Configuration.ConfigurationManager.AppSettings["ComicStore"];
            return fileName;
        }
    }
}
