using System.Threading;
using System.Threading.Tasks;
using Dapper;
using GetMore.Application.Interfaces;
using MediatR;

namespace GetMore.Application.Commands
{
    public class AddUnitOfWorkCommandHandler : IRequestHandler<AddUnitOfWorkCommand>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public AddUnitOfWorkCommandHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(AddUnitOfWorkCommand command, CancellationToken cancellationToken)
        {
            const string sql = "INSERT INTO [dbo].[UnitsOfWork] " +
                               "([ProjectId] " +
                               ",[EmployeeId] " +
                               ",[TimeAmount] " +
                               ",[WorkDate]) " +
                               "VALUES " +
                               "(@ProjectId  " +
                               ",@EmployeeId " +
                               ",@TimeAmount " +
                               ",@WorkDate)";

            var connection = this._sqlConnectionFactory.GetOpenConnection();

            await connection.ExecuteAsync(sql, new
            {
                command.ProjectId,
                command.EmployeeId,
                command.TimeAmount,
                command.WorkDate
            });

            return Unit.Value;
        }
    }
}