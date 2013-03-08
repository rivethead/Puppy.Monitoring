@nugets = [
	{
		:package_id => 'Puppy.Monitoring',
		:description => 'Puppy.Monitoring',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/Core/Puppy.Monitoring/',
		:files => [
			['Puppy.Monitoring.dll', 'lib\net45'],
			['Puppy.Monitoring.pdb', 'lib\net45'],
		],
		:dependencies => [
			['Common.Logging', '[2.1.1]']
		]
	},
	{
		:package_id => 'Puppy.Monitoring.SqlServerPublisher',
		:description => 'Puppy.Monitoring.SqlServerPublisher',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/Publishers/Puppy.Monitoring.SqlServerPublisher/',
		:files => [
			['Puppy.Monitoring.SqlServerPublisher.dll', 'lib\net45'],
			['Puppy.Monitoring.SqlServerPublisher.pdb', 'lib\net45'],
			['install_publisher.ps1', 'tools'],
			['migrations/tools/**/*.*', 'tools']
		],
		:dependencies => [
			['Common.Logging', '[2.1.1]'],
			['Puppy.Monitoring', '']
		]
	},
	{
		:package_id => 'Puppy.Monitoring.Contrib',
		:description => 'Puppy.Monitoring.Contrib',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/Core/Puppy.Monitoring.Contrib/',
		:files => [
			['Puppy.Monitoring.Contrib.dll', 'lib\net45'],
			['Puppy.Monitoring.Contrib.pdb', 'lib\net45']
		],
		:dependencies => [
			['Puppy.Monitoring', '']
		]
	},
	{
		:package_id => 'Puppy.Monitoring.Daemon',
		:description => 'Puppy.Monitoring.Daemon',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/Daemon/Puppy.Monitoring.Daemon/',
		:files => [
			['Puppy.Monitoring.Daemon.exe', 'lib\net45'],
			['*.config', 'lib\net45'],
			['imps.xml', 'lib\net45'],
			['*.boo', 'lib\net45']
		],
		:dependencies => [
			['Puppy.Monitoring', ''],
			['Boo', '0.9.4'],
			['Boo-Compiler', '0.9.4'],
			['Common.Logging', '2.1.2'],
			['Quartz', '2.1.2'],
			['RhinoDSL', '1.0.0'],
			['TopShelf', '3.1.0'],
		]
	},
	{
		:package_id => 'Puppy.Monitoring.Imps',
		:description => 'Puppy.Monitoring.Imps',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/Daemon/Puppy.Monitoring.Imps/',
		:files => [
			['Puppy.Monitoring.Imps.dll', 'lib\net45'],
			['Puppy.Monitoring.Imps.pdb', 'lib\net45']
		],
		:dependencies => [
			['Common.Logging', '2.1.2'],
			['Quartz', '2.1.2']
		]
	},
	{
		:package_id => 'Puppy.Monitoring.SqlServer.Imps',
		:description => 'Puppy.Monitoring.SqlServer.Imps',
		:authors => 'rivethead_',
		:base_folder => 'Puppy.Monitoring/Daemon/Puppy.Monitoring.SqlServer.Imps/',
		:files => [
			['Puppy.Monitoring.SqlServer.Imps.dll', 'lib\net45'],
			['Puppy.Monitoring.SqlServer.Imps.pdb', 'lib\net45']
		],
		:dependencies => [
			['Common.Logging', '2.1.2'],
			['Quartz', '2.1.2']
		]
	}	
]