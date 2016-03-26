using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace PsychoTest.ViewModels
{
    public class ParticipantViewModel : ViewModelBase
    {
        private string _firstName;
        private string _secondName;
        private bool _choosen;

        public ParticipantViewModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string FirstName
        {
            get
            {
#if DEBUG
                return "Имя_" + Id.ToString().Substring(0, 6);
#else
                return _firstName;
#endif
            }
            set
            {
                Set(ref _firstName, value);
                this.RaisePropertyChanged(() => this.FullName);
            }
        }

        public string SecondName
        {
            get
            {
#if DEBUG
                return "Фамилия_" + Id.ToString().Substring(0, 4);
#else
                return _secondName;
#endif
            }
            set
            {
                Set(ref _secondName, value);
                this.RaisePropertyChanged(() => this.FullName);
            }
        }

        public string FullName => $"{SecondName} {FirstName}";

        public bool NotEmpty
        {
            get
            {
                return !string.IsNullOrEmpty(FirstName)
                       && !string.IsNullOrEmpty(SecondName);
            }
        }
    }
}
