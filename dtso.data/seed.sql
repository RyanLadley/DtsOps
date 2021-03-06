USE [DtsOps]
GO
SET IDENTITY_INSERT [dbo].[RegionalAccountCodes] ON 

INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (1, 5221000, N'03-30', 9000, 935, N'Maintenance Paving', N'3900003')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (2, 5221000, N'03-30', 9000, 935, N'Digout', N'3900004')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (3, 5222000, N'03-30', 9000, 935, N'Pothole', N'3900005')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (4, 5224000, N'03-30', 9000, 935, N'Concrete', N'3900011')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (5, 5225000, N'03-30', 9000, 935, N'Project Suport', N'3900012')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (6, 5226000, N'03-30', 9000, 935, N'Signs & Markings', N'3900014')
INSERT [dbo].[RegionalAccountCodes] ([RegionalAccountCodeId], [AccountNumber], [AccountPrefix], [DeptartmentNumber], [FundNumber], [ProjectDescription], [ProjectNumber]) VALUES (7, 5223000, N'03-30', 9000, 935, N'Pipe', N'3900007')
SET IDENTITY_INSERT [dbo].[RegionalAccountCodes] OFF
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (1, 2.0000, N'In House Resurfacing', 1, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (2, 2000.0000, N'Maitenance Paving', 1, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (4, 20000.0000, N'Structural Digout', 2, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (5, 3000.0000, N'North District Digout', 2, 1, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (6, 520.0000, N'South District Digout', 2, 2, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (7, 8963.0000, N'East District Digout', 2, 3, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (8, 876.0000, N'West District Digout', 2, 4, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (9, 100.0000, N'District Maintenance Paving', 1, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (10, 8635.0000, N'North Maintenence Paving', 1, 1, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (11, 7548.0000, N'South Maintenance Paving', 1, 2, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (12, 9863.0000, N'East Maintenance Paving', 1, 3, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (13, 10025.0000, N'West Maintenance Paving', 1, 4, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (14, 331447.8000, N'Pothole Patching Repair', 3, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (15, 7888.0000, N'Asphalt Repair', 3, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (16, 896.2500, N'Propane', 3, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (17, 7865.5000, N'Shovels/Rakes/etc', 3, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (18, 137447.8000, N'In House Pipe', 7, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (19, 896.5400, N'Pipe Materials', 7, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (20, 788.3600, N'Saws/Hand Tools/Etc', 7, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (21, 3523678.9200, N'In House Concrete', 4, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (22, 5496.5400, N'Concrete Materials', 4, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (23, 7778.5700, N'Tool/Supplies', 4, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (24, 158016.8000, N'In House Project Support', 5, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (25, 7835.0000, N'Maint Paving Manpower', 5, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (26, 785.0000, N'Maint Paving Rental Equip', 5, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (27, 4563.5000, N'Maint District Manpower', 5, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (28, 785.0000, N'North District Manpower', 5, 1, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (29, 7854.0000, N'South District Manpower', 5, 2, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (30, 455.0000, N'East District Manpower', 5, 3, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (31, 456.0000, N'West District Manpower', 5, 4, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (32, 7854.0000, N'Maint District Rental Equip', 5, NULL, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (33, 456.0000, N'North District Rental Equip', 5, 1, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (34, 7863.0000, N'South District Rental Equip', 5, 2, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (35, 4596.0000, N'East District Rental Equip', 5, 3, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (36, 8754.0000, N'West District Rental Equip', 5, 4, 4)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (37, 86.5700, N'In House Pipe Manpower', 5, NULL, 5)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (38, 9875.0000, N'In House Pipe Rental Equip', 5, NULL, 6)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (39, 1265.0000, N'In House Concrete Manpower', 5, NULL, 7)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (40, 7868.0000, N'In House Concrete Equip', 5, NULL, 8)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (41, 782.0000, N'Signs & Markings Manpower', 5, NULL, 9)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (42, 785.0000, N'Reserves', 5, NULL, 10)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (43, 14135000.0000, N'Signs & Markings', 6, NULL, NULL)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (44, 785.0000, N'Crosswalks/School Legends', 6, NULL, 1)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (45, 7863.0000, N'Long Line Contract', 6, NULL, 2)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (46, 125184.0000, N'District Mill/Pave Rental', 5, 1, 10)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (47, 201273.4000, N'Misc.', 5, 2, 10)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (48, 31160.0000, N'Saftey', 5, NULL, 11)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (49, 306000.0000, N'Roadway Paint', 6, NULL, 3)
INSERT [dbo].[Accounts] ([AccountId], [AnnualBudget], [Description], [RegionalAccountCodeId], [ShredNo], [SubNo]) VALUES (50, 0.0000, N'Sign Mainenance', 6, NULL, 4)
SET IDENTITY_INSERT [dbo].[Accounts] OFF
SET IDENTITY_INSERT [dbo].[Vendors] ON 

INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (1, CAST(N'2018-03-23T00:00:00.0000000' AS DateTime2), N'TS1534', CAST(N'2015-06-11T00:00:00.0000000' AS DateTime2), N'tom@grainger.com', N'Grainger', N'719-555-3924', N'Tom McGrainger', N'www.grainger.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (2, CAST(N'2018-04-19T00:00:00.0000000' AS DateTime2), N'ASGE87', CAST(N'2017-06-09T00:00:00.0000000' AS DateTime2), N'bob@Keiwit.com', N'Keiwit', N'719-205-9636', N'Bob Keiwitiest', N'www.Keiwit.com', 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (9, CAST(N'2017-10-28T00:00:00.0000000' AS DateTime2), N'DSG2', CAST(N'2017-10-18T00:00:00.0000000' AS DateTime2), NULL, N'NewMater', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (11, CAST(N'2017-11-11T00:00:00.0000000' AS DateTime2), N'ASKI56', CAST(N'2017-10-04T00:00:00.0000000' AS DateTime2), NULL, N'Known Mattie', NULL, NULL, NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (12, CAST(N'2017-11-22T00:00:00.0000000' AS DateTime2), N'SDHSG', CAST(N'2017-11-14T00:00:00.0000000' AS DateTime2), NULL, N'Wawawewa', N'746-963-9878', N'Wa', NULL, 1)
INSERT [dbo].[Vendors] ([VendorId], [ContractEnd], [ContractNumber], [ContractStart], [Email], [Name], [PhoneNumber], [PointOfContact], [Website], [Active]) VALUES (13, CAST(N'2017-11-11T00:00:00.0000000' AS DateTime2), N'ERASSG', CAST(N'2017-10-04T00:00:00.0000000' AS DateTime2), NULL, N'Lama Yars', NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Vendors] OFF
SET IDENTITY_INSERT [dbo].[InvoiceTypes] ON 

INSERT [dbo].[InvoiceTypes] ([InvoiceTypeId], [Name]) VALUES (1, N'Equipment')
INSERT [dbo].[InvoiceTypes] ([InvoiceTypeId], [Name]) VALUES (2, N'Labor')
INSERT [dbo].[InvoiceTypes] ([InvoiceTypeId], [Name]) VALUES (3, N'Material')
SET IDENTITY_INSERT [dbo].[InvoiceTypes] OFF
SET IDENTITY_INSERT [dbo].[Invoices] ON 

INSERT [dbo].[Invoices] ([InvoiceId], [Description], [InvoiceDate], [DatePaid], [InvoiceTypeId], [VendorId], [InvoiceNumber]) VALUES (12, N'Adjusting!', CAST(N'2017-11-10T00:00:00.0000000' AS DateTime2), CAST(N'2017-11-13T00:00:00.0000000' AS DateTime2), 1, 2, N'123ABCDEFG')
INSERT [dbo].[Invoices] ([InvoiceId], [Description], [InvoiceDate], [DatePaid], [InvoiceTypeId], [VendorId], [InvoiceNumber]) VALUES (19, N'This is a multi Account Invoice!!', CAST(N'2017-11-14T00:00:00.0000000' AS DateTime2), CAST(N'2017-11-15T00:00:00.0000000' AS DateTime2), 1, 1, N'SDHaRS')
INSERT [dbo].[Invoices] ([InvoiceId], [Description], [InvoiceDate], [DatePaid], [InvoiceTypeId], [VendorId], [InvoiceNumber]) VALUES (22, N'Give Me Tickets!!', CAST(N'2017-11-08T00:00:00.0000000' AS DateTime2), CAST(N'2017-11-29T00:00:00.0000000' AS DateTime2), 1, 9, N'ASH')
INSERT [dbo].[Invoices] ([InvoiceId], [Description], [InvoiceDate], [DatePaid], [InvoiceTypeId], [VendorId], [InvoiceNumber]) VALUES (23, N'MORE INVOICEA', CAST(N'2017-11-01T00:00:00.0000000' AS DateTime2), CAST(N'2017-08-10T00:00:00.0000000' AS DateTime2), 2, 11, N'ASGGDS')
INSERT [dbo].[Invoices] ([InvoiceId], [Description], [InvoiceDate], [DatePaid], [InvoiceTypeId], [VendorId], [InvoiceNumber]) VALUES (24, N'AGSDSG -LOL', CAST(N'2017-11-16T00:00:00.0000000' AS DateTime2), CAST(N'2017-12-30T00:00:00.0000000' AS DateTime2), 1, 2, N'235SDGsdsg')
SET IDENTITY_INSERT [dbo].[Invoices] OFF
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (5, N'Awesome Wood 2.0', N'Feet')
INSERT [dbo].[Materials] ([MaterialId], [Name], [Unit]) VALUES (7, N'Super Cool Glue', N'Gallon')
SET IDENTITY_INSERT [dbo].[Materials] OFF
SET IDENTITY_INSERT [dbo].[MaterialVendors] ON 

INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (2, 5, 11, 6.0800)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (4, 5, 9, 5.0200)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (5, 7, 12, 3.0200)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (6, 7, 9, 2.0000)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (7, 7, 11, 96.3600)
INSERT [dbo].[MaterialVendors] ([MaterialVendorId], [MaterialId], [VendorId], [Cost]) VALUES (8, 5, 1, 9.0300)
SET IDENTITY_INSERT [dbo].[MaterialVendors] OFF
SET IDENTITY_INSERT [dbo].[Tickets] ON 

INSERT [dbo].[Tickets] ([TicketId], [VendorId], [AccountId], [MaterialVendorId], [TicketNumber], [Date], [Quantity], [Cost], [InvoiceId]) VALUES (2, 11, 16, 4, N'36554AS', CAST(N'2017-10-11T00:00:00.0000000' AS DateTime2), 0.17, 0.8700, NULL)
INSERT [dbo].[Tickets] ([TicketId], [VendorId], [AccountId], [MaterialVendorId], [TicketNumber], [Date], [Quantity], [Cost], [InvoiceId]) VALUES (3, 11, 43, 4, N'ASf', CAST(N'2017-10-26T00:00:00.0000000' AS DateTime2), 1.0354, 5.1977, NULL)
INSERT [dbo].[Tickets] ([TicketId], [VendorId], [AccountId], [MaterialVendorId], [TicketNumber], [Date], [Quantity], [Cost], [InvoiceId]) VALUES (4, 11, 43, 4, N'ASJGSH.LE', CAST(N'2018-01-27T00:00:00.0000000' AS DateTime2), 12, 60.2400, 23)
INSERT [dbo].[Tickets] ([TicketId], [VendorId], [AccountId], [MaterialVendorId], [TicketNumber], [Date], [Quantity], [Cost], [InvoiceId]) VALUES (5, 11, 43, 4, N'4236GR', CAST(N'2017-07-12T00:00:00.0000000' AS DateTime2), 54, 271.0800, NULL)
INSERT [dbo].[Tickets] ([TicketId], [VendorId], [AccountId], [MaterialVendorId], [TicketNumber], [Date], [Quantity], [Cost], [InvoiceId]) VALUES (6, 9, 21, 4, N'SDHDH', CAST(N'2017-11-09T00:00:00.0000000' AS DateTime2), 12, 36.2400, 22)
INSERT [dbo].[Tickets] ([TicketId], [VendorId], [AccountId], [MaterialVendorId], [TicketNumber], [Date], [Quantity], [Cost], [InvoiceId]) VALUES (7, 9, 2, 4, N'ASF', CAST(N'2017-11-22T00:00:00.0000000' AS DateTime2), 21, 63.4200, NULL)
INSERT [dbo].[Tickets] ([TicketId], [VendorId], [AccountId], [MaterialVendorId], [TicketNumber], [Date], [Quantity], [Cost], [InvoiceId]) VALUES (8, 9, 2, 6, N'JR$34', CAST(N'2017-11-25T00:00:00.0000000' AS DateTime2), 23, 69.4600, NULL)
SET IDENTITY_INSERT [dbo].[Tickets] OFF
SET IDENTITY_INSERT [dbo].[Transfers] ON 

INSERT [dbo].[Transfers] ([TransferId], [FromAccountId], [ToAccountId], [Description], [DateCreated], [Amount]) VALUES (1, 15, 24, N'First Transfer', CAST(N'2017-11-12T20:51:29.257' AS DateTime), 96.3500)
SET IDENTITY_INSERT [dbo].[Transfers] OFF
SET IDENTITY_INSERT [dbo].[InvoiceAccounts] ON 

INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (10, 12, 21, 50.0000)
INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (19, 19, 5, 58.0000)
INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (20, 19, 1, 325.0000)
INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (23, 22, 14, 235.0000)
INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (25, 24, 19, 21.0000)
INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (27, 23, 20, 32.0000)
INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (28, 23, 37, 2532.0000)
INSERT [dbo].[InvoiceAccounts] ([InvoiceAccountId], [InvoiceId], [AccountId], [Expense]) VALUES (29, 22, 16, 96.0000)
SET IDENTITY_INSERT [dbo].[InvoiceAccounts] OFF
SET IDENTITY_INSERT [dbo].[CityAccounts] ON 

INSERT [dbo].[CityAccounts] ([CityAccountId], [Name]) VALUES (1, N'Cold Weather Gear')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name]) VALUES (2, N'Road Maitenance')
INSERT [dbo].[CityAccounts] ([CityAccountId], [Name]) VALUES (3, N'PMO')
SET IDENTITY_INSERT [dbo].[CityAccounts] OFF
SET IDENTITY_INSERT [dbo].[CityExpenses] ON 

INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (1, 10, 1, 54.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (10, 19, 1, 23.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (11, 19, 1, 32.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (12, 20, 2, 325.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (20, 27, 1, 32.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (21, 19, 3, 6.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (22, 23, 2, 96.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (23, 23, 1, 69.0000)
INSERT [dbo].[CityExpenses] ([CityExpenseId], [InvoiceAccountId], [CityAccountId], [Expense]) VALUES (24, 29, 3, 21.0000)
SET IDENTITY_INSERT [dbo].[CityExpenses] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Email], [FirstName], [LastName], [Password], [Salt], [Permissions]) VALUES (2, N'ladley.ryan@gmail.com', N'Ryan', N'Ladley', N'URCErr0ZaG8irKcMFCpJ94Bct2aU5p1AwHs6l9C2kzw=', 0xF51238762AAFC3E0CCBC4E7988FB5DE9, 0)
INSERT [dbo].[Users] ([UserId], [Email], [FirstName], [LastName], [Password], [Salt], [Permissions]) VALUES (3, N'user@email.com', N'User', N'Testerson', N'IWsXNg2Njkdx8vTBw/PjlPoVXFdGnVfTt34EAmJ7ZeE=', 0x32EC0EFED0D2CCCBF2F5A64556030353, 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
