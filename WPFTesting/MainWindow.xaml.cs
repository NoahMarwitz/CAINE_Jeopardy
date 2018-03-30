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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTesting.scripts;

namespace WPFTesting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static QuestionViewer openViewer;
        static AnswerViewer openAnsViewer;
        QuestionManager master;

        static int seconds;
        public static int Seconds
        {
            get { return seconds; }
        }
        static string[] mainCategories;
        public static string[] MainCategories
        {
            get { return mainCategories; }
        }

        public MainWindow()
        {

            master = new QuestionManager();

            if(!master.SanityCheck())
            {
                Close();
            }

            openViewer = null;
            openAnsViewer = new AnswerViewer();
            openAnsViewer.Show();
            master.readInQuestions("q.txt");
            master.readInSettings("s.txt");


            seconds = master.SecondsSettings;
            mainCategories = master.Categories;
            InitializeComponent();

            ttext1.Text = mainCategories[0];
            ttext2.Text = mainCategories[1];
            ttext3.Text = mainCategories[2];
            ttext4.Text = mainCategories[3];
            ttext5.Text = mainCategories[4];
        }

        void ValueClick(object sender, RoutedEventArgs e)
        {
            if(openAnsViewer == null)
            {
                openAnsViewer = new AnswerViewer();
                openAnsViewer.Show();
            }

            if (openViewer == null)
            {
                openViewer = new QuestionViewer();
                openViewer.Show();
            }
            else
            {
                if (openViewer.QuestionActive) return;
            }



            var button = sender as Button;

            string ValAndCat = button.Name;
            string[] atts = ValAndCat.Split('_');
            int cat = int.Parse(atts[0].Substring(3)) - 1;
            int val = (int.Parse(atts[1])  / 100) - 1;

            Question grabbedQuestion = null;
            int remainingQuestions = master.checkQuestionCount(cat, val);

            if (remainingQuestions > 0)
            {
                grabbedQuestion = master.getRandom(cat, val);
                if(remainingQuestions == 1)
                {
                    button.IsEnabled = false;
                }
            }
            else
            {
                button.IsEnabled = false;
                return;
            }

            openViewer.PrepQuestion(grabbedQuestion);
            openAnsViewer.GetAnswer(grabbedQuestion.Answer);

        }

        static public void ClosedWindow(int type)
        {
            switch (type)
            {
                case 0:
                    openViewer = null;
                    break;
                case 1:
                    openAnsViewer = null;
                    break;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (openViewer != null)
                openViewer.ForceClose();
            if (openAnsViewer != null)
                openAnsViewer.Close();
            base.OnClosing(e);
        }

    }
}
