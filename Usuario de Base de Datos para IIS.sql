CREATE LOGIN [IIS APPPOOL\pokemon-api] FROM WINDOWS;
CREATE USER [IIS APPPOOL\pokemon-api] FOR LOGIN [IIS APPPOOL\pokemon-api];
-- Adicional a este paso agregar la membresia de 'db_owner'
