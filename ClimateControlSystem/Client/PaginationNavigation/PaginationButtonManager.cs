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

        private readonly int _visibleSiteDistance = (int)Math.Floor(DefaultPagesWithNumberCount / EachSiteVisibleButtonsCount);
        private readonly int _paginationButtonsCount = DefaultPagesWithNumberCount + NumberOfReservedButtons;

        private List<BasePaginationButton> _paginationButtons;
        private int _numberOfPaginationButtonsCount;
        private long _recordsCount;
        private int _currentPageNumber;
        private int _lastPageNumber;
        private int _recordsPerPage;

        public List<BasePaginationButton> PaginationButtons => _paginationButtons;

        public Action<RecordsRequest> PageChangedEvent;

        public PaginationButtonManager(long recordsCount, int recordsPerPage)
        {
            try
            {
                _paginationButtons = new List<BasePaginationButton>(_paginationButtonsCount);

                UpdatePaginationInfo(recordsCount, recordsPerPage);

                InitializeButtons();

                UpdateButtonsContent();
            }
            catch
            {
                throw;
            }
        }


        public void UpdateRecordsCount(int recordsCount, int recordsPerPage)
        {
            UpdatePaginationInfo(recordsCount, recordsPerPage);

            UpdateButtonsContent();
        }

        public void Close()
        {
            UnsubscribeFromPages();
            _paginationButtons.Clear();
        }

        private void UpdateButtonsContent()
        {
            SetSpecialPagesButtonsAvailability();

            UpdateNumberPages(new VisiblePagesRange(StartPageNumber, _numberOfPaginationButtonsCount));
        }

        private void UpdatePaginationInfo(long recordsCount, int recordsPerPage)
        {
            if (recordsCount < 0) throw new ArgumentException();
            if (recordsPerPage < 1) throw new ArgumentException();

            _recordsCount = recordsCount;
            _recordsPerPage = recordsPerPage;

            _lastPageNumber = CalculateLastPageNumber(recordsCount, recordsPerPage);
            _currentPageNumber = StartPageNumber;
        }

        private int CalculateLastPageNumber(long recordsCount, int recordsPerPage)
        {
            int lastPageNumber = (int)(recordsCount / recordsPerPage);
            if (recordsCount % recordsPerPage != 0)
            {
                lastPageNumber += 1;
            }
            return lastPageNumber;
        }

        private void InitializeButtons()
        {
            _paginationButtons.Add(SpecialPaginationButton.CreateFirstPageButton());
            _paginationButtons.Add(SpecialPaginationButton.CreatePreviousPageButton());

            _numberOfPaginationButtonsCount = _lastPageNumber > DefaultPagesWithNumberCount ? DefaultPagesWithNumberCount : _lastPageNumber;

            for (int i = 0; i < _numberOfPaginationButtonsCount; i++)
            {
                _paginationButtons.Add(new NumberPaginationButton(StartPageNumber + i));
            }

            _paginationButtons.Add(SpecialPaginationButton.CreateNextPageButton());
            _paginationButtons.Add(SpecialPaginationButton.CreateLastPageButton());

            SubscribeOnButtonsEvents();
        }

        private void SetSpecialPagesButtonsAvailability()
        {
            if (_currentPageNumber == _paginationButtons.Skip(SkippingNotNumberPages).First().PageNumber)
            {
                _paginationButtons[0].Disable();
                _paginationButtons[1].Disable();
            }
            else
            {
                _paginationButtons[0].Enable();
                _paginationButtons[1].Enable();
            }

            if (_currentPageNumber == _lastPageNumber)
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

        private void SubscribeOnButtonsEvents()
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
            _currentPageNumber = GetNextPageNumber(nextPage);

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
            int startPageRangeNumber = _currentPageNumber - _visibleSiteDistance;
            int endPageRangeNumber = _currentPageNumber + _visibleSiteDistance;

            if (_lastPageNumber < DefaultPagesWithNumberCount)
            {
                return new VisiblePagesRange(StartPageNumber, _lastPageNumber);
            }
            else
            {
                if (startPageRangeNumber < StartPageNumber)
                {
                    endPageRangeNumber += (StartPageNumber - startPageRangeNumber);
                    return new VisiblePagesRange(StartPageNumber, endPageRangeNumber);
                }
                else if (endPageRangeNumber > _lastPageNumber)
                {
                    startPageRangeNumber -= (endPageRangeNumber - _lastPageNumber);
                    return new VisiblePagesRange(startPageRangeNumber, _lastPageNumber);
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
            int offset = _recordsPerPage * (_currentPageNumber - PageOffset);

            PageChangedEvent?.Invoke(new RecordsRequest(offset, _recordsPerPage));
        }
    }
}
