using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace HangManWPF
{
    public class Hangman
    {
        Ellipse head;
        Line body;
        Line armLeft;
        Line armRigth;
        Line legLeft;
        Line legRigth;
        Line rope;
        Line barTop;
        Line barLeft;
        Line barHold;
        Line barBottom;

        
        public Ellipse Head()
        {
            head = new Ellipse { Width = 30, Height = 30, Stroke = Brushes.White, StrokeThickness = 4};
            
            return head;
        }

        public Line Body()
        {

            body = new Line { StrokeThickness = 4, X1 = 215, Y1 = 230, X2 = 215, Y2 = 260, Stroke = Brushes.White };
            return body;
        }
        public Line ArmLeft()
        {
            armLeft = new Line { StrokeThickness = 4, X1 = 215, Y1 = 245, X2 = 230, Y2 = 235, Stroke = Brushes.White };
            return armLeft;
        }
        public Line ArmRigth()
        {
            armRigth = new Line { StrokeThickness = 4, X1 = 215, Y1 = 245, X2 = 200, Y2 = 235, Stroke = Brushes.White };
            return armRigth;
        }

        public Line LegRigth()
        {
            legRigth = new Line { StrokeThickness = 4, X1 = 215, Y1 = 260, X2 = 200, Y2 = 270, Stroke = Brushes.White };
            return legRigth;
        }
        public Line LegLeft()
        {
            legLeft = new Line { StrokeThickness = 4, X1 = 215, Y1 = 260, X2 = 230, Y2 = 270, Stroke = Brushes.White };
            return legLeft;
        }

        public Line Rope()
        {

            rope = new Line { StrokeThickness = 4, X1 = 215, Y1 = 200, X2 = 215, Y2 = 170, Stroke = Brushes.White };
            return rope;
        }

        public Line BarTop()
        {

            barTop = new Line { StrokeThickness = 4, X1 = 160, Y1 = 170, X2 = 215, Y2 = 170, Stroke = Brushes.White };
            return barTop;
        }

        public Line BarLeft()
        {

            barLeft = new Line { StrokeThickness = 4, X1 = 160, Y1 = 170, X2 = 160, Y2 = 290, Stroke = Brushes.White };
            return barLeft;
        }

        public Line BarBottom()
        {

            barBottom = new Line { StrokeThickness = 4, X1 = 130, Y1 = 290, X2 = 190, Y2 = 290, Stroke = Brushes.White };
            return barBottom;
        }

        public Line BarHold()
        {

            barHold = new Line { StrokeThickness = 4, X1 = 160, Y1 = 190, X2 = 180, Y2 = 170, Stroke = Brushes.White };
            return barHold;
        }
    }
}
