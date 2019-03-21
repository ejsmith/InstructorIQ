﻿using System;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Queries;
using InstructorIQ.Core.Domain.Models;
using InstructorIQ.Core.Domain.Queries;
using InstructorIQ.Core.Extensions;
using InstructorIQ.Core.Security;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace InstructorIQ.Core.Multitenancy
{
    public class TenantModelResolver : ITenantResolver<TenantReadModel>
    {
        public TenantModelResolver(IMediator mediator, ILoggerFactory loggerFactory, UserClaimManager userClaimManager)
        {
            Mediator = mediator;
            UserClaimManager = userClaimManager;
            Logger = loggerFactory.CreateLogger(GetType());
        }

        protected UserClaimManager UserClaimManager { get; }

        protected ILogger Logger { get; }

        protected IMediator Mediator { get; }

        public async Task<TenantContext<TenantReadModel>> ResolveAsync(HttpContext context)
        {
            var tenant = await ResolveFromPath(context);

            if (tenant == null)
                tenant = await ResolveFromUser(context);

            if (tenant == null)
                return null;

            var tenantContext = new TenantContext<TenantReadModel>(tenant);
            return tenantContext;
        }

        private async Task<TenantReadModel> ResolveFromUser(HttpContext context)
        {
            if (!context.User.Identity.IsAuthenticated)
                return null;

            var slug = UserClaimManager.GetTenantSlug(context.User);
            if (slug.IsNullOrEmpty())
                return null;

            var query = new TenantSlugQuery(context.User, slug);
            var tenant = await Mediator.Send(query);

            return tenant;
        }

        private async Task<TenantReadModel> ResolveFromPath(HttpContext context)
        {
            if (!context.Request.Path.HasValue)
                return null;

            var slug = context.Request.Path.Value
                .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault();

            if (slug.IsNullOrEmpty())
                return null;

            var query = new TenantSlugQuery(context.User, slug);
            var tenant = await Mediator.Send(query);

            return tenant;
        }
    }
}