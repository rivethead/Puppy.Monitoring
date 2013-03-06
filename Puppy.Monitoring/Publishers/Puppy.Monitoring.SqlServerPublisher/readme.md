# SqlServer publisher #

The publisher publishes to a SQL Server instance.

## Install the SQL Server objects ##
Run the install_package.ps1 Powershell script. You can update the script as you see fit. By default the script will create:

- a database called "puppy.monitoring"
- a SQL Server alias called "puppy" point to . instance
- a SQL Server login called "puppy" with password "A55imilate hound?" (Change the password for your installation!)
- a database user called "puppy" in the created database
- a table called `ReportingEvent`

## Configuration ##
The component uses a connection string named "puppy.sqlserver".