using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace PracticaJSON_Forms
{
    public class Persona
    {
        public int DNI;
        public string Nombre;
        public Mascota mascota;

        public Persona() { }
        public Persona(int dni,string nombre,Mascota m)
        {
            DNI = dni;
            Nombre = nombre;
            mascota  = m;
        }

         
    }
    public class Mascota
    {
       public enum TIPO
        {
            NINGUNA=0,
            PERRO=1,
            GATO=2,
            PEZ=3
        }
        public string Nombre;
        public TIPO tipo;
        public Mascota() { }    
        public Mascota(string nombre,TIPO t)
        {
            Nombre = nombre;
            tipo = t;
        }
    }//classMascota

    //ESTE METODO GUARDARA CUALQUIER TIPO DE DATO PUES NO ESPECIFICAMOS EL TIPO DE DATO 
    //SE USA EL List<T> valores=new List<T>(); PARA GUARDAR CUALQUIER TIPO DE DATO 
    public class BasedeDatos<T>
    {
        public  List<T> valores=new List<T>();
        public string ruta;

        public BasedeDatos() { }
        public BasedeDatos(string r)
        {
            ruta=r;
        }
        public void Guardar()
        {
            string texto=JsonConvert.SerializeObject(valores);
            File.WriteAllText(ruta,texto);
        }

        public void Cargar()
        {
            try
            {

                string archivo = File.ReadAllText(ruta);
                valores = JsonConvert.DeserializeObject<List<T>>(archivo);
            }catch (Exception e) {
            MessageBox.Show(e.Message);
            }
        }

        public void Insertar(T nuevo)
        {
            valores.Add(nuevo);
            Guardar();
        }

        public List<T> Buscar(Func<T,bool> criterio)
        {
            return valores.Where(criterio).ToList();
        }

        public void Eliminar(Func<T, bool> criterio)
        {
            //Nos quedaremos con los valores que sean diferentes a esta condicion(los que sean true serane liminados)
            valores=valores.Where(x=>!criterio(x)).ToList();   
        }

        public void Actualizar(Func<T, bool> criterio,T nuevo)
        {
            valores = valores.Select(x =>
            {
                if (criterio(x)) x = nuevo;
                return x;
            }
            
            
            ).ToList();
        }
    }
}
