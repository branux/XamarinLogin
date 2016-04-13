using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Android.Preferences;

namespace App1
{
    [Activity(Label = "Squadra", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, Icon = "@drawable/Icone")]
    public class SplashScreen : Activity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ISharedPreferences prefs = PreferenceManager.GetSharedPreferences("logado", FileCreationMode.Private);
            bool logado = prefs.GetBoolean("logado", false);
            
            if(!logado)
            {
                //Display Splash Screen for 4 Sec
                Thread.Sleep(4000);
                //Start Activity1 Activity
                StartActivity(typeof(MainActivity));
            }
            else
            {
                //Display Splash Screen for 4 Sec
                Thread.Sleep(4000);
                //Start Activity1 Activity
                StartActivity(typeof(Principal));
            }

            


            
        }
    }
}