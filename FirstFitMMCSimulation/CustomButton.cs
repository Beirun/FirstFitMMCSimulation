using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFitMMCSimulation
{
    internal class CustomButton
    {
        private Thread transitionThread, transitionThread2, labelThread;
        private List<Color> interpolatedColors;
        private List<Color> interpolatedForeColors;
        Button button;
        private Color ForeColor;
        private Color BackColor;
        int i = 0;

        public CustomButton()
        {
            
        }

        public void Button_MouseEnter(object? sender, EventArgs e)
        {
            button = sender as Button;
            if(i == 0)
            {
                i++;
                ForeColor = button.ForeColor;
                BackColor = button.BackColor;
            }
            button.Cursor = Cursors.Hand;
            if (transitionThread2 != null && transitionThread2.IsAlive) transitionThread2.Interrupt();
            if (transitionThread != null && transitionThread.IsAlive) transitionThread.Interrupt();
            button.FlatAppearance.MouseOverBackColor = button.BackColor;
            interpolatedColors = InterpolateColors(button.FlatAppearance.MouseOverBackColor, ForeColor, 20);
            interpolatedForeColors = InterpolateColors(button.ForeColor, BackColor, 20);
            transitionThread = new Thread(TransitionColorToWhite);
            transitionThread2 = new Thread(TransitionForeColor);
            transitionThread.Start();
            transitionThread2.Start();
        }

        public void Button_MouseLeave(object? sender, EventArgs e)
        {
            button = sender as Button;
            
            button.BackColor = button.FlatAppearance.MouseOverBackColor;
            transitionThread.Interrupt();
            transitionThread2.Interrupt();
            interpolatedColors = InterpolateColors(button.BackColor, BackColor, 20);
            interpolatedForeColors = InterpolateColors(button.ForeColor, ForeColor, 20);
            transitionThread = new Thread(TransitionColorToGreen);
            transitionThread2 = new Thread(TransitionForeColor);
            transitionThread.Start();
            transitionThread2.Start();
        }
        private List<Color> InterpolateColors(Color startColor, Color endColor, int steps)
        {
            List<Color> interpolatedColors = new List<Color>();

            float stepR = (endColor.R - startColor.R) / (float)steps;
            float stepG = (endColor.G - startColor.G) / (float)steps;
            float stepB = (endColor.B - startColor.B) / (float)steps;

            for (int i = 0; i <= steps; i++)
            {
                int r = (int)(startColor.R + stepR * i);
                int g = (int)(startColor.G + stepG * i);
                int b = (int)(startColor.B + stepB * i);
                interpolatedColors.Add(Color.FromArgb(r, g, b));
            }

            return interpolatedColors;
        }

        private void TransitionColorToGreen()
        {
            try
            {
                foreach (Color color in interpolatedColors)
                {

                    button.BackColor = color;

                    Thread.Sleep(1);
                }
            }
            catch (Exception) { }
        }

        private void TransitionColorToWhite()
        {
            try
            {
                foreach (Color color in interpolatedColors)
                {

                    button.FlatAppearance.MouseOverBackColor = color;

                    Thread.Sleep(1);
                }
            }
            catch (Exception) { }
        }
        private void TransitionForeColor()
        {
            try
            {
                foreach (Color color in interpolatedForeColors)
                {
                    button.ForeColor = color;
                    button.FlatAppearance.BorderColor = color;
                    Thread.Sleep(1);
                }
            }
            catch (Exception) { }
        }
    }
}
