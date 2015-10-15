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
        public frmSearch(IDBDocRepository repo)
        {
            _documentRepo = repo;
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _documentRepo.Documents;
            
        }

    }
}
