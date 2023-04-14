using WebClient.PaginationNavigation;

namespace WebClient.NavigationPages;

public sealed class NumberPaginationButton : BasePaginationButton
{
    public override string Title => PageNumber.ToString();

    public bool IsActivePage { get; private set; }

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