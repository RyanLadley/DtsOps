USE [master]
GO
/****** Object:  Database [DtsOps]    Script Date: 12/23/2017 7:10:21 PM ******/
CREATE DATABASE [DtsOps]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DtsOps', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\DtsOps.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
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
/****** Object:  Schema [HangFire]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE SCHEMA [HangFire]
GO
/****** Object:  Table [dbo].[RegionalAccountCodes]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  View [dbo].[vAccounts]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vAccounts] AS SELECT  Accounts.AccountId AS AccountId, 	     Accounts.RegionalAccountCodeId AS RegionalAccountCodeId,         Regional.AccountNumber AS AccountNumber,         Accounts.SubNo AS SubNo,         Accounts.ShredNo AS ShredNo,         Accounts.Description AS Description,         Accounts.AnnualBudget AS AnnualBudget,         Regional.FundNumber AS FundNumber,         Regional.ProjectNumber AS ProjectNumber,         Regional.ProjectDescription AS ProjectDescription,         Regional.AccountPrefix AS AccountPrefix     FROM Accounts    JOIN RegionalAccountCodes AS Regional ON Regional.RegionalAccountCodeId = Accounts.RegionalAccountCodeId
GO
/****** Object:  UserDefinedFunction [dbo].[DamLevDistance]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Bugs]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bugs](
	[BugId] [int] IDENTITY(1,1) NOT NULL,
	[Severity] [varchar](25) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CityAccounts]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CityAccounts](
	[CityAccountId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CityAccountNumber] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_CityAccounts] PRIMARY KEY CLUSTERED 
(
	[CityAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CityExpenses]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[InvoiceAccounts]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Invoices]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[InvoiceTypes]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Materials]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[MaterialVendors]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Tickets]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Transfers]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [dbo].[Vendors]    Script Date: 12/23/2017 7:10:22 PM ******/
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
/****** Object:  Table [HangFire].[AggregatedCounter]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[AggregatedCounter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [bigint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_CounterAggregated] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Counter]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Counter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [smallint] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Counter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Hash]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Hash](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Field] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime2](7) NULL,
 CONSTRAINT [PK_HangFire_Hash] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Job]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Job](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NULL,
	[StateName] [nvarchar](20) NULL,
	[InvocationData] [nvarchar](max) NOT NULL,
	[Arguments] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobParameter]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobParameter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_JobParameter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[JobQueue]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[JobQueue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Queue] [nvarchar](50) NOT NULL,
	[FetchedAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_JobQueue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[List]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[List](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Value] [nvarchar](max) NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_List] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Schema]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Schema](
	[Version] [int] NOT NULL,
 CONSTRAINT [PK_HangFire_Schema] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Server]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Server](
	[Id] [nvarchar](100) NOT NULL,
	[Data] [nvarchar](max) NULL,
	[LastHeartbeat] [datetime] NOT NULL,
 CONSTRAINT [PK_HangFire_Server] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[Set]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[Set](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[Score] [float] NOT NULL,
	[Value] [nvarchar](256) NOT NULL,
	[ExpireAt] [datetime] NULL,
 CONSTRAINT [PK_HangFire_Set] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [HangFire].[State]    Script Date: 12/23/2017 7:10:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [HangFire].[State](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Reason] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Data] [nvarchar](max) NULL,
 CONSTRAINT [PK_HangFire_State] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (1, 0.0000, N'In House Resurfacing', 1, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (2, 0.0000, N'Maitenance Paving', 1, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (4, 0.0000, N'Structural Digout', 2, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (5, 0.0000, N'North District Digout', 2, 1, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (6, 0.0000, N'South District Digout', 2, 2, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (7, 0.0000, N'East District Digout', 2, 3, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (8, 0.0000, N'West District Digout', 2, 4, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (9, 0.0000, N'District Maintenance Paving', 1, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (10, 0.0000, N'North Maintenence Paving', 1, 1, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (11, 0.0000, N'South Maintenance Paving', 1, 2, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (12, 0.0000, N'East Maintenance Paving', 1, 3, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (13, 0.0000, N'West Maintenance Paving', 1, 4, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (14, 0.0000, N'Pothole Patching Repair', 3, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (15, 0.0000, N'Asphalt Repair', 3, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (16, 0.0000, N'Propane', 3, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (17, 0.0000, N'Shovels/Rakes/etc', 3, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (18, 0.0000, N'In House Pipe', 7, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (19, 0.0000, N'Pipe Materials', 7, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (20, 0.0000, N'Saws/Hand Tools/Etc', 7, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (21, 0.0000, N'In House Concrete', 4, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (22, 0.0000, N'Concrete Materials', 4, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (23, 0.0000, N'Tool/Supplies', 4, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (24, 0.0000, N'In House Project Support', 5, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (25, 0.0000, N'Maint Paving Manpower', 5, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (26, 0.0000, N'Maint Paving Rental Equip', 5, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (27, 0.0000, N'Maint District Manpower', 5, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (28, 0.0000, N'North District Manpower', 5, 1, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (29, 0.0000, N'South District Manpower', 5, 2, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (30, 0.0000, N'East District Manpower', 5, 3, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (31, 0.0000, N'West District Manpower', 5, 4, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (32, 0.0000, N'Maint District Rental Equip', 5, NULL, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (33, 0.0000, N'North District Rental Equip', 5, 1, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (34, 0.0000, N'South District Rental Equip', 5, 2, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (35, 0.0000, N'East District Rental Equip', 5, 3, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (36, 0.0000, N'West District Rental Equip', 5, 4, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (37, 0.0000, N'In House Pipe Manpower', 5, NULL, 5)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (38, 0.0000, N'In House Pipe Rental Equip', 5, NULL, 6)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (39, 0.0000, N'In House Concrete Manpower', 5, NULL, 7)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (40, 0.0000, N'In House Concrete Equip', 5, NULL, 8)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (41, 0.0000, N'Signs & Markings Manpower', 5, NULL, 9)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (42, 0.0000, N'Reserves', 5, NULL, 10)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (43, 0.0000, N'Signs & Markings', 6, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (44, 0.0000, N'Crosswalks/School Legends', 6, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (45, 0.0000, N'Long Line Contract', 6, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (46, 0.0000, N'District Mill/Pave Rental', 5, 1, 10)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (47, 0.0000, N'Misc.', 5, 2, 10)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (48, 0.0000, N'Saftey', 5, NULL, 11)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (49, 0.0000, N'Roadway Paint', 6, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (50, 0.0000, N'Sign Mainenance', 6, NULL, 4)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
SET IDENTITY_INSERT [dbo].[CityAccounts] ON 

INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (4, N'Aggregate Material', N'52185')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (5, N'Civilian Salaries', N'51205')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (6, N'Overtime', N'51210')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (7, N'Seasonal Tempory Positions', N'51220')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (8, N'Special Assignment  Pay', N'51250')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (9, N'PERA', N'51610')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (10, N'Worker''s  Comp.', N'51615')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (11, N'Equitable Life Insurance', N'51620')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (12, N'Dental Insurance', N'51640')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (13, N'Medicare', N'51690')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (14, N'City EPO Medical Plan', N'51695')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (15, N'Office Supplies', N'52110')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (16, N'Paper Supplies', N'52111')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (17, N'Computer Software', N'52120')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (18, N'Cell Phone Equipment', N'52122')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (19, N'General Supplies', N'52125')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (20, N'Construction  Supplies', N'52127')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (21, N'Concrete Supplies', N'52131')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (22, N'Postage', N'52135')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (23, N'Wearing Apparel', N'52140')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (24, N'Paint & Chemical', N'52145')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (25, N'Seed & Fertilizer', N'52150')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (26, N'Fuel', N'52160')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (27, N'Licenses & Tags', N'52165')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (28, N'Signs', N'52175')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (29, N'Asphaltic Material', N'52180')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (30, N'Janitorial Supplies', N'52190')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (31, N'Stormwater Permit', N'52192')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (32, N'Maintenance Trees', N'52210')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (33, N'Maintenance  Office  Machines', N'52220')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (34, N'Maintenance  Machines/App', N'52235')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (35, N'Maintenance  Signs', N'52255')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (36, N'Maintenance Building & Str', N'52265')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (37, N'Maintenance  Infrastructure', N'52281')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (38, N'Advertising Services', N'52405')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (39, N'Building Security', N'52410')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (40, N'Contracts & Special Projects', N'52415')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (41, N'Telecommunication Service', N'52423')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (42, N'Environmental  Services', N'52425')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (43, N'Mun Fac Runoff', N'52426')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (44, N'Consulting  Services', N'52431')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (45, N'Garbage Removal', N'52435')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (46, N'Debris Waste Service', N'52437')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (47, N'Janitorial Services', N'52445')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (48, N'Parking Services', N'52560')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (49, N'Snow Removal', N'52571')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (50, N'General Services', N'52575')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (51, N'Temperary Employment', N'52590')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (52, N'Dues & Memership', N'52615')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (53, N'In Town Meeting Expenses', N'52625')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (54, N'Training', N'52630')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (55, N'Subscriptions', N'52645')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (56, N'Communications', N'52705')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (57, N'Wireless Communication', N'52706')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (58, N'Rental Property', N'52725')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (59, N'State/County/PPRTA Tax', N'52731')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (60, N'Long Distance Phone', N'52735')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (61, N'Cell Air Time', N'52736')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (62, N'Cell Base', N'52738')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (63, N'Utilities - Electric', N'52746')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (64, N'Utilities - Gas', N'52747')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (65, N'Utilities - Sewer', N'52748')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (66, N'Utilities - Water', N'52749')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (67, N'Lease Purchase Payment', N'52765')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (68, N'Saftey Equipment', N'52770')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (69, N'Minor Equipment/Tools', N'52775')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (70, N'Rental of Equipment', N'52795')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (71, N'Charges for Community Service', N'52811')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (72, N'Maint. Fleet Vehicles & Equipment', N'52872')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (73, N'Office Services - Print', N'52874')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (74, N'Rental to Fleet Vehicles', N'52893')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (75, N'Recruitment', N'65160')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (76, N'Constructoin Costs', N'65280')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (77, N'Vehicle Insurance', N'65325')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (78, N'Emplyee Awards', N'65352')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (79, N'Retire Awards', N'65356')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (80, N'Computer Network', N'53020')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (81, N'Machinery & Apparatus', N'53050')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (82, N'Vehicles Addition', N'53080')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name], [CityAccountNumber]) VALUES (83, N'Building and Structures', N'53090')
SET IDENTITY_INSERT [dbo].[CityAccounts] OFF
SET IDENTITY_INSERT [dbo].[InvoiceTypes] ON 

INSERT [dbo].[InvoiceTypes] ([InvoiceTypeId], [Name]) VALUES (1, N'Equipment')
INSERT [dbo].[InvoiceTypes] ([InvoiceTypeId], [Name]) VALUES (2, N'Labor')
INSERT [dbo].[InvoiceTypes] ([InvoiceTypeId], [Name]) VALUES (3, N'Material')
SET IDENTITY_INSERT [dbo].[InvoiceTypes] OFF
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (5, N'Plant mixed asphaltic surfacing material grading SC PG 64-22', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (7, N'Plant mixed asphaltic surfacing material grading SX PG 64-28', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (9, N'CSS-1H liquid emulsified asphalt', N'Gallon')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (11, N'Plant mixed asphaltic surfacing material grading 3/8 minus plant mix seal (PMS) PG 64-22', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (12, N'Plant mixed asphaltic surfacing material grading S PG 64-28', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (13, N'Plant mixed asphaltic surfacing material grading S PG 64-34', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (14, N'Cold mix for winter patching', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (15, N'Plant-mixed asphaltic surfacing material, grading S PG 58-28', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (16, N'Capital Plant S PG 64-22', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (17, N'PPM', N'Ton')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (18, N'Plant mix 3/8"', N'Ton')
SET IDENTITY_INSERT [dbo].[Materials] OFF
SET IDENTITY_INSERT [dbo].[MaterialVendors] ON 

INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (14, 5, 16, 45.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (15, 7, 16, 56.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (16, 9, 16, 3.7500)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (17, 11, 16, 61.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (18, 12, 16, 55.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (19, 14, 16, 135.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (20, 17, 18, 115.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (21, 5, 18, 48.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (22, 9, 18, 3.5000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (23, 14, 18, 95.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (24, 18, 17, 45.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (25, 16, 17, 47.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (26, 5, 17, 45.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (27, 9, 17, 3.5000)
SET IDENTITY_INSERT [dbo].[MaterialVendors] OFF
SET IDENTITY_INSERT [dbo].[RegionalAccountCodes] ON 

INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (1, 5221000, N'03-30', 9000, 935, N'Maintenance Paving', N'3900003')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (2, 5221000, N'03-30', 9000, 935, N'Digout', N'3900004')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (3, 5222000, N'03-30', 9000, 935, N'Pothole', N'3900005')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (4, 5224000, N'03-30', 9000, 935, N'Concrete', N'3900011')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (5, 5225000, N'03-30', 9000, 935, N'Project Suport', N'3900012')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (6, 5226000, N'03-30', 9000, 935, N'Signs & Markings', N'3900014')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (7, 5223000, N'03-30', 9000, 935, N'Pipe', N'3900007')
SET IDENTITY_INSERT [dbo].[RegionalAccountCodes] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Email], [FirstName], [LastName], [Password], [Salt], [Permissions]) VALUES (2, N'ladley.ryan@gmail.com', N'Ryan', N'Ladley', N'URCErr0ZaG8irKcMFCpJ94Bct2aU5p1AwHs6l9C2kzw=', 0xF51238762AAFC3E0CCBC4E7988FB5DE9, 0)
INSERT [dbo].[Users] ([UserId], [Email], [FirstName], [LastName], [Password], [Salt], [Permissions]) VALUES (3, N'user@email.com', N'User', N'Testerson', N'IWsXNg2Njkdx8vTBw/PjlPoVXFdGnVfTt34EAmJ7ZeE=', 0x32EC0EFED0D2CCCBF2F5A64556030353, 1)
INSERT [dbo].[Users] ([UserId], [Email], [FirstName], [LastName], [Password], [Salt], [Permissions]) VALUES (1002, N'jladley@springsgov.com ', N'Jack', N'Ladley', N'nnCt9uKVFFjGFOqAGg6GUINxllpTxFlC6JzuNZsYz84=', 0x21968F21D6DF25E0BFFDD6774744D5DB, 0)
INSERT [dbo].[Users] ([UserId], [Email], [FirstName], [LastName], [Password], [Salt], [Permissions]) VALUES (1003, N'mdaniel@springsgov.com', N'Megan', N'Daniel', N'xqKXq8sKQJegvm1QbHFAeik4pUOJBsVoM9FMqApfZ8c=', 0x77130A8136CF12B105B600C6CE266908, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
SET IDENTITY_INSERT [dbo].[Vendors] ON 

INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (16, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), N'Unknown', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'Unknown@email.com', N'Kiewit', N'Unknown', N'Unknown', N'Unknown', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (17, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), N'Unknown', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'Uknown@email.com', N'Martin Marietta', N'Unknown', N'Unknown', N'Unknown', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (18, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), N'Unknown', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'Unknown@email.com', N'Schmidt', N'Unknown', N'Unknown', N'Unknown', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (19, CAST(N'2018-02-01T00:00:00.0000000' AS DateTime2), N'Unknown', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'mdaniel@springsgov.com', N'Verizon Wireless', N'7193856801', N'Megan Daniel', N'www.vzw.com/mybusinessaccount', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (20, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), N'M007492', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Glaser Energy Group Inc.', N'7195964765', N'Glaser', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (21, CAST(N'2018-01-01T00:00:00.0000000' AS DateTime2), N'M004394', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'ar@BillsToolRental.com', N'Springs Contractor Supply', N'7193142262', N'Bill', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (22, CAST(N'2021-04-28T00:00:00.0000000' AS DateTime2), N'M008186', CAST(N'2016-04-29T00:00:00.0000000' AS DateTime2), N'dean@safetystation.net', N'Safety Station', N'7195502850', N'Dean Bennett', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (23, CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), N'M008381', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'HD Supply Construction', N'7192640995', N'045 - Colorado Springs', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (24, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'A007887', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'Denise.Brown@ExpressPros.com', N'Express Services', N'7193901300', N'Denise Brown', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (25, CAST(N'2017-12-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-24T00:00:00.0000000' AS DateTime2), NULL, N'Cobitco Inc', N'3032968575', N'N/A', N'www.cobitco.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (26, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'jrowe@wagnerequipment.com', N'Wagner Equipment CO', N'7193309125', N'John Rowe', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (27, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'coloradoreuse@gmail.com', N'JD''s Propane Service', N'7194810556', N'Joshua Hosler', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (28, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'A006533', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Pete Lien & Sons Inc', N'6053427224', N'N/A', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (29, CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Kennametal', N'18004583608', N'Vicki', N'www.kennametal.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (30, CAST(N'2021-09-30T00:00:00.0000000' AS DateTime2), N'T008356', CAST(N'2016-09-19T00:00:00.0000000' AS DateTime2), N'kdreher@4riversequipment.com', N'4 Rivers Equipment', N'7194995730', N'Kevin Dreher', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (31, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'todd@dissco.net', N'DISSCO', N'3039352485', N'Todd Mellema', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (32, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-02-03T00:00:00.0000000' AS DateTime2), NULL, N'Summa Inc', N'2065271050', N'N/A', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (33, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-02-07T00:00:00.0000000' AS DateTime2), N'rkolberg@bobcatoftherockies.com', N'Bobcat of the Rockies', N'7192192940', N'Ryan Kolberg', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (34, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-02-01T00:00:00.0000000' AS DateTime2), N'dave.m@maxwellproducts.com', N'Maxwell Products', NULL, N'Dave McIntosh', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (35, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-02-01T00:00:00.0000000' AS DateTime2), NULL, N'Fleet', N'7193856801', N'Colorado Springs Fleet', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (36, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'R006664', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'dmartin@usstandardsign.com', N'US Standard Sign Co', N'N/A', N'Daniel Martin', N'N/A', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (37, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'apatel@ennisflint.com', N'Flint Trading Inc', N'336-308-3775', N'Arati Patel', N'www.ennisflint.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (38, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'karen.gresham@hilit.com', N'Hilti Inc', N'800-879-8000', N'Karen Gresham', N'www.us.hilti.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (39, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Reliance Metal', N'7193904911', N'Reliance Metal', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (40, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'mhaptonstall@springsgov.com', N'ADD Staff', N'719385277', N'Accounts Payable', N'N/A', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (41, CAST(N'2021-08-10T00:00:00.0000000' AS DateTime2), N'M008334', CAST(N'2016-08-11T00:00:00.0000000' AS DateTime2), N'accounting@whsupply.com', N'Western Hardscape Supply', N'7195500070', N'N/A', N'www.whsupply.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (42, CAST(N'2021-09-19T00:00:00.0000000' AS DateTime2), N'T008357', CAST(N'2016-09-19T00:00:00.0000000' AS DateTime2), N'DCorley@power-equip.com', N'Power Equipment Company', N'303-475-7924', N'Doug Corley', N'www.power-equip.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (43, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'patrick@ewslag.com', N'EWS Steel Aggregate LLC', N'7194920706', N'Patrick Malfitano', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (44, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'kwoodard@mfcpinc.com', N'Motion & Flow Control Products', N'7192648275', N'Kaleb Woodard', N'n/a', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (45, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Safety Kleen', N'8003235040', N'Safety Kleen', N'www.safety-kleen.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (46, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'M008334', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Western Hardscape Supply', N'7195500070', N'Western Hardscape Supply', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (47, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-04-01T00:00:00.0000000' AS DateTime2), N'greg.wurster@titanmachinery.com', N'Titan Machinery', N'7196604474', N'Greg Wurster', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (48, CAST(N'2017-12-31T00:00:00.0000000' AS DateTime2), N'T006947', CAST(N'2017-01-20T00:00:00.0000000' AS DateTime2), N'kendra.stamper@potterbeads.com', N'Potter Industries LLC', NULL, N'Kendra Stamper', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (49, CAST(N'2017-10-31T00:00:00.0000000' AS DateTime2), N'T008409', CAST(N'2016-11-14T00:00:00.0000000' AS DateTime2), N'roseann.meredith@swarco.com', N'Swarco Industries LLC', N'9313885900', N'Roseann Meredith', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (50, CAST(N'2017-12-31T00:00:00.0000000' AS DateTime2), N'A008447', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'Dave.vangorp@vogeltraffic.com', N'Vogel Paint & Wax CO Inc', N'7127372476', N'Dan Van Gorp', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (51, CAST(N'2021-05-31T00:00:00.0000000' AS DateTime2), N'M008225', CAST(N'2016-06-01T00:00:00.0000000' AS DateTime2), N'Gary.Duncan@hdsupply.com', N'HD Supply Waterworks', N'7195768588', N'Gary Duncan', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (52, CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2016-02-01T00:00:00.0000000' AS DateTime2), N'fmmoler@winwaterworks.com', N'Front Range Winwater', N'7196228884', N'Fran Moler', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (53, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'sw7405@sherwin.com', N'Sherwin-Williams', N'7195976556', N'Michael Adams', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (54, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Rampart Supply Inc', N'7194827335', N'Accounts Receivable', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (55, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-06-01T00:00:00.0000000' AS DateTime2), N'olin.pruitt@zep.com', N'Zep Sales & Service', N'7195101161', N'Olin Pruitt', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (56, CAST(N'2018-03-31T00:00:00.0000000' AS DateTime2), N'T008581', CAST(N'2017-04-17T00:00:00.0000000' AS DateTime2), N'lhead@crownthermo.com', N'Crown Technologies LLC', N'7065539500', N'Laura Head', N'www.crownthermo.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (57, CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'United Rentals', N'7194730000', N'Paul Senrick', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (58, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), N'mark@aawsi.com', N'All American Waste Services', N'18005604381', N'Mark Montoya', N'www.aawsi.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (59, CAST(N'2019-07-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-07-01T00:00:00.0000000' AS DateTime2), NULL, N'Mark Rite Lines Inc', N'4068699900', NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (60, CAST(N'2018-04-07T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), N'destiny.clark@ccsand.com', N'C&C Sand CO', NULL, N'Destiny Clark', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (61, CAST(N'2020-01-02T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'Northwest Sign Recycling', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (62, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'M007837', CAST(N'2017-08-10T00:00:00.0000000' AS DateTime2), NULL, N'SB PortaBowl', N'7194471414', NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (63, CAST(N'2017-12-31T00:00:00.0000000' AS DateTime2), N'T008495', CAST(N'2017-02-09T00:00:00.0000000' AS DateTime2), N'bids@kolbestriping.com', N'Kolbe Striping Inc', N'3036889516', N'Autumn Giefer', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (64, CAST(N'2018-03-31T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-04-01T00:00:00.0000000' AS DateTime2), NULL, N'MacDonald Equipment', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (65, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'Hickman Trailer', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (66, CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'D&B Precision Products', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (67, CAST(N'2019-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'EZ Liner', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (68, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'Lindsay Precast', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (69, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'3M', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (70, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'Road Side Supplies', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (71, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'AM Signal', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (72, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'Colorado Barricade', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (73, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-08-01T00:00:00.0000000' AS DateTime2), NULL, N'Craig''s Power Equipment', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (74, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'T008493', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Roadsafe', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (75, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-09-01T00:00:00.0000000' AS DateTime2), NULL, N'Dallas Lite & Barricade', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (76, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-09-01T00:00:00.0000000' AS DateTime2), NULL, N'Grainger', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (77, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'M008225', CAST(N'2017-09-01T00:00:00.0000000' AS DateTime2), NULL, N'Core & Main', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (78, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'N/A', CAST(N'2017-09-01T00:00:00.0000000' AS DateTime2), NULL, N'Crafco', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (79, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-09-01T00:00:00.0000000' AS DateTime2), NULL, N'Right Pointe', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (80, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'NA', CAST(N'2017-10-01T00:00:00.0000000' AS DateTime2), N'e.spicer@coloradoasphalt.com', N'Colorado Asphalt Services Inc', N'7202578789', N'Erin Spicer', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (81, CAST(N'2020-01-01T00:00:00.0000000' AS DateTime2), N'M008189', CAST(N'2017-01-01T00:00:00.0000000' AS DateTime2), NULL, N'Transit Mix CO', NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Vendors] OFF
SET IDENTITY_INSERT [HangFire].[AggregatedCounter] ON 

INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (1, N'stats:deleted', 3, NULL)
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (2, N'stats:succeeded', 4, NULL)
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (3, N'stats:succeeded:2017-12-20', 2, CAST(N'2018-01-20T21:51:01.260' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (6, N'stats:succeeded:2017-12-23', 1, CAST(N'2018-01-23T02:50:01.233' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (7, N'stats:succeeded:2017-12-23-02', 1, CAST(N'2017-12-24T02:50:01.233' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (8, N'stats:failed:2017-12-24', 1, CAST(N'2018-01-24T01:06:50.443' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (9, N'stats:failed:2017-12-24-01', 1, CAST(N'2017-12-25T01:06:50.443' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (10, N'stats:succeeded:2017-12-24', 1, CAST(N'2018-01-24T01:07:41.303' AS DateTime))
INSERT [HangFire].[AggregatedCounter] ([Id], [Key], [Value], [ExpireAt]) VALUES (11, N'stats:succeeded:2017-12-24-01', 1, CAST(N'2017-12-25T01:07:41.303' AS DateTime))
SET IDENTITY_INSERT [HangFire].[AggregatedCounter] OFF
SET IDENTITY_INSERT [HangFire].[Hash] ON 

INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (1, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'Job', N'{"Type":"dtso.core.ScheduledTasks, dtso.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"Daily_SpreadsheetBackup","ParameterTypes":"[]","Arguments":"[]"}', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (2, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'Cron', N'0 7  * * *', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (3, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'TimeZoneId', N'UTC', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (4, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'Queue', N'default', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (5, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'CreatedAt', N'2017-12-20T01:39:33.8832153Z', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (6, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'NextExecution', N'2017-12-24T07:00:00.0000000Z', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (7, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'Job', N'{"Type":"dtso.core.ScheduledTasks, dtso.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"Daily_CleanupStaticDocuments","ParameterTypes":"[]","Arguments":"[]"}', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (8, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'Cron', N'0 8  * * *', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (9, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'TimeZoneId', N'UTC', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (10, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'Queue', N'default', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (11, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'CreatedAt', N'2017-12-20T02:00:42.1481297Z', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (12, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'NextExecution', N'2017-12-24T08:00:00.0000000Z', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (13, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'LastExecution', N'2017-12-24T01:07:00.0430429Z', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (14, N'recurring-job:ScheduledTasks.Daily_SpreadsheetBackup', N'LastJobId', N'7', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (15, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'LastExecution', N'2017-12-24T01:07:00.1055942Z', NULL)
INSERT [HangFire].[Hash] ([Id], [Key], [Field], [Value], [ExpireAt]) VALUES (16, N'recurring-job:ScheduledTasks.Daily_CleanupStaticDocuments', N'LastJobId', N'8', NULL)
SET IDENTITY_INSERT [HangFire].[Hash] OFF
SET IDENTITY_INSERT [HangFire].[Job] ON 

INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (5, 97, N'Failed', N'{"Type":"dtso.core.ScheduledTasks, dtso.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"Daily_SpreadsheetBackup","ParameterTypes":"[]","Arguments":"[]"}', N'[]', CAST(N'2017-12-23T02:50:00.817' AS DateTime), NULL)
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (6, 55, N'Succeeded', N'{"Type":"dtso.core.ScheduledTasks, dtso.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"Daily_CleanupStaticDocuments","ParameterTypes":"[]","Arguments":"[]"}', N'[]', CAST(N'2017-12-23T02:50:01.057' AS DateTime), CAST(N'2017-12-24T02:50:01.243' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (7, 117, N'Deleted', N'{"Type":"dtso.core.ScheduledTasks, dtso.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"Daily_SpreadsheetBackup","ParameterTypes":"[]","Arguments":"[]"}', N'[]', CAST(N'2017-12-24T01:07:00.063' AS DateTime), CAST(N'2017-12-25T01:09:52.917' AS DateTime))
INSERT [HangFire].[Job] ([Id], [StateId], [StateName], [InvocationData], [Arguments], [CreatedAt], [ExpireAt]) VALUES (8, 110, N'Succeeded', N'{"Type":"dtso.core.ScheduledTasks, dtso.core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null","Method":"Daily_CleanupStaticDocuments","ParameterTypes":"[]","Arguments":"[]"}', N'[]', CAST(N'2017-12-24T01:07:00.107' AS DateTime), CAST(N'2017-12-25T01:07:41.320' AS DateTime))
SET IDENTITY_INSERT [HangFire].[Job] OFF
SET IDENTITY_INSERT [HangFire].[JobParameter] ON 

INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (15, 5, N'RecurringJobId', N'"ScheduledTasks.Daily_SpreadsheetBackup"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (16, 5, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (17, 5, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (18, 6, N'RecurringJobId', N'"ScheduledTasks.Daily_CleanupStaticDocuments"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (19, 6, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (20, 6, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (21, 5, N'RetryCount', N'10')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (22, 7, N'RecurringJobId', N'"ScheduledTasks.Daily_SpreadsheetBackup"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (23, 7, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (24, 7, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (25, 8, N'RecurringJobId', N'"ScheduledTasks.Daily_CleanupStaticDocuments"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (26, 8, N'CurrentCulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (27, 8, N'CurrentUICulture', N'"en-US"')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (28, 8, N'RetryCount', N'1')
INSERT [HangFire].[JobParameter] ([Id], [JobId], [Name], [Value]) VALUES (29, 7, N'RetryCount', N'3')
SET IDENTITY_INSERT [HangFire].[JobParameter] OFF
INSERT [HangFire].[Schema] ([Version]) VALUES (5)
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'leu-pc:17f82f26-f102-43c9-9d78-92a375f7c133', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2017-12-24T01:43:30.7439554Z"}', CAST(N'2017-12-24T01:43:30.903' AS DateTime))
INSERT [HangFire].[Server] ([Id], [Data], [LastHeartbeat]) VALUES (N'leu-pc:6f30f40f-de6f-4f59-ba6b-a501d11b685f', N'{"WorkerCount":20,"Queues":["default"],"StartedAt":"2017-12-24T01:44:16.407258Z"}', CAST(N'2017-12-24T01:44:16.557' AS DateTime))
SET IDENTITY_INSERT [HangFire].[Set] ON 

INSERT [HangFire].[Set] ([Id], [Key], [Score], [Value], [ExpireAt]) VALUES (1, N'recurring-jobs', 0, N'ScheduledTasks.Daily_SpreadsheetBackup', NULL)
INSERT [HangFire].[Set] ([Id], [Key], [Score], [Value], [ExpireAt]) VALUES (6, N'recurring-jobs', 0, N'ScheduledTasks.Daily_CleanupStaticDocuments', NULL)
SET IDENTITY_INSERT [HangFire].[Set] OFF
SET IDENTITY_INSERT [HangFire].[State] ON 

INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (51, 5, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2017-12-23T02:50:00.850' AS DateTime), N'{"EnqueuedAt":"2017-12-23T02:50:00.7980258Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (52, 5, N'Processing', NULL, CAST(N'2017-12-23T02:50:01.050' AS DateTime), N'{"StartedAt":"2017-12-23T02:50:01.0350863Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (53, 6, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2017-12-23T02:50:01.063' AS DateTime), N'{"EnqueuedAt":"2017-12-23T02:50:01.0574214Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (54, 6, N'Processing', NULL, CAST(N'2017-12-23T02:50:01.077' AS DateTime), N'{"StartedAt":"2017-12-23T02:50:01.0739264Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"ffa059b1-77e6-46f5-8526-3c9407e42d6e"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (55, 6, N'Succeeded', NULL, CAST(N'2017-12-23T02:50:01.240' AS DateTime), N'{"SucceededAt":"2017-12-23T02:50:01.2273171Z","PerformanceDuration":"65","Latency":"100"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (56, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T02:50:11.533' AS DateTime), N'{"FailedAt":"2017-12-23T02:50:11.4549832Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (57, 5, N'Scheduled', N'Retry attempt 1 of 10: Failure sending mail.', CAST(N'2017-12-23T02:50:11.537' AS DateTime), N'{"EnqueueAt":"2017-12-23T02:50:44.4686666Z","ScheduledAt":"2017-12-23T02:50:11.4686666Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (58, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T02:50:46.423' AS DateTime), N'{"EnqueuedAt":"2017-12-23T02:50:46.4204225Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (59, 5, N'Processing', NULL, CAST(N'2017-12-23T02:50:46.440' AS DateTime), N'{"StartedAt":"2017-12-23T02:50:46.4390002Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (60, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T02:50:53.680' AS DateTime), N'{"FailedAt":"2017-12-23T02:50:53.6560980Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (61, 5, N'Scheduled', N'Retry attempt 2 of 10: Failure sending mail.', CAST(N'2017-12-23T02:50:53.680' AS DateTime), N'{"EnqueueAt":"2017-12-23T02:51:09.6595620Z","ScheduledAt":"2017-12-23T02:50:53.6595620Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (62, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T02:51:16.453' AS DateTime), N'{"EnqueuedAt":"2017-12-23T02:51:16.4499958Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (63, 5, N'Processing', NULL, CAST(N'2017-12-23T02:51:16.497' AS DateTime), N'{"StartedAt":"2017-12-23T02:51:16.4942990Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (64, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T02:51:23.480' AS DateTime), N'{"FailedAt":"2017-12-23T02:51:23.4595020Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (65, 5, N'Scheduled', N'Retry attempt 3 of 10: Failure sending mail.', CAST(N'2017-12-23T02:51:23.480' AS DateTime), N'{"EnqueueAt":"2017-12-23T02:53:21.4689815Z","ScheduledAt":"2017-12-23T02:51:23.4689815Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (66, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T02:53:31.563' AS DateTime), N'{"EnqueuedAt":"2017-12-23T02:53:31.5606297Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (67, 5, N'Processing', NULL, CAST(N'2017-12-23T02:53:31.597' AS DateTime), N'{"StartedAt":"2017-12-23T02:53:31.5953910Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (68, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T02:53:38.890' AS DateTime), N'{"FailedAt":"2017-12-23T02:53:38.8518013Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (69, 5, N'Scheduled', N'Retry attempt 4 of 10: Failure sending mail.', CAST(N'2017-12-23T02:53:38.890' AS DateTime), N'{"EnqueueAt":"2017-12-23T02:56:06.8747229Z","ScheduledAt":"2017-12-23T02:53:38.8747232Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (70, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T02:56:16.670' AS DateTime), N'{"EnqueuedAt":"2017-12-23T02:56:16.6661052Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (71, 5, N'Processing', NULL, CAST(N'2017-12-23T02:56:16.713' AS DateTime), N'{"StartedAt":"2017-12-23T02:56:16.7135303Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (72, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T02:56:24.237' AS DateTime), N'{"FailedAt":"2017-12-23T02:56:24.2099523Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (73, 5, N'Scheduled', N'Retry attempt 5 of 10: Failure sending mail.', CAST(N'2017-12-23T02:56:24.237' AS DateTime), N'{"EnqueueAt":"2017-12-23T03:00:55.2175792Z","ScheduledAt":"2017-12-23T02:56:24.2175795Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (74, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T03:01:01.800' AS DateTime), N'{"EnqueuedAt":"2017-12-23T03:01:01.7984477Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (75, 5, N'Processing', NULL, CAST(N'2017-12-23T03:01:01.823' AS DateTime), N'{"StartedAt":"2017-12-23T03:01:01.8236388Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (76, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T03:01:08.980' AS DateTime), N'{"FailedAt":"2017-12-23T03:01:08.9531353Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (77, 5, N'Scheduled', N'Retry attempt 6 of 10: Failure sending mail.', CAST(N'2017-12-23T03:01:08.983' AS DateTime), N'{"EnqueueAt":"2017-12-23T03:13:18.9644605Z","ScheduledAt":"2017-12-23T03:01:08.9644608Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (78, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T03:13:32.220' AS DateTime), N'{"EnqueuedAt":"2017-12-23T03:13:32.2164048Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (79, 5, N'Processing', NULL, CAST(N'2017-12-23T03:13:32.250' AS DateTime), N'{"StartedAt":"2017-12-23T03:13:32.2491142Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (80, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T03:13:39.987' AS DateTime), N'{"FailedAt":"2017-12-23T03:13:39.9412926Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (81, 5, N'Scheduled', N'Retry attempt 7 of 10: Failure sending mail.', CAST(N'2017-12-23T03:13:39.990' AS DateTime), N'{"EnqueueAt":"2017-12-23T03:35:44.9572406Z","ScheduledAt":"2017-12-23T03:13:39.9572406Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (82, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T03:35:47.963' AS DateTime), N'{"EnqueuedAt":"2017-12-23T03:35:47.9623555Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (83, 5, N'Processing', NULL, CAST(N'2017-12-23T03:35:47.987' AS DateTime), N'{"StartedAt":"2017-12-23T03:35:47.9837755Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (84, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T03:35:55.277' AS DateTime), N'{"FailedAt":"2017-12-23T03:35:55.2321174Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (85, 5, N'Scheduled', N'Retry attempt 8 of 10: Failure sending mail.', CAST(N'2017-12-23T03:35:55.277' AS DateTime), N'{"EnqueueAt":"2017-12-23T04:16:43.2423443Z","ScheduledAt":"2017-12-23T03:35:55.2423443Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (86, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T04:16:49.093' AS DateTime), N'{"EnqueuedAt":"2017-12-23T04:16:49.0902431Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (87, 5, N'Processing', NULL, CAST(N'2017-12-23T04:16:49.120' AS DateTime), N'{"StartedAt":"2017-12-23T04:16:49.1188561Z","ServerId":"leu-pc:ee203321-62e5-44c4-b737-2a191fdb2db0","WorkerId":"d37aded9-e9f0-44da-bc63-1c70c0c5876d"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (88, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T04:16:56.407' AS DateTime), N'{"FailedAt":"2017-12-23T04:16:56.3815277Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (89, 5, N'Scheduled', N'Retry attempt 9 of 10: Failure sending mail.', CAST(N'2017-12-23T04:16:56.407' AS DateTime), N'{"EnqueueAt":"2017-12-23T05:27:51.3888029Z","ScheduledAt":"2017-12-23T04:16:56.3888032Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (90, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-23T05:27:52.703' AS DateTime), N'{"EnqueuedAt":"2017-12-23T05:27:52.6387441Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (91, 5, N'Processing', NULL, CAST(N'2017-12-23T05:27:52.723' AS DateTime), N'{"StartedAt":"2017-12-23T05:27:52.7216117Z","ServerId":"leu-pc:99ba47d1-dab1-4cdb-b183-906af2ad31e1","WorkerId":"115cc61f-0bf6-4182-8de8-6253280a5e38"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (92, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-23T05:28:01.443' AS DateTime), N'{"FailedAt":"2017-12-23T05:28:01.3736856Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 40"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (93, 5, N'Scheduled', N'Retry attempt 10 of 10: Failure sending mail.', CAST(N'2017-12-23T05:28:01.447' AS DateTime), N'{"EnqueueAt":"2017-12-23T07:20:37.3857635Z","ScheduledAt":"2017-12-23T05:28:01.3857635Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (94, 5, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-24T01:05:28.377' AS DateTime), N'{"EnqueuedAt":"2017-12-24T01:05:28.3455766Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (95, 5, N'Processing', NULL, CAST(N'2017-12-24T01:05:28.420' AS DateTime), N'{"StartedAt":"2017-12-24T01:05:28.4181679Z","ServerId":"leu-pc:d8652840-88ea-4495-bf6a-6965b87aed4a","WorkerId":"369c02fc-5589-4780-935a-6de32b612b46"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (96, 5, N'Processing', NULL, CAST(N'2017-12-24T01:06:41.040' AS DateTime), N'{"StartedAt":"2017-12-24T01:06:40.9754380Z","ServerId":"leu-pc:82e620e4-cf80-4eb7-a572-f63355606638","WorkerId":"32158a7e-8e82-4e31-b9da-721be636173a"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (97, 5, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-24T01:06:50.443' AS DateTime), N'{"FailedAt":"2017-12-24T01:06:50.3684937Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 39"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (98, 7, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2017-12-24T01:07:00.087' AS DateTime), N'{"EnqueuedAt":"2017-12-24T01:07:00.0546081Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (99, 7, N'Processing', NULL, CAST(N'2017-12-24T01:07:00.103' AS DateTime), N'{"StartedAt":"2017-12-24T01:07:00.1020730Z","ServerId":"leu-pc:82e620e4-cf80-4eb7-a572-f63355606638","WorkerId":"0506a316-a67c-4a52-a0bf-e1a32038f6ba"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (100, 8, N'Enqueued', N'Triggered by recurring job scheduler', CAST(N'2017-12-24T01:07:00.107' AS DateTime), N'{"EnqueuedAt":"2017-12-24T01:07:00.1056705Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (101, 8, N'Processing', NULL, CAST(N'2017-12-24T01:07:00.130' AS DateTime), N'{"StartedAt":"2017-12-24T01:07:00.1201432Z","ServerId":"leu-pc:82e620e4-cf80-4eb7-a572-f63355606638","WorkerId":"718421c6-d56d-4ee0-83b7-6375a1e1ddb2"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (102, 8, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-24T01:07:00.267' AS DateTime), N'{"FailedAt":"2017-12-24T01:07:00.2340563Z","ExceptionType":"System.IO.IOException","ExceptionMessage":"The process cannot access the file ''E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.api\\StaticDocuments\\Backup-20171223180641267.xlsx'' because it is being used by another process.","ExceptionDetails":"System.IO.IOException: The process cannot access the file ''E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.api\\StaticDocuments\\Backup-20171223180641267.xlsx'' because it is being used by another process.\r\n   at System.IO.Win32FileSystem.DeleteFile(String fullPath)\r\n   at System.IO.FileInfo.Delete()\r\n   at dtso.core.ScheduledTasks.Daily_CleanupStaticDocuments() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 48"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (103, 8, N'Scheduled', N'Retry attempt 1 of 10: The process cannot access the file ''E:\Programing…', CAST(N'2017-12-24T01:07:00.267' AS DateTime), N'{"EnqueueAt":"2017-12-24T01:07:30.2547367Z","ScheduledAt":"2017-12-24T01:07:00.2547367Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (104, 7, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-24T01:07:08.453' AS DateTime), N'{"FailedAt":"2017-12-24T01:07:08.4282703Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 39"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (105, 7, N'Scheduled', N'Retry attempt 1 of 10: Failure sending mail.', CAST(N'2017-12-24T01:07:08.457' AS DateTime), N'{"EnqueueAt":"2017-12-24T01:07:30.4313974Z","ScheduledAt":"2017-12-24T01:07:08.4313974Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (106, 7, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-24T01:07:41.070' AS DateTime), N'{"EnqueuedAt":"2017-12-24T01:07:41.0639762Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (107, 8, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-24T01:07:41.113' AS DateTime), N'{"EnqueuedAt":"2017-12-24T01:07:41.1128966Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (108, 7, N'Processing', NULL, CAST(N'2017-12-24T01:07:41.190' AS DateTime), N'{"StartedAt":"2017-12-24T01:07:41.1898414Z","ServerId":"leu-pc:82e620e4-cf80-4eb7-a572-f63355606638","WorkerId":"0506a316-a67c-4a52-a0bf-e1a32038f6ba"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (109, 8, N'Processing', NULL, CAST(N'2017-12-24T01:07:41.297' AS DateTime), N'{"StartedAt":"2017-12-24T01:07:41.2963645Z","ServerId":"leu-pc:82e620e4-cf80-4eb7-a572-f63355606638","WorkerId":"718421c6-d56d-4ee0-83b7-6375a1e1ddb2"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (110, 8, N'Succeeded', NULL, CAST(N'2017-12-24T01:07:41.320' AS DateTime), N'{"SucceededAt":"2017-12-24T01:07:41.3023270Z","PerformanceDuration":"1","Latency":"41193"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (111, 7, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-24T01:08:07.357' AS DateTime), N'{"FailedAt":"2017-12-24T01:08:07.2862897Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 39"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (112, 7, N'Scheduled', N'Retry attempt 2 of 10: Failure sending mail.', CAST(N'2017-12-24T01:08:07.357' AS DateTime), N'{"EnqueueAt":"2017-12-24T01:08:53.3183291Z","ScheduledAt":"2017-12-24T01:08:07.3183291Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (113, 7, N'Enqueued', N'Triggered by DelayedJobScheduler', CAST(N'2017-12-24T01:09:02.140' AS DateTime), N'{"EnqueuedAt":"2017-12-24T01:09:02.1400435Z","Queue":"default"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (114, 7, N'Processing', NULL, CAST(N'2017-12-24T01:09:02.270' AS DateTime), N'{"StartedAt":"2017-12-24T01:09:02.2667031Z","ServerId":"leu-pc:82e620e4-cf80-4eb7-a572-f63355606638","WorkerId":"0506a316-a67c-4a52-a0bf-e1a32038f6ba"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (115, 7, N'Failed', N'An exception occurred during performance of the job.', CAST(N'2017-12-24T01:09:18.577' AS DateTime), N'{"FailedAt":"2017-12-24T01:09:18.5475504Z","ExceptionType":"System.Net.Mail.SmtpException","ExceptionMessage":"Failure sending mail.","ExceptionDetails":"System.Net.Mail.SmtpException: Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException: No connection could be made because the target machine actively refused it 127.0.0.1:25\r\n   at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress)\r\n   at System.Net.Sockets.Socket.Connect(EndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP)\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n--- End of stack trace from previous location where exception was thrown ---\r\n   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()\r\n   at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port)\r\n   at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port)\r\n   at System.Net.Mail.SmtpClient.GetConnection()\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   --- End of inner exception stack trace ---\r\n   at System.Net.Mail.SmtpClient.Send(MailMessage message)\r\n   at dtso.core.ScheduledTasks.Daily_SpreadsheetBackup() in E:\\Programing\\Projects\\NET\\DtsOps\\DtsOps\\dtso.core\\ScheduledTasks.cs:line 39"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (116, 7, N'Scheduled', N'Retry attempt 3 of 10: Failure sending mail.', CAST(N'2017-12-24T01:09:18.577' AS DateTime), N'{"EnqueueAt":"2017-12-24T01:10:13.5582406Z","ScheduledAt":"2017-12-24T01:09:18.5582406Z"}')
INSERT [HangFire].[State] ([Id], [JobId], [Name], [Reason], [CreatedAt], [Data]) VALUES (117, 7, N'Deleted', N'Triggered via Dashboard UI', CAST(N'2017-12-24T01:09:52.913' AS DateTime), N'{"DeletedAt":"2017-12-24T01:09:52.9096029Z"}')
SET IDENTITY_INSERT [HangFire].[State] OFF
/****** Object:  Index [IX_Accounts_RegionalAccountCodeId_SubNo_ShredNo]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Accounts_RegionalAccountCodeId_SubNo_ShredNo] ON [dbo].[Accounts]
(
	[RegionalAccountCodeId] ASC,
	[SubNo] ASC,
	[ShredNo] ASC
)
WHERE ([SubNo] IS NOT NULL AND [ShredNo] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InvoiceAccounts_AccountId]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_InvoiceAccounts_AccountId] ON [dbo].[InvoiceAccounts]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_InvoiceTypeId]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_InvoiceTypeId] ON [dbo].[Invoices]
(
	[InvoiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Invoices_VendorId]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Invoices_VendorId] ON [dbo].[Invoices]
(
	[VendorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transfers]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_Transfers] ON [dbo].[Transfers]
(
	[TransferId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UX_HangFire_CounterAggregated_Key]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_HangFire_CounterAggregated_Key] ON [HangFire].[AggregatedCounter]
(
	[Key] ASC
)
INCLUDE ( 	[Value]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Counter_Key]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Counter_Key] ON [HangFire].[Counter]
(
	[Key] ASC
)
INCLUDE ( 	[Value]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Hash_ExpireAt]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Hash_ExpireAt] ON [HangFire].[Hash]
(
	[ExpireAt] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Hash_Key]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Hash_Key] ON [HangFire].[Hash]
(
	[Key] ASC
)
INCLUDE ( 	[ExpireAt]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UX_HangFire_Hash_Key_Field]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_HangFire_Hash_Key_Field] ON [HangFire].[Hash]
(
	[Key] ASC,
	[Field] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Job_ExpireAt]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_ExpireAt] ON [HangFire].[Job]
(
	[ExpireAt] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Job_StateName]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Job_StateName] ON [HangFire].[Job]
(
	[StateName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_JobParameter_JobIdAndName]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_JobParameter_JobIdAndName] ON [HangFire].[JobParameter]
(
	[JobId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_JobQueue_QueueAndFetchedAt]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_JobQueue_QueueAndFetchedAt] ON [HangFire].[JobQueue]
(
	[Queue] ASC,
	[FetchedAt] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_List_ExpireAt]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_List_ExpireAt] ON [HangFire].[List]
(
	[ExpireAt] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_List_Key]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_List_Key] ON [HangFire].[List]
(
	[Key] ASC
)
INCLUDE ( 	[ExpireAt],
	[Value]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_Set_ExpireAt]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_ExpireAt] ON [HangFire].[Set]
(
	[ExpireAt] ASC
)
INCLUDE ( 	[Id]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_HangFire_Set_Key]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_Set_Key] ON [HangFire].[Set]
(
	[Key] ASC
)
INCLUDE ( 	[ExpireAt],
	[Value]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UX_HangFire_Set_KeyAndValue]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UX_HangFire_Set_KeyAndValue] ON [HangFire].[Set]
(
	[Key] ASC,
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_HangFire_State_JobId]    Script Date: 12/23/2017 7:10:22 PM ******/
CREATE NONCLUSTERED INDEX [IX_HangFire_State_JobId] ON [HangFire].[State]
(
	[JobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((0.0)) FOR [AnnualBudget]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((-1)) FOR [ShredNo]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((-1)) FOR [SubNo]
GO
ALTER TABLE [dbo].[Bugs] ADD  CONSTRAINT [DF_Bugs_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO
ALTER TABLE [dbo].[CityAccounts] ADD  CONSTRAINT [DF_CityAccounts_CityAccountNumber]  DEFAULT ((0)) FOR [CityAccountNumber]
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
ALTER TABLE [HangFire].[JobParameter]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_JobParameter_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[JobParameter] CHECK CONSTRAINT [FK_HangFire_JobParameter_Job]
GO
ALTER TABLE [HangFire].[State]  WITH CHECK ADD  CONSTRAINT [FK_HangFire_State_Job] FOREIGN KEY([JobId])
REFERENCES [HangFire].[Job] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [HangFire].[State] CHECK CONSTRAINT [FK_HangFire_State_Job]
GO
/****** Object:  StoredProcedure [dbo].[Search]    Script Date: 12/23/2017 7:10:22 PM ******/
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

	declare @threshold float = 0.335

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
