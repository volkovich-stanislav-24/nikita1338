using System.Text;

namespace NetChat
{
    static class Program
    {
        const string __IP = "127.0.0.1";
        const int __PORT = 80;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Factory.Server(__IP, __PORT);
            Console.ReadKey();
        }
    }
}
