using CodeChallengeApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApp.Data.Repositories
{
    public class InMemoryRepository : ICompanyRepository
    {
        private List<Company> Db;

        public InMemoryRepository()
        {
            Db = new List<Company>();
        }

        public int Add(Company company)
        {
            company.CompanyId = GenerateId();
            Db.Add(company);

            return company.CompanyId;
        }

        private int GenerateId()
        {
            return IdHelper.GenerateId(Db, "CompanyId");
        }

        public void Delete(int companyId)
        {
            Company company = GetCompany(companyId);
            if (company != null)
            {
                Db.Remove(company);
            }
        }

        private Company GetCompany(int companyId)
        {
            return Db.FirstOrDefault(c => c.CompanyId == companyId);
        }

        public IEnumerable<Company> GetAll()
        {
            return Db;
        }

        public Company GetById(int companyId)
        {
            return Db.FirstOrDefault(c => c.CompanyId == companyId);
        }

        public void Update(Company company)
        {
            int position = Db.FindIndex(c => c.CompanyId == company.CompanyId);
            if(position >= 0)
            {
                Db[position] = company;
            }
        }
    }
}
