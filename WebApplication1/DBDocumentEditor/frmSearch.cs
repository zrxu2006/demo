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
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string tableName=txtTableName.Text.Trim();
            dataGridView1.Visible = false;
            if (string.IsNullOrEmpty(tableName))
            {
                MessageBox.Show("请输入表名！");
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
                dataGridView1.CurrentRow.Cells["Description"].Value.ToString());
            //_documentRepo.u
            //MessageBox.Show(msg);
        }
        
    }
}
