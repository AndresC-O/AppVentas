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
    public partial class frmRoles : Form
    {
        public frmRoles()
        {
            InitializeComponent();
        }

        private void frmRoles_Load(object sender, EventArgs e)
        {
            using(sistemaVentasEntities db = new sistemaVentasEntities())
            {
                var joinTablas = from usuar in db.usuarios
                                 from rolesUs in db.roles_usuarios
                                 where usuar.id == rolesUs.idUsuario

                                 select new
                                 {
                                     Id = usuar.id,
                                     Email = usuar.email,
                                     TipoRol = rolesUs.tipo_rol
                                 };

                //dtVistaRoles.DataSource = joinTablas.ToList();
                foreach (var iterar in joinTablas)
                {
                    dtVistaRoles.Rows.Add(iterar.Id, iterar.Email, iterar.TipoRol);
                }
            }
        }
    }
}
