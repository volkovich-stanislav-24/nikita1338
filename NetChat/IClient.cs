namespace NetChat
{
    public interface IClient
    {
        public IServer Server { get; }
        public ulong Id { get; }

        public void Disconnet();
    }
}
