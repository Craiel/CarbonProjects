@echo off

call SetupEnvironment.bat

cls
%MSBUILD% %MSBUILDARGS% "%SOLUTIONFILE%" /p:configuration=release

SET BUILD_STATUS=%ERRORLEVEL%

if %BUILD_STATUS%==0 goto end 
if not %BUILD_STATUS%==0 goto fail 

:fail 
ECHO Failed building - Release
pause 
exit /b 1 

:end
ECHO Done building - Release
pause
exit /b 0 
