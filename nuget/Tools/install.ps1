param($installPath, $toolsPath, $package, $project)

$path = [System.IO.Path]
$referencesFile = $path::Combine($path::GetDirectoryName($project.FileName), "Scripts\_references.js")

Add-Content $referencesFile "`r`n/// <reference path=""proxyapi.intellisense.js"" />"