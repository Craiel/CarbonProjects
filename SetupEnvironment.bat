@echo off

ECHO Root Directory: %~dp0
ECHO Working Directory: %cd%

call %~dp0\..\CarbonCore\Sys\SetupBaseEnvironment.bat

SET SOLUTIONFILE=%~dp0\Source\CarbonProjects.sln
SET ROOTDIR=%~dp0
