
USE Myflix;

CREATE TABLE [Movie]
(
	MovieId INT PRIMARY KEY IDENTITY,
	MovieTitle NVARCHAR(255) NOT NULL,
	MovieOverview NVARCHAR(MAX),
	MovieReleaseDate DATE,
	MoviePosterPath NVARCHAR(MAX),
	MovieBackdropPath NVARCHAR(MAX),
	MoviePopularity FLOAT,
	MovieVoteAverage FLOAT,
	MovieVoteCount INT
);

CREATE TABLE [Genre]
(
	GenreId INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(100) NOT NULL
);

CREATE TABLE [Actor]
(
	ActorId INT PRIMARY KEY IDENTITY,
	ActorName NVARCHAR(100) NOT NULL,
	ActorBio NVARCHAR(MAX),
	ActorProfilePath NVARCHAR(255),
	ActorWiki NVARCHAR(MAX)
);

CREATE TABLE [MovieGenre]
(
	MG_MovieIdRef INT,
	MG_GenreIdRef INT,
	PRIMARY KEY(MG_MovieIdRef, MG_GenreIdRef),
	CONSTRAINT FK_MG_Movie FOREIGN KEY (MG_MovieIdRef) REFERENCES Movie(MovieId) ON DELETE CASCADE,
	CONSTRAINT FK_MG_Genre FOREIGN KEY (MG_GenreIdRef) REFERENCES Genre(GenreId) ON DELETE CASCADE
);

CREATE TABLE [MovieActor]
(
	MA_MovieIdRef INT,
	MA_ActorIdRef INT,
	PRIMARY KEY(MA_MovieIdRef, MA_ActorIdRef),
	CONSTRAINT FK_MA_Movie FOREIGN KEY (MA_MovieIdRef) REFERENCES Movie(MovieId) ON DELETE CASCADE,
	CONSTRAINT FK_MA_Actor FOREIGN KEY (MA_ActorIdRef) REFERENCES Actor(ActorId) ON DELETE CASCADE
);

CREATE TABLE [Review]
(
	ReviewId INT PRIMARY KEY IDENTITY,
	ReviewText NVARCHAR(MAX),
	ReviewCreatedAt DATETIME DEFAULT GETDATE(),
	Review_UserIdRef NVARCHAR(450),
	Review_MovieIdRef INT,
	Review_ParentReviewIdRef INT NULL,
	CONSTRAINT FK_Review_User FOREIGN KEY (Review_UserIdRef) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Review_Movie FOREIGN KEY (Review_MovieIdRef) REFERENCES Movie(MovieId) ON DELETE CASCADE,
	CONSTRAINT FK_Review_Parent FOREIGN KEY (Review_ParentReviewIdRef) REFERENCES Review(ReviewId)
);

CREATE TABLE [Watchlist]
(
	Watchlist_UserIdRef NVARCHAR(450),
	Watchlist_MovieIdRef INT,
	PRIMARY KEY(Watchlist_UserIdRef, Watchlist_MovieIdRef),
	CONSTRAINT FK_Watchlist_User FOREIGN KEY (Watchlist_UserIdRef) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Watchlist_Movie FOREIGN KEY (Watchlist_MovieIdRef) REFERENCES Movie(MovieId) ON DELETE CASCADE
);

CREATE TABLE [Rating]
(
	Rating_UserIdRef NVARCHAR(450),
	Rating_MovieIdRef INT,
	RatingValue INT CHECK (RatingValue BETWEEN 1 AND 10),
	PRIMARY KEY(Rating_MovieIdRef, Rating_UserIdRef),
	CONSTRAINT FK_Rating_User FOREIGN KEY (Rating_UserIdRef) REFERENCES AspNetUsers(Id) ON DELETE CASCADE,
	CONSTRAINT FK_Rating_Movie FOREIGN KEY (Rating_MovieIdRef) REFERENCES Movie(MovieId) ON DELETE CASCADE
);

RESTORE FILELISTONLY FROM DISK = 'C:\Users\lizak\source\repos\myflix\apps\sql-server\Myflix.bak'