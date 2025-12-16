using KlantenSimulatorBL.Model;
using KlantenSimulatorUI_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlantenSimulatorUI_WPF.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerUI MapFromDomain(CustomerUI customer)
        {
            return new CustomerUI(customer.Name);
        }
        public static Customer MapToDomain(CustomerUI customer)
        {
            return new Customer(customer.Name);
        }
        
    }
   
    
}
