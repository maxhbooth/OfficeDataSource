﻿param(
	[String]$projectName="OfficeLocationMicroservice"
)
cls

# '[p]sake' is the same as 'psake' but $Error is not polluted
remove-module [p]sake

# find psake's path

$psakeModule = (Get-ChildItem ("temp\$ProjectName\lib\psake\psake.psm1")).FullName | Sort-Object $_ | select -last 1
 
Import-Module $psakeModule

cd "c:\temp\$ProjectName"

# you can put arguments to task in multiple lines using `
Invoke-psake -buildFile .\deploy\Database\default.ps1 `
			 -parameters @{
				 "enviornment" = "DEV"
                 "databaseServer" = "***REMOVED***"
                 "projectName" = "$projectName"
				}

Write-Host "Build exit code:" $LastExitCode

# Propagating the exit code so that builds actually fail when there is a problem
exit $LastExitCode