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

namespace App1
{
    [Activity(Label = "Squadra", MainLauncher = true, Theme = "@style/Theme.Splash", NoHistory = true, Icon = "@drawable/Icone")]
    public class SplashScreen : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Display Splash Screen for 4 Sec
            Thread.Sleep(4000);
            //Start Activity1 Activity
            StartActivity(typeof(MainActivity));
        }
    }
}