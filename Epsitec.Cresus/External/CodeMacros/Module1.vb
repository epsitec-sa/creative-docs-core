Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports System.Diagnostics

Public Module Module1

    Sub ToggleCSharpNavigationBar()
        Dim objShowNavigationBar As EnvDTE.Property
        objShowNavigationBar = DTE.Properties("TextEditor", "CSharp").Item("ShowNavigationBar")
        objShowNavigationBar.Value = Not objShowNavigationBar.Value
    End Sub

    Sub Export()
        DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
        DTE.ActiveWindow.Object.GetItem("Epsitec.Cresus\X.Export").Select(vsUISelectionType.vsUISelectionTypeSelect)
        DTE.ExecuteCommand("ClassViewContextMenus.ClassViewProject.Build")
    End Sub

    Sub ExecuteResGenerator()
        DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
        DTE.ActiveWindow.Object.GetItem("Epsitec.Cresus\X.Tool.ResGenerator").Select(vsUISelectionType.vsUISelectionTypeSelect)
        DTE.ExecuteCommand("Project.SetasStartUpProject")
        DTE.ExecuteCommand("Debug.StartWithoutDebugging")
    End Sub

    Sub ExecuteCrDoc()
        DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
        DTE.ActiveWindow.Object.GetItem("Epsitec.Cresus\App.CresusDocuments").Select(vsUISelectionType.vsUISelectionTypeSelect)
        DTE.ExecuteCommand("Project.SetasStartUpProject")
        DTE.ExecuteCommand("Debug.StartWithoutDebugging")
    End Sub

    Sub ExecutePictogram()
        DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
        DTE.ActiveWindow.Object.GetItem("Epsitec.Cresus\App.CresusPictogrammes").Select(vsUISelectionType.vsUISelectionTypeSelect)
        DTE.ExecuteCommand("Project.SetasStartUpProject")
        DTE.ExecuteCommand("Debug.StartWithoutDebugging")
    End Sub

    Sub ExecuteDesigner()
        DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
        DTE.ActiveWindow.Object.GetItem("Epsitec.Cresus\App.Designer").Select(vsUISelectionType.vsUISelectionTypeSelect)
        DTE.ExecuteCommand("Project.SetasStartUpProject")
        DTE.ExecuteCommand("Debug.StartWithoutDebugging")
    End Sub

    Sub ExecuteCresusDatabaseTests()
        DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
        DTE.ActiveWindow.Object.GetItem("Epsitec.Cresus\Cresus.Database.Tests").Select(vsUISelectionType.vsUISelectionTypeSelect)
        DTE.ExecuteCommand("Project.SetasStartUpProject")
        DTE.ExecuteCommand("Debug.StartWithoutDebugging")
    End Sub

    Sub ExecuteCresusDataLayerTests()
        DTE.Windows.Item(Constants.vsWindowKindSolutionExplorer).Activate()
        DTE.ActiveWindow.Object.GetItem("Epsitec.Cresus\Cresus.DataLayer.Tests").Select(vsUISelectionType.vsUISelectionTypeSelect)
        DTE.ExecuteCommand("Project.SetasStartUpProject")
        DTE.ExecuteCommand("Debug.StartWithoutDebugging")
    End Sub


End Module

