
SET IDENTITY_INSERT RegionalAccountCodes ON

INSERT INTO RegionalAccountCodes (RegionalAccountCodeId, AccountNumber, FundNumber, DeptartmentNumber, ProjectNumber, ProjectDescription, AccountPrefix)
	VALUES	(1,5221000,935,9000,3900003,'Maintenance Paving','03-30'),
			(2,5221000,935,9000,3900004,'Digout','03-30'),
			(3,5222000,935,9000,3900005,'Pothole','03-30'),
			(4,5224000,935,9000,3900011,'Concrete','03-30'),
			(5,5225000,935,9000,3900012,'Project Suport','03-30'),
			(6,5226000,935,9000,3900014,'Signs & Markings','03-30'),
			(7,5223000,935,9000,3900007,'Pipe','03-30');

SET IDENTITY_INSERT RegionalAccountCodes OFF

SET IDENTITY_INSERT Accounts ON

INSERT INTO Accounts (AccountId, RegionalAccountCodeId, SubNo,ShredNo, Description, AnnualBudget)
	VALUES	(1,1,NULL,NULL,'In House Resurfacing',2.00),
			(2,1,1,NULL,'Maitenance Paving',2000.00),
			(4,2,2,NULL,'Structural Digout',20000.00),
			(5,2,2,1,'North District Digout',3000.00),
			(6,2,2,2,'South District Digout',520.00),
			(7,2,2,3,'East District Digout',8963.00),
			(8,2,2,4,'West District Digout',876.00),
			(9,1,3,NULL,'District Maintenance Paving',100.00),
			(10,1,3,1,'North Maintenence Paving',8635.00),
			(11,1,3,2,'South Maintenance Paving',7548.00),
			(12,1,3,3,'East Maintenance Paving',9863.00),
			(13,1,3,4,'West Maintenance Paving',10025.00),
			(14,3,NULL,NULL,'Pothole Patching Repair',331447.80),
			(15,3,1,NULL,'Asphalt Repair',7888.00),
			(16,3,2,NULL,'Propane',896.25),
			(17,3,3,NULL,'Shovels/Rakes/etc',7865.50),
			(18,7,NULL,NULL,'In House Pipe',137447.80),
			(19,7,1,NULL,'Pipe Materials',896.54),
			(20,7,2,NULL,'Saws/Hand Tools/Etc',788.36),
			(21,4,NULL,NULL,'In House Concrete',3523678.92),
			(22,4,1,NULL,'Concrete Materials',5496.54),
			(23,4,2,NULL,'Tool/Supplies',7778.57),
			(24,5,NULL,NULL,'In House Project Support',158016.80),
			(25,5,1,NULL,'Maint Paving Manpower',7835.00),
			(26,5,2,NULL,'Maint Paving Rental Equip',785.00),
			(27,5,3,NULL,'Maint District Manpower',4563.50),
			(28,5,3,1,'North District Manpower',785.00),
			(29,5,3,2,'South District Manpower',7854.00),
			(30,5,3,3,'East District Manpower',455.00),
			(31,5,3,4,'West District Manpower',456.00),
			(32,5,4,NULL,'Maint District Rental Equip',7854.00),
			(33,5,4,1,'North District Rental Equip',456.00),
			(34,5,4,2,'South District Rental Equip',7863.00),
			(35,5,4,3,'East District Rental Equip',4596.00),
			(36,5,4,4,'West District Rental Equip',8754.00),
			(37,5,5,NULL,'In House Pipe Manpower',86.57),
			(38,5,6,NULL,'In House Pipe Rental Equip',9875.00),
			(39,5,7,NULL,'In House Concrete Manpower',1265.00),
			(40,5,8,NULL,'In House Concrete Equip',7868.00),
			(41,5,9,NULL,'Signs & Markings Manpower',782.00),
			(42,5,10,NULL,'Reserves',785.00),
			(43,6,NULL,NULL,'Signs & Markings',14135000.00),
			(44,6,1,NULL,'Crosswalks/School Legends',785.00),
			(45,6,2,NULL,'Long Line Contract',7863.00),
			(46,5,10,1,'District Mill/Pave Rental',125184.00),
			(47,5,10,2,'Misc.',201273.40),
			(48,5,11,NULL,'Saftey',31160.00),
			(49,6,3,NULL,'Roadway Paint',306000.00),
			(50,6,4,NULL,'Sign Mainenance',0.00);

SET IDENTITY_INSERT Accounts OFF

SET IDENTITY_INSERT InvoiceTypes ON

INSERT INTO InvoiceTypes (InvoiceTypeId, Name)
	VALUES (1,'Equipment'),(2,'Labor'),(3,'Material');

SET IDENTITY_INSERT InvoiceTypes OFF