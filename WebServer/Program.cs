using DataCenter.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebServer.Requests;
using WebServer.Settings;

namespace WebServer
{
    internal class Program
    {
        static HttpListener server;

        private static async Task RouteRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            var path = request.Url.AbsolutePath;
            var method = request.HttpMethod;

            if (path.StartsWith("/api") && path.TrimEnd('/') == "/api")
            {
                if(method == "GET")
                {
                    string htmlFilePath = "";
                    string htmlContent = File.ReadAllText(htmlFilePath);
                    await Response.SendResponse(response, htmlContent = "text/html");
                }
            }
            else if (path.StartsWith("/api/Patient"))
            {
                switch (method)
                {
                    case "GET":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await PatientRequests.HandleGetPatient(response, request);
                        break;
                    case "POST":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await PatientRequests.HandlePostPatient(request, response);
                        break;
                    case "PUT":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        
                        break;
                    case "DELETE":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);

                        break;
                    default:
                        response.StatusCode = 404;
                        break;
                }
            }
            else if (path.StartsWith("/api/track"))
            {
                switch (method)
                {
                    case "GET":
                        // Логика для возврата данных о последнем местоположении сотрудников
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await TrackRequests.HandleGetTrack(request, response);
                        break;
                    default:
                        response.StatusCode = 405; // Method Not Allowed
                        await Response.SendResponse(response, "Method Not Allowed", "text/plain");
                        break;
                }
            }
            else if (path.StartsWith("/api/login"))
            {
                switch (method)
                {
                    case "POST":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await DoctorRequests.HandleAuthUser(request, response);
                        break;
                }
            }
            else if (path.StartsWith("/api/schedules"))
            {
                switch (method)
                {
                    case "GET":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await DoctorRequests.HandleGetSchedules(request, response);
                        break;
                }
            }
            else if (path.StartsWith("/api/filterSpeciality"))
            {
                switch (method)
                {
                    case "GET":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await DoctorRequests.HandleGetSchedulesSpeciality(request, response);
                        break;
                }
            }
            else if (path.StartsWith("/api/specialities"))
            {
                switch (method)
                {
                    case "GET":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await DoctorRequests.HandleGetSpecialities(request, response);
                        break;
                }
            }
            else if (path.StartsWith("/api/doctors"))
            {
                switch (method)
                {
                    case "GET":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await DoctorRequests.HandleGetDoctors(request, response);
                        break;
                }
            }
            else if (path.StartsWith("/api/getDoctorFilters"))
            {
                switch (method)
                {
                    case "GET":
                        Logger.Log($"Received {request.HttpMethod} request on {request.Url.AbsolutePath}", ConsoleColor.DarkGray);
                        await DoctorRequests.HandleGetSchedulesDoctors(request, response);
                        break;
                }
            }
            else
            {
                Logger.Log($"Resources not found.", ConsoleColor.DarkRed, HttpStatusCode.NotFound);
                response.StatusCode = 404;
            }
            response.Close();
        }

        private static async Task ServerStart()
        {
            server = new HttpListener();
            server.Prefixes.Add("http://localhost:8080/api/");
            server.Start();
            Logger.Log("Server started listening on http://localhost:8080/api/", ConsoleColor.DarkGray);

            while (true)
            {
                HttpListenerContext context = await server.GetContextAsync();
                await RouteRequest(context.Request, context.Response);
            }
        }

        static void Main(string[] args)
        {
            Task.Run(() => ServerStart()).GetAwaiter().GetResult();
        }
    }
}
