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
using Microsoft.AspNetCore.Mvc;

namespace Manager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoletimController : ControllerBase
    {
        private readonly IBoletimService _boletimService;
        private readonly IMapper _mapper;

        public BoletimController(IBoletimService boletimService, IMapper mapper)
        {
            _boletimService = boletimService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/v1/boletim/create")]

        public async Task<IActionResult> Create([FromBody] CreateBoletimViewModel createclassViewModel)
        {
            try
            {
                var classDTO = _mapper.Map<BoletimDTO>(createclassViewModel);
                var classCreated = await _boletimService.Create(classDTO);

                return Ok(new ResultViewModel
                {
                    Message = "Class created with success",   
                    Success = true,
                    Data = classCreated
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