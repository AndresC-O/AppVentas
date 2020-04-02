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
using proVentas.Vista;

namespace proVentas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            using (sistemaVentasEntities datbase = new sistemaVentasEntities())
            {
                var lista = from usuario in datbase.usuarios
                            where usuario.email == txtNombre.Text
                            && usuario.contrasenia == txtContrasenia.Text
                            select usuario;

                if (lista.Count() > 0)
                {
                    frmMenu menu = new frmMenu();
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("El Usuario no existe");
                }
            }
        }
    }
}
