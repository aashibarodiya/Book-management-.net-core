using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.Utils
{
    public class DbManager
    {
        Func<DbConnection> connectionFactory;
        public DbManager(Func<DbConnection> connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<T> ExecuteCommand<T>(Func<DbCommand,Task<T>> commandExecutor)
        {
            DbConnection connection = null;
            try
            {
                connection = connectionFactory();
                connection.Open();
                var command=connection.CreateCommand();

                //I don't know what to do with this command
                return await commandExecutor(command);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
