using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.IO;
using System.Xml.Serialization;
using System.Threading.Tasks;
using System.Net;
namespace Ejercicio_Android2
{
    [Activity(Label = "Principal")]
    public class Principal : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.vistaprincipal);
            var lblDestino = FindViewById<TextView>
                (Resource.Id.lblusuario);
            var Imagen = FindViewById<ImageView>
                (Resource.Id.imagen);
            var txtFolio = FindViewById<EditText>
                (Resource.Id.txtfolio);
            var txtDomicilio = FindViewById<EditText>
                (Resource.Id.txtdomicilio);
            var txtNombre = FindViewById<EditText>
                (Resource.Id.txtnombre);
            var txtCorreo = FindViewById<EditText>
                (Resource.Id.txtcorreo);
            var txtEdad = FindViewById<EditText>
                (Resource.Id.txtedad);
            var txtSaldo = FindViewById<EditText>
                (Resource.Id.txtsaldo);
            var btnGuardar = FindViewById<Button>
                (Resource.Id.btnguardar);
            var btnBuscar = FindViewById<Button>
                (Resource.Id.btnbuscar);
            var btnGuardarSQLite = FindViewById<Button>
                (Resource.Id.btnguardarSQLite);
            var btnBuscarSqLite = FindViewById<Button>
                (Resource.Id.btnBuscarSQLite);
            string Usuario;
            Usuario = Intent.GetStringExtra("Usuario");
            lblDestino.Text = Usuario;
            if (Usuario == "Leomaris")
            {
                ArchivoImagen("https://pbs.twimg.com/profile_images/1063271293057941504/2C5bvQTI_400x400.jpg");
            }
            if (Usuario == "Dannae")
            {
                ArchivoImagen("https://pbs.twimg.com/profile_images/818166002294943745/Z_gJ6b6E_400x400.jpg");
            }
            if (Usuario == "Ashley")
            {
                ArchivoImagen("https://pbs.twimg.com/profile_images/1044260109285646337/Xv15IZ0X_400x400.jpg");
            }
            btnGuardar.Click += delegate {
                var DC = new Datos();
                try
                {
                    DC.Folio = int.Parse(txtFolio.Text);
                    DC.Nombre = txtNombre.Text;
                    DC.Correo = txtCorreo.Text;
                    DC.Domicilio = txtDomicilio.Text;
                    DC.Edad = int.Parse(txtEdad.Text);
                    DC.Saldo = double.Parse(txtSaldo.Text);
                    var serializador = new XmlSerializer
                        (typeof(Datos));
                    var Escritura = new StreamWriter
                        (Path.Combine(System.Environment.GetFolderPath
                        (System.Environment.SpecialFolder.Personal),
                                      txtFolio.Text + ".xml"));
                    serializador.Serialize(Escritura, DC);
                    Escritura.Close();
                    txtFolio.Text = "";
                    txtNombre.Text = "";
                    txtCorreo.Text = "";
                    txtSaldo.Text = "";
                    txtEdad.Text = "";
                    txtDomicilio.Text = "";
                    Toast.MakeText
                         (this, "Archivo XML: Guardado Correctamente",
                                   ToastLength.Long).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText
                         (this, ex.Message,
                       ToastLength.Long).Show();
                }
            };
            btnBuscar.Click += delegate {
                var DC = new Datos();
                try
                {
                    DC.Folio = int.Parse(txtFolio.Text);
                    var serializador = new XmlSerializer
                        (typeof(Datos));
                    var Lectura = new StreamReader
                        (Path.Combine(System.Environment.GetFolderPath
                        (System.Environment.SpecialFolder.Personal),
                                      txtFolio.Text + ".xml"));
                    var datos = (Datos)serializador.Deserialize
                        (Lectura);
                    Lectura.Close();
                    txtNombre.Text = datos.Nombre;
                    txtDomicilio.Text = datos.Domicilio;
                    txtCorreo.Text = datos.Correo;
                    txtEdad.Text = datos.Edad.ToString();
                    txtSaldo.Text = datos.Saldo.ToString();
                }
                catch (Exception ex)
                {
                    Toast.MakeText
                         (this, ex.Message,
                       ToastLength.Long).Show();
                }
            };
            async void ArchivoImagen(string url)
            {
                var ruta = await DescargaImagen(url);
                Android.Net.Uri rutaImagen = Android.Net.Uri.Parse
                    (ruta);
                Imagen.SetImageURI(rutaImagen);
            }

            btnGuardarSQLite.Click += delegate
            {
                try
                {
                    var csql = new ClaseSQLite();
                    csql.nombre = txtNombre.Text;
                    csql.domicilio = txtDomicilio.Text;
                    csql.correo = txtCorreo.Text;
                    csql.edad = int.Parse(txtEdad.Text);
                    csql.saldo = double.Parse(txtSaldo.Text);
                    csql.ConexionBase();
                    if ((csql.IngresarDatos(csql.nombre, csql.domicilio, 
                        csql.correo, csql.edad, csql.saldo)) == true)
                    {
                        Toast.MakeText
                            (this, "Guardado Correctamente en SQLite",
                                ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText
                            (this, "No guardado",
                                ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText
                        (this, ex.Message,
                            ToastLength.Long).Show();
                }
            };
            btnBuscarSqLite.Click += delegate
            {
                try
                {
                    var csql = new ClaseSQLite();
                    csql.folio = int.Parse(txtFolio.Text);
                    csql.Buscar(csql.folio);
                    txtFolio.Text = csql.folio.ToString();
                    txtNombre.Text = csql.nombre;
                    txtDomicilio.Text = csql.domicilio;
                    txtCorreo.Text = csql.correo;
                    txtEdad.Text = csql.edad.ToString();
                    txtSaldo.Text = csql.saldo.ToString();
                }
                catch (Exception ex)
                {
                    Toast.MakeText
                    (this, ex.Message,
                        ToastLength.Long).Show();
                }
            };
        }
        public async Task<string> DescargaImagen(string url)
        {
            WebClient client = new WebClient();
            byte[] imageData = await client.DownloadDataTaskAsync
                (url);
            string documentspath = System.Environment.GetFolderPath
                (System.Environment.SpecialFolder.Personal);
            string localfilename = "foto1.jpg";
            string localpath = Path.Combine(documentspath, localfilename);
            File.WriteAllBytes(localpath, imageData);
            return localpath;
        }
    }
}