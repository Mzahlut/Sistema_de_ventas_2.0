using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNeogcio
{
    public class CN_Proovedor
    {

        private CD_Proovedor objCdProovedor = new CD_Proovedor();


        public List<Proovedor> listar()
        {
            return objCdProovedor.Listar();
        }

        public int Register(Proovedor proovedor, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (proovedor.Documento == "")
            {
                Mensaje += "Es necesario el docuemento del proovedor\n";
            }
            if (proovedor.RazonSocial == "")
            {
                Mensaje += "Es necesaria la razon social del proovedor\n";
            }

            if (proovedor.Correo == "")
            {
                Mensaje += "Es necesario el correo del proovedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objCdProovedor.Register(proovedor, out Mensaje);
            }

        }

        public bool Edit(Proovedor proovedor, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (proovedor.Documento == "")
            {
                Mensaje += "Es necesario el docuemento del proovedor\n";
            }
            if (proovedor.RazonSocial == "")
            {
                Mensaje += "Es necesaria la razon socual del proovedor\n";
            }

            if (proovedor.Correo == "")
            {
                Mensaje += "Es necesario el correo del proovedor\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objCdProovedor.Edit(proovedor, out Mensaje);

            }

        }

        public bool Delete(Proovedor proovedor, out string Mensaje)
        {
            return objCdProovedor.Delete(proovedor, out Mensaje);
        }





    }
}
