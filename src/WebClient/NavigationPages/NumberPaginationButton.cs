using WebClient.PaginationNavigation;

namespace WebClient.NavigationPages
{
    public sealed class NumberPaginationButton : BasePaginationButton
    {
        private bool _isActivePage;

        public override string Title => PageNumber.ToString();

        public bool IsActivePage
        {
            get => _isActivePage;
            set => _isActivePage = value;
        }

        public NumberPaginationButton(int pageNumber)
        {
            PageNumber = pageNumber;
            ButtonType = PaginationButtonType.Number;
            IsEnabled = true;
            IsActivePage = false;
        }

        public void MakeActive()
        {
            IsActivePage = true;
        }

        public void Deactivate()
        {
            IsActivePage = false;
        }
    }
}
