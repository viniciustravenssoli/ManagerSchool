using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Manager.Core.Exceptions;
using Manager.Domain.Entities;
using Manager.Infra.Interfaces;
using Manager.Services.DTO;
using Manager.Services.Interface;

namespace Manager.Services.Services
{
    public class BoletimService : IBoletimService
    {
        private readonly IMapper _mapper;
        private readonly IBoletimRepository _boletimRepository;

        public BoletimService(IMapper mapper, IBoletimRepository boletimRepository)
        {
            _mapper = mapper;
            _boletimRepository = boletimRepository;
        }

        public async Task<BoletimDTO> Create(BoletimDTO boletimDTO)
        {
            var teacherExists = await _boletimRepository.GetById(boletimDTO.BoletimId);
            if (teacherExists != null)
                throw new DomainException("Já existe um usuario cadastrado com esse email");

            var boletim = _mapper.Map<Boletim>(boletimDTO);
            boletim.CalcularNotaFinal(boletim.Nota1, boletim.Nota2);
            //teacher.Validate();

            var boletimCreated = await _boletimRepository.Create(boletim);

            return _mapper.Map<BoletimDTO>(boletimCreated);
        }

        public Task<Boletim> Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Boletim>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Boletim> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task Remove(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Boletim> Update(Boletim teacherDTO)
        {
            throw new NotImplementedException();
        }
    }
}