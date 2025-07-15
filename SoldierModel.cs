using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Windows.Forms;


namespace CounselQuickPlatinum
{
    internal class SoldierModel
    {
        private static DataTable soldierStatusTable;
        private static DataTable formattedSoldiersTable;

        internal delegate void SoldierModelRefreshedEvent();
        internal static event SoldierModelRefreshedEvent soldierModelRefreshed;

        private const int unassignedSoldierID = -1;
        public static int UnassignedSoldierID { get { return unassignedSoldierID; } }

        private static System.Threading.Mutex tableMutex;

        static int lockLevels = 0;

        static SoldierModel()
        {
            Logger.Trace("SoldierModel - constructor");

            tableMutex = new System.Threading.Mutex();

            RefreshSoldierStatusTable();
            RefreshFormattedSoldiersTable();

            //CleanupSoldierDirectories();
        }

        private static void Lock()
        {
            lockLevels += 1;
            Logger.Frame();
            Logger.Trace("SoldierModel - Lock: lockLevels = " + lockLevels);

            tableMutex.WaitOne();
        }

        private static void Unlock()
        {
            tableMutex.ReleaseMutex();

            lockLevels -= 1;
            Logger.Trace("SoldierModel - UNlock: lockLevels = " + lockLevels);
            Logger.Frame();
        }

        
        internal static DataTable FormattedSoldiersTableCopy
        {
            get
            {
                Lock();
                
                Logger.Trace("SoldierModel - FormattedSoldierTable - get");

                if (formattedSoldiersTable == null)
                    RefreshFormattedSoldiersTable();

                Unlock();
                return formattedSoldiersTable.Copy();
            }
        }
        

        internal static DataTable GetSoldierDatabaseValues(Guid guid)
        {
            DataTable soldierValues;

            try
            {
                string query = "select * from soldiers where soldierguid = \"" + guid.ToString() + "\"";
                Logger.Trace("SoldierModel - GetSoldierDatabaseValues by GUID - query " + query);

                soldierValues = DatabaseConnection.Query(query);
            }
            catch (QueryFailedException ex)
            {
                string message = "Could not retrieve Soldier from the database.";

                Logger.Error("*** SoldierModel - GetSoldierDatabaseValues - QueryFailedException caught - " + message);
                Logger.Error(ex);

                throw new DataLoadFailedException(message, ex);
            }

            if (soldierValues.Rows.Count == 0)
            {
                string message = "Could not retrieve Soldier from the database.";

                Logger.Error("*** SoldierModel - GetSoldierDatabaseValues - 0 rows " + message);

                throw new DataLoadFailedException(message);
            }

            return soldierValues;
        }


        internal static DataTable GetSoldierDatabaseValues(int soldierID)
        {
            DataTable soldierValues;

            try
            {
                string query = "select * from soldiers where soldierid = " + soldierID;

                Logger.Trace("SoldierModel - GetSoldierDatabaseValues by ID - query " + query);

                soldierValues = DatabaseConnection.Query(query);
            }
            catch (QueryFailedException ex)
            {
                string message = "Could not retrieve soldier from the database.";
                
                Logger.Error("*** SoldierModel - GetSoldierDatabaseValues - QueryFailedException caught - " + message);

                throw new DataLoadFailedException(message, ex);
            }

            if (soldierValues.Rows.Count == 0)
            {
                string message = "Could not retrieve Soldier from the database.";

                Logger.Error("*** SoldierModel - GetSoldierDatabaseValues - 0 rows " + message);

                throw new DataLoadFailedException(message);
            }

            return soldierValues;
        }


        internal static DataTable GetAllStatusEnums()
        {
            Logger.Trace("SoldierModel - GetAllStatusEnums");

            try
            {
                DataTable statusEnums = DatabaseConnection.GetTable("soldierstatusenums");
                return statusEnums;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException("Could not retrieve Soldier statuses.", ex);
            }
        }


        internal static void DeleteSoldier(int selectedSoldierID)
        {
            Logger.Trace("SoldierModel - DeleteSoldier - " + selectedSoldierID);

            Lock();

            DatabaseConnection.Delete("soldiers", "soldierid", selectedSoldierID.ToString());

            DocumentModel.DeleteAllDocumentsForSoldier(selectedSoldierID);
            DeleteAllNotesForSoldier(selectedSoldierID);
            DeleteAllFilesForSoldier(selectedSoldierID);

            Unlock();

            Refresh();
            DatabaseConnection.Backup();
        }


