using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    partial  class SimpleUserService
    {
        public async Task<Role> CreateRole(Role role)
        {
            await _roleRepository.Add(role);

            return role;
        }

        public async Task<IEnumerable<string>> GetAllRoles()
        {

            return from role in await _roleRepository.GetAll()
                   select role.Name;
        }
    }
}
