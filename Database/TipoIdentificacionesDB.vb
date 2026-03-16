Imports Proyecto_Sist_Cuentas_x_Pagar.Utils

Public Class TipoIdentificacionesDB
    Private db As New DbHealper
    Public Function ConsultarTipoIdentificaciones(ByRef errorMessage) As List(Of Models.TipoIdentificaciones)
        Try
            Dim query As String = "sp_Cargar_TipoIdentificaciones_ddl"
            Dim dt As DataTable = db.ExecuteQuery(errorMessage, query, True)

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim listaTipoIdentificaciones As New List(Of Models.TipoIdentificaciones)()

                For x As Integer = 0 To dt.Rows.Count - 1
                    Dim tipoIdentificacion As New Models.TipoIdentificaciones() With {
                        .IdTipo = Convert.ToInt32(dt.Rows(x)("Valor").ToString()),
                        .Descripcion = dt.Rows(x)("Texto").ToString()
                    }
                    listaTipoIdentificaciones.Add(tipoIdentificacion)
                Next

                Return listaTipoIdentificaciones
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
