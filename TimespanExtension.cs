using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTCls
{
  public static class TimespanExtension
    {
        /// <summary>
        ///This is an Extension method to figure which property of Time span[Hours,Minutes,Seconds] will be used for comparison based on SRT Type. 
        /// </summary>
        /// <param name="TimeSpan">TimeSpan </param>
        /// <param name="srtType">If SRT Type is H then 0,M then 1,S then 2</param>
        ///<returns> If returned value is 1 then current object will be pushed to right of the Binary Tree else to the left side </returns> 
      public static double GetTime(this TimeSpan T, int srtType)
      {
          double i =0;
          if(srtType == 0)
          {
              i= T.Hours;
          }
          else if (srtType == 1)
          {
              i= T.Minutes;
          }
          else if (srtType == 2)
          {
              i = T.TotalSeconds;
          }
          else
          {
              i=5;
          }
          return i;
      }
    }
}
