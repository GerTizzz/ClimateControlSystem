using WebClient.NavigationPages;

namespace WebClient.PaginationNavigation
{
    public sealed class PaginationButtonManager
    {
        private const int StartPageNumber = 1;
        private const int SkippingNotNumberPages = 2;
        private const int PageOffset = 1;
        private const int NumberOfReservedButtons = 4;
        private const double EachSiteVisibleButtonsCount = 2.0;
        private const int DefaultPagesWithNumberCount = 5;
        private const int PaginationButtonsCount = DefaultPagesWithNumberCount + NumberOfReservedButtons;

        private readonly int _visibleSiteDistance = (int)Math.Floor(DefaultPagesWithNumberCount / EachSiteVisibleButtonsCount);

        private int _numberOfPaginationButtonsCount;
        private int _currentPageNumber;
        private int _lastPageNumber;
        private int _recordsPerPage;

        public List<BasePaginationButton> PaginationButtons { get; }

        public Action<RecordsRequest> PageChangedEvent;

        public PaginationButtonManager(long recordsCount, int recordsPerPage)
        {
            if (recordsCount < 0) throw new ArgumentException();
            if (recordsPerPage < 1) throw new ArgumentException();

            _recordsPerPage = recordsPerPage;

            PaginationButtons = new List<BasePaginationButton>(PaginationButtonsCount);

            _lastPageNumber = CalculateLastPageNumber(recordsCount, recordsPerPage);

            _currentPageNumber = StartPageNumber;

            InitializeButtons();

            UpdateButtonsContent();
        }

        public void Close()
        {
            UnsubscribeFromPages();
            PaginationButtons.Clear();
        }

        private void UpdateButtonsContent()
        {
            SetSpecialPagesButtonsAvailability();

            UpdateNumberPages(new VisiblePagesRange(StartPageNumber, _numberOfPaginationButtonsCount));
        }

        private static int CalculateLastPageNumber(long recordsCount, int recordsPerPage)
        {
            var lastPageNumber = (int)(recordsCount / recordsPerPage);

            if (recordsCount % recordsPerPage != 0)
            {
                lastPageNumber += 1;
            }

            return lastPageNumber;
        }

        private void InitializeButtons()
        {
            PaginationButtons.Add(SpecialPaginationButton.CreateFirstPageButton());
            PaginationButtons.Add(SpecialPaginationButton.CreatePreviousPageButton());

            _numberOfPaginationButtonsCount = _lastPageNumber > DefaultPagesWithNumberCount ? DefaultPagesWithNumberCount : _lastPageNumber;

            for (var i = 0; i < _numberOfPaginationButtonsCount; i++)
            {
                PaginationButtons.Add(new NumberPaginationButton(StartPageNumber + i));
            }

            PaginationButtons.Add(SpecialPaginationButton.CreateNextPageButton());
            PaginationButtons.Add(SpecialPaginationButton.CreateLastPageButton());

            SubscribeOnButtonsEvents();
        }

        private void SetSpecialPagesButtonsAvailability()
        {
            if (_currentPageNumber == PaginationButtons.Skip(SkippingNotNumberPages).First().PageNumber)
            {
                PaginationButtons[0].Disable();
                PaginationButtons[1].Disable();
            }
            else
            {
                PaginationButtons[0].Enable();
                PaginationButtons[1].Enable();
            }

            if (_currentPageNumber == _lastPageNumber)
            {
                PaginationButtons[^2].Disable();
                PaginationButtons[^1].Disable();
            }
            else
            {
                PaginationButtons[^2].Enable();
                PaginationButtons[^1].Enable();
            }
        }

        private void SubscribeOnButtonsEvents()
        {
            foreach (var page in PaginationButtons)
            {
                page.ButtonPressedEvent += GoToPage;
            }
        }

        private void UnsubscribeFromPages()
        {
            foreach (var page in PaginationButtons)
            {
                page.ButtonPressedEvent -= GoToPage;
            }
        }

        private void GoToPage(BasePaginationButton nextPage)
        {
            _currentPageNumber = GetNextPageNumber(nextPage);

            var pagesRange = DefineStartAndEndIndices();

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
                    return _currentPageNumber <= StartPageNumber
                        ? _currentPageNumber
                        : _currentPageNumber - 1;
                }

                if (specPage.IsNextPage())
                {
                    return _currentPageNumber >= _lastPageNumber
                        ? _currentPageNumber
                        : _currentPageNumber + 1;
                }

                if (specPage.IsLastPage())
                {
                    return _lastPageNumber;
                }
            }

            if (nextPage.PageNumber > _lastPageNumber)
            {
                return _lastPageNumber;
            }
            else if (nextPage.PageNumber < StartPageNumber)
            {
                return StartPageNumber;
            }

            return nextPage.PageNumber;
        }

        private VisiblePagesRange DefineStartAndEndIndices()
        {
            var startPageRangeNumber = _currentPageNumber - _visibleSiteDistance;
            var endPageRangeNumber = _currentPageNumber + _visibleSiteDistance;

            if (_lastPageNumber < DefaultPagesWithNumberCount)
            {
                return new VisiblePagesRange(StartPageNumber, _lastPageNumber);
            }
            else
            {
                if (startPageRangeNumber < StartPageNumber)
                {
                    endPageRangeNumber += StartPageNumber - startPageRangeNumber;
                    return new VisiblePagesRange(StartPageNumber, endPageRangeNumber);
                }
                else if (endPageRangeNumber > _lastPageNumber)
                {
                    startPageRangeNumber -= endPageRangeNumber - _lastPageNumber;
                    return new VisiblePagesRange(startPageRangeNumber, _lastPageNumber);
                }
            }

            return new VisiblePagesRange(startPageRangeNumber, endPageRangeNumber);
        }

        private void UpdateNumberPages(VisiblePagesRange pagesRange)
        {
            var counter = 0;

            foreach (var button in PaginationButtons)
            {
                if (button is not NumberPaginationButton numberPageButton)
                {
                    continue;
                }

                var step = pagesRange.Start + counter;

                if (step <= pagesRange.End)
                {
                    numberPageButton.PageNumber = step;
                    numberPageButton.Enable();
                    numberPageButton.Deactivate();

                    if (step == _currentPageNumber)
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
            var offset = _recordsPerPage * (_currentPageNumber - PageOffset);

            PageChangedEvent?.Invoke(new RecordsRequest(offset, _recordsPerPage));
        }
    }
}
