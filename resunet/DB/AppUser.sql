USE test; 

-- variables
DECLARE @TABLE_NAME VARCHAR(10); 
SET @TABLE_NAME = 'AppUser'; 

-- make sure you can run the script multiple times and not run into an error

-- create a table
IF (OBJECT_ID(@TABLE_NAME) IS NOT NULL)
	BEGIN
		PRINT(@TABLE_NAME + ' table already exists'); 
	END
ELSE 
	BEGIN
		CREATE TABLE AppUser (
			UserID INT IDENTITY(1, 1) PRIMARY KEY, 
			Email VARCHAR(50) NOT NULL, 
			Password VARCHAR(100), 
			Salt VARCHAR(100), 
			Status INT
		); 
	END


-- create a unique index for email
-- - make read operations faster
-- - make sure no duplicate emails can be added (no need to set UNIQUE constraint on email explicitly - it will be created when creating an index)
IF (INDEXPROPERTY(OBJECT_ID(@TABLE_NAME), 'IX_AppUserEmail', 'IndexID') IS NOT NULL) 
	BEGIN 
		PRINT('A unique index for email already exists'); 
	END
ELSE 
	BEGIN 
		CREATE UNIQUE NONCLUSTERED INDEX IX_AppUserEmail ON AppUser(
			Email
		); 
	END