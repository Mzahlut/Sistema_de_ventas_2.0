using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNeogcio
{
    public class CN_Usuario
    {
        private CD_Usuario objCdUsuario= new CD_Usuario();


        public List<Usuario> listar()
        {
            return objCdUsuario.Listar();
        }

        public int Register(Usuario usuario, out string Mensaje)
        {
            Mensaje = string.Empty;
            
            if(usuario.Documento == "")
            {
                Mensaje += "Es necesario el docuemento del usuario\n";
            }
            if(usuario.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }

            if (usuario.Clave == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }

            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

            return objCdUsuario.Register(usuario, out Mensaje);
            }

        }

        public bool Edit(Usuario usuario, out string Mensaje)
        {

            Mensaje = string.Empty; 

            if (usuario.Documento == "")
            {
                Mensaje += "Es necesario el docuemento del usuario\n";
            }
            if (usuario.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del usuario\n";
            }

            if (usuario.Clave == "")
            {
                Mensaje += "Es necesario la clave del usuario\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {

            return objCdUsuario.Edit(usuario, out Mensaje);

            }

        }

        public bool Delete(Usuario usuario, out string Mensaje)
        {
            return objCdUsuario.Delete(usuario, out Mensaje);
        }




    }
}
