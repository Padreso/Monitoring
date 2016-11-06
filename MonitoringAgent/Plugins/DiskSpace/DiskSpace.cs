﻿using System;
using System.Collections.Generic;
using System.IO;

namespace DiskSpaceInfo
{
    public class DiskSpace
    {
        public Dictionary<string, string> Output()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            foreach (DriveInfo d in allDrives)
            {
                /*Console.WriteLine("Drive {0}", d.Name);
                Console.WriteLine("     Drive Type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    Console.WriteLine("  Volume label: {0}", d.VolumeLabel);
                    Console.WriteLine("  File system: {0}", d.DriveFormat);
                    Console.WriteLine(
                        "  Available space to current user:{0, 15} bytes",
                        d.AvailableFreeSpace);

                    Console.WriteLine(
                        "  Total available space:          {0, 15} bytes",
                        d.TotalFreeSpace);

                    Console.WriteLine(
                        "  Total size of drive:            {0, 15} bytes ",
                        d.TotalSize);
                }       */

                res.Add(d.Name, d.VolumeLabel);
            }

            return res;
        }
    }
}