# CHANGE THE VARIABLES AS YOU SEE FIT"

$server = "."
$dbname = "puppy.monitoring";
$user = "puppy";
$connectionString = "Data Source=puppy;Integrated Security=False;Initial Catalog=$dbname;UID=$user;PWD=A55imilate hound?"

# STOP HERE

$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath
$migrator = "$dir/Migrations/tools/Migrate.exe"
$assembly = "Puppy.Monitoring.SqlServerPublisher.dll"

if (-not(Test-Path($migrator))) {
	$migrator = "$dir/bin/debug/Migrations/tools/Migrate.exe"
	Write-Host $migrator
}

if(-not(Test-Path($assembly))) {
	$assembly = "$dir/bin/debug/$assembly"
}


# Give PowerShell even more power...
Set-ExecutionPolicy "Unrestricted" -Scope Process -Confirm:$false
Set-ExecutionPolicy "Unrestricted" -Scope CurrentUser -Confirm:$false


# ---------------------------------------- SQL SERVER ALIASES ----------------------------------------------
$x86 = "HKLM:\Software\Microsoft\MSSQLServer\Client\ConnectTo"
$x64 = "HKLM:\Software\Wow6432Node\Microsoft\MSSQLServer\Client\ConnectTo"

if ((Test-Path -Path $x86) -ne $True) { New-Item $x86 | Out-Null }
if ((Test-Path -Path $x64) -ne $True) { New-Item $x64 | Out-Null }

# Creating TCP/IP Aliases
$TCPAlias = "DBMSSOCN," + $server + ",1433"
Write-Host "Creating alias" $TCPAlias
Set-ItemProperty -Path $x86 -Name "puppy" -Type String -Value $TCPAlias | Out-Null
Set-ItemProperty -Path $x64 -Name "puppy" -Type String -Value $TCPAlias | Out-Null

# ---------------------------------------- CREATE DATABASE ----------------------------------------------
# Open ADO.NET Connection with Windows authentification to local SQL Server.
$con = New-Object Data.SqlClient.SqlConnection;

$con.ConnectionString = "Data Source=" + $server + ";Initial Catalog=master;Integrated Security=True;";
$con.Open();

Write-Host "Connected to: " $con.ConnectionString

$sql = "
	USE [master]

	declare @dbName nvarchar(max)
	set	@dbName = '$dbname'

	if exists(select * from master.sys.databases where name = @dbName)
		return

	if not exists(select * from sys.server_principals where name = N'$user')
		CREATE LOGIN $user WITH PASSWORD=N'A55imilate hound?', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

	declare @defaultPath nvarchar(max)
	declare @sql nvarchar(max)

	select	@defaultPath = (SELECT SUBSTRING(physical_name, 1, CHARINDEX(N'master.mdf', LOWER(physical_name)) - 1)
							FROM master.sys.master_files
							WHERE database_id = 1 AND file_id = 1)

	set @sql = 'CREATE DATABASE [' + @dbName + '] ON  PRIMARY
	( NAME = ''' + @dbName + ''', FILENAME = ''' + @defaultPath + @dbName + '.mdf' + ''', SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
	 LOG ON
	( NAME = ''' + @dbName + '_log'', FILENAME = ''' + @defaultPath + @dbName + '.ldf' + ''', SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)'

	exec sp_executesql @sql

	set @sql = 'ALTER DATABASE [' + @dbName + '] SET COMPATIBILITY_LEVEL = 100'
	exec sp_executesql @sql

	if (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
	begin
		set @sql = 'EXEC [' + @dbName + '].[dbo].[sp_fulltext_database] @action = ''enable'' '
		exec sp_executesql @sql
	end

	set @sql = 'ALTER DATABASE [' + @dbName + '] SET ANSI_NULL_DEFAULT OFF'
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET ANSI_NULLS OFF'
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET ANSI_PADDING OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET ANSI_WARNINGS OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET ARITHABORT OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET AUTO_CLOSE OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET AUTO_CREATE_STATISTICS ON '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET AUTO_SHRINK OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET AUTO_UPDATE_STATISTICS ON '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET CURSOR_CLOSE_ON_COMMIT OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET CURSOR_DEFAULT  GLOBAL '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET CONCAT_NULL_YIELDS_NULL OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET NUMERIC_ROUNDABORT OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET QUOTED_IDENTIFIER OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET RECURSIVE_TRIGGERS OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET  DISABLE_BROKER '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET AUTO_UPDATE_STATISTICS_ASYNC OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET DATE_CORRELATION_OPTIMIZATION OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET TRUSTWORTHY OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET ALLOW_SNAPSHOT_ISOLATION OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET PARAMETERIZATION SIMPLE '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET READ_COMMITTED_SNAPSHOT OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET HONOR_BROKER_PRIORITY OFF '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET  READ_WRITE '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET RECOVERY FULL'
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET  MULTI_USER '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET PAGE_VERIFY CHECKSUM  '
	exec sp_executesql @sql
	set @sql = 'ALTER DATABASE [' + @dbName + '] SET DB_CHAINING OFF '
	exec sp_executesql @sql

	IF  EXISTS (SELECT * FROM sys.server_principals WHERE name = N'NT AUTHORITY\NETWORK SERVICE')
		DROP LOGIN [NT AUTHORITY\NETWORK SERVICE]

	CREATE LOGIN [NT AUTHORITY\NETWORK SERVICE] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]

	ALTER LOGIN [$user] WITH DEFAULT_DATABASE=[$dbname], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

	";

$cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
$cmd.ExecuteNonQuery() | Out-Null

$sql = "
	USE [$dbname]

	IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'NT AUTHORITY\NETWORK SERVICE')
	DROP USER [NT AUTHORITY\NETWORK SERVICE]

	CREATE USER [NT AUTHORITY\NETWORK SERVICE] FOR LOGIN [NT AUTHORITY\NETWORK SERVICE] WITH DEFAULT_SCHEMA=[dbo]

	EXEC sp_addrolemember N'db_owner', N'NT AUTHORITY\NETWORK SERVICE'

	IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'$user')
	DROP USER [$user]

	CREATE USER [$user] FOR LOGIN [$user] WITH DEFAULT_SCHEMA=[dbo]

	EXEC sp_addrolemember N'db_owner', N'$user'
	";

$cmd = New-Object Data.SqlClient.SqlCommand $sql, $con;
$cmd.ExecuteNonQuery() | Out-Null


# Close & Clear all objects.
$cmd.Dispose();
$con.Close();
$con.Dispose();


# ---------------------------------------- FLUENT MIGRATIONS ----------------------------------------------
Write-Host "Applying database updates..."

$command = $migrator + " -c '" + $connectionString + "' -db sqlserver2008 -a $assembly"

Write-Host $command

invoke-expression $command | Out-Null

