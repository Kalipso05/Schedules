using DataCenter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServer.Settings;

namespace WebServer.Requests
{
    public static class TrackRequests
    {
        public static async Task HandleGetTrack(HttpListenerRequest request, HttpListenerResponse response)
        {
            using (var db = new dbModel())
            {
                try
                {
                    // Получаем данные о местоположении всех сотрудников
                    var trackingInfo = await db.SecurityAccessLog.ToListAsync();

                    // Сериализуем данные в JSON
                    string jsonResponse = JsonConvert.SerializeObject(trackingInfo);

                    // Отправляем ответ
                    await Response.SendResponse(response, jsonResponse, "application/json", HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    // Логируем ошибку
                    Logger.Log($"Error retrieving tracking data: {ex.Message}", ConsoleColor.DarkRed, HttpStatusCode.InternalServerError);

                    // Отправляем ошибку клиенту
                    await Response.SendResponse(response, "Internal server error.", "application/json", HttpStatusCode.InternalServerError);
                }
            }
        }
    }

}
