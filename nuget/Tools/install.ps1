param($installPath, $toolsPath, $package, $project)

$path = [System.IO.Path]
$referencesFile = $path::Combine($path::GetDirectoryName($project.FileName), "Scripts\_references.js")

Add-Content -Encoding ASCII $referencesFile "/// <reference path=""proxyapi.intellisense.js"" />"