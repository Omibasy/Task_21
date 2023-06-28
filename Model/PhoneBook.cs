using System.Data.Entity.Migrations;
using Task_20.Model.Data;
using Task_20.Model.Repositorys;

namespace Task_20.Model
{
    public class PhoneBook
    {
        private const string dbName = "DbConnection";

        public PhoneBook()
        {
            List<Person> list = GetPersonalities();

            if (list == null || list.Count <= 0)
            {
                FillingInDatav();
            }
        }

        public List<Person> GetPersonalities()
        {
            using (DbPhoneBook dbPhoneBock = new DbPhoneBook(dbName))
            {
                return dbPhoneBock.DatabasePerson.ToList();
            }
        }

        private void FillingInDatav()
        {
            using (DbPhoneBook db = new DbPhoneBook(dbName))
            {
                foreach (var item in Repository.GetPersonalities())
                {
                    db.DatabasePerson.Add(item);
                }

                foreach (var item in Repository.GetPersonalData())
                {
                    db.DatabasePersonalData.Add(item);
                }

                db.SaveChanges();
            }
        }

        public Task<Person> GetPersonalities(int id) 
        {
            return Task<Person>.Factory.StartNew(() => 
            {
                using (DbPhoneBook db = new DbPhoneBook(dbName)) 
                {
                   return  db.DatabasePerson.FirstOrDefault(o => o.ID == id);
                }              
            });      
        }

        public Task<PersonalData> GetPersonalData(int personId) 
        {
            return Task<PersonalData>.Factory.StartNew(() =>
            {
                using (DbPhoneBook db = new DbPhoneBook(dbName))
                {
                    return db.DatabasePersonalData.FirstOrDefault(o => o.ID_Person == personId);
                }

            });

        }

        private void DeletePhoto(int id, IWebHostEnvironment appEnvironment)
        {
          
                string path = "/photo/" + $"photo_{id}.jpg";

                FileInfo fileInf = new FileInfo(appEnvironment.WebRootPath + path);

                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }
            

        }

        private Person DeletePersonalities(int id)
        {
            Person value;

            using (DbPhoneBook db = new DbPhoneBook(dbName))
            {  
                value =  db.DatabasePerson.FirstOrDefault(x => x.ID == id);

                if (value != null)
                {
                    db.DatabasePerson.Remove(value);

                    db.SaveChanges();
                }           
            }

            return value;
        }

        public Person DeleteAnEntry(int id, IWebHostEnvironment appEnvironment)
        {

            if (id > 0)
            {
                DeletePhoto(id, appEnvironment);

                return  DeletePersonalities(id);

            }
            else
            {
                return null;        
            }
        }

        public Task<Person> DataChanges(Person person, PersonalData personalData)
        {
            
            personalData.PersonID = person;
            personalData.PhoneNumber = EditPhoneNumber(personalData.PhoneNumber);

            using (DbPhoneBook db = new DbPhoneBook(dbName))
            {
                db.DatabasePerson.AddOrUpdate(person);
                db.DatabasePersonalData.AddOrUpdate(personalData);            

                db.SaveChanges();
            }
        
            return Task<Person>.Factory.StartNew(() => 
            {
                return person;
            });

        }

        public void AddingNewData(Person person,
                                        PersonalData personalData, 
                                        IFormFile file, 
                                        IWebHostEnvironment appEnvironment)
        {
            
                int id;

                using (DbPhoneBook db = new DbPhoneBook(dbName))
                {

                    personalData.PersonID = person;
                    personalData.PhoneNumber = EditPhoneNumber(personalData.PhoneNumber);

                    db.DatabasePerson.Add(person);
                    db.DatabasePersonalData.Add(personalData);

                    db.SaveChanges();

                    id = db.DatabasePerson.FirstOrDefault(o => o.Name == person.Name).ID;
                }

                SavePhoto(id, file, appEnvironment);

           ;          
        }

        private string EditPhoneNumber(string phoneNumder)
        {
            phoneNumder = phoneNumder.Replace("(", string.Empty);
            phoneNumder = phoneNumder.Replace(")", string.Empty);
            phoneNumder = phoneNumder.Replace(" ", "-");
            phoneNumder = phoneNumder.Remove(0, 1);

            return phoneNumder;
        }

        private void SavePhoto(int id, IFormFile incomingFile, IWebHostEnvironment appEnvironment)
        {
           

                if (incomingFile != null)
                {
                    string path = "/photo/" + $"photo_{id}.jpg";

                    using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        incomingFile.CopyToAsync(fileStream);
                    }
                }
            
        }


        public void EditPhoto(int id, IFormFile newPhoto, IWebHostEnvironment appEnvironment)
        {
            DeletePhoto(id, appEnvironment);
            SavePhoto(id, newPhoto, appEnvironment);
        }
    }
}
