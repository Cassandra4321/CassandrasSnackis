using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Snackis4.Models;

namespace Snackis4.Areas.Identity.Data;
public class Snackis4User : IdentityUser
{
    public string Nickname { get; set; }
    public string? ProfilePicture { get; set; }

}

