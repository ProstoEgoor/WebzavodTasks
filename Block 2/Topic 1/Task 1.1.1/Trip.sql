CREATE TABLE [dbo].[Trip]
(
	[trip_no] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[ID_comp] INT NOT NULL,
	[plane] CHAR(10) NOT NULL,
	[town_from] CHAR(25) NOT NULL,
	[town_to] CHAR(25) NOT NULL,
	[time_out] DATETIME NOT NULL,
	[time_in] DATETIME NOT NULL,

	FOREIGN KEY (ID_comp) REFERENCES Company(ID_comp) ON UPDATE CASCADE
)
