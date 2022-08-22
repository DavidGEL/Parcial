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
    public partial class frmProducto : Form
    {
        private string cadenaconexion = "server=localhost; database=Parcial; Integrated security=true;";

        public frmProducto()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var frm = new frmProductoEdit();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var nombre = ((TextBox)frm.Controls["txtNombre"]).Text;
                var Marca = ((ComboBox)frm.Controls["cboMarca"]).SelectedValue.ToString();
                var Categoria = ((ComboBox)frm.Controls["cboCategoria"]).SelectedValue.ToString();
                var Stock = ((TextBox)frm.Controls["txtStock"]).Text;
                var Precio = ((TextBox)frm.Controls["txtPrecio"]).Text;

                using (var conexion = new SqlConnection(cadenaconexion))
                {
                    conexion.Open();
                    var sql = "INSERT INTO Cliente (Nombres ,Marca,Categoria,Stock,Precio) " +
                        "VALUES(@nombre, @marca,@categoria,@stock, @precio)";

                    using (var comando = new SqlCommand(sql, conexion))
                    {
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@marca", Marca);
                        comando.Parameters.AddWithValue("@categoria", Categoria);
                        comando.Parameters.AddWithValue("@stock", Stock);
                        comando.Parameters.AddWithValue("@precio", Precio);
                        
                        
                        
                        
                        
                        int resultado = comando.ExecuteNonQuery();
                        if (resultado > 0)
                        {
                            MessageBox.Show("El cliente ha sido registrado", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("El proceso de creación del cliente ha fallado.", "Sistemas",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
        private void cargarDatos()
        {
            dgvDatos.Rows.Clear();
            using (var conexion = new SqlConnection(cadenaconexion))
            {
                conexion.Open();
                var sql = "select * from producto";
                using (var comando = new SqlCommand(sql, conexion))
                {
                    using (var lector = comando.ExecuteReader())
                    {
                        if (lector != null && lector.HasRows)
                        {
                            while (lector.Read())
                            {
                                dgvDatos.Rows.Add(lector[0], lector[1], lector[2], lector[3], lector[4], lector[5],
                                    lector[6], lector[7]);
                            }
                        }
                    }
                }
            }
        }

    }
}