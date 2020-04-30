using System;
using System.Threading.Tasks;
using Firebase.Auth;
using offlineFirebase.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthDroid))]
namespace offlineFirebase.Droid
{
    public class AuthDroid : IAuth
    {
        FirebaseAuth auth;
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            auth = FirebaseAuth.GetInstance(FirestoreService.Instance.App);
            try
            {
                var user = await auth.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdTokenAsync(false);
                CurrentUser.Uid = user.User.Uid;
                return token.Token;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return "error = " + msg;
            }
        }

        public string SignUpWithEmailPassword(string email, string password)
        {
            try
            {
                var signUpTask = auth.CreateUserWithEmailAndPassword(email, password);
                return "";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}