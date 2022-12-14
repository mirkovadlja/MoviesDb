USE [MovieDB]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 9/29/2022 4:14:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[Id] [uniqueidentifier] NOT NULL,
	[TmdbId] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[DateCreated] [date] NULL,
 CONSTRAINT [Genre_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 9/29/2022 4:14:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[Id] [uniqueidentifier] NOT NULL,
	[TmdbId] [int] NOT NULL,
	[OriginalTitle] [nvarchar](255) NULL,
	[ReleaseDate] [date] NULL,
	[DateCreated] [date] NULL,
 CONSTRAINT [Movie_pkey] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieCredit]    Script Date: 9/29/2022 4:14:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieCredit](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MovieGenre]    Script Date: 9/29/2022 4:14:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieGenre](
	[MovieId] [uniqueidentifier] NOT NULL,
	[GenreId] [uniqueidentifier] NOT NULL,
	[DateCreated] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 9/29/2022 4:14:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [uniqueidentifier] NOT NULL,
	[TmdbId] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[DateCreated] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonMovieCredit]    Script Date: 9/29/2022 4:14:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonMovieCredit](
	[MovieId] [uniqueidentifier] NOT NULL,
	[PersonId] [uniqueidentifier] NOT NULL,
	[MovieCreditId] [uniqueidentifier] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MovieGenre]  WITH CHECK ADD FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([Id])
GO
ALTER TABLE [dbo].[MovieGenre]  WITH CHECK ADD FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[PersonMovieCredit]  WITH CHECK ADD FOREIGN KEY([MovieId])
REFERENCES [dbo].[Movie] ([Id])
GO
ALTER TABLE [dbo].[PersonMovieCredit]  WITH CHECK ADD FOREIGN KEY([MovieCreditId])
REFERENCES [dbo].[MovieCredit] ([Id])
GO
ALTER TABLE [dbo].[PersonMovieCredit]  WITH CHECK ADD FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
