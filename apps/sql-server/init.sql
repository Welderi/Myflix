USE master;
IF DB_ID('Myflix') IS NULL
BEGIN
    RESTORE DATABASE Myflix
    FROM DISK = '/var/backup/Myflix.bak'
    WITH 
        MOVE 'Myflix' TO '/var/opt/mssql/data/Myflix.mdf',
        MOVE 'Myflix_log' TO '/var/opt/mssql/data/Myflix_log.ldf',
        REPLACE;
END