using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Threading;

namespace WinFormsUI
{
    class Images
    {
        public Int32 ImageID;
        public string ImageDir = "C:\\Temp\\TempCoinImages\\";
        private Type matlabtype;
        private object matlab;

        public Images()
        {
            //Get the type info
            matlabtype = Type.GetTypeFromProgID("matlab.application");
            //Create an instance of MATLAB
            matlab = Activator.CreateInstance(matlabtype);
        }


        public void SetImageID()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(ImageDir);
            if (dirInfo.GetFiles().Length == 0)
            {
                ImageID = 100001;
            }
            else
            {
                string[] Files = null;
                Files = System.IO.Directory.GetFiles(ImageDir);
                Array.Sort(Files);
                string lastFileName = Files[Files.Length - 1];

                string imageNum = lastFileName.Substring(ImageDir.Length, 6);

                int result;
                if (int.TryParse(imageNum, out result))
                {
                    ImageID = Convert.ToInt32(imageNum);
                }
                else
                {
                    ImageID = 100001;
                }

            }
        }

        public void CallMatlab(string fullImageName)
        {
            StringBuilder matlabCommand = new StringBuilder();
            if (!File.Exists(fullImageName))
            {
                string message = "File Not Found";
                string caption = "Error Detected in Input";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);


            }
            FileInfo fi = new FileInfo("CoinCrop.m");
            matlabCommand.AppendLine("imageName = '" + fullImageName + "';");
            matlabCommand.AppendLine("run('" + fi.FullName + "');");

            //Call in the same Thread:
            //CallMatlabScript(matlabCommand);
            Action cm = () => CallMatlabScript(matlabCommand);
            Task.Run(cm);

        }

        private void CallMatlabScript(StringBuilder matlabScript)
        {
            object[] arrayInput = new Object[] { matlabScript.ToString() };
            Random rnd = new Random();
            int dice = rnd.Next(1);
            if (dice == 0)
            {
                matlabtype.InvokeMember("Execute", BindingFlags.InvokeMethod, null, matlab, arrayInput);
            }
        }

    }
}
