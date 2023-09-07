using System.Net;
using System.Net.Sockets;

namespace NetChat
{
    sealed class __TcpServer : __Server
    {
        const string __CONNECT_BEGIN = "Сервер {0}:{1} соединяется...";
        const string __CONNECT_END = "Сервер {0}:{1} соединился.";
        const string __LISTEN = "Сервер {0}:{1} слушает.";
        const string __CLIENT_CONNECT = "{0} соединился.";
        const string __CLIENT_DISCONNECT = "{0} отсоединился.";

        readonly TcpListener __platform;

        public override void Disconnect()
        {
            base.Disconnect();
            __platform.Stop();
        }

        public __TcpServer(ReadOnlySpan<char> ip, int port)
        {
            __platform = new TcpListener(IPAddress.Parse(ip), port);

            string ip_as_string = ip.ToString();

            void Connect()
            {
                Console.WriteLine(__CONNECT_BEGIN, ip_as_string, port);
                __platform.Start();
                Console.WriteLine(__CONNECT_END, ip_as_string, port);
            }

            void Listen()
            {
                Console.WriteLine(__LISTEN, ip_as_string, port);
                var bs = new byte[0];
                while (true)
                {
                    // Соединяем ожидающих соединения клиентов.
                    while (__platform.Pending())
                    {
                        var client = new __TcpClient(this, __platform.AcceptTcpClient());
                        _clients.Add(client);
                        Console.WriteLine(__CLIENT_CONNECT, client);
                    }

                    // Перебираем все клиенты в обратном порядке, т.к. нам может понадобиться удалять из _clients, изменяя Count.
                    for (var i = _clients.Count - 1; i >= 0; --i)
                    {
                        var client = (__TcpClient)_clients[i];
                        try
                        {
                            // Отправляем сообщение.
                            client.platform.Client.Send(bs);
                        }
                        catch (SocketException)
                        {
                            // Если отправка не удалась, отключаем клиента, т.к. он недоступен.
                            _clients.Remove(client);
                            client.Disconnet();
                            Console.WriteLine(__CLIENT_DISCONNECT, client);
                        }
                    }
                }
            }

            Connect();
            Listen();
        }
    }
}
