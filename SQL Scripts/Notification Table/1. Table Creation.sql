CREATE TABLE TRANSAC_NOTIFICATIONS (
	ID INT IDENTITY(1,1),
	ACCOUNT_NUMBER VARCHAR(15) NOT NULL,
	TRIGGER_MESSAGE VARCHAR(100) NOT NULL

	PRIMARY KEY (ID)
);