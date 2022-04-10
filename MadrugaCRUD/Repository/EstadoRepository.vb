Imports Dapper
Imports MadrugaCRUD.MadrugaCRUD.Repository

Public Class EstadoRepository

    Public Shared Function BuscarTodosEstados() As List(Of Estado)
        Try
            Dim rep = New SQLRepository(Of Estado)("dbo.spListarEstado")
            Dim parametros = New Dictionary(Of String, Object)()
            Return rep.Exec(parametros).ToList()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function BuscarEstadoPorId(ByVal id As String) As Estado
        Try
            Dim rep = New SQLRepository(Of Estado)("dbo.spListarEstado")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Id", id)
            Return rep.Exec(parametros).ToList().FirstOrDefault()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub IncluirEstado(ByVal obj As Estado)
        Try
            Dim rep = New SQLRepository(Of Estado)("dbo.spIncluirEstado")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Nome", obj.Nome)
            parametros.Add("UF", obj.UF.ToUpper())
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Shared Sub AtualizarEstado(ByVal obj As Estado)
        Try
            Dim rep = New SQLRepository(Of Estado)("dbo.spAtualizarEstado")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Nome", obj.Nome)
            parametros.Add("UF", obj.UF.ToUpper())
            parametros.Add("Id", obj.Id)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub RemoverEstadoPorId(ByVal id As String)
        Try
            Dim rep = New SQLRepository(Of Estado)("dbo.spRemoverEstado")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Id", id)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
