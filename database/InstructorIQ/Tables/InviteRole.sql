﻿CREATE TABLE [IQ].[InviteRole]
(
    [Id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_InviteRole_Id] DEFAULT (NEWSEQUENTIALID()),

    [InviteId] UNIQUEIDENTIFIER NOT NULL,
    [RoleId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [PK_InviteRole] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InviteRole_Invite_InviteId] FOREIGN KEY ([InviteId]) REFERENCES [IQ].[Invite]([Id]),
    CONSTRAINT [FK_InviteRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [IQ].[Role]([Id]),
)

GO

CREATE INDEX [UX_InviteRole_InviteId]
ON [IQ].[InviteRole] ([InviteId])

GO