using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.API.ViewModels.ClaS;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    //[Authorize]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly IMapper _mapper;

        public ClassController(IClassService classService, IMapper mapper)
        {
            _classService = classService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/classes/create")]

        public async Task<IActionResult> Create([FromBody] CreateClassViewModel createclassViewModel)
        {
            try
            {
                var classDTO = _mapper.Map<ClassDTO>(createclassViewModel);
                var classCreated = await _classService.Create(classDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Aluno criado com sucesso",
                    Success = true,
                    Data = classCreated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            // catch (Exception)
            // {
            //     return StatusCode(500, Responses.ApplicationErrorMessage());
            // }
        }

        [HttpPut]
        [Route("/api/v1/classes/update")]

        public async Task<IActionResult> Update([FromBody] UpdateClassViewModel updateClassViewModel)
        {
            try
            {
                var classDTO = _mapper.Map<ClassDTO>(updateClassViewModel);

                var teacherUpdated = await _classService.Update(classDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuario atualizado om sucesso!",
                    Success = true,
                    Data = teacherUpdated
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpDelete]
        [Route("/api/v1/classes/remove")]

        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                var teacherExists = await _classService.Get(id);

                if (teacherExists == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Aluno não encontrado!",
                        Success = false,
                        Data = null
                    });

                await _classService.Remove(id);

                return Ok(new ResultViewModel
                {
                    Message = "Aluno removido om sucesso!",
                    Success = true,
                    Data = null
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/classes/get/{id}")]

        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var teacher = await _classService.Get(id);

                if (teacher == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuario não encontrado!",
                        Success = true,
                        Data = teacher
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado om sucesso!",
                    Success = true,
                    Data = teacher
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }

        [HttpGet]
        [Route("/api/v1/classes/get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allTeachers = await _classService.Get();


                return Ok(new ResultViewModel
                {
                    Message = "Usuarios encontrados om sucesso!",
                    Success = true,
                    Data = allTeachers
                });
            }
            catch (DomainException ex)
            {
                return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Responses.ApplicationErrorMessage());
            }
        }       
    }
}
