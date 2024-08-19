using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNeogcio
{
    public class CN_Negocio
    {
        private CD_Negocio objCdNegocio = new CD_Negocio();


        public Negocio ObtenerDatos()
        {
            return objCdNegocio.ObtenerDatos();
        }

        public bool GuardarDatos(Negocio neg, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (neg.NombreNegocio == "")
            {
                Mensaje += "Es necesario el nombre del negocio\n";
            }
            if (neg.Ruc == "")
            {
                Mensaje += "Es necesario el cuit del negocio\n";
            }

            if (neg.Direccion == "")
            {
                Mensaje += "Es necesario la direccion del negocio\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objCdNegocio.GuardarDatos(neg, out Mensaje);
            }

        }


        public byte[] ObtenerLogo(out bool obtenido)
        {
            return objCdNegocio.ObtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] image, out string mensaje)
        {
            return objCdNegocio.ActualizarLogo(image, out mensaje);
        }



    }
}
