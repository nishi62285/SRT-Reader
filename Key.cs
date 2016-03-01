using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SRTCls
{
    public class Key : IComparable, ICompareRange
    {
        #region variable declaration
        public TimeSpan min;
        public TimeSpan max;
        public TimeSpan compareTs;
        public String dialogue;
        public static double duration = 0;
        public enum TimeSpanData { Hour, Munite, Second };
        public string ComparisonKey;
        public static int srtType = 0;
        #endregion


        /// <summary>
        ///This is a constructor method which initializes TimeSpan objects and corresponding dialogues
        /// </summary>
        /// <param name="_min">TimeSpan object</param>
        /// <param name="_max">TimeSpan object</param>
        /// <param name="_dialogue">dialogue corresponding to _min and _max TimeSpan objects</param>
        public Key(TimeSpan _min, TimeSpan _max, string _dialogue)
        {
            this.max = _max;
            this.min = _min;
            this.dialogue = _dialogue;
        }
        public Key()
        {
          
        }

        /// <summary>
        ///This is a constructor method which initializes TimeSpan object.
        /// </summary>
        /// <param name="timeSpan">TimeSpan object</param>
        public Key(TimeSpan timeSpan)
        {
            this.compareTs = timeSpan;
        }
        public override bool Equals(object obj)
        {
            Key pom = (Key)obj;

            if (pom.max <= max && pom.min >= min)
            {
                return true;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return 1;
        }

       

        /// <summary>
        ///This method is called before inserting object into binary tree.
        ///If max hours and min hours is greater than duration i.e. (video/audio length) / 2 in terms then return 1 else return -1.
        /// </summary>
        /// <param name="obj">Object whose  to be compared</param>
        ///<returns> If returned value is 1 then current object will be pushed to right of the Binary Tree else to the left side </returns> 
        public int CompareTo(object obj)
        {

            if (((Key)this).max.GetTime(srtType) >= duration && ((Key)this).min.GetTime(srtType) >= duration)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        //This is a search function.
        //This comparison is made to get the dialogue corresponding to provided timespan range. 
        //public int CompareRange(object k)//object k : object to be searched in the binary tree
        //{

        //    //If current node/object's Hour is greater than duration provided
        //    if (((Key)this).compareTs.Hours >= duration)//***make a common function***
        //    {
        //        //If TotalSeconds,Minutes,Hours of the object[search obj] is greater than min timespan of current node/obj and less than max
        //        //means we have found node/obj from binary tree
        //        if ((((Key)this).compareTs.TotalSeconds >= ((Key)k).min.TotalSeconds && ((Key)this).compareTs.Minutes >= ((Key)k).min.Minutes && ((Key)this).compareTs.Hours >= ((Key)k).min.Hours) && (((Key)this).compareTs.TotalSeconds <= ((Key)k).max.TotalSeconds && ((Key)this).compareTs.Minutes <= ((Key)k).max.Minutes && ((Key)this).compareTs.Hours <= ((Key)k).max.Hours))
        //        {
        //            return 1;
        //        }
        //        //since current node/object's Hour is greater than duration provided return 2 . 
        //        //Value 2 means we have not found obj/node and continue searching object to the right side of the binary tree
        //        return 2;
        //    }
        //    else if (((Key)this).compareTs.Hours < duration)//***make a common function***
        //    {
        //        //If TotalSeconds,Minutes,Hours of the object[search obj] is greater than min timespan of current node/obj and less than max
        //        //means we have found node/obj from binary tree
        //        if ((((Key)this).compareTs.TotalSeconds >= ((Key)k).min.TotalSeconds && ((Key)this).compareTs.Minutes >= ((Key)k).min.Minutes && ((Key)this).compareTs.Hours >= ((Key)k).min.Hours) && (((Key)this).compareTs.TotalSeconds <= ((Key)k).max.TotalSeconds && ((Key)this).compareTs.Minutes <= ((Key)k).max.Minutes && ((Key)this).compareTs.Hours <= ((Key)k).max.Hours))
        //        {
        //            return 1;
        //        }
        //        //since current node/object's Hour is less than duration provided return 3 . 
        //        //Value 3 means we have not found obj/node and continue searching object to the left side of the binary tree
        //        return 3;
        //    }
        //    else
        //    {
        //        return 0;
        //    }

        //}


        /// <summary>
        ///This is a search function
        ///Here comparison is made to get the dialogue corresponding to provided timespan range. 
        /// </summary>
        /// <param name="object">Object to be searched</param>
        ///<returns> If returned value is 3 then continue searching current object on the Left side.If value is 2 then continue searching current object on the Right side. If value is 1 then current object is found.</returns> 
        public int CompareRange(object k)
        {

            //If current node/object's Hour is greater than duration provided
            if (((Key)this).compareTs.GetTime(srtType) >= duration)//***make a common function***
            {
                //If TotalSeconds,Minutes,Hours of the object[search obj] is greater than min timespan of current node/obj and less than max
                //means we have found node/obj from binary tree
                if ((((Key)this).compareTs.TotalSeconds >= ((Key)k).min.TotalSeconds && ((Key)this).compareTs.Minutes >= ((Key)k).min.Minutes && ((Key)this).compareTs.Hours >= ((Key)k).min.Hours) && (((Key)this).compareTs.TotalSeconds <= ((Key)k).max.TotalSeconds && ((Key)this).compareTs.Minutes <= ((Key)k).max.Minutes && ((Key)this).compareTs.Hours <= ((Key)k).max.Hours))
                {
                    return 1;
                }
                //since current node/object's Hour is greater than duration provided return 2 . 
                //Value 2 means we have not found obj/node and continue searching object to the right side of the binary tree
                return 2;
            }
            else if (((Key)this).compareTs.GetTime(srtType) < duration)//***make a common function***
            {
                //If TotalSeconds,Minutes,Hours of the object[search obj] is greater than min timespan of current node/obj and less than max
                //means we have found node/obj from binary tree
                if ((((Key)this).compareTs.TotalSeconds >= ((Key)k).min.TotalSeconds && ((Key)this).compareTs.Minutes >= ((Key)k).min.Minutes && ((Key)this).compareTs.Hours >= ((Key)k).min.Hours) && (((Key)this).compareTs.TotalSeconds <= ((Key)k).max.TotalSeconds && ((Key)this).compareTs.Minutes <= ((Key)k).max.Minutes && ((Key)this).compareTs.Hours <= ((Key)k).max.Hours))
                {
                    return 1;
                }
                //since current node/object's Hour is less than duration provided return 3 . 
                //Value 3 means we have not found obj/node and continue searching object to the left side of the binary tree
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }
}
