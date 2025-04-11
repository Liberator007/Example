USE master;
GO

-- �������������� ���������� ���� ���������� � �� ����� ���������������
ALTER DATABASE Example SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

RESTORE DATABASE Example
FROM DISK = '/var/opt/mssql/backups/Example.bak'
WITH 
	MOVE 'Example' TO '/var/opt/mssql/data/Example.mdf',
	MOVE 'Example_log' TO '/var/opt/mssql/data/Example_log.ldf',
REPLACE;

-- ���������� ��������������������� �����
ALTER DATABASE Example SET MULTI_USER;
GO