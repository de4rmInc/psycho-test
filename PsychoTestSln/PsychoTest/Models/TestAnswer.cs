using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoTest.Models
{
    [Serializable]
    public class TestAnswer
    {
        public TestQuestion Question { get; set; }
        public Participant Answer { get; set; }

        public TestAnswer()
        {
            
        }

        public TestAnswer(TestQuestion question, Participant answer)
        {
            Question = question;
            Answer = answer;
        }
    }
}
