﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFrameworkCore.CommandQuery.Commands;
using InstructorIQ.Core.Domain.Models;
using InstructorIQ.Core.Domain.Queries;
using InstructorIQ.WebApplication.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InstructorIQ.WebApplication.Pages.Topic.Session
{
    public class CreateModel : EntityModelBase<SessionCreateModel>
    {
        public CreateModel(IMediator mediator, ILoggerFactory loggerFactory)
            : base(mediator, loggerFactory)
        {

        }

        [BindProperty(SupportsGet = true)]
        public Guid TopicId { get; set; }

        [BindProperty]
        public IReadOnlyCollection<InstructorDropdownModel> Instructors { get; set; }

        [BindProperty]
        public IReadOnlyCollection<LocationDropdownModel> Locations { get; set; }

        [BindProperty]
        public IReadOnlyCollection<GroupDropdownModel> Groups { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Instructors = await LoadInstructors();
            Locations = await LoadLocations();
            Groups = await LoadGroups();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var createModel = new SessionCreateModel
            {
                TopicId = TopicId
            };

            // only update input fields
            await TryUpdateModelAsync(
                createModel,
                nameof(Entity),
                p => p.Note,
                p => p.StartTime,
                p => p.EndTime,
                p => p.LocationId,
                p => p.GroupId,
                p => p.LeadInstructorId
            );

            var command = new EntityCreateCommand<SessionCreateModel, SessionReadModel>(createModel, User);
            var result = await Mediator.Send(command);

            ShowAlert("Successfully created topic session");

            return RedirectToPage("/Topic/Session/Edit", new { result.Id, TopicId });
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