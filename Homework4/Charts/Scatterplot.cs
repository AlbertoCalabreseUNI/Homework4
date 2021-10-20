using Homework4.DataObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4.Charts
{
    public class Scatterplot
    {
        private IEnumerable<DataPoint> sample;
        private SolidBrush blueColor;
        public Scatterplot(IEnumerable<DataPoint> sampleIn)
        {
            this.sample = sampleIn;
            this.blueColor = new SolidBrush(Color.FromArgb(0, 0, 255));
        }

        public void Draw(Graphics g)
        {
            if (g == null) return;
            foreach(DataPoint unit in sample)
            {
                g.FillEllipse(blueColor, new Rectangle(unit.x, unit.y, 5, 5));
            }
        }
    }
}
