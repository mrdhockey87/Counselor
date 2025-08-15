using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;

using System.Data.SQLite;

using System.Security.Cryptography;

namespace CounselQuickPlatinum
{
    static class DatabaseConnection
    {
        private class InvalidUpdateScriptException : Exception
        {
            public InvalidUpdateScriptException(string errorDisplayMessage) : base(errorDisplayMessage)
            {
            }
        }

        static private SQLiteDataAdapter adapter;
        static private SQLiteConnection connection;
        static private bool isValid;
        
        static private string databaseFilename;

        static private string oldBackupName = "";
        static private string currentBackupName = "";

        //static private string oldCheckpointFilename = "";
        static private string currentCheckpointFilename = "";

        static private Mutex databaseLock;

        static private Mutex batchUpdateMutex;
        static private Thread batchUpdateThread;
        static private bool performingBatchUpdate;

        static internal bool PerformingBatchUpdate { get { return performingBatchUpdate; } }

        static SQLiteTransaction transaction;
        static DirectoryInfo databaseDirectory;

        static public void BeginTransaction()
        {
            transaction = connection.BeginTransaction();
        }

        static public void EndTransaction()
        {
            transaction.Commit();
            transaction = null;
        }


        static public void Backup()
        {
            Lock();

            if (currentCheckpointFilename != "")
            {
                Unlock();
                return;
            }

            //string databasePath = Utilities.GetCQPUserDataDirectory() + @"\sqlite";
            //string databaseBackupFilename = databasePath + @"\~" + DateTime.Now.Ticks + ".db3";
            string databaseBackupFilename = databaseDirectory.FullName + @"\~" + DateTime.Now.Ticks + ".db3";

            oldBackupName = currentBackupName;
            currentBackupName = databaseBackupFilename;

            //System.IO.File.Copy(databaseFilename, currentBackupName, true);
            FileUtils.BlockingFileCopy(databaseFilename, currentBackupName);

            if(oldBackupName != "")
                System.IO.File.Delete(oldBackupName);

            Unlock();
        }


        static public void SetCheckPoint()
        {
            Lock();

            //oldCheckpointFilename = currentCheckpointFilename;

            //string databasePath = Utilities.GetCQPUserDataDirectory() + @"\sqlite";
            //currentCheckpointFilename = databasePath + @"\~checkpoint" + DateTime.Now.Ticks + ".db3";
            currentCheckpointFilename = databaseDirectory.FullName + @"\~checkpoint" + DateTime.Now.Ticks + ".db3";

            //System.IO.File.Copy(databaseFilename, currentCheckpointFilename, true);
            FileUtils.BlockingFileCopy(databaseFilename, currentCheckpointFilename);

            //if (oldCheckpointFilename != "")
            //    System.IO.File.Delete(oldCheckpointFilename);

            if (currentBackupName != "")
            {
                File.Delete(currentBackupName);
                currentBackupName = "";
            }

            Unlock();
        }


        internal static void Restore()
        {
            if (currentCheckpointFilename == "")
            {
                //System.IO.File.Copy(currentBackupName, databaseFilename,true);
                //FileUtils.BlockingFileCopy(currentBackupName, databaseFilename);
                //File.Delete(currentBackupName);
                RestoreBackup();

            }
            else
            {
                //System.IO.File.Copy(currentCheckpointFilename, databaseFilename, true);
                //FileUtils.BlockingFileCopy(currentCheckpointFilename, databaseFilename);
                //File.Delete(currentCheckpointFilename);

                //currentCheckpointFilename = "";
                //return;
                RestoreCheckpoint();

            }

            //currentBackupName = "";
        }

        private static void RestoreBackup()
        {
            if (currentBackupName == "")
                return;

            FileUtils.BlockingFileCopy(currentBackupName, databaseFilename);
            //File.Delete(currentBackupName);
            //Backup();
        }


        internal static void RestoreCheckpoint()
        {
            //System.IO.File.Copy(currentCheckpointFilename, databaseFilename, true);
            FileUtils.BlockingFileCopy(currentCheckpointFilename, databaseFilename);
            File.Delete(currentCheckpointFilename);
            Backup();

            currentCheckpointFilename = "";
            //oldCheckpointFilename = "";
        }


        internal static void ClearCheckpoint()
        {
            if (currentCheckpointFilename != "")
            {
                File.Delete(currentCheckpointFilename);
                currentCheckpointFilename = "";
            }

            Backup();
        }


        internal static void DeleteBackups()
        {
            try
            {
                Cleanup();
                /*System.IO.File.Delete(currentBackupName);
                ClearCheckpoint();*/
            }
            catch (Exception)
            {

            }
        }


