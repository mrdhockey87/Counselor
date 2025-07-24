using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

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

        public static string GetPlatoonName(int platoonID)
        {
            string error = "Could not retrieve the unit hierarchy for the selected Soldier";

            try
            {
                string platoonName = DatabaseConnection.GetSingleValue("platoons", "platoonid", platoonID, "platoonname");
                platoonName = FormatUnitHierarchyText(platoonName);
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
                sectionSquadName = FormatUnitHierarchyText(sectionSquadName);
                return sectionSquadName;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException(error, ex);
            }
        }

        internal static string GetUnitName(int unitID)
        {
            string error = "Could not retrieve the unit hierarhy for the selected soldier";

            try
            {
                string unitName = DatabaseConnection.GetSingleValue("units", "unitid", unitID, "unitname");
                unitName = FormatUnitHierarchyText(unitName);
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
                designatorName = FormatUnitHierarchyText(designatorName);
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
                battalionName = FormatUnitHierarchyText(battalionName);
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

            Refresh();
            DatabaseConnection.Backup();

            return battalionID;
        }

        // Unit creation methods
        internal static int CreateUnit(string unitName)
        {
            string insertCommand = "insert into units (unitname) values (@unitname)";
            Params paramValues = new Params();
            paramValues.Add("@unitname", unitName);

            int unitID = DatabaseConnection.Insert(insertCommand, paramValues);

            Refresh();
            DatabaseConnection.Backup();

            return unitID;
        }

        internal static bool UnitNameExists(string unitName)
        {
            DataRow[] rows = unitHierarchyDataSet.Tables["units"].Select("unitname = '" + unitName + "'");
            return (rows.Count() > 0);
        }

        internal static int GetUnitID(string unitName)
        {
            try
            {
                DataRow[] row = unitHierarchyDataSet.Tables["units"].Select("unitname = '" + unitName + "'");
                int unitID = Convert.ToInt32(row[0]["unitid"]);
                return unitID;
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred accessing the unit hierarchy.");
            }
        }

        // Unit Designator creation methods
        internal static int CreateUnitDesignator(string unitDesignatorName)
        {
            string insertCommand = "insert into unitdesignators (unitdesignatorname) values (@unitdesignatorname)";
            Params paramValues = new Params();
            paramValues.Add("@unitdesignatorname", unitDesignatorName);

            int unitDesignatorID = DatabaseConnection.Insert(insertCommand, paramValues);

            Refresh();
            DatabaseConnection.Backup();

            return unitDesignatorID;
        }

        internal static bool UnitDesignatorNameExists(string unitDesignatorName)
        {
            DataRow[] rows = unitHierarchyDataSet.Tables["unitdesignators"].Select("unitdesignatorname = '" + unitDesignatorName + "'");
            return (rows.Count() > 0);
        }

        internal static int GetUnitDesignatorID(string unitDesignatorName)
        {
            try
            {
                DataRow[] row = unitHierarchyDataSet.Tables["unitdesignators"].Select("unitdesignatorname = '" + unitDesignatorName + "'");
                int unitDesignatorID = Convert.ToInt32(row[0]["unitdesignatorid"]);
                return unitDesignatorID;
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred accessing the unit hierarchy.");
            }
        }

        // Platoon creation methods
        internal static int CreatePlatoon(string platoonName)
        {
            string insertCommand = "insert into platoons (platoonname) values (@platoonname)";
            Params paramValues = new Params();
            paramValues.Add("@platoonname", platoonName);

            int platoonID = DatabaseConnection.Insert(insertCommand, paramValues);

            Refresh();
            DatabaseConnection.Backup();

            return platoonID;
        }

        internal static bool PlatoonNameExists(string platoonName)
        {
            DataRow[] rows = unitHierarchyDataSet.Tables["platoons"].Select("platoonname = '" + platoonName + "'");
            return (rows.Count() > 0);
        }

        internal static int GetPlatoonID(string platoonName)
        {
            try
            {
                DataRow[] row = unitHierarchyDataSet.Tables["platoons"].Select("platoonname = '" + platoonName + "'");
                int platoonID = Convert.ToInt32(row[0]["platoonid"]);
                return platoonID;
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred accessing the unit hierarchy.");
            }
        }

        // Squad/Section creation methods
        internal static int CreateSquadSection(string squadSectionName)
        {
            string insertCommand = "insert into squadsections (squadsectionname) values (@squadsectionname)";
            Params paramValues = new Params();
            paramValues.Add("@squadsectionname", squadSectionName);

            int squadSectionID = DatabaseConnection.Insert(insertCommand, paramValues);

            Refresh();
            DatabaseConnection.Backup();

            return squadSectionID;
        }

        internal static bool SquadSectionNameExists(string squadSectionName)
        {
            DataRow[] rows = unitHierarchyDataSet.Tables["squadsections"].Select("squadsectionname = '" + squadSectionName + "'");
            return (rows.Count() > 0);
        }

        internal static int GetSquadSectionID(string squadSectionName)
        {
            try
            {
                DataRow[] row = unitHierarchyDataSet.Tables["squadsections"].Select("squadsectionname = '" + squadSectionName + "'");
                int squadSectionID = Convert.ToInt32(row[0]["squadsectionid"]);
                return squadSectionID;
            }
            catch (Exception)
            {
                throw new DataLoadFailedException("An error occurred accessing the unit hierarchy.");
            }
        }

        // Enhanced method to create or get IDs for unit hierarchy components with custom entries
        internal static int CreateUnitHierarchyWithCustomEntries(UnitHierarchy unitHierarchy, 
            string customUnitName = null, 
            string customUnitDesignatorName = null, 
            string customPlatoonName = null, 
            string customSquadSectionName = null)
        {
            // Handle Battalion (already exists)
            if (unitHierarchy.battalionID == -1 && !string.IsNullOrEmpty(unitHierarchy.battalionName))
                unitHierarchy.battalionID = CreateBattalion(unitHierarchy.battalionName);

            // Handle Unit
            if (unitHierarchy.unitID == -1 && !string.IsNullOrEmpty(customUnitName))
            {
                if (!UnitNameExists(customUnitName))
                    unitHierarchy.unitID = CreateUnit(customUnitName);
                else
                    unitHierarchy.unitID = GetUnitID(customUnitName);
            }

            // Handle Unit Designator
            if (unitHierarchy.unitDesignatorID == -1 && !string.IsNullOrEmpty(customUnitDesignatorName))
            {
                if (!UnitDesignatorNameExists(customUnitDesignatorName))
                    unitHierarchy.unitDesignatorID = CreateUnitDesignator(customUnitDesignatorName);
                else
                    unitHierarchy.unitDesignatorID = GetUnitDesignatorID(customUnitDesignatorName);
            }

            // Handle Platoon
            if (unitHierarchy.platoonID == -1 && !string.IsNullOrEmpty(customPlatoonName))
            {
                if (!PlatoonNameExists(customPlatoonName))
                    unitHierarchy.platoonID = CreatePlatoon(customPlatoonName);
                else
                    unitHierarchy.platoonID = GetPlatoonID(customPlatoonName);
            }

            // Handle Squad/Section
            if (unitHierarchy.squadID == -1 && !string.IsNullOrEmpty(customSquadSectionName))
            {
                if (!SquadSectionNameExists(customSquadSectionName))
                    unitHierarchy.squadID = CreateSquadSection(customSquadSectionName);
                else
                    unitHierarchy.squadID = GetSquadSectionID(customSquadSectionName);
            }

            // Create the unit hierarchy entry
            return CreateUnitHierarchyIfNotExists(unitHierarchy);
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
            DataRow[] rows = SoldierModel.GetSoldiersByUnitHierarchy(unitHierarchyID); 

            if (rows.Length > 0)
                return true;

            return false;
        }

        private static void RemoveUnitHierarchy(int unitHierarchyID)
        {
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

            DataRow[] unitHierarchyRows = unitHierarchyDataSet.Tables["unithierarchies"].Select(selectStatement);
            if (unitHierarchyRows.Length == 0)
            {
                Unlock();
                return -1;
            }

            int unitHierarchyID = Convert.ToInt32(unitHierarchyRows[0]["unithierarchyid"]);
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

        /// <summary>
        /// Formats text with proper capitalization for unit hierarchy entries
        /// </summary>
        /// <param name="text">The text to format</param>
        /// <returns>Formatted text with first letter capitalized and rest lowercase for alphabetic entries</returns>
        public static string FormatUnitHierarchyText(string text)
        {
            string trimmedText = text.ToSelectiveTitleCase();
            return trimmedText;
          /*  if (string.IsNullOrWhiteSpace(text))
                return text;

            // Check if the text contains only letters (and possibly spaces/hyphens)
            if (text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-'))
            {
                // Split the text into individual words
                string[] words = text.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                
                if (words.Length == 0)
                    return text;

                // Format each word individually with proper title case
                System.Globalization.TextInfo textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
                List<string> formattedWords = new List<string>();
                
                for (int i = 0; i < words.Length; i++)
                {
                    string word = words[i];
                    
                    // Handle words with hyphens by splitting and formatting each part
                    if (word.Contains("-"))
                    {
                        string[] hyphenParts = word.Split('-');
                        for (int j = 0; j < hyphenParts.Length; j++)
                        {
                            if (!string.IsNullOrWhiteSpace(hyphenParts[j]))
                            {
                                hyphenParts[j] = textInfo.ToTitleCase(hyphenParts[j].ToLower());
                            }
                        }
                        formattedWords.Add(string.Join("-", hyphenParts));
                    }
                    else
                    {
                        formattedWords.Add(textInfo.ToTitleCase(word.ToLower()));
                    }
                }
                
                // Join the formatted words back together with single spaces
                return string.Join(" ", formattedWords);
            }
            
            // For mixed alphanumeric or numeric entries, return as-is
            return text;*/
        }
    }
}
public static class StringExtensions
{
    public static string ToSelectiveTitleCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var textInfo = CultureInfo.CurrentCulture.TextInfo;

        // Split on whitespace while preserving the whitespace
        var parts = Regex.Split(input, @"(\s+)");

        for (int i = 0; i < parts.Length; i++)
        {
            // Skip whitespace parts
            if (string.IsNullOrWhiteSpace(parts[i]))
                continue;

            // Check if the part contains only alphabetic characters
            if (parts[i].All(char.IsLetter))
            {
                parts[i] = textInfo.ToTitleCase(parts[i].ToLower());
            }
            // Leave non-alphabetic parts unchanged
        }

        return string.Join("", parts);
    }
}