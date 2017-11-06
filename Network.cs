using System.Net;
using System.Net.Sockets;

namespace OPG.Signage.Info
{
    public static class Network
	{
        private static string _HostName = string.Empty;
        private static string _Ip = string.Empty;
        
        public static string HostName
        {
            get
            {
                if(_HostName == string.Empty)
                {
                    _HostName = Dns.GetHostName();
                }
                return _HostName;
            }
        }

		public static string Ip
		{
            get
            {
                if(_Ip == string.Empty)
                {
                    using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
                    {
                        socket.Connect("8.8.8.8", 65530);
                        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                        _Ip = endPoint.Address.ToString();
                    }
                }
                return _Ip;
            }
			
		}
	}
}
