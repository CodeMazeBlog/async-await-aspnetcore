using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly RepositoryContext _repoContext;

		public CompanyRepository(RepositoryContext repoContext)
		{
			_repoContext = repoContext;
		}

		public async Task<IEnumerable<Company>> GetAllCompanies()
		{
			//throw new Exception("Custom exception for testing purposes");
			return await _repoContext.Companies
				.OrderBy(c => c.Name)
				.ToListAsync();
		}

		public async Task<Company> GetCompany(Guid companyId) =>
			await _repoContext.Companies
				.SingleOrDefaultAsync(c => c.Id.Equals(companyId));

		public async Task CreateCompany(Company company)
		{
			_repoContext.Add(company);
			await _repoContext.SaveChangesAsync();
		}
	}
}
