using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SmartShareDataAccessLayer;
using System.IO;
using System.Net;
using SmartShareCommon;
using System.Diagnostics;
using System.Net.Sockets;

namespace SmartShareCoreBusinessLayer
{
    public static class CoreBusinessManager
    {
        public static String GetSystemIPAddress()
        {
            string hostName = GetSystemName(); // Retrive the Name of HOST  
            // Get the IP  
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }

        public static String GetSystemName()
        {
            return Dns.GetHostName(); // Retrive the Name of HOST
        }

        public static void AddSharedDirectory(String sharedPath)
        {
            String directory = DataAccess.GetSharedDirectory();

            if (String.IsNullOrEmpty(directory))
                DataAccess.SaveSharedDirectory(sharedPath);
            else
                DataAccess.UpdateSharedDirectory(sharedPath);
        }

        public static void AddClient(Client client)
        {
            DataAccess.SaveClient(client);
        }

        public static string GetSharedDirectory()
        {
            String sharedDirectory = DataAccess.GetSharedDirectory();

            if (String.IsNullOrEmpty(sharedDirectory))
                return null;

            //DirectoryInfo di = new DirectoryInfo(sharedDirectory);
            //if (di != null && di.Exists)
            //if(Directory.Exists(sharedDirectory))
            //{
            return sharedDirectory;
            //}
            //else
            //{
            //    return null;
            //}
        }

        public static List<Client> GetClients()
        {
            return DataAccess.GetSavedClients();
        }

        private static void MapNetworkDrive(String remoteDirectory)
        {
            try
            {
        //        System.Diagnostics.Process.Start("net.exe",
        //@"use K: " + remoteDirectory).
        //    WaitForExit();


                // Map Network drive
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();

                // Notes:
                //      Use /C To carry out the command specified by string and then terminates
                //      You can omit the passord or username and password
                //      Use /PERSISTENT:YES to keep the mapping when the machine is restarted
                psi.FileName = "cmd.exe";
                psi.Arguments = ""
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.StartInfo = psi;

                process.Start();
              
                process.WaitForExit();

                //// Map Network drive
                //System.Diagnostics.Process process = new System.Diagnostics.Process();
                //System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                //psi.CreateNoWindow = true;
                //psi.WindowStyle = ProcessWindowStyle.Hidden;
                //psi.FileName = "net.exe";
                //psi.Arguments = @"use X: " + Path.GetDirectoryName(remoteDirectory);
                //psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                //process.StartInfo = psi;
                //process.Start();
                //process.WaitForExit();

                DriveInfo[] d = null;
                Boolean value = false;

                while (!value)
                {
                    d = DriveInfo.GetDrives();
                    foreach (DriveInfo di in d)
                    {
                        if (di.Name.Contains("X:"))
                            value = true;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        private static void RemoveMappedNetworkDrive()
        {
            try
            {
                System.Diagnostics.Process.Start("net.exe",
        @"use K: /delete").
            WaitForExit();
            }
            catch (Exception e)
            {
            }
        }

        public static void ShareFiles(object files)
        {
            try
            {
                String[] fileNames = files as String[];
                Client c = GlobalDataInstances.GetInstance().SelectedClient;
                if (c == null)
                    return;
                String remotePath = "\\" + c.SystemName + "\\" + c.ClientSharedPath;
                //MapNetworkDrive(remotePath);
                //RemoveMappedNetworkDrive();
                File.Copy(fileNames[0], remotePath);
            }
            catch (Exception e)
            {

            }
        }

        public static void CloseDBConnection()
        {
            DataBaseConnection.GetDbInstance().CloseDBConnection();
        }

       
    }
}
