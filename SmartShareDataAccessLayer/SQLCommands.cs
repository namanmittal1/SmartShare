using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareDataAccessLayer
{
    public class SQLCommands
    {
        public static Boolean InsertUpdateRemoveEntity(SQLiteCommand cmd)
        {
            SQLiteTransaction trans;
            trans = cmd.Connection.BeginTransaction();
            cmd.Transaction = trans;
            int retval = 0;
            try
            {
                retval = cmd.ExecuteNonQuery();
                if (retval == 1)
                {
                    trans.Commit();
                    return true;
                }
                else
                {
                    trans.Rollback();
                    return false;
                }
            }
            catch (SQLiteException ex)
            {
                trans.Rollback();
                return false;
            }
            finally
            {

            }
        }

        public static SQLiteDataReader GetAllEntities(SQLiteCommand cmd)
        {
            try
            {
                SQLiteDataReader r = cmd.ExecuteReader();
                return r;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }

        public static Boolean UpdateEntity(SQLiteCommand cmd)
        {
            return (InsertUpdateRemoveEntity(cmd));
        }

        public static Boolean RemoveEntity(SQLiteCommand cmd)
        {
            return (InsertUpdateRemoveEntity(cmd));
        }
    }
}
