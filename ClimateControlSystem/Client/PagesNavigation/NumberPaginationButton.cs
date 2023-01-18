namespace ClimateControlSystem.Client.PagesNavigation
{
    public sealed class NumberPaginationButton : BasePaginationButton
    {
        public override string Title => PageNumber.ToString();

        public NumberPaginationButton(int pageNumber)
        {
            PageNumber = pageNumber;
            ButtonType = PaginationButtonType.Number;
            IsEnabled = true;
        }
    }
}
