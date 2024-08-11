using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNeogcio
{
    public class CN_Categoria
    {

        private CD_Categoria objCdCategoria = new CD_Categoria();


        public List<Categoria> listar()
        {
            return objCdCategoria.Listar();
        }

        public int Register(Categoria Categoria, out string Mensaje)
        {
            Mensaje = string.Empty;

 

            if (Categoria.Descripcion == "")
            {
                Mensaje += "La descripcion de la categoria no debe estar vacia\n";
            }

            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {

                return objCdCategoria.Register(Categoria, out Mensaje);
            }

        }

        public bool Edit(Categoria Categoria, out string Mensaje)
        {

            Mensaje = string.Empty;



            if (Categoria.Descripcion == "")
            {
                Mensaje += "La descripcion de la categoria no debe estar vacia\n";
            }

            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {

                return objCdCategoria.Edit(Categoria, out Mensaje);

            }

        }

        public bool Delete(Categoria Categoria, out string Mensaje)
        {
            return objCdCategoria.Delete(Categoria, out Mensaje);
        }


    }
}
