using Core.DataAccess.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Views;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SqlConnect
{
    public class SCDataElementDal : IDataElementDal
    {
        public List<DataElement> Get(Property property)
        {
            List<DataElement> dataElements = new List<DataElement>();
            string commandText = "";
            if(property.dataTypeName == "proc")
            {
                commandText = $"exec {property.dataTypeValue}";
            }
            else if(property.dataTypeName == "view")
            {
                commandText = $"select * from {property.dataTypeValue}";
            }
            else if(property.dataTypeName == "function")
            {
                commandText = $"select * from {property.dataTypeValue}()";
            }
            using (SqlConnection _con = new SqlConnection(ConnectionString.GetScConnectionString()))
            {
                _con.Open();
                using (var _cmd = new SqlCommand() { Connection = _con, CommandText = commandText })
                {
                    SqlDataReader reader = _cmd.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            DataElement dataElement = new DataElement();
                            dataElement.Id = reader.GetInt32(0);
                            dataElement.Year = reader.GetInt32(1);
                            dataElement.SoldProduct = reader.GetInt32(2);
                            dataElement.DataSetId = reader.GetInt32(3);
                            dataElements.Add(dataElement);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                _con.Close();
            }
            return dataElements;
        }
        public List<DataElement> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<DataElement> GetAllByFunction()
        {
            throw new NotImplementedException();
        }

        public List<DataElement> GetAllByStoredProcedure()
        {
            throw new NotImplementedException();
        }

        public List<GetDataElementView> GetAllByView()
        {
            throw new NotImplementedException();
        }

        public bool TryConnection()
        {
            try
            {
                using (SqlConnection _con = new SqlConnection(ConnectionString.GetScConnectionString()))
                {
                    _con.Open();
                    _con.Close();
                }
                return true;
            }
            catch{
                return false;
            }
        }
        public List<string> GetDatabaseNames()
        {
            List<string> databaseNames = new List<string>();
            try
            {
                using (SqlConnection _con = new SqlConnection(ConnectionString.GetScConnectionString()))
                {
                    _con.Open();
                    DataTable databases = _con.GetSchema("Databases");
                    foreach (DataRow database in databases.Rows)
                    {
                        databaseNames.Add(database.Field<string>("database_name"));
                    }
                }
            }
            catch { }
            return databaseNames;
        }
        public List<Property> GetPropertyNames()
        {
            List<Property> Properties = new List<Property>();
            try
            {
                using (SqlConnection _con = new SqlConnection(ConnectionString.GetScConnectionString()))
                {
                    _con.Open();
                    using (var _cmd = new SqlCommand() { Connection = _con, CommandText = "SELECT OBJECT_NAME(sys.sql_modules.object_id), OBJECT_DEFINITION(sys.sql_modules.object_id) FROM sys.sql_modules" })
                    {              
                        SqlDataReader reader = _cmd.ExecuteReader();
                        try
                        {
                            while (reader.Read())
                            {
                                Property property = new Property();
                                string propertyNameInMethod = reader.GetString(0);
                                string propertyType = reader.GetString(1).ToLower(new CultureInfo("en-US", false));
                                string[] propertyTypeList = propertyType.Split(' ');
                                propertyType = propertyTypeList[1];
                                property.dataTypeValue = propertyNameInMethod;
                                property.dataTypeName = propertyType;
                                Properties.Add(property);
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
            }
            catch { }
            return Properties;
        }
    }
}
