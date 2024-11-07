namespace DenemeTakipAPI.Application.Features.Queries.Konu.GetAllKonularPaginated
{
    public class GetAllKonularPaginatedQueryResponse
    {
        public int TotalCount { get; set; }
        public object Konular { get; set; }
    }
}