        internal static void SendSoldierToRecycleBin(int soldierID)
        {
            Logger.Trace("SoldierModel - sendsoldierToRecyclebin " + soldierID);

            try
            {
                DatabaseConnection.Update("soldiers", "deleted", "1", "soldierid", soldierID.ToString());
            }
            catch (NonQueryFailedException ex)
            {
                string error = "An unexpected error occurred sending the "
                                + "Soldier to the recycling bin.";

                Logger.Error("**** SoldierModel - SendSoldierToRecycleBin - NonQueryFailedExceptionCaught: " + error + ", " + ex.Message);
                Logger.Error(ex);

                throw new DataStoreFailedException(error, ex);
            }

            Refresh();
            DatabaseConnection.Backup();
        }

        internal static void RemoveSoldierFromRecyclingBin(int soldierID)
        {
            Logger.Trace("SoldierModel - RemoveSoldierFromRecyclingBin");

            DatabaseConnection.Update("soldiers", "deleted", "0", "soldierid", soldierID.ToString());
            
            Refresh();
            DatabaseConnection.Backup();
        }


        //internal static void DeleteAllDocumentsForSoldier(int soldierID)
        //{
        //    DataRow []documents = DocumentModel.UserGeneratedDocumentsTable.Select("soldierid = " + soldierID);

        //    foreach (DataRow document in documents)
        //    {
        //        int documentID = Convert.ToInt32(document["generateddocid"]);
        //        DocumentModel.DeleteDocumentPermanently(documentID);
        //    }

        //    //Refresh();
        //}


        private static void DeleteAllNotesForSoldier(int soldierID)
        {
            Logger.Trace("SoldierModel - DeleteAllNotesForSoldier " + soldierID);
            DataRow []rows = NotesModel.GetNotesTable().Select("soldierid = " + soldierID);

            if (rows.Count() == 0)
                return;

            DataRow[] notes = rows.CopyToDataTable().Rows.Cast<DataRow>().ToArray();

            foreach(DataRow row in notes)
            {
                int noteID = Convert.ToInt32(row["noteid"]);

                Logger.Trace("    SoldierModel - DeleteAllNotesForSoldier: DeleteNote: " + noteID);
                NotesModel.DeleteNote(noteID);
            }
        }


        private static void DeleteAllFilesForSoldier(int soldierID)
        {
            Logger.Trace("SoldierModel - DeleteAllFilesForSoldier " + soldierID);

            string directoryPath = GetSoldierBaseDataDirectory(soldierID);

            if(Directory.Exists(directoryPath))
                Directory.Delete(directoryPath, true);
        }


        public static DataTable GetSoldierStatuses(int soldierID)
        {
            Logger.Trace("SoldierModel - GetSoldierStatuses - " + soldierID);

            DataTable soldierStatusValues;

            try
            {
                soldierStatusValues = DatabaseConnection.Query("select * from soldierstatuses "
                    + " inner join soldierstatusenums "
                    + " on soldierstatuses.statusenumid = soldierstatusenums.statusenumid "
                    + " where soldierid = " + soldierID);
            }
            catch (QueryFailedException ex)
            {
                Logger.Trace("    SoldierModel - GetSoldierStatuses - QueryFailedException : " + ex.Message, ex);

                throw new DataLoadFailedException("Could not retrieve status information for selected Soldier");
            }
            catch (DataLoadFailedException ex)
            {
                Logger.Trace("    SoldierModel - GetSoldierStatuses - DataLoadFailedException : " + ex.Message, ex);

                throw ex;
            }

            return soldierStatusValues;
        }


        private static void RefreshSoldierStatusTable()
        {
            Logger.Trace("SoldierModel - RefreshSoldierStatusTable");
            
            //Lock();

            try
            {
                soldierStatusTable = DatabaseConnection.GetTable("soldierstatuses");
                //Unlock();
            }
            catch (QueryFailedException ex)
            {
                Logger.Error("    SoldierModel - RefreshSoldierStatusTable - QueryFailedException - " + ex.Message);
                Logger.Error(ex);

                throw new DataLoadFailedException("Could not retrieve the Soldier status information.", ex);
            }
            catch (DataLoadFailedException ex)
            {
                Logger.Error("    SoldierModel - RefreshSoldierStatusTable - DataLoadFailedExcpetion - " + ex.Message);
                Logger.Error(ex);

                throw ex;
            }
        }

