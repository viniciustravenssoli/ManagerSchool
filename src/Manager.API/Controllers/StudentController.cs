using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Manager.API.Utilities;
using Manager.API.ViewModels;
using Manager.Core.Exceptions;
using Manager.Services.DTO;
using Manager.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    //[Authorize]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;

        public StudentController(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/students/create")]

        public async Task<IActionResult> Create([FromBody] CreateStudentViewModel studentViewModel)
        {
            try
            {
                var studentDTO = _mapper.Map<StudentDTO>(studentViewModel);
                var studentCreated = await _studentService.Create(studentDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Aluno criado com sucesso",
                    Success = true,
                    Data = studentCreated
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
        [Route("/api/v1/students/update")]

        public async Task<IActionResult> Update([FromBody] UpdateStudentViewModel studentViewModel)
        {
            try
            {
                var studentDTO = _mapper.Map<StudentDTO>(studentViewModel);

                var studentUpdated = await _studentService.Update(studentDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Usuario atualizado om sucesso!",
                    Success = true,
                    Data = studentUpdated
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
        [Route("/api/v1/students/remove")]

        public async Task<IActionResult> Remove(long id)
        {
            try
            {
                var studentExists = await _studentService.Get(id);

                if (studentExists == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Aluno não encontrado!",
                        Success = false,
                        Data = null
                    });

                await _studentService.Remove(id);

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
        [Route("/api/v1/students/get/{id}")]

        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var student = await _studentService.Get(id);

                if (student == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Usuario não encontrado!",
                        Success = true,
                        Data = student
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado om sucesso!",
                    Success = true,
                    Data = student
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
        [Route("/api/v1/students/get-all")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var allStudents = await _studentService.Get();
                

                return Ok(new ResultViewModel
                {
                    Message = "Usuarios encontrados om sucesso!",
                    Success = true,
                    Data = allStudents
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
        [Route("/api/v1/students/get-by-email")]

        public async Task<IActionResult> GetByEmail([FromQuery] string email)
        {
            try
            {
                var student = await _studentService.GetByEmail(email);

                if (student == null)
                    return Ok(new ResultViewModel
                    {
                        Message = "Nenhum Usuario encontrado com esse email!",
                        Success = true,
                        Data = student
                    });

                return Ok(new ResultViewModel
                {
                    Message = "Usuario encontrado com sucesso!",
                    Success = true,
                    Data = student
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
        [Route("/api/v1/students/search-by-name")]

        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            try
            {
                var allStudents = await _studentService.SearchByName(name);

                if (allStudents.Count == 0)
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
                    Data = allStudents
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
        [Route("/api/v1/students/search-by-email")]

        public async Task<IActionResult> SearchByEmail([FromQuery] string email)
        {
            try
            {
                var allStudents = await _studentService.SearchByEmail(email);

                if (allStudents.Count == 0)
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
                    Data = allStudents
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