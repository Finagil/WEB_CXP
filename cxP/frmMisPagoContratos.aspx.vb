Public Class frmMisPagoContratos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim taEmpresa As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        'If Session.Item("Usuario") = "lmercado" Or Session.Item("Usuario") = "maria.montes" Then
        '    Session.Item("idConcepto") = taEmpresa.ObtTipoConceptoPCts_ScalarQuery(Session.Item("Empresa"))
        'End If

        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
            GridView1.RowStyle.BackColor = System.Drawing.Color.FromArgb(213, 244, 255)
        End If
        For Each row As GridViewRow In GridView1.Rows
            Dim btn As Button = row.Cells(5).FindControl("btnOpciones")
            Session.Item("Leyenda") = "Mis solicitudes de pago de contratos"
            'If row.Cells(3).Text = "Pagada" Then
            '    btn.Visible = False
            'Else
            '    btn.Visible = True
            '    btn.Enabled = False
            'End If

            Dim taAutorizaciones As New dsProduccionTableAdapters.Vw_CXP_MisPagosContratosTableAdapter
            Dim dtAutorizaciones As New dsProduccion.Vw_CXP_MisPagosContratosDataTable
            Dim drAutorizaciones As dsProduccion.Vw_CXP_MisPagosContratosRow

            taAutorizaciones.ObtAutorizante_FillBy(dtAutorizaciones, CDec(row.Cells(0).Text.Trim))
            If dtAutorizaciones.Rows.Count > 0 Then
                drAutorizaciones = dtAutorizaciones.Rows(0)
                If row.Cells(5).Text.Trim = "Autoriza 1" Then
                    row.Cells(5).Text = "Autorizó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(5).Text.Trim = "Rechazada 1" Then
                    row.Cells(5).Text = "Rechazó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(5).Text.Trim = "Autoriza 2" Then
                    row.Cells(5).Text = "Autorizó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                ElseIf row.Cells(5).Text.Trim = "Rechazada 2" Then
                    row.Cells(5).Text = "Rechazó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                End If
            End If
        Next
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub
End Class