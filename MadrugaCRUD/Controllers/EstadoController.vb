Imports System.Web.Mvc

Namespace Controllers
    Public Class EstadoController
        Inherits Controller

        <HttpGet>
        Public Function BuscarTodosEstados() As JsonResult
            Return Json(New With {Key .lista = EstadoRepository.BuscarTodosEstados()}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpGet>
        Public Function BuscarEstadoPorId(ByVal id As Integer) As JsonResult
            Return Json(New With {Key .item = EstadoRepository.BuscarEstadoPorId(id)}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function IncluirEstado(ByVal obj As Estado) As JsonResult
            If IsNothing(obj) Then
                Return Json(New With {Key .sucesso = False, .msg = "Objeto vazio"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.Nome) Then
                Return Json(New With {Key .sucesso = False, .msg = "Nome inválido"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.UF) Or obj.UF.Length <> 2 Then
                Return Json(New With {Key .sucesso = False, .msg = "UF inválido"}, JsonRequestBehavior.AllowGet)
            End If

            EstadoRepository.IncluirEstado(obj)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function AtualizarEstado(ByVal obj As Estado) As JsonResult
            If IsNothing(obj) Then
                Return Json(New With {Key .sucesso = False, .msg = "Objeto vazio"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.Nome) Then
                Return Json(New With {Key .sucesso = False, .msg = "Nome inválido"}, JsonRequestBehavior.AllowGet)
            End If

            If String.IsNullOrWhiteSpace(obj.UF) Or obj.UF.Length <> 2 Then
                Return Json(New With {Key .sucesso = False, .msg = "UF inválido"}, JsonRequestBehavior.AllowGet)
            End If

            EstadoRepository.AtualizarEstado(obj)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function

        <HttpPost>
        Public Function RemoverEstado(ByVal id As Integer) As JsonResult
            If CidadeRepository.BuscarCidadePorEstadoId(id).Any() Then
                Return Json(New With {Key .sucesso = False, .msg = "Não foi possível remover pois existe uma cidade cadastrado com esse estado"}, JsonRequestBehavior.AllowGet)
            End If

            EstadoRepository.RemoverEstadoPorId(id)

            Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
        End Function
    End Class
End Namespace