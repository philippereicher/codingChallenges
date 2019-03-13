using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCode
{
    class Ride
    {
        public int xPosStart;
        public int xPosEnd;
        public int yPosStart;
        public int yPosEnd;
        public int timeStart;
        public int timeEnd;
        public int index;

        public Ride(int xPosStart, int xPosEnd, int yPosStart, int yPosEnd, int timeStart, int timeEnd, int index)
        {
            this.xPosStart = xPosStart;
            this.xPosEnd = xPosEnd;
            this.yPosStart = yPosStart;
            this.yPosEnd = yPosEnd;
            this.timeStart = timeStart;
            this.timeEnd = timeEnd;
            this.index = index;
        }
    }
}
