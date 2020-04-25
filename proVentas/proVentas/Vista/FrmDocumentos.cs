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
    public partial class FrmDocumentos : Form
    {
        public FrmDocumentos()
        {
            InitializeComponent();
        }

        tb_documento document = new tb_documento();

        void CargarDatos()
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                var tbDocumentos = db.tb_documento;

                foreach (var iterarDatosDocs in tbDocumentos)
                {

                    dtvDocumentos.Rows.Add(iterarDatosDocs.iDDocumento, iterarDatosDocs.nombreDocumento);
                }
            }
        }
        void LimpiarDatos()
        {
            txtDocumento.Text = "";
        }

        private void FrmDocumentos_Load(object sender, EventArgs e)
        {
            CargarDatos();
            LimpiarDatos();

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDocumento.Equals(""))
            {
                using (sistemaVentasEntities db = new sistemaVentasEntities())
                {
                    document.nombreDocumento = txtDocumento.Text;

                    db.tb_documento.Add(document);
                    db.SaveChanges();
                }

                dtvDocumentos.Rows.Clear();
                CargarDatos();
                LimpiarDatos();
            }
            else
            {
                MessageBox.Show("Introduzca Valores primero.");
            }
        }

        private void dtvDocumentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String nombreDoc = dtvDocumentos.CurrentRow.Cells[1].Value.ToString();

            txtDocumento.Text = nombreDoc;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvDocumentos.CurrentRow.Cells[0].Value.ToString();

                int idC = int.Parse(id);

                document = db.tb_documento.Where(VerificarID => VerificarID.iDDocumento == idC).First();
                document.nombreDocumento = txtDocumento.Text;


                db.Entry(document).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            dtvDocumentos.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvDocumentos.CurrentRow.Cells[0].Value.ToString();

                document = db.tb_documento.Find(int.Parse(id));
                db.tb_documento.Remove(document);
                db.SaveChanges();
            }

            dtvDocumentos.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtDocumento.Text = "";
            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }
    }
}
