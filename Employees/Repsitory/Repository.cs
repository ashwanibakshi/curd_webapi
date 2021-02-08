using Employees;
using Employees.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Employees.Repsitory
{
    public class Repository
    {
        public Author chkAuthor(string auth)
        {
            using (BookContext db = new BookContext())
            {
                Author authr = new Author();
                if (db.Authors.Where(author => author.name == auth).Count() == 0)
                {
                    authr.name = auth;
                    db.Authors.Add(authr);
                    db.SaveChanges();
                    authr = db.Authors.Where(author => author.name == auth).SingleOrDefault();
                }
                else
                {
                    authr = db.Authors.Where(author => author.name == auth).SingleOrDefault();
                }
                return authr;
            }
        }
        public Gener chkGenere(string gname)
        {
            using (BookContext db = new BookContext())
            {
                Gener gen = new Gener();
                if (db.Geners.Where(gener => gener.gname == gname).Count() == 0)
                {
                    gen.gname = gname;
                    db.Geners.Add(gen);
                    db.SaveChanges();
                    gen = db.Geners.Where(gener => gener.gname == gname).SingleOrDefault();
                }
                else
                {
                    gen = db.Geners.Where(gener => gener.gname == gname).SingleOrDefault();
                }
                return gen;
            }
        }

        public void updateBook(AuthBook authBook)
        {

            using (BookContext db = new BookContext())
            {
                Author authr = new Author();
                Book bk = new Book();
                Gener gen = new Gener();

                //author
                authr = db.Authors.Find(authBook.Auhtor_id);
                authr.name = authBook.Author_Name;

                //book
                bk = db.Books.Find(authBook.Book_id);
                bk.bname = authBook.Book_Name;

                //genres
                gen = db.Geners.Find(authBook.Gener_id);
                gen.gname = authBook.Gener_Name;

                db.SaveChanges();
            }
        }

        
        public int deleteBook(int id)
        {
            using (BookContext DB = new BookContext())
            {
                Book bk = new Book();
                bk = DB.Books.Find(id);
                DB.Books.Remove(bk);
                int i = DB.SaveChanges();
                return i;
            }
        }

        public AuthBook findBook(int id)
        {
            using (var Db = new BookContext())
            {
                var query = Db.Books.Include("Authors").
                    Include("Geners").Where(p => p.Id == id)
                    .Select(x => new AuthBook {
                       Book_id=x.Id,
                       Book_Name = x.bname,
                       Author_Name = x.Author.name,
                       Auhtor_id = x.Author.Id,
                       Gener_id = x.Gener.Id,
                       Gener_Name = x.Gener.gname
                    }).SingleOrDefault();
         
                return query;
            }
        }
    }
}
    
