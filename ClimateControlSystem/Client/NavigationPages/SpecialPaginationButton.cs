using ClimateControl.WebClient.PaginationNavigation;

namespace ClimateControl.WebClient.NavigationPages
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

        public override string Title { get; }

        private SpecialPaginationButton(PaginationButtonType selectionPageButtonType)
        {
            ButtonType = selectionPageButtonType;
            IsEnabled = true;

            switch (ButtonType)
            {
                case PaginationButtonType.StartPage:
                    PageNumber = StartPageNumber;
                    Title = StartPageTitle;
                    break;
                case PaginationButtonType.PreviousPage:
                    PageNumber = PreviousPageNumber;
                    Title = PreviousPageTitle;
                    break;
                case PaginationButtonType.NextPage:
                    PageNumber = NextPageNumber;
                    Title = NextPageTitle;
                    break;
                default:
                    PageNumber = LastPageNumber;
                    Title = LastPageTitle;
                    break;
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
