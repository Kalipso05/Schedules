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
    public static class DoctorRequests
    {
        public static async Task HandleAuthUser(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                string requestBody;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    requestBody = await reader.ReadToEndAsync();
                }

                var authDoctor = JsonConvert.DeserializeObject<Users>(requestBody);
                if (authDoctor == null)
                {
                    await Response.SendResponse(response, "Incorrect data was sent!", "application/json", HttpStatusCode.BadRequest);
                    Logger.Log("Incorrect data was sent!", ConsoleColor.DarkRed, HttpStatusCode.BadRequest);
                    return;
                }

                using (var db = new dbModel())
                {
                    var user = db.Users.FirstOrDefault(item => item.Login == authDoctor.Login && item.Password == authDoctor.Password);
                    if (user != null)
                    {
                        await Response.SendResponse(response, JsonConvert.SerializeObject(new { message = "Authentication successful" }), "application/json", HttpStatusCode.OK);
                    }
                    else
                    {
                        await Response.SendResponse(response, JsonConvert.SerializeObject(new { message = "Invalid username or password" }), "application/json", HttpStatusCode.Unauthorized);
                        Logger.Log("Invalid username or password", ConsoleColor.DarkYellow, HttpStatusCode.Unauthorized);
                    }
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Logger.Log($"Error retrieving tracking data: {ex.Message}", ConsoleColor.DarkRed, HttpStatusCode.InternalServerError);

                // Отправляем ошибку клиенту
                await Response.SendResponse(response, "Internal server error.", "application/json", HttpStatusCode.InternalServerError);
            }
        }
        public static async Task HandleGetSchedulesSpeciality(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                var specialityId = Convert.ToInt32(request.QueryString["id"]);

                using (var db = new dbModel())
                {

                    var schedules = db.Schedules
                                      .Where(s => s.Users.IDSpeciality == specialityId)
                                      .Include(s => s.ScheduleEvents)
                                      .ToList();

                    var schedulesDto = schedules.Select(s => new ScheduleDTO
                    {
                        ScheduleID = s.ScheduleID,
                        ScheduleDate = s.ScheduleDate,
                        StartTime = s.StartTime.ToString(),
                        EndTime = s.EndTime.ToString(),
                        ScheduleType = s.ScheduleType,
                        Color = s.Color,
                        DoctorName = $"{s.Users.LastName} {s.Users.FirstName} {s.Users.Patronymic}",
                        SpecialityName = s.Users.Speciality.Title
                    }).ToList();

                    if (schedulesDto.Any())
                    {

                        var schedulesJson = JsonConvert.SerializeObject(schedulesDto);
                        await Response.SendResponse(response, schedulesJson, "application/json", HttpStatusCode.OK);
                        Logger.Log("Get Successfully!");

                    }
                    else
                    {
                        await Response.SendResponse(response, "No schedules found.", "application/json", HttpStatusCode.NotFound);
                        Logger.Log("No schedules found.", ConsoleColor.Red, HttpStatusCode.NotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ConsoleColor.Red, HttpStatusCode.InternalServerError);
                await Response.SendResponse(response, $"Internal server error: {ex.Message}", "application/json", HttpStatusCode.InternalServerError);
            }
        }

        public static async Task HandleGetSchedulesDoctors(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                var doctorsId = Convert.ToInt32(request.QueryString["id"]);

                using (var db = new dbModel())
                {

                    var schedules = db.Schedules
                                      .Where(s => s.UserID == doctorsId)
                                      .Include(s => s.ScheduleEvents)
                                      .ToList();

                    var schedulesDto = schedules.Select(s => new ScheduleDTO
                    {
                        ScheduleID = s.ScheduleID,
                        ScheduleDate = s.ScheduleDate,
                        StartTime = s.StartTime.ToString(),
                        EndTime = s.EndTime.ToString(),
                        ScheduleType = s.ScheduleType,
                        Color = s.Color,
                        DoctorName = $"{s.Users.LastName} {s.Users.FirstName} {s.Users.Patronymic}",
                        SpecialityName = s.Users.Speciality.Title
                    }).ToList();

                    if (schedulesDto.Any())
                    {

                        var schedulesJson = JsonConvert.SerializeObject(schedulesDto);
                        await Response.SendResponse(response, schedulesJson, "application/json", HttpStatusCode.OK);
                        Logger.Log("Get Successfully!");

                    }
                    else
                    {
                        await Response.SendResponse(response, "No schedules found.", "application/json", HttpStatusCode.NotFound);
                        Logger.Log("No schedules found.", ConsoleColor.Red, HttpStatusCode.NotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ConsoleColor.Red, HttpStatusCode.InternalServerError);
                await Response.SendResponse(response, $"Internal server error: {ex.Message}", "application/json", HttpStatusCode.InternalServerError);
            }
        }

        public static async Task HandleGetSchedules(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                var query = request.Url.Query;
                var parameters = query.TrimStart('?').Split('&')
                    .Select(part => part.Split('='))
                    .ToDictionary(split => split[0], split => Uri.UnescapeDataString(split[1]));

                DateTime scheduleDate;
                if (!parameters.TryGetValue("date", out var dateParam) || !DateTime.TryParse(dateParam, out scheduleDate))
                {

                    Logger.Log("Incorrect or missing date parameter.", ConsoleColor.Red, HttpStatusCode.InternalServerError);
                    await Response.SendResponse(response, "Incorrect or missing date parameter.", "application/json", HttpStatusCode.BadRequest);
                    return;
                }

                using (var db = new dbModel())
                {

                    var schedules = db.Schedules
                                      .Where(s => s.ScheduleDate == scheduleDate)
                                      .Include(s => s.ScheduleEvents)
                                      .ToList();

                    var schedulesDto = schedules.Select(s => new ScheduleDTO
                    {
                        ScheduleID = s.ScheduleID,
                        ScheduleDate = s.ScheduleDate,
                        StartTime = s.StartTime.ToString(),
                        EndTime = s.EndTime.ToString(),
                        ScheduleType = s.ScheduleType,
                        Color = s.Color,
                        DoctorName = $"{s.Users.LastName} {s.Users.FirstName} {s.Users.Patronymic}",
                        SpecialityName = s.Users.Speciality.Title
                    }).ToList();

                    if (schedulesDto.Any())
                    {

                        var schedulesJson = JsonConvert.SerializeObject(schedulesDto);
                        await Response.SendResponse(response, schedulesJson, "application/json", HttpStatusCode.OK);
                        Logger.Log("Get Successfully!");

                    }
                    else
                    {
                        await Response.SendResponse(response, "No schedules found.", "application/json", HttpStatusCode.NotFound);
                        Logger.Log("No schedules found.", ConsoleColor.Red, HttpStatusCode.NotFound);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ConsoleColor.Red, HttpStatusCode.InternalServerError);
                await Response.SendResponse(response, $"Internal server error: {ex.Message}", "application/json", HttpStatusCode.InternalServerError);
            }
        }

        public static async Task HandleGetSpecialities(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                using (var db = new dbModel())
                {
                    var specialities = db.Speciality.Select(s => new { s.ID, s.Title }).ToList();
                    if (specialities.Any())
                    {
                        var specialitiesJson = JsonConvert.SerializeObject(specialities);
                        await Response.SendResponse(response, specialitiesJson, "application/json", HttpStatusCode.OK);
                        Logger.Log("Successfully GET");
                    }
                    else
                    {
                        await Response.SendResponse(response, "No specialities found.", "application/json", HttpStatusCode.NotFound);
                        Logger.Log("No specialities found.", ConsoleColor.Red, HttpStatusCode.NotFound);

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ConsoleColor.Red, HttpStatusCode.InternalServerError);
                await Response.SendResponse(response, $"Internal server error: {ex.Message}", "application/json", HttpStatusCode.InternalServerError);
            }
        }
        public static async Task HandleGetDoctors(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                using (var db = new dbModel())
                {
                    var doctors = db.Users.Select(s => new {s.UserID, s.FirstName, s.LastName, s.Patronymic }).ToList();
                    if (doctors.Any())
                    {
                        var specialitiesJson = JsonConvert.SerializeObject(doctors);
                        await Response.SendResponse(response, specialitiesJson, "application/json", HttpStatusCode.OK);
                        Logger.Log("Successfully GET");
                    }
                    else
                    {
                        await Response.SendResponse(response, "No specialities found.", "application/json", HttpStatusCode.NotFound);
                        Logger.Log("No specialities found.", ConsoleColor.Red, HttpStatusCode.NotFound);

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, ConsoleColor.Red, HttpStatusCode.InternalServerError);
                await Response.SendResponse(response, $"Internal server error: {ex.Message}", "application/json", HttpStatusCode.InternalServerError);
            }
        }
    }
}