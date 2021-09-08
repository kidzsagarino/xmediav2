ALTER PROCEDURE [customer].[spGetIdOfNewlyInsertedCustomer]
@MasterID int Output,
@FirstName nvarchar (50),
@LastName nvarchar(50)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		 INSERT INTO customer.MasterData (FirstName,LastName)
		 VALUES (@FirstName,@LastName)
		 SELECT @MasterID = SCOPE_IDENTITY()
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT
		ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END


[customer].[spInsertNewCustomerAccount] 'nonoymarinas@gmail.com','nonoypassword','Mary Grace','Marinas','MCBI','0322723456','09173010779','~/images'

ALTER PROCEDURE [customer].[spInsertNewCustomerAccount]
@EmailAdress nvarchar(320),
@Istillloveyou char(60),
@FirstName nvarchar (50),
@LastName nvarchar(50),
@CompanyName nvarchar(250),
@LandlineNo varchar (15),
@MobileNo varchar(15),
@PhotoImageUrl nvarchar (max)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		/* Get the MasterID of newly inserted Customer Account */
		 DECLARE @MasterID int
		 EXECUTE customer.spGetIdOfNewlyInsertedCustomer @MasterID Output, @FirstName,@LastName
		
		/* Insert other Customer info using MasterId */
		 INSERT INTO customer.EmailAddress(MasterID,EmailAddress,DateCreated)
		 VALUES (@MasterID,@EmailAdress,GETDATE())

		 INSERT INTO customer.IStillLoveYou(MasterID,IstillLoveYou,DateCreated)
		 VALUES (@MasterID,@Istillloveyou,GETDATE())

		 INSERT INTO customer.CompanyName(MasterID,CompanyName)
		 VALUES (@MasterID,@CompanyName)

		 INSERT INTO customer.LandlineNo(MasterID,LandlineNo,DateCreated)
		 VALUES (@MasterID,@LandlineNo,GETDATE())

		 INSERT INTO customer.MobileNo(MasterID,MobileNo,DateCreated)
		 VALUES (@MasterID,@MobileNo,GETDATE())

		 IF (@PhotoImageUrl is not null)
			 BEGIN
				INSERT INTO customer.PhotoImagesUrl(MasterID,PhotoImageUrl,DateCreated)
				VALUES (@MasterID,@PhotoImageUrl,GETDATE())
			 END
		 
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
		SELECT
		ERROR_MESSAGE() AS ErrorMessage
	END CATCH
END

SELECT * FROM customer.MasterData
SELECT * FROM customer.EmailAddress
SELECT * FROM customer.IStillLoveYou
SELECT * FROM customer.LandlineNo
SELECT * FROM customer.MobileNo
SELECT * FROM customer.PhotoImagesUrl