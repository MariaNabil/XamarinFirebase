using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Gms.Extensions;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using Xamarin.Forms;

[assembly: Dependency(typeof(offlineFirebase.Droid.PersonRepository))]
namespace offlineFirebase.Droid
{
    public class PersonRepository : Java.Lang.Object, IFirebaseRepository<Persons>, IOnSuccessListener, Firebase.Firestore.IEventListener 
    {
        List<Persons> listOfPersons = new List<Persons>();
        public async Task<List<Persons>> NewGetAll()
        {
            List<Persons> allPersons = new List<Persons>();
            /*var all = await FirestoreService.Instance
                .Collection("persons").Get().AddOnSuccessListener(this);*/
            var all = await FirestoreService.Instance.Collection("persons")
                .WhereEqualTo("author", CurrentUser.Uid).Get()
                .AddOnSuccessListener(this);
            return listOfPersons;
        }

        public async Task<List<Persons>> NewGetAllAndListens()
        {
            var all =  FirestoreService.Instance
                .Collection("persons").WhereEqualTo("author", CurrentUser.Uid).AddSnapshotListener(this);
            return listOfPersons;
        }
        public static Persons DocumentSnapshotToPersons(DocumentSnapshot documentSnapshot)
        {
            int id = int.Parse(documentSnapshot.Get("PersonId").ToString());
            string name = documentSnapshot.Get("Name").ToString();
            int age = int.Parse(documentSnapshot.Get("Age").ToString());
            return new Persons(id, name, age);
        }
        public Persons GetOne(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Persons> GetOneAsync(string id)
        {
            throw new NotImplementedException();
        }

        public void OnSuccess(Java.Lang.Object result)
        {
            var snapshot = (QuerySnapshot)result;

            listOfPersons = new List<Persons>();
            if (!snapshot.IsEmpty)
            {
                var documents = snapshot.Documents;
                listOfPersons = documents.ToList().ConvertAll(new Converter<DocumentSnapshot, Persons>(DocumentSnapshotToPersons));
            }
        }

        public async void OnEvent(Java.Lang.Object value, FirebaseFirestoreException error)
        {
            try
            {
                var snapshot = (QuerySnapshot)value;

                if (!snapshot.IsEmpty)
                {
                    var documents = snapshot.Documents;
                    listOfPersons.Clear();
                    listOfPersons = documents.ToList().ConvertAll(new Converter<DocumentSnapshot, Persons>(DocumentSnapshotToPersons));
                }
                MainPage.updateListView(listOfPersons);
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
            }
        }

        public bool SetPerson(int id,string name, int age)
        {
            try
            {
                HashMap map = new HashMap();
                map.Put("Name", name);
                map.Put("PersonId", id);
                map.Put("Age", age);
                map.Put("author", CurrentUser.Uid);
                DocumentReference docRef = FirestoreService.Instance.Collection("persons").Document();
                docRef.Set(map);
                return true;
            }catch(Java.Lang.Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }
    }
}