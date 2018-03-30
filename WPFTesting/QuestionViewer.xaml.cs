using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using WPFTesting.scripts;

namespace WPFTesting
{
    /// <summary>
    /// Interaction logic for QuestionViewer.xaml
    /// </summary>
    public partial class QuestionViewer : Window
    {
        bool questionActive = false;
        public bool QuestionActive
        {
            get { return questionActive; }
        }

        TimeSpan time;
        DispatcherTimer timer;
        int timeLength = 60;

        int width;

        SolidColorBrush red = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        SolidColorBrush green = new SolidColorBrush(Color.FromRgb(0, 255, 0));
        byte redVal = 0;
        byte greenVal = 255;


        public QuestionViewer()
        {
            InitializeComponent();

            maintext.FontSize = 32;

            width = Convert.ToInt32(timeleft.Width);

            time = TimeSpan.FromSeconds(timeLength);
            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
              {
                  //This delegate is in the Tick call of the timer.

                  //Manage the rectangle each tick.
                  float totalTime = (time.Hours * 3600) + (time.Minutes * 60) + time.Seconds;

                  float ratio = (totalTime) / ((float)timeLength);
                  redVal = Convert.ToByte(254 * (1 - ratio));
                  greenVal = Convert.ToByte(254 * ratio);
                  timeleft.Width = width * ratio;
                  timeleft.Fill = new SolidColorBrush(Color.FromRgb(redVal, greenVal, 0));

                  //End condition
                  if (time == TimeSpan.Zero)
                  {
                      timer.Stop();
                      questionActive = false;
                      startbutton.Content = "Start";
                      startbutton.IsEnabled = true;
                      time = TimeSpan.FromSeconds(timeLength);
                  }
                  time = time.Add(TimeSpan.FromSeconds(-1));

              }, Application.Current.Dispatcher);

            timer.Stop();

            timeLength = MainWindow.Seconds;
        }

        public void PrepQuestion(Question q)
        {
            maintext.Text = q.QuestionText;
            switch (q.Category)
            {
                case 1:
                    category.Text = MainWindow.MainCategories[0];
                    break;
                case 2:
                    category.Text = MainWindow.MainCategories[1];
                    break;
                case 3:
                    category.Text = MainWindow.MainCategories[2];
                    break;
                case 4:
                    category.Text = MainWindow.MainCategories[3];
                    break;
                case 5:
                    category.Text = MainWindow.MainCategories[4];
                    break;
            }
            value.Text = q.Value.ToString();

            time = TimeSpan.FromSeconds(timeLength);
            questionActive = true;
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            if (questionActive)
            {
                e.Cancel = true;
                return;
            }
            MainWindow.ClosedWindow(0);

            base.OnClosing(e);
        }

        void StartButton(object sender, RoutedEventArgs e)
        {
            time = TimeSpan.FromSeconds(timeLength);
            Console.WriteLine("START " + time.Seconds);
            timer.Start();
            startbutton.Content = "";
            startbutton.IsEnabled = false;
            questionActive = true;
        }

        public void ForceClose()
        {
            questionActive = false;
            Close();
        }
    }
}
