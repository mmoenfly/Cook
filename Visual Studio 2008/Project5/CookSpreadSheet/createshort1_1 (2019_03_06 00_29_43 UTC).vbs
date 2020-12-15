Option Explicit
Dim objShell, objDesktop, objLink,objFSO,objFolder,colItems,objItem,oShell
Dim strAppPath, strWorkDir, strIconPath,pos,File_Name, sRoot, sExt, sArr
dim temp, iCount

 
on error resume next

err.clear

temp = Property("CustomActionData")   ' Folder Path
if error.number <> 0 then temp = "c:\as400da\"
err.clear
Set objShell = CreateObject("WScript.Shell")
objDesktop = objShell.SpecialFolders("Desktop")
Set oShell = CreateObject( "WScript.Shell" )  
sRoot=oShell.ExpandEnvironmentStrings("%SystemRoot%")  

Set objFSO = CreateObject("Scripting.FileSystemObject")
Set objFolder = objFSO.GetFolder(temp)  

 if   objFSO.FolderExists(objDeskTop  &"\AS400 SpreadSheets") Then _
      objFSO.DeleteFolder(objDeskTop  &"\AS400 SpreadSheets" ) 

 if Not objFSO.FolderExists(objDeskTop  &"\CCI SpreadSheets") Then _
      objFSO.CreateFolder(objDeskTop  & "\CCI SpreadSheets" ) 
 
iCount = 0 
 
Set colItems = objFolder.Files

For Each objItem in colItems
     If objitem.Name <> "installlog.txt" Then
	 pos = InStrRev(objItem.Name,".",-1) 
     File_Name = Left(objItem.Name, pos - 1)
     sArr = split(objItem.Name,".")
     sExt = sArr(1)
        IF sExt = "xlsm" or sExt = "xls" Then

            
            Set objLink = objShell.CreateShortcut(objDesktop &"\CCI SpreadSheets\"  & File_Name &  ".lnk")

            strAppPath = "%SystemRoot%\notepad.exe"

            strAppPath = temp + "\" + objItem.Name
            strIconPath = temp + "\" + objItem.Name
            objLink.IconLocation = temp + "\excelcci.ico"
            objLink.TargetPath = strAppPath
            objLink.WindowStyle = 3
            objLink.WorkingDirectory = sRoot
            objLink.Save
            if err.number <> 0 Then iCount = iCount + 1
            Set objLink = Nothing
      End if 
  End If
Next
 
 
if iCount > 0 Then MsgBox("Your shortcuts did not create. Please contact support at " + "supportdesk@cookconsulting.net or call 800-425-0720")
 
