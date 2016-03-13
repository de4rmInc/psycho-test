using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.CommandWpf;

namespace PsychoTest.ViewModels
{
    public class TestViewModel : TabViewModelBase
    {
        private const int RegistrationPagesCount = 2;
        private int _registrationPageNo = 1;

        private List<ParticipantViewModel> _participants;
        private Queue<TestItemViewModel> _testItems;

        private RelayCommand<object> _nextTestItemCommand;
        private TestItemViewModel _currentTestItem;
        private string _testName;
        private int? _participantsCount;

        public TestViewModel(List<string> questions)
        {
            _testItems = new Queue<TestItemViewModel>(questions.Select(question => new TestItemViewModel(question)));
            RegistrationButtonName = "Далее";
            TestButtonName = "Продолжить";
            IsRegistrationState = true;
        }

        public string TestName
        {
            get { return _testName; }
            set { Set(ref _testName, value); }
        }

        public string RegistrationButtonName
        {
            get { return _registrationButtonName; }
            set { Set(ref _registrationButtonName, value); }
        }

        public string TestButtonName
        {
            get { return _testButtonName; }
            set { Set(ref _testButtonName, value); }
        }

        public bool IsRegistrationState
        {
            get { return _isRegistrationState; }
            set { Set(ref _isRegistrationState, value); }
        }

        public bool IsRegistrationFinishing
        {
            get { return _isRegistrationFinishing; }
            set { Set(ref _isRegistrationFinishing, value); }
        }

        public int? ParticipantsCount
        {
            get { return _participantsCount; }
            set { Set(ref _participantsCount, value); }
        }

        public TestItemViewModel CurrentTestItem
        {
            get { return _currentTestItem; }
            set { Set(ref _currentTestItem, value); }
        }
        
        public RelayCommand<object> NextTestItemCommand
        {
            get
            {
                return _nextTestItemCommand ??
                       (_nextTestItemCommand = new RelayCommand<object>(NextTestItem, CanNextTestItem));
            }
        }

        private RelayCommand<object> _nextRegistrationPageCommand;
        private string _registrationButtonName;
        private string _testButtonName;
        private bool _isRegistrationState;
        private bool _isRegistrationFinishing;

        public RelayCommand<object> NextRegistrationPageCommand
        {
            get
            {
                return _nextRegistrationPageCommand ??
                       (_nextRegistrationPageCommand = new RelayCommand<object>(NextRegistrationPage, CanNextRegistrationPage));
            }
        }

        public List<ParticipantViewModel> Participants
        {
            get { return _participants; }
            set { Set(ref _participants, value); }
        }

        private bool CanNextRegistrationPage(object o)
        {
            if (_registrationPageNo == 1)
            {
                return ParticipantsCount.HasValue && ParticipantsCount > 1;
            }
            else
            {
                return _participants !=null && _participants.All(participant => participant.NotEmpty);
            }
        }

        private void NextRegistrationPage(object o)
        {
            if (_registrationPageNo == 1)
            {
                RegistrationButtonName = "Начать тест";
                var participants = new List<ParticipantViewModel>();
                for (int i = 0; i < ParticipantsCount; i++)
                {
                    participants.Add(new ParticipantViewModel());
                }
                Participants = participants;
                IsRegistrationFinishing = true;
            }
            else if (_registrationPageNo == 2)
            {
                IsRegistrationState = false;
            }
            _registrationPageNo++;
        }

        private bool CanNextTestItem(object o)
        {
            return CurrentTestItem == null || CurrentTestItem.GotAnswer;
        }

        private void NextTestItem(object o)
        {
            
        }
    }
}
