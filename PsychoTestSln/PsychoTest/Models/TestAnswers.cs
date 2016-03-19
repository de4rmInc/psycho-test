using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PsychoTest.ViewModels;

namespace PsychoTest.Models
{
    [Serializable]
    public class TestAnswers
    {
        private readonly Dictionary<Participant, List<TestAnswer>> _answers;

        public TestAnswers()
        {
            _answers = new Dictionary<Participant, List<TestAnswer>>(Participant.IdComparer);
        }

        public string TestName { get; set; }

        public int Count { get { return _answers.Count; } }

        public void AddAnswers(Participant participant, List<TestAnswer> answers)
        {
            _answers.Add(participant, answers);
        }
        
        public IReadOnlyList<Participant> Participants { get { return _answers.Keys.ToList(); } }

        public IReadOnlyList<TestAnswer> GetAnswersForParticipant(Participant participant)
        {
            return _answers.ContainsKey(participant)
                ? _answers[participant]
                : new List<TestAnswer>();
        }
    }
}
