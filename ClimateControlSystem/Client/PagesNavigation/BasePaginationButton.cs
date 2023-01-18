namespace ClimateControlSystem.Client.PagesNavigation
{
    public abstract class BasePaginationButton
    {
        private int _pageNumber;
        private bool _isActivePage;
        private bool _isEnabled;
        private PaginationButtonType _buttonType;

        public event Action<BasePaginationButton> ButtonPressedEvent;

        public PaginationButtonType ButtonType
        {
            get => _buttonType;
            protected set => _buttonType = value;
        }

        public bool IsActivePage
        {
            get => _isActivePage;
            set => _isActivePage = value;
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value;
        }

        public abstract string Title { get; }

        public void Enable()
        {
            IsEnabled = true;
        }

        public void Disable()
        {
            IsEnabled = false;
        }

        public void ChangePage()
        {
            ButtonPressedEvent?.Invoke(this);
        }
    }
}
