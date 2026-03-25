-- PROCEDIMIENTO ALMACENADO QUE GUARDA UN REGISTRO CUANDO OCURRE UN ERROR EN EL SISTEMA
ALTER PROC sp_Inserta_ErrorLogger
(
@ErrorMessage NVARCHAR(MAX), 
@ErrorSeverity INT, 
@ErrorState INT, 
@ErrorLine INT,
@ErrorProcedure NVARCHAR(200)
)
AS
BEGIN
	INSERT INTO ErrorLog (ErrorMessage, ErrorSeverity, ErrorState, ErrorProcedure, ErrorLine, ErrorDateTime) 
    VALUES (@ErrorMessage, @ErrorSeverity, @ErrorState, @ErrorProcedure, @ErrorLine, GETDATE())
END