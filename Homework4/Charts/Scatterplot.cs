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

        private int scaleDistance;

        private Font font;
        private int textWidth;
        private int textHeight;

        //TODO FOR FUTURE: Making all the sub-variables being scaled with the scatterplot size
        public Scatterplot(IEnumerable<DataPoint> sampleIn)
        {
            this.sample = sampleIn;

            this.SP_START_X = 80;
            this.SP_START_Y = 180;
            this.SP_WIDTH = 180;
            this.SP_HEIGHT = 190;

            this.gapY = 10;
            this.minValueGapY = 140;
            this.maxValueGapY = 190;
            this.gapX = 10;
            this.minValueGapX = 0;
            this.maxValueGapX = 100;
            this.gapLineLength = 5;

            this.scaleDistance = 1;

            this.font = new Font("Arial", 8);
            this.textColor = new SolidBrush(Color.Black);
            this.textWidth = 25;
            this.textHeight = 5;

            this.blueColor = new SolidBrush(Color.FromArgb(0, 0, 255));
            this.backgroundColor = new SolidBrush(Color.White);
            this.pen = new Pen(Color.Black);
        }

        public void Draw(Graphics g)
        {
            if (g == null) return;
            g.FillRectangle(this.backgroundColor, new Rectangle(SP_START_X, SP_START_Y, SP_WIDTH - (int)pen.Width, SP_HEIGHT - (int)pen.Width));
            g.DrawRectangle(this.pen, new Rectangle(SP_START_X, SP_START_Y, SP_WIDTH - (int)pen.Width, SP_HEIGHT - (int)pen.Width));

            
            //Let's draw Y gaps (horizontal lines)
            
            for (int i = 0; i <= maxValueGapY/* - minValueGapY*/; i+= gapY)
            {
                //This is to fix a stupid visualization thing that bothers me.
                if(i == 0)
                    g.DrawLine(pen, new Point(SP_START_X - this.gapLineLength, this.SP_START_Y + this.SP_HEIGHT - (i * this.scaleDistance) - 1), new Point(SP_START_X + this.gapLineLength, this.SP_START_Y + this.SP_HEIGHT - (i * this.scaleDistance) - 1));
                else
                    g.DrawLine(pen, new Point(SP_START_X - this.gapLineLength, this.SP_START_Y + this.SP_HEIGHT - (i * this.scaleDistance)), new Point(SP_START_X + this.gapLineLength, this.SP_START_Y + this.SP_HEIGHT - (i * this.scaleDistance)));
            }
            
                
            //Let's draw X gaps (vertical lines)
            for (int i = 0; i <= maxValueGapX /*- minValueGapX*/; i += gapX)
                g.DrawLine(this.pen, new Point(this.SP_START_X + (i* this.scaleDistance), this.SP_START_Y + this.SP_HEIGHT + this.gapLineLength), new Point(this.SP_START_X + (i* this.scaleDistance), this.SP_START_Y + this.SP_HEIGHT - this.gapLineLength));
        
            //Let's draw the gaps numbers
            //Y gaps
            for(int i = 0; i <= maxValueGapY /*- minValueGapY*/; i += gapY)
            {
                if (i == 0)
                    g.DrawString((/*minValueGapY*/ + i).ToString(), this.font, this.textColor, new PointF(SP_START_X - this.gapLineLength - this.textWidth, this.SP_START_Y + this.SP_HEIGHT - (i * this.scaleDistance) - 1 - this.textHeight));
                else
                    g.DrawString((/*minValueGapY*/ + i).ToString(), this.font, this.textColor, new PointF(SP_START_X - this.gapLineLength - this.textWidth, this.SP_START_Y + this.SP_HEIGHT - (i * this.scaleDistance) - textHeight));
            }
            for (int i = 0; i <= maxValueGapX - minValueGapX; i += gapX)
                g.DrawString((/*minValueGapX*/ + i).ToString(), this.font, this.textColor, new PointF(this.SP_START_X + (27 * i)/gapX, this.SP_START_Y + this.SP_HEIGHT + this.textHeight));
        
            //Let's draw elements inside the scatterplot
            foreach(DataPoint point in sample)
            {
                //173, 65
                Point viewportPoint = WindowtoViewport(point.x, point.y, 1260,0,0,600, this.SP_START_X+this.SP_WIDTH, this.SP_START_Y,this.SP_START_X, this.SP_START_Y + this.SP_HEIGHT);
                g.FillEllipse(this.blueColor,viewportPoint.X, viewportPoint.Y, 8,8);
                g.FillEllipse(this.blueColor, point.x, point.y,8,8);
            }
            
        }

        static Point WindowtoViewport(int x_w, int y_w,
                             int x_wmax, int y_wmax,
                             int x_wmin, int y_wmin,
                             int x_vmax, int y_vmax,
                             int x_vmin, int y_vmin)
        {
            // point on viewport 
            int x_v, y_v;

            // scaling factors for x coordinate  
            // and y coordinate 
            float sx, sy;

            // calculatng Sx and Sy 
            sx = (float)(x_vmax - x_vmin) /
                        (x_wmax - x_wmin);
            sy = (float)(y_vmax - y_vmin) /
                        (y_wmax - y_wmin);

            // calculating the point on viewport 
            x_v = (int)(x_vmin +
                (float)((x_w - x_wmin) * sx));
            y_v = (int)(y_vmin +
                (float)((y_w - y_wmin) * sy));
            return new Point(x_v, y_v);
        }
    }
}
