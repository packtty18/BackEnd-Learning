namespace Backend.Shared.Runtime
{
    public class ServerStartupOptions
    {
        public bool ShouldInitializeOnStart { get; }
        public bool ShouldLogResult { get; }

        public ServerStartupOptions(bool shouldInitializeOnStart, bool shouldLogResult)
        {
            ShouldInitializeOnStart = shouldInitializeOnStart;
            ShouldLogResult = shouldLogResult;
        }

        public static ServerStartupOptions Default()
        {
            return new ServerStartupOptions(true, true);
        }
    }
}
