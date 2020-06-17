using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        private MyContext db = new MyContext();

        [Authorize]
        // GET: api/Usuario - Select All Usuarios
        public IQueryable<Usuario> GetUsuarios()
        {
            return db.Usuarios;
        }

        [Authorize]
        // GET: api/Usuario/5 - Select Usuario by Id
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(int id)
        {
            Usuario usuario = db.Usuarios.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Authorize]
        // GET: api/Usuario/?login=login&senha=senha - Select Usuario by login and password
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(string login, string senha)
        {
            Expression<Func<Usuario, bool>> filter = x => x.Login == login && x.Senha == senha;
            //Usuario user = (from u in db.Usuarios where u.Login == login && u.Senha == senha select u).FirstOrDefault();
            Usuario usuario = db.Usuarios.AsQueryable().Where(filter).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        [Authorize]
        // PUT: api/Usuario/5 - Update Usuario
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUsuario(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            db.Entry(usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        // POST: api/Usuario - Insert Usuario
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            return Ok();
            //return CreatedAtRoute("DefaultApi", new { id = usuario.Id }, usuario);
        }

        [Authorize]
        // DELETE: api/Usuario/5 - Remove Usuario
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(int id)
        {
            Usuario usuario = db.Usuarios.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuarios.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(int id)
        {
            return db.Usuarios.AsQueryable().Count(e => e.Id == id) > 0;
        }
    }
}