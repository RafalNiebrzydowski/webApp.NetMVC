USE [Geodeta]
GO
/****** Object:  Table [dbo].[Area]    Script Date: 2015-01-18 08:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Area](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[DateMod] [datetime] NOT NULL,
	[IsNewVersion] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Line]    Script Date: 2015-01-18 08:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Line](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AreaId] [int] NOT NULL,
	[NoteId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ListOfPoints]    Script Date: 2015-01-18 08:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListOfPoints](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PointId] [int] NOT NULL,
	[LineId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Note]    Script Date: 2015-01-18 08:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ContentNote] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Point]    Script Date: 2015-01-18 08:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Point](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CoordinateX] [float] NOT NULL,
	[CoordinateY] [float] NOT NULL,
	[NoteId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2015-01-18 08:04:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](20) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Token] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Area] ADD  DEFAULT ((0)) FOR [IsNewVersion]
GO
ALTER TABLE [dbo].[User] ADD  DEFAULT ((1)) FOR [Token]
GO
ALTER TABLE [dbo].[Area]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Line]  WITH CHECK ADD FOREIGN KEY([AreaId])
REFERENCES [dbo].[Area] ([ID])
GO
ALTER TABLE [dbo].[Line]  WITH CHECK ADD FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([ID])
GO
ALTER TABLE [dbo].[ListOfPoints]  WITH CHECK ADD FOREIGN KEY([LineId])
REFERENCES [dbo].[Line] ([ID])
GO
ALTER TABLE [dbo].[ListOfPoints]  WITH CHECK ADD FOREIGN KEY([PointId])
REFERENCES [dbo].[Point] ([ID])
GO
ALTER TABLE [dbo].[Point]  WITH CHECK ADD FOREIGN KEY([NoteId])
REFERENCES [dbo].[Note] ([ID])
GO
