using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Activamos las referencias
using Capa_Entidad;
using Capa_Negocio;

namespace Agenda___LUIS_MONTERO
{
    public partial class Formulario : Form
    {
        //Objetos de las Clases
        C_Entidad entidad = new C_Entidad();
        C_Negocio negocio = new C_Negocio();

        public Formulario()
        {
            InitializeComponent();
            tablaContactos.DataSource = negocio.N_Listar_Contactos();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_nuevo_Click(object sender, EventArgs e)
        {
            //Limpiar campos
            limpiar();
            //Mensaje
            MessageBox.Show("¡Campos limpios!", "Mensaje", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void limpiar()
        {
            edit_buscar.Text = "";
            edit_codigo.Text = "";
            edit_telefono.Text = "";
            edit_nombre.Text = "";
            edit_correo.Text = "";
            tablaContactos.DataSource = negocio.N_Listar_Contactos();//Refresca
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(edit_buscar.Text))
            {
                try
                {
                    entidad.Telefono = edit_buscar.Text.Trim();
                    DataTable table = negocio.N_buscar_Contacto(entidad);
                    tablaContactos.DataSource = table;

                    //Mensaje si la tabla viene vacia
                    if (table.Rows.Count == 0)
                    {
                        
                        MessageBox.Show("No se encontró este número.", "Resultado",
                            MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        tablaContactos.DataSource = negocio.N_Listar_Contactos();
                    }
                    
                } catch (Exception ex)
                {
                    
                    MessageBox.Show("Error al buscar contacto: " + ex.Message, "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tablaContactos.DataSource = negocio.N_Listar_Contactos();
                }
                
            } else
            {
                MessageBox.Show("Favor, llenar el campo", "Alert!", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }  
        }

        private void tablaContactos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = tablaContactos.Rows[e.RowIndex];

                edit_codigo.Text = fila.Cells[0].Value.ToString();
                edit_nombre.Text = fila.Cells[1].Value.ToString();
                edit_telefono.Text = fila.Cells[2].Value.ToString();
                edit_correo.Text = fila.Cells[3].Value.ToString();
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edit_nombre.Text) || string.IsNullOrEmpty(edit_telefono.Text)
                || string.IsNullOrEmpty(edit_correo.Text))
            {
                MessageBox.Show("Favor llenar todos lo campos.", "Alert",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                try
                {
                    entidad.Nombre = edit_nombre.Text;
                    entidad.Telefono = edit_telefono.Text;
                    entidad.Email = edit_correo.Text;

                    negocio.N_insertar_Contacto(entidad);

                    MessageBox.Show("Contacto agregado", "Mensaje", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    tablaContactos.DataSource = negocio.N_Listar_Contactos();
                    limpiar();
                } catch(Exception ex)
                {
                    MessageBox.Show("Error al intentar agregar contacto: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_modificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edit_codigo.Text) || string.IsNullOrEmpty(edit_nombre.Text) 
                || string.IsNullOrEmpty(edit_telefono.Text)
                || string.IsNullOrEmpty(edit_correo.Text))
            {
                MessageBox.Show("Favor seleccione un Contacto.", "Alert",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    entidad.Id = int.Parse(edit_codigo.Text);
                    entidad.Nombre = edit_nombre.Text;
                    entidad.Telefono = edit_telefono.Text;
                    entidad.Email = edit_correo.Text;

                    negocio.N_modificar_Contacto(entidad);

                    MessageBox.Show("Contacto actualizado", "Mensaje", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    tablaContactos.DataSource = negocio.N_Listar_Contactos();
                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar modificar contacto: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edit_codigo.Text) || string.IsNullOrEmpty(edit_nombre.Text) 
                || string.IsNullOrEmpty(edit_telefono.Text)
                || string.IsNullOrEmpty(edit_correo.Text))
            {
                MessageBox.Show("Favor, seleccione un Contacto.", "Alert",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    entidad.Id = int.Parse(edit_codigo.Text);

                    negocio.N_eliminar_Contacto(entidad);

                    MessageBox.Show("Contacto eliminado.", "Mensaje", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    tablaContactos.DataSource = negocio.N_Listar_Contactos();//Refresca la tabla
                    limpiar();//Limpias las cajas de textos
                }
                catch (Exception ex)
                {
                    //Mensaje de error:
                    MessageBox.Show("Error al intentar agregar contacto: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
