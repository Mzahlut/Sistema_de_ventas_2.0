using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNeogcio
{
    public class CN_Producto
    {
        private CD_Producto objCdProducto = new CD_Producto();


        public List<Producto> listar()
        {
            return objCdProducto.Listar();
        }

        public int Register(Producto producto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (producto.Codigo == "")
            {
                Mensaje += "Es necesario el codigo del producto\n";
            }
            if (producto.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }

            if (producto.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion del producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objCdProducto.Register(producto, out Mensaje);
            }

        }

        public bool Edit(Producto producto, out string Mensaje)
        {

            Mensaje = string.Empty;

            if (producto.Codigo == "")
            {
                Mensaje += "Es necesario el codigo del producto\n";
            }
            if (producto.Nombre == "")
            {
                Mensaje += "Es necesario el nombre del producto\n";
            }

            if (producto.Descripcion == "")
            {
                Mensaje += "Es necesario la descripcion del producto\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objCdProducto.Edit(producto, out Mensaje);

            }

        }

        public bool Delete(Producto producto, out string Mensaje)
        {
            return objCdProducto.Delete(producto, out Mensaje);
        }




    }


}


