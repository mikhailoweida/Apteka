-- Служебные таблицы приложения "Аптека" (WPF). Выполнить один раз в SSMS.
-- Предполагается, что база apteka уже создана (ЛР 2-4).
USE apteka;
GO

IF OBJECT_ID(N'dbo.AppRole') IS NULL
CREATE TABLE AppRole (
    id        INT PRIMARY KEY,
    role_name NVARCHAR(30) NOT NULL UNIQUE
);
GO

IF OBJECT_ID(N'dbo.AppUser') IS NULL
CREATE TABLE AppUser (
    id            INT IDENTITY(1,1) PRIMARY KEY,
    login         NVARCHAR(30) NOT NULL UNIQUE,
    password_hash CHAR(64)     NOT NULL,            -- SHA-256 в hex
    role_id       INT          NOT NULL,
    CONSTRAINT fk_appuser_role FOREIGN KEY (role_id) REFERENCES AppRole(id)
);
GO

IF OBJECT_ID(N'dbo.ActionLog') IS NULL
CREATE TABLE ActionLog (
    id       INT IDENTITY(1,1) PRIMARY KEY,
    log_time DATETIME2     NOT NULL CONSTRAINT DF_ActionLog_time DEFAULT SYSDATETIME(),
    login    NVARCHAR(30)  NULL,
    action   NVARCHAR(300) NOT NULL
);
GO

IF NOT EXISTS (SELECT 1 FROM AppRole)
    INSERT INTO AppRole (id, role_name) VALUES (1, N'Администратор'), (2, N'Фармацевт');
GO

-- Тестовые пользователи: admin / admin123 и pharm / pharm123
IF NOT EXISTS (SELECT 1 FROM AppUser)
    INSERT INTO AppUser (login, password_hash, role_id) VALUES
    (N'admin', CONVERT(CHAR(64), HASHBYTES('SHA2_256', 'admin123'), 2), 1),
    (N'pharm', CONVERT(CHAR(64), HASHBYTES('SHA2_256', 'pharm123'), 2), 2);
GO

-- Процедура оформления заказа
CREATE OR ALTER PROCEDURE dbo.sp_CreateOrder
    @patient_id  INT,
    @medicine_id INT,
    @quantity    INT
AS
BEGIN
    INSERT INTO [Order] (patient_id) VALUES (@patient_id);
    INSERT INTO OrderComposition (order_id, medicine_id, quantity)
    VALUES (SCOPE_IDENTITY(), @medicine_id, @quantity);
END;
GO

-- v_AllMedicines создаётся в ЛР 4; если её нет, создаём запасной вариант.
IF OBJECT_ID(N'dbo.v_AllMedicines', N'V') IS NULL
EXEC('
CREATE VIEW dbo.v_AllMedicines AS
SELECT m.id, m.name, N''готовое'' AS kind, rm.type AS category,
       rm.price, rm.stock_quantity, rm.expiry_date, m.critical_norm
FROM dbo.Medicine m
JOIN dbo.ReadyMedicine rm ON rm.medicine_id = m.id
UNION ALL
SELECT m.id, m.name, N''изготовляемое'', cm.type,
       cm.price, NULL, NULL, m.critical_norm
FROM dbo.Medicine m
JOIN dbo.CompoundedMedicine cm ON cm.medicine_id = m.id;
');
GO
