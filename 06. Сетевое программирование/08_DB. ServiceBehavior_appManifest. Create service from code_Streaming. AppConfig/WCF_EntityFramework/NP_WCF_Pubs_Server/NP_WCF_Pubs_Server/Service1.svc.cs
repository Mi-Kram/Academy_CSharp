using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NP_WCF_Pubs_Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service1 : IService1
    {
        pubsEntities context = new pubsEntities();

        public void AddAuthor(MyAuthor myAuthor)
        {
            try
            {
                author author = new author
                {
                    au_id = myAuthor.Au_id,
                    au_fname = myAuthor.FirstName,
                    au_lname = myAuthor.LastName,
                    state = myAuthor.State,
                    city = myAuthor.City,
                    address = myAuthor.Address,
                    zip = myAuthor.Zip,
                    contract = myAuthor.Contract,
                    phone = myAuthor.Phone
                };

                context.authors.Add(author);

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.InnerException?.ToString());
            }
        }

        public void UpdateAuthor(MyAuthor myAuthor)
        {
            string selectedId = myAuthor.Au_id;

            author selectedAuthorDB = (from t in context.authors
                                       where t.au_id == selectedId
                                       select t).First();

            selectedAuthorDB.au_id = myAuthor.Au_id;
            selectedAuthorDB.au_fname = myAuthor.FirstName;
            selectedAuthorDB.au_lname = myAuthor.LastName;
            selectedAuthorDB.city = myAuthor.City;
            selectedAuthorDB.address = myAuthor.Address;
            selectedAuthorDB.state = myAuthor.State;
            selectedAuthorDB.phone = myAuthor.Phone;
            selectedAuthorDB.zip = myAuthor.Zip;
            selectedAuthorDB.contract = myAuthor.Contract;

            context.SaveChanges();
        }

        public void DeleteAuthor(string au_id)
        {
            author del_author = (from t in context.authors
                                 where t.au_id == au_id
                                 select t).First();

            // Удалить из БД автора с данным ID
            context.authors.Remove(del_author);

            // Синхронизировать изменения
            context.SaveChanges();
        }

        public List<MyAuthor> GetAuthors()
        {
            var result = from t in context.authors
                         select new MyAuthor
                         {
                             Au_id = t.au_id,
                             FirstName = t.au_fname,
                             LastName = t.au_lname,
                             City = t.city,
                             State = t.state,
                             Phone = t.phone,
                             Address = t.address,
                             Zip = t.zip,
                             Contract = t.contract
                         };

            return new List<MyAuthor>(result);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
