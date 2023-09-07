using System.Net.Sockets;

namespace NetChat
{
    sealed class __TcpClient : __Client
    {
        public readonly TcpClient platform;

        public override void Disconnet()
            => platform.Close();

        public __TcpClient(__TcpServer server, TcpClient platform) : base(server)
            => this.platform = platform;
    }
}