        internal static void LoadSoldierValues(Soldier soldier, Guid guid)
        {
            Lock();
            IEnumerable<DataRow> rows = formattedSoldiersTable.Rows.Cast<DataRow>();
            IEnumerable<DataRow> matches = rows.Where(soldierRow => new Guid(soldierRow["soldierguid"].ToString()) == guid);

            if (matches.Count() > 1)
            {
                Unlock();
                throw new CQPLoadSoldierFailedException("Attemp to load soldier by ID: " + guid.ToString() + " failed!  More than one match.");
            }
            else if (matches.Count() < 1)
            {
                Unlock();
                throw new CQPLoadSoldierFailedException("Attempt to load soldier by ID: " + guid.ToString() + " failed!  No matches!");
            }

            DataRow soldierData = matches.First();
            int soldierID = Convert.ToInt32(soldierData["soldierID"]);

            Unlock();
            LoadSoldierValues(soldier, soldierID);
        }


        internal static Image GetSoldierImage(Soldier soldier)
        {
            string fullImagePath = GetSoldierImagePath(soldier.SoldierID);          

            if (File.Exists(fullImagePath))
            {
                try{
                    //soldier.hasCustomImage = true;
                    //return Image.FromFile(fullImagePath);

                    Image img;
                    using (var bmpTemp = new Bitmap(fullImagePath))
                    {
                        img = new Bitmap(bmpTemp);
                        bmpTemp.Dispose();
                        //bmpTemp = null;
                    }
                    return img;
                }
                catch(Exception)
                {
                    // if there was one specified but the picture is missing
                    return Properties.Resources.error;
                }
            }
            else
            {
                if (soldier.Rank != Ranking.None)
                {
                    int rankID = Convert.ToInt32(soldier.Rank) - 1;
                    return RankingModel.GetRankingImages()[rankID];
                }
                else
                {
                    return null;
                }
            }
        }


        internal static void LoadSoldierValues(Soldier soldier, int soldierID)
        {
            Lock();
            IEnumerable<DataRow> rows = formattedSoldiersTable.Rows.Cast<DataRow>();
            IEnumerable<DataRow> matches = rows.Where(soldierRow => Convert.ToInt32(soldierRow["soldierid"]) == soldierID);

            if (matches.Count() > 1)
            {
                Unlock();
                throw new CQPLoadSoldierFailedException("Attemp to load soldier by ID: " + soldierID + " failed!  More than one match.");
            }
            else if (matches.Count() < 1)
            {
                Unlock();
                throw new CQPLoadSoldierFailedException("Attempt to load soldier by ID: " + soldierID + " failed!  No matches!");
            }

            DataRow soldierData = matches.First();

            InitializeStaticEnumList(soldier);
            soldier.SoldierID = soldierID;
            //soldier.DateOfBirth = Convert.ToDateTime(soldierData["dateofbirth"]);
            
            string dateOfRankStr =  soldierData["dateofrank"].ToString();
            if (dateOfRankStr != "")
            {
                long dateOfRankTicks = Convert.ToInt64(dateOfRankStr);
                soldier.DateOfRank = new DateTime(dateOfRankTicks);
            }

            string dateOfBirthStr = soldierData["dateofbirth"].ToString();
            if (dateOfBirthStr != "")
            {
                long dateOfBirthTicks = Convert.ToInt64(dateOfBirthStr);
                soldier.DateOfBirth = new DateTime(dateOfBirthTicks);
            }
            
            //soldier.DateOfRank = Convert.ToDateTime(soldierData["dateofrank"]);
            soldier.FirstName = soldierData["firstname"].ToString();
            soldier.LastName = soldierData["lastname"].ToString();
            soldier.MiddleInitial = Convert.ToChar(soldierData["middleinitial"]);
            soldier.OtherStatus = soldierData["otherstatustext"].ToString();
            //soldier.PictureFilename = soldierData["imagefilepath"].ToString();
            
            soldier.Rank = (Ranking)(Convert.ToInt32(soldierData["rankingid"]));
            soldier.soldierGUID = new Guid(soldierData["soldierguid"].ToString());
            DataTable soldierStatuses = SoldierModel.GetSoldierStatuses(soldierID);

            foreach (DataRow soldierStatusRow in soldierStatuses.Rows)
            {
                int statusid = Convert.ToInt32(soldierStatusRow["statusenumid"]);

                //for (int i = 0; i < Statuses.Count; i++)
                foreach (int statusID in soldier.Statuses.Keys)
                {
                    if (soldier.Statuses[statusID].statusEnumID == statusid)
                        soldier.Statuses[statusID].applies = true;
                }
            }

            //soldier.UnitHierarchy = Convert.ToInt32(soldierData["unithierarchyid"]);
            string unitHierarchyIDString = soldierData["unithierarchyid"].ToString();
            int unitHierarchyID = Convert.ToInt32(unitHierarchyIDString);

            soldier.UnitHierarchy = UnitHierarchyModel.GetUnitHierarchyByID(unitHierarchyID);

            string soldierImagePath = GetSoldierImagePath(soldier.SoldierID);
            if (File.Exists(soldierImagePath))
            {
                soldier.hasCustomImage = true;
            }

            Unlock();
            //return soldier;
        }


