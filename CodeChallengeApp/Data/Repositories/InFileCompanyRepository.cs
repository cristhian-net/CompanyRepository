using CodeChallengeApp.Utils;
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

            WriteArrayToFile(companies);

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

            WriteArrayToFile(companies);
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
            WriteArrayToFile(companies);
        }

        private int GenerateId(IEnumerable<Company> companies)
        {
            return IdHelper.GenerateId(companies, "CompanyId");
        }

        private string GetCompaniesFromFileAsJson()
        {
            return File.ReadAllText(file);
        }

        private void WriteArrayToFile(List<Company> companies)
        {
            string newJson = JsonConvert.SerializeObject(companies);
            File.WriteAllText(file, newJson);
        }
    }
}
