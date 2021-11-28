CREATE TABLE [dbo].[Pass_in_trip]
(
	[trip_no] INT NOT NULL,
	[date] DATETIME NOT NULL,
	[ID_psg] INT NOT NULL,
	[place] CHAR(10) NOT NULL

	PRIMARY KEY(trip_no, date, ID_psg),

	FOREIGN KEY (trip_no) REFERENCES Trip(trip_no) ON UPDATE CASCADE,
	FOREIGN KEY (ID_psg) REFERENCES Passenger(ID_psg) ON UPDATE CASCADE
)
