﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.ViewModels
{
    public class DeleteOrganisationViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public int OrgId { get; set; }
    }
}