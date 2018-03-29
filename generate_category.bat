@echo off
set /p category=Enter category (Positive, Negative, Endpoint, Create, Edit, Find, Delete): 
start /wait "nunit" ".\packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe" .\RestApiTest\bin\Debug\RestApiTest.dll --where "cat == %category%" && start "reportunit" ".\packages\ReportUnit.1.2.1\tools\ReportUnit.exe" .\TestResult.xml .\TestResult.html