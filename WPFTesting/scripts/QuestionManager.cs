using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace WPFTesting.scripts
{
    class QuestionManager
    {
        List<List<List<Question>>> Questions; //IN ORDER: CATEGORY, VALUE, QUESTIONS
        Random randomizer;

        string folder = @"\questions\";
        string settings = @"\settings\";

        int timer;

        public int SecondsSettings
        {
            get { return timer; }
        }
        string[] categories;
        public string[] Categories
        {
            get { return categories; }
        }


        public QuestionManager()
        {
            Questions = new List<List<List<Question>>>();
            randomizer = new Random();
            for (int i = 0; i < 6; i++) //Create lists for questions in master list
            {
                Questions.Add(new List<List<Question>>());
                for(int j = 0; j < 5; j++)
                {
                    Questions[i].Add(new List<Question>()); //Question value lists per category
                }
            }
        }

        public void readInQuestions(string fileName)
        {
            StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + folder + fileName);
            try
            {
                do
                {
                    string[] items = new string[4]; //Used to create a question. Done this way because I didn't want to write mutators. 
                    //{Question,Answer,Value,Category}
                    string readLine = reader.ReadLine();

                    while (readLine[0] != '#')
                    {
                        Console.WriteLine("READING: " + readLine);
                        switch (char.ToLower(readLine[0]))
                        {
                            case 'q':
                                items[0] = readLine.Substring(1).Trim();
                                break;
                            case 'a':
                                items[1] = readLine.Substring(1).Trim();
                                break;
                            case 'c':
                                items[2] = readLine.Substring(1).Trim();
                                break;
                            case 'v':
                                items[3] = readLine.Substring(1).Trim();
                                break;
                            default:
                                Console.WriteLine("FUCK");
                                break;
                        }
                        readLine = reader.ReadLine();
                    }
                    Question question = new Question(items[0], items[1], int.Parse(items[2]), int.Parse(items[3]));
                    Questions[question.Category - 1][(question.Value / 100) - 1].Add(question);
                    Console.WriteLine(question);
                } while (reader.Peek() != -1);
            }
            catch
            {
                Console.WriteLine("SHIT");
            }
            finally
            {
                reader.Close();
            }
        }

        public void readInSettings(string fileName)
        {
            StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + settings + fileName);
            int catIndex = 0;
            categories = new string[5];
            try
            {
                do
                {
                    string line = reader.ReadLine();
                    string[] inLine = line.Split(' ');

                    switch(inLine[0].ToLower())
                    {
                        case "timer":
                            timer = int.Parse(inLine[1]);
                            break;
                        case "category":
                            if(catIndex < 5)
                            {
                                Console.WriteLine(line.Substring(inLine[0].Length));
                                categories[catIndex] = line.Substring(inLine[0].Length);
                                catIndex++;
                            }
                            break;
                    }

                } while (reader.Peek() != -1);
            }
            catch
            {
                Console.WriteLine("SHIT");
            }
            finally
            {
                reader.Close();
            }
        }

        public int checkQuestionCount(int category, int value)
        {
            return Questions[category][value].Count;
        }

        public Question getRandom(int category, int value)
        {
            List<Question> currentList = Questions[category][value];
            int ceil = currentList.Count - 1;
            int selection = randomizer.Next(ceil);
            Question retQ = currentList[selection];
            currentList.RemoveAt(selection);
            return retQ;
        }

        public bool SanityCheck()
        {
            if( !Directory.Exists(Directory.GetCurrentDirectory() + folder) || !Directory.Exists(Directory.GetCurrentDirectory() + settings))
            {
                System.Windows.MessageBox.Show("Failed to find Directory. Make sure that a \"questions\" and \"settings\" directory exist in the same directory as this.");
                return false;
            }
            if(!File.Exists(Directory.GetCurrentDirectory() + folder + "q.txt") || !File.Exists(Directory.GetCurrentDirectory() + settings + "s.txt"))
            {
                System.Windows.MessageBox.Show("Failed to find \"q.txt\" or \"s.txt\". Make sure q is in questions and s is in settings.");
                return false;
            }
            return true;
        }
    }
}
