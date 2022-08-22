using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExamenParcial
{
    public partial class frmProductoEdit : Form
    {
        private string cadenaconexion = "server=localhost; database=Parcial; Integrated security=true;";

        public frmProductoEdit()
        {
            InitializeComponent();
        }

        private void frmProductoEdit_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            using (var conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();

                var sql = "SELECT * FROM Producto";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Dictionary<string, string> tipoDocumentoSource = new Dictionary<string, string>();
                            while (lector.Read())
                            {
                                tipoDocumentoSource.Add(lector[0].ToString(), lector[2].ToString());
                            }
                            cboMarca.DataSource = new BindingSource(tipoDocumentoSource, null);
                            cboMarca.DisplayMember = "Value";
                            cboMarca.ValueMember = "Key";
                        }
                    }
                }

                sql = "SELECT * FROM Categoria";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            Dictionary<string, string> tipoDocumentoSource = new Dictionary<string, string>();
                            while (lector.Read())
                            {
                                tipoDocumentoSource.Add(lector[3].ToString(), lector[2].ToString());
                            }
                            cboCategoria.DataSource = new BindingSource(tipoDocumentoSource, null);
                            cboCategoria.DisplayMember = "Value";
                            cboCategoria.ValueMember = "Key";

                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}