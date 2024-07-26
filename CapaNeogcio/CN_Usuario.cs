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

       
    }
}
