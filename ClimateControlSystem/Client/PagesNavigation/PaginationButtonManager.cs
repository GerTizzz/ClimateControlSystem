using Microsoft.AspNetCore.Components;

namespace ClimateControlSystem.Client.PagesNavigation
{
    public sealed class PaginationButtonManager
    {
        private const int StartPageNumber = 1;
        private const int SkippingNotNumberPages = 2;
        private const int PageOffset = 1;
        private const int NumberOfReservedButtons = 4;
        private const double EachSiteVisibleButtonsCount = 2.0;
        private const int DefaultPagesWithNumberCount = 5;

        private List<BasePaginationButton> _paginationButtons;
        private int _visibleSiteDistance;
        private int _paginationButtonsCount;
        private int _numberOfPaginationButtonsCount;

        public List<BasePaginationButton> PaginationButtons => _paginationButtons;
        public int LastNumberPageNumber { get; private set; }
        public int CurrentPageNumber { get; private set; }
        public int RecordsCount { get; set; }
        public int RecordsPerPage { get; set; }

        public Func<Tuple<int, int>, Task> PageChangedEvent;

        public PaginationButtonManager(int recordsCount, int recordsPerPage)
        {
            if (RecordsCount < 0 || RecordsPerPage < 1)
            {
                throw new Exception();
            }

            RecordsCount = recordsCount;
            RecordsPerPage = recordsPerPage;

            _visibleSiteDistance = (int)Math.Floor(DefaultPagesWithNumberCount / EachSiteVisibleButtonsCount);

            CalculateLastPageNumber();

            _numberOfPaginationButtonsCount = LastNumberPageNumber > DefaultPagesWithNumberCount ? DefaultPagesWithNumberCount : LastNumberPageNumber;
            _paginationButtonsCount = DefaultPagesWithNumberCount + NumberOfReservedButtons;
            _paginationButtons = new List<BasePaginationButton>(_paginationButtonsCount);

            InitializePaginationButtonsList();

            CurrentPageNumber = StartPageNumber;
        }

        public void Close()
        {
            UnsubscribeFromPages();
            PaginationButtons.Clear();
        }

        private void CalculateLastPageNumber()
        {
            LastNumberPageNumber = RecordsCount / RecordsPerPage;
            if (RecordsCount % RecordsPerPage != 0)
            {
                LastNumberPageNumber += 1;
            }
        }

        private void InitializePaginationButtonsList()
        {
            PaginationButtons.Add(SpecialPaginationButton.CreateFirstPageButton());
            PaginationButtons.Add(SpecialPaginationButton.CreatePreviousPageButton());

            for (int i = 0; i < _numberOfPaginationButtonsCount; i++)
            {
                PaginationButtons.Add(new NumberPaginationButton(StartPageNumber + i));
            }

            PaginationButtons.Add(SpecialPaginationButton.CreateNextPageButton());
            PaginationButtons.Add(SpecialPaginationButton.CreateLastPageButton());

            CheckPagesButtonsAvailability();

            SubscribeOnPages();
        }

        private void CheckPagesButtonsAvailability()
        {
            if (CurrentPageNumber == PaginationButtons.Skip(SkippingNotNumberPages).First().PageNumber)
            {
                PaginationButtons[0].Disable();
                PaginationButtons[1].Disable();
            }
            else
            {
                PaginationButtons[0].Enable();
                PaginationButtons[1].Enable();
            }

            if (CurrentPageNumber == LastNumberPageNumber)
            {
                PaginationButtons[PaginationButtons.Count - 2].Disable();
                PaginationButtons[PaginationButtons.Count - 1].Disable();
            }
            else
            {
                PaginationButtons[PaginationButtons.Count - 2].Enable();
                PaginationButtons[PaginationButtons.Count - 1].Enable();
            }
        }

        private void SubscribeOnPages()
        {
            foreach (var page in _paginationButtons)
            {
                page.ButtonPressedEvent += GoToPage;
            }
        }

        private void UnsubscribeFromPages()
        {
            foreach (var page in _paginationButtons)
            {
                page.ButtonPressedEvent -= GoToPage;
            }
        }

        private void GoToPage(BasePaginationButton nextPage)
        {
            CurrentPageNumber = GetNextPageNumber(nextPage);

            int offset = RecordsPerPage * (CurrentPageNumber - PageOffset);

            (int start, int end) = DefineStartAndEndIndices();

            CheckPagesButtonsAvailability();
        }

        private int GetNextPageNumber(BasePaginationButton nextPage)
        {
            if (nextPage is SpecialPaginationButton specPage)
            {
                if (specPage.IsStartPage())
                {
                    return StartPageNumber;
                }

                if (specPage.IsPreviousPage())
                {
                    return CurrentPageNumber <= StartPageNumber
                        ? CurrentPageNumber
                        : CurrentPageNumber - 1;
                }

                if (specPage.IsNextPage())
                {
                    return CurrentPageNumber >= LastNumberPageNumber
                        ? CurrentPageNumber
                        : CurrentPageNumber + 1;
                }

                if (specPage.IsLastPage())
                {
                    return LastNumberPageNumber;
                }
            }

            return nextPage.PageNumber;
        }

        private (int start, int end) DefineStartAndEndIndices()
        {
            int startIndex = CurrentPageNumber - _visibleSiteDistance;
            int endIndex = CurrentPageNumber + _visibleSiteDistance;

            if (LastNumberPageNumber < DefaultPagesWithNumberCount)
            {
                startIndex = StartPageNumber;
                endIndex = LastNumberPageNumber;
            }
            else
            {
                if (startIndex < StartPageNumber)
                {
                    endIndex += (StartPageNumber - startIndex);
                    startIndex = StartPageNumber;
                }
                else if (endIndex > LastNumberPageNumber)
                {
                    startIndex -= (endIndex - LastNumberPageNumber);
                    endIndex = LastNumberPageNumber;
                }
            }

            return (startIndex, endIndex);
        }
    }
}
