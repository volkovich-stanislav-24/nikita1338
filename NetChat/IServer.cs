namespace NetChat
{
    public interface IServer
    {
        public IReadOnlyList<IClient> Clients { get; }
        public IClient? GetClient(ulong id);

        public void Disconnect();
    }
}
