using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NormalTest
{
    public partial class Test : Form
    {
        int rot = 0;

        public Test()
        {
            InitializeComponent();
            label1.AutoSize = true;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (0x0112):
                    if (m.WParam.ToInt32() == 0xF060)
                    {
                        Console.WriteLine("Denied AltF4.");
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    else Console.WriteLine("Unhandled SYSCOMMAND: 0x{0:X}", m.WParam.ToInt32());
                    break;
                case (0x0200):
                    label1.Text = "Mouse Pos: " + UnPack(m.LParam.ToInt32());
                    break;
                case (0x020A):
                    int wParam = m.WParam.ToInt32();
                    rot += (wParam >> 16) / 120;

                    Console.WriteLine("Rotation: {0}", rot);
                    break;
                case(0x0100):
                    Console.WriteLine("Key: {0}", m.WParam.ToInt32() & 0xFFFF);
                    break;
            }
            base.WndProc(ref m);
        }

        private Point UnPack(int lParam)
        {
            int low = lParam >> 16;
            int high = lParam & 0xFFFF;

            return new Point(high, low);
        }
    }
}
