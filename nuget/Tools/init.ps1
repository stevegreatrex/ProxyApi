 Register-ObjectEvent -inputObject $DTE.Events.BuildEvents -eventName OnBuildDone -Action { $DTE.ExecuteCommand("Build.TransformAllT4Templates")  }
