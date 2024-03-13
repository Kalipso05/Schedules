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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentPageNumber = 1;
        private const int PageSize = 25;
        public MainWindow()
        {
            InitializeComponent();
            LoadData(currentPageNumber);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            currentPageNumber++;
            LoadData(currentPageNumber);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageNumber > 1)
            {
                currentPageNumber--;
                LoadData(currentPageNumber);
            }
        }


        private async void LoadData(int pageNumber)
        {
            var patients = await GetPatientsAsync(pageNumber);
            GridViewData.ItemsSource = patients;
        }


        private async Task<List<PatientDTO>> GetPatientsAsync(int pageNumber)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"http://localhost:8080/api/Patient?page={pageNumber}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<PatientDTO>>(json);
                }
                else
                {
                    MessageBox.Show("Failed!");
                    return new List<PatientDTO>();
                }
            }
        }

    }
}
