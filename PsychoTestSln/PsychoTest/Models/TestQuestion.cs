using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychoTest.Models
{
    [Serializable]
    public enum QuestionType
    {
        ProQualities,
        PersonalQualities
    }

    [Serializable]
    public class TestQuestion
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public QuestionType Type { get; set; }
    }
}
