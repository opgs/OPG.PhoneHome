using System.Reflection;

namespace OPG.Signage.Info
{
    public static class APP
	{
        private static string _AppVersion = string.Empty;

        public static string AppVersion
        {
            get
            {
                if(_AppVersion == string.Empty)
                {
                    AssemblyName name = Assembly.GetCallingAssembly().GetName();

                    _AppVersion = name.Version.Major.ToString() + "." + name.Version.Minor.ToString() + "." + name.Version.Build.ToString();
                }
                return _AppVersion;
            }
        }
	}
}
