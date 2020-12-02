using System.Data;

namespace GetMore.Application.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }
}