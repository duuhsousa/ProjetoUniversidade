using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProjetoUniversidade.Dados;
using ProjetoUniversidade.Models;

namespace ProjetoUniversidade.Controllers
{
    [Route("Area")]
    public class AreaController:Controller
    {
        readonly UniversidadeContexto contexto;

        public AreaController(UniversidadeContexto Contexto){
            this.contexto = Contexto;
        }

        /// <summary>
        /// Listar Area
        /// </summary>
        /// <returns>Lista de Areas</returns>
        /// <response code="200">Retorna uma lista de areas</response>
        /// <response code="400">Ocorreu um erro</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<Area>),200)]
        [ProducesResponseType(typeof(string),400)]
        public IEnumerable<Area> ListarArea(){
            return contexto.Area.ToList();
        }


        /// <summary>
        /// Busca uma área pelo seu ID
        /// </summary>
        /// <param name="id">ID da área</param>
        /// <returns>Retorna uma área</returns>
        /// <response code="200">Retorna uma área</response>
        /// <response code="400">Ocorreu um erro</response>
        /// <response code="404">Area não encontrada</response>
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<Area>),200)]
        [ProducesResponseType(typeof(string),400)]
        [ProducesResponseType(typeof(string),404)]
        public Area ListarArea(int id){
                return contexto.Area.Where(a=>a.IdArea==id).FirstOrDefault();
        } 


        /// <summary>
        /// Cadastra uma nova área
        /// </summary>
        /// <param name="area">Nova área para registrar</param>
        /// <remarks>
        /// Modelo de dados que deve ser enviado para cadastrar para a area:
        /// 
        ///     POST /Area
        ///     {
        ///         "nome" : "nome da area"
        ///     }    
        /// </remarks>
        /// <response code="200">Retorna uma área cadastrada</response>
        /// <response code="400">Ocorreu um erro</response>
        [HttpPost]
        [ProducesResponseType(typeof(Area),200)]
        [ProducesResponseType(typeof(string),400)]
        public IActionResult PostarArea([FromBody] Area area){
            
            try{
                //Realiza a validação dos campos do modelo Area (DataAnotations)
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                contexto.Area.Add(area);

                int x = contexto.SaveChanges();

                if(x>0){
                    return Ok();
                }
                
            }
            catch(Exception ex){
                throw new Exception("Erro ao cadastrar: "+ex.Message);
            }

            return BadRequest();
        }


        /// <summary>
        /// Apaga uma area
        /// </summary>
        /// <param name="id">ID da area que sera apagada</param>
        /// <remarks>
        /// Modelo da área que irá ser apagada request:
        /// 
        ///     PUT /Area
        ///     {
        ///         "idarea" : 0,
        ///         "nomearea" : "Nome da área atualizado"
        ///     }
        /// </remarks>
        /// <returns>Retorna a area apagada</returns>
        /// <response code="200">Retorna uma área apagada</response>
        /// <response code="400">Ocorreu um erro</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Area),200)]
        [ProducesResponseType(typeof(string),400)]
        public IActionResult DeletarArea(int id){
            
            try{
                var area = contexto.Area.Where(a=>a.IdArea==id).FirstOrDefault();

                if(area==null){
                    return NotFound();
                }

                contexto.Area.Remove(area);

                int x = contexto.SaveChanges();

                if(x>0){
                    return Ok();
                }
            }
            catch(Exception ex){
                throw new Exception("Erro ao remover Area: "+ex.Message);
            }

            return BadRequest();
        }

        /// <summary>
        /// Atualiza uma área
        /// </summary>
        /// <param name="area">ID da área que vai ser atualizada</param>
        /// <remarks>
        /// Modelo da área que irá ser atualizada request:
        /// 
        ///     PUT /Area
        ///     {
        ///         "idarea" : 0,
        ///         "nomearea" : "Nome da área atualizado"
        ///     }
        /// </remarks>
        /// <returns>Retorna a area atualizada</returns>
        /// <response code="200">Retorna uma área cadastrada</response>
        /// <response code="400">Ocorreu um erro</response>
        [HttpPut]
        [ProducesResponseType(typeof(Area),200)]
        [ProducesResponseType(typeof(string),400)]
        public IActionResult AtualizarArea([FromBody]Area area){
        try{
            if(!ModelState.IsValid){
                return BadRequest();
            }   

            contexto.Area.Update(area);

            int x = contexto.SaveChanges();

            if(x>0){
                return Ok();
            }
        }
        catch(Exception ex){
            throw new Exception(ex.Message);
        }

        return BadRequest();

        }
    }
}