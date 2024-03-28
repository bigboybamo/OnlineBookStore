using OnlineBookStore.Models;

namespace OnlineBookStore.Services
{
    public interface IServiceClass
    {
        Tuple<bool,string> DeleteBooks(List<int> bookIds);
        Tuple<decimal,string> CalculateRevenue(DateTime startDate, DateTime endDate);

        Tuple<IEnumerable<Author>, string> GetTopSellingAuthors();
    }
}
