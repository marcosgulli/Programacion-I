using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpProg
{
    class Docente:Personas
    {
        private int legajo;

        private DateTime fecha_alta;

        public int pLegajo
        {
            set { legajo = value; }
            get { return legajo; }
        }

        public DateTime pfecha_alta
        {
            set { fecha_alta = value; }
            get { return fecha_alta; }
        }


        public Docente() : base()
        {
            legajo = 0;

            fecha_alta = DateTime.Today;
        }
        public Docente(int legajo, int id_persona, DateTime fecha_alta, string nombre, string apellido, int id_tipo_doc, string num_documento, DateTime fecha_nacimiento, string email, string telefono, string calle, string num_calle, int id_barrio, int id_estado_civil, int id_genero) : base(id_persona, nombre, apellido, id_tipo_doc, num_documento, fecha_nacimiento, email, telefono, calle, num_calle, id_barrio, id_estado_civil, id_genero)
        {
            this.legajo = legajo;

            this.fecha_alta = fecha_alta;
        }
        public string toStringDocentes()
        {
            return legajo + " - " + fecha_alta + "-" + base.toStringPersonas();
        }
    }
}
