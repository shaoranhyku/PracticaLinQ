using PracticaLinQ.resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace PracticaLinQ
{
    public partial class MainPage : ContentPage
    {

        ObservableCollection<Person> datos = new ObservableCollection<Person>();

        public MainPage()
        {
            InitializeComponent();

            datos = LeerArchivoXML("PracticaLinQ.data.alumnos.xml");

            lstPeople.ItemsSource = datos;

            btnAdd.Clicked += BtnAdd_Clicked;
            btnWhere.Clicked += BtnWhere_Clicked;
            btnFirstOrDefault.Clicked += BtnFirstOrDefault_Clicked;
            btnSingleOrDefault.Clicked += BtnSingleOrDefault_Clicked;
            btnLastOrDefault.Clicked += BtnLastOrDefault_Clicked;
            btnOrderBy.Clicked += BtnOrderBy_Clicked;
            btnOrderByDesc.Clicked += BtnOrderByDesc_Clicked;
            btnSkipWhile.Clicked += BtnSkipWhile_Clicked;
            btnTakeWhile.Clicked += BtnTakeWhile_Clicked;
        }

        /// <summary>
        /// Permite leer un archivo XML a partir de una ruta recibida.
        /// </summary>
        /// <param name="ruta">Ruta donde se encuentra el archivo XML</param>
        /// <returns>Lista de contactos creados a partir del archivo</returns>
        public ObservableCollection<Person> LeerArchivoXML(String ruta)
        {

            ObservableCollection<Person> arrText = new ObservableCollection<Person>();

            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(ruta);
            StreamReader objReader = new StreamReader(stream);
            var doc = XDocument.Load(stream);

            foreach (XElement element in doc.Root.Elements())
            {
                arrText.Add(new Person(element.Element("NOMBRE").Value, element.Element("EDAD").Value));
            }

            return arrText;
        }

        /// <summary>
        /// Añade una persona a partir de los campos de nombre y edad.
        /// </summary>
        /// <remarks>
        /// Toma los valores de los Entry txtName y txtAge y añade un objeto de la clase Person a la lista
        /// de la List View.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnAdd_Clicked(object sender, EventArgs e)
        {
            int edad;
            if(int.TryParse(txtAge.Text,out edad))
            { 
                datos.Add(new Person(txtName.Text,txtAge.Text));
                lstPeople.ItemsSource = datos;
            }
        }

        /// <summary>
        /// Filtra la lista de personas por nombre.
        /// </summary>
        /// <remarks>
        /// Toma el valor del Entry txtWhere devuelve una lista con todos las personas que tengan ese nombre
        /// de la lista de personas inicial, y la asigna a la List View.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnWhere_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>(datos.Where(t => t.Nombre == txtWhere.Text).ToList());
            lstPeople.ItemsSource = nuevaLista;
        }

        /// <summary>
        /// Toma el primer elemento de la lista que coincida con un nombre.
        /// </summary>
        /// <remarks>
        /// Toma el valor del Entry txtFirstOrDefault, devuelve una lista con la primera persona que tengan ese nombre
        /// de la lista de personas inicial, y la asigna a la List View.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnFirstOrDefault_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>();
            nuevaLista.Add(datos.FirstOrDefault(t => t.Edad == txtFirstOrDefault.Text));
            lstPeople.ItemsSource = nuevaLista;
        }

        /// <summary>
        /// Toma el único elemento de la lista que coincida con un nombre.
        /// </summary>
        /// <remarks>
        /// Toma el valor del Entry txtSingleOrDefault, devuelve una lista con la unica persona que tengan ese nombre
        /// de la lista de personas inicial, y la asigna a la List View.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnSingleOrDefault_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>();
            nuevaLista.Add(datos.Where(t => t.Nombre == txtSingleOrDefaultName.Text).SingleOrDefault(t => t.Edad == txtSingleOrDefaultAge.Text));
            lstPeople.ItemsSource = nuevaLista;
        }

        /// <summary>
        /// Toma el último elemento de la lista que coincida con un nombre.
        /// </summary>
        /// <remarks>
        /// Toma el valor del Entry txtLastOrDefault, devuelve una lista con la última persona que tengan ese nombre
        /// de la lista de personas inicial, y la asigna a la List View.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnLastOrDefault_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>();
            nuevaLista.Add(datos.LastOrDefault(t => t.Edad == txtLastOrDefault.Text));
            lstPeople.ItemsSource = nuevaLista;
        }

        /// <summary>
        /// Ordena la lista ascendentemente.
        /// </summary>
        /// <remarks>
        /// Ordena a lista inicial de personas ascendentemente por el nombre.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnOrderBy_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>(datos.OrderBy(t => t.Nombre).ToList());
            lstPeople.ItemsSource = nuevaLista;
        }

        /// <summary>
        /// Ordena la lista descendentemente.
        /// </summary>
        /// <remarks>
        /// Ordena a lista inicial de personas descendentemente por el nombre.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnOrderByDesc_Clicked(object sender, EventArgs e)
        {
            ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>(datos.OrderByDescending(t => t.Nombre).ToList());
            lstPeople.ItemsSource = nuevaLista;
        }

        /// <summary>
        /// Crea una lista con personas a partir de que encuentre una persona que no cumpla la condición.
        /// </summary>
        /// <remarks>
        /// Toma el valor del Entry txtSkipWhile, devuelve una lista con personas a partir de encontrar
        /// una persona cuya edad sea menor que la introducida de la lista de personas inicial, y la asigna a la List View.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnSkipWhile_Clicked(object sender, EventArgs e)
        {
            int edadSkipWhile;
            if(int.TryParse(txtSkipWhile.Text,out edadSkipWhile))
            {
                ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>(datos.SkipWhile(t => int.Parse(t.Edad) > edadSkipWhile ).ToList());
                lstPeople.ItemsSource = nuevaLista;
            }
        }

        /// <summary>
        /// Crea una lista con personas hasta que encuentre una persona que no cumpla la condición.
        /// </summary>
        /// <remarks>
        /// Toma el valor del Entry txtTakeWhile, devuelve una lista con personas hasta encontrar
        /// una persona cuya edad sea menor que la introducida de la lista de personas inicial, y la asigna a la List View.
        /// </remarks>
        /// <param name="sender">Objeto que desencadena el evento</param>
        /// <param name="e">Arumentos del evento</param>
        private void BtnTakeWhile_Clicked(object sender, EventArgs e)
        {
            int edadTakeWhile;
            if (int.TryParse(txtTakeWhile.Text, out edadTakeWhile))
            {
                ObservableCollection<Person> nuevaLista = new ObservableCollection<Person>(datos.TakeWhile(t => int.Parse(t.Edad) > edadTakeWhile).ToList());
                lstPeople.ItemsSource = nuevaLista;
            }
        }
    }
}
