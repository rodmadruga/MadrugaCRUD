Namespace Controllers
    Public Class ClienteController
        Inherits Controller

        <HttpGet>
        Public Function BuscarTodosClientes() As JsonResult
            Return Json(New With {Key .lista = ClienteRepository.BuscarTodosClientes()}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpGet>
        Public Function BuscarClientePorId(ByVal id As Integer) As JsonResult
            Return Json(New With {Key .item = ClienteRepository.BuscarClientePorId(id)}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function IncluirCliente(ByVal obj As Cliente) As JsonResult
            If IsNothing(obj) Then
                Return Json(New With {Key .sucesso = False, .msg = "Objeto vazio"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.Nome) Then
                Return Json(New With {Key .sucesso = False, .msg = "Nome inválido"}, JsonRequestBehavior.AllowGet)
            End If

            If IsNothing(CidadeRepository.BuscarCidadePorId(obj.CidadeId)) Then
                Return Json(New With {Key .sucesso = False, .msg = "Cidade não encontrada"}, JsonRequestBehavior.AllowGet)
            End If

            ClienteRepository.IncluirCliente(obj)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function AtualizarCliente(ByVal obj As Cliente) As JsonResult
            If IsNothing(obj) Then
                Return Json(New With {Key .sucesso = False, .msg = "Objeto vazio"}, JsonRequestBehavior.AllowGet)
            End If

            If IsNothing(ClienteRepository.BuscarClientePorId(obj.Id)) Then
                Return Json(New With {Key .sucesso = False, .msg = "Cliente não encontrado"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.Nome) Then
                Return Json(New With {Key .sucesso = False, .msg = "Nome inválido"}, JsonRequestBehavior.AllowGet)
            End If

            If IsNothing(CidadeRepository.BuscarCidadePorId(obj.CidadeId)) Then
                Return Json(New With {Key .sucesso = False, .msg = "Cidade não encontrada"}, JsonRequestBehavior.AllowGet)
            End If

            ClienteRepository.AtualizarCliente(obj)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function RemoverCliente(ByVal id As Integer) As JsonResult
            ClienteRepository.RemoverClientePorId(id)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function

    End Class
End Namespace