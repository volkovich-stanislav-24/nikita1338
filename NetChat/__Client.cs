namespace NetChat
{
    abstract class __Client : IClient
    {
        readonly __Server __server;
        public IServer Server => __server;
        ulong __id;
        public ulong Id => __id;

        public abstract void Disconnet();
        public override string ToString()
            => $"Клиент[{__id}]";

        protected __Client(__Server server)
        {
            __server = server;
            __id = 1;
            while (__server.GetClient(__id) != null)
                ++__id;
        }
    }
}
