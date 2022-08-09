
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/09/2022 06:03:40
-- Generated from EDMX file: C:\Users\pc\source\repos\StTest\MyTests\EDM.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MyTestsDataBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Answers_Questions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_Answers_Questions];
GO
IF OBJECT_ID(N'[dbo].[FK_Answers_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_Answers_Users];
GO
IF OBJECT_ID(N'[dbo].[FK_Questions_Tests]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_Questions_Tests];
GO
IF OBJECT_ID(N'[dbo].[FK_Tests_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tests] DROP CONSTRAINT [FK_Tests_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answers];
GO
IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[Tests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tests];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [IdUserAnswer] int  NOT NULL,
    [IdQuestion] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [Answer] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [IdQuestion] int  NOT NULL,
    [IdTest] int  NOT NULL,
    [Content] nvarchar(150)  NOT NULL,
    [Answer] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'Tests'
CREATE TABLE [dbo].[Tests] (
    [IdTest] int  NOT NULL,
    [IdUser] int  NOT NULL,
    [Name] nvarchar(150)  NOT NULL,
    [Image] varbinary(max)  NULL,
    [IsAnswersVisible] bit  NOT NULL,
    [IsVisible] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [IdUser] int  NOT NULL,
    [Login] nvarchar(50)  NOT NULL,
    [Password] nvarchar(50)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Info] nvarchar(50)  NULL,
    [Image] varbinary(max)  NULL,
    [Post] nvarchar(25)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Patronymic] nvarchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdUserAnswer] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([IdUserAnswer] ASC);
GO

-- Creating primary key on [IdQuestion] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([IdQuestion] ASC);
GO

-- Creating primary key on [IdTest] in table 'Tests'
ALTER TABLE [dbo].[Tests]
ADD CONSTRAINT [PK_Tests]
    PRIMARY KEY CLUSTERED ([IdTest] ASC);
GO

-- Creating primary key on [IdUser] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdQuestion] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_Answers_Questions]
    FOREIGN KEY ([IdQuestion])
    REFERENCES [dbo].[Questions]
        ([IdQuestion])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Answers_Questions'
CREATE INDEX [IX_FK_Answers_Questions]
ON [dbo].[Answers]
    ([IdQuestion]);
GO

-- Creating foreign key on [IdUser] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_Answers_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Answers_Users'
CREATE INDEX [IX_FK_Answers_Users]
ON [dbo].[Answers]
    ([IdUser]);
GO

-- Creating foreign key on [IdTest] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_Questions_Tests]
    FOREIGN KEY ([IdTest])
    REFERENCES [dbo].[Tests]
        ([IdTest])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Questions_Tests'
CREATE INDEX [IX_FK_Questions_Tests]
ON [dbo].[Questions]
    ([IdTest]);
GO

-- Creating foreign key on [IdUser] in table 'Tests'
ALTER TABLE [dbo].[Tests]
ADD CONSTRAINT [FK_Tests_Users]
    FOREIGN KEY ([IdUser])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Tests_Users'
CREATE INDEX [IX_FK_Tests_Users]
ON [dbo].[Tests]
    ([IdUser]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------