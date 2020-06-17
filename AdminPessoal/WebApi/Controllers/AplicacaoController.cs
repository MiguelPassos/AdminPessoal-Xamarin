using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class AplicacaoController : ApiController
    {
        private MyContext db = new MyContext();

        [Authorize]
        // GET: api/Aplicacao
        public IQueryable<Aplicacao> GetAplicacao()
        {
            return db.Aplicacoes.Where(x => x.Ativo == true);
        }

        [Authorize]
        // GET: api/Aplicacao/5
        [ResponseType(typeof(Aplicacao))]
        public IHttpActionResult GetAplicacao(int id)
        {
            Aplicacao aplicacao = db.Aplicacoes.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
            if (aplicacao == null)
            {
                return NotFound();
            }

            return Ok(aplicacao);
        }

        [Authorize]
        // GET: api/Aplicacao/5
        [ResponseType(typeof(Aplicacao))]
        public IHttpActionResult GetAplicacao(string nome, string senha)
        {
            Expression<Func<Aplicacao, bool>> filter = x => x.Nome == nome && x.Senha == senha && x.Ativo == true;
            Aplicacao aplicacao = db.Aplicacoes.AsQueryable().Where(filter).FirstOrDefault();
            if (aplicacao == null)
            {
                return NotFound();
            }

            return Ok(aplicacao);
        }

        [Authorize]
        // PUT: api/Aplicacao/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAplicacao(int id, Aplicacao aplicacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != aplicacao.Id)
            {
                return BadRequest();
            }

            db.Entry(aplicacao).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AplicacaoExists(id))
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
        // POST: api/Aplicacao
        [ResponseType(typeof(Aplicacao))]
        public IHttpActionResult PostAplicacao(Aplicacao aplicacao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Aplicacoes.Add(aplicacao);
            db.SaveChanges();

            return Ok();
            //return CreatedAtRoute("DefaultApi", new { id = aplicacao.Id }, aplicacao);
        }

        [Authorize]
        // DELETE: api/Aplicacao/5
        [ResponseType(typeof(Aplicacao))]
        public IHttpActionResult DeleteAplicacao(int id)
        {
            Aplicacao aplicacao = db.Aplicacoes.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
            if (aplicacao == null)
            {
                return NotFound();
            }

            //aplicacao.Ativo = false;

            //db.Entry(aplicacao);

            db.Aplicacoes.Remove(aplicacao);
            db.SaveChanges();

            return Ok(aplicacao);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AplicacaoExists(int id)
        {
            return db.Aplicacoes.Count(e => e.Id == id) > 0;
        }
    }
}