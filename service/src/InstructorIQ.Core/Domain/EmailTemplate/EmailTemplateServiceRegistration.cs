﻿using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using InstructorIQ.Core.Data.Entities;
using InstructorIQ.Core.Domain.Models;

// ReSharper disable once CheckNamespace
namespace InstructorIQ.Core.Domain
{
    public class EmailTemplateServiceRegistration : DomainServiceRegistrationBase
    {
        public override void Register(IServiceCollection services, IDictionary<string, object> data)
        {
            RegisterEntityQuery<Guid, EmailTemplate, EmailTemplateReadModel>(services);
            RegisterEntityCommand<Guid, EmailTemplate, EmailTemplateReadModel, EmailTemplateCreateModel, EmailTemplateUpdateModel>(services);
        }
    }
}