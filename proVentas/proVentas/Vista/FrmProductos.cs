using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using proVentas.ConsultasRpt;
using proVentas.Model;

namespace proVentas.Vista
{
    public partial class FrmProductos : Form
    {
        public FrmProductos()
        {
            InitializeComponent();
        }

        producto prod = new producto();

        void CargarDatos()
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                var tbProductos = db.producto;

                foreach (var iterarDatosProductos in tbProductos)
                {

                    dtvProductos.Rows.Add(iterarDatosProductos.idProducto, iterarDatosProductos.nombreProducto, iterarDatosProductos.precioProducto, iterarDatosProductos.estadoProducto);
                }
            }
        }

        void LimpiarDatos()
        {
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtEstado.Text = "";
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            CargarDatos();

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtProducto.Equals("") || txtPrecio.Equals("") || txtEstado.Equals(""))
            {
                MessageBox.Show("Introduzca Valores primero.");
            }
            else
            {
                using (sistemaVentasEntities db = new sistemaVentasEntities())
                {
                    prod.nombreProducto = txtProducto.Text;
                    prod.precioProducto = txtPrecio.Text;
                    prod.estadoProducto = txtEstado.Text;

                    db.producto.Add(prod);
                    db.SaveChanges();
                }

                dtvProductos.Rows.Clear();
                CargarDatos();
                LimpiarDatos();
            }
        }

        private void dtvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String nombreProd = dtvProductos.CurrentRow.Cells[1].Value.ToString();
            String precio = dtvProductos.CurrentRow.Cells[2].Value.ToString();
            String estado = dtvProductos.CurrentRow.Cells[3].Value.ToString();

            txtProducto.Text = nombreProd;
            txtPrecio.Text = precio;
            txtEstado.Text = estado;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvProductos.CurrentRow.Cells[0].Value.ToString();

                int idC = int.Parse(id);

                prod = db.producto.Where(VerificarID => VerificarID.idProducto == idC).First();
                prod.nombreProducto = txtProducto.Text;
                prod.precioProducto = txtPrecio.Text;
                prod.estadoProducto = txtEstado.Text;

                db.Entry(prod).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            dtvProductos.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvProductos.CurrentRow.Cells[0].Value.ToString();

                prod = db.producto.Find(int.Parse(id));
                db.producto.Remove(prod);
                db.SaveChanges();
            }

            dtvProductos.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtProducto.Text = "";
            txtPrecio.Text = "";
            txtEstado.Text = "";
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }

        private void btnInforme_Click(object sender, EventArgs e)
        {
            FrmRptProductos report = new FrmRptProductos();
            report.Show();
        }
    }
}
