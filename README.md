# CompuMaster.IO.Directory

Provides missing features for filtering files by their full name (instead of default windows behaviour with compatibility for short file names with 8.3-limitations)

[![NuGet CompuMaster.IO.Directory](https://img.shields.io/nuget/v/CompuMaster.IO.Directory.svg?label=NuGet%20CM.IO.Directory)](https://www.nuget.org/packages/CompuMaster.IO.Directory/) [![Travis](https://img.shields.io/travis/CompuMasterGmbH/CompuMaster.IO.Directory.svg?label=Build%20with%20Mono)](https://travis-ci.org/CompuMasterGmbH/CompuMaster.IO.Directory/)

## Some background on directory listings with Microsoft Windows
By default, Microsoft Windows (so the .NET framework, too) find files which match to your search pattern with their full names as well as their short names.
These search filters the files again, so the result is that you'll receive only those results which are valid with their full name.

## Sample
Given are the files abc.doc and abc.docx. Searching for *.doc with default windows/.NET behaviour will find both files because the file abc.docx is represented in 8.3-style with abc~1.doc. Using the methods in this class will reduce this result to the correct result with only abc.doc.
    
