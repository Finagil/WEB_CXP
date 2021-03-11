Imports System.Web
Public Class utilerias
    Implements IHttpModule

    Private WithEvents _context As HttpApplication

    ''' <summary>
    '''  Deberá configurar este módulo en el archivo web.config de su
    '''  web y registrarlo en IIS para poder usarlo. Para obtener más información
    '''  consulte el vínculo siguiente: https://go.microsoft.com/?linkid=8101007
    ''' </summary>
#Region "Miembros de IHttpModule"

    Public Sub Dispose() Implements IHttpModule.Dispose

        ' Ponga aquí el código de limpieza

    End Sub

    Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
        _context = context
    End Sub

#End Region

    Public Sub OnLogRequest(ByVal source As Object, ByVal e As EventArgs) Handles _context.LogRequest

        ' Controla el evento LogRequest para proporcionar una implementación de 
        ' registro personalizado para él

    End Sub

    Public Function validaCaracteresEspeciales(ByVal linea As String)

        Dim patronNum As New Regex("[0-9]")
        Dim caractEspecial As New Regex("[^a-zA-Z0-9]")

        'If patronNum.Matches(linea).Count > 0 Then
        '    Console.WriteLine("No coincide")
        '    Return False
        '    Exit Function
        'End If

        If caractEspecial.Matches(linea).Count > 0 Then
            'Console.WriteLine("No coincide")
            Return False
            Exit Function
        End If
        Return True
    End Function

    Public Function obtNumCadena(cadena As String)
        Dim str As String = String.Empty
        For i = 0 To cadena.Trim().Length - 1
            Try
                If IsNumeric(cadena.Substring(i, 1)) Then
                    str += cadena.Substring(i, 1)
                End If
            Catch exp As Exception
                str = ""
            End Try
        Next
        Return str
    End Function
End Class
