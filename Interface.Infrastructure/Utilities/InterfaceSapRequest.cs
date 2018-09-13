using Common.Logging;

using System.Data;
using SAP.Middleware.Connector;
using System.Collections.Generic;

using Interface.Infrastructure.Entities;


namespace Interface.Infrastructure.Utilities
{
    public class InterfaceSapRequest
    {
        private static bool _isConnec;

        public static bool IsConnec
        {
            get
            {
                return _isConnec;
            }
        }

        private static ILog logeer = LogManager.GetLogger("InterfaceSapRequest");


        public static DataTable Query(SapParameters sap)
        {
            DataTable dt = new DataTable();

            try
            {
                RfcDestination rfc = sap.ServerHost;

                rfc.Ping();

                RfcRepository repo = rfc.Repository;
                IRfcFunction function = repo.CreateFunction(sap.FunctionName);

                foreach (KeyValuePair<string, string> keyValuePair in sap.Parameter)
                    function.SetValue(keyValuePair.Key, keyValuePair.Value);

                function.Invoke(rfc);

                IRfcTable rfcTable = function.GetTable(sap.TableName);
                dt = GetDataTableFromRFCTable(rfcTable);

                _isConnec = true;
            }
            catch (System.Exception ex)
            {
                _isConnec = false;
                logeer.InfoFormat("{0}", ex.Message);
            }

            return dt;
        }

        private static DataTable GetDataTableFromRFCTable(IRfcTable rfcTable)
        {
            if (rfcTable == null || rfcTable.RowCount == 0)
                return null;

            DataTable data = new DataTable();

            int columns = 0;

            for (columns = 0; columns <= rfcTable.ElementCount - 1; columns++)
            {
                RfcElementMetadata metadata = rfcTable.GetElementMetadata(columns);
                data.Columns.Add(metadata.Name);
            }

            foreach (IRfcStructure Row in rfcTable)
            {
                DataRow ldr = data.NewRow();

                for (columns = 0; columns <= rfcTable.ElementCount - 1; columns++)
                {
                    RfcElementMetadata metadata = rfcTable.GetElementMetadata(columns);
                    ldr[metadata.Name] = Row.GetString(metadata.Name);
                }
                data.Rows.Add(ldr);
            }

            return data;
        }
    }
}
