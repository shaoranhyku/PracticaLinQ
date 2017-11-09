namespace PracticaLinQ.resources
{
    class Person
    {
        //Campos
        private string nombre, edad;

        //Constructor
        public Person(string nombre, string edad)
        {
            this.nombre = nombre;
            this.edad = edad;
        }

        //Propiedades
        /// <summary>
        /// Nombre de la persona.
        /// </summary>
        public string Nombre
        {
            get
            {
                return nombre;
            }
        }

        /// <summary>
        /// Edad de la persona.
        /// </summary>
        public string Edad
        {
            get
            {
                return edad;
            }
        }

        /// <summary>
        /// Obtiene una representación del objeto en cadena.
        /// </summary>
        /// <returns>Cadena con las propiedades del objeto</returns>
        override
        public string ToString()
        {
            return "Nombre: " + Nombre + " Edad: " + Edad;
        }
    }
}
