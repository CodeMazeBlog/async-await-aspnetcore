using Entities.Models;
using System;
using System.Collections.Generic;

namespace Contracts
{
	public interface ICompanyRepository
	{
		IEnumerable<Company> GetAllCompanies();
		Company GetCompany(Guid companyId);
		void CreateCompany(Company company);
	}
}
