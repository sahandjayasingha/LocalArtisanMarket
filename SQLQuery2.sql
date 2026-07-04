
INSERT INTO [dbo].[Users] ([FullName], [Email], [PasswordHash], [Role], [CreatedAt])
VALUES ('Test Customer', 'customer@gmail.com', '123', 'Customer', GETDATE());


INSERT INTO [dbo].[Users] ([FullName], [Email], [PasswordHash], [Role], [CreatedAt])
VALUES ('Test Artisan', 'artisan@gmail.com', '123', 'Artisan', GETDATE());