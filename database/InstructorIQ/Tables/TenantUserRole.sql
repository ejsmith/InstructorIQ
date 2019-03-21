﻿CREATE TABLE [IQ].[TenantUserRole]
(
    [Id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_TenantUserRole_Id] DEFAULT (NEWSEQUENTIALID()),

    [TenantId] UNIQUEIDENTIFIER NOT NULL,
    [UserName] NVARCHAR(100) NOT NULL,
    [RoleName] NVARCHAR(100) NOT NULL,

    CONSTRAINT [PK_TenantUserRole] PRIMARY KEY NONCLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenantUserRole_Tenant_TenantId] FOREIGN KEY ([TenantId]) REFERENCES [IQ].[Tenant]([Id]),
)
