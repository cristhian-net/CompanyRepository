using System.Collections.Generic;

namespace CodeChallengeApp
{
    public class EmptyCompanyRepository : ICompanyRepository
    {
        public int Add(Company company)
        {
            return 1;
        }

        public void Delete(int companyId)
        {
            
        }

        public IEnumerable<Company> GetAll()
        {
            return new List<Company>();
        }

        public Company GetById(int companyId)
        {
            return new Company() { CompanyId = companyId };
        }

        public void Update(Company company)
        {
            
        }
    }
}
