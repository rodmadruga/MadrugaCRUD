Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index() As ActionResult
        Return View()
    End Function

    <HttpPut>
    Public Function TestePut() As JsonResult
        Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
    End Function

    <HttpGet>
    Public Function TesteGet() As JsonResult
        Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
    End Function

    <HttpDelete>
    Public Function TesteDelete() As JsonResult
        Return Json(New With {Key .sucesso = True}, JsonRequestBehavior.AllowGet)
    End Function

End Class
