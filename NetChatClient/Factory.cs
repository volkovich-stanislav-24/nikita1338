namespace NetChatClient
{
    // Используем фабрику, чтобы скрыть создание конкретных типов.
    public static class Factory
    {
        // Используем ReadOnlySpan, чтобы допустить использование промежутков символьных коллекций без перераспределения памяти (например, как это делает string.Split, разделяя один string на несколько).
        public static IClient Client(ReadOnlySpan<char> ip, int port)
            => new __TcpClient(ip, port);
    }
}
