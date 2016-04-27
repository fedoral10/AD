using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADSI
{
    class commons
    {

        public static DateTime convertLDAPDateToDateTime(string value) 
        {
            if (!String.IsNullOrEmpty(value))
            {
                DateTime dt = DateTime.FromFileTime(long.Parse(value));

                return dt;
            }
            return new DateTime();
        }

        public static DateTime convertLDAPWhenCreateToDateTime(string value)
        {
            string format = "yyyyMMddHHmmss.0Z";
            DateTime dt = DateTime.ParseExact(value, format, System.Globalization.CultureInfo.InvariantCulture);

            return dt;
        }

        public static string allProperties(ResultPropertyCollection prop, string nombre)
        {
            string salida = null;
            if (prop[nombre].Count > 1)
            {
                foreach (object s in prop[nombre])
                {
                    salida = salida + s.ToString() + ",";
                }
                salida=salida.Substring(0, salida.Length - 1);
            }
            else
            {
                salida = prop[nombre][0].ToString();
            }
            return salida;
        }

        public static string getSaveFile(string filter)
        {
            string arch=null;
            SaveFileDialog sfd = new SaveFileDialog();

            //"txt files (*.txt)|*.txt|All files (*.*)|*.*" ;

            sfd.Filter = filter;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                arch = sfd.FileName;
            }
            return arch;
        }

        public static string getOpenFile(string filter)
        {
            string arch = null;
            OpenFileDialog sfd = new OpenFileDialog();

            //"txt files (*.txt)|*.txt|All files (*.*)|*.*" ;

            sfd.Filter = filter;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                arch = sfd.FileName;
            }
            return arch;
        }

        public static void showMessageBoxInformation(string titulo, string mensaje)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void showMessageBoxError(string titulo, string mensaje)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
}
