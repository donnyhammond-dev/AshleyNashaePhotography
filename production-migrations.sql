IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260730142220_InitialCreate'
)
BEGIN
    CREATE TABLE [HomePageSettings] (
        [Id] int NOT NULL IDENTITY,
        [HeroImagePath] nvarchar(max) NOT NULL,
        [HeroHeading] nvarchar(max) NOT NULL,
        [HeroSubheading] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_HomePageSettings] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260730142220_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260730142220_InitialCreate', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260730142942_TitleErrorFix'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260730142942_TitleErrorFix', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260804172837_CreatePhotoAndPortfolioModels'
)
BEGIN
    DROP TABLE [HomePageSettings];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260804172837_CreatePhotoAndPortfolioModels'
)
BEGIN
    CREATE TABLE [Photos] (
        [Id] int NOT NULL IDENTITY,
        [ImagePath] nvarchar(max) NOT NULL,
        [PortfolioId] int NULL,
        CONSTRAINT [PK_Photos] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260804172837_CreatePhotoAndPortfolioModels'
)
BEGIN
    CREATE TABLE [Portfolios] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NOT NULL,
        [HeroImageId] int NULL,
        [Quote] nvarchar(max) NULL,
        CONSTRAINT [PK_Portfolios] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Portfolios_Photos_HeroImageId] FOREIGN KEY ([HeroImageId]) REFERENCES [Photos] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260804172837_CreatePhotoAndPortfolioModels'
)
BEGIN
    CREATE INDEX [IX_Photos_PortfolioId] ON [Photos] ([PortfolioId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260804172837_CreatePhotoAndPortfolioModels'
)
BEGIN
    CREATE INDEX [IX_Portfolios_HeroImageId] ON [Portfolios] ([HeroImageId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260804172837_CreatePhotoAndPortfolioModels'
)
BEGIN
    ALTER TABLE [Photos] ADD CONSTRAINT [FK_Photos_Portfolios_PortfolioId] FOREIGN KEY ([PortfolioId]) REFERENCES [Portfolios] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260804172837_CreatePhotoAndPortfolioModels'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260804172837_CreatePhotoAndPortfolioModels', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260805150222_AddHomePage'
)
BEGIN
    CREATE TABLE [HomePages] (
        [Id] int NOT NULL IDENTITY,
        [HeroWildId] int NULL,
        [HeroLoveId] int NULL,
        [HeroCelebrationId] int NULL,
        [HeroQuote] nvarchar(max) NULL,
        CONSTRAINT [PK_HomePages] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_HomePages_Photos_HeroCelebrationId] FOREIGN KEY ([HeroCelebrationId]) REFERENCES [Photos] ([Id]),
        CONSTRAINT [FK_HomePages_Photos_HeroLoveId] FOREIGN KEY ([HeroLoveId]) REFERENCES [Photos] ([Id]),
        CONSTRAINT [FK_HomePages_Photos_HeroWildId] FOREIGN KEY ([HeroWildId]) REFERENCES [Photos] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260805150222_AddHomePage'
)
BEGIN
    CREATE INDEX [IX_HomePages_HeroCelebrationId] ON [HomePages] ([HeroCelebrationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260805150222_AddHomePage'
)
BEGIN
    CREATE INDEX [IX_HomePages_HeroLoveId] ON [HomePages] ([HeroLoveId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260805150222_AddHomePage'
)
BEGIN
    CREATE INDEX [IX_HomePages_HeroWildId] ON [HomePages] ([HeroWildId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260805150222_AddHomePage'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260805150222_AddHomePage', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806152001_CelebrateChangedToRadiant'
)
BEGIN
    ALTER TABLE [HomePages] DROP CONSTRAINT [FK_HomePages_Photos_HeroCelebrationId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806152001_CelebrateChangedToRadiant'
)
BEGIN
    EXEC sp_rename N'[HomePages].[HeroCelebrationId]', N'HeroRadiantId', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806152001_CelebrateChangedToRadiant'
)
BEGIN
    EXEC sp_rename N'[HomePages].[IX_HomePages_HeroCelebrationId]', N'IX_HomePages_HeroRadiantId', 'INDEX';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806152001_CelebrateChangedToRadiant'
)
BEGIN
    ALTER TABLE [HomePages] ADD CONSTRAINT [FK_HomePages_Photos_HeroRadiantId] FOREIGN KEY ([HeroRadiantId]) REFERENCES [Photos] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260806152001_CelebrateChangedToRadiant'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260806152001_CelebrateChangedToRadiant', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260807154255_AddLayoutPropToPhoto'
)
BEGIN
    ALTER TABLE [Photos] ADD [Layout] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260807154255_AddLayoutPropToPhoto'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260807154255_AddLayoutPropToPhoto', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260812143026_AddAboutMeToHomePage'
)
BEGIN
    ALTER TABLE [HomePages] ADD [AboutMeDescription] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260812143026_AddAboutMeToHomePage'
)
BEGIN
    ALTER TABLE [HomePages] ADD [AboutMePhotoId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260812143026_AddAboutMeToHomePage'
)
BEGIN
    CREATE INDEX [IX_HomePages_AboutMePhotoId] ON [HomePages] ([AboutMePhotoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260812143026_AddAboutMeToHomePage'
)
BEGIN
    ALTER TABLE [HomePages] ADD CONSTRAINT [FK_HomePages_Photos_AboutMePhotoId] FOREIGN KEY ([AboutMePhotoId]) REFERENCES [Photos] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260812143026_AddAboutMeToHomePage'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260812143026_AddAboutMeToHomePage', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260821180229_AddPhotoSession'
)
BEGIN
    CREATE TABLE [PhotoSessions] (
        [Id] int NOT NULL IDENTITY,
        [ClientName] nvarchar(max) NOT NULL,
        [StartTime] datetime2 NOT NULL,
        [Length] int NOT NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [ClientNotes] nvarchar(max) NULL,
        CONSTRAINT [PK_PhotoSessions] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260821180229_AddPhotoSession'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260821180229_AddPhotoSession', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260823153210_PhotoSessionDateOnly'
)
BEGIN
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PhotoSessions]') AND [c].[name] = N'StartTime');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [PhotoSessions] DROP CONSTRAINT ' + @var + ';');
    ALTER TABLE [PhotoSessions] DROP COLUMN [StartTime];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260823153210_PhotoSessionDateOnly'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD [Date] date NOT NULL DEFAULT '0001-01-01';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260823153210_PhotoSessionDateOnly'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260823153210_PhotoSessionDateOnly', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260827160409_AddFirstLastNameToPhotoSession'
)
BEGIN
    EXEC sp_rename N'[PhotoSessions].[ClientName]', N'LastName', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260827160409_AddFirstLastNameToPhotoSession'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260827160409_AddFirstLastNameToPhotoSession'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260827160409_AddFirstLastNameToPhotoSession', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    EXEC(N'CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260901160545_AddIdentity'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260901160545_AddIdentity', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    ALTER TABLE [HomePages] DROP CONSTRAINT [FK_HomePages_Photos_HeroLoveId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    ALTER TABLE [HomePages] DROP CONSTRAINT [FK_HomePages_Photos_HeroRadiantId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    ALTER TABLE [HomePages] DROP CONSTRAINT [FK_HomePages_Photos_HeroWildId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    DROP INDEX [IX_HomePages_HeroLoveId] ON [HomePages];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    DROP INDEX [IX_HomePages_HeroRadiantId] ON [HomePages];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    DROP INDEX [IX_HomePages_HeroWildId] ON [HomePages];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    DECLARE @var1 nvarchar(max);
    SELECT @var1 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HomePages]') AND [c].[name] = N'HeroLoveId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [HomePages] DROP CONSTRAINT ' + @var1 + ';');
    ALTER TABLE [HomePages] DROP COLUMN [HeroLoveId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    DECLARE @var2 nvarchar(max);
    SELECT @var2 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HomePages]') AND [c].[name] = N'HeroRadiantId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [HomePages] DROP CONSTRAINT ' + @var2 + ';');
    ALTER TABLE [HomePages] DROP COLUMN [HeroRadiantId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    DECLARE @var3 nvarchar(max);
    SELECT @var3 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HomePages]') AND [c].[name] = N'HeroWildId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [HomePages] DROP CONSTRAINT ' + @var3 + ';');
    ALTER TABLE [HomePages] DROP COLUMN [HeroWildId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    ALTER TABLE [Portfolios] ADD [HomePageHeroImageId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    CREATE INDEX [IX_Portfolios_HomePageHeroImageId] ON [Portfolios] ([HomePageHeroImageId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    ALTER TABLE [Portfolios] ADD CONSTRAINT [FK_Portfolios_Photos_HomePageHeroImageId] FOREIGN KEY ([HomePageHeroImageId]) REFERENCES [Photos] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260908170536_RefactorHomePagePortfolio'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260908170536_RefactorHomePagePortfolio', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260910204653_AddTitleQuoteStringReq'
)
BEGIN
    DECLARE @var4 nvarchar(max);
    SELECT @var4 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Portfolios]') AND [c].[name] = N'Title');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Portfolios] DROP CONSTRAINT ' + @var4 + ';');
    ALTER TABLE [Portfolios] ALTER COLUMN [Title] nvarchar(25) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260910204653_AddTitleQuoteStringReq'
)
BEGIN
    DECLARE @var5 nvarchar(max);
    SELECT @var5 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Portfolios]') AND [c].[name] = N'Quote');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Portfolios] DROP CONSTRAINT ' + @var5 + ';');
    ALTER TABLE [Portfolios] ALTER COLUMN [Quote] nvarchar(100) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260910204653_AddTitleQuoteStringReq'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260910204653_AddTitleQuoteStringReq', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260911151548_RemovePhotoLayout'
)
BEGIN
    DECLARE @var6 nvarchar(max);
    SELECT @var6 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Photos]') AND [c].[name] = N'Layout');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Photos] DROP CONSTRAINT ' + @var6 + ';');
    ALTER TABLE [Photos] DROP COLUMN [Layout];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260911151548_RemovePhotoLayout'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260911151548_RemovePhotoLayout', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260921170754_AddBlockedDates'
)
BEGIN
    CREATE TABLE [BlockedDates] (
        [Id] int NOT NULL IDENTITY,
        [Date] date NOT NULL,
        CONSTRAINT [PK_BlockedDates] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260921170754_AddBlockedDates'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260921170754_AddBlockedDates', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922172246_AddSessionType'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD [SessionTypeId] int NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922172246_AddSessionType'
)
BEGIN
    CREATE TABLE [SessionTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] decimal(10,2) NOT NULL,
        CONSTRAINT [PK_SessionTypes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922172246_AddSessionType'
)
BEGIN
    CREATE INDEX [IX_PhotoSessions_SessionTypeId] ON [PhotoSessions] ([SessionTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922172246_AddSessionType'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD CONSTRAINT [FK_PhotoSessions_SessionTypes_SessionTypeId] FOREIGN KEY ([SessionTypeId]) REFERENCES [SessionTypes] ([Id]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260922172246_AddSessionType'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260922172246_AddSessionType', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260923000117_RenameSessionType'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260923000117_RenameSessionType', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260923151201_RemoveLengthFromPhotoSession'
)
BEGIN
    DECLARE @var7 nvarchar(max);
    SELECT @var7 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PhotoSessions]') AND [c].[name] = N'Length');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [PhotoSessions] DROP CONSTRAINT ' + @var7 + ';');
    ALTER TABLE [PhotoSessions] DROP COLUMN [Length];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260923151201_RemoveLengthFromPhotoSession'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260923151201_RemoveLengthFromPhotoSession', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260923162857_AddDurationMinutesToSessionType'
)
BEGIN
    ALTER TABLE [SessionTypes] ADD [DurationMinutes] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260923162857_AddDurationMinutesToSessionType'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260923162857_AddDurationMinutesToSessionType', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260924140027_AddFAQ'
)
BEGIN
    CREATE TABLE [FAQs] (
        [Id] int NOT NULL IDENTITY,
        [Question] nvarchar(max) NOT NULL,
        [Answer] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_FAQs] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260924140027_AddFAQ'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260924140027_AddFAQ', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260928145147_AddReviews'
)
BEGIN
    CREATE TABLE [Reviews] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Reviews] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260928145147_AddReviews'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260928145147_AddReviews', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260928154528_AddIsPublishedToReview'
)
BEGIN
    ALTER TABLE [Reviews] ADD [IsPublished] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260928154528_AddIsPublishedToReview'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260928154528_AddIsPublishedToReview', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260928161527_AddPostSessionEmailSentToPhotoSession'
)
BEGIN
    ALTER TABLE [Reviews] ADD [PrivateFeedback] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260928161527_AddPostSessionEmailSentToPhotoSession'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD [PostSessionEmailSent] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260928161527_AddPostSessionEmailSentToPhotoSession'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260928161527_AddPostSessionEmailSentToPhotoSession', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    ALTER TABLE [PhotoSessions] DROP CONSTRAINT [FK_PhotoSessions_SessionTypes_SessionTypeId];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    DECLARE @var8 nvarchar(max);
    SELECT @var8 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'Description');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT ' + @var8 + ';');
    ALTER TABLE [Reviews] DROP COLUMN [Description];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    DECLARE @var9 nvarchar(max);
    SELECT @var9 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'PrivateFeedback');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT ' + @var9 + ';');
    ALTER TABLE [Reviews] ALTER COLUMN [PrivateFeedback] nvarchar(1000) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    ALTER TABLE [Reviews] ADD [ReviewText] nvarchar(1000) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    DROP INDEX [IX_PhotoSessions_SessionTypeId] ON [PhotoSessions];
    DECLARE @var10 nvarchar(max);
    SELECT @var10 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PhotoSessions]') AND [c].[name] = N'SessionTypeId');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [PhotoSessions] DROP CONSTRAINT ' + @var10 + ';');
    EXEC(N'UPDATE [PhotoSessions] SET [SessionTypeId] = 0 WHERE [SessionTypeId] IS NULL');
    ALTER TABLE [PhotoSessions] ALTER COLUMN [SessionTypeId] int NOT NULL;
    ALTER TABLE [PhotoSessions] ADD DEFAULT 0 FOR [SessionTypeId];
    CREATE INDEX [IX_PhotoSessions_SessionTypeId] ON [PhotoSessions] ([SessionTypeId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    DECLARE @var11 nvarchar(max);
    SELECT @var11 = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[PhotoSessions]') AND [c].[name] = N'Email');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [PhotoSessions] DROP CONSTRAINT ' + @var11 + ';');
    EXEC(N'UPDATE [PhotoSessions] SET [Email] = N'''' WHERE [Email] IS NULL');
    ALTER TABLE [PhotoSessions] ALTER COLUMN [Email] nvarchar(max) NOT NULL;
    ALTER TABLE [PhotoSessions] ADD DEFAULT N'' FOR [Email];
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD CONSTRAINT [FK_PhotoSessions_SessionTypes_SessionTypeId] FOREIGN KEY ([SessionTypeId]) REFERENCES [SessionTypes] ([Id]) ON DELETE CASCADE;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929145646_AddModelValidationRequirments'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260929145646_AddModelValidationRequirments', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929151327_AddReviewToken'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD [ReviewToken] nvarchar(max) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929151327_AddReviewToken'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260929151327_AddReviewToken', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260929155415_SessionTypeNullable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260929155415_SessionTypeNullable', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261005171701_AddReviewSubmittedBool'
)
BEGIN
    ALTER TABLE [PhotoSessions] ADD [ReviewSubmitted] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261005171701_AddReviewSubmittedBool'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261005171701_AddReviewSubmittedBool', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261007161301_AddLastInitialToReview'
)
BEGIN
    EXEC sp_rename N'[Reviews].[Name]', N'LastInitial', 'COLUMN';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261007161301_AddLastInitialToReview'
)
BEGIN
    ALTER TABLE [Reviews] ADD [FirstName] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261007161301_AddLastInitialToReview'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261007161301_AddLastInitialToReview', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009163847_AddPhotoDisplayOrder'
)
BEGIN
    ALTER TABLE [Photos] ADD [DisplayOrder] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261009163847_AddPhotoDisplayOrder'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261009163847_AddPhotoDisplayOrder', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014194722_AddPortfolioQuoteTextColor'
)
BEGIN
    ALTER TABLE [Portfolios] ADD [QuoteTextColor] int NOT NULL DEFAULT 0;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261014194722_AddPortfolioQuoteTextColor'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261014194722_AddPortfolioQuoteTextColor', N'10.0.14');
END;

COMMIT;
GO

