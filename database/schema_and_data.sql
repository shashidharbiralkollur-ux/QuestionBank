USE [QuestionBank]
GO
/****** Object:  Table [dbo].[Options]    Script Date: 2025-09-01 11:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Options](
	[OptionID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[OptionLabel] [nvarchar](50) NULL,
	[OptionText] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[OptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Questions]    Script Date: 2025-09-01 11:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](200) NOT NULL,
	[QuestionText] [nvarchar](500) NULL,
	[CorrectAnswer] [char](1) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2025-09-01 11:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](50) NOT NULL DEFAULT ('User'),
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Options] ON 

INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (6, 3, N'A', N'Java')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (7, 3, N'B', N'Python')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (8, 3, N'C', N'.NET Core')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (9, 3, N'D', N'C#')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (10, 4, N'A', N'Javascript')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (11, 4, N'B', N'Python')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (12, 4, N'C', N'.NET Core')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (13, 4, N'D', N'All')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (14, 5, N'A', N'Java')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (15, 5, N'B', N'Python')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (16, 5, N'C', N'Node.js')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (17, 5, N'D', N'C#')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (30, 7, N'A', N'Windows')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (31, 7, N'B', N'linus')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (32, 7, N'C', N'mac os')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (33, 7, N'D', N'All')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (34, 8, N'A', N'.net 5')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (35, 8, N'B', N'.net 6')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (36, 8, N'C', N'.net 8')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (37, 8, N'D', N'.net 7')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (38, 9, N'A', N'a')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (39, 9, N'B', N'b')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (40, 9, N'C', N'c')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (41, 9, N'D', N'a')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (42, 10, N'A', N'q')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (43, 10, N'B', N'a')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (44, 10, N'C', N'z')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (45, 10, N'D', N'x')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (46, 11, N'A', N'gg')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (47, 11, N'B', N'hh')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (48, 11, N'C', N'kk')
INSERT [dbo].[Options] ([OptionID], [QuestionID], [OptionLabel], [OptionText]) VALUES (49, 11, N'D', N'll')
SET IDENTITY_INSERT [dbo].[Options] OFF
SET IDENTITY_INSERT [dbo].[Questions] ON 

INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (3, N'C# Basics', N'Which language runs in .NET?', N'D', 2, CAST(N'2025-08-25 11:47:41.030' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (4, N'C# Basics', N'Which language runs in .NET?', N'D', 2, CAST(N'2025-08-25 11:49:36.550' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (5, N'C# Basics', N'Which of these is a Programming language?', N'D', 2, CAST(N'2025-08-25 14:44:39.120' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (7, N'C# Basics', N'.Net supports core works on?', N'D', 2, CAST(N'2025-08-28 14:23:32.383' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (8, N'C# Basics', N'.net core latest version', N'C', 2, CAST(N'2025-08-28 14:30:42.900' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (9, N'c', N'aaa', N'a', 2, CAST(N'2025-08-28 14:32:29.533' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (10, N'c', N'www', N'b', 2, CAST(N'2025-08-28 14:34:46.163' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [Subject], [QuestionText], [CorrectAnswer], [CreatedBy], [CreatedAt]) VALUES (11, N'c++', N'zzzzz', N'a', 2, CAST(N'2025-08-28 14:56:50.063' AS DateTime))
SET IDENTITY_INSERT [dbo].[Questions] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (2, N'admin', N'P@ssw0rd', N'Admin')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (3, N'shashi', N'123456', N'User')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (4, N'john', N'@john', N'User')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (5, N'testuser', N'12345', N'Student')
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Role]) VALUES (6, N'Biral', N'@Biral', N'User')
SET IDENTITY_INSERT [dbo].[Users] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__Users__536C85E4A12D77B2]    Script Date: 2025-09-01 11:29:54 ******/
ALTER TABLE [dbo].[Users] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Options]  WITH CHECK ADD FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([QuestionID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserID])
GO
