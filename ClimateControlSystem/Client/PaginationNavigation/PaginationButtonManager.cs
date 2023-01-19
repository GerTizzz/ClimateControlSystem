using ClimateControlSystem.Client.PaginationNavigation;

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
        public int LastPageNumber { get; private set; }
        public int CurrentPageNumber { get; private set; }
        public int RecordsCount { get; set; }
        public int RecordsPerPage { get; set; }

        public Action<RecordsRequest> PageChangedEvent;

        public PaginationButtonManager(int recordsCount, int recordsPerPage)
        {
            if (recordsCount < 0 || recordsPerPage < 1)
            {
                throw new Exception();
            }

            RecordsCount = recordsCount;
            RecordsPerPage = recordsPerPage;

            _visibleSiteDistance = (int)Math.Floor(DefaultPagesWithNumberCount / EachSiteVisibleButtonsCount);

            LastPageNumber = CalculateLastPageNumber(recordsCount, recordsPerPage);

            _numberOfPaginationButtonsCount = LastPageNumber > DefaultPagesWithNumberCount ? DefaultPagesWithNumberCount : LastPageNumber;
            _paginationButtonsCount = DefaultPagesWithNumberCount + NumberOfReservedButtons;
            _paginationButtons = new List<BasePaginationButton>(_paginationButtonsCount);

            InitializePaginationButtonsList();

            CurrentPageNumber = StartPageNumber;
        }

        public void Close()
        {
            UnsubscribeFromPages();
            _paginationButtons.Clear();
        }

        private int CalculateLastPageNumber(int recordsCount, int recordsPerPage)
        {
            int lastPageNumber = recordsCount / recordsPerPage;
            if (recordsCount % recordsPerPage != 0)
            {
                lastPageNumber += 1;
            }
            return lastPageNumber;
        }

        private void InitializePaginationButtonsList()
        {
            _paginationButtons.Add(SpecialPaginationButton.CreateFirstPageButton());
            _paginationButtons.Add(SpecialPaginationButton.CreatePreviousPageButton());

            for (int i = 0; i < _numberOfPaginationButtonsCount; i++)
            {
                _paginationButtons.Add(new NumberPaginationButton(StartPageNumber + i));
            }

            _paginationButtons.Add(SpecialPaginationButton.CreateNextPageButton());
            _paginationButtons.Add(SpecialPaginationButton.CreateLastPageButton());

            UpdateNumberPages(new VisiblePagesRange(StartPageNumber, _numberOfPaginationButtonsCount));

            SubscribeOnPages();
        }

        private void SetSpecialPagesButtonsAvailability()
        {
            if (CurrentPageNumber == _paginationButtons.Skip(SkippingNotNumberPages).First().PageNumber)
            {
                _paginationButtons[0].Disable();
                _paginationButtons[1].Disable();
            }
            else
            {
                _paginationButtons[0].Enable();
                _paginationButtons[1].Enable();
            }

            if (CurrentPageNumber == LastPageNumber)
            {
                _paginationButtons[_paginationButtons.Count - 2].Disable();
                _paginationButtons[_paginationButtons.Count - 1].Disable();
            }
            else
            {
                _paginationButtons[_paginationButtons.Count - 2].Enable();
                _paginationButtons[_paginationButtons.Count - 1].Enable();
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

            VisiblePagesRange pagesRange = DefineStartAndEndIndices();

            UpdateNumberPages(pagesRange);

            SetSpecialPagesButtonsAvailability();

            CallPageChangedEvent();
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
                    return CurrentPageNumber >= LastPageNumber
                        ? CurrentPageNumber
                        : CurrentPageNumber + 1;
                }

                if (specPage.IsLastPage())
                {
                    return LastPageNumber;
                }
            }

            if (nextPage.PageNumber > LastPageNumber)
            {
                return LastPageNumber;
            }
            else if (nextPage.PageNumber < StartPageNumber)
            {
                return StartPageNumber;
            }

            return nextPage.PageNumber;
        }

        private VisiblePagesRange DefineStartAndEndIndices()
        {
            int startPageRangeNumber = CurrentPageNumber - _visibleSiteDistance;
            int endPageRangeNumber = CurrentPageNumber + _visibleSiteDistance;

            if (LastPageNumber < DefaultPagesWithNumberCount)
            {
                return new VisiblePagesRange(StartPageNumber, LastPageNumber);
            }
            else
            {
                if (startPageRangeNumber < StartPageNumber)
                {
                    endPageRangeNumber += (StartPageNumber - startPageRangeNumber);
                    return new VisiblePagesRange(StartPageNumber, endPageRangeNumber);
                }
                else if (endPageRangeNumber > LastPageNumber)
                {
                    startPageRangeNumber -= (endPageRangeNumber - LastPageNumber);
                    return new VisiblePagesRange(startPageRangeNumber, LastPageNumber);
                }
            }

            return new VisiblePagesRange(startPageRangeNumber, endPageRangeNumber);
        }

        private void UpdateNumberPages(VisiblePagesRange pagesRange)
        {
            int counter = 0;

            foreach (var button in _paginationButtons)
            {
                if (button is not NumberPaginationButton numberPageButton)
                {
                    continue;
                }

                int step = pagesRange.Start + counter;

                if (step <= pagesRange.End)
                {
                    numberPageButton.PageNumber = step;
                    numberPageButton.Enable();
                    numberPageButton.Deactivate();

                    if (step == CurrentPageNumber)
                    {
                        numberPageButton.MakeActive();
                    }
                }
                else
                {
                    numberPageButton.Disable();
                }

                counter++;
            }
        }

        private void CallPageChangedEvent()
        {
            int offset = RecordsPerPage * (CurrentPageNumber - PageOffset);

            PageChangedEvent?.Invoke(new RecordsRequest(offset, RecordsPerPage));
        }
    }
}
