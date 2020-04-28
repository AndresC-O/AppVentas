﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
                cmbTipDoc.ValueMember = "iDDocumento";
            }
        }

        void RetornoId()
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                var tbVentas = db.tb_venta;

                txtNumVenta.Text = "1";
                foreach (var iterarDatosVentas in tbVentas)
                {
                    int IdVenta = iterarDatosVentas.idVenta;
                    int suma = IdVenta + 1;
                    txtNumVenta.Text = suma.ToString();
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
            

            try
            {
                Calculo();
            }
            catch(Exception ex)
            {

            }

            dtvProductos.Rows.Add(txtCodProd.Text, txtNombrePrd.Text, txtPrecioProd.Text, txtCantidad.Text, txtTotal.Text);

            Double Suma = 0;
            for (int i = 0; i < dtvProductos.RowCount; i++)
            {
                String DatosAOperar = dtvProductos.Rows[i].Cells[4].Value.ToString();
                Double DatosCovertidos = Convert.ToDouble(DatosAOperar);

                Suma += DatosCovertidos;
                txtTotalGeneral.Text = Suma.ToString();
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using(sistemaVentasEntities bd = new sistemaVentasEntities())
            {
                tb_venta venta = new tb_venta();

                String comboDoc = cmbTipDoc.SelectedValue.ToString();
                String comboCli = cmbCliente.SelectedValue.ToString();

                venta.idDocumento = Convert.ToInt32(comboDoc);
                venta.iDCliente = Convert.ToInt32(comboCli);
                venta.iDUsuario = 8;
                venta.totalVenta = Convert.ToDecimal(txtTotalGeneral.Text);
                venta.fecha = Convert.ToDateTime(dtpFecha.Text);

                bd.tb_venta.Add(venta);
                bd.SaveChanges();

                detalleVenta dventa = new detalleVenta();

                for (int i = 0; i < dtvProductos.RowCount; i++)
                {
                    String idProducto = dtvProductos.Rows[i].Cells[0].Value.ToString();
                    int idProdConvertido = Convert.ToInt32(idProducto);

                    String Cantidad = dtvProductos.Rows[i].Cells[3].Value.ToString();
                    int cantidadConver = Convert.ToInt32(Cantidad);

                    String Precio = dtvProductos.Rows[i].Cells[2].Value.ToString();
                    Decimal precioConver = Convert.ToDecimal(Precio);

                    String Total = dtvProductos.Rows[i].Cells[4].Value.ToString();
                    Decimal totalConver = Convert.ToDecimal(Total);

                    dventa.idVenta = Convert.ToInt32(txtNumVenta.Text);
                    dventa.idProducto = idProdConvertido;
                    dventa.cantidad = cantidadConver;
                    dventa.precio = precioConver;
                    dventa.total = totalConver;

                    bd.detalleVenta.Add(dventa);
                    bd.SaveChanges();
                }
            }
        }
    }
}
