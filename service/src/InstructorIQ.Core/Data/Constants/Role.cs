﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InstructorIQ.Core.Data.Constants
{
    public static class Role
    {
        public static readonly Guid Member = new Guid("d373fbb2-39eb-e711-87c1-708bcd56aa6d");
        public static readonly Guid Instructor = new Guid("808c0ec0-39eb-e711-87c1-708bcd56aa6d");
        public static readonly Guid Administrator = new Guid("8fa6aec8-39eb-e711-87c1-708bcd56aa6d");

        public const string MemberName = "Member";
        public const string InstructorName = "Instructor";
        public const string AdministratorName = "Administrator";
        public const string GlobalAdministrator = "GlobalAdministrator";

    }
}
