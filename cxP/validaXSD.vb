Imports System.Xml
Imports System.Xml.Schema
Imports System.IO
Imports System.Text
Imports Valida_SAT_WS
Imports System.Collections
Imports System.Data
Imports System.Xml.XmlText
Imports System.Xml.XmlDocument
Imports System.Security.Cryptography

Public Class validaXSD
    Private _errors As New List(Of ValidationEventArgs)()

    Public Sub ValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
        If args.Severity = XmlSeverityType.Error Then
            _errors.Add(args)
        End If
    End Sub

    Public Function GetErrors() As String
        If _errors.Count <> 0 Then
            Dim builder As New StringBuilder()
            builder.Append("The following ")
            builder.Append(_errors.Count.ToString())
            builder.AppendLine(" error(s) were found while validating the XML document against the XSD:")
            For Each i As ValidationEventArgs In _errors
                builder.Append("* ")
                builder.AppendLine(i.Message)
            Next
            Return builder.ToString()
        Else
            Return Nothing
        End If
    End Function

    Public Function LoadValidatedXmlDocument(xmlFilePath As String, xsdFilePath As String, xsdFilePathT As String, xsdFilePathIL As String, xsdPagoTerceros As String, xsdRegistroFiscal As String) As XmlDocument
        Dim doc As New XmlDocument()
        doc.Load(xmlFilePath)
        doc.Schemas.Add(Nothing, xsdFilePath)
        doc.Schemas.Add(Nothing, xsdFilePathT)
        doc.Schemas.Add(Nothing, xsdFilePathIL)
        'doc.Schemas.Add(Nothing, xsdPagos)
        doc.Schemas.Add(Nothing, xsdPagoTerceros)
        doc.Schemas.Add(Nothing, xsdRegistroFiscal)
        Dim errorBuilder As New validaXSD()
        doc.Validate(New ValidationEventHandler(AddressOf errorBuilder.ValidationEventHandler))
        Dim errorsText As String = errorBuilder.GetErrors()
        If errorsText IsNot Nothing Then
            Throw New Exception(errorsText)
        End If
        Return doc

    End Function

    Public Sub LoadXml(xmlFilePath As String, xsdFilePath As String, xsdFilePathT As String, xsdFilePathIL As String, xsdPagoTerceros As String, xsdRegistroFiscal As String)
        Dim settings As New XmlReaderSettings()
        settings.Schemas.Add(Nothing, xsdFilePath)
        settings.Schemas.Add(Nothing, xsdFilePathT)
        settings.Schemas.Add(Nothing, xsdFilePathIL)
        'settings.Schemas.Add(Nothing, xsdPagos)
        settings.Schemas.Add(Nothing, xsdPagoTerceros)
        settings.Schemas.Add(Nothing, xsdRegistroFiscal)
        settings.ValidationType = ValidationType.Schema
        Dim errorBuilder As New validaXSD()
        AddHandler settings.ValidationEventHandler, New ValidationEventHandler(AddressOf errorBuilder.ValidationEventHandler)
        Dim reader As XmlReader = XmlReader.Create(xmlFilePath, settings)
        ' Read the document...
        Dim errorsText As String = errorBuilder.GetErrors()
        If errorsText IsNot Nothing Then
            ' Handle the errors
        End If
        reader.Close()
    End Sub

    Public Function LoadValidatedXDocument(xmlFilePath As String, xsdFilePath As String, xsdFilePathT As String, xsdFilePathIL As String, xsdPagoTerceros As String, xsdRegistroFiscal As String) As XDocument
        Dim doc As XDocument = XDocument.Load(xmlFilePath)
        Dim schemas As New XmlSchemaSet()
        schemas.Add(Nothing, xsdFilePath)
        schemas.Add(Nothing, xsdFilePathT)
        schemas.Add(Nothing, xsdFilePathIL)
        'schemas.Add(Nothing, xsdPagos)
        schemas.Add(Nothing, xsdPagoTerceros)
        schemas.Add(Nothing, xsdRegistroFiscal)
        Dim errorBuilder As New validaXSD()
        doc.Validate(schemas, New ValidationEventHandler(AddressOf errorBuilder.ValidationEventHandler))
        Dim errorsText As String = errorBuilder.GetErrors()
        If errorsText IsNot Nothing Then
            Throw New Exception(errorsText)
        End If
        Return doc
    End Function




End Class

