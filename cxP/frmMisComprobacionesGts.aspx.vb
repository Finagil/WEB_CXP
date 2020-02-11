﻿Public Class frmMisComprobacionesGts
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Session.Item("Leyenda") = "Mis Comprobaciones de gastos"
        If Session.Item("Usuario") = "" Or Session.Item("Usuario") = "0" Then
            Response.Redirect("~/Login.aspx")
            Exit Sub
        End If
        If Session.Item("Empresa") = "24" Then
            GridView1.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(75, 165, 255)
        End If
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        LabelError.Visible = False
        Dim taPagos As New dsProduccionTableAdapters.CXP_PagosTableAdapter
        Dim td As New dsProduccion.CXP_PagosDataTable
        Dim taComprobacion As New dsProduccionTableAdapters.CXP_ComprobGtosTableAdapter
        Dim dtComprobacion As New dsProduccion.CXP_ComprobGtosDataTable
        If e.CommandName = "Select" Then
            HiddenID.Value = e.CommandSource.Text
            HiddenEstatus.Value = e.CommandArgument
        ElseIf InStr(HiddenEstatus.Value, "Cancelada") > 0 Then
            LabelError.Visible = True
            LabelError.Text = UCase("SOLICITUD " & HiddenID.Value & " YA FUE CANCELADA")
        ElseIf e.CommandName = "Cancelar" Then
            taPagos.ObtFolioParaCancelarGastos_FillBy(td, Session.Item("Usuario"), CInt(Session.Item("Empresa")), HiddenID.Value)
            For Each rows As dsProduccion.CXP_PagosRow In td
                taPagos.Insert(rows.idProveedor, rows.idUsuario, rows.folioSolicitud, Date.Now.ToLongDateString, rows.fechaSolicitud, rows.serie, rows.folio, rows.uuid, (rows.subtotalPagado) * -1, (rows.totalPagado) * -1, (rows.trasladosPagados) * -1, (rows.retencionesPagadas) * -1, rows.decripcion, rows.idConcepto, -1, rows.usuario, rows.idEmpresas, "Cancelacion", rows.autoriza1, rows.autoriza2, Nothing, Nothing, rows.moneda, Date.Now.ToLongDateString, rows.contrato, Nothing, Nothing, Nothing, Nothing, rows.cCostos, rows.fPago)
                taPagos.ActualizaACancelada_UpdateQuery(rows.folioSolicitud, rows.uuid)
            Next
            taComprobacion.ConsultaRegistros_FillBy(dtComprobacion, HiddenID.Value, CInt(Session.Item("Empresa")), HiddenID.Value)
            For Each rowsC As dsProduccion.CXP_ComprobGtosRow In dtComprobacion
                taComprobacion.Insert(rowsC.idProveedorUsuario, rowsC.idFolioSolicitud, rowsC.idEmpresa, rowsC.uuid, (rowsC.importe) * -1, rowsC.idConcepto, rowsC.descripcion, rowsC.destinoN, rowsC.destinoE, rowsC.motivo, rowsC.fechaLlegada, rowsC.fechaSalida, rowsC.folioComprobacion, rowsC.ok1, rowsC.ok2, rowsC.naAutoriza1, rowsC.naAutoriza2, rowsC.mail1, rowsC.mail2, Date.Now, rowsC.folio, rowsC.serie, "Cancelacion")
                taComprobacion.ActualizaACancelada_UpdateQuery(HiddenID.Value, CInt(Session.Item("Empresa")), HiddenID.Value)
            Next
            Response.Redirect("~/frmMisComprobacionesGts.aspx")
        Else
            LabelError.Visible = True
            LabelError.Text = UCase("Selecion no válida.")
        End If
    End Sub
End Class