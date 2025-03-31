CREATE DATABASE [UserAuthDB]
GO

USE [UserAuthDB]
GO

CREATE TABLE [dbo].[User](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [Logins] [nvarchar](50) NOT NULL,
    [Passwords] [nvarchar](50) NOT NULL,
    [Roles] [nvarchar](50) NOT NULL,
    [FIO] [nvarchar](100) NOT NULL,
    [Gender] [nvarchar](10) NULL,
    [PhoneNumber] [nvarchar](20) NULL,
    [PhotoURL] [nvarchar](255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
)
GO

INSERT INTO [dbo].[User] ([Logins], [Passwords], [Roles], [FIO], [Gender], [PhoneNumber], [PhotoURL]) 
VALUES 
    ('admin', 'admin123', 'Администратор', 'Иванов Иван Иванович', 'Мужской', '+7(123)456-78-90', 'https://example.com/admin.jpg'),
    ('manager1', 'mng123', 'Менеджер', 'Петрова Анна Сергеевна', 'Женский', '+7(234)567-89-01', 'https://example.com/manager1.jpg'),
    ('manager2', 'mng456', 'Менеджер', 'Сидоров Дмитрий Петрович', 'Мужской', '+7(345)678-90-12', 'https://example.com/manager2.jpg'),
    ('user1', 'user123', 'Пользователь', 'Кузнецова Елена Владимировна', 'Женский', '+7(456)789-01-23', 'https://example.com/user1.jpg'),
    ('user2', 'user456', 'Пользователь', 'Федоров Михаил Александрович', 'Мужской', '+7(567)890-12-34', 'https://example.com/user2.jpg'),
    ('user3', 'user789', 'Пользователь', 'Смирнова Ольга Игоревна', 'Женский', '+7(678)901-23-45', 'https://example.com/user3.jpg'),
    ('guest1', 'guest123', 'Гость', 'Николаев Артем Сергеевич', 'Мужской', '+7(789)012-34-56', 'https://example.com/guest1.jpg'),
    ('guest2', 'guest456', 'Гость', 'Васильева Татьяна Дмитриевна', 'Женский', '+7(890)123-45-67', 'https://example.com/guest2.jpg'),
    ('test1', 'test123', 'Тестировщик', 'Григорьев Павел Андреевич', 'Мужской', '+7(901)234-56-78', 'https://example.com/test1.jpg'),
    ('test2', 'test456', 'Тестировщик', 'Белова Юлия Олеговна', 'Женский', '+7(012)345-67-89', 'https://example.com/test2.jpg'),
    ('dev1', 'dev123', 'Разработчик', 'Козлов Алексей Викторович', 'Мужской', '+7(123)456-78-91', 'https://example.com/dev1.jpg'),
    ('dev2', 'dev456', 'Разработчик', 'Орлова Марина Евгеньевна', 'Женский', '+7(234)567-89-12', 'https://example.com/dev2.jpg')
GO
