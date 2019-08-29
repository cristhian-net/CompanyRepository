using CodeChallengeApp.Data.Repositories;
using System;
using System.Linq;
using Xunit;

namespace CodeChallengeApp.Tests
{
    public class InMemoryRepositoryTests
    {
        [Fact]
        public void GetAllShouldReturnEmptyList()
        {
            var sut = new InMemoryCompanyRepository();

            var list = sut.GetAll();

            Assert.Empty(list);
        }

        [Fact]
        public void AddShouldAddOneItem()
        {
            var sut = new InMemoryCompanyRepository();
            Company company = new Company()
            {
                CreatedDate = new DateTime(),
                FoundedDate = new DateTime(),
                Name = "My Company"
            };
            sut.Add(company);
            var list = sut.GetAll();

            Assert.Single(list);
            Assert.Equal(company.Name, list.First().Name);
        }

        [Fact]
        public void DeleteAfterAddingShouldRemoveItem()
        {
            var sut = new InMemoryCompanyRepository();
            Company company = new Company()
            {
                CreatedDate = new DateTime(),
                FoundedDate = new DateTime(),
                Name = "My Company"
            };
            sut.Add(company);
            sut.Delete(company.CompanyId);
            var list = sut.GetAll();

            Assert.Empty(list);
        }

        [Fact]
        public void UpdateShouldChangeItemInList()
        {
            var sut = new InMemoryCompanyRepository();
            Company company = new Company()
            {
                CreatedDate = new DateTime(),
                FoundedDate = new DateTime(),
                Name = "My Company"
            };
            int id = sut.Add(company);

            Company anotherCompany = new Company()
            {
                CreatedDate = company.CreatedDate,
                FoundedDate = company.FoundedDate,
                Name = "Not my company",
                CompanyId = id
            };
            sut.Update(anotherCompany);
            var list = sut.GetAll();

            Assert.Equal("Not my company", list.First().Name);
        }
    }
}
