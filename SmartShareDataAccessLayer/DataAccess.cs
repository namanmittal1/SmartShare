using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartShareCommon;

namespace SmartShareDataAccessLayer
{
    public static class DataAccess
    {       

        public static Boolean SaveClient(Client client)
        {
            SQLiteConnection connection = null;
            SQLiteCommand cmd = null;

            try
            {
                connection = DataBaseConnection.GetDbInstance().GetDBConnection();

                string SQL = "INSERT INTO Clients (Name,IPAddress,SystemName,SharedPath) VALUES";
                SQL += "(@name, @ipaddress, @systemname, @sharedpath)";

                cmd = new SQLiteCommand(SQL);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@name", client.FriendlyName);
                cmd.Parameters.AddWithValue("@ipaddress", client.IPAddress);
                cmd.Parameters.AddWithValue("@systemname", client.SystemName);
                cmd.Parameters.AddWithValue("@sharedpath", client.ClientSharedPath);

                Boolean result = SQLCommands.InsertUpdateRemoveEntity(cmd);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public static List<Client> GetSavedClients()
        {
            SQLiteConnection connection = null;
            SQLiteCommand cmd = null;
            List<Client> clients = null;
            try
            {
                clients = new List<Client>();
                connection = DataBaseConnection.GetDbInstance().GetDBConnection();
                string SQL = "Select * From Clients";

                cmd = new SQLiteCommand(SQL);
                cmd.Connection = connection;

                SQLiteDataReader reader = SQLCommands.GetAllEntities(cmd);
                while (reader.Read())
                {
                    clients.Add(new Client(reader.GetString(1), reader.GetString(2),
                        reader.GetString(3), reader.GetString(4)));
                }
                return clients;
            }
            catch (Exception e)
            {
                return clients;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public static Boolean UpdateSharedDirectory(String sharedDirectory)
        {
            SQLiteConnection connection = null;
            SQLiteCommand cmd = null;
            try
            {
                connection = DataBaseConnection.GetDbInstance().GetDBConnection();
                string SQL = "Update UserSharedPath set SharedPath='" + sharedDirectory + "' where Id=1";

                cmd = new SQLiteCommand(SQL);
                cmd.Connection = connection;
                Boolean result = SQLCommands.InsertUpdateRemoveEntity(cmd);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public static String GetSharedDirectory()
        {
            SQLiteConnection connection = null;
            SQLiteCommand cmd = null;
            try
            {
                String sharedPath = "";
                connection = DataBaseConnection.GetDbInstance().GetDBConnection();
                string SQL = "Select SharedPath From UserSharedPath";

                cmd = new SQLiteCommand(SQL);
                cmd.Connection = connection;

                SQLiteDataReader reader = SQLCommands.GetAllEntities(cmd);
                while (reader.Read())
                {
                    sharedPath = reader.GetString(0);
                }
                return sharedPath;
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }

        public static Boolean SaveSharedDirectory(string sharedPath)
        {
            SQLiteConnection connection = null;
            SQLiteCommand cmd = null;

            try
            {
                connection = DataBaseConnection.GetDbInstance().GetDBConnection();

                string SQL = "INSERT INTO UserSharedPath (SharedPath) VALUES";
                SQL += "(@sharedpath)";

                cmd = new SQLiteCommand(SQL);
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@sharedpath", sharedPath);

                Boolean result = SQLCommands.InsertUpdateRemoveEntity(cmd);

                return result;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                cmd.Dispose();
                connection.Close();
            }
        }
    }
}
