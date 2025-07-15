using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounselQuickPlatinum
{
    internal class SoldierStatus : IEquatable<SoldierStatus>
    {
        internal int statusEnumID;
        internal string statusString;
        internal bool applies;

        internal SoldierStatus(int statusEnumID, string statusString, bool applies)
        {
            this.statusEnumID = statusEnumID;
            this.statusString = statusString;
            this.applies = applies;
        }

        public bool Equals(SoldierStatus other)
        {
            bool equals = true;
            equals &= this.statusEnumID == other.statusEnumID;
            equals &= this.statusString == other.statusString;
            equals &= this.applies == other.applies;

            return equals;
        }

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashCode = statusEnumID.GetHashCode();

            hashCode ^= statusString.GetHashCode() * 1361;

            hashCode ^= applies.GetHashCode();

            //Calculate the hash code for the product.
            return hashCode;
        }
    }
}
