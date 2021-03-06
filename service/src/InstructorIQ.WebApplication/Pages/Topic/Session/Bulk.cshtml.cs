﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR.CommandQuery.Queries;
using InstructorIQ.Core.Domain.Commands;
using InstructorIQ.Core.Domain.Models;
using InstructorIQ.Core.Domain.Queries;
using InstructorIQ.Core.Multitenancy;
using InstructorIQ.Core.Security;
using InstructorIQ.WebApplication.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InstructorIQ.WebApplication.Pages.Topic.Session
{
    [Authorize(Policy = UserPolicies.AdministratorPolicy)]
    public class BulkModel : MediatorModelBase
    {
        public BulkModel(ITenant<TenantReadModel> tenant, IMediator mediator, ILoggerFactory loggerFactory)
            : base(tenant, mediator, loggerFactory)
        {
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public List<SessionBulkUpdateModel> Sessions { get; set; }

        public TopicReadModel Topic { get; set; }

        public IReadOnlyCollection<InstructorDropdownModel> Instructors { get; set; }

        public IReadOnlyCollection<LocationDropdownModel> Locations { get; set; }

        public IReadOnlyCollection<GroupDropdownModel> Groups { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            var loadTopicTask = LoadTopic();
            var loadSessionsTask = LoadSessions();
            var instructorTask = LoadInstructors();
            var locationTask = LoadLocations();
            var groupTask = LoadGroups();

            // load all in parallel
            await Task.WhenAll(
                loadSessionsTask,
                loadTopicTask,
                instructorTask,
                locationTask,
                groupTask
            );

            Topic = loadTopicTask.Result;
            Instructors = instructorTask.Result;
            Locations = locationTask.Result;
            Groups = groupTask.Result;

            // shared layout title
            ViewData["TopicTitle"] = $" - {Topic.Title}";

            // convert to bulk update
            Sessions = loadSessionsTask.Result
                .Select(i => new SessionBulkUpdateModel
                {
                    Id = i.Id,
                    StartDate = i.StartDate,
                    StartTime = i.StartTime,
                    EndDate = i.EndDate,
                    EndTime = i.EndTime,
                    GroupId = i.GroupId,
                    LeadInstructorId = i.LeadInstructorId,
                    LocationId = i.LocationId,
                    Note = i.Note
                })
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var bulkUpdateCommand = new SessionBulkUpdateCommand(User, Sessions);
            var result = await Mediator.Send(bulkUpdateCommand);

            ShowAlert("Successfully saved topic sessions");

            return RedirectToPage("/Topic/Session/Index", new { Id, tenant = TenantRoute });
        }


        private async Task<TopicReadModel> LoadTopic()
        {
            var command = new EntityIdentifierQuery<Guid, TopicReadModel>(User, Id);
            var result = await Mediator.Send(command);

            return result;
        }

        private async Task<IReadOnlyCollection<SessionReadModel>> LoadSessions()
        {
            var query = new SessionTopicQuery(User, Id);
            var items = await Mediator.Send(query);

            return items;
        }

        private async Task<IReadOnlyCollection<InstructorDropdownModel>> LoadInstructors()
        {
            var dropdownQuery = new InstructorDropdownQuery(User);
            var items = await Mediator.Send(dropdownQuery);

            return items;
        }

        private async Task<IReadOnlyCollection<LocationDropdownModel>> LoadLocations()
        {
            var dropdownQuery = new LocationDropdownQuery(User);
            var items = await Mediator.Send(dropdownQuery);

            return items;
        }

        private async Task<IReadOnlyCollection<GroupDropdownModel>> LoadGroups()
        {
            var dropdownQuery = new GroupDropdownQuery(User);
            var items = await Mediator.Send(dropdownQuery);

            return items;
        }

    }
}