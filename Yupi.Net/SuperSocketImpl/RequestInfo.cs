namespace Yupi.Net.SuperSocketImpl
{
    public class RequestInfo : BinaryRequestInfo
    {
        public RequestInfo(byte[] body, bool isFlashRequest = false) : base("__MESSAGE__", body)
        {
            IsFlashRequest = isFlashRequest;
        }

        public bool IsFlashRequest { get; private set; }
    }
}