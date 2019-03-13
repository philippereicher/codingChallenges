using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode
{
    class Vehicle
    {
        private int Xpos { get; set; }
        private int Ypos { get; set; }
        private int Time { get; set; }
        private int Index { get; set; }

        void SetXPos(int x)
        {
            Xpos = x;
        }

        void SetYPos(int y)
        {
            Ypos = y;
        }

        void SetTime(int t)
        {
            Time = t;
        }

        void SetIndex(int i)
        {
            Index = i;
        }

        public List<int> rides = new List<int>();

        public Vehicle(int x = 0, int y = 0, int t = 0)
        {
            Xpos = x;
            Ypos = y;
            Time = t;
        }

        public int DoRide(Ride r)
        {
            int points = 0;
            int endTime = CalcRide(r);
            if(endTime >= 0)
            {
                //add ride to list
                rides.Add(r.index);
                Xpos = r.xPosEnd;
                Ypos = r.yPosEnd;
                Time = endTime;
                int distance = Math.Abs(r.xPosStart - r.xPosEnd) + Math.Abs(r.yPosStart - r.yPosEnd);
                points += distance;
                if (r.timeStart + distance == Time)
                {
                    points += Program.bonus;
                }
            }

            return points;
        }

        public int CalcRide(Ride r)
        {
            int wastedTime = 0;
            int res = -1;
            int maxTime = r.timeEnd;

            // calc Time to reach start point
            int neededTime = Math.Abs(Xpos - r.xPosStart) + Math.Abs(Ypos - r.yPosStart);
            // calc Time to reach end point
            neededTime += Math.Abs(r.xPosStart - r.xPosEnd) + Math.Abs(r.yPosStart - r.yPosEnd);
            // see if it is even possible
            if (Time + neededTime > r.timeEnd)
            {
                return -1;
            }
            else
            {
                // calc time to wait for startTime
                wastedTime = r.timeStart - Time;
                // set time to start
                if (Time > r.timeStart)
                {
                    res = Time;
                }
                else
                {
                    res = r.timeStart;
                }
                // add needed time
                res += neededTime;
            }
        
            return res;
        }

        public int CalcWastedTime(Ride r)
        {
            int wastedTime = 0;
            int res = -1;
            int maxTime = r.timeEnd;

            // calc Time to reach start point
            int neededTime = Math.Abs(Xpos - r.xPosStart) + Math.Abs(Ypos - r.yPosStart);
            // calc Time to reach end point
            neededTime += Math.Abs(r.xPosStart - r.xPosEnd) + Math.Abs(r.yPosStart - r.yPosEnd);
            // see if it is even possible
            if (Time + neededTime > r.timeEnd)
            {
                return int.MaxValue;
            }
            else
            {
                // calc time to wait for startTime
                wastedTime = r.timeStart - Time;
            }

            return wastedTime;
        }
    }
}
