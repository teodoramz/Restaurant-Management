USE [master]
GO
/****** Object:  Database [Restaurant-Management]    Script Date: 16-Nov-21 19:35:30 ******/
CREATE DATABASE [Restaurant-Management]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Restaurant-Management', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Restaurant-Management.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Restaurant-Management_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Restaurant-Management_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Restaurant-Management] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Restaurant-Management].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Restaurant-Management] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Restaurant-Management] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Restaurant-Management] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Restaurant-Management] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Restaurant-Management] SET ARITHABORT OFF 
GO
ALTER DATABASE [Restaurant-Management] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Restaurant-Management] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Restaurant-Management] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Restaurant-Management] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Restaurant-Management] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Restaurant-Management] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Restaurant-Management] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Restaurant-Management] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Restaurant-Management] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Restaurant-Management] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Restaurant-Management] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Restaurant-Management] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Restaurant-Management] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Restaurant-Management] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Restaurant-Management] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Restaurant-Management] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Restaurant-Management] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Restaurant-Management] SET RECOVERY FULL 
GO
ALTER DATABASE [Restaurant-Management] SET  MULTI_USER 
GO
ALTER DATABASE [Restaurant-Management] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Restaurant-Management] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Restaurant-Management] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Restaurant-Management] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Restaurant-Management] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Restaurant-Management] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Restaurant-Management', N'ON'
GO
ALTER DATABASE [Restaurant-Management] SET QUERY_STORE = OFF
GO
USE [Restaurant-Management]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 16-Nov-21 19:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
	[Details] [varchar](100) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[History]    Script Date: 16-Nov-21 19:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[History](
	[IdHistory] [int] IDENTITY(1,1) NOT NULL,
	[IdUser] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[TotalPrice] [money] NOT NULL,
 CONSTRAINT [PK_History] PRIMARY KEY CLUSTERED 
(
	[IdHistory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 16-Nov-21 19:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[ProductName] [varchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[Ingredients] [varchar](100) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistrationCodes]    Script Date: 16-Nov-21 19:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistrationCodes](
	[IdRegistration] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[ExpirationDate] [date] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_RegistrationCodes] PRIMARY KEY CLUSTERED 
(
	[IdRegistration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 16-Nov-21 19:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sells]    Script Date: 16-Nov-21 19:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sells](
	[IdSell] [int] IDENTITY(1,1) NOT NULL,
	[IdHistory] [int] NOT NULL,
	[IdProduct] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_Sells] PRIMARY KEY CLUSTERED 
(
	[IdSell] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 16-Nov-21 19:35:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Phone] [varchar](10) NOT NULL,
	[Email] [varchar](50) NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (1, N'Ciorbe', N'vacuta, pui, perisoare, potroace, zarzavat. rosii, fasole, spanac')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (2, N'Fripturi', N'porc, pui, curcan')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (3, N'Sucuri', N'acidulate, naturale')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (4, N'Deserturi', N'prajituri, placinte, clatite, strudel, cheesecake, tiramisu')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (5, N'Bauturi', N'energizante, alcoolice, non-alcoolice')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (6, N'Aperitive', N'mititei, chiftelute')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (7, N'Salate', N'caesar, pui, greceasca')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (8, N'Paste', N'carbonara, quatro formaggi')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (9, N'Peste', N'biban, somon, ton')
GO
INSERT [dbo].[Categories] ([CategoryID], [CategoryName], [Details]) VALUES (10, N'Garnituri', N'cartofi copti, cartofi piure, cartofi prajiti, legume sote')
GO
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (2, 1, N'Ciorba de vacuta', 13.0000, N'apa, carne de vaca, ulei, legume')
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (5, 1, N'Ciorba de pui', 13.0000, N'apa, carne de pui, ulei, legume')
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (6, 1, N'Ciorba de perisoare', 15.0000, N'apa, carne de porc, vita, ulei, legume')
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (7, 1, N'Ciorba de potroace', 12.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (8, 1, N'Ciorba de zarzavat', 13.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (9, 1, N'Ciorba de rosii', 13.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (10, 1, N'Ciorba de fasole', 12.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (11, 1, N'Ciorba de spanac', 15.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (12, 2, N'Friptura de porc', 16.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (13, 2, N'Friptura de pui', 18.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (14, 2, N'Friptura de curcan', 16.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (15, 3, N'Sprite', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (16, 3, N'Coca Cola', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (17, 3, N'Fanta', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (18, 3, N'Prigat', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (22, 3, N'Capi', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (23, 3, N'Pepsi', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (24, 3, N'Timbark', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (25, 4, N'Prajitura Savarina', 10.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (26, 4, N'Placinta de mere', 8.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (27, 4, N'Placinta cu branza', 9.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (29, 4, N'Clatite cu dulceata', 12.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (30, 4, N'Strudel cu mere', 8.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (31, 4, N'Cheesecake', 13.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (32, 4, N'Tiramisu', 12.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (33, 5, N'RedBull', 10.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (34, 5, N'Hell', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (35, 5, N'Bere Ciuc', 3.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (36, 5, N'Bere Timisoreana', 4.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (37, 5, N'Bere Skol', 5.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (38, 5, N'Limonada', 10.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (39, 6, N'Mititei', 4.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (40, 6, N'Chiftelute', 6.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (41, 7, N'Salata Caesar', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (42, 7, N'Salata de pui', 7.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (43, 7, N'Salata greceasca', 8.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (44, 8, N'Paste Carbonara', 12.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (45, 8, N'Paste Quatro Formaggi', 15.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (46, 9, N'Ton', 13.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (47, 9, N'Somon', 13.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (48, 9, N'Biban', 12.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (49, 9, N'Salau', 13.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (50, 9, N'Sturion', 14.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (52, 10, N'Cartofi prajiti', 6.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (53, 10, N'Cartofi copti', 5.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (54, 10, N'Piure', 5.0000, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [ProductCategoryID], [ProductName], [Price], [Ingredients]) VALUES (55, 10, N'Legume sote', 7.0000, NULL)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'admin')
GO
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'employee')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (9, N'Marian Popovici', N'arahide', N'0748596123', N'marian.p@arc.co', 1)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (12, N'Dumitru Ilie', N'cuvinte', N'0726472642', N'dumitru.i@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (13, N'Carina Pop', N'karin223', N'0784965284', N'carina.p@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (15, N'Andrei Marin', N'shukarboss', N'0712584965', N'andrei.m@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (16, N'Cristian Ast', N'wowlol', N'0784953521', N'cristian.a@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (17, N'Catalina Isc', N'kkty221', N'0715352459', N'catalina.i@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (18, N'Teodor Mihai', N'mishu14', N'0713467982', N'teodor.m@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (19, N'Ilisca Susu', N'ili1414', N'0718293827', N'ilisca.s@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (20, N'Casca Ilie', N'ops2424', N'0715595742', N'casca.i@arc.co', 1)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (21, N'Oprea Maria', N'oprik98', N'0728391728', N'oprea.m@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (22, N'Popa Neculina', N'iwejhdy34', N'0707071885', N'popa.n@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (23, N'Petrescu Andrei', N'asdasd23d', N'0751849563', N'petrescu.a@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (24, N'Sulea Patrica', N'34g34gs56', N'0784956322', N'sulea.p@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (25, N'Suhan Alin', N'fgh56h46', N'0784916657', N'suhan.a@arc.co', 2)
GO
INSERT [dbo].[Users] ([UserID], [Username], [Password], [Phone], [Email], [RoleID]) VALUES (26, N'Moraru Ion', N'rhrth34', N'0718816517', N'moraru.i@arc.co', 2)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[History] ADD  CONSTRAINT [DF_History_TotalPrice]  DEFAULT ((0)) FOR [TotalPrice]
GO
ALTER TABLE [dbo].[History]  WITH CHECK ADD  CONSTRAINT [FK_History_Users] FOREIGN KEY([IdUser])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[History] CHECK CONSTRAINT [FK_History_Users]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[RegistrationCodes]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationCodes_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserID])
GO
ALTER TABLE [dbo].[RegistrationCodes] CHECK CONSTRAINT [FK_RegistrationCodes_Users]
GO
ALTER TABLE [dbo].[Sells]  WITH CHECK ADD  CONSTRAINT [FK_Sells_History] FOREIGN KEY([IdHistory])
REFERENCES [dbo].[History] ([IdHistory])
GO
ALTER TABLE [dbo].[Sells] CHECK CONSTRAINT [FK_Sells_History]
GO
ALTER TABLE [dbo].[Sells]  WITH CHECK ADD  CONSTRAINT [FK_Sells_Products] FOREIGN KEY([IdProduct])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[Sells] CHECK CONSTRAINT [FK_Sells_Products]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
USE [master]
GO
ALTER DATABASE [Restaurant-Management] SET  READ_WRITE 
GO
