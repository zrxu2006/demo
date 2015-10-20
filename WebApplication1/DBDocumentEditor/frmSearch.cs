using DBDocumentEditor.Domain;
using DBDocumentEditor.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBFieldEditor
{
    public partial class frmSearch : Form
    {
        IDBDocRepository _documentRepo;
        public frmSearch()
        {            
            InitializeComponent();
            LoadTableList();
        }

        private void LoadTableList()
        {
            lbTableList.DisplayMember = "Name";
            lbTableList.ValueMember = "Name";
            lbTableList.DataSource = DocumentFactory.CreateRepository(string.Empty).Tables;

            lbTableList.SelectedItem = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string tableName=txtTableName.Text.Trim();
            dataGridView1.Visible = false;
            if (string.IsNullOrEmpty(tableName))
            {
                dataGridView1.DataSource = null;
                //MessageBox.Show("请输入表名！");
                return;
            }
            
            _documentRepo = DocumentFactory.CreateRepository(tableName);
            var dataSource = _documentRepo.Documents;

            dataGridView1.Visible = dataSource.Count >= 0;
         
            dataGridView1.DataSource = _documentRepo.Documents;
            dataGridView1.Columns["ObjectId"].Visible = false;
            //dataGridView1.Columns["Description"].
        }

        private void txtTableName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                btnSearch_Click(sender, e);
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //string msg = string.Format("{0}|{1}|{2}",
            //dataGridView1.CurrentRow.Cells["TableName"].Value.ToString(),
            //dataGridView1.CurrentRow.Cells["FieldName"].Value.ToString(),
            //dataGridView1.CurrentRow.Cells["Description"].Value.ToString());

            _documentRepo.UpdateDescription(dataGridView1.CurrentRow.Cells["FieldName"].Value.ToString(),
                string.Format("{0}",dataGridView1.CurrentRow.Cells["Description"].Value));
            //_documentRepo.u
            //MessageBox.Show(msg);
        }

        private void lbTableList_Click(object sender, EventArgs e)
        {
            txtTableName.Text = string.Format("{0}", lbTableList.SelectedValue);
            
            btnSearch_Click(sender, e);
        }

        private void lbTableList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbTableList_Click(sender, e);
        }        
    }
}
