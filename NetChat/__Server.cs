namespace NetChat
{
    abstract class __Server : IServer
    {
        protected readonly List<IClient> _clients = new();
        public IReadOnlyList<IClient> Clients => _clients;
        public IClient? GetClient(ulong id)
        {
            foreach (var client in _clients)
                if (client.Id == id)
                    return client;
            return null;
        }

        public virtual void Disconnect()
        {
            foreach (var client in _clients)
                client.Disconnet();
        }
    }
}
