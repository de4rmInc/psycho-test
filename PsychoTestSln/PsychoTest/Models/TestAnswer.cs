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
        public List<Participant> Answers { get; set; }

        public TestAnswer()
        {
            
        }

        public TestAnswer(TestQuestion question, List<Participant> answers)
        {
            Question = question;
            Answers = answers;
        }
    }
}
