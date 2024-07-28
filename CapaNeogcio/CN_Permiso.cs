using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNeogcio
{
    public class CN_Permiso
    {

        private CD_Permiso objCdPermiso = new CD_Permiso();


        public List<Permiso> listar(int idUsuario)
        {
            return objCdPermiso.Listar(idUsuario);
        }



    }
}
