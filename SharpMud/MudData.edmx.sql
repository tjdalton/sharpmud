
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 03/30/2015 16:50:32
-- Generated from EDMX file: C:\Users\Tim\Documents\Visual Studio 2013\Projects\SharpMud\SharpMud\MudData.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MobRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Mobs] DROP CONSTRAINT [FK_MobRoom];
GO
IF OBJECT_ID(N'[dbo].[FK_PlayerMob]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Players] DROP CONSTRAINT [FK_PlayerMob];
GO
IF OBJECT_ID(N'[dbo].[FK_PermissionPlayer_Permission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionPlayer] DROP CONSTRAINT [FK_PermissionPlayer_Permission];
GO
IF OBJECT_ID(N'[dbo].[FK_PermissionPlayer_Player]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PermissionPlayer] DROP CONSTRAINT [FK_PermissionPlayer_Player];
GO
IF OBJECT_ID(N'[dbo].[FK_ExitDirection]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Exits] DROP CONSTRAINT [FK_ExitDirection];
GO
IF OBJECT_ID(N'[dbo].[FK_ExitRoom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Exits] DROP CONSTRAINT [FK_ExitRoom];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Rooms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rooms];
GO
IF OBJECT_ID(N'[dbo].[Mobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Mobs];
GO
IF OBJECT_ID(N'[dbo].[Players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players];
GO
IF OBJECT_ID(N'[dbo].[Exits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Exits];
GO
IF OBJECT_ID(N'[dbo].[Permissions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permissions];
GO
IF OBJECT_ID(N'[dbo].[Directions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Directions];
GO
IF OBJECT_ID(N'[dbo].[PermissionPlayer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PermissionPlayer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Rooms'
CREATE TABLE [dbo].[Rooms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Mobs'
CREATE TABLE [dbo].[Mobs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Room_Id] int  NOT NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [LastLogin] datetime  NOT NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Mob_Id] int  NOT NULL
);
GO

-- Creating table 'Exits'
CREATE TABLE [dbo].[Exits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [To] int  NOT NULL,
    [Direction_Id] int  NOT NULL,
    [Room_Id] int  NOT NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [dbo].[Permissions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Directions'
CREATE TABLE [dbo].[Directions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [From] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PermissionPlayer'
CREATE TABLE [dbo].[PermissionPlayer] (
    [Permissions_Id] int  NOT NULL,
    [Players_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Rooms'
ALTER TABLE [dbo].[Rooms]
ADD CONSTRAINT [PK_Rooms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Mobs'
ALTER TABLE [dbo].[Mobs]
ADD CONSTRAINT [PK_Mobs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Exits'
ALTER TABLE [dbo].[Exits]
ADD CONSTRAINT [PK_Exits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Permissions'
ALTER TABLE [dbo].[Permissions]
ADD CONSTRAINT [PK_Permissions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Directions'
ALTER TABLE [dbo].[Directions]
ADD CONSTRAINT [PK_Directions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Permissions_Id], [Players_Id] in table 'PermissionPlayer'
ALTER TABLE [dbo].[PermissionPlayer]
ADD CONSTRAINT [PK_PermissionPlayer]
    PRIMARY KEY CLUSTERED ([Permissions_Id], [Players_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Room_Id] in table 'Mobs'
ALTER TABLE [dbo].[Mobs]
ADD CONSTRAINT [FK_MobRoom]
    FOREIGN KEY ([Room_Id])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MobRoom'
CREATE INDEX [IX_FK_MobRoom]
ON [dbo].[Mobs]
    ([Room_Id]);
GO

-- Creating foreign key on [Mob_Id] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [FK_PlayerMob]
    FOREIGN KEY ([Mob_Id])
    REFERENCES [dbo].[Mobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlayerMob'
CREATE INDEX [IX_FK_PlayerMob]
ON [dbo].[Players]
    ([Mob_Id]);
GO

-- Creating foreign key on [Permissions_Id] in table 'PermissionPlayer'
ALTER TABLE [dbo].[PermissionPlayer]
ADD CONSTRAINT [FK_PermissionPlayer_Permission]
    FOREIGN KEY ([Permissions_Id])
    REFERENCES [dbo].[Permissions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Players_Id] in table 'PermissionPlayer'
ALTER TABLE [dbo].[PermissionPlayer]
ADD CONSTRAINT [FK_PermissionPlayer_Player]
    FOREIGN KEY ([Players_Id])
    REFERENCES [dbo].[Players]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PermissionPlayer_Player'
CREATE INDEX [IX_FK_PermissionPlayer_Player]
ON [dbo].[PermissionPlayer]
    ([Players_Id]);
GO

-- Creating foreign key on [Direction_Id] in table 'Exits'
ALTER TABLE [dbo].[Exits]
ADD CONSTRAINT [FK_ExitDirection]
    FOREIGN KEY ([Direction_Id])
    REFERENCES [dbo].[Directions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExitDirection'
CREATE INDEX [IX_FK_ExitDirection]
ON [dbo].[Exits]
    ([Direction_Id]);
GO

-- Creating foreign key on [Room_Id] in table 'Exits'
ALTER TABLE [dbo].[Exits]
ADD CONSTRAINT [FK_ExitRoom]
    FOREIGN KEY ([Room_Id])
    REFERENCES [dbo].[Rooms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExitRoom'
CREATE INDEX [IX_FK_ExitRoom]
ON [dbo].[Exits]
    ([Room_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------