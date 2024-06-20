using ContactManagementSystemAPI.Data;
using ContactManagementSystemAPI.IRepository;
using ContactManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ContactManagementSystemAPI.Repository
{    
    public class ContactJsonRepository
    {
        string filePath = "Data/DataBase.json";
        public List<Contact> ReadJSONFile()
        {
            var serializer = new JsonSerializer();
            List<Contact> context = new();
            using (var streamReader = new StreamReader(@filePath))
            using (var textReader = new JsonTextReader(streamReader))
            {
                context = serializer.Deserialize<List<Contact>>(textReader);
                if (context == null)
                    context = new List<Contact>();
            }
            return context;
        }

        public async Task<List<Contact>> GetAll()
        {
            //return await context.Set<T>().ToListAsync();
            var context = ReadJSONFile();
            return context;
        }

        public Contact GetById(string Id)
        {
            //return await context.Set<T>().FindAsync(Id);
            var context = ReadJSONFile();
            return context.FirstOrDefault(p => p.Id == Id);
        }

        public void Add(Contact entity)
        {
            //context.Set<T>().Add(entity);
            var context = ReadJSONFile();
            entity.Id = Guid.NewGuid().ToString();
            context.Add(entity);
            SaveJSONFile(context);
        }

        public void AddRange(List<Contact> entity)
        {
            //context.Set<T>().AddRange(entity);
            var context = ReadJSONFile();
            context.AddRange(entity);
            SaveJSONFile(context);
        }

        public bool Delete(string Id)
        {
            try
            {
                //var entity = await context.Set<T>().FindAsync(Id);
                //if (entity != null)
                //    context.Set<T>().Remove(entity);

                var context = ReadJSONFile();
                var data = context.FirstOrDefault(p => p.Id == Id);
                context.Remove(data);
                SaveJSONFile(context);
                return true;
            }
            catch { return false; }
        }

        public void Update(Contact entity)
        {
            //context.Set<T>().Update(entity);
            var context = ReadJSONFile();
            var data = context.FirstOrDefault(p => p.Id == entity.Id);
            data.FirstName = entity.FirstName;
            data.LastName = entity.LastName;
            data.Email = entity.Email;
            SaveJSONFile(context);
        }

        public List<Contact> Search(string text)
        {
            var context = ReadJSONFile();
            var query = from p in context
                        where (p.FirstName.ToLower().Contains(text.ToLower())
                              || p.LastName.ToLower().Contains(text.ToLower())
                              || p.Email.ToLower().Contains(text.ToLower()))
                        select p;
            return query.ToList();
        }

        //public async Task<int> Commit()
        //{
        //    return await context.SaveChangesAsync();
        //}

        void SaveJSONFile(List<Contact> context)
        {
            using (StreamWriter file = File.CreateText(@filePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, context);
            }
        }
    }
}