        private static void InitializeStaticEnumList(Soldier soldier)
        {
            DataTable statusEnums = SoldierModel.GetAllStatusEnums();

            foreach (DataRow statusEnumRow in statusEnums.Rows)
            {
                int statusID = Convert.ToInt32(statusEnumRow["statusenumid"]);
                string statusText = statusEnumRow["statusenumtext"].ToString();

                //Statuses.Add(new SoldierStatus(statusID, statusText, false));
                soldier.Statuses[statusID] = new SoldierStatus(statusID, statusText, false);
            }
        }


        private static void RefreshFormattedSoldiersTable()
        {
            Logger.Trace("SoldierModel - RefreshFormattedSoldiersTable");

            //Lock();

            try
            {
                formattedSoldiersTable
                    = DatabaseConnection.Query("select * from soldiers "
                                                + " inner join "
                                                + " rankings "
                                                + " on soldiers.rankingid = rankings.rankingid "
                                                + " inner join "
                    //+ " squadsections "
                    //+ " on soldiers.squadsectionid = squadsections.squadsectionid "
                    //+ " inner join "
                    //+ " platoons "
                    //+ " on squadsections.platoonid = platoons.platoonid "
                    //+ " inner join "
                    //+ " units "
                    //+ " on platoons.unitid = units.unitid");
                                                + " unithierarchies "
                                                + " on soldiers.unithierarchyid = unithierarchies.unithierarchyid "
                                                + " inner join "
                                                + " units "
                                                + " on unithierarchies.unitid = units.unitid "
                                                + " inner join "
                                                + " unitdesignators "
                                                + " on unithierarchies.unitdesignatorid = unitdesignators.unitdesignatorid "
                                                + " inner join "
                                                + " platoons "
                                                + " on unithierarchies.platoonid = platoons.platoonid "
                                                + " inner join "
                                                + " squadsections "
                                                + " on unithierarchies.squadsectionid = squadsections.squadsectionid ");
                formattedSoldiersTable.TableName = "soldierstable";

                foreach (DataRow row in formattedSoldiersTable.Rows)
                {
                    byte[] IVbytes = Encryption.GetBytes(row["soldierIV"].ToString());

                    if (IVbytes.Count() == 0)
                        continue;

                    byte[] IV = Convert.FromBase64String(Encryption.GetString(IVbytes));

                    byte[] lastNameBytes = Convert.FromBase64String(row["lastname"].ToString());
                    //string lastNameEncrypted = Encryption.GetString(lastNameBytes);
                    //string lastname = Encryption.DecryptString(lastNameEncrypted, IV);
                    string lastname = Encryption.DecryptString(lastNameBytes, IV);

                    byte[] firstNameBytes = Convert.FromBase64String(row["firstname"].ToString());
                    //string firstnameEncrypted = Encryption.GetString(firstNameBytes);
                    //string firstname = Encryption.DecryptString(firstnameEncrypted, IV);
                    string firstname = Encryption.DecryptString(firstNameBytes, IV);

                    byte[] middleBytes = Convert.FromBase64String(row["middleinitial"].ToString());
                    //string middleInitialEncrypted = Encryption.GetString(middleBytes);
                    //string middle = Encryption.DecryptString(middleInitialEncrypted, IV);
                    string middle = Encryption.DecryptString(middleBytes, IV);
                    
                    //string middleEncrypted = row["middleinitial"].ToString();
                    //string middle = Encryption.DecryptString(row["middleinitial"].ToString(), IV);


                    byte[] dateofrankBytes = Convert.FromBase64String(row["dateofrank"].ToString());
                    //string dateofRankEncrypted = Encryption.GetString(dateofrankBytes);
                    //string DOR = Encryption.DecryptString(dateofRankEncrypted, IV);
                    string DOR = Encryption.DecryptString(dateofrankBytes, IV);

                    byte[] dateofbirthBytes = Convert.FromBase64String(row["dateofbirth"].ToString());
                    //string dateofbirth = Encryption.GetString(dateofbirthBytes);
                    //string DOB = Encryption.DecryptString(dateofbirth, IV);
                    string DOB = Encryption.DecryptString(dateofbirthBytes, IV);

                    byte[] otherstatusBytes = Convert.FromBase64String(row["otherstatustext"].ToString());
                    //string otherstatusEncrypted = Encryption.GetString(otherstatusBytes);
                    //string otherStatus = Encryption.DecryptString(otherstatusEncrypted, IV);
                    string otherStatus = Encryption.DecryptString(otherstatusBytes, IV);

                    row["lastname"] = lastname;
                    row["firstname"] = firstname;
                    row["middleinitial"] = middle;
                    row["dateofrank"] = DOR;
                    row["dateofbirth"] = DOB;
                    row["otherstatustext"] = otherStatus;

                }

                //Unlock();
            }
            catch (QueryFailedException ex)
            {
                Logger.Error("    SoldierModel - RefreshFormattedSoldeirsTable - QueryFailedException - " + ex.Message);
                Logger.Error(ex);
                
                //Unlock();

                throw new DataLoadFailedException("Could not load formatted Soldier table", ex);
            }
        }


