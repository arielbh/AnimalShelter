using SuiteValue.UI.MVVM;

namespace AnimalShelter.ViewModels
{
    class HeaderViewModel : ViewModelBase
    {
        private string _header;

        public string Header
        {
            get { return _header; }
            set
            {
                if (value != _header)
                {
                    _header = value;
                    OnPropertyChanged(() => Header);
                }
            }
        }

        public virtual void Activate()
        {
        }
    }
}