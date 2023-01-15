namespace ClimateControlSystem.Client.PagesNavigation
{
    public sealed class PaginationButton
    {
        public const int FirstPageNumber = -1;
        public const int PreviousPageNumber = -2;
        public const int NextPageNumber = -3;
        public const int LastPageNumber = -4;

        public const string FirstPageTitle = "<<";
        public const string PreviousPageTitle = "<";
        public const string NextPageTitle = ">";
        public const string LastPageTitle = ">>";

        private string _title;
        private int _pageNumber;
        private bool _isActivePage;
        private Visibility _visibilityState;
        private SelectionPageButtonType _buttonType;

        public event Func<PaginationButton, Task> Activated;

        public SelectionPageButtonType ButtonType
        {
            get => _buttonType;
            private set => _buttonType = value;
        }

        public bool IsActivePage
        {
            get => _isActivePage;
            set => _isActivePage = value;
        }

        public Visibility VisibilityState
        {
            get => _visibilityState;
            private set => _visibilityState = value;
        }

        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                _pageNumber = value;
                Title = _pageNumber.ToString();
            }
        }

        public string Title
        {
            get => _title;
            private set => _title = value;
        }

        public bool IsGoToTheFirstPage => PageNumber == FirstPageNumber;
        public bool IsGoToThePreviousPage => PageNumber == PreviousPageNumber;
        public bool IsGoToTheNextPage => PageNumber == NextPageNumber;
        public bool IsGoToTheLastPage => PageNumber == LastPageNumber;

        public PaginationButton()
        {
        }

        public static PaginationButton CreateGoToTheFirstPage()
        {
            return new PaginationButton()
            {
                PageNumber = FirstPageNumber,
                VisibilityState = Visibility.Hidden,
                IsActivePage = false,
                Title = FirstPageTitle,
                ButtonType = SelectionPageButtonType.StartPage
            };
        }

        public static PaginationButton CreateGoToThePreviousPage()
        {
            return new PaginationButton()
            {
                PageNumber = PreviousPageNumber,
                VisibilityState = Visibility.Hidden,
                IsActivePage = false,
                Title = PreviousPageTitle,
                ButtonType = SelectionPageButtonType.PreviousPage
            };
        }

        public static PaginationButton CreateGoToTheNextPage()
        {
            return new PaginationButton()
            {
                PageNumber = NextPageNumber,
                VisibilityState = Visibility.Hidden,
                IsActivePage = false,
                Title = NextPageTitle,
                ButtonType = SelectionPageButtonType.NextPage
            };
        }

        public static PaginationButton CreateGoToTheLastPage()
        {
            return new PaginationButton()
            {
                PageNumber = LastPageNumber,
                VisibilityState = Visibility.Hidden,
                IsActivePage = false,
                Title = LastPageTitle,
                ButtonType = SelectionPageButtonType.LastPage
            };
        }

        public static PaginationButton CreateConcretePageViewModel(int pageNumber)
        {
            return new PaginationButton()
            {
                VisibilityState = Visibility.Hidden,
                IsActivePage = false,
                PageNumber = pageNumber,
                ButtonType = SelectionPageButtonType.Number
            };
        }

        public void Show()
        {
            VisibilityState = Visibility.Visible;
        }

        public void Hide()
        {
            VisibilityState = Visibility.Hidden;
        }

        public void Collapse()
        {
            VisibilityState = Visibility.Collapsed;
        }

        public void ChangePage()
        {
            Activated?.Invoke(this);
        }
    }
}
