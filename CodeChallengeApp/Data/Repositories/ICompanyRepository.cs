using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallengeApp
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company GetById(int companyId);
        int Add(Company company);
        void Update(Company company);
        void Delete(int companyId);
    }
}
