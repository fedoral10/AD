using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ADSI
{
    public partial class dataGridViewFiltrado : DataGridView
    {
        public dataGridViewFiltrado()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public void setFiltro(string columna, string filtro)
        {
            string rowFilter = string.Format("[{0}] LIKE '%{1}%'", columna, filtro);
            (this.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
        }

        public void setFiltro(string filtro)
        {
            string rowFilter = "";
            if (this.Columns.Count >= 0)
            {
                foreach (DataGridViewColumn columna in this.Columns)
                {

                    rowFilter += string.Format("[{0}] LIKE '%{1}%' OR ", columna.DataPropertyName, filtro);
                    Console.WriteLine(columna.DataPropertyName);
                }
                rowFilter = rowFilter.Substring(0, rowFilter.Length - 3);
                (this.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }
        }
        public void clearFiltro()
        {
            (this.DataSource as DataTable).DefaultView.RowFilter = null;
        }
    }
}
