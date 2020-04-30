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
using Firebase;
using Firebase.Analytics;
using Firebase.Firestore;

namespace offlineFirebase.Droid
{
    public static class FirestoreService
    {
        private static Firebase.FirebaseApp app;
        public static FirebaseFirestore Instance
        {
            get
            {
                return FirebaseFirestore.GetInstance(app);
            }
        }

        public static string AppName { get; } = "SampleAppName";

        public static void Init(Android.Content.Context context)
        {
            var options = new FirebaseOptions.Builder()
.SetApplicationId("1:812309437328:android:25ce5624a3dfad6a55f115")
.SetApiKey("AIzaSyA8S9_ZKzmzRbebNsLOKxeBpI5r24-0D30")
.SetDatabaseUrl("https://authtrial-62711.firebaseio.com").SetProjectId("authtrial-62711").Build();
            
            app = Firebase.FirebaseApp.InitializeApp(context, options,AppName);
            FirebaseFirestoreSettings settings = new FirebaseFirestoreSettings.Builder()
.SetPersistenceEnabled(true).Build();
            FirebaseFirestore.GetInstance(app).FirestoreSettings = settings;
        }
    }
}