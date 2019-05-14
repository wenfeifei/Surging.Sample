namespace Surging.Core.CPlatform.Runtime.Session
{
    public abstract class SurgingSessionBase : ISrcpSession
    {
        public abstract long? UserId { get; }
        public abstract string UserName { get; }

    }
}
