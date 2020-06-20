using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_Ingenieria_Publicidad.Models
{
    public class Usuario
    {
        public Usuario(string usuario, string contra, string correo)
        {
            this.usuario = usuario;
            this.contra = contra;
            this.correo = correo;
        }
        public Usuario(string usuario, string contra)
        {
            this.usuario = usuario;
            this.contra = contra;
          
        }
        public string usuario { get; set; }
        public string contra { get; set; }
        public string correo { get; set; }

    }
}
