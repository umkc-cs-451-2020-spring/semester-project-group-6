USE [commerceDB]
GO
/****** Object:  StoredProcedure [dbo].[RETRIEVE_SPECIFIC_NOTIFICATION]    Script Date: 4/26/2020 5:52:20 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[RETRIEVE_SPECIFIC_NOTIFICATION]
	@ACCOUNT_NUMBER VARCHAR(15)

AS

	BEGIN
	SELECT * FROM TRANSAC_NOTIFICATIONS WHERE @ACCOUNT_NUMBER = ACCOUNT_NUMBER;

	END