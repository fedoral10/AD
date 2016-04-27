using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ADSI.Forms.Usuario
{
    public partial class frmUsuarios : System.Windows.Forms.Form
    {

        private string[] _columnas ={
                                "sAMAccountName",
                                "displayName",
                                "description",
                                "department",
                                "mail",
                                "telephoneNumber",
                                "physicalDeliveryOfficeName",
                                "lastLogoff",
                                "lastLogon",
                                "lastLogonTimestamp",
                                "company",
                                "whenChanged",
                                "whenCreated",
                                "pwdLastSet",
                                "title",
                                "givenname",
                                "sn",
                                "cn",
                                "objectclass",
                                "distinguishedname"
                                  };

        private DataTable _dataTable;

        public frmUsuarios()
        {
            InitializeComponent();
            _dataTable = new DataTable();
            foreach (string s in AD.ADScopes())
            {
                cmbADScope.Items.Add(s);
                chkList.Items.Add(s);
            }
            foreach (string s in AD.domainControllers())
            {
                cmbDomainController.Items.Add(s);
            }
            cmbDomainController.SelectedIndex = cmbDomainController.Items.Count - 1;
            cmbADScope.SelectedIndex = cmbADScope.Items.Count - 1;

            foreach (string col in _columnas)
            {
                /* var columna = new DataGridViewColumn();
                 columna.Name = col;
                 dgvUsuarios.Columns.Add(columna);*/
                _dataTable.Columns.Add(col);

            }
            dgvUsuarios.DataSource = _dataTable;
        }

        private void actualizarGrid()
        {
            _dataTable.Rows.Clear();
            string[] arr = cmbADScope.SelectedItem.ToString().Split('.');
            string root = "";
            foreach (String s in arr)
            {
                root = root + "DC=" + s + ",";
            }
            root = root.Substring(0, root.Length - 1);

            SearchResultCollection sr = AD.queryAD("(&(sAMAccountName=*)(objectClass=user)(!(objectClass=computer))(!(objectClass=organizationalUnit)))", root, System.DirectoryServices.SearchScope.Subtree,_columnas);

            foreach (SearchResult s in sr)
            {
                //Console.WriteLine(s.Properties["samaccountname"][0]);
                _dataTable.Rows.Add();
                foreach (string col in _columnas)
                {
                    _dataTable.Rows[_dataTable.Rows.Count - 1][col] = s.Properties[col].Count > 0 ? commons.allProperties(s.Properties,col) : null;
                }
                
            }
            dgvUsuarios.DataSource = _dataTable;
            if (!String.IsNullOrEmpty(txtFiltro.Text))
            {
                dgvUsuarios.setFiltro(txtFiltro.Text);
            }
        }

        private void actualizarGridChk()
        {
            _dataTable.Rows.Clear();

            List<string> lista_scope = chkList.CheckedItems.Cast<string>().ToList();

            foreach(string scope in lista_scope)
            { 
                string[] arr = scope.Split('.');
                string root = "";
                foreach (String s in arr)
                {
                    root = root + "DC=" + s + ",";
                }
                root = root.Substring(0, root.Length - 1);

                SearchResultCollection sr = AD.queryAD("(&(sAMAccountName=*)(objectClass=user)(!(objectClass=computer))(!(objectClass=organizationalUnit)))", root, System.DirectoryServices.SearchScope.Subtree, _columnas);

                foreach (SearchResult s in sr)
                {
                    //Console.WriteLine(s.Properties["samaccountname"][0]);
                    _dataTable.Rows.Add();
                    foreach (string col in _columnas)
                    {
                        _dataTable.Rows[_dataTable.Rows.Count - 1][col] = s.Properties[col].Count > 0 ? commons.allProperties(s.Properties, col) : null;
                    }

                }
            }
            dgvUsuarios.DataSource = _dataTable;
            if (!String.IsNullOrEmpty(txtFiltro.Text))
            {
                dgvUsuarios.setFiltro(txtFiltro.Text);
            }
        }
        /*private BackgroundWorker worker;
        
        private void actulizarGrid(string[] dc)
        {
            _dataTable.Rows.Clear();
            string[] arr = dc;
            string root = "";
            foreach (String s in arr)
            {
                root = root + "DC=" + s + ",";
            }
            root = root.Substring(0, root.Length - 1);

            SearchResultCollection sr = AD.queryAD("(&(sAMAccountName=*)(objectClass=user)(!(objectClass=computer))(!(objectClass=organizationalUnit)))", root, System.DirectoryServices.SearchScope.Subtree, _columnas);
            
            int i = 0;
            if (sbProgressBar.GetCurrentParent().InvokeRequired)
            {
                sbProgressBar.GetCurrentParent().Invoke(new MethodInvoker(delegate { this.sbProgressBar.Maximum = sr.Count; }));
            }
            
            foreach (SearchResult s in sr)
            {
                //Console.WriteLine(s.Properties["samaccountname"][0]);
                _dataTable.Rows.Add();
                foreach (string col in _columnas)
                {
                    _dataTable.Rows[_dataTable.Rows.Count - 1][col] = s.Properties[col].Count > 0 ? commons.allProperties(s.Properties, col) : null;
                }
                Thread.Sleep(100);
                this.worker.ReportProgress(i);
                i++;
            }
           // dgvUsuarios.DataSource = _dataTable;
            //if (!String.IsNullOrEmpty(txtFiltro.Text))
            //{
            //    dgvUsuarios.setFiltro(txtFiltro.Text);
            //}
        }

        

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i <= 100; i++)
            {
                // Wait 100 milliseconds.
                // Thread.Sleep(100);
                // Report progress.
                
            }
            
            
            actulizarGrid((string[])e.Argument);
            
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            if (this.sbProgressBar.GetCurrentParent().InvokeRequired)
            {
                this.sbProgressBar.GetCurrentParent().Invoke(new MethodInvoker(delegate { this.sbProgressBar.Value = e.ProgressPercentage; }));
                // Set the text.
                //this.StatusBar.Parent.Invoke(new MethodInvoker(delegate { this.setStatusBarText(e.ProgressPercentage.ToString()); }));
                this.StatusBar.Refresh();
            }
            
        }

        private void worker_finalizacion(object sender, RunWorkerCompletedEventArgs e)
        { 
        }
*/
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            /*worker = new BackgroundWorker();
            
            worker.DoWork += new DoWorkEventHandler(this.worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(this.worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.worker_finalizacion);
            worker.WorkerReportsProgress = true;

            worker.RunWorkerAsync(cmbADScope.SelectedItem.ToString().Split('.'));
            */
            try
            {
                if (chkList.CheckedItems.Count > 0)
                {
                    actualizarGridChk();
                }
                else
                {
                    actualizarGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo acceder al AD\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("pwdLastSet: " + dgvUsuarios.SelectedRows[0].Cells["pwdLastSet"].Value);
            DateTime dt=  DateTime.FromFileTime(long.Parse(dgvUsuarios.SelectedRows[0].Cells["pwdLastSet"].Value.ToString()));
            Console.WriteLine(dt);
            Console.WriteLine();
            Console.WriteLine("lastLogon: " + dgvUsuarios.SelectedRows[0].Cells["lastLogon"].Value);
            DateTime d = DateTime.FromFileTime(long.Parse(dgvUsuarios.SelectedRows[0].Cells["lastLogon"].Value.ToString()));
            Console.WriteLine(d);
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            Reportes.frmReporte frm = new Reportes.frmReporte(_dataTable);
            frm.exportXLS();
            //frm.Show();
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            dgvUsuarios.setFiltro(txtFiltro.Text);
        }

        
    }
}
