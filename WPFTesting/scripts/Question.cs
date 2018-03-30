using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTesting.scripts
{
    public class Question
    {
        private string questiontext;
        public string QuestionText
        {
            get { return questiontext; }
        }

        private string answer;
        public string Answer
        {
            get { return answer; }
        }

        private int category;
        public int Category
        {
            get { return category; }
        }

        private int value;
        public int Value
        {
            get { return value; }
        }

        public Question(string q, string a,int c, int v)
        {
            questiontext = q;
            answer = a;
            category = c;
            value = v;
        }

        public override string ToString()
        {
            return "QUESTION: " + questiontext + " | ANSWER: " + answer + " | VALUE: " + value + " | CATEGORY:  " + category;
        }


    }
}
