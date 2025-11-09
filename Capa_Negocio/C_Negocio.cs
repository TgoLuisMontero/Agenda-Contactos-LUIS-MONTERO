using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Activamos las referencias
using System.Data;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Negocio
{
    public class C_Negocio
    {
        //Objeto de la clase Mantenimiento de la Capa de Datos
        C_Mantenimiento datos = new C_Mantenimiento();

        //Metodo para Listar Contactos
        public DataTable N_Listar_Contactos()
        {
            return datos.D_listar_contactos();
        }

        //Metodo para buscar un contacto
        public DataTable N_buscar_Contacto(C_Entidad obj)
        {
            //Llamada al metodo de la Clase Mantenimiento de la Cpa Datos
            return datos.D_buscar_Contacto(obj);
        }

        //Metodo Insertar del negogio
        public bool N_insertar_Contacto(C_Entidad obj)
        {
            return datos.D_insertar_Contacto(obj);
        }

        //Metodo para Modificar un Contacto
        public bool N_modificar_Contacto(C_Entidad obj)
        {
            return datos.D_modificar_Contacto(obj);
        }

        //Metodo para Eliminar un COntacto
        public bool N_eliminar_Contacto(C_Entidad obj)
        {
            return datos.D_eliminar_Contacto(obj);
        }
        //Logica del Negocio....
    }
}
