using System.Data;
using System.Data.SqlClient;
using Common.Logging;

namespace Interface.Infrastructure.Persistence
{
    public class DataHandler
    {
        private string _connectionString = string.Empty;

        private static ILog logeer = LogManager.GetLogger("Persistence");

        public DataHandler(string connectionString)
        {
            _connectionString = connectionString;
        }

        private static SqlParameter GetParameters(DataTable table)
        {
            SqlParameter tvpParam = new SqlParameter("@ManyRows", SqlDbType.Structured) { Value = table };
            return tvpParam;
        }

        public DataTable QueryParameters(string cmdText)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                            sda.Fill(dataTable);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable QueryParameters(string cmdText, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters.Length > 0) command.Parameters.AddRange(parameters);

                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                            sda.Fill(dataTable);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetInterfaceData(string cmdText)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                            sda.Fill(dataTable);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable GetInterfaceData(string cmdText, SqlParameter[] parameters)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;
                        command.CommandType = CommandType.StoredProcedure;

                        if (parameters.Length > 0) command.Parameters.AddRange(parameters);

                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                            sda.Fill(dataTable);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public DataTable ReturnInterfaceData(string cmdText)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;

                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter sda = new SqlDataAdapter(command))
                            sda.Fill(dataTable);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return dataTable;
        }

        public void UpdateInterfaceData(string cmdText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;

                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void AddInterfaceData(string cmdText, DataTable source)
        {
            if (source == null || source.Rows.Count == 0) return;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;

                        SqlParameter sql = GetParameters(source);
                        command.Parameters.Add(sql);

                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateInterfaceData(string cmdText, DataTable source)
        {
            if (source == null || source.Rows.Count == 0) return;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand command = new SqlCommand(cmdText, connection))
                    {
                        connection.Open();
                        command.CommandTimeout = 600;

                        SqlParameter sql = GetParameters(source);
                        command.Parameters.Add(sql);

                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
