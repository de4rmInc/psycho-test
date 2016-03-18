using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoTest.Models
{
    public enum QuestionType
    {
        ProQualities,
        PersonalQuailities
    }

    public class TestQuestion
    {
        public string Question { get; set; }
        public QuestionType Type { get; set; }
    }
}
