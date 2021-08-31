using Domain.Entity;
using Domain.Repository;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class UrlMetricRepository : GenericRepository<UrlMetric>, IUrlMetricRepository
    {
        public UrlMetricRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
