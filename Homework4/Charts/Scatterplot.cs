using Homework4.CustomForm;
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
        private SolidBrush backgroundColor;
        private SolidBrush textColor;
        private Pen pen;

        private int SP_START_X;
        private int SP_START_Y;
        private int SP_WIDTH;
        private int SP_HEIGHT;

        private int gapY;
        private int maxValueGapY;
        private int minValueGapY;
        private int gapX;
        private int maxValueGapX;
        private int minValueGapX;
        private int gapLineLength;

        private int scaleDistanceX;
        private int scaleDistanceY;

        private Font font;
        private int textWidth;
        private int textHeight;

        //TODO FOR FUTURE: Making all the sub-variables being scaled with the scatterplot size
        public Scatterplot(IEnumerable<DataPoint> sampleIn)
        {
            this.sample = sampleIn;

            this.SP_START_X = 40;
            this.SP_START_Y = 200;
            this.SP_WIDTH = 200;
            this.SP_HEIGHT = 200;

            this.gapY = 10;
            this.minValueGapY = 0;
            this.maxValueGapY = 200;
            this.gapX = 10;
            this.minValueGapX = 0;
            this.maxValueGapX = 100;
            this.gapLineLength = 5;

            this.scaleDistanceY = this.SP_HEIGHT / this.maxValueGapY;
            this.scaleDistanceX = this.SP_WIDTH / this.maxValueGapX;

            this.font = new Font("Arial", 8);
            this.textColor = new SolidBrush(Color.Black);
            this.textWidth = 10;
            this.textHeight = 5;

            this.blueColor = new SolidBrush(Color.FromArgb(0, 0, 255));
            this.backgroundColor = new SolidBrush(Color.White);
            this.pen = new Pen(Color.Black);
        }

        public void Draw(Graphics g)
        {
            if (g == null) return;
            g.FillRectangle(this.backgroundColor, new Rectangle(SP_START_X, SP_START_Y, SP_WIDTH + (int)pen.Width, SP_HEIGHT + (int)pen.Width));
            g.DrawRectangle(this.pen, new Rectangle(SP_START_X, SP_START_Y, SP_WIDTH + (int)pen.Width, SP_HEIGHT + (int)pen.Width));

            //Let's start drawing the markers for the Y axis
            for(int i = minValueGapY; i <= maxValueGapY; i += this.gapY)
            {
                if(i != maxValueGapY)
                    g.DrawLine(this.pen, new Point(this.SP_START_X - this.gapLineLength, this.SP_START_Y + i), new Point(this.SP_START_X + this.gapLineLength, this.SP_START_Y + i));
                //This else is needed to fix a simple visualization "bug" that occurs since the pen has a width of 1 pixel
                else
                    g.DrawLine(this.pen, new Point(this.SP_START_X - this.gapLineLength, this.SP_START_Y + i + 1), new Point(this.SP_START_X + this.gapLineLength, this.SP_START_Y + i + 1));
            }
            //Let's draw the markers for the X axis
            for (int i = minValueGapX; i <= maxValueGapX; i += this.gapX)
            {
                if(i != maxValueGapX)
                    g.DrawLine(this.pen, new Point(this.SP_START_X + i * this.scaleDistanceX, this.SP_START_Y + this.SP_HEIGHT - this.gapLineLength), new Point(this.SP_START_X + i * this.scaleDistanceX, this.SP_START_Y + this.SP_HEIGHT + this.gapLineLength + 1));
                else
                    g.DrawLine(this.pen, new Point(this.SP_START_X + i * this.scaleDistanceX + 1, this.SP_START_Y + this.SP_HEIGHT - this.gapLineLength), new Point(this.SP_START_X + i * this.scaleDistanceX + 1, this.SP_START_Y + this.SP_HEIGHT + this.gapLineLength + 1));
            }

            //173,65
            foreach (DataPoint point in this.sample)
            {
                Point test = relativePointPosition(point.x, point.y);
                g.FillEllipse(this.blueColor,test.X, test.Y, 8, 8);
                //g.FillEllipse(this.blueColor, this.SP_START_X + point.x, this.SP_START_Y + this.SP_HEIGHT - point.y, 4, 4);
            }


        }

        public Point relativePointPosition(int x, int y)
        {
            int newX = this.SP_START_X + (x * this.scaleDistanceX);
            int newY = (this.SP_START_Y + this.SP_HEIGHT) - (y * this.scaleDistanceY);
            return new Point(newX, newY);
        }
    }
}
