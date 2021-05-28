using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		public async Task<IActionResult> GetCompanies()
		{
			try
			{
				var companies = await _repository.GetAllCompanies();

				var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

				_logger.LogInfo("All companies fetched from the database");

				return Ok(companiesDto);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Exception occurred with a message: {ex.Message}");
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}", Name = "CompanyById")]
		public async Task<IActionResult> GetCompany(Guid id)
		{
			var company = await _repository.GetCompany(id);
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
		public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
		{
			if (company == null)
			{
				_logger.LogError("CompanyForCreationDto object sent from client is null.");
				return BadRequest("CompanyForCreationDto object is null");
			}

			var companyEntity = _mapper.Map<Company>(company);

			await _repository.CreateCompany(companyEntity);

			var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);

			return CreatedAtRoute("CompanyById", new { id = companyToReturn.Id }, companyToReturn);
		}
	}
}