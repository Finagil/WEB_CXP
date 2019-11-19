Imports System.IO
Imports System.Web.Services.Protocols.SoapDocumentServiceAttribute
Imports Valida_SAT_WS
Imports XMLValidation
Imports System.Xml.XmlText
Imports System.Xml
Imports System.Xml.Schema

Public Class ValidaCfdi



    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Item("Leyenda") = "Validación de CFDI"
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            FileUpload1.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            FileUpload2.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            btnValidar.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        End If
    End Sub

    Public Sub FileUpload12(sender As Object, e As EventArgs) Handles FileUpload1.Load
        'lblValidacion.Visible = False
    End Sub


    Protected Sub btnValidar_Click(sender As Object, e As EventArgs) Handles btnValidar.Click

        Dim rootPathSave As String
        Dim rutasCFDI As String = ""
        Dim rutaSaveProceso As String = ""
        If Session.Item("rfcEmpresa") = "SAR951230N5A" Then
            If Directory.Exists(Server.MapPath("~\Procesados\ARFIN\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day))) Then
                rutasCFDI = "\ARFIN\Todos"
                rootPathSave = Server.MapPath("Arfin")
                rutaSaveProceso = Server.MapPath("~\Procesados\ARFIN\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day))
            Else
                rutasCFDI = "\ARFIN\Todos"
                rootPathSave = Server.MapPath("Arfin")
                'Directory.CreateDirectory(Server.MapPath("\\server-nas\Contabilidad CFDI\ARCHIVOS ADD CONTPAQi\CFDI_PROV\ARFIN\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day)))
                rutaSaveProceso = Server.MapPath("~\Procesados\ARFIN\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day))
            End If
        Else
            If Directory.Exists((Server.MapPath("~\Procesados\ARFIN\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day)))) Then
                rutasCFDI = "\FINAGIL\Todos"
                rootPathSave = Server.MapPath("Finagil")
                rutaSaveProceso = Server.MapPath("~\Procesados\FINAGIL\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day))
            Else
                rutasCFDI = "\FINAGIL\Todos"
                rootPathSave = Server.MapPath("Finagil")
                'Directory.CreateDirectory(Server.MapPath("\\server-nas\Contabilidad CFDI\ARCHIVOS ADD CONTPAQi\CFDI_PROV\FINAGIL\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day)))
                rutaSaveProceso = Server.MapPath("~\Procesados\FINAGIL\" & Date.Now.Year.ToString & "-" & MonthName(Date.Now.Month) & "-" & String.Format("{0:00}", Date.Now.Day))
            End If
        End If

            lblError.Visible = False
        'lblValidacion.Visible = False
        lblError.Text = ""
        'lblValidacion.Text = ""

        Dim resXML As readXML_CFDI_class = New readXML_CFDI_class
        Dim resXSD As validaXSD = New validaXSD

        Dim dtDetalleA As New DataTable("Validaciones")
        Dim rowA As DataRow
        dtDetalleA.Columns.Add("uuid", Type.GetType("System.String"))
        dtDetalleA.Columns.Add("sat", Type.GetType("System.String"))
        dtDetalleA.Columns.Add("xsd", Type.GetType("System.String"))

        Try
            Dim rootPath As String = Server.MapPath("files")


            For Each arch As HttpPostedFile In FileUpload1.PostedFiles
                Dim ext() As String = arch.FileName.Split(".")
                Dim GU As Guid = Guid.NewGuid
                If ext(1) <> "xml" Then
                    lblError.Visible = True
                    lblError.Text = "Solo se permiten archivos XML"
                    Exit For
                End If
                arch.SaveAs(Path.Combine(rootPath, GU.ToString & "." & ext(1)))
                Dim Nom As String = arch.FileName

                For Each archPdf As HttpPostedFile In FileUpload2.PostedFiles
                    Dim extPdf() As String = archPdf.FileName.Split(".")
                    If extPdf(1) <> "pdf" Then
                        lblError.Visible = True
                        lblError.Text = "Solo se permiten archivos PDF"
                        Exit For
                    End If


                    Dim doc As XmlDocument
                    doc = New XmlDocument
                    doc.Load(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1))
                    Dim ns As XmlNamespaceManager = New XmlNamespaceManager(doc.NameTable)
                    ns.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3")
                    Dim node As XmlNode = doc.DocumentElement.SelectSingleNode("descendant::cfdi:Addenda", ns)

                    If node IsNot Nothing Then
                        node.ParentNode.RemoveChild(node)
                        doc.Save(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1))
                    End If

                    If ext(0) = extPdf(0) Then
                        'lblValidacion.Visible = True
                        Dim var As String = "Sin errores en XSD"
                        Try
                            'var = resXML.Valida_SAT(resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "RFCE"), resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "RFCR"), resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "Total"), resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "UUID"))
                            resXSD.LoadValidatedXmlDocument(rootPath + "\" & GU.ToString + "." & ext(ext.Length - 1), rootPath + "\cfdv33.xsd", rootPath + "\TimbreFiscalDigitalv11.xsd", rootPath + "\implocal.xsd", rootPath + "\Pagos10.xsd")
                            resXSD.LoadValidatedXDocument(rootPath + "\" & GU.ToString + "." & ext(ext.Length - 1), rootPath + "\cfdv33.xsd", rootPath + "\TimbreFiscalDigitalv11.xsd", rootPath + "\implocal.xsd", rootPath + "\Pagos10.xsd")
                            resXSD.LoadXml(rootPath + "\" & GU.ToString + "." & ext(ext.Length - 1), rootPath + "\cfdv33.xsd", rootPath + "\TimbreFiscalDigitalv11.xsd", rootPath + "\implocal.xsd", rootPath + "\Pagos10.xsd")

                        Catch ex As Exception
                            'lblError.Visible = True
                            var = ex.ToString
                        End Try

                        rowA = dtDetalleA.NewRow
                        rowA("uuid") = resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "UUID")
                        rowA("sat") = resXML.Valida_SAT(resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "RFCE"), resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "RFCR"), resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "Total"), resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "UUID"))
                        If rowA("sat") = "Vigente" And var.Length < 20 Then
                            arch.SaveAs(Path.Combine(rootPathSave, resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "UUID") & "." & ext(1)))
                            archPdf.SaveAs(Path.Combine(rootPathSave, resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "UUID") & "." & extPdf(1)))

                            arch.SaveAs(Path.Combine(rutaSaveProceso, resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "UUID") & "." & ext(1)))
                            archPdf.SaveAs(Path.Combine(rutaSaveProceso, resXML.LeeXML(rootPath & "\" & GU.ToString & "." & ext(ext.Length - 1), "UUID") & "." & extPdf(1)))
                        End If
                        rowA("xsd") = var
                        dtDetalleA.Rows.Add(rowA)
                    End If

                Next
            Next
        Catch ex2 As Exception
            lblError.Visible = True
            lblError.Text = ex2.ToString + vbNewLine
        End Try

        GridView1.DataSource = dtDetalleA
        GridView1.DataBind()
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class