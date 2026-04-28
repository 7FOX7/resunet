USE test; 

-- variables
DECLARE @TABLE_NAME VARCHAR(10); 
SET @TABLE_NAME = 'Profile'; 

-- make sure you can run the script multiple times and not run into an error

-- create a table
IF (OBJECT_ID(@TABLE_NAME) IS NOT NULL)
	BEGIN
		PRINT(@TABLE_NAME + ' table already exists'); 
	END
ELSE 
	BEGIN
		CREATE TABLE Profile (
			/* a user can have many resumes */
			ProfileID INT IDENTITY (1, 1) PRIMARY KEY, 
			/* hold a reference to logged in user */
			UserID INT, 
			/* role name */
			ProfileName VARCHAR(50), 
			FirstName VARCHAR(50), 
			LastName VARCHAR(50), 
			ProfileImage VARCHAR(150)
		); 
	END