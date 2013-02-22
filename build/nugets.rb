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
	}
]