using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System.IO;
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
            async void ArchivoImagen(string url)
            {
                var ruta = await DescargaImagen(url);
                Android.Net.Uri rutaImagen = Android.Net.Uri.Parse
                    (ruta);
                Imagen.SetImageURI(rutaImagen);
            }
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