        static public void Open()
        {
            Lock();
            //connection.Open();
        }


        static internal void BatchUpdateLock()
        {
            performingBatchUpdate = true;
            batchUpdateMutex.WaitOne();
            batchUpdateThread = Thread.CurrentThread;
        }


        static internal void BatchUpdateUnlock()
        {
            batchUpdateMutex.ReleaseMutex();
            batchUpdateThread = null;
            performingBatchUpdate = false;
        }


        static void Lock()
        {
            if (batchUpdateThread == null || batchUpdateThread != Thread.CurrentThread)
                batchUpdateMutex.WaitOne();

            databaseLock.WaitOne();
        }


        static void Unlock()
        {
            databaseLock.ReleaseMutex();

            if (!performingBatchUpdate && batchUpdateThread != Thread.CurrentThread)
                batchUpdateMutex.ReleaseMutex();
        }


        static public void Close()
        {
            //connection.Close();
            Unlock();
        }

        static public void CloseDatabase()
        {
            if(connection != null)
                connection.Close();
        }


        static public bool Initialize(FileInfo databaseFile)
        {
            CloseDatabase();

            Logger.Trace();

            if (Directory.Exists(databaseFile.DirectoryName) == false)
                Directory.CreateDirectory(databaseFile.DirectoryName);

            if (databaseFile.Directory.GetFiles("*.db3").Count() > 1)
            {
                RepairFromMostRecentBackup(databaseFile);
            }
            else if (databaseFile.Exists == false)
            {
                FileInfo restoreCopy = GetBaseCopy();
                FileUtils.BlockingFileCopy(restoreCopy, databaseFile);
            }

            databaseFilename = databaseFile.FullName;

            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder();
            connectionStringBuilder.DataSource = databaseFile.FullName;
            connectionStringBuilder.Version = 3;
            connectionStringBuilder.ForeignKeys = false;
            connectionStringBuilder.Password = "fr#=eqAphe2e@azaD?t8brab5-p3s3e#";
            connectionStringBuilder.DateTimeFormat = SQLiteDateFormats.Ticks;
            
            connection = new SQLiteConnection(connectionStringBuilder.ConnectionString);
            
            databaseLock = new Mutex();
            batchUpdateMutex = new Mutex();

            performingBatchUpdate = false;

            try
            {
                connection.Open();
                //connection.ChangePassword("fr#=eqAphe2e@azaD?t8brab5-p3s3e#");
                connection.GetSchema("Tables");
            }
            catch (SQLiteException ex)
            {
                isValid = false;
                CQPMessageBox.Show("The soldiers and documents database could not be opened.", "Error", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                Logger.Error("Error initializing the database", ex);

                return isValid;
            }

            databaseDirectory = databaseFile.Directory;

            //Cleanup(databaseFile);
            Cleanup();

            Backup();

            isValid = true;
            return isValid;
        }


        //private static void Cleanup(FileInfo databaseFile)
        private static void Cleanup()
        {
            Logger.Trace();

            //string sqlDirectory = Utilities.GetCQPUserDataDirectory() + "\\sqlite";
            string sqlDirectory = databaseDirectory.FullName;
            DirectoryInfo directory = new DirectoryInfo(sqlDirectory);


            FileInfo []files 
                //= databaseFile.Directory.GetFiles("*.db3").Where(file => file.Name != databaseFile.Name).ToArray();
                //= Directory.GetFiles(sqlDirectory, "*.db3").Where(file => file != databaseFilename).ToArray();
                = directory.GetFiles("*.db3").Where(file => file.FullName.ToLower() != databaseFilename.ToLower()).ToArray();

            Logger.Trace("Deleting " + files.Count() + " files");

            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }


        private static FileInfo GetBaseCopy()
        {
            Logger.Trace("Regenerating.  Current working directory is: " + Directory.GetCurrentDirectory());

            string applicationSQLPath = @"..\SQLite";
            FileInfo baseDatabase = new FileInfo(applicationSQLPath + @"\EmptyCQPForXML.db3");

            Logger.Trace("Expected file path: " + baseDatabase.FullName);

            if (baseDatabase.Exists == false)
                throw new CQPException("Fatal error:  Working copy database not present and base could not be found.");

            return baseDatabase;
        }


        static internal void RepairFromMostRecentBackup(FileInfo databaseFile)
        {
            FileInfo[] files = databaseFile.Directory.GetFiles("*.db3");
            files = files.OrderBy(file => file.LastAccessTime).ToArray();

            int i = files.Length - 1;
            while (i >= 0)
            {
                if (files[i].Name != databaseFile.Name)
                    break;

                i--;
            }

            if (i > -1)
                FileUtils.BlockingFileCopy(files[i], databaseFile);

            // else
            // Attempt repair?
            // Find abandoned nodes and whatnot?
        }




        static public bool IsValid
        {
            get { return isValid; }
        }


        static public void Delete(string tableName, string whereColumn, string pkid)
        {
            Open();

            string deleteCommand = "delete from " + tableName + " where " + whereColumn + " = @pkid";
            //string deleteCommand = "delete from @tableName where @whereColumn = @pkid";

            try
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = deleteCommand;
                //command.Parameters.AddWithValue("@tableName", tableName);
                //command.Parameters.AddWithValue("@whereColumn", whereColumn);
                command.Parameters.AddWithValue("@pkid", pkid);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
           /* catch (NonQueryFailedException ex)
            {
                throw ex;
            }*/

            Close();
        }


        static public int GetLastInsertID()
        {
            int lastInsertID;

            try
            {
                string selectCommand = "select last_insert_rowid()";
                DataTable resultsSet = NonLockQuery(selectCommand);

                if (resultsSet.Rows.Count == 0 || resultsSet.Columns.Count == 0)
                    throw new QueryFailedException("Error retrieving the last insert ID.");

                lastInsertID = Convert.ToInt32(resultsSet.Rows[0][0]);
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return lastInsertID;
        }


        static private DataTable NonLockQuery(string query)
        {
            SQLiteCommand command = new SQLiteCommand();
            DataTable resultSet = new DataTable();

            try
            {
                command = connection.CreateCommand();
                adapter = new SQLiteDataAdapter(query, connection);

                resultSet = new DataTable();
                adapter.Fill(resultSet);
            }
            catch (SQLiteException)
            {
                throw new QueryFailedException("Could not execute query \"" + query + "\"");
            }

            return resultSet;
        }

        static internal void FillSQLParams(string query, Params paramValues, SQLiteCommand command)
        {
            if (paramValues == null)
                return;

            foreach (string param in paramValues.Keys)
            {
                string paramValue = paramValues[param];
                command.Parameters.AddWithValue(param, paramValue);
            }
        }

        /*
        static public DataTable Query(string query, Dictionary<string, string> paramValues)
        {
            Open();

            SQLiteCommand command = new SQLiteCommand();
            DataTable resultSet = new DataTable();

            try
            {
                command = connection.CreateCommand();
                command.CommandText = query;

                FillSQLParams(query, paramValues, command);

                adapter = new SQLiteDataAdapter(command);
                //adapter = new SQLiteDataAdapter(query, connection);

                resultSet = new DataTable();
                adapter.Fill(resultSet);
            }
            catch (SQLiteException)
            {
                throw new QueryFailedException("Could not execute query \"" + query + "\"");
            }

            Close();
            return resultSet;
        }
        */

        
        static public DataTable Query(string query)
        {
            Open();

            SQLiteCommand command = new SQLiteCommand();
            DataTable resultSet = new DataTable();

            try
            {
                command = connection.CreateCommand();
                adapter = new SQLiteDataAdapter(query, connection);

                resultSet = new DataTable();
                adapter.Fill(resultSet);
            }
            catch (SQLiteException)
            {
                throw new QueryFailedException("Could not execute query \"" + query + "\"");
            }

            Close();
            return resultSet;
        }
        

        
        //static public int Insert(string insertcommand, Dictionary<string, string> paramValues)
        static public int Insert(string insertcommand, Params paramValues)
        {
            int lastInsertID = -1;

            try
            {
                if (transaction == null)
                {
                    Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = insertcommand;

                    FillSQLParams(insertcommand, paramValues, command);

                    command.ExecuteNonQuery();

                    lastInsertID = GetLastInsertID();

                    Close();
                }
                else
                {
                    Open();
                    SQLiteCommand command = new SQLiteCommand(insertcommand, connection, transaction);

                    Close();
                }
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }

            return lastInsertID;
        }
        

        /*
        static public int Insert(string insertcommand)
        {
            int lastInsertID = -1;

            try
            {
                if (transaction == null)
                {
                    Open();
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = insertcommand;
                    command.ExecuteNonQuery();

                    lastInsertID = GetLastInsertID();

                    Close();
                }
                else
                {
                    Open();
                    SQLiteCommand command = new SQLiteCommand(insertcommand, connection, transaction);

                    Close();
                }
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }

            return lastInsertID;
        }
         * */
         

        /*
        static private string BuildInsertCommand(string tableName, List<string> columns, List<string> values)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("insert into " + tableName + " (");
            foreach (string columnName in columns)
            {
                sb.Append(columnName + ", ");
            }
            sb = sb.Remove(sb.Length - 2, 2);

            sb.Append(") values (");

            foreach (string columnName in columns)
            {
                sb.Append("@" + columnName + ", ");
            }
            sb = sb.Remove(sb.Length - 2, 2);

            sb.Append(")");

            return sb.ToString();
        }


        static public int Insert(string tableName, List<string> columns, List<string> values)
        {
            int lastInsertID = -1;

            try
            {
                Open();

                SQLiteCommand command = new SQLiteCommand();
                DataTable resultSet = new DataTable();

                string insertCommand = BuildInsertCommand(tableName, columns, values);
                command = connection.CreateCommand();
                command.CommandText = insertCommand;

                for (int i = 0; i < columns.Count; i++)
                {
                    command.Parameters.AddWithValue("@" + columns[i], values[i]);
                }

                command.ExecuteNonQuery();
                lastInsertID = GetLastInsertID();

                Close();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }

            return lastInsertID;
        }
        */

        static public string GetSingleValue(string tablename, string primaryKeyColumn, int primaryKey, string columnName)
        {
            Open();

            SQLiteCommand command = new SQLiteCommand();
            DataTable resultSet = new DataTable();
            string result;

            try
            {
                command = connection.CreateCommand();
                command.CommandText = "select " + columnName + " from " + tablename 
                                      + " where " + primaryKeyColumn + " = @primarykey";
                command.Parameters.AddWithValue("@primaryKey", primaryKey);

                adapter = new SQLiteDataAdapter(command);
                adapter.Fill(resultSet);

                if (resultSet.Rows.Count != 1)
                    throw new QueryFailedException("The specified query did not return a unique set of results.");

                result = resultSet.Rows[0][columnName].ToString();
            }
            catch (SQLiteException)
            {
                throw new QueryFailedException("The specified query did not return a unit set of results.");
            }

            Close();
            return result;
        }



        static public DataTable GetTable(string tableName)
        {
            string query = "select * from " + tableName;
            DataTable queryResults;

            try
            {
                queryResults = Query(query);
            }
            catch (SQLiteException)
            {
                throw new QueryFailedException("Unable to retrieve table: " + tableName);
            }
            
            return queryResults;
        }


        static public DataTable GetTableWhereNot(string tableName, string pkidColumn, int pkid)
        {
            string query = "select * from " + tableName + " where " + pkidColumn + " <> " + pkid;
            //Dictionary<string, string> paramValues = new Dictionary<string,string>();
            //paramValues.Add("@pkid", pkid.ToString());
            
            DataTable queryResults;

            try
            {
                queryResults = Query(query);
            }
            catch (SQLiteException)
            {
                throw new QueryFailedException("Unable to retrieve table: " + tableName);
            }

            return queryResults;
        }


        static public DataTable GetJoinedTables(List<string> tableNames, List<string> joinColumns, string joinType)
        {
            if (tableNames.Count != joinColumns.Count)
                throw new QueryFailedException("Table name count != join column count");

            string query = "select * from ";

            query += tableNames[0] + " " + joinType + " " + tableNames[1]
                + " on " + tableNames[0] + "." + joinColumns[0]
                + " = " + tableNames[1] + "." + joinColumns[1];

            for (int i = 2; i < tableNames.Count; i++)
            {
                query += " " + joinType + " " + tableNames[i]
                    + " on " + tableNames[i - 1] + "." + joinColumns[i - 1]
                    + " = " + tableNames[i] + "." + joinColumns[i];
            }

            DataTable resultSet;
            try
            {
                 resultSet = Query(query);
            }
            catch (QueryFailedException ex)
            {
                throw ex;
            }

            return resultSet;
        }


        static public void Update(string tableName, string columnName,
            string newValue, string whereColumn, string whereValue)
        {
            Open();

            string updateCommand = "update " + tableName + " set " + columnName + " = "
                + " @newValue where " + whereColumn + " = @whereValue";
            try
            {
                if (transaction == null)
                {
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = updateCommand;
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@whereValue", whereValue);

                    int numRowsAffected = command.ExecuteNonQuery();
                }
                else
                {
                    Open();
                    //SQLiteCommand command = new SQLiteCommand(updateCommand, connection, transaction);
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = updateCommand;
                    command.Parameters.AddWithValue("@newValue", newValue);
                    command.Parameters.AddWithValue("@whereValue", whereValue);
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();

                    Close();
                }
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }

            Close();
        }


        static private string BuildUpdateErrorString(string tableName, List<string> columnNames,
            List<string> newValues, List<string> whereColumns, List<string> whereValues)
        {
            string error = "Invalid update exceptiion: "
                + "Table: " + tableName + "\n"
                + "Update Columns: ";

            foreach (string columnName in columnNames)
                error += columnName + "  ";

            error += "\n";

            error += "Update Values: ";
            foreach (string newValue in newValues)
                error += newValue + "  ";

            error += "\n";

            error += "Where Columns: ";
            foreach (string whereColumn in whereColumns)
                error += whereColumn + "  ";

            error += "\n";

            error += "Where Values: ";
            foreach (string whereValue in whereValues)
                error += whereValue + "  ";

            return error;
        }


        static private bool UpdateParametersValid(List<string> columnNames, List<string> newValues,
                                                    List<string> whereColumns, List<string> whereValues)
        {
            int numColumnNames = columnNames.Count;
            int numNewValues = newValues.Count;
            int numWhereColumns = whereColumns.Count;
            int numWhereValues = whereValues.Count;

            bool valid = (numColumnNames == numNewValues) && (numWhereColumns == numWhereValues);

            return valid;
        }


        static public void Update(string tableName, List<string> columnNames,
            List<string> newValues, List<string> whereColumns, List<string> whereValues)
        {
            bool valid = UpdateParametersValid(columnNames, newValues, whereColumns, whereValues);

            if (!valid)
            {
                string error = BuildUpdateErrorString(tableName, columnNames,
                                                    newValues, whereColumns, whereValues);

                throw new DataStoreFailedException(error);
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("update " + tableName + " set ");

            for (int i = 0; i < columnNames.Count; i++)
            {
                sb.Append(columnNames[i] + " = @newValue" + i);
                
                if (i < columnNames.Count - 1)
                    sb.Append(", ");
            }

            sb.Append(" where ");

            for (int i = 0; i < whereColumns.Count; i++)
            {
                sb.Append(whereColumns[i] + " = @whereValue" + i);

                if (i < whereColumns.Count - 1)
                    sb.Append(" and ");
            }

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = sb.ToString();
            for (int i = 0; i < columnNames.Count; i++)
                command.Parameters.AddWithValue("@newValue" + i, newValues[i]);

            for (int i = 0; i < whereValues.Count; i++)
                command.Parameters.AddWithValue("@whereValue" + i, whereValues[i]);

            try
            {
                Open();

                if (transaction != null)
                    command.Transaction = transaction;

                command.ExecuteNonQuery();

                Close();
            }
            catch (SQLiteException ex)
            {
                throw ex;
            }
        }




        internal static void IssueBatchCommands(List<string> commands)
        {
            Open();

            foreach (string sqlcommand in commands)
            {
                try
                {
                    SQLiteCommand command = connection.CreateCommand();
                    command.CommandText = sqlcommand;
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    throw ex;
                }
            }

            Close();
        }

        internal enum Comparator
        {
            LESS_THAN,
            GREATER_THAN,
            EQUALS,
            LIKE
        }

        internal struct Condition
        {
            internal Condition(string columnName, Comparator comparator, string value)
            {
                this.columnName = columnName;
                this.comparator = comparator;
                this.value = value;
            }

            internal string columnName;
            internal Comparator comparator;
            internal string value;
        }

        static Dictionary<Comparator, string> symbols = new Dictionary<Comparator, string> { { Comparator.LESS_THAN, "<" }, { Comparator.GREATER_THAN, ">" }, { Comparator.EQUALS, "=" }, { Comparator.LIKE, "%" } };

        private static string BuildQuery(string tableName, List<string> columnsToRetrieve, List<Condition> conditions)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select ");

            // if there are specified columns, append the list,
            // else get *
            if (columnsToRetrieve != null && columnsToRetrieve.Count > 0)
            {
                sb.Append("(");
                for (int i = 0; i < columnsToRetrieve.Count; i++)
                {
                    sb.Append(columnsToRetrieve[i]);
                    //sb.Append("@columnName" + i);
                    if(i < columnsToRetrieve.Count - 1)
                        sb.Append(", ");
                }
                sb.Append(")");
            }
            else
            {
                sb.Append("*");
            }

            //sb.Append(" from @tableName ");
            sb.Append(" from " + tableName + " ");

            // if there are conditions, apply them
            if (conditions != null && conditions.Count > 0)
            {
                sb.Append("where ");
                for (int i = 0; i < conditions.Count; i++)
                {
                    //sb.Append("@conditionName" + i + " ");
                    sb.Append(conditions[i].columnName + " ");
                    sb.Append(symbols[conditions[i].comparator]);
                    sb.Append(" @conditionValue" + i);

                    if (i < conditions.Count - 1)
                        sb.Append(" and ");
                }
            }

            return sb.ToString();
        }


        internal static DataTable Query(string tableName, List<string> columnsToRetrieve, List<Condition> conditions)
        {
            Open();

            string query = BuildQuery(tableName, columnsToRetrieve, conditions);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = query;

            //command.Parameters.AddWithValue("@tableName", tableName);

            //for (int i = 0; i < columnsToRetrieve.Count; i++)
            //    command.Parameters.AddWithValue("@columnName" + i, columnsToRetrieve[i]);
            //for (int i = 0; i < conditions.Count; i++)
            //    command.Parameters.AddWithValue("@conditionName" + i, conditions[i].columnName);

            for (int i = 0; i < conditions.Count; i++)
                command.Parameters.AddWithValue("@conditionValue" + i, conditions[i].value);

            DataTable resultSet;

            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);

                resultSet = new DataTable();
                adapter.Fill(resultSet);
            }
            catch (SQLiteException)
            {
                throw new QueryFailedException("Could not execute query \"" + query + "\"");
            }

            Close();
            return resultSet;
        }


