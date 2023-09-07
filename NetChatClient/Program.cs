using System.Text;

namespace NetChatClient
{
    // Пусть программа создаёт клиент.
    static class Program
    {
        const string __IP = "127.0.0.1";
        const int __PORT = 80;

        static void Main(string[] args)
        {
            // Явно задаём Юникод (UTF16), чтобы поддерживать русский, т.к. консоль может по умолчанию использовать другую кодировку.
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            // Используя фабрику, создаём клиент.
            Factory.Client(__IP, __PORT);
            // Читаем любой ключ перед закрытием программы.
            Console.ReadKey();
        }
    }
}
