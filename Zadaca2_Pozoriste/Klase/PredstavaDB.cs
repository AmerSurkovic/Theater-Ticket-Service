using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel;

namespace Zadaca2_Pozoriste
{
    public class PredstavaDB
    {
        public PredstavaDB()
        { 
        
        }

        string host = "80.65.65.66",
               serviceName = "etflab.db.lab.etf.unsa.ba",
               userID = "AS16781",
               password = "Eto5al0y";

        // Postavljanje konekcije
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
            catch (Exception ex)
            {
                //Log exception...
                return null;
            }
        }

        // Kreiramo tabelu predstave
        public bool CreatePredstavaTable()
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                {
                    //kreiramo oracle komandu
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        //otvorimo konekciju!!!!!
                        oc.Open();

                        cmd.CommandText = "CREATE TABLE Predstave(ID int PRIMARY KEY, NazivPredstave varchar(50), TipPredstave varchar(50), CijenaKarte DECIMAL, DatumPredstave DATE, KategorijaPredstave varchar(50))";
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

        // Brisemo tabelu predstave
        public bool DropPredstavaTable()
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                {
                    using (OracleCommand cmd = oc.CreateCommand())
                    {
                        oc.Open();
                        cmd.CommandText = "DROP TABLE Predstave";
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

        public bool InsertPredstava(Predstava predstava)
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    //Imamo auto inkrement na employee tabeli
                    string sqlInsert = "insert into Predstave (ID, nazivPredstave, tipPredstave, cijenaKarte, datumPredstave, kategorijaPredstave)";
                    sqlInsert += "values (:ID, :nazivPredstave, :tipPredstave, :cijenaKarte, :datumPredstave, :kategorijaPredstave)";
                    cmd.CommandText = sqlInsert;

                    //parametar se moze ovako praviti
                    //redoslijed parametara se mora podudariti sa redoslijedom u upitu
                    OracleParameter id = new OracleParameter();
                    id.Value = predstava.ID;
                    id.ParameterName = "ID";
                    cmd.Parameters.Add(id);

                    //Ili ovako inline
                    cmd.Parameters.Add(new OracleParameter("nazivPredstave", predstava.nazivPredstave));
                    cmd.Parameters.Add(new OracleParameter("tipPredstave", predstava.tipPredstave));
                    cmd.Parameters.Add(new OracleParameter("cijenaKarte", predstava.cijenaKarte));
                    cmd.Parameters.Add(new OracleParameter("datumPredstave", predstava.datumPredstave));
                    cmd.Parameters.Add(new OracleParameter("kategorijaPredstave", predstava.kategorijaPredstave));

                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePredstava(Predstava predstava)
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlDelete = "Delete from Predstave Where ID = :ID";
                    cmd.CommandText = sqlDelete;

                    cmd.Parameters.Add(new OracleParameter("ID", predstava.ID));
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdatePredstava(Predstava predstava)
        {
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlUpdate = "Update Predstave set nazivPredstave=:nazivPredstave, tipPredstave=:tipPredstave, cijenaKarte=:cijenaKarte, datumPredstave=:datumPredstave, kategorijaPredstave=:kategorijaPredstave"
                                     + " where nazivPredstave=:nazivPredstave ";
                    cmd.CommandText = sqlUpdate;

                    //redoslijed se mora podudariti sa redoslijedom u upitu
                    cmd.Parameters.Add(new OracleParameter("nazivPredstave", predstava.nazivPredstave));
                    cmd.Parameters.Add(new OracleParameter("tipPredstave", predstava.tipPredstave));
                    cmd.Parameters.Add(new OracleParameter("cijenaKarte", predstava.cijenaKarte));
                    cmd.Parameters.Add(new OracleParameter("datumPredstave", predstava.datumPredstave));
                    cmd.Parameters.Add(new OracleParameter("kategorijaPredstave", predstava.kategorijaPredstave));


                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public BindingList<Predstava> ReadAllPredstave()
        {
            BindingList<Predstava> predstave = new BindingList<Predstava>();
            try
            {
                using (OracleConnection oc = GetConnection())
                using (OracleCommand cmd = oc.CreateCommand())
                {
                    oc.Open();

                    string sqlSelect = "SELECT * FROM Predstave";
                    cmd.CommandText = sqlSelect;

                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            predstave.Add(new Predstava()
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                nazivPredstave = reader["nazivPredstave"].ToString(),
                                tipPredstave = reader["tipPredstave"].ToString(),
                                cijenaKarte = Convert.ToDecimal(reader["cijenaKarte"]),
                                datumPredstave = Convert.ToDateTime(reader["datumPredstave"]),
                                kategorijaPredstave = reader["kategorijaPredstave"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //log
            }
            return predstave;
        }
    
    }
}
