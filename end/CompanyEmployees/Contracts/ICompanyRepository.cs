using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts
{
	public interface ICompanyRepository
	{
		Task<IEnumerable<Company>> GetAllCompanies();
		Task<Company> GetCompany(Guid companyId);
		Task CreateCompany(Company company);
	}
}
