using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proVentas.Vista
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        //private void AbrirFormulario(object formHijo)
        //{
        //    if(this.panelContenedor.Controls.Count > 0)
        //    {
        //        this.panelContenedor.Controls.RemoveAt(0);
        //        Form fh = formHijo as Form;
        //        fh.TopLevel = false;
        //        fh.Dock = DockStyle.Fill;
        //        this.panelContenedor.Controls.Add(fh);
        //        this.panelContenedor.Tag = fh;
        //        fh.Show();
        //    }
        //}

        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoles r = new frmRoles();
            r.MdiParent = this;
            r.Show();

            //AbrirFormulario(new frmRoles());
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuarios usu = new frmUsuarios();
            usu.MdiParent = this;
            usu.Show();

            //AbrirFormulario(new frmUsuarios());
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Estas seguro que deseas salir?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if(result == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        public static FrmVentas ventas = new FrmVentas();

        private void controlVentasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ventas.MdiParent = this;
            ventas.Show();
            //AbrirFormulario(new FrmVentas());
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClientes clientes = new FrmClientes();
            clientes.MdiParent = this;
            clientes.Show();
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductos prod = new FrmProductos();
            prod.MdiParent = this;
            prod.Show();
        }

        private void documentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDocumentos doc = new FrmDocumentos();
            doc.MdiParent = this;
            doc.Show();
        }
    }
}
