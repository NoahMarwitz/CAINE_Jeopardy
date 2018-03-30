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

namespace WPFTesting
{
    /// <summary>
    /// Interaction logic for AnswerViewer.xaml
    /// </summary>
    public partial class AnswerViewer : Window
    {
        bool toggled;
        public AnswerViewer()
        {
            InitializeComponent();
            toggled = false;
            answerText.Visibility = Visibility.Hidden;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MainWindow.ClosedWindow(1);
            base.OnClosing(e);
        }

        public void GetAnswer(string ans)
        {
            answerText.Text = ans;
        }

        void Toggle(object sender, RoutedEventArgs e)
        {
            if(toggled)
            {
                answerText.Visibility = Visibility.Hidden;
                toggled = false;
            }
            else
            {
                answerText.Visibility = Visibility.Visible;
                toggled = true;
            }
        }
    }
}
