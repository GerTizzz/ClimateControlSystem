namespace ClimateControlSystem.Client.PaginationNavigation
{
    public struct VisiblePagesRange
    {
        public int Start { get; }
        public int End { get; }

        public VisiblePagesRange(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
