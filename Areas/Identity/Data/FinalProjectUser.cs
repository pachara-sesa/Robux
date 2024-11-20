// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using Microsoft.AspNetCore.Identity;

namespace AS3_1660706126.Areas.Identity.Data
{
    // คลาสที่สืบทอดมาจาก IdentityUser
    public class FinalProjectUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobilePhone { get; set; }
    }
}
