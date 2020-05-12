using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class OwnerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        

        //return the owner object with its account details
        public IEnumerable<AccountDto> Accounts { get; set; }
    }
}
