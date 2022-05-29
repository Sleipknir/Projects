using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebForum.Models
{
    public class ApplicationUser : IdentityUser 
    {   
       
      public int? NumberOfPosts { get; set; }
       
    }
}
