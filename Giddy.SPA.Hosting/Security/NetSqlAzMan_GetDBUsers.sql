/* 
    NetSqlAzMan GetDBUsers TABLE Function
    ************************************************************************
    Creation Date: August, 23  2006
    Purpose: Retrieve from a DB a list of custom Users (DBUserSid, DBUserName)
    Author: Andrea Ferendeles 
    Revision: 1.0.0.0
    Updated by: Christian Jensen (chrisjensenuk@gmail.com)
    Parameters: 
	use: 
		1)     SELECT * FROM dbo.GetDBUsers(<storename>, <applicationname>, NULL, NULL)            -- to retrieve all DB Users
		2)     SELECT * FROM dbo.GetDBUsers(<storename>, <applicationname>, <customsid>, NULL)  -- to retrieve DB User with specified <customsid>
		3)     SELECT * FROM dbo.GetDBUsers(<storename>, <applicationname>, NULL, <username>)  -- to retrieve DB User with specified <username>

    Remarks: 
	- Update this Function with your CUSTOM CODE
	- Returned DBUserSid must be unique
	- Returned DBUserName must be unique
*/
ALTER FUNCTION [dbo].[netsqlazman_GetDBUsers] (@StoreName nvarchar(255), @ApplicationName nvarchar(255), @DBUserSid VARBINARY(85) = NULL, @DBUserName nvarchar(255) = NULL)  
RETURNS TABLE 
AS  
RETURN 
	SELECT TOP 100 PERCENT

	CONVERT(VARBINARY(85), p.UserId) AS DBUserSid,
	p.UserName AS DBUserName,
	p.FullName,
	NULL AS OtherFields
	 FROM webpages_Membership m
	INNER JOIN UserProfile p ON m.UserId = p.UserId
	WHERE
	(@DBUserSid IS NOT NULL AND CONVERT(VARBINARY(85), p.UserID) = @DBUserSid OR @DBUserSid  IS NULL)
			AND
			(@DBUserName IS NOT NULL AND UserName = @DBUserName OR @DBUserName IS NULL)
		ORDER BY UserName