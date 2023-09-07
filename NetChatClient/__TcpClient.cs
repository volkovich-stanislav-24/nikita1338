using System.Net;
using System.Net.Sockets;

namespace NetChatClient
{
    // Пусть конкретный клиент использует TCP.
    sealed class __TcpClient : IClient
    {
        // Выносим литералы в константы.
        const string __CONNECT_BEGIN = "Клиент соединяется с сервером {0}:{1}...";
        const string __CONNECT_END = "Клиент соединился с сервером {0}:{1}.";
        const string __DISCONNECT_BEGIN = "Клиент отсоединяется...";
        const string __DISCONNECT_END = "Клиент отсоединился.";
        const string __LISTEN = "Клиент слушает сервер {0}:{1}.";
        const string __LISTEN_ERROR = "Сервер {0}:{1} недоступен.";

        readonly TcpClient __platform;

        public void Disconnet()
        {
            Console.WriteLine(__DISCONNECT_BEGIN);
            __platform.Close();
            Console.WriteLine(__DISCONNECT_END);
        }

        public __TcpClient(ReadOnlySpan<char> ip, int port)
        {
            __platform = new TcpClient();

            // Конвертируем ip в string для WriteLine. К сожалению, WriteLine (и string.Format) не может взаимодействовать с ReadOnlySpan.
            string ip_as_string = ip.ToString();

            // Использум локальную функцию, чтобы выделить функционал соединения.
            // Передаём ip через аргумент, т.к. ref struct (которым является ReadOnlySpan) не может передаваться из контекста вышестоящей функции.
            void Connect(ReadOnlySpan<char> ip)
            {
                Console.WriteLine(__CONNECT_BEGIN, ip_as_string, port);
                // Выделяем переменную до цикла.
                var platform_ip = IPAddress.Parse(ip);
                // Бесконечно пытаемся соединиться.
                while (true)
                {
                    try
                    {
                        __platform.Connect(platform_ip, port);
                        // Если соединились, прекращаем.
                        break;
                    }
                    // Используем конкретное исключение (SocketException), чтобы ловить именно исключения соединения, а не, например, ошибки в аргументах.
                    catch (SocketException)
                    {
                        // Если не соединились, повторяем.
                    }
                }
                Console.WriteLine(__CONNECT_END);
            }

            void Listen()
            {
                Console.WriteLine(__LISTEN, ip_as_string, port);
                var bs = new byte[0];
                // Бесконечно слушаем.
                while (true)
                {
                    try
                    {
                        __platform.Client.Receive(bs);
                        // Если сообщение получено, продолжаем.
                    }
                    catch (SocketException)
                    {
                        Console.WriteLine(__LISTEN_ERROR, ip_as_string, port);
                        // Если сообщение не получено, отсоедияемся и прекращаем, т.к. сервер недоступен.
                        Disconnet();
                        break;
                    }
                }
            }

            Connect(ip);
            Listen();
        }
    }
}
