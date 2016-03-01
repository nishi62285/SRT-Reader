using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRTCls
{
    public class BinaySearch : IDisposable
    {
        private string dialogue ;
        private  StreamReader read = default(StreamReader);
        private  Stream stream = default(Stream);
        private IBinarySearchTree<Key> bTree = default(IBinarySearchTree<Key>);
        private int Days , TotalSeconds, TotalMinutes, TotalHours, MiliSec, n;
        private TimeSpan Ts = default(TimeSpan);
        private bool isNumeric;
        private string[] field = new string[2];
        private Key key = default(Key);
        private StringBuilder sb = default(StringBuilder);

        /// <summary>
        /// This is constructor method initializes duration and SRTType parameters
        /// </summary>
        /// <param name="Duration">Total Time period of Video/Audio file</param>
        /// <param name="SRTType">Type of Video/Audio file in terms of Time</param>
        public BinaySearch(double duration, SRTType srtType)
        {
            duration = Convert.ToInt16(duration / 2);
            this.bTree = new BinarySearchTree<Key>(duration, GetSrtType(srtType));
        }

        /// <summary>
        /// This is method gets numeric value of SRT based on SRT Type provided.
        /// </summary>
        /// <param name="srtType">Type of Video/Audio file in terms of Time</param>
        private int GetSrtType(SRTType srtType)
        {
            int srtTp=0;

            if (srtType == SRTType.Hour)
            {
                srtTp = (int)SRTType.Hour;
            }
            else if (srtType == SRTType.Minute)
            {
                srtTp = (int)SRTType.Minute;
            }
            else if (srtType == SRTType.Second)
            {
                srtTp = (int)SRTType.Second;
            }
            return srtTp;
        }

        /// <summary>
        /// This method organizes contents of SRT file i.e. dialogue, time intervals organizes dialogues in the form of Binary Tree.
        /// </summary>
        /// <param name="srtPath">Phisical path of the SRT file</param>
        public void PrepareBinaryTree(string srtPath)
        {
           
            using (stream = new FileStream(srtPath, FileMode.Open))
            {
                read = new StreamReader(stream);

                while (!read.EndOfStream)
                {
                    //Read SRT file line by line 
                    dialogue = read.ReadLine();

                    //Find whether line is numeric.If yes then ignore the line else process it.
                    isNumeric = int.TryParse(dialogue, out n);

                    if ((dialogue.Replace("&nbsp;", "") != "" || dialogue != null) && !isNumeric)//need to verify
                    {
                         field = dialogue.Split("-->".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        //since SRT timeline is stored in the 100:00:00,970 --> 00:00:03,000 format so after split on --> we should get  string array of length 2
                        if (field.Length >= 2)
                        {
                            #region  create two timespan for start time and end time respectively[100:00:00,970 --> 00:00:03,000]

                             Ts = TimeSpan.Parse(field[0].Replace(',', '.'));

                            Days = Ts.Days;
                            TotalSeconds = Ts.Seconds;
                            TotalMinutes = Ts.Minutes;
                            TotalHours = Ts.Hours;
                            MiliSec = Ts.Milliseconds;
                            TimeSpan t = new TimeSpan(Days, TotalHours, TotalMinutes, TotalSeconds, MiliSec);

                            Ts = TimeSpan.Parse(field[1].Replace(',', '.'));
                            Days = Ts.Days;
                            TotalSeconds = Ts.Seconds;
                            TotalMinutes = Ts.Minutes;
                            TotalHours = Ts.Hours;
                            MiliSec = Ts.Milliseconds;
                            TimeSpan t1 = new TimeSpan(Days, TotalHours, TotalMinutes, TotalSeconds, MiliSec);

                            #endregion

                            sb = new System.Text.StringBuilder();
                            dialogue = read.ReadLine();

                            n = 0;
                            isNumeric = int.TryParse(dialogue, out n);

                            #region  read dialogue from SRT file corresponding to timespan calculated above[100:00:00,970 --> 00:00:03,000].Dialogue may vary from one line to multiple lines.
                            while (!isNumeric)
                            {
                                sb.Append(dialogue);
                                dialogue = read.ReadLine();
                                if (!read.EndOfStream)
                                {
                                    n = 0;
                                    isNumeric = int.TryParse(dialogue, out n);
                                }
                                else
                                    isNumeric = true;
                            }
                            #endregion

                            //Create a unique Key object which will store timespan duration,dialogue for that duration
                            key = new Key(t, t1, sb.ToString());

                            //insert key into Binary Search Tree
                            bTree.Insert(key);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Search function to get dialogue from SRT file corresponding to provided Time Span.
        /// </summary>
        /// <param name="timeSpan">Time Span for which dialogue is to be searched in the SRT file</param>
        public string GetDialogue(TimeSpan timeSpan)
        {
            key = new Key(timeSpan);
            key = bTree.FindRange(key);
            dialogue = key != null ? key.dialogue : "";
            return dialogue;
        }


        public void Dispose()
        {
            read.Dispose();
            stream.Dispose();

        }
    }



}
