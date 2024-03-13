using DataCenter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ScheduleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient _client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadInitialDataAsync();
        }

        private async Task LoadInitialDataAsync()
        {
            try
            {
                // Загрузка специализаций
                var specialitiesResponse = await _client.GetAsync("http://localhost:8080/api/specialities");
                var doctorsResponse = await _client.GetAsync("http://localhost:8080/api/doctors");
                if (specialitiesResponse.IsSuccessStatusCode)
                {
                    var specialitiesJson = await specialitiesResponse.Content.ReadAsStringAsync();
                    var specialities = JsonConvert.DeserializeObject<List<Speciality>>(specialitiesJson);
                    specialityComboBox.ItemsSource = specialities;
                    specialityComboBox.DisplayMemberPath = "Title";
                    specialityComboBox.SelectedValuePath = "ID";
                }
                if (doctorsResponse.IsSuccessStatusCode)
                {
                    var doctorsJson = await doctorsResponse.Content.ReadAsStringAsync();
                    var doctors = JsonConvert.DeserializeObject<List<Users>>(doctorsJson);
                    doctorComboBox.ItemsSource = doctors;
                    doctorComboBox.DisplayMemberPath = "LastName";
                    doctorComboBox.SelectedValuePath = "UserID";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading initial data: {ex.Message}");
            }
        }


        private async void ApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = datePicker.SelectedDate;
            var selectedSpeciality = specialityComboBox.SelectedItem as Speciality;
            var selectedDoctors = doctorComboBox.SelectedItem as Users;

            if (selectedDoctors != null)
            {
                var response = await _client.GetAsync($"http://localhost:8080/api/getDoctorFilters?id={selectedDoctors.UserID}");
                if (response.IsSuccessStatusCode)
                {
                    var schedulesJson = await response.Content.ReadAsStringAsync();
                    var schedules = JsonConvert.DeserializeObject<List<ScheduleDTO>>(schedulesJson);

                    dailyScheduleDataGrid.ItemsSource = schedules.Where(s => s.DoctorName.StartsWith(selectedDoctors.LastName)).ToList();

                    weeklyScheduleDataGrid.ItemsSource = schedules.Where(s => s.DoctorName.StartsWith(selectedDoctors.LastName)).ToList();
                }
                else
                {
                    MessageBox.Show("Error getting schedule.");
                }
            }

            if (selectedSpeciality != null)
            {
                var response = await _client.GetAsync($"http://localhost:8080/api/filterSpeciality?id={selectedSpeciality.ID}");
                if (response.IsSuccessStatusCode)
                {
                    var schedulesJson = await response.Content.ReadAsStringAsync();
                    var schedules = JsonConvert.DeserializeObject<List<ScheduleDTO>>(schedulesJson);

                    dailyScheduleDataGrid.ItemsSource = schedules.Where(s => s.SpecialityName == selectedSpeciality.Title).ToList();

                    weeklyScheduleDataGrid.ItemsSource = schedules.Where(s => s.SpecialityName == selectedSpeciality.Title).ToList();
                }
                else
                {
                    MessageBox.Show("Error getting schedule.");
                }
            }

            if (selectedDate.HasValue)
            {
                var response = await _client.GetAsync($"http://localhost:8080/api/schedules?date={selectedDate.Value:yyyy-MM-dd}");
                if (response.IsSuccessStatusCode)
                {
                    var schedulesJson = await response.Content.ReadAsStringAsync();
                    var schedules = JsonConvert.DeserializeObject<List<ScheduleDTO>>(schedulesJson);

                    dailyScheduleDataGrid.ItemsSource = schedules.Where(s => s.ScheduleDate.Value.Date == selectedDate.Value.Date).ToList();
                    weeklyScheduleDataGrid.ItemsSource = schedules.Where(s => s.ScheduleDate.Value.Date >= selectedDate.Value.Date && s.ScheduleDate.Value.Date <= selectedDate.Value.Date.AddDays(7)).ToList();
                }
                else
                {
                    MessageBox.Show("Error getting schedule.");
                }
            }
        }
    }
}
