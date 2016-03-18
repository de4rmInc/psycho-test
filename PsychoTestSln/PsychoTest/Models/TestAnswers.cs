using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PsychoTest.ViewModels;

namespace PsychoTest.Models
{
    public class TestAnswers
    {
        private readonly Dictionary<Participant, List<TestAnswer>> _answers;

        public TestAnswers()
        {
            _answers = new Dictionary<Participant, List<TestAnswer>>(Participant.IdComparer);
        }

        public int Count { get { return _answers.Count; } }

        public void AddAnswers(Participant participant, List<TestAnswer> answers)
        {
            _answers.Add(participant, answers);
        }
    }
}
