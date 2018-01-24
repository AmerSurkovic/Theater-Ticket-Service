using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel;

namespace Zadaca2_Pozoriste
{
    public class IzuzetakDB
    {
        public IzuzetakDB()
        { 
        
        }

        string host = "80.65.65.66",
               serviceName = "etflab.db.lab.etf.unsa.ba",
               userID = "AS16781",
               password = "Eto5al0y";
        
        public OracleConnection GetConnection()
        {
            try
            {
                OracleConnection dbConnection = new OracleConnection();
                dbConnection.ConnectionString = string.Format(
                    @"Data Source=
                        (DESCRIPTION =
                                (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = 1521))
                                (CONNECT_DATA =
                                    (SERVER = DEDICATED)
                                    (SERVICE_NAME = {1})
                                )
                        )
                    ;User Id= {2}; Password= {3}; Persist Security Info=True;",
                    host, serviceName, userID, password);

                return dbConnection; 
            }
            catch(Exception ex)
            {
                //Log exception...
                return null;
            }
        }

        public bool CreateIzuzetakTable()
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                {
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        oc.Open();

                        cmd.CommandText = "CREATE TABLE Izuzeci(ID int PRIMARY KEY, tipIzuzetka varchar(50), datumIzuzetka DATE)";
                        int result = cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                //log exception
                return false;
            }
        }

        public bool DropIzuzetakTable()
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                {
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        oc.Open();
                        cmd.CommandText = "DROP TABLE Izuzeci";
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (OracleException oEx)
            {
                if (oEx.Number == 942)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool InsertIzuzetak(Izuzetak izuzetak)
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        oc.Open();

                        string sqlInsert = "insert into Izuzeci (ID, tipIzuzetka, datumIzuzetka)";
                        sqlInsert += "values (:ID, :tipIzuzetka, :datumIzuzetka)";
                        cmd.CommandText = sqlInsert;

                        OracleParameter id = new OracleParameter();
                        id.Value = izuzetak.ID;
                        id.ParameterName = "ID";
                        cmd.Parameters.Add(id);

                        //Ili ovako inline
                        cmd.Parameters.Add(new OracleParameter("tipIzuzetka", izuzetak.tipIzuzetka));
                        cmd.Parameters.Add(new OracleParameter("datumIzuzetka", izuzetak.datumIzuzetka));

                        cmd.ExecuteNonQuery();
                    }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteIzuzetak(Izuzetak izuzetak)
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        oc.Open();

                        string sqlDelete = "Delete from Izuzeci Where ID = :ID";
                        cmd.CommandText = sqlDelete;

                        cmd.Parameters.Add(new OracleParameter("ID", izuzetak.ID));
                        cmd.ExecuteNonQuery();
                    }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BindingList<Izuzetak> ReadAllIzuzeci()
        {
            BindingList<Izuzetak> izuzeci = new BindingList<Izuzetak>();
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlSelect = "SELECT * FROM Izuzeci";
                    cmd.CommandText = sqlSelect;

                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            izuzeci.Add(new Izuzetak()
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                tipIzuzetka = reader["tipIzuzetka"].ToString(),
                                datumIzuzetka = Convert.ToDateTime(reader["datumIzuzetka"]),
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log
            }
            return izuzeci;
        }

    }
}
