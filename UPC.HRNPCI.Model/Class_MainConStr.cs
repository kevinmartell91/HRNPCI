using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using UPC.HRNPCI.Model.ConfiguracionModel;

namespace UPC.HRNPCI.Model
{
    public static class Class_MainConStr
    {
        public static String UDF_MainCnStr()
        {
            string res = "NO se realizó el backup";
            if (InternetChecker.IsConnectedToInternet())
            {

                //InternetChecker.UploadFileToFTP("D:\\RutasBackupLocal\\kevin.bak");
                InternetChecker.UploadFileToFTP(System.IO.Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "HRNPCIData.sdf"));

                //WebRequestGetExample.Main();
                res = "Se realizó el backup con existo";
            }
            return res;
        }
    }

    public class InternetChecker
    {
        [System.Runtime.InteropServices.DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        //Creating a function that uses the API function...
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        public static void UploadFileToFTP(string source)
        {
            String ftpurl = "ftp://ftp.smarterasp.net/db/buckupFiles/HRNPCIData.sdf"; // e.g. ftp://serverip/foldername/foldername
            String ftpusername = "kevimartell91-001"; // e.g. username
            String ftppassword = "o0I9u8Y7"; // e.g. password

            try
            {
                string filename = Path.GetFileName(source);
                string ftpfullpath = ftpurl;
                FtpWebRequest ftp = (FtpWebRequest)FtpWebRequest.Create(ftpfullpath);
                ftp.Credentials = new NetworkCredential(ftpusername, ftppassword);
 
                ftp.KeepAlive = true;
                ftp.UseBinary = true;
                ftp.Method = WebRequestMethods.Ftp.UploadFile;
                
                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
    
                Stream ftpstream = ftp.GetRequestStream();
                ftpstream.Write(buffer, 0, buffer.Length);
                ftpstream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    //public class WebRequestGetExample
    //{
    //    public static void Main()
    //    {
    //        try
    //        {
    //             // Get the object used to communicate with the server.
    //            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.smarterasp.net/db/buckupFiles/kevin.bak");//" ftp://208.118.63.139/ /"
    //        request.Method = WebRequestMethods.Ftp.UploadFile;

    //        // This example assumes the FTP site uses anonymous logon.
    //        request.Credentials = new NetworkCredential("kevimartell91-001", "o0I9u8Y7");

    //        // Copy the contents of the file to the request stream.
    //        StreamReader sourceStream = new StreamReader("D:\\RutasBackupLocal\\kevin.bak");
    //        byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
    //        sourceStream.Close();
    //        request.ContentLength = fileContents.Length;

    //        Stream requestStream = request.GetRequestStream();
    //        requestStream.Write(fileContents, 0, fileContents.Length);
    //        requestStream.Close();

    //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();

    //        Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

    //        response.Close();
    //        }
    //        catch (Exception ex)
    //        {
                
    //            throw ex;
    //        }
    //    }
    //}
   
}
