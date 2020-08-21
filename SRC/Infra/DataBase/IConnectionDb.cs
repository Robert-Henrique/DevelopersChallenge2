using System.Collections.Generic;
using System.Data;

namespace Infra.DataBase
{
    public interface IConnectionDb
    {
        void SaveDataSQLServer(string script, Dictionary<string, object> parameters);
        DataSet GetDataFromSQLServer(string script, Dictionary<string, object> parameters);
    }
}
