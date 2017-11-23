USE [master]
GO
/****** Object:  Database [DtsOps]    Script Date: 11/23/2017 11:05:30 AM ******/
CREATE DATABASE [DtsOps]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DtsOps', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\DtsOps.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DtsOps_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\DtsOps_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DtsOps] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DtsOps].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DtsOps] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DtsOps] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DtsOps] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DtsOps] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DtsOps] SET ARITHABORT OFF 
GO
ALTER DATABASE [DtsOps] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DtsOps] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DtsOps] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DtsOps] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DtsOps] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DtsOps] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DtsOps] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DtsOps] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DtsOps] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DtsOps] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DtsOps] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DtsOps] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DtsOps] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DtsOps] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DtsOps] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DtsOps] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [DtsOps] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DtsOps] SET RECOVERY FULL 
GO
ALTER DATABASE [DtsOps] SET  MULTI_USER 
GO
ALTER DATABASE [DtsOps] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DtsOps] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DtsOps] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DtsOps] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DtsOps] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DtsOps', N'ON'
GO
ALTER DATABASE [DtsOps] SET QUERY_STORE = OFF
GO
USE [DtsOps]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DtsOps]
GO
/****** Object:  Table [dbo].[RegionalAccountCodes]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegionalAccountCodes](
	[RegionalAccountCodeId] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [int] NOT NULL,
	[AccountPrefix] [nvarchar](max) NOT NULL,
	[DeptartmentNumber] [int] NOT NULL,
	[FundNumber] [int] NOT NULL,
	[ProjectDescription] [nvarchar](max) NULL,
	[ProjectNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RegionalAccountCodes] PRIMARY KEY CLUSTERED 
(
	[RegionalAccountCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AnnualBudget] [money] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[RegionalAccountCodeId] [int] NOT NULL,
	[ShredNo] [int] NULL,
	[SubNo] [int] NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vAccounts]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vAccounts] AS SELECT  Accounts.AccountId AS AccountId, 	     Accounts.RegionalAccountCodeId AS RegionalAccountCodeId,         Regional.AccountNumber AS AccountNumber,         Accounts.SubNo AS SubNo,         Accounts.ShredNo AS ShredNo,         Accounts.Description AS Description,         Accounts.AnnualBudget AS AnnualBudget,         Regional.FundNumber AS FundNumber,         Regional.ProjectNumber AS ProjectNumber,         Regional.ProjectDescription AS ProjectDescription,         Regional.AccountPrefix AS AccountPrefix     FROM Accounts    JOIN RegionalAccountCodes AS Regional ON Regional.RegionalAccountCodeId = Accounts.RegionalAccountCodeId
GO
/****** Object:  UserDefinedFunction [dbo].[DamLevDistance]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Computes and returns the Damerau-Levenshtein edit distance between two strings, 
-- i.e. the number of insertion, deletion, substitution, and transposition edits
-- required to transform one string to the other.  This value will be >= 0, where
-- 0 indicates identical strings. Comparisons use the case-sensitivity configured
-- in SQL Server (case-insensitive by default). This algorithm is basically the
-- Levenshtein algorithm with a modification that considers transposition of two
-- adjacent characters as a single edit.
-- http://blog.softwx.net/2015/01/optimizing-damerau-levenshtein_19.html
-- See http://en.wikipedia.org/wiki/Damerau%E2%80%93Levenshtein_distance
-- Note that this uses Sten Hjelmqvist's "Fast, memory efficient" algorithm, described
-- at http://www.codeproject.com/Articles/13525/Fast-memory-efficient-Levenshtein-algorithm.
-- This version differs by including some optimizations, and extending it to the Damerau-
-- Levenshtein algorithm.
-- Note that this is the simpler and faster optimal string alignment (aka restricted edit) distance
-- that difers slightly from the full Damerau-Levenshtein algorithm by imposing the restriction
-- that no substring is edited more than once. So for example, "CA" to "ABC" has an edit distance
-- of 2 by a complete application of Damerau-Levenshtein, but a distance of 3 by this method that
-- uses the optimal string alignment algorithm. See wikipedia article for more detail on this
-- distinction.
-- 
-- @s - String being compared for distance.
-- @t - String being compared against other string.
-- @max - Maximum distance allowed, or NULL if no maximum is desired. Returns NULL if distance will exceed @max.
-- returns int edit distance, >= 0 representing the number of edits required to transform one string to the other.
-- =============================================
 
CREATE FUNCTION [dbo].[DamLevDistance](
 
    @s nvarchar(4000)
  , @t nvarchar(4000)
  , @max int
)
RETURNS int
WITH SCHEMABINDING
AS
BEGIN
    DECLARE @distance int = 0 -- return variable
          , @v0 nvarchar(4000)-- running scratchpad for storing computed distances
          , @v2 nvarchar(4000)-- running scratchpad for storing previous column's computed distances
          , @start int = 1      -- index (1 based) of first non-matching character between the two string
          , @i int, @j int      -- loop counters: i for s string and j for t string
          , @diag int          -- distance in cell diagonally above and left if we were using an m by n matrix
          , @left int          -- distance in cell to the left if we were using an m by n matrix
          , @nextTransCost int-- transposition base cost for next iteration 
          , @thisTransCost int-- transposition base cost (2 distant along diagonal) for current iteration
          , @sChar nchar      -- character at index i from s string
          , @tChar nchar      -- character at index j from t string
          , @thisJ int          -- temporary storage of @j to allow SELECT combining
          , @jOffset int      -- offset used to calculate starting value for j loop
          , @jEnd int          -- ending value for j loop (stopping point for processing a column)
          -- get input string lengths including any trailing spaces (which SQL Server would otherwise ignore)
          , @sLen int = datalength(@s) / datalength(left(left(@s, 1) + '.', 1))    -- length of smaller string
          , @tLen int = datalength(@t) / datalength(left(left(@t, 1) + '.', 1))    -- length of larger string
          , @lenDiff int      -- difference in length between the two strings
    -- if strings of different lengths, ensure shorter string is in s. This can result in a little
    -- faster speed by spending more time spinning just the inner loop during the main processing.
    IF (@sLen > @tLen) BEGIN
        SELECT @v0 = @s, @i = @sLen -- temporarily use v0 for swap
        SELECT @s = @t, @sLen = @tLen
        SELECT @t = @v0, @tLen = @i
    END
    SELECT @max = ISNULL(@max, @tLen)
         , @lenDiff = @tLen - @sLen
    IF @lenDiff > @max RETURN NULL
 
    -- suffix common to both strings can be ignored
    WHILE(@sLen > 0 AND SUBSTRING(@s, @sLen, 1) = SUBSTRING(@t, @tLen, 1))
        SELECT @sLen = @sLen - 1, @tLen = @tLen - 1
 
    IF (@sLen = 0) RETURN @tLen
 
    -- prefix common to both strings can be ignored
    WHILE (@start < @sLen AND SUBSTRING(@s, @start, 1) = SUBSTRING(@t, @start, 1)) 
        SELECT @start = @start + 1
    IF (@start > 1) BEGIN
        SELECT @sLen = @sLen - (@start - 1)
             , @tLen = @tLen - (@start - 1)
 
        -- if all of shorter string matches prefix and/or suffix of longer string, then
        -- edit distance is just the delete of additional characters present in longer string
        IF (@sLen <= 0) RETURN @tLen
 
        SELECT @s = SUBSTRING(@s, @start, @sLen)
             , @t = SUBSTRING(@t, @start, @tLen)
    END
 
    -- initialize v0 array of distances
    SELECT @v0 = '', @j = 1
    WHILE (@j <= @tLen) BEGIN
        SELECT @v0 = @v0 + NCHAR(CASE WHEN @j > @max THEN @max ELSE @j END)
        SELECT @j = @j + 1
    END
     
    SELECT @v2 = @v0 -- copy...doesn't matter what's in v2, just need to initialize its size
         , @jOffset = @max - @lenDiff
         , @i = 1
    WHILE (@i <= @sLen) BEGIN
        SELECT @distance = @i
             , @diag = @i - 1
             , @sChar = SUBSTRING(@s, @i, 1)
             -- no need to look beyond window of upper left diagonal (@i) + @max cells
             -- and the lower right diagonal (@i - @lenDiff) - @max cells
             , @j = CASE WHEN @i <= @jOffset THEN 1 ELSE @i - @jOffset END
             , @jEnd = CASE WHEN @i + @max >= @tLen THEN @tLen ELSE @i + @max END
             , @thisTransCost = 0
        WHILE (@j <= @jEnd) BEGIN
            -- at this point, @distance holds the previous value (the cell above if we were using an m by n matrix)
            SELECT @nextTransCost = UNICODE(SUBSTRING(@v2, @j, 1))
                 , @v2 = STUFF(@v2, @j, 1, NCHAR(@diag))
                 , @tChar = SUBSTRING(@t, @j, 1)
                 , @left = UNICODE(SUBSTRING(@v0, @j, 1))
                 , @thisJ = @j
            SELECT @distance = CASE WHEN @diag < @left AND @diag < @distance THEN @diag    --substitution
                                    WHEN @left < @distance THEN @left                    -- insertion
                                    ELSE @distance                                        -- deletion
                                END
            SELECT @distance = CASE WHEN (@sChar = @tChar) THEN @diag    -- no change (characters match)
                                    WHEN @i <> 1 AND @j <> 1
                                        AND @tChar = SUBSTRING(@s, @i - 1, 1)
                                        AND @thisTransCost < @distance
                                        AND @sChar = SUBSTRING(@t, @j - 1, 1)
                                        THEN 1 + @thisTransCost        -- transposition
                                    ELSE 1 + @distance END
            SELECT @v0 = STUFF(@v0, @thisJ, 1, NCHAR(@distance))
                 , @diag = @left
                 , @thisTransCost = @nextTransCost
                 , @j = case when (@distance > @max) AND (@thisJ = @i + @lenDiff) then @jEnd + 2 else @thisJ + 1 end
        END
        SELECT @i = CASE WHEN @j > @jEnd + 1 THEN @sLen + 1 ELSE @i + 1 END
    END
    RETURN CASE WHEN @distance <= @max THEN @distance ELSE NULL END
END
GO
/****** Object:  Table [dbo].[CityAccounts]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityAccounts](
	[CityAccountId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_CityAccounts] PRIMARY KEY CLUSTERED 
(
	[CityAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CityExpenses]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityExpenses](
	[CityExpenseId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceAccountId] [int] NOT NULL,
	[CityAccountId] [int] NOT NULL,
	[Expense] [money] NOT NULL,
 CONSTRAINT [PK_CityExpenses_1] PRIMARY KEY CLUSTERED 
(
	[CityExpenseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceAccounts]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceAccounts](
	[InvoiceAccountId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[Expense] [money] NOT NULL,
 CONSTRAINT [PK_InvoiceAccounts_1] PRIMARY KEY CLUSTERED 
(
	[InvoiceAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[InvoiceDate] [datetime2](7) NOT NULL,
	[DatePaid] [datetime2](7) NOT NULL,
	[InvoiceTypeId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[InvoiceNumber] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceTypes]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceTypes](
	[InvoiceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_InvoiceType] PRIMARY KEY CLUSTERED 
(
	[InvoiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materials]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materials](
	[MaterialId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Unit] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED 
(
	[MaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialVendors]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialVendors](
	[MaterialVendorId] [int] IDENTITY(1,1) NOT NULL,
	[MaterialId] [int] NOT NULL,
	[VendorId] [int] NOT NULL,
	[Cost] [money] NOT NULL,
 CONSTRAINT [PK_MaterialVendors] PRIMARY KEY CLUSTERED 
(
	[MaterialVendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tickets](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[MaterialVendorId] [int] NOT NULL,
	[TicketNumber] [nvarchar](max) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[Quantity] [float] NOT NULL,
	[Cost] [money] NOT NULL,
	[InvoiceId] [int] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transfers]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfers](
	[TransferId] [int] IDENTITY(1,1) NOT NULL,
	[FromAccountId] [int] NOT NULL,
	[ToAccountId] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_Transfers] PRIMARY KEY CLUSTERED 
(
	[TransferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Salt] [varbinary](max) NOT NULL,
	[Permissions] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[ContractEnd] [datetime2](7) NULL,
	[ContractNumber] [nvarchar](45) NULL,
	[ContractStart] [datetime2](7) NULL,
	[Email] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](12) NULL,
	[PointOfContact] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Vendors] PRIMARY KEY CLUSTERED 
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_Accounts_RegionalAccountCodeId_SubNo_ShredNo]    Script Date: 11/23/2017 11:05:31 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Accounts_RegionalAccountCodeId_SubNo_ShredNo] ON [dbo].[Accounts]
(
	[RegionalAccountCodeId] ASC,
	[SubNo] ASC,
	[ShredNo] ASC
)
WHERE ([SubNo] IS NOT NULL AND [ShredNo] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InvoiceAccounts_AccountId]    Script Date: 11/23/2017 11:05:31 AM ******/
CREATE NONCLUSTERED INDEX [IX_InvoiceAccounts_AccountId] ON [dbo].[InvoiceAccounts]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_InvoiceTypeId]    Script Date: 11/23/2017 11:05:31 AM ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_InvoiceTypeId] ON [dbo].[Invoices]
(
	[InvoiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_VendorId]    Script Date: 11/23/2017 11:05:31 AM ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_VendorId] ON [dbo].[Invoices]
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transfers]    Script Date: 11/23/2017 11:05:31 AM ******/
CREATE NONCLUSTERED INDEX [IX_Transfers] ON [dbo].[Transfers]
(
	[TransferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((0.0)) FOR [AnnualBudget]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((-1)) FOR [ShredNo]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((-1)) FOR [SubNo]
GO
ALTER TABLE [dbo].[CityExpenses] ADD  CONSTRAINT [DF_CityExpenses_Expense]  DEFAULT ((0)) FOR [Expense]
GO
ALTER TABLE [dbo].[InvoiceAccounts] ADD  CONSTRAINT [DF__InvoiceAc__Expen__35BCFE0A]  DEFAULT ((0.0)) FOR [Expense]
GO
ALTER TABLE [dbo].[Transfers] ADD  CONSTRAINT [DF_Transfers_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[Vendors] ADD  CONSTRAINT [DF_Vendors_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_RegionalAccountCodes_RegionalAccountCodeId] FOREIGN KEY([RegionalAccountCodeId])
REFERENCES [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_RegionalAccountCodes_RegionalAccountCodeId]
GO
ALTER TABLE [dbo].[CityExpenses]  WITH CHECK ADD  CONSTRAINT [FK_CityExpenses_CityAccounts] FOREIGN KEY([CityAccountId])
REFERENCES [dbo].[CityAccounts] ([CityAccountId])
GO
ALTER TABLE [dbo].[CityExpenses] CHECK CONSTRAINT [FK_CityExpenses_CityAccounts]
GO
ALTER TABLE [dbo].[CityExpenses]  WITH CHECK ADD  CONSTRAINT [FK_CityExpenses_InvoiceAccounts] FOREIGN KEY([InvoiceAccountId])
REFERENCES [dbo].[InvoiceAccounts] ([InvoiceAccountId])
GO
ALTER TABLE [dbo].[CityExpenses] CHECK CONSTRAINT [FK_CityExpenses_InvoiceAccounts]
GO
ALTER TABLE [dbo].[InvoiceAccounts]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceAccounts_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceAccounts] CHECK CONSTRAINT [FK_InvoiceAccounts_Accounts_AccountId]
GO
ALTER TABLE [dbo].[InvoiceAccounts]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceAccounts_Invoices_InvoiceId] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([InvoiceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvoiceAccounts] CHECK CONSTRAINT [FK_InvoiceAccounts_Invoices_InvoiceId]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_InvoiceType_InvoiceTypeId] FOREIGN KEY([InvoiceTypeId])
REFERENCES [dbo].[InvoiceTypes] ([InvoiceTypeId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_InvoiceType_InvoiceTypeId]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Vendors_VendorId] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendors] ([VendorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Vendors_VendorId]
GO
ALTER TABLE [dbo].[MaterialVendors]  WITH CHECK ADD  CONSTRAINT [FK_MaterialVendors_Materials] FOREIGN KEY([MaterialId])
REFERENCES [dbo].[Materials] ([MaterialId])
GO
ALTER TABLE [dbo].[MaterialVendors] CHECK CONSTRAINT [FK_MaterialVendors_Materials]
GO
ALTER TABLE [dbo].[MaterialVendors]  WITH CHECK ADD  CONSTRAINT [FK_MaterialVendors_Vendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendors] ([VendorId])
GO
ALTER TABLE [dbo].[MaterialVendors] CHECK CONSTRAINT [FK_MaterialVendors_Vendors]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Accounts]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([InvoiceId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Invoice]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_MaterialVendors] FOREIGN KEY([MaterialVendorId])
REFERENCES [dbo].[MaterialVendors] ([MaterialVendorId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_MaterialVendors]
GO
ALTER TABLE [dbo].[Tickets]  WITH CHECK ADD  CONSTRAINT [FK_Tickets_Vendors] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendors] ([VendorId])
GO
ALTER TABLE [dbo].[Tickets] CHECK CONSTRAINT [FK_Tickets_Vendors]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_FromAccount_Transfers] FOREIGN KEY([FromAccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_FromAccount_Transfers]
GO
ALTER TABLE [dbo].[Transfers]  WITH CHECK ADD  CONSTRAINT [FK_ToAccount_Transfers] FOREIGN KEY([ToAccountId])
REFERENCES [dbo].[Accounts] ([AccountId])
GO
ALTER TABLE [dbo].[Transfers] CHECK CONSTRAINT [FK_ToAccount_Transfers]
GO
/****** Object:  StoredProcedure [dbo].[Search]    Script Date: 11/23/2017 11:05:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Search]
	-- Add the parameters for the stored procedure here
	@searchString nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @threshold float = 0.25

    --INVOICES--
	SELECT  invoice.InvoiceId AS Id, 
			invoice.InvoiceNumber  AS Name, 
			vendor.Name AS SubName,
			'Invoice' AS Type,
			(SELECT SUM(Expense)
					FROM InvoiceAccounts
					WHERE InvoiceId = invoice.InvoiceId) AS Expense,
			invoice.Description AS Description,
			dbo.DamLevDistance(invoice.InvoiceNumber, @searchString, Len(invoice.InvoiceNumber) *@threshold) AS Relavance

		FROM Invoices AS invoice
			JOIN Vendors AS vendor ON vendor.VendorId = invoice.VendorId
		WHERE dbo.DamLevDistance(invoice.InvoiceNumber, @searchString, Len(invoice.InvoiceNumber) *@threshold) IS NOT NULL
			OR invoice.InvoiceNumber LIKE '%' +@searchString +'%'

	UNION
	--VENDORS--
	SELECT  vendor.VendorId AS Id, 
			vendor.Name  AS Name, 
			vendor.ContractNumber AS SubName,
			'Vendor' AS Type,
			NULL AS Expense,
			NULL AS Description,
			dbo.DamLevDistance(vendor.Name, @searchString, Len(vendor.Name) *@threshold) AS Relavance

		FROM Vendors AS vendor
		WHERE dbo.DamLevDistance(vendor.Name, @searchString, Len(vendor.Name) *@threshold) IS NOT NULL
			OR vendor.Name LIKE '%' +@searchString +'%'

	UNION
	--TICKETS--
	SELECT  ticket.TicketId AS Id, 
			ticket.TicketNumber  AS Name, 
			vendor.Name AS SubName,
			'Ticket' AS Type,
			ticket.Cost AS Expense,
			NULL AS Description,
			dbo.DamLevDistance(ticket.TicketNumber, @searchString, Len(ticket.TicketNumber) *@threshold) AS Relavance

		FROM Tickets AS ticket
			JOIN Vendors AS vendor ON vendor.VendorId = ticket.VendorId
		WHERE dbo.DamLevDistance(ticket.TicketNumber, @searchString, Len(ticket.TicketNumber) *@threshold) IS NOT NULL
			OR ticket.TicketNumber LIKE '%' +@searchString +'%'


	UNION
	--Materials--
	SELECT  material.MaterialId AS Id, 
			material.Name  AS Name, 
			material.Unit AS SubName,
			'Material' AS Type,
			NULL AS Expense,
			NULL AS Description,
			dbo.DamLevDistance(material.Name, @searchString, Len(material.Name) *@threshold) AS Relavance

		FROM Materials AS material
		WHERE dbo.DamLevDistance(material.Name, @searchString, Len(material.Name) *@threshold) IS NOT NULL
			OR material.Name LIKE '%' +@searchString +'%';
END
GO
USE [master]
GO
ALTER DATABASE [DtsOps] SET  READ_WRITE 
GO
