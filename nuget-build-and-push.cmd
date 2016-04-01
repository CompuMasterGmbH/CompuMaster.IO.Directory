::REM -UpdateNuGetExecutable not required since it's updated by VS.NET mechanisms
PowerShell -NoProfile -ExecutionPolicy Bypass -Command "& 'CompuMaster.IO.Directory\_CreateNewNuGetPackage\DoNotModify\New-NuGetPackage.ps1' -ProjectFilePath '.\CompuMaster.IO.Directory\CompuMaster.IO.Directory.vbproj' -verbose -NoPrompt -DoNotUpdateNuSpecFile -PushPackageToNuGetGallery"
pause