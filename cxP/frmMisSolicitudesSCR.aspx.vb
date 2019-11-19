Public Class frmMisSolicitudesSCR
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim taEmpresa As New dsProduccionTableAdapters.CXP_EmpresasTableAdapter
        Session.Item("idConcepto") = taEmpresa.ObtTipoConceptoReem_ScalarQuery(Session.Item("Empresa"))

        'odsMisSolicitudesSC.FilterExpression = "idConcepto ='" & taEmpresa.ObtTipoConceptoGts_ScalarQuery(Session.Item("Empresa")) & "'"

        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        End If
        For Each row As GridViewRow In GridView1.Rows
            Dim btn As Button = row.Cells(5).FindControl("btnOpciones")
            Session.Item("Leyenda") = "Mis solicitudes de reembolso"
            If row.Cells(3).Text = "Pagada" Then
                btn.Visible = False
            Else
                btn.Visible = True
            End If

            Dim taAutorizaciones As New dsProduccionTableAdapters.Vw_CXP_MisSolicitudesSCTableAdapter
            Dim dtAutorizaciones As New dsProduccion.Vw_CXP_MisSolicitudesSCDataTable
            Dim drAutorizaciones As dsProduccion.Vw_CXP_MisSolicitudesSCRow

            taAutorizaciones.ObtAuroizante_FillBy(dtAutorizaciones, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CDec(row.Cells(0).Text.Trim))
            If dtAutorizaciones.Rows.Count > 0 Then
                drAutorizaciones = dtAutorizaciones.Rows(0)
                If row.Cells(4).Text.Trim = "Autoriza 1" Then
                    row.Cells(4).Text = "Autorizó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Rechazada 1" Then
                    row.Cells(4).Text = "Rechazó (1): " & vbCrLf & drAutorizaciones.Autoriza1
                ElseIf row.Cells(4).Text.Trim = "Autoriza 2" Then
                    row.Cells(4).Text = "Autorizó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                ElseIf row.Cells(4).Text.Trim = "Rechazada 2" Then
                    row.Cells(4).Text = "Rechazó (2): " & vbCrLf & drAutorizaciones.Autoriza2
                End If
            End If
        Next
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim td As New dsProduccion.CXP_PagosDataTable
        If e.CommandName = "Cancelar" Then
            taPagos.ObtFolioParaCancelar_FillBy(td, Session.Item("Usuario"), CInt(Session.Item("Empresa")), CInt(GridView1.Rows(e.CommandArgument).Cells(0).Text))
            For Each rows As dsProduccion.CXP_PagosRow In td
                taPagos.Insert(rows.idProveedor, rows.idUsuario, rows.folioSolicitud, Date.Now.ToLongDateString, rows.fechaSolicitud, rows.serie, rows.folio, rows.uuid, (rows.subtotalPagado) * -1, (rows.totalPagado) * -1, (rows.trasladosPagados) * -1, (rows.retencionesPagadas) * -1, rows.decripcion, rows.idConcepto, -1, rows.usuario, rows.idEmpresas, "Cancelacion", rows.autoriza1, rows.autoriza2, rows.ok1, rows.ok2, rows.moneda, Date.Now.ToLongDateString, False, rows.noContrato, rows.idAutoriza2, rows.naAutoriza2, rows.naAutoriza1, rows.cCostos, rows.fPago)
                taPagos.ActualizaACancelada_UpdateQuery(rows.folioSolicitud, rows.uuid)
            Next
        End If
        Response.Redirect("~/frmMisSolicitudesSCR.aspx")
    End Sub
End Class