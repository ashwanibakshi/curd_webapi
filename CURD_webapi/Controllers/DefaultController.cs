using Employees;
using Employees.Repsitory;
using Employees.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CURD_webapi.Controllers
{
    public class DefaultController : ApiController
    {
        Repository repo = new Repository();
        // GET: api/Default
        [HttpGet]
        [ActionName("AllBook")]
        public HttpResponseMessage GetAllBook()
        {
            using (BookContext db = new BookContext())
            {
                var entity = (from Books in db.Books
                              join Authors in db.Authors on
                              Books.authors_Id equals Authors.Id
                              join Geners in db.Geners on
                              Books.geners_Id equals Geners.Id
                              select new { Authorname = Authors.name, Author_Id = Authors.Id, Book_Name = Books.bname,Book_Id = Books.Id, GenerName = Geners.gname, Gener_Id = Geners.Id }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, entity);
            }
        }

        // GET: api/Default/5
        [HttpGet]
        [ActionName("BookById")]
        public HttpResponseMessage FindBookById(int id)
        {
            using (BookContext db = new BookContext())
            {
                AuthBook authbook = repo.findBook(id);
                if (authbook != null)
                {
                    return this.Request.CreateResponse(HttpStatusCode.OK,authbook);
                }
                else
                 return this.Request.CreateResponse(HttpStatusCode.BadRequest,"no book found");
            }
        }

        // POST: api/Default

        [HttpPost]
        [ActionName("AddBook")]
        public HttpResponseMessage NewBook([FromBody]AuthBook data)
        {
            try
            { 
                using (var DB = new BookContext())
                {

                    //check if duplicate book
                    if (DB.Books.Where(b=>b.bname==data.Book_Name).Count()==0)
                    {
                        Author auth = new Author();
                        Gener gns = new Gener();
                        Book bks = new Book();
                        auth = repo.chkAuthor(data.Author_Name);
                        gns = repo.chkGenere(data.Gener_Name);
                                            
                        bks.bname = data.Book_Name;
                        bks.authors_Id = auth.Id;
                        bks.geners_Id = gns.Id;
                        DB.Books.Add(bks);
                        DB.SaveChanges();
                        List<Book> query = DB.Books.Where(b => b.authors_Id == auth.Id & b.geners_Id == gns.Id & b.bname==bks.bname).ToList();
                        return this.Request.CreateResponse(HttpStatusCode.OK,query);
                    }
                    else
                    return this.Request.CreateResponse(HttpStatusCode.Forbidden,"duplicate book");
                }
            }
            catch(Exception ex)
            {
                    return this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Default/5
        [HttpPut]
        [ActionName("UpdateBook")]
        public HttpResponseMessage UpdateBookData([FromBody]AuthBook value)
        { 
            try
            {
                if (ModelState.IsValid)
                {       
                        repo.updateBook(value);
                        return this.Request.CreateResponse(HttpStatusCode.OK,"data updated");
                }
                else
                    return this.Request.CreateResponse(HttpStatusCode.BadRequest, "fill all fields");
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden,ex);
            }
        }

        // DELETE: api/Default/5
        [HttpDelete]
        [ActionName("DelBook")]
        public HttpResponseMessage DeleteBookById([FromUri]int id)
        {
            if (repo.deleteBook(id) > 0)
            {
                return this.Request.CreateResponse(HttpStatusCode.OK, "book is deleted");
            }
            else
                return this.Request.CreateResponse(HttpStatusCode.Forbidden, "book not deleted");
        }
    }
}
