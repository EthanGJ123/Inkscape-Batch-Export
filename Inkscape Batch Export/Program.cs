using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Svg;

namespace Inkscape_Batch_Export
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            string[] svgFiles = getFiles("Select Inkscape File(s)", "Inkscape SVG Files(*.svg) | *.svg", true);
            string resFile = getFiles("Select Resolutions File", "Text Files(*.txt) | *.txt", false)[0];

            List<Resolution> resolutions = new List<Resolution>();

            using(StreamReader reader = new StreamReader(resFile))
            {
                while(!reader.EndOfStream)
                {
                    resolutions.Add(new Resolution(reader.ReadLine()));
                }
            }

            FileInfo svgFileInfo;

            foreach(string svgFile in svgFiles)
            {
                svgFileInfo = new FileInfo(svgFile);
                foreach (Resolution res in resolutions)
                {
                    SvgDocument svgDocument = SvgDocument.Open<SvgDocument>(svgFile);
                    Bitmap svgBmp = svgDocument.Draw(res.width, res.height);
                    svgBmp.Save(svgFileInfo.DirectoryName + "\\" + res.wxh + "\\" + svgFileInfo.Name.Replace(".svg", "") + " " + res.wxh + ".png");
                }
            }

            
        }

        static string[] getFiles(string title, string filter, bool multiselect, string initialDirectory = null)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = filter,
                Multiselect = multiselect,
                InitialDirectory = initialDirectory
            };
            switch (openFileDialog.ShowDialog())
            {
                case DialogResult.OK:
                    return openFileDialog.FileNames;
                default:
                    Application.Exit();
                    break;
            }
            return null;
        }
    }
}
