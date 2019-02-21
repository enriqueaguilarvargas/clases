using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using Android.Content;
namespace Ejercicio_Android2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        string Usuario, Password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            var btnIngresa = FindViewById<Button>
                (Resource.Id.btningresar);
            var txtUser = FindViewById<EditText>
                (Resource.Id.txtusuario);
            var txtPass = FindViewById<EditText>
                (Resource.Id.txtpassword);
            var Imagen = FindViewById<ImageView>
                (Resource.Id.imagen);
            Imagen.SetImageResource(Resource.Drawable.logo);
            btnIngresa.Click += delegate
            {
                try
                {
                    Usuario = txtUser.Text;
                    Password = txtPass.Text;
                    if (Usuario == "Leomaris")
                        if (Password == "123")
                        {
                            Cargar();
                        }
                        else
                            Toast.MakeText(this, "Error de Password",
                                           ToastLength.Short)
                                .Show();
                    else
                        if (Usuario == "Dannae")
                        if (Password == "456")
                            Cargar();
                        else
                            Toast.MakeText(this, "Error de Password",
                                           ToastLength.Short)
                                .Show();
                    else
                        if (Usuario == "Ashley")
                        if (Password == "789")
                            Cargar();
                        else
                            Toast.MakeText(this, "Error de Password",
                                           ToastLength.Short)
                                .Show();
                    else
                        Toast.MakeText(this, "Error de Usuario",
                                       ToastLength.Short)
                            .Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short)
                        .Show();
                }
            };
        }
        public void Cargar()
        {
            Intent objIntent = new Intent(this, typeof
                                          (Principal));
            objIntent.PutExtra("Usuario", Usuario);
            StartActivity(objIntent);
        }
    }
}