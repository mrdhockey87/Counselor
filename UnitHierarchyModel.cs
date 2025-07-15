using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal static class UnitHierarchyModel
    {
        static DataSet unitHierarchyDataSet;

        internal delegate void UnitHierarchyModelUpdatedEvent();
        static private event UnitHierarchyModelUpdatedEvent OnUnitHierarchyModelUpdated;
        static private System.Threading.Mutex mutex;

        internal class Battalion
        {
            internal int BattalionID;
            internal string BattalionName;
        }

        internal class UnitHierarchy
        {
            internal int unitHierarchyID;
            internal int battalionID;
            internal string battalionName;
            internal int unitID;
            internal int unitDesignatorID;
            internal int platoonID;
            internal int squadID;

            internal UnitHierarchy()
            {
                unitHierarchyID = -1;
                battalionID = -1;
                battalionName = "";
                unitID = -1;
                unitDesignatorID = -1;
                platoonID = -1;
                squadID = -1;

            }

            public static bool operator==(UnitHierarchy lhs, UnitHierarchy rhs)
            {
                if (Equals(rhs, null))
                    return false;
                
                bool equals = true;

                //equals &= lhs.unitHierarchyID == rhs.unitHierarchyID;
                equals &= lhs.battalionID == rhs.battalionID;
                equals &= lhs.unitID == rhs.unitID;
                equals &= lhs.unitDesignatorID == rhs.unitDesignatorID;
                equals &= lhs.platoonID == rhs.platoonID;
                equals &= lhs.squadID == rhs.squadID;
                
                return equals;
            }

            public static bool operator !=(UnitHierarchy lhs, UnitHierarchy rhs)
            {
                bool equals = lhs == rhs;

                return equals;
            }

            public override int GetHashCode()
            {
                int hash = 13;
                hash = (hash * 7) + unitHierarchyID.GetHashCode();
                hash = (hash * 7) + unitID.GetHashCode();
                hash = (hash * 7) + battalionID.GetHashCode();
                hash = (hash * 7) + battalionName.GetHashCode();
                hash = (hash * 7) + platoonID.GetHashCode();
                hash = (hash * 7) + squadID.GetHashCode();

                return hash;
            }

            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                UnitHierarchy u = obj as UnitHierarchy;
                if ((System.Object)u == null)
                    return false;

                return this == u;
            }
        }

        static UnitHierarchyModel()
        {
            mutex = new System.Threading.Mutex();
            RefreshUnitHierarchyDataSet();
        }


        internal static DataSet GetAllUnitInfo()
        {
            if (unitHierarchyDataSet == null)
            {
                RefreshUnitHierarchyDataSet();
                return unitHierarchyDataSet;
            }

            return unitHierarchyDataSet;
        }


        internal static UnitHierarchyModelUpdatedEvent UnitHierarchyModelUpdated
        {
            set
            {
                OnUnitHierarchyModelUpdated += value;
            }
        }


        internal static void Refresh()
        {
            DatabaseConnection.Backup();
            
            RefreshUnitHierarchyDataSet();

            if (OnUnitHierarchyModelUpdated != null)
                OnUnitHierarchyModelUpdated();
        }


        internal static void RefreshUnitHierarchyDataSet()
        {
            try
            {
                Lock();
                unitHierarchyDataSet = new DataSet();
                DataTable unitHierarchyTable = DatabaseConnection.GetTableWhereNot("unithierarchies", "unithierarchyid", -1);
                DataTable batallionsTable = DatabaseConnection.GetTableWhereNot("battalions", "battalionid", -1);
                DataTable unitTable = DatabaseConnection.GetTableWhereNot("units", "unitid", -1);
                DataTable unitDesignatorsTable = DatabaseConnection.GetTableWhereNot("unitdesignators", "unitdesignatorid", -1);
                DataTable platoonsTable = DatabaseConnection.GetTableWhereNot("platoons", "platoonid", -1);
                DataTable squadssectionsTable = DatabaseConnection.GetTableWhereNot("squadsections", "squadsectionid", -1);

                unitHierarchyTable.TableName = "unithierarchies";
                batallionsTable.TableName = "battalions";
                unitTable.TableName = "units";
                unitDesignatorsTable.TableName = "unitdesignators";
                platoonsTable.TableName = "platoons";
                squadssectionsTable.TableName = "squadsections";

                unitHierarchyDataSet.Tables.Add(unitHierarchyTable);
                unitHierarchyDataSet.Tables.Add(batallionsTable);
                unitHierarchyDataSet.Tables.Add(unitTable);
                unitHierarchyDataSet.Tables.Add(unitDesignatorsTable);
                unitHierarchyDataSet.Tables.Add(platoonsTable);
                unitHierarchyDataSet.Tables.Add(squadssectionsTable);
                Unlock();
            }
            catch (QueryFailedException ex)
            {
                Unlock();
                throw new DataLoadFailedException("An error occured while attempting to load the unit hierarchy.", ex);
            }
        }


        //public static int GetPlatoonIDForSquadSection(int squadSectionID)
        //{
        //    DataTable results;
        //    int platoonid;
        //    string error = "Could not retrieve the unit hierarchy for the selected soldier";

        //    try
        //    {
        //        results = DatabaseConnection.Query("select * from squadsections where squadsectionid = " + squadSectionID);

        //        if (results.Rows.Count != 1)
        //            throw new DataLoadFailedException("Could not retrieve the unit hierarchy for the selected soldier");

        //        platoonid = Convert.ToInt32(results.Rows[0]["platoonid"]);

        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //    catch (DataException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the unit hierarchy for the selected soldier", ex);
        //    }
        //    catch (InvalidCastException ex)
        //    {
        //        throw new DataLoadFailedException("Could not retrieve the unit hierarchy for the selected soldier", ex);
        //    }


        //    return platoonid;
        //}


        public static string GetPlatoonName(int platoonID)
        {
            string error = "Could not retrieve the unit hierarchy for the selected Soldier";

            try
            {
                string platoonName = DatabaseConnection.GetSingleValue("platoons", "platoonid", platoonID, "platoonname");
                return platoonName;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException(error, ex);
            }
        }


        internal static string GetSectionSquadName(int sectionSquadID)
        {
            string error = "Could not retrieve the unit hierarchy for the selected Soldier";

            try
            {
                string sectionSquadName = DatabaseConnection.GetSingleValue("squadsections", "squadsectionid", sectionSquadID, "squadsectionname");
                return sectionSquadName;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException(error, ex);
            }
        }


        //internal static int GetUnitIDForPlatoonID(int platoonID)
        //{
        //    string error = "Could not retrieve the unit hierarchy for the selected soldier";

        //    try
        //    {
        //        string unitID = DatabaseConnection.GetSingleValue("platoons", "platoonid", platoonID, "unitid");
        //        return Convert.ToInt32(unitID);
        //    }
        //    catch (InvalidCastException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //    catch (QueryFailedException ex)
        //    {
        //        throw new DataLoadFailedException(error, ex);
        //    }
        //}


        internal static string GetUnitName(int unitID)
        {
            string error = "Could not retrieve the unit hierarhy for the selected soldier";

            try
            {
                string unitName = DatabaseConnection.GetSingleValue("units", "unitid", unitID, "unitname");
                return unitName;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException(error, ex);
            }
        }


        internal static UnitHierarchy GetUnassignedUnitHierarchy()
        {
            UnitHierarchy hierarchy = new UnitHierarchy();
            hierarchy.unitHierarchyID = -1;
            hierarchy.unitID = -1;
            hierarchy.unitDesignatorID = -1;
            hierarchy.battalionID = -1;
            hierarchy.battalionName = "UNASSIGNED";
            hierarchy.platoonID = -1;
            hierarchy.squadID = -1;

            return hierarchy;
        }


        internal static UnitHierarchy GetUnitHierarchyByID(int unitHierarchyID)
        {
            try
            {
                if (unitHierarchyID == -1)
                    return GetUnassignedUnitHierarchy();

                DataRow[] row = unitHierarchyDataSet.Tables["unithierarchies"].Select("unithierarchyid = " + unitHierarchyID);

                UnitHierarchy hierarchy = new UnitHierarchy();
                hierarchy.unitHierarchyID = Convert.ToInt32(row[0]["unithierarchyid"]);
                hierarchy.battalionID = Convert.ToInt32(row[0]["battalionid"]);
                hierarchy.battalionName = GetBattalionName(hierarchy.battalionID);
                hierarchy.unitID = Convert.ToInt32(row[0]["unitid"]);
                hierarchy.unitDesignatorID = Convert.ToInt32(row[0]["unitdesignatorid"]);
                hierarchy.platoonID = Convert.ToInt32(row[0]["platoonid"]);
                hierarchy.squadID = Convert.ToInt32(row[0]["squadsectionid"]);

                return hierarchy;
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred attempting to load the unit hierarchy.");
            }
        }


        internal static string GetUnitDesignatorName(int unitDesignatorID)
        {
            try
            {
                DataRow[] row = unitHierarchyDataSet.Tables["unitdesignators"].Select("unitdesignatorid = " + unitDesignatorID);
                string designatorName = row[0]["unitdesignatorname"].ToString();

                return designatorName;
            }
            catch (Exception ex)
            {
                //throw new DataLoadFailedException("An error occurred accessing the unit hierarchy.");
                Logger.Error("UnitHierarchyModel - GetUnitDesignatorModel - error accessing unit hierarchy");
                Logger.Error(ex);

                return "None";
            }
        }


        internal static bool BattalionNameExists(string battalionName)
        {
            DataRow[] rows = unitHierarchyDataSet.Tables["battalions"].Select("battalionname = '" + battalionName + "'");

            return (rows.Count() > 0);
        }


        internal static int GetBattalionID(string battalionName)
        {
            try
            {
                DataRow[] row = unitHierarchyDataSet.Tables["battalions"].Select("battalionname = \'" + battalionName + "'");

                int battalionID = Convert.ToInt32(row[0]["battalionid"]);
                return battalionID;
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred accessing the unit hierarchy.");
            }
        }


        internal static string GetBattalionName(int battalionID)
        {
            try
            {
                DataRow[] row = unitHierarchyDataSet.Tables["battalions"].Select("battalionid = " + battalionID);

                string battalionName = row[0]["battalionname"].ToString();
                return battalionName;
            }
            catch (Exception ex)
            {
                Logger.Error("UnitHierarchyModel - GetBattalionName - error accessing unit hierarchy", ex);

                return "None";
            }
        }

        internal static int CreateBattalion(string battalionName)
        {
            string insertCommand = "insert into battalions (battalionname) values (@battalionname)";
            Params paramValues = new Params();
            paramValues.Add("@battalionname", battalionName);

            int battalionID = DatabaseConnection.Insert(insertCommand, paramValues);

            //int battalionID = DatabaseConnection.Insert("insert into battalions (battalionname) "
            //                                            + " values (\"" + battalionName + "\")");

            Refresh();
            DatabaseConnection.Backup();

            return battalionID;
        }


        internal static void UpdateUnitHierarchyID(UnitHierarchy unitHierarchy)
        {
            try
            {
                int oldUnitHierarchyID = unitHierarchy.unitHierarchyID;

                unitHierarchy.unitHierarchyID = CreateUnitHierarchyIfNotExists(unitHierarchy);
            }
            catch (DataStoreFailedException ex)
            {
                throw ex;
            }
        }


        private static bool UnitHierarchyReferenced(int unitHierarchyID)
        {
            DataRow[] rows
                //= unitHierarchyDataSet.Tables["unithierarchies"].Select("unithierarchyid = " + unitHierarchyID);
                = SoldierModel.GetSoldiersByUnitHierarchy(unitHierarchyID); //SoldierModel.FormattedSoldiersTable.Select("unithierarchyid = " + unitHierarchyID);

            if (rows.Length > 0)
                return true;

            return false;
        }


        private static void RemoveUnitHierarchy(int unitHierarchyID)
        {
            //string deleteCommand = "delete from unithierarchies where unithierarchyid = " + unitHierarchy;
            try
            {
                DatabaseConnection.Delete("unithierarchies", "unithierarchyid", unitHierarchyID.ToString());
                
                Refresh();
                DatabaseConnection.Backup();
            }
            catch (NonQueryFailedException ex)
            {
                throw new DataStoreFailedException("Failed deleting the unit hierarchy.", ex);
            }
        }


        internal static int CreateUnitHierarchyIfNotExists(UnitHierarchy unitHierarchy)
        {
            //if (unitHierarchy.battalionID == -1 || unitHierarchy.battalionID == 0)
            if (unitHierarchy.battalionID == -1)
                unitHierarchy.battalionID = CreateBattalion(unitHierarchy.battalionName);

            string selectStatement = "battalionid = " + unitHierarchy.battalionID
                                    + " and unitid = " + unitHierarchy.unitID
                                    + " and unitdesignatorid = " + unitHierarchy.unitDesignatorID
                                    + " and platoonid = " + unitHierarchy.platoonID
                                    + " and squadsectionid = " + unitHierarchy.squadID;

            DataRow[] rows = unitHierarchyDataSet.Tables["unithierarchies"].Select(selectStatement);

            int unitHierarchyID;

            if (rows.Length != 0)
            {
                //    return unitHierarchy.unitHierarchyID;
                unitHierarchyID = Convert.ToInt32(rows[0]["unithierarchyid"]);
                return unitHierarchyID;
            }


            string insertStatement = "insert into unithierarchies "
                                    + "(battalionid, unitid, unitdesignatorid, platoonid, squadsectionid) "
                                    + " values "
                                    + " ( @battalionID, @unitID, @unitdesignatorid, @platoonID, @squadID )";
            string battalionID = unitHierarchy.battalionID.ToString();
            string unitID = unitHierarchy.unitID.ToString();
            string platoonID = unitHierarchy.platoonID.ToString();
            string squadID = unitHierarchy.squadID.ToString();
            string unitDesignatorID = unitHierarchy.unitDesignatorID.ToString();

            Params paramValues = new Params();
            paramValues.Add("@battalionID", battalionID);
            paramValues.Add("@unitID", unitID);
            paramValues.Add("@platoonID", platoonID);
            paramValues.Add("@squadID", squadID);
            paramValues.Add("@unitdesignatorid", unitDesignatorID);

            unitHierarchyID = DatabaseConnection.Insert(insertStatement, paramValues);

            Refresh();
            DatabaseConnection.Backup();

            return unitHierarchyID;
        }

        internal static void RemoveUnitHierarchyIfUnreferenced(int oldUnitHierarchyID)
        {
            if (!UnitHierarchyReferenced(oldUnitHierarchyID))
                RemoveUnitHierarchy(oldUnitHierarchyID);
        }

        internal static int GetUnitHierarchyIDIfExists(UnitHierarchy unitHierarchy)
        {
            Lock();
            int bID = unitHierarchy.battalionID;
            int uID = unitHierarchy.unitID;
            int udID = unitHierarchy.unitDesignatorID;
            int pID = unitHierarchy.platoonID;
            int sID = unitHierarchy.squadID;

            string selectStatement = "battalionid = " + bID + " and "
                                        + "unitid = " + uID + " and "
                                        + "unitdesignatorid = " + udID + " and "
                                        + "platoonid = " + pID + " and "
                                        + "squadsectionid = " + sID;

            DataRow unitHierarchyRow = unitHierarchyDataSet.Tables["unithierarchies"].Select(selectStatement).First();
            if (unitHierarchyRow == null)
                return -1;

            int unitHierarchyID = Convert.ToInt32(unitHierarchyRow["unithierarchyid"]);
            Unlock();

            return unitHierarchyID;
        }

        private static void Lock()
        {
            mutex.WaitOne();
        }

        private static void Unlock()
        {
            mutex.ReleaseMutex();
        }

        internal static DataRow[] GetNonEmptyUnitHierarchies()
        {
            Lock();
            DataTable soldiers = SoldierModel.GetSoldierRows("").CopyToDataTable();
            DataTable unitHierarchyTable = unitHierarchyDataSet.Tables["unithierarchies"];

            DataRow[] rows = unitHierarchyTable.AsEnumerable().Where(uh => soldiers.Select("unithierarchyid = " + Convert.ToInt32(uh["unithierarchyid"])).Count() > 0).ToArray();
            Unlock();
            return rows;
        }
    }
}
