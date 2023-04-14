namespace WebClient.PaginationNavigation;

public struct RecordsRequest
{
    public int RecordsOffset { get; }

    public int RecordsCount { get; }

    public RecordsRequest(int offset, int count)
    {
        RecordsOffset = offset;
        RecordsCount = count;
    }
}