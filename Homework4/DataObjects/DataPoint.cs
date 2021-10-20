using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4.DataObjects
{
    public class DataPoint
    {
        public int x { get; set; }
        public int y { get; set; }
        public DataPoint(int inX, int inY)
        {
            this.x = inX;
            this.y = inY;
        }
    }
}
