using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using App1.Classes;

namespace App1
{
    [Activity(Label = "Untitled", MainLauncher = true, Icon = "@drawable/Icone")]
    public class MainActivity : Activity, IFacebookCallback
    {
        private ICallbackManager mCallBackManager;
        private MyProfileTracker mProfileTracker;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Window.RequestFeature(WindowFeatures.NoTitle);
            Com.Pixate.Freestyle.PixateFreestyle.Init(this);

            FacebookSdk.SdkInitialize(this.ApplicationContext);

            mProfileTracker = new MyProfileTracker();
            mProfileTracker.mOnProfileChanged += mProfileTracker_mOnProfileChanged;
            mProfileTracker.StartTracking();
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            

            Button btnFacebook = FindViewById<Button>(Resource.Id.btnFacebook);
            if (AccessToken.CurrentAccessToken != null)
            {
               //The user is logged in through Facebook
                btnFacebook.Text = "Logged Out";                
            }


            // LoginButton button = FindViewById<LoginButton>(Resource.Id.btnFacebook);
            //  button.SetReadPermissions("user_friends");

            mCallBackManager = CallbackManagerFactory.Create();
            // button.RegisterCallback(mCallBackManager, this);

            LoginManager.Instance.RegisterCallback(mCallBackManager, this);




            btnFacebook.Click += (o, e) =>
            {
                if (AccessToken.CurrentAccessToken != null)
                {
                    //The user is logged in through Facebook
                    LoginManager.Instance.LogOut();
                    btnFacebook.Text = "Logue com Facebook";
                }
                else
                {
                    //The user is not logged in
                    LoginManager.Instance.LogInWithReadPermissions(this, new List<string> { "public_profile", "user_friends" });
                    btnFacebook.Text = "Sair";
                }
            };


        }

        private void mProfileTracker_mOnProfileChanged(object sender, OnProfileChangedEventArgs e)
        {
            Pessoa alguem = new Pessoa();
            if (e.mProfile != null)
            {
                try
                {         
                    alguem.primeiroNome = e.mProfile.FirstName;
                    alguem.sobrenome = e.mProfile.LastName;
                    alguem.nome = e.mProfile.Name;
                }

                catch (Exception ex)
                {
                    //Handle error
                }
            }

            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetTitle("Mensagem");
            builder.SetMessage("" + alguem.primeiroNome + ", " + alguem.sobrenome + ", " + alguem.nome);
            builder.SetCancelable(false);
            builder.SetPositiveButton("OK", (senderAlert, args) => { });
            builder.Show();


        }
  


        public void OnCancel()
        {
            //throw new NotImplementedException();
        }

        public void OnError(FacebookException error)
        {
           // throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            LoginResult loginResult = result as LoginResult;
            Console.WriteLine(AccessToken.CurrentAccessToken.UserId);
        }


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            mCallBackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }

        protected override void OnDestroy()
        {
            mProfileTracker.StopTracking();
            base.OnDestroy();
        }

    }
}

