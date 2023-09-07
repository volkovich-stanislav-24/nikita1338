namespace NetChat
{
    public static class Factory
    {
        public static IServer Server(ReadOnlySpan<char> ip, int port)
            => new __TcpServer(ip, port);
    }
}
