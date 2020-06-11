using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proVentas.Model;
using proVentas.Reports;

namespace proVentas.ConsultasRpt
{
    public partial class FrmRptProductos : Form
    {
        public FrmRptProductos()
        {
            InitializeComponent();
        }

        private void FrmRptProductos_Load(object sender, EventArgs e)
        {
            using(sistemaVentasEntities bd = new sistemaVentasEntities())
            {
                rptProductos rptPr = new rptProductos();
                rptPr.SetDataSource(bd.producto.ToList());

                crProductos.ReportSource = rptPr;
            }
        }
    }
}
