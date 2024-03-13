using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer.Settings
{
    public static class Response
    {
        public static async Task SendResponse(HttpListenerResponse response, string content, string contentType = "application/json", HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            byte[] data = Encoding.UTF8.GetBytes(content);

            response.ContentType = contentType;
            response.ContentLength64 = data.Length;
            response.StatusCode = (int)statusCode;

            // Использование буфера для передачи данных частями
            int bufferSize = 1024 * 64; // 64KB, можно адаптировать под ваш случай
            for (int i = 0; i < data.Length; i += bufferSize)
            {
                int chunkSize = Math.Min(bufferSize, data.Length - i);
                await response.OutputStream.WriteAsync(data, i, chunkSize);
            }

            response.Close();
        }
    }
}
