﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;

namespace MtnFrameGrabProvider {
    public class ThumbCreator {
        const int MAX_PATH = 255;

        // We need our temp path as a short path due to a bug in mtn 
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern int GetShortPathName(
            [MarshalAs(UnmanagedType.LPTStr)]
             string path,
            [MarshalAs(UnmanagedType.LPTStr)]
             StringBuilder shortPath,
            int shortPathLength
            );

         
        public static bool CreateThumb(string video, ref string destination, int secondsFromStart) {

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Plugin.MtnExe;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            // see: http://moviethumbnail.sourceforge.net/usage.en.html
            psi.Arguments = string.Format("\"{0}\" -P -c 1 -r 1 -B {1} -C 2 -i -t -z -D 8 -o .jpg -O {2}",
                video, secondsFromStart, GetShortPath(Path.GetTempPath()));
            var process = Process.Start(psi);
            process.WaitForExit();

            destination = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(video) + ".jpg");
            if (process.ExitCode == 0)
                return true;
            else
                return false;
        }

        private static string GetShortPath(string path) {
            var shortPath = new StringBuilder(MAX_PATH);
            GetShortPathName(path, shortPath, MAX_PATH);
            return shortPath.ToString();
        }
    }
}
