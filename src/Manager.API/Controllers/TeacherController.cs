using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.API.ViewModels.Teachers;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    //[Authorize]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly IMapper _mapper;

        public TeacherController(ITeacherService teacherservice, IMapper mapper)
        {
            _teacherService = teacherservice;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/teachers/create")]

        public async Task<IActionResult> Create([FromBody] CreateTeacherViewModel teacherViewModel)
        {
            try
            {
                var teacherDTO = _mapper.Map<TeacherDTO>(teacherViewModel);
                var teacherCreated = await _teacherService.Create(teacherDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Aluno criado com sucesso",
                    Success = true,
                    Data = teacherCreated
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

        [HttpPut]
        [Route("/api/v1/teachers/update")]

        public async Task<IActionResult> Update([FromBody] UpdateTeacherViewModel updateTeacherViewModel)
        {
            try
            {
                var teacherDTO = _mapper.Map<TeacherDTO>(updateTeacherViewModel);

                var teacherUpdated = await _teacherService.Update(teacherDTO);

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
        [Route("/api/v1/teachers/remove")]

        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                var teacherExists = await _teacherService.Get(id);

                if (teacherExists == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Aluno não encontrado!",
                        Success = false,
                        Data = null
                    });

                await _teacherService.Remove(id);

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
        [Route("/api/v1/teachers/get/{id}")]

        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var teacher = await _teacherService.Get(id);

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
        [Route("/api/v1/teachers/get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allTeachers = await _teacherService.Get();
                

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

        [HttpGet]
        [Route("/api/v1/teachers/get-by-email")]

        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                var teacher = await _teacherService.GetByEmail(email);

                if (teacher == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuario encontrado com esse email!",
                        Success = true,
                        Data = teacher
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado com sucesso!",
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
        [Route("/api/v1/teachers/search-by-name")]

        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var allTeachers = await _teacherService.SearchByName(name);

                if (allTeachers.Count == 0)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuario encontrado com esse nome!",
                        Success = true,
                        Data = null
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado com sucesso!",
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

        [HttpGet]
        [Route("/api/v1/teachers/search-by-email")]

        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            try
            {
                var allTeachers = await _teacherService.SearchByEmail(email);

                if (allTeachers.Count == 0)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuario encontrado com esse email!",
                        Success = true,
                        Data = null
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado com sucesso!",
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