using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using GetMore.Application.Interfaces;
using MediatR;

namespace GetMore.Application.Queries
{
    public class GetUnitsOfWorkForWeekQueryHandler : IRequestHandler<GetUnitsOfWorkForWeekQuery, IEnumerable<UnitsOfWorkForWeekDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetUnitsOfWorkForWeekQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<IEnumerable<UnitsOfWorkForWeekDto>> Handle(GetUnitsOfWorkForWeekQuery query, CancellationToken cancellationToken)
        {
            const string sql = "SELECT [UnitOfWorkId] " +
                               ",[UnitsOfWork].[ProjectId] " +
                               ",[Projects].[Name] AS ProjectName " +
                               ",[UnitsOfWork].[EmployeeId] " +
                               ",[Employees].[Name] AS EmployeeName " +
                               ",[UnitsOfWork].[TimeAmount] " +
                               ",[UnitsOfWork].[WorkDate] " +
                               "FROM [HourRegistration].[dbo].[UnitsOfWork] " +
                               "LEFT JOIN Projects ON UnitsOfWork.ProjectId = Projects.ProjectId " +
                               "LEFT JOIN Employees ON UnitsOfWork.EmployeeId = Employees.EmployeeId " +
                                "WHERE WorkDate >= @StartWorkDate AND WorkDate <= @EndWorkDate";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            return await connection.QueryAsync<UnitsOfWorkForWeekDto>(sql, new
            {
                query.StartWorkDate, query.EndWorkDate
            });
        }
    }
}