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
    public partial class frmUsuarios : Form
    {
        public frmUsuarios()
        {
            InitializeComponent();
        }

        usuarios user = new usuarios();

        void CargarDatos()
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                //dtvUsuarios.DataSource = db.usuarios.ToList();

                var tbUsuarios = db.usuarios;

                foreach (var iterarDatosUsuarios in tbUsuarios)
                {
                    
                    dtvUsuarios.Rows.Add(iterarDatosUsuarios.id, iterarDatosUsuarios.email, iterarDatosUsuarios.contrasenia);
                }
            }
        }

        void LimpiarDatos()
        {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            dtvUsuarios.Rows.Clear();
            CargarDatos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(txtUsuario.Equals("") || txtContrasenia.Equals(""))
            {
                using (sistemaVentasEntities db = new sistemaVentasEntities())
                {
                    user.email = txtUsuario.Text;
                    user.contrasenia = txtContrasenia.Text;

                    db.usuarios.Add(user);
                    db.SaveChanges();
                }

                dtvUsuarios.Rows.Clear();
                CargarDatos();
                LimpiarDatos();
            }
            else
            {
                MessageBox.Show("Introduzca Valores primero.");
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvUsuarios.CurrentRow.Cells[0].Value.ToString();

                user = db.usuarios.Find(int.Parse(id));
                db.usuarios.Remove(user);
                db.SaveChanges();
            }

            dtvUsuarios.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void dtvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            String email = dtvUsuarios.CurrentRow.Cells[1].Value.ToString();
            String contra = dtvUsuarios.CurrentRow.Cells[2].Value.ToString();

            txtUsuario.Text = email;
            txtContrasenia.Text = contra;

            btnGuardar.Enabled = false;
            btnEditar.Enabled = true;
            btnEliminar.Enabled = true;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities db = new sistemaVentasEntities())
            {
                String id = dtvUsuarios.CurrentRow.Cells[0].Value.ToString();

                int idC = int.Parse(id);

                user = db.usuarios.Where(VerificarID => VerificarID.id == idC).First();
                user.email = txtUsuario.Text;
                user.contrasenia = txtContrasenia.Text;

                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            dtvUsuarios.Rows.Clear();
            CargarDatos();
            LimpiarDatos();
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarDatos();

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = false;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void dtvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = "";
            txtContrasenia.Text = "";

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnGuardar.Enabled = true;
        }
    }
}
