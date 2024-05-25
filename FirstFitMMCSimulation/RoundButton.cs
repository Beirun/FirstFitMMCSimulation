using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFitMMCSimulation
{
    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath p = new GraphicsPath();
            p.AddEllipse(2, 2, ClientSize.Width - 4, ClientSize.Height - 4) ;
            this.Region = new Region(p);
            base.OnPaint(e);
        }
    }
}
