using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNeogcio
{
    public class CN_Cliente
    {
        private CD_Cliente objCdCliente = new CD_Cliente();


        public List<Cliente> listar()
        {
            return objCdCliente.Listar();
        }

        public int Register(Cliente cliente, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (cliente.Documento == "")
            {
                Mensaje += "Es necesario el docuemento del cliente\n";
            }
            if (cliente.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del cliente\n";
            }

            if (cliente.Correo == "")
            {
                Mensaje += "Es necesario el correo del cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objCdCliente.Register(cliente, out Mensaje);
            }

        }

        public bool Edit(Cliente cliente, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (cliente.Documento == "")
            {
                Mensaje += "Es necesario el docuemento del cliente\n";
            }
            if (cliente.NombreCompleto == "")
            {
                Mensaje += "Es necesario el nombre del cliente\n";
            }

            if (cliente.Correo == "")
            {
                Mensaje += "Es necesario el correo del cliente\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objCdCliente.Edit(cliente, out Mensaje);

            }

        }

        public bool Delete(Cliente cliente, out string Mensaje)
        {
            return objCdCliente.Delete(cliente, out Mensaje);
        }




    }
}
