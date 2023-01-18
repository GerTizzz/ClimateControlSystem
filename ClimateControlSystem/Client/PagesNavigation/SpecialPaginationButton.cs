namespace ClimateControlSystem.Client.PagesNavigation
{
    public sealed class SpecialPaginationButton : BasePaginationButton
    {
        private const string StartPageTitle = "<<";
        private const string PreviousPageTitle = "<";
        private const string NextPageTitle = ">";
        private const string LastPageTitle = ">>";

        private const int StartPageNumber = -1;
        private const int PreviousPageNumber = -2;
        private const int NextPageNumber = -3;
        private const int LastPageNumber = -4;

        private string _title;

        public override string Title => _title;

        public SpecialPaginationButton(PaginationButtonType selectionPageButtonType)
        {
            ButtonType = PaginationButtonType.Number;
            IsEnabled = true;

            if (ButtonType == PaginationButtonType.StartPage)
            {
                PageNumber = StartPageNumber;
                _title = StartPageTitle;
            }
            else if (ButtonType == PaginationButtonType.PreviousPage)
            {
                PageNumber = PreviousPageNumber;
                _title = PreviousPageTitle;
            }
            else if (ButtonType == PaginationButtonType.NextPage)
            {
                PageNumber = NextPageNumber;
                _title = NextPageTitle;
            }
            else
            {
                PageNumber = LastPageNumber;
                _title = LastPageTitle;
            }
        }

        public bool IsStartPage()
        {
            return ButtonType == PaginationButtonType.StartPage;
        }

        public bool IsPreviousPage()
        {
            return ButtonType == PaginationButtonType.PreviousPage;
        }

        public bool IsNextPage()
        {
            return ButtonType == PaginationButtonType.NextPage;
        }

        public bool IsLastPage()
        {
            return ButtonType == PaginationButtonType.LastPage;
        }

        public static SpecialPaginationButton CreateFirstPageButton()
        {
            return new SpecialPaginationButton(PaginationButtonType.StartPage);
        }

        public static SpecialPaginationButton CreatePreviousPageButton()
        {
            return new SpecialPaginationButton(PaginationButtonType.PreviousPage);
        }

        public static SpecialPaginationButton CreateNextPageButton()
        {
            return new SpecialPaginationButton(PaginationButtonType.NextPage);
        }

        public static SpecialPaginationButton CreateLastPageButton()
        {
            return new SpecialPaginationButton(PaginationButtonType.LastPage);
        }
    }
}
