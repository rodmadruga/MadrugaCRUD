Imports MadrugaCRUD.MadrugaCRUD.Repository

Public Class ClienteRepository

    Public Shared Function BuscarTodosClientes() As List(Of Cliente)
        Try
            Dim rep = New SQLRepository(Of Cliente)("dbo.spListarCliente")
            Dim parametros = New Dictionary(Of String, Object)()
            Dim lista = rep.Exec(parametros).ToList()
            For Each item As Cliente In lista
                item.Cidade = CidadeRepository.BuscarCidadePorId(item.CidadeId)
            Next
            Return lista
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function BuscarClientePorId(ByVal id As String) As Cliente
        Try
            Dim rep = New SQLRepository(Of Cliente)("dbo.spListarCliente")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Id", id)
            Dim lista = rep.Exec(parametros).ToList()
            For Each item As Cliente In lista
                item.Cidade = CidadeRepository.BuscarCidadePorId(item.CidadeId)
            Next
            Return lista.FirstOrDefault()
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function BuscarClientesPorCidadeId(ByVal CidadeId As String) As List(Of Cliente)
        Try
            Dim rep = New SQLRepository(Of Cliente)("dbo.spListarCliente")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("CidadeId", CidadeId)
            Dim lista = rep.Exec(parametros).ToList()
            For Each item As Cliente In lista
                item.Cidade = CidadeRepository.BuscarCidadePorId(item.CidadeId)
            Next
            Return lista
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub IncluirCliente(ByVal obj As Cliente)
        Try
            Dim rep = New SQLRepository(Of Cliente)("dbo.spIncluirCliente")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Nome", obj.Nome)
            parametros.Add("CidadeId", obj.CidadeId)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Shared Sub AtualizarCliente(ByVal obj As Cliente)
        Try
            Dim rep = New SQLRepository(Of Cliente)("dbo.spAtualizarCliente")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Nome", obj.Nome)
            parametros.Add("CidadeId", obj.CidadeId)
            parametros.Add("Id", obj.Id)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Shared Sub RemoverClientePorId(ByVal id As String)
        Try
            Dim rep = New SQLRepository(Of Cliente)("dbo.spRemoverCliente")
            Dim parametros = New Dictionary(Of String, Object)()
            parametros.Add("Id", id)
            rep.Exec(parametros)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
