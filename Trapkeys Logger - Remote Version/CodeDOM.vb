Imports System.Reflection
Imports System.Resources
Imports System.IO
Imports System.CodeDom.Compiler

Public Class Generator
    Public Class CodeDom
        Private _Source, _Output, _Main, _Icon As String, _Visible, _CSharp As Boolean
        Public Property MainClass() As String
            Get
                Return _Main
            End Get
            Set(ByVal value As String)
                _Main = value
            End Set
        End Property
        Public Property VisibleForm() As Boolean
            Get
                Return _Visible
            End Get
            Set(ByVal value As Boolean)
                _Visible = value
            End Set
        End Property
        Public Property Source() As String
            Get
                Return _Source
            End Get
            Set(ByVal value As String)
                _Source = value
            End Set
        End Property
        Public Property CSharp As Boolean
            Get
                Return _CSharp
            End Get
            Set(ByVal value As Boolean)
                _CSharp = value
            End Set
        End Property
        Public Property Icon As String
            Get
                Return _Icon
            End Get
            Set(ByVal value As String)
                _Icon = value
            End Set
        End Property
        Sub New(ByVal Output As String)
            _Output = Output
        End Sub
        Sub AddResource(resourcename As String, file__1 As String)
            Dim filename As String = Path.GetFileName(file__1)
            If Not Directory.Exists("Temp") Then
                Directory.CreateDirectory("Temp")
            End If
            File.WriteAllBytes("Temp\" & filename, File.ReadAllBytes(file__1))
            File.Move("Temp\" & filename, "temp\" & resourcename)
        End Sub
        Sub Compile(ByVal ResourceName As String)
            Dim I = Path.Combine(Path.GetDirectoryName(Environment.SpecialFolder.CommonApplicationData), "K.ico")
            If _Source = Nothing Then Exit Sub
            Dim CodeProvider As Object
            If _CSharp Then
                CodeProvider = New Microsoft.CSharp.CSharpCodeProvider
            Else
                CodeProvider = New Microsoft.VisualBasic.VBCodeProvider
            End If

            If IO.Directory.Exists("Temp") AndAlso IO.Directory.GetFiles("Temp\") IsNot Nothing Then
                Using R As New ResourceWriter(ResourceName & ".resources")
                    For Each resource As String In IO.Directory.GetFiles("Temp\")
                        Dim file__1 As String = Path.GetFileName(resource)
                        If file__1 <> "thumbs.db" OrElse Not File.Exists(resource) Then
                            R.AddResource(file__1, File.ReadAllBytes(resource))
                        End If
                    Next
                    R.Close()
                End Using
            End If

            Dim Parameters As New System.CodeDom.Compiler.CompilerParameters
            With Parameters
                .GenerateExecutable = True
                .OutputAssembly = _Output
                If Not _Visible Then .CompilerOptions &= "/target:winexe"
                .CompilerOptions &= " /platform:x86"
                If Not _Icon = Nothing Then
                    IO.File.Copy(_Icon, I, True)
                    .CompilerOptions &= " /win32icon:" & I
                End If
                .MainClass = _Main
                .IncludeDebugInformation = False
                For Each ASM In AppDomain.CurrentDomain.GetAssemblies
                    .ReferencedAssemblies.AddRange(New String() {"System.dll", "System.Data.dll", "System.Windows.Forms.dll", "System.Xml.dll", "System.Management.dll", "System.Drawing.dll"})
                Next
                '.ReferencedAssemblies.Add("System.Management.dll")
                If Directory.Exists("Temp") Then
                    Parameters.EmbeddedResources.Add(ResourceName & ".resources")
                End If
            End With
            Dim Results = CodeProvider.CompileAssemblyFromSource(Parameters, _Source)
            If IO.File.Exists(I) Then IO.File.Delete(I)
            If Directory.Exists("Temp") Then
                Directory.Delete("Temp", True)
            End If
            If Results.Errors.Count > 0 Then
                For Each E As CompilerError In Results.Errors
                    MessageBox.Show(E.ErrorText & vbNewLine & "Column: " & E.Column & vbNewLine & "Line: " & E.Line, "Compilation Error!", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Next
            End If
        End Sub
    End Class
    Public Class Assembly
        Public Structure FileInformation
            Dim FileName, Description, Company, Product, Copyright, Trademarks, OriginalName As String
            Dim FileVersion As FileVersionInfo, ProductVersion As ProductVersionInfo
            Public Structure FileVersionInfo
                Dim Major, Minor, Build, Rivision As Integer, Full As String
            End Structure
            Public Structure ProductVersionInfo
                Dim Major, Minor, Build, Rivision As Integer, Full As String
            End Structure
        End Structure
        Public Shared Function GetFileVersion(ByVal File As String) As FileInformation
            Dim x As New FileInformation, F As New FileInformation.FileVersionInfo, P As New FileInformation.ProductVersionInfo
            Dim Diagnostic = System.Diagnostics.FileVersionInfo.GetVersionInfo(File) : x.OriginalName = Diagnostic.OriginalFilename
            x.FileName = Diagnostic.FileName : x.Company = Diagnostic.CompanyName : x.Description = Diagnostic.FileDescription
            x.Copyright = Diagnostic.LegalCopyright : x.Product = Diagnostic.ProductName : x.Trademarks = Diagnostic.LegalTrademarks
            F.Full = Diagnostic.FileVersion : F.Major = Diagnostic.FileMajorPart : F.Minor = Diagnostic.FileMinorPart
            F.Build = Diagnostic.FileBuildPart : F.Rivision = Diagnostic.FilePrivatePart
            P.Full = Diagnostic.ProductVersion : P.Major = Diagnostic.ProductMajorPart : P.Minor = Diagnostic.ProductMinorPart
            P.Build = Diagnostic.ProductBuildPart : P.Rivision = Diagnostic.ProductPrivatePart
            x.ProductVersion = P : x.FileVersion = F : Return x
        End Function
    End Class
End Class