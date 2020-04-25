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
using proVentas.Vista.FormsVentas;

namespace proVentas.Vista
{
    public partial class FrmVentas : Form
    {
        public FrmVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            RetornoId();
            CargarCombo();
        }

        void CargarCombo()
        {
            using(sistemaVentasEntities bd = new sistemaVentasEntities())
            {
                var clientes = bd.tb_cliente.ToList();
                cmbCliente.DataSource = clientes;
                cmbCliente.DisplayMember = "nombreCliente";
                cmbCliente.ValueMember = "iDCliente";

                var tipoDocumento = bd.tb_documento.ToList();
                cmbTipDoc.DataSource = tipoDocumento;
                cmbTipDoc.DisplayMember = "nombreDocumento";
                cmbTipDoc.ValueMember = "nombreDocumento";
            }
        }

        void RetornoId()
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                var tbVentas = db.tb_venta;

                foreach (var iterarDatosVentas in tbVentas)
                {
                    txtNumVenta.Text = iterarDatosVentas.idVenta.ToString();
                    //dtvUsuarios.Rows.Add(iterarDatosUsuarios.id, iterarDatosUsuarios.email, iterarDatosUsuarios.contrasenia);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FrmBusqueda browse = new FrmBusqueda();
            browse.Show();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            dtvProductos.Rows.Add(txtCodProd.Text, txtNombrePrd.Text, txtPrecioProd.Text, txtCantidad.Text, txtTotal.Text);


            try
            {
                Calculo();

            }
            catch(Exception ex)
            {

            }
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            Calculo();
        }

        void Calculo()
        {
            try
            {
                Double precioProd;
                Double cantidad;
                Double total;

                precioProd = Double.Parse(txtPrecioProd.Text);
                cantidad = Double.Parse(txtCantidad.Text);

                total = (precioProd * cantidad);
                txtTotal.Text = total.ToString();
            }
            catch (Exception ex)
            {
                txtCantidad.Text = "0";
                MessageBox.Show("No puedes ingresar cantidades menores a 0");
                txtCantidad.Select();
            }
        }
    }
}
