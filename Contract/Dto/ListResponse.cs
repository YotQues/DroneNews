
namespace DroneNews.Contract.Dto;

public class ListResponse<TData>
{
    public int TotalItems { get; set; }

    public IEnumerable<TData> Items { get; set; }
    public bool IsLastPage { get; set; }

}
