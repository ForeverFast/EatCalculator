namespace Clients.Maui.Helpers
{
    internal class PlatformHelper
    {
        public static bool IsDesktop
        {
            get
            {
#if WINDOWS || MACCATALYST
                return true;
#else
                return false;
#endif
            }
        }
    }
}
