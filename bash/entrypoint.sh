#!/bin/bash
# Ожидание запуска MSSQL Server
echo "Ожидание запуска MSSQL Server..."
sleep 20s

# Установка mssql-tools, если отсутствует
if [ ! -f "/opt/mssql-tools/bin/sqlcmd" ]; then
    echo "Устанавливаем mssql-tools..."
    apt-get update && apt-get install -y curl gnupg
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list
    apt-get update
    apt-get install -y mssql-tools unixodbc-dev

    #apt-get update && apt-get clean && apt-get autoclean && apt-get autoremove -y
    #apt-get install -y unixodbc unixodbc-dev
    #apt-get install -y msodbcsql17=17.10.6.1-1 mssql-tools unixodbc-dev

    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
    source ~/.bashrc
fi

# Проверяем, существует ли база данных
DB_EXISTS=$(/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF EXISTS (SELECT name FROM sys.databases WHERE name = 'Example') PRINT 'EXISTS'" -h -1)

if [ "$DB_EXISTS" == "EXISTS" ]; then
    echo "База данных уже существует. Восстановление не требуется."
else
    echo "Восстанавливаем базу данных из резервной копии..."
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -i /var/opt/mssql/init.sql
    echo "Восстановление завершено!"
fi

# Не завершать контейнер после выполнения скрипта
exec /opt/mssql/bin/sqlservr