        private static bool TableExists(string tableName)
        {
            DataTable results = new DataTable();

            try
            {
                Open();
                results = Query("foo", null, null);
                //results = Query("SELECT * FROM sqlite_master WHERE name ='" + tableName + "' and type='table'");
                Close();
            }
            catch (Exception)
            {
                Close();
                return false;
            }

            return (results.Rows.Count > 0);
        }

        /*
        private static bool TableExists(string tableName)
        {
            DataTable results = new DataTable();

            try
            {
                Open();
                results = Query("SELECT * FROM sqlite_master WHERE name ='" + tableName + "' and type='table'");
                Close();
            }
            catch (Exception)
            {
                Close();
                return false;
            }

            return (results.Rows.Count > 0);
        }
        */

        private static void CleanupUpdates()
        {
            try
            {
                string updatesDirectory = Utilities.GetCQPUserDataDirectory() + @"\updates";
                FileUtils.CreateDirectoryIfNotExists(updatesDirectory);

                //FileInfo[] files = new DirectoryInfo(updatesDirectory).GetFiles("*.sql");
                FileInfo[] files = new DirectoryInfo(updatesDirectory).GetFiles("*.dat");

                foreach (FileInfo file in files)
                    file.Delete();
            }
            catch (Exception ex)
            {
                Logger.Error("DatabaseConnection.CleanupUpdates: " + ex);
            }
        }


