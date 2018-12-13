using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmaQueue.Models.CustomerViewModels
{


    /*
        Author: Ricky Bruner
        Purpose: Holds the search query from the navbar search input, and a collection of products that contain that query string within its Title. This view model feed the Search.cshtml in Products.
    */

    public class CustomerSearchViewModel
    {

        public string Search { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}