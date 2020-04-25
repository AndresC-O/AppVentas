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

namespace proVentas.Vista
{
    public partial class FrmClientes : Form
    {
        public FrmClientes()
        {
            InitializeComponent();
        }

        tb_cliente cliente = new tb_cliente();

        void CargarDatos()
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                var tbClientes = db.tb_cliente;

                foreach (var iterarDatosClientes in tbClientes)
                {

                    dtvClientes.Rows.Add(iterarDatosClientes.iDCliente, iterarDatosClientes.nombreCliente, iterarDatosClientes.direccionCliente, iterarDatosClientes.duiCliente);
                }
            }
        }

        void LimpiarDatos()
        {
            txtCliente.Text = "";
            txtDireccion.Text = "";
            txtDui.Text = "";
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
            LimpiarDatos();

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCliente.Equals("") || txtDireccion.Equals("") || txtDui.Equals(""))
            {
                using (sistemaVentasEntities db = new sistemaVentasEntities())
                {
                    cliente.nombreCliente = txtCliente.Text;
                    cliente.direccionCliente = txtDireccion.Text;
                    cliente.duiCliente = txtDui.Text;

                    db.tb_cliente.Add(cliente);
                    db.SaveChanges();
                }

                dtvClientes.Rows.Clear();
                CargarDatos();
                LimpiarDatos();
            }
            else
            {
                MessageBox.Show("Introduzca Valores primero.");
            }

        }

        private void dtvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String nombreCliente = dtvClientes.CurrentRow.Cells[1].Value.ToString();
            String direccion = dtvClientes.CurrentRow.Cells[2].Value.ToString();
            String dui = dtvClientes.CurrentRow.Cells[3].Value.ToString();

            txtCliente.Text = nombreCliente;
            txtDireccion.Text = direccion;
            txtDui.Text = dui;


            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvClientes.CurrentRow.Cells[0].Value.ToString();

                int idC = int.Parse(id);

                cliente = db.tb_cliente.Where(VerificarID => VerificarID.iDCliente == idC).First();
                cliente.nombreCliente = txtCliente.Text;
                cliente.direccionCliente = txtDireccion.Text;
                cliente.duiCliente = txtDui.Text;

                db.Entry(cliente).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            dtvClientes.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvClientes.CurrentRow.Cells[0].Value.ToString();

                cliente = db.tb_cliente.Find(int.Parse(id));
                db.tb_cliente.Remove(cliente);
                db.SaveChanges();
            }

            dtvClientes.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtCliente.Text = "";
            txtDireccion.Text = "";
            txtDui.Text = "";
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }
    }
}
