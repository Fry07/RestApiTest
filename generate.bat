start /wait "nunit" "%cd%\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe" %cd%\RestApiTest\bin\Debug\RestApiTest.dll && start "reportunit" "%cd%\packages\ReportUnit.1.2.1\tools\ReportUnit.exe" %cd%\TestResult.xml %cd%\TestResult.html