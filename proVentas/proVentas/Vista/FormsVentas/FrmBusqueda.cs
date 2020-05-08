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

namespace proVentas.Vista.FormsVentas
{
    public partial class FrmBusqueda : Form
    {
        public FrmBusqueda()
        {
            InitializeComponent();
        }

        private void FrmBusqueda_Load(object sender, EventArgs e)
        {
            Filtro();
        }

        void Filtro()
        {
            using (sistemaVentasEntities bd = new sistemaVentasEntities())
            {
                String nombre = txtBusqueda.Text;

                var BuscarPrd = from tbProd in bd.producto
                                where tbProd.nombreProducto.Contains(nombre)

                                select new
                                {
                                    Id = tbProd.idProducto,
                                    Nombre = tbProd.nombreProducto,
                                    Precio = tbProd.precioProducto
                                };

                //foreach(var iterar in BuscarPrd)
                //{
                //    dtvProductos.Rows.Add(iterar.Id, iterar.Nombre, iterar.Precio);
                //}
                dtvProductos.DataSource = BuscarPrd.ToList();
            }
        }

        void Envio()
        {
            String id = dtvProductos.CurrentRow.Cells[0].Value.ToString();
            String Nombre = dtvProductos.CurrentRow.Cells[1].Value.ToString();
            String Precio = dtvProductos.CurrentRow.Cells[2].Value.ToString();

            frmMenu.ventas.txtCodProd.Text = id;
            frmMenu.ventas.txtNombrePrd.Text = Nombre;
            frmMenu.ventas.txtPrecioProd.Text = Precio;

            frmMenu.ventas.txtCantidad.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtro();
        }

        private void dtvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Envio();
        }

        private void dtvProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Envio();
                this.Close();
            }
        }
    }
}
