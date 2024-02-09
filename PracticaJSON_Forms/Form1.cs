using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticaJSON_Forms
{
    public partial class Form1 : Form
    {   
        BasedeDatos<Persona> bd= new BasedeDatos<Persona>("bd.json");

        void mostrar(List<Persona> lista)
        {
            dgvDatos.Rows.Clear();

            foreach (Persona p in lista)
            {
                DataGridViewRow fila = new DataGridViewRow();
                fila.Cells.Add(new DataGridViewTextBoxCell() { Value = p.DNI });
                fila.Cells.Add(new DataGridViewTextBoxCell() { Value = p.Nombre });
                dgvDatos.Rows.Add(fila);
            }
        }

        public Form1()
        {
            InitializeComponent();
            cbMascota.SelectedIndex = 0;
            bd.Cargar();
            mostrar(bd.valores);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            Mascota.TIPO t;
            switch (cbMascota.SelectedIndex)
            {
                default: t = Mascota.TIPO.NINGUNA;break;
                case 1: t = Mascota.TIPO.PERRO; break;
                case 2: t = Mascota.TIPO.GATO; break;
                case 3: t = Mascota.TIPO.PEZ; break;
               

            }
            Mascota m= new Mascota(txbNombreMascota.Text,t);
            Persona p = new Persona((new Random()).Next(10000,99999),txbNombre.Text,m);
            bd.Insertar(p);
            mostrar(bd.valores);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int DNI = int.Parse(txbDNI.Text);
            Mascota.TIPO t;
            switch (cbMascota.SelectedIndex)
            {
                default: t = Mascota.TIPO.NINGUNA; break;
                case 1: t = Mascota.TIPO.PERRO; break;
                case 2: t = Mascota.TIPO.GATO; break;
                case 3: t = Mascota.TIPO.PEZ; break;


            }
            Mascota m = new Mascota(txbNombreMascota.Text, t);
            Persona p = new Persona((DNI), txbNombre.Text, m);
            bd.Actualizar(x => x.DNI == DNI, p);
            mostrar(bd.valores);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int DNI = int.Parse(txbDNI.Text);
            bd.Eliminar(x => x.DNI == DNI);
            mostrar(bd.valores);
        }

        private void txbBuscar_TextChanged(object sender, EventArgs e)
        {
            var lista = bd.Buscar(x=>x.Nombre.Contains(txbBuscar.Text));
            mostrar(lista);
        }

        private void dgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Persona p = bd.Buscar(x => x.DNI.ToString() == dgvDatos.CurrentRow.Cells[0].Value.ToString())[0];
        txbDNI.Text=p.DNI.ToString();
    txbNombre.Text=p.Nombre.ToString();
            txbNombreMascota.Text = p.mascota.Nombre;
            cbMascota.SelectedIndex = (int)p.mascota.tipo;
        }

        private void dgvDatos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
