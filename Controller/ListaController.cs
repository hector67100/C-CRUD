using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using Testcrud.Data;
using Testcrud.Models;

namespace Testcrud.Controllers
{
    [ApiController]
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    public class ListaController: ControllerBase
    {
        private readonly ApplicationDbContext DBContext;

        public ListaController(ApplicationDbContext DBContext)
        {
            this.DBContext = DBContext;
        }

        [HttpGet("GetLista")]
        public async Task<ActionResult<List<lista>>> Get()
        {
            var List = await DBContext.Lista.Select(
                s => new lista
                {
                    idLista = s.idLista ,
                    tarea = s.tarea,
                    listo = s.listo

                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }
    
        [HttpGet("GetListaId/{idLista}")]
        public async Task<ActionResult<lista>> GetById(int idLista)
        {
            lista ListaRecibida = await DBContext.Lista.Select(
                s => new lista
                {
                    idLista = s.idLista,
                    tarea = s.tarea,
                    listo = s.listo

                }
            ).FirstOrDefaultAsync(s => s.idLista == idLista);

            if (ListaRecibida == null)
            {
                return NotFound();
            }
            else
            {
                return ListaRecibida;
            }
        }

        [HttpPost("InsertLista")]
        public async Task<HttpStatusCode> InsertarLista(lista s)
        {
            var entity = new lista()
                {
                    idLista = s.idLista ,
                    tarea = s.tarea,
                    listo = s.listo

                };
            DBContext.Lista.Add(entity);
            await DBContext.SaveChangesAsync();

            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateLista")]
        public async Task<HttpStatusCode> UpdateLista(lista listaRecibida)
        {
            var entity = await DBContext.Lista.FirstOrDefaultAsync(s => s.idLista == listaRecibida.idLista);
                if (entity != null)
                {
                    entity.idLista = listaRecibida.idLista;
                    entity.tarea = listaRecibida.tarea;
                    entity.listo = listaRecibida.listo;
                     await DBContext.SaveChangesAsync();
                     return HttpStatusCode.OK;
                }
            return HttpStatusCode.NoContent;
        }

        [HttpPut("UpdateListas")]
        public async Task<HttpStatusCode> UpdateListas(lista[] listaRecibida)
        {
            for(int i=0; i<listaRecibida.Length; i++)
            {
                var entity = await DBContext.Lista.FirstOrDefaultAsync(s => s.idLista == listaRecibida[i].idLista);
                if(entity != null)
                {
                    entity.idLista = listaRecibida[i].idLista;
                    entity.tarea = listaRecibida[i].tarea;
                    entity.listo = listaRecibida[i].listo;
                    
                }
            }
            
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }


        [HttpDelete("DeleteLista/{id}")]
        public async Task<HttpStatusCode> DeleteLista(int id)
        {
            var entity = new lista()
            {
                idLista = id,
            };

            DBContext.Lista.Remove(entity);
            await DBContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }
}