using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using PsychoTest.Messages;
using PsychoTest.Models;

namespace PsychoTest.ViewModels
{
    public class TestItemViewModel : ViewModelBase
    {
        private readonly TestQuestion _question;
        private ObservableCollection<TestAnswerViewModel> _answers;

        public TestItemViewModel(TestQuestion question)
        {
            _question = question;
            MessengerInstance.Register<AnsweredMessage>(this, Answered);
        }

        public bool GotAnswer { get; private set; }

        public ObservableCollection<TestAnswerViewModel> Answers
        {
            get { return _answers; }
            private set { Set(ref _answers, value); }
        }

        public string QuestionText
        {
            get { return _question.Question; }
        }

        public TestQuestion Question
        {
            get { return _question; }
        }

        public ParticipantViewModel Answer { get { return Answers.First(model => model.Selected).Participant; } }

        public void LoadAnswers(List<TestAnswerViewModel> answers)
        {
            Answers = new ObservableCollection<TestAnswerViewModel>(answers);
        }

        private void Answered(AnsweredMessage answeredMessage)
        {
            if (_answers != null)
            {
                GotAnswer = _answers.Any(model => model.Selected);
                MessengerInstance.Send(new GotAnswersMessage {Anwsered = GotAnswer});
            }
        }

        public void CleanTest()
        {
            GotAnswer = false;
            Answers = null;
        }

        public void ReloadTest(List<TestAnswerViewModel> answers)
        {
            CleanTest();
            LoadAnswers(answers);
        }
    }

    public class TestAnswerViewModel : ViewModelBase
    {
        private bool _selected;

        public TestAnswerViewModel(ParticipantViewModel participant)
        {
            Participant = participant;
        }

        public string Answer => Participant.FullName;

        public ParticipantViewModel Participant { get; private set; }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                Participant.Choosen = value;
                Set(ref _selected, value, true);
                MessengerInstance.Send(new AnsweredMessage {Anwsered = value });
            }
        }
    }
}
