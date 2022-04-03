using Microsoft.Data.SqlClient;
using MediatR;
using Dapper;

namespace APISupport.Domain.Queries
{
    public class LastSupportRequestsQueryHandler :
        IRequestHandler<LastSupportRequestsQuery, IEnumerable<LastSupportRequestsQueryResult>>
    {
        private readonly IConfiguration _configuration;

        public LastSupportRequestsQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<LastSupportRequestsQueryResult>> Handle(LastSupportRequestsQuery request, CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(
                _configuration.GetConnectionString("DBSupport"));
            
            connection.Open();
            var result = await connection.QueryAsync<LastSupportRequestsQueryResult>(
                "SELECT TOP (@NumberLastSupportRequests) * " +
                "FROM dbo.SupportRequests " +
                "ORDER BY Id DESC",
                new { NumberLastSupportRequests = request.NumberLastSupportRequests });
            connection.Close();

            return result;
        }
    }
}