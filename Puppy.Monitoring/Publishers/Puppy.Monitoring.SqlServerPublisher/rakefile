require 'albacore'

connection_string = 'data source=.;Trusted_Connection=true;Initial Catalog=Puppy.Monitoring'

task :default do
	puts "Try rake migrate to upgrade the database"
	puts "Try rake rollback to roll back to the previous version"
	puts "Try rake reset to reset the database"
end

msbuild :build do |msb|
	msb.targets = [:clean, :build]
	msb.verbosity = "minimal"
	msb.properties = { :configuration => :debug }
  	msb.solution = "../../Puppy.Monitoring.sln"
end

desc "Migrate Puppy.Monitoring database"
fluentmigrator :migrate => [:build] do |migrator|
	migrator.command = '../../packages/FluentMigrator.Tools.1.0.6.0/tools/AnyCPU/40/Migrate.exe'
	migrator.provider = 'sqlserver2008'
	migrator.target = 'bin/Debug/Puppy.Monitoring.SqlServerPublisher.dll'
	migrator.connection = connection_string
end

desc "rollback 1 version"
fluentmigrator :rollback => [:build] do |migrator|
	migrator.command = '../../packages/FluentMigrator.Tools.1.0.6.0/tools/AnyCPU/40/Migrate.exe'
	migrator.provider = 'sqlserver2008'
	migrator.target = 'bin/Debug/Puppy.Monitoring.SqlServerPublisher.dll'
	migrator.connection = connection_string
	migrator.task = 'rollback'
end

desc "rollback to version 0"
fluentmigrator :reset => [:build] do |migrator|
	migrator.command = '../../packages/FluentMigrator.Tools.1.0.6.0/tools/AnyCPU/40/Migrate.exe'
	migrator.provider = 'sqlserver2008'
	migrator.target = 'bin/Debug/Puppy.Monitoring.SqlServerPublisher.dll'
	migrator.connection = connection_string
	migrator.task = 'rollback:all'
end