        internal static void RunSQLUpdateScripts()
        {
            SetCheckPoint();

            try
            {
                string sqlUpdateDirectoryPath = Utilities.GetCQPUserDataDirectory() + @"\updates";
                FileUtils.CreateDirectoryIfNotExists(sqlUpdateDirectoryPath);
                
                DirectoryInfo sqlUpdateDirectory = new DirectoryInfo(sqlUpdateDirectoryPath);

                //FileInfo[] files = sqlUpdateDirectory.GetFiles("*.sql");
                FileInfo[] files = sqlUpdateDirectory.GetFiles("*.dat");
                files.OrderBy(file => Convert.ToInt32(FileUtils.FilenameWithoutExtension(file)));

                foreach (FileInfo file in files)
                {
                    RunSQLUpdateScript(file);
                }
            }
            catch (InvalidUpdateScriptException ex)
            {
                RestoreCheckpoint();
                CQPMessageBox.ShowDialog(ex.Message, "Update Error", CQPMessageBox.CQPMessageBoxButtons.OK, CQPMessageBox.CQPMessageBoxIcon.Error);
                CleanupUpdates();

                return;
            }
            catch (Exception ex)
            {
                RestoreCheckpoint();
                Logger.Error("DatabaseConnection: RunSQLUpdateScripts: Some error occurred outside of RunSQLUpdateScript! : " + ex.Message);
                CQPMessageBox.ShowDialog("An error occurred attempting to apply the updates - the update appears to be invalid and will not be applied.\n\n"
                                            + "The updates will be retried at a later time.");
                CleanupUpdates();

                return;
            }

            ClearCheckpoint();
        }

        
        private static void Update(string updateCommand)
        {
            try
            {
                Open();
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = updateCommand;
                command.ExecuteNonQuery();
                Close();
            }
            catch (SQLiteException ex)
            {
                Close();
                throw ex;
            }
        }

        
        private static void Delete(string deleteCommand)
        {
            try
            {
                Open();
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = deleteCommand;
                command.ExecuteNonQuery();
                Close();
            }
            catch (SQLiteException ex)
            {
                Close();
                throw ex;
            }
        }
        

