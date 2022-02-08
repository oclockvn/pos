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
GO

IF SCHEMA_ID(N'products') IS NULL EXEC(N'CREATE SCHEMA [products];');
GO

CREATE TABLE [products].[Product] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductName] nvarchar(250) NOT NULL,
    [WholesalesPrice] decimal(18,2) NOT NULL,
    [SalesPrice] decimal(18,2) NOT NULL,
    [ImportPrice] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);
GO

CREATE UNIQUE INDEX [IX_Product_ProductName] ON [products].[Product] ([ProductName]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220208135116_Init', N'6.0.1');
GO

COMMIT;
GO

