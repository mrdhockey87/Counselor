using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    class Soldier
    {
        internal int SoldierID { get; set; }
        internal Ranking Rank { get; set; }
        internal DateTime DateOfRank { get; set; }
        internal string LastName { get; set; }
        internal string FirstName { get; set; }
        internal char MiddleInitial { get; set; }
        internal DateTime DateOfBirth { get; set; }
        private SortedDictionary<int, SoldierStatus> statuses;
        internal SortedDictionary<int, SoldierStatus> Statuses { get { return statuses; } }
        internal string OtherStatus { get; set; }
        internal UnitHierarchyModel.UnitHierarchy UnitHierarchy { get; set; }

        internal bool HasUnsavedChanges { get; set; }

        //private bool pictureNeedsSaved;
        //internal bool PictureNeedsSaved { get { return pictureNeedsSaved; } }
        
        //private string newPictureFilename;
        internal string NewPictureFilename { get; set; }

        internal bool hasCustomImage;

        //private Image picture;
        internal Image Picture
        {
            get
            {
                return SoldierModel.GetSoldierImage(this);
            }
        }

        internal Guid soldierGUID { get; set; }

        internal Soldier()
        {
            SoldierID = -1;
            LastName = "";
            FirstName = "";
            MiddleInitial = ' ';
            DateOfBirth = new DateTime();
            DateOfRank = new DateTime();
            OtherStatus = "";
            //Picture = null;
            //pictureFilename = "";
            //pictureNeedsSaved = false;
            HasUnsavedChanges = false;
            //this.picture = null;
            NewPictureFilename = "";
            this.Rank = Ranking.PVT;
            UnitHierarchy = new UnitHierarchyModel.UnitHierarchy();
            statuses = new SortedDictionary<int, SoldierStatus>();
            soldierGUID = Guid.NewGuid();
            hasCustomImage = false;

            //InitializeStaticEnumList();
        }

        internal Soldier(int soldierID) : this()
        {
            if (soldierID < -1)
                return;

            //InitializeStaticEnumList();
            SoldierModel.LoadSoldierValues(this, soldierID);
            //DataTable soldierValues = SoldierModel.GetSoldierDatabaseValues(soldierID);
            //LoadSoldier(soldierValues);
        }

        public Soldier(Guid guid) : this()
        {
            //InitializeStaticEnumList();
            SoldierModel.LoadSoldierValues(this, guid);
            //DataTable soldierValues = SoldierModel.GetSoldierDatabaseValues(guid);
            //LoadSoldier(soldierValues);
        }

        /*
        static string GetValue(DataTable values, string column)
        {
            string value;

            try
            {
                value = values.Rows[0][column].ToString();
            }
            catch (Exception ex)
            {
                throw new DataLoadFailedException("Could not retrieve value: " + column + " from Soldiers table for selected Soldier", ex);
            }

            return value;
        }
        */
        /*
        void LoadSoldier(DataTable soldierValues)
        {
            try
            {
                SoldierID = Convert.ToInt32(GetValue(soldierValues, "soldierid"));
                LastName = GetValue(soldierValues, "lastname");
                FirstName = GetValue(soldierValues, "firstname");
                MiddleInitial = GetValue(soldierValues, "middleinitial")[0];

                string dateOfRankStr = GetValue(soldierValues, "dateofrank");
                if (dateOfRankStr != "")
                {
                    long dateOfRankTicks = Convert.ToInt64(dateOfRankStr);
                    DateOfRank = new DateTime(dateOfRankTicks);
                }

                string dateOfBirthStr = GetValue(soldierValues, "dateofbirth");
                if (dateOfBirthStr != "")
                {
                    long dateOfBirthTicks = Convert.ToInt64(dateOfBirthStr);
                     DateOfBirth = new DateTime(dateOfBirthTicks);
                }

                string rankStr = GetValue(soldierValues, "rankingid");
                Rank = (Ranking)(Convert.ToInt32(rankStr));

                //string squadSectionStr = GetValue(soldierValues, "squadsectionid");
                //SquadSectionID = Convert.ToInt32(squadSectionStr);

                string imageFilePath = GetValue(soldierValues, "imagefilepath");

                PictureFilename = imageFilePath;

                try
                {
                    if (imageFilePath != "")
                    {
                        Picture = new Bitmap(imageFilePath);
                    }
                }
                catch (Exception)
                {
                    //Picture = new Bitmap("..\\..\\images\\error.bmp");
                    Picture = CounselQuickPlatinum.Properties.Resources.error;
                }


                OtherStatus = GetValue(soldierValues, "otherstatustext");

                LoadSoldierStatuses();

                string unitHierarchyIDString = GetValue(soldierValues, "unithierarchyid");
                int unitHierarchyID = Convert.ToInt32(unitHierarchyIDString);

                UnitHierarchy = UnitHierarchyModel.GetUnitHierarchyByID(unitHierarchyID);

                string guidString = GetValue(soldierValues, "soldierguid");
                
                soldierGUID = new Guid(guidString);
            }
            catch (DataLoadFailedException ex)
            {
                throw new DataLoadFailedException("Error retrieving values for the selected Soldier", ex);
            }
        }
         */





        private void LoadSoldierStatuses()
        {
            int soldierID = SoldierID;

            DataTable soldierStatuses = SoldierModel.GetSoldierStatuses(soldierID);

            foreach (DataRow soldierStatusRow in soldierStatuses.Rows)
            {
                int statusid = Convert.ToInt32(soldierStatusRow["statusenumid"]);

                //for (int i = 0; i < Statuses.Count; i++)
                foreach(int statusID in Statuses.Keys)
                {
                    if (Statuses[statusID].statusEnumID == statusid)
                        Statuses[statusID].applies = true;
                }
            }
        }


        internal int Save()
        {
            Logger.Trace("Soldier - Save()");

            try
            {
                SoldierID = SoldierModel.SaveSoldier(this);
                Logger.Trace("Soldier - soldier saved...");
                //pictureNeedsSaved = false;
                return SoldierID;
            }
            catch (FileException ex)
            {
                Logger.Error("FileException in Soldier.Save", ex);
                throw new DataStoreFailedException("An unexpected error occured while trying to save the Soldier data.", ex);
            }
            catch (DataStoreFailedException ex)
            {
                Logger.Error("DataStoreFailedException in Soldier.Save", ex);
                throw new DataStoreFailedException("An unexpected error occured while trying to save the Soldier data.", ex);
            }
        }
        /*
        private void LoadPicture()
        {
            // if there was no image specified
            if (picture == null && (PictureFilename == null || PictureFilename == ""))
            {
                int rankID = Convert.ToInt32(Rank);
                picture = RankingModel.GetRankingImages()[rankID];
            }
            else if (picture == null && !File.Exists(PictureFilename))
            {
                // if there was one specified but the picture is missing
                picture = Properties.Resources.error;
            }
            else if (picture == null)
            {
                // if there was a picture specified and it exists
                picture = Image.FromFile(PictureFilename);
            }
            else
            {
                // else return it
                //return picture;
            }

            //return picture;
        }
         * */
    }
}
