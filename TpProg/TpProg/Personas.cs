using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TpProg
{
    class Personas
    {
        int id_persona;
        string nombre;
        string apellido;
        int id_tipo_doc;
        string num_documento;
        DateTime fecha_nacimiento;
        string email;
        string telefono;
        string calle;
        string num_calle;
        int id_barrio;
        int id_estado_civil;
        int id_genero;


        public int pid_persona
        {
            get { return id_persona; }
            set { id_persona = value; }
        }

        public string papellido
        {
            get { return apellido; }
            set { apellido = value; }
        }

        public string pnombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string pnum_documento
        {
            get { return num_documento; }
            set { num_documento = value; }
        }


        public int pid_tipo_doc
        {
            get { return id_tipo_doc; }
            set { id_tipo_doc = value; }
        }

        public DateTime pfecha_nacimiento
        {
            get { return fecha_nacimiento; }
            set { fecha_nacimiento = value; }
        }

        public string pemail
        {
            get { return email; }
            set { email = value; }
        }

        public string ptelefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string pcalle
        {
            get { return calle; }
            set { calle = value; }
        }

        public string pnum_calle
        {
            get { return num_calle; }
            set { num_calle = value; }
        }

        public int pid_barrio
        {
            get { return id_barrio; }
            set { id_barrio = value; }
        }

        public int pid_estado_civil
        {
            get { return id_estado_civil; }
            set { id_estado_civil = value; }
        }

        public int pid_genero
        {
            get { return id_genero; }
            set { id_genero = value; }
        }
        public Personas()
        {
            id_persona = 0;
            nombre = null;
            apellido = null;
            id_tipo_doc = 0;
            num_documento = null;
            fecha_nacimiento = DateTime.Today;
            email = null;
            telefono = null;
            calle = null;
            num_calle = null;
            id_barrio = 0;
            id_estado_civil = 0;
            id_genero = 0;
        }


        public Personas(int id_persona, string nombre, string apellido, int id_tipo_doc, string num_documento, DateTime fecha_nacimiento, string email, string telefono, string calle, string num_calle, int id_barrio, int id_estado_civil, int id_genero)
        {
            this.id_persona = id_persona;
            this.nombre = nombre;
            this.apellido = apellido;
            this.id_tipo_doc = id_tipo_doc;
            this.num_documento = num_documento;
            this.fecha_nacimiento = fecha_nacimiento;
            this.email = email;
            this.telefono = telefono;
            this.calle = calle;
            this.num_calle = num_calle;
            this.id_barrio = id_barrio;
            this.id_estado_civil = id_estado_civil;
            this.id_genero = id_genero;
        }

  
        public string toStringPersonas()
        {
            return apellido + ", " + nombre;
        }

        public override string ToString()
        {
            return apellido + ", " + nombre;
        }
    }
}
