using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallengeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        ICompanyRepository companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        // GET api/company
        [HttpGet]
        public ActionResult<IEnumerable<Company>> Get()
        {
            return new List<Company>(companyRepository.GetAll());
        }

        // GET api/company/5
        [HttpGet("{id}")]
        public ActionResult<Company> Get(int id)
        {
            Company company = companyRepository.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return company;
        }

        // POST api/company
        [HttpPost]
        public ActionResult<Company> Post(Company company)
        {
            int companyId = companyRepository.Add(company);
            return CreatedAtAction(nameof(Post), new { id = companyId }, company);
        }

        // PUT api/company/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, Company company)
        {
            if (id != company.CompanyId)
            {
                return BadRequest();
            }

            companyRepository.Update(company);

            return NoContent();
        }

        // DELETE api/company/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            companyRepository.Delete(id);
            return NoContent();
        }
    }
}
