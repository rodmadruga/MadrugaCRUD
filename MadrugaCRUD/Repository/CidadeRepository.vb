Imports MadrugaCRUD.MadrugaCRUD.Repository

Public Class CidadeRepository

    Public Shared Function BuscarTodasCidades() As List(Of Cidade)
        Try
            Dim rep = New SQLRepository(Of Cidade)("dbo.spListarCidade")
            Dim parametros = New Dictionary(Of String, Object)()
            Dim lista = rep.Exec(parametros).ToList()
            For Each item As Cidade In lista
                item.Estado = EstadoRepository.BuscarEstadoPorId(item.EstadoId)
            Next
            Return lista
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function BuscarCidadePorId(ByVal id As String) As Cidade
        Try
            Dim rep = New SQLRepository(Of Cidade)("dbo.spListarCidade")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Id", id)
            Dim lista = rep.Exec(parametros).ToList()
            For Each item As Cidade In lista
                item.Estado = EstadoRepository.BuscarEstadoPorId(item.EstadoId)
            Next
            Return lista.FirstOrDefault()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function BuscarCidadePorEstadoId(ByVal EstadoId As String) As List(Of Cidade)
        Try
            Dim rep = New SQLRepository(Of Cidade)("dbo.spListarCidade")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("EstadoId", EstadoId)
            Dim lista = rep.Exec(parametros).ToList()
            For Each item As Cidade In lista
                item.Estado = EstadoRepository.BuscarEstadoPorId(item.EstadoId)
            Next
            Return lista
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub IncluirCidade(ByVal obj As Cidade)
        Try
            Dim rep = New SQLRepository(Of Cidade)("dbo.spIncluirCidade")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Nome", obj.Nome)
            parametros.Add("EstadoId", obj.EstadoId)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Shared Sub AtualizarCidade(ByVal obj As Cidade)
        Try
            Dim rep = New SQLRepository(Of Cidade)("dbo.spAtualizarCidade")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Nome", obj.Nome)
            parametros.Add("EstadoId", obj.EstadoId)
            parametros.Add("Id", obj.Id)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub RemoverCidadePorId(ByVal id As String)
        Try
            Dim rep = New SQLRepository(Of Cidade)("dbo.spRemoverCidade")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Id", id)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
