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
		:base_folder => 'Puppy.Monitoring/Publishers/Puppy.Monitoring.Contrib/',
		:files => [
			['Puppy.Monitoring.Contrib.dll', 'lib\net45'],
			['Puppy.Monitoring.Contrib.pdb', 'lib\net45']
		],
		:dependencies => [
			['Puppy.Monitoring', '']
		]
	}	
]