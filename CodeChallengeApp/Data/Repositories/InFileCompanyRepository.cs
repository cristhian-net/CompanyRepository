using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApp.Data.Repositories
{
    public class InFileCompanyRepository : ICompanyRepository
    {
        private string file = @"companyDb.json";

        public int Add(Company company)
        {
            List<Company> companies = GetAll().ToList();

            company.CompanyId = GenerateId(companies);
            companies.Add(company);

            string newJson = JsonConvert.SerializeObject(companies);
            File.WriteAllText(file, newJson);

            return company.CompanyId;
        }

        public void Delete(int companyId)
        {
            List<Company> companies = GetAll().ToList();
            Company company = companies.FirstOrDefault(c => c.CompanyId == companyId);
            if (company != null)
            {
                companies.Remove(company);
            }
            string newJson = JsonConvert.SerializeObject(JArray.FromObject(companies));
            File.WriteAllText(file, newJson);
        }

        private int GenerateId(IEnumerable<Company> companies)
        {
            return companies.Count() == 0 ? 1 : companies.Max(c => c.CompanyId) + 1;
        }

        public IEnumerable<Company> GetAll()
        {
            if (!File.Exists(file))
            {
                List<Company> emptyList = new List<Company>();
                string json = JsonConvert.SerializeObject(JArray.FromObject(emptyList));
                File.AppendAllText(file, json);
            }
            return JsonConvert.DeserializeObject<List<Company>>(GetCompaniesFromFileAsJson());
        }

        private string GetCompaniesFromFileAsJson()
        {
            return File.ReadAllText(file);
        }

        public Company GetById(int companyId)
        {
            List<Company> companies = GetAll().ToList();
            return companies.FirstOrDefault(c => c.CompanyId == companyId);
        }

        public void Update(Company company)
        {
            List<Company> companies = GetAll().ToList();
            int position = companies.FindIndex(c => c.CompanyId == company.CompanyId);
            if (position >= 0)
            {
                companies[position] = company;
            }
            string newJson = JsonConvert.SerializeObject(companies);
            File.WriteAllText(file, newJson);
        }
    }
}