        /*
        private static bool ValidateScriptFile(string validationLine, string filename)
        {
            string key = filename + "Mentor Security Signature SHA 512" + filename;
            byte[] shaKey;
            byte[] shaValidation;

            using (System.Security.Cryptography.SHA512 s = new System.Security.Cryptography.SHA512Managed() )
            {
                shaKey = s.ComputeHash(Encoding.Default.GetBytes(key.ToString()));
                //shaValidation = s.ComputeHash(Encoding.Default.GetBytes(validationLine));
                shaValidation = Encoding.Default.GetBytes(validationLine); //s.ComputeHash(Encoding.Default.GetBytes(validationLine));
            }

            byte[] onlyInFirst = shaKey.Where(b => !shaValidation.Contains(b)).ToArray();
            byte[] onlyInSecond = shaValidation.Where(b => !shaKey.Contains(b)).ToArray();

            if (onlyInFirst.Count() > 0 || onlyInSecond.Count() > 0)
                return false;

            return true;
        }
         * */


        private static string Decode(string text)
        {
            text = text.Replace('!', '=');

            // get the MD5 - reverse it from it's current (backwards) orientation
            char[] md5Array = text.Substring(0, 32).ToCharArray();
            Array.Reverse(md5Array);
            string md5str = new string(md5Array);

            // get the rest of the queries
            text = text.Substring(32);
            char[] base64Array = text.ToCharArray();
            base64Array = base64Array.Reverse().ToArray();
            text = new string(base64Array);

            //if (base64Array.Length != text.Length)
            //    Logger.Error("Error:  The base64 array was not the correct length when decoding the update.");

            MD5 md5 = MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] hash = md5.ComputeHash(inputBytes);
            //string strHash = Encoding.UTF8.GetString(hash);