        private static string GetSoldierBaseDataDirectory(int soldierID)
        {
            string directory = Utilities.GetCQPUserDataDirectory() + @"\data\soldiers\" + soldierID.ToString();

            Logger.Trace("SoldierModel - GetSoldierBaseDataDirectory: " + directory);

            return directory;
        }


        internal static string GetSoldierDocDirectory(int soldierID)
        {
            string directoryPath = GetSoldierBaseDataDirectory(soldierID) + "\\documents";

            Logger.Trace("SoldierModel - GetSoldierDocDirectory: " + directoryPath);

            if (!Directory.Exists(directoryPath))
            {
                Logger.Trace("    SoldierModel - GetSoldierDocDirectory: Creating directory");
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }


        internal static string GetSoldierImagesDirectory(int soldierID)
        {
            string directoryPath = GetSoldierBaseDataDirectory(soldierID) + "\\images";

            Logger.Trace("SoldierModel - GetSoldierImagesDirectory " + directoryPath);

            if (!Directory.Exists(directoryPath))
            {
                Logger.Trace("    SoldierModel - GetSoldierImagesDirectory : Creating directory");
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }



        internal static int SaveSoldier(Soldier soldier)
        {
            Logger.Trace("SoldierModel - SaveSoldier: " + soldier.SoldierID);

            Lock();

            try
            {
                //if (formattedSoldiersTable.Select("soldierid <> -1 and soldierid = " + soldier.SoldierID).Count() == 0)
                if(soldier.SoldierID == -1)
                {
                    Logger.Trace("    SoldierModel - SaveSoldier: No matching rows: Saving New Soldier: " + soldier.SoldierID);
                    Logger.Frame();
                    GenerateNewSoldierRecord(soldier);
                    // this part is a bit redundant, but gets the image saved
                    // properly the first time.
                    //SaveSoldierValues(soldier);
                }
                //else
                {
                    Logger.Trace("    SoldierModel - SaveSoldier: Saving existing Soldier: " + soldier.SoldierID);
                    Logger.Frame();
                    SaveSoldierValues(soldier);
                    SaveSoldierStatuses(soldier);

                    //if (soldier.PictureNeedsSaved)
                    TryMakeLocalCopyOfImage(soldier);

                }

                Unlock();
            }
            catch (DataStoreFailedException ex)
            {
                Unlock();
                Logger.Error("    SoldierModel - SaveSoldier: Caught DatastorefailedException" + soldier.SoldierID + ", " + ex.Message);
                Logger.Error(ex);

                throw new DataStoreFailedException("An error occurred attempting to save the Soldier.", ex);
            }


            Logger.Trace("    SoldierModel - calling refresh...");
            Refresh();
            DatabaseConnection.Backup();

            return soldier.SoldierID;
        }


        internal static void SaveSoldierValues(Soldier soldier)
        {
            Logger.Trace("SoldierModel - SaveSoldierValues: " + soldier.SoldierID);
            Logger.Frame();
            int pkid = soldier.SoldierID;
            string tableName = "soldiers";
            string key = "soldierID";

            int oldUnitHierarchyID = soldier.UnitHierarchy.unitHierarchyID;

            UnitHierarchyModel.UpdateUnitHierarchyID(soldier.UnitHierarchy);

            byte[] IV = Encryption.GenerateIV();
            //string IVstring = Encryption.GetString(IV);

            string IVstring = Convert.ToBase64String(IV);
            /*string lastnameEncrypted = Convert.ToBase64String( Encryption.GetBytes(Encryption.EncryptString(soldier.LastName, IV) ) );
            string firstnameEncrypted = Convert.ToBase64String( Encryption.GetBytes(Encryption.EncryptString(soldier.FirstName, IV) ) );
            string middleInitialEncrypted = Convert.ToBase64String( Encryption.GetBytes( Encryption.EncryptString(soldier.MiddleInitial.ToString(), IV) ) );
            string DORencrypted = Convert.ToBase64String( Encryption.GetBytes( Encryption.EncryptString(soldier.DateOfRank.Ticks.ToString(), IV) ) );
            string DOBencrypted = Convert.ToBase64String( Encryption.GetBytes( Encryption.EncryptString(soldier.DateOfBirth.Ticks.ToString(), IV) ) ) ;
            string otherStatusEncrypted = Convert.ToBase64String( Encryption.GetBytes( Encryption.EncryptString(soldier.OtherStatus, IV) ) );*/

            string lastnameEncrypted = Convert.ToBase64String(Encryption.EncryptString(soldier.LastName, IV));
            string firstnameEncrypted = Convert.ToBase64String(Encryption.EncryptString(soldier.FirstName, IV));
            string middleInitialEncrypted = Convert.ToBase64String(Encryption.EncryptString(soldier.MiddleInitial.ToString(), IV));
            string DORencrypted = Convert.ToBase64String(Encryption.EncryptString(soldier.DateOfRank.Ticks.ToString(), IV));
            string DOBencrypted = Convert.ToBase64String(Encryption.EncryptString(soldier.DateOfBirth.Ticks.ToString(), IV));
            string otherStatusEncrypted = Convert.ToBase64String(Encryption.EncryptString(soldier.OtherStatus, IV));

            Model.SaveColumn(tableName, key, pkid, "soldierIV", IVstring);
            Model.SaveColumn(tableName, key, pkid, "rankingid", ((int)soldier.Rank).ToString());
            Model.SaveColumn(tableName, key, pkid, "lastname", lastnameEncrypted);
            Model.SaveColumn(tableName, key, pkid, "firstname", firstnameEncrypted);
            Model.SaveColumn(tableName, key, pkid, "middleinitial", middleInitialEncrypted);
            Model.SaveColumn(tableName, key, pkid, "dateofrank", DORencrypted);
            Model.SaveColumn(tableName, key, pkid, "dateofbirth", DOBencrypted);
            Model.SaveColumn(tableName, key, pkid, "otherstatustext", otherStatusEncrypted);
            //Model.SaveColumn(tableName, key, pkid, "imagefilepath", soldier.PictureFilename);
            Model.SaveColumn(tableName, key, pkid, "unithierarchyid", soldier.UnitHierarchy.unitHierarchyID.ToString());

            if (oldUnitHierarchyID != soldier.UnitHierarchy.unitHierarchyID)
            {
                UnitHierarchyModel.RemoveUnitHierarchyIfUnreferenced(oldUnitHierarchyID);
            }
        }


        internal static void SaveSoldierStatuses(Soldier soldier)
        {
            Logger.Trace("SoldierModel - SaveSoldierStatuses " + soldier.SoldierID);

            int soldierID = soldier.SoldierID;
            string table = "soldierstatuses";
            string soldierIDColumn = "soldierid";
            string statusEnumColumn = "statusenumid";

            DatabaseConnection.Delete(table, soldierIDColumn, soldierID.ToString());

            foreach (SoldierStatus status in soldier.Statuses.Values)
            {
                if (status.applies == false)
                    continue;

                Model.InsertTuple(table, soldierIDColumn, soldierID.ToString(), statusEnumColumn, status.statusEnumID.ToString());
            }
        }


        internal static string GetSoldierImagePath(int soldierID)
        {
            string imagesPath = SoldierModel.GetSoldierImagesDirectory(soldierID);
            string filename = "soldierimage";
            string fullpath = imagesPath + "/" + filename;

            return fullpath;
        }


        internal static void TryMakeLocalCopyOfImage(Soldier soldier)
        {
            // There should only ever be one soldier image
            // Is there one?

            //if (soldier.PictureNeedsSaved == false)
            //    return;

            Logger.Trace("SoldierModel - TryMakeLocalCopyOfImage: " + soldier.SoldierID);
            string customImagePath = GetSoldierImagePath(soldier.SoldierID);
            string base64IV = DatabaseConnection.GetSingleValue("soldiers", "soldierid", soldier.SoldierID, "soldierIV");
            byte[] ivBytes = Convert.FromBase64String(base64IV);

            if (soldier.hasCustomImage == false)
                File.Delete(customImagePath);

            if (soldier.NewPictureFilename != "")
            {
                File.Delete(customImagePath);
                File.Copy(soldier.NewPictureFilename, customImagePath);
                //Encryption.EncryptFile(soldier.NewPictureFilename, customImagePath, ivBytes);
            }

            /*
            // is the image a default ranking image?
            if (RankingModel.GetRankingImages().Contains(soldier.Picture))
            {
                // delete the old custom image if it exists...
                File.Delete(fullpath);
            }
            else
            {
                try
                {
                    using (Bitmap image = new Bitmap(soldier.Picture))
                    {
                        //soldier.Picture.Save(fullpath);
                        soldier.Picture.Dispose();
                        image.Save(fullpath);
                        soldier.Picture = image;
                    }
                }
                catch (Exception ex)
                {

                }
            }*/
        }


        internal static int GenerateNewSoldierRecord(Soldier soldier)
        {
            Logger.Trace("SoldierModel - SaveNewSoldier:  " + soldier.SoldierID);

            int oldUnitHierarchyID = soldier.UnitHierarchy.unitHierarchyID;
            UnitHierarchyModel.UpdateUnitHierarchyID(soldier.UnitHierarchy);

            int soldierID = soldier.SoldierID;

            StringBuilder insertCommand = new StringBuilder();
            insertCommand.Append("insert into soldiers (" + (soldierID != -1 ? "soldierid, " : ""));
            insertCommand.Append("  rankingid, lastname, firstname, middleinitial, dateofrank, dateofbirth, ");
            insertCommand.Append("  otherstatustext, imagefilepath, unithierarchyid, soldierguid");
            insertCommand.Append(") values (");
            insertCommand.Append(soldierID != -1 ? soldierID + ", " : "");
            insertCommand.Append(((int)soldier.Rank).ToString() + ", ");
            insertCommand.Append("\"\" , ");
            insertCommand.Append("\"\" , ");
            insertCommand.Append("\"\", ");
            insertCommand.Append("\"\", ");
            insertCommand.Append("\"\", ");
            insertCommand.Append("\"\", ");
            insertCommand.Append("\"\", ");
            insertCommand.Append(soldier.UnitHierarchy.unitHierarchyID.ToString() + ", ");
            insertCommand.Append("\"" + soldier.soldierGUID.ToString() + "\"");
            insertCommand.Append(")");


            /*byte[] IV = Encryption.GenerateIV();
            string lastnameEncrypted = Encryption.GetIVAndEncryptedString(soldier.LastName, IV);
            string firstnameEncrypted = Encryption.GetIVAndEncryptedString(soldier.FirstName, IV);
            string middleInitialEncrypted = Encryption.GetIVAndEncryptedString(soldier.MiddleInitial.ToString(), IV);
            string DORencrypted = Encryption.GetIVAndEncryptedString(soldier.DateOfRank.Ticks.ToString(), IV);
            string DOBencrypted = Encryption.GetIVAndEncryptedString(soldier.DateOfBirth.Ticks.ToString(), IV);
            string otherStatusEncrypted = Encryption.GetIVAndEncryptedString(soldier.OtherStatus, IV);

            StringBuilder insertCommand = new StringBuilder();
            insertCommand.Append("insert into soldiers (" + (soldierID != -1 ? "soldierid, " : ""));
            insertCommand.Append("  rankingid, lastname, firstname, middleinitial, dateofrank, dateofbirth, ");
            insertCommand.Append("  otherstatustext, imagefilepath, unithierarchyid, soldierguid");
            insertCommand.Append(") values (");
            insertCommand.Append(soldierID != -1 ? soldierID + ", " : "");
            insertCommand.Append(((int)soldier.Rank).ToString() + ", ");
            insertCommand.Append("\"" + lastnameEncrypted + "\" , ");
            insertCommand.Append("\"" + firstnameEncrypted + "\" , ");
            insertCommand.Append("\"" + middleInitialEncrypted + "\", ");
            insertCommand.Append("\"" + DORencrypted + "\", ");
            insertCommand.Append("\"" + DOBencrypted + "\", ");
            insertCommand.Append("\"" + otherStatusEncrypted + "\", ");
            insertCommand.Append("\"" + soldier.PictureFilename + "\", ");
            insertCommand.Append(soldier.UnitHierarchy.unitHierarchyID.ToString() + ", ");
            insertCommand.Append("\"" + soldier.soldierGUID.ToString() + "\"");
            insertCommand.Append(")");*/

            //insertCommand.Append("\"" + soldier.LastName + "\" , ");
            //insertCommand.Append("\"" + soldier.FirstName + "\" , ");
            //insertCommand.Append("\"" + soldier.MiddleInitial + "\",");
            //insertCommand.Append(soldier.DateOfRank.Ticks.ToString() + ", ");
            //insertCommand.Append(soldier.DateOfBirth.Ticks.ToString() + ", ");
            //insertCommand.Append("\"" + soldier.OtherStatus + "\", ");

            try
            {
                soldierID = DatabaseConnection.Insert(insertCommand.ToString(), null);
                
                soldier.SoldierID = soldierID;
            }
            catch (NonQueryFailedException ex)
            {
                Logger.Error("**** SoldierModel - SaveNewSoldier - Caught NonQueryFailedException: " + ex.Message);
                Logger.Error(ex);

                throw new DataStoreFailedException("An error occured attempting to save the Soldier information.", ex);
            }

            SaveSoldierStatuses(soldier);

            //Refresh();
            DatabaseConnection.Backup();

            return soldier.SoldierID;
        }


        internal static void Refresh()
        {
            Logger.Trace("SoldierModel - Refresh");

            Lock();

            RefreshFormattedSoldiersTable();
            RefreshSoldierStatusTable();
            Unlock();

            if (DatabaseConnection.PerformingBatchUpdate || DataImporter.performingImport)
            {
                return;
            }

            if (soldierModelRefreshed != null)
            {
                Logger.Trace("    SoldierModel - Calling SoldierModelRefreshed Callback");
                soldierModelRefreshed();
            }

            //Unlock();
        }


        public static DataTable SoldiersStatusTable
        {
            get
            {
                Logger.Trace("SoldierModel - Get SoldierStatusTable");
                return soldierStatusTable;
            }
        }


        internal static SoldierModelRefreshedEvent SoldierModelRefreshed
        {
            set
            {
                soldierModelRefreshed += value;
            }
        }



        internal static bool SoldierExists(Guid guid)
        {
            string guidText = guid.ToString();

            string query = "soldierguid = '" + guidText + "'";
            int count = formattedSoldiersTable.Select(query).Count();

            return count > 0;
        }

        internal static int GetSoldierTableHashcode()
        {
            Lock();
            int hashCode = formattedSoldiersTable.GetHashCode();
            Unlock();
            return hashCode;
        }

        internal static DataRow[] GetSoldiersByUnitHierarchy(int unitHierarchyID)
        {
            Lock();
            DataRow[] rows = (DataRow[]) formattedSoldiersTable.Select("unithierarchyid = " + unitHierarchyID).Clone();
            Unlock();
            return rows;
        }

        internal static DataRow[] GetSoldierRows(string searchString)
        {
            Lock();
            DataRow[] rows = (DataRow[]) formattedSoldiersTable.Select(searchString).Clone();
            Unlock();
            return rows;
        }


    }
}
