Imports System.Web.Mvc

Namespace Controllers
    Public Class CidadeController
        Inherits Controller

        <HttpGet>
        Public Function BuscarTodasCidades() As JsonResult
            Return Json(New With {Key .lista = CidadeRepository.BuscarTodasCidades()}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpGet>
        Public Function BuscarCidadePorId(ByVal id As Integer) As JsonResult
            Return Json(New With {Key .item = CidadeRepository.BuscarCidadePorId(id)}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function IncluirCidade(ByVal obj As Cidade) As JsonResult
            If IsNothing(obj) Then
                Return Json(New With {Key .sucesso = False, .msg = "Objeto vazio"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.Nome) Then
                Return Json(New With {Key .sucesso = False, .msg = "Nome inválido"}, JsonRequestBehavior.AllowGet)
            End If

            If IsNothing(EstadoRepository.BuscarEstadoPorId(obj.EstadoId)) Then
                Return Json(New With {Key .sucesso = False, .msg = "Estado não encontrado"}, JsonRequestBehavior.AllowGet)
            End If

            CidadeRepository.IncluirCidade(obj)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function AtualizarCidade(ByVal obj As Cidade) As JsonResult
            If IsNothing(obj) Then
                Return Json(New With {Key .sucesso = False, .msg = "Objeto vazio"}, JsonRequestBehavior.AllowGet)
            End If

            If IsNothing(CidadeRepository.BuscarCidadePorId(obj.Id)) Then
                Return Json(New With {Key .sucesso = False, .msg = "Cidade não encontrada"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.Nome) Then
                Return Json(New With {Key .sucesso = False, .msg = "Nome inválido"}, JsonRequestBehavior.AllowGet)
            End If

            If IsNothing(EstadoRepository.BuscarEstadoPorId(obj.EstadoId)) Then
                Return Json(New With {Key .sucesso = False, .msg = "Estado não encontrado"}, JsonRequestBehavior.AllowGet)
            End If

            CidadeRepository.AtualizarCidade(obj)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function RemoverCidade(ByVal id As Integer) As JsonResult
            If ClienteRepository.BuscarClientesPorCidadeId(id).Any() Then
                Return Json(New With {Key .sucesso = False, .msg = "Não foi possível remover pois existe um cliente cadastrado com essa cidade"}, JsonRequestBehavior.AllowGet)
            End If

            CidadeRepository.RemoverCidadePorId(id)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace