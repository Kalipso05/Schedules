using DataCenter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DesktopTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TrackerService _trackerService;
        //7460 × 2580
        private double originalImageWidth = 7460;
        private double originalImageHeight = 2580;

        private readonly DispatcherTimer _refreshTimer;

        public MainWindow()
        {
            InitializeComponent();
            _trackerService = new TrackerService();
            _refreshTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(3)
            };
            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();
        }

        private async void RefreshTimer_Tick(object sender, EventArgs e)
        {
            // Получаем данные с сервера
            var trackingData = await _trackerService.GetTrackingDataAsync();
            // Отображаем данные
            DisplayPeopleOnCanvas(trackingData);
        }

        // Обработчик нажатия кнопки
        private async void buttonUpdateLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<SecurityAccessLog> jsonResponse = await _trackerService.GetTrackingDataAsync();
                DisplayPeopleOnCanvas(jsonResponse);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void DisplayPeopleOnCanvas(List<SecurityAccessLog> securityAccessLogs)
        {
            try
            {
                // Очистите предыдущие маркеры
                TrackingCanvas.Children.Clear();

                foreach (var person in securityAccessLogs)
                {
                    int scudNumber = person.LastSecurityPointNumber ?? 0;
                    Point location = GetCanvasCoordinatesFromScudNumber(scudNumber);

                    // Создаем визуальный маркер для человека
                    Ellipse personMarker = new Ellipse
                    {
                        Width = 10,
                        Height = 10,
                        Fill = person.PersonRole == "Клиент" ? Brushes.Green : Brushes.Blue
                    };

                    // Установка координат маркера на канвасе
                    Canvas.SetLeft(personMarker, location.X);
                    Canvas.SetTop(personMarker, location.Y);

                    // Добавление маркера на канвас
                    TrackingCanvas.Children.Add(personMarker);
                    string direction = person.LastSecurityPointDirection == "in" ? "Зашел" : "Вышел";
                    // Создаем стикер с информацией
                    TextBlock infoSticker = new TextBlock
                    {
                        Text = $"{person.PersonCode}\n {person.PersonRole}\n {person.LastSecurityPointTime}\n {direction}",
                        Foreground = Brushes.Black,
                        Background = Brushes.White,
                        Padding = new Thickness(2)
                    };

                    // Установка координат стикера на канвасе (с небольшим смещением от маркера)
                    Canvas.SetLeft(infoSticker, location.X + 15); // смещение по X
                    Canvas.SetTop(infoSticker, location.Y - 10); // смещение по Y

                    // Добавление стикера на канвас
                    TrackingCanvas.Children.Add(infoSticker);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        // Словарь с кооридинатами
        private Dictionary<int, Point> scudNumberToCanvasCoordinates = new Dictionary<int, Point>()
        {
            { 15, new Point(1177, 1646) },
            { 21, new Point(2179, 930) },
            { 22, new Point(2809, 925) },
        };

        private Point GetCanvasCoordinatesFromScudNumber(int scudNumber)
        {
            try
            {
                if (scudNumberToCanvasCoordinates.TryGetValue(scudNumber, out Point imageCoordinates))
                {
                    // Если изображение масштабируется, применяем масштабирование p.s. она у нас должна быть в оригинальных размерах
                    double scaleX = TrackingCanvas.ActualWidth / originalImageWidth;
                    double scaleY = TrackingCanvas.ActualHeight / originalImageHeight;

                    // Преобразование координат изображения в координаты канваса (канвас не трогай, пусть будет пустым)
                    double canvasX = imageCoordinates.X * scaleX;
                    double canvasY = imageCoordinates.Y * scaleY;

                    return new Point(canvasX, canvasY);
                }
                else
                {
                    return new Point(0, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new Point(0, 0);
            }
        }
    }
}
