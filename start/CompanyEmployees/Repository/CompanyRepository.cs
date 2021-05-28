using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly RepositoryContext _repoContext;

		public CompanyRepository(RepositoryContext repoContext)
		{
			_repoContext = repoContext;
		}

		public IEnumerable<Company> GetAllCompanies() =>
		   _repoContext.Companies
			.OrderBy(c => c.Name)
			.ToList();

		public Company GetCompany(Guid companyId) =>
			_repoContext.Companies
			.SingleOrDefault(c => c.Id.Equals(companyId));

		public void CreateCompany(Company company)
		{
			_repoContext.Add(company);
			_repoContext.SaveChanges();
		}
	}
}
