<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="Site desenvolvido pelo Rodrigo Madruga para aperfeiçoar suas técnicas de desenvolvimento.">
    <title>Madruga</title>
    <link rel="shortcut icon" href="~/Imagens/brazil.ico" type="image/x-icon" />
    <link href="~/Content/GoogleStyleRoboto.css" rel="stylesheet" type="text/css" />
    <link href="~/Content/icons/rpg-awesome/css/rpg-awesome.min.css" rel="stylesheet" /> @*Link:https://nagoshiashumari.github.io/Rpg-Awesome/*@
    <link href="~/Content/icons/fontawesome/css/font-awesome.min.css" rel="stylesheet" /> @*Link:https://fontawesome.com/v4.7/icons/*@
    <link href="~/Content/icons/icomoon/styles.css" rel="stylesheet" />
    <link href="~/Content/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Content/dashboard.css" rel="stylesheet" />
    <link href="~/Content/InputGroup.css" rel="stylesheet" />
    <link href="~/Content/bootstrap-validation.css" rel="stylesheet" />
    <link href="~/Content/swal.css" rel="stylesheet" />
    @Styles.Render("~/bundles/usuariocss")
</head>
<body>
    <nav class="navbar navbar-dark sticky-top bg-dark flex-md-nowrap p-0">
        <a class="navbar-brand col-sm-3 col-md-2 mr-0" style="color: #fff;">Bem-vindo!</a>
        <ul class="nav navbar-nav px-3 d-block d-sm-none" style="padding: .5rem 1rem;">
            <li class="dropdown dropdown-user">
                <a class="dropdown-toggle" data-toggle="dropdown">
                    <span style="color:#ffffff">MENU</span>
                    <i class="caret"></i>
                </a>
                <ul class="dropdown-menu px-3" style="background-color: #343a40 !important; border: 0px;">
                    <li class="nav-item">
                        <a class="nav-link" id="ItemMenuMobileClientes" onclick="loadPartialView('Clientes')">
                            <span data-feather="users"></span>&nbsp;
                            Clientes
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="ItemMenuMobileEstados" onclick="loadPartialView('Estados')">
                            <span data-feather="map"></span>&nbsp;
                            Estados
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="ItemMenuMobileCidades" onclick="loadPartialView('Cidades')">
                            <span data-feather="map-pin"></span>&nbsp;
                            Cidades
                        </a>
                    </li>
                </ul>
            </li>
        </ul>

    </nav>
    <div class="container-fluid">
        <div class="row">
            <nav class="col-md-2 d-none d-md-block bg-light sidebar">
                <div class="sidebar-sticky" id="divMenuUsuario" style="padding-top: 1.5rem;">
                    <ul class="nav flex-column">
                        <li class="nav-item">
                            <a class="nav-link active" id="ItemMenuPaginaInicial" onclick="loadPartialView('PaginaInicial')">
                                <span data-feather="home"></span>
                                Página Inicial <span class="sr-only">(current)</span>
                            </a>
                        </li>
                    </ul>
                    <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
                        <span>Funcionalidades</span>
                    </h6>
                    <ul class="nav flex-column mb-2">
                        <li class="nav-item">
                            <a class="nav-link" id="ItemMenuClientes" onclick="loadPartialView('Clientes')">
                                <span data-feather="users"></span>
                                Clientes
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="ItemMenuEstados" onclick="loadPartialView('Estados')">
                                <span data-feather="map"></span>
                                Estados
                            </a>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" id="ItemMenuCidades" onclick="loadPartialView('Cidades')">
                                <span data-feather="map-pin"></span>
                                Cidades
                            </a>
                        </li>
                    </ul>
                </div>
            </nav>

            @RenderBody()
            <div class="modal" tabindex="-1" data-backdrop="static" data-keyboard="false" id="ModalLoad" style="z-index: 1051;">
                <div class="modal-dialog modal-dialog-centered text-center" role="document">
                    <div class="spinner-border" style="margin: 0 auto; color: #00b760;"></div>
                </div>
            </div>
        </div>
    </div>

    <script src="~/Scripts/jquery-3.5.1.min.js"></script>
    <script src="~/Scripts/popper.min.js"></script>
    <script src="~/Scripts/bootstrap.min.js"></script>
    <script src="~/Scripts/plugins/notifications/sweet_alert.min.js"></script>
    <script src="~/Scripts/jquery.inputmask.bundle.js"></script>
    <script src="~/Scripts/autosize.js"></script>
    @Scripts.Render("~/bundles/usuariojs")
    <script src="~/Scripts/vue.js"></script>
    <script src="~/Scripts/axios.min.js"></script>
    @RenderSection("scripts", required:=False)

    <!-- Icons -->
    <script src="~/Scripts/feather.min.js"></script>
    <script>
        feather.replace()
    </script>
</body>
</html>
