using MicroService.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace MicroService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SafraController : ControllerBase
    {
        public DateTime dataAtual = DateTime.Now;
        [HttpGet]
        public object GetUsers()
        {
            using (EntityModelContext context = new EntityModelContext())
            {
                return context.Safra
                              .AsNoTracking()
                              .ToList();
            }
        }
        [HttpPost]
        public object PostUsers([FromBody] Safra InfosSafra)
        {
            using (EntityModelContext context = new EntityModelContext())
            {
                bool existsNomeSafra = context.Safra.Any(safra => safra.Nome == InfosSafra.Nome);
                if (existsNomeSafra)
                    return new { success = false, code = 200, msg = "Já possui este nome de Safra cadastrado em nossa base de dados" };

                context.Safra.Add(new Safra
                {
                    Nome = InfosSafra.Nome,
                    Ano = InfosSafra.Ano
                });
                context.SaveChanges();
                return new { success = true, code = 200, msg = "Cadastro realizado com sucesso" };
            }
        }

        [HttpPut]
        public object UpdateUsers([FromBody] Safra InfosSafra)
        {
            using (EntityModelContext context = new EntityModelContext())
            {
                Safra safra = context.Safra.Where(s => s.IdSafra == InfosSafra.IdSafra).FirstOrDefault();
                if (safra == null)
                    return new { success = false, code = 200, msg = "Não exists esta Safra em nossa base de dados" };

                safra.Nome = string.IsNullOrEmpty(InfosSafra.Nome) ? safra.Nome : InfosSafra.Nome;
                safra.Ano = InfosSafra.Ano == null ? safra.Ano : InfosSafra.Ano;

                context.SaveChanges();
                return new { success = true, code = 200, msg = "Atualização realizada com sucesso" };
            }
        }
        [HttpDelete]
        public object DeleteUsers([FromBody] Safra InfosSafra) /*Delete gralmente realiza a troca de chave de ativo = 1 para ativo = 0. Porém o teste era um crud e decidi fazer o delete mesmo*/
        {
            using (EntityModelContext context = new EntityModelContext())
            {
                Safra safraDelete = context.Safra.Where(a => a.IdSafra == InfosSafra.IdSafra).FirstOrDefault();
                if (safraDelete == null)
                    return new { success = false, code = 200, msg = "Safra não encontrada"};

                context.Safra.Remove(safraDelete);
                context.SaveChanges();
                return new { success = true, code = 200, msg = $"Safra {safraDelete.Nome} deletado(a) com sucesso" };
            }
        }
    }
}
