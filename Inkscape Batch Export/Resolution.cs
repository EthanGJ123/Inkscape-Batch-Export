using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inkscape_Batch_Export
{
    internal class Resolution
    {
        public int width;
        public int height;
        public string wxh
        {
            get { return width + "x" + height; }
        }

        public Resolution(string resValue)
        {
            string[] resValues = resValue.Split(',');
            width = int.Parse(resValues[0]);
            height = int.Parse(resValues[1]);
        }
    }
}
