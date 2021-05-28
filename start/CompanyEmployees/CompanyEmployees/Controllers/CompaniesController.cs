using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployees.Controllers
{
	[Route("api/companies")]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		private readonly ICompanyRepository _repository;
		private readonly ILoggerManager _logger;
		private readonly IMapper _mapper;

		public CompaniesController(ICompanyRepository repository, ILoggerManager logger, IMapper mapper)
		{
			_repository = repository;
			_logger = logger;
			_mapper = mapper;
		}

		[HttpGet]
		public IActionResult GetCompanies()
		{
			var companies = _repository.GetAllCompanies();

			var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

			_logger.LogInfo("All companies fetched from the database");

			return Ok(companiesDto);
		}

		[HttpGet("{id}", Name = "CompanyById")]
		public IActionResult GetCompany(Guid id)
		{
			var company = _repository.GetCompany(id);
			if (company == null)
			{
				_logger.LogInfo($"Company with id: {id} doesn't exist in the database.");
				return NotFound();
			}
			else
			{
				var companyDto = _mapper.Map<CompanyDto>(company);
				return Ok(companyDto);
			}
		}

		[HttpPost]
		public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
		{
			if (company == null)
			{
				_logger.LogError("CompanyForCreationDto object sent from client is null.");
				return BadRequest("CompanyForCreationDto object is null");
			}

			var companyEntity = _mapper.Map<Company>(company);

			_repository.CreateCompany(companyEntity);

			var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

			return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
		}
	}
}