            //if (inputBytes.Length != text.Length)
            //    Logger.Error("Error:  The inputBytes array was not the correct length when decoding the update.");

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            //return sb.ToString();
            string strHash = sb.ToString();

            if (md5str.ToLower() != strHash.ToLower())
            {
                //LogAndThrowUpdateScriptInvalidError(file);
                throw new Exception("An error occurred while decoding the update script.");
                //return;
            }

            byte[] encodedDataAsBytes 
                = System.Convert.FromBase64String(text);

            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);

            //if (encodedDataAsBytes.Length != text.Length)
            //    Logger.Error("Error:  The encodedDataAsBytes array was not the correct length when decoding the update.");
            
            return returnValue;
            
            /*
            int size = text.Length;
            StringBuilder returnvalue = new StringBuilder();
            int bytesRead = 0;
            int z = 0;

            while (bytesRead < text.Length)
            {
                if (z > 0)
                    Logger.Error("Found long one.");

                byte[] encodedDataAsBytes
                    = System.Convert.FromBase64String(text.Substring(bytesRead));
                returnvalue.Append(System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes));
                bytesRead += encodedDataAsBytes.Length;
                z++;
            }

            if (bytesRead != text.Length)
                Logger.Error("Error:  didn't read the correct number of bytes.");

            return returnvalue.ToString();
             * */
        }


        private static void LogAndThrowUpdateScriptInvalidError(FileInfo file)
        {
            string version = FileUtils.FilenameWithoutExtension(file);

            string errorLogMessage = "The update file: " + file.Name + " does not appear to be a valid update script, "
                                    + "and the update was aborted.";

            string errorDisplayMessage = "An error occurred when attempting to perform the update Couneslor to the latest version. \n\n"  
                                            +  "The update will be skipped and retried at a later time.";

            Logger.Error(errorLogMessage);

            throw new InvalidUpdateScriptException(errorDisplayMessage);
        }


        private static bool ValidateScriptFile(string validationLine, string filename)
        {
            /*byte[] bytes = Encoding.Default.GetBytes(validationLine);
            string value = Logger.DecryptStringFromBytes(bytes);

            if (value == filename + "Mentor Security Signature SHA 512" + filename)*/
                //return true;

            return false;
        }


        private static void RunSQLUpdateScript(FileInfo file)
        {
            //List<string> lines = File.ReadAllLines(file.FullName, ).ToList();
            //string validationLine = lines[0];

            // convert ! to =
            string text = File.ReadAllText(file.FullName);


            List<string> lines = File.ReadAllText(file.FullName).Split('?').ToList();
            //string validationLine = lines[0];

            //if (!ValidateScriptFile(validationLine, file.Name))
            //{
            //    LogAndThrowUpdateScriptInvalidError(file);
            //    return;
            //}

            //lines.RemoveAt(0);

            try
            {
                foreach (string line in lines)
                {
                    //string formattedLine = line.ToLower();
                    //string formattedLine = FormatSQLLine
                    string decoded = Decode(line);
                    //if (decoded.Contains("Army physical fitness training"))
                    //{
                        //decoded = decoded.Insert(0, " ");
                        //StreamWriter s = new StreamWriter("tmp.txt");
                        //s.WriteLine(decoded);
                        //s.Close();
                        //continue;
                    //}

                    RunSQLLine(decoded);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error running SQL in file: " + file.Name, ex);
                LogAndThrowUpdateScriptInvalidError(file);
            }

            file.Delete();
        }


        private static void RunSQLLine(string line)
        {
            if (line.StartsWith("/*"))
                return;
            else if (line.StartsWith("--"))
                return;
            else if (line.Length == 0)
                return;
            
            ThrowIfInvalid(line);

            if (line.StartsWith("update", true, System.Globalization.CultureInfo.CurrentCulture))
                Update(line);
            else if (line.StartsWith("insert", true, System.Globalization.CultureInfo.CurrentCulture))
                Insert(line, null);
            else
                throw new Exception("RunSQLLine: Not update or insert");
        }


        private static void ThrowIfInvalid(string sqlCommand)
        {
            string line = sqlCommand.ToLower();
            string errorMessage 
                = "The update script attempts to address a locked table "
                    + "or attempts to insert multiple commands on one line.";

            List<string> forbiddenTables
                = new List<string>
                    {"battalions", "notes", "notevalues", "soldiers",
                      "soldierstatuses", "unithierarchies",
                      "usergenerateddocs", "usergeneratedvalues",
                      "useroptions"};

            bool containsForbiddenKeyword = false;

            foreach (string table in forbiddenTables)
            {
                if (line.Contains("update " + table)
                    || line.Contains("delete from " + table)
                    || line.Contains("drop table " + table))
                {
                    string error = "Attempt to modify locked table: " + table + "\n"
                        + "Command: " + sqlCommand;
                    Logger.Error("Attempt to modify locked table: " + sqlCommand);
                }
            }

                //= line.Contains("update battalions")
                //|| line.Contains("delete from battalions")
                //|| line.Contains("update notes")
                //|| line.Contains("delete from notes")
                //|| line.Contains("update notevalues")
                //|| line.Contains("delete frmnotevalues")
                ////|| (line.Contains(" settings ") && line.Contains(" update "))
                //|| line.Contains("update soldiers")
                //|| line.Contains("delete from soldiers")
                //|| line.Contains("update soldierstatuses")
                //|| line.Contains("delete from soldierstatuses")
                //|| line.Contains("update unithierarchies")
                //|| line.Contains("delete from unithierarchies")
                //|| line.Contains("update usergeneratedvalues")
                //|| line.Contains("delete from usergeneratedvalues")
                //|| line.Contains("update usergenerateddocs")
                //|| line.Contains("delete from usergenerateddocs")
                //|| line.Contains("update useroptions")
                //|| line.Contains("delete from useroptions");
                //|| line.Contains("drop")
                //|| line.Contains("delete ");

            bool containsMultipleCommands = false;

            /*int firstSemiColon = line.IndexOf(';');
            if(firstSemiColon != -1)
            {
                if(line.Length - firstSemiColon > 1)
                    containsMultipleCommands = true;
            }*/

            if(containsForbiddenKeyword || containsMultipleCommands)
            {
                throw new InvalidUpdateScriptException(errorMessage);
            }
        }


        /*
        private static bool ValidateSQL(string line)
        {
            if (line.StartsWith("update"))
                return ValidateUpdate(line);
            else if (line.StartsWith("insert"))
                return ValidateInsert(line);
            else
                return false;
        }


        private static bool ValidateUpdate(string line)
        {
            string[] words = line.Split(' ');
            
            //   1      2        3    4 [ 5/6 ]
            // update tablename set column=value, column=value, ... column=value [where column=value]
            if (words.Length < 4)
                return false;

            string tableName = words[1];
            if (!TableExists(tableName))
                return false;



            return false;
        }

        private static bool ValidateInsert(string line)
        {
            return false;
        }
         * */


    }
}
