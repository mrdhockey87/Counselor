using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    [System.Reflection.ObfuscationAttribute(Feature = "properties renaming")]
    public enum Ranking : int
    {
        None = -1,
        PVT = 1,
        PV2 = 2,
        PFC = 3,
        SPC = 4,
        CPL = 5,
        SGT = 6,
        SSG = 7,
        SFC = 8,
        MSG = 9,
        FirstSG = 10,
        SGM = 11,
        CSM = 12,
        WO1 = 13,
        CW2 = 14,
        CW3 = 15,
        CW4 = 16,
        CW5 = 17,
        SecondLT = 18,
        FirstLT = 19,
        CPT = 20,
        MAJ = 21,
        LTC = 22,
        COL = 23,
        BG = 24,
        MG = 25,
        LTG = 26,
        GEN = 27
    }

    //[System.Reflection.ObfuscationAttribute(Feature = "properties renaming", ApplyToMembers = true)]
    //[System.Reflection.ObfuscationAttribute(Feature = "renaming", ApplyToMembers = true)]
    internal class RankingModel
    {
        private static DataTable rankingsTable;
        private static List<Image> rankingImages;

        internal static List<Image> GetRankingImages()
        {
            Logger.Trace("GetRankingImages");

            if (rankingImages != null)
                return rankingImages;

            rankingImages = new List<Image>();
            string rankingImagePath = SettingsModel.RankingImageDirectory;

            if (rankingsTable == null)
                RefreshRankingsTable();

            Logger.Trace("Refreshed Rankings Table");

            SortedDictionary<string, object> resourcesDictionary = SettingsModel.ResourcesDictionary;

            foreach (DataRow row in rankingsTable.Rows)
            {
                string rankImage = row["rankingimagepath"].ToString();

                Logger.Trace("rankImage - " + rankImage);

                Image imagePath = (Image) resourcesDictionary[rankImage];

                rankingImages.Add(imagePath);
            }

            //Image newImagePath = (Image)CounselQuickPlatinum.Properties.Resources.NEW;
            //rankingImages.Add(newImagePath);

            Logger.Trace("Exit GetRankingImages");

            return rankingImages;
        }


        internal static DataTable GetRankingTable()
        {
            try
            {
                if (rankingsTable == null)
                    RefreshRankingsTable();

                return rankingsTable;
            }
            catch (DataLoadFailedException ex)
            {
                throw ex;
            }
            //try
            //{
            //    rankingsTable = DatabaseConnection.GetTable("rankings");
            //    return rankingsTable;
            //}
            //catch (QueryFailedException ex)
            //{
            //    throw new DataLoadFailedException("Could not retrieve the rankings table.", ex);
            //}
        }


        internal static void RefreshRankingsTable()
        {
            try
            {
                rankingsTable = DatabaseConnection.GetTableWhereNot("rankings", "rankingid", -1);
                //return rankingsTable;
            }
            catch (QueryFailedException ex)
            {
                throw new DataLoadFailedException("Could not retrieve the rankings table.", ex);
            }
        }


        internal static string RankToString(Ranking rank)
        {
            if (rank == Ranking.None)
                return "";

            if (rankingsTable == null)
                RefreshRankingsTable();

            DataRow[] row = rankingsTable.Select("rankingid = " + (int)rank);
            string rankingAbbr = row[0]["rankingabbreviation"].ToString();

            return rankingAbbr;
        }


        internal static string RankingToGrade(Ranking rank)
        {
            if (rank == Ranking.None)
                return "";

            if (rankingsTable == null)
                RefreshRankingsTable();

            DataRow[] row = rankingsTable.Select("rankingid = " + (int)rank);
            string grade = row[0]["rankinggradetext"].ToString();

            return grade;
        }

        internal static string RankingAbbreviationFromEnum(Ranking ranking)
        {
            if (rankingsTable == null)
                RefreshRankingsTable();

            if (ranking == Ranking.None)
                return "";

            DataRow row = rankingsTable.Select("rankingid = " + (int)ranking).First();
            string abbreviation = row["rankingabbreviation"].ToString();
            return abbreviation;
        }





    }

}
