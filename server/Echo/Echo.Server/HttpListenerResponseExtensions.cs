using System.Net;

namespace Echo.Server
{
    public static class HttpListenerResponseExtensions
    {
        public static void AsBadRequestAndClose(this HttpListenerResponse response)
        {
            response.StatusCode = 400;
            response.Close();
        }

        public static void AsInternalServerErrorAndClose(this HttpListenerResponse response)
        {
            response.StatusCode = 500;
            response.Close();
        }
    }
}
