using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TpProg
{
    public partial class Reporte : Form
    {
        public Reporte()
        {
            InitializeComponent();
        }

        private void Reporte_Load(object sender, EventArgs e)
        {
            AccesoDatos datos = new AccesoDatos("Data Source = MARCOS; Initial Catalog = bd_utn_programacion; Integrated Security = True");
            CrystalReport1 rpt = new CrystalReport1();
            string sql;
            DataTable dt = new DataTable();
            sql = "select d.legajo, p.nombre, p.apellido, td.descripcion, p.num_documento, p.telefono, d.fecha_alta from docentes d join personas p on d.id_persona=p.id_persona join tipos_documentos td on td.id_tipo_doc=p.id_tipo_doc";
            dt = datos.consultar(sql);
            rpt.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rpt;
            crystalReportViewer1.Refresh();
            crystalReportViewer1.Show();
        }
    }
}
