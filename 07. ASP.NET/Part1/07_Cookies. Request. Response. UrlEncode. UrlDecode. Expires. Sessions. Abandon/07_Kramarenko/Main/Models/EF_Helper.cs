using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Main.Models
{
	public static class EF_Helper
	{
		public static SqlConnection GetDatabaseConnection(string initialCatalog, string dataSource,
				bool integratedSecurity = true, int timeout = 15)
		{
			SqlConnection sqlConnection = new SqlConnection();

			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
			builder.InitialCatalog = initialCatalog;
			builder.DataSource = dataSource;
			builder.IntegratedSecurity = integratedSecurity;
			builder.ConnectTimeout = timeout;

			sqlConnection.ConnectionString = builder.ConnectionString;
			return sqlConnection;
		}
	}
}