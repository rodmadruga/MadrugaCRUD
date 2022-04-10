<main id="root" role="main" class="col-md-9 ml-sm-auto col-lg-10 pt-3 px-4">
    <div id="divPaginaInicial" class="itemUsuarioMenu">
        @Html.Partial("_PaginaInicial")
    </div>
    <div id="divClientes" class="itemUsuarioMenu d-none">
        <div id="modal-cliente" class="modal fade" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-lg-12 text-center">
                                <span v-if="!cliente.id">Cadastrar novo cliente:</span>
                                <span v-else>Alterar cliente:</span>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-lg-3">
                                <span>Nome:</span>
                            </div>
                            <div class="col-lg-9">
                                <input type="text" id="clienteNome" v-model="cliente.nome" class="form-control">
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-lg-3">
                                <span>Cidade:</span>
                            </div>
                            <div class="col-lg-9">
                                <select class="form-control" v-model="cliente.cidadeid" id="clienteCidade">
                                    <option value="">Selecione</option>
                                    <option v-for="(itemcidade,index) in listaCidades" :value="itemcidade.Id">{{ itemcidade.Nome }}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btnVermelhoRedondo" style="height: 36px;" data-dismiss="modal">Cancelar</button>
                        <button type="button" v-on:click="salvarCliente()" :disabled="desabilitarBotoes" class="btn bg-green-800"><b><i class="icon-paperplane"></i></b><span class="ladda-label">Salvar</span></button>
                    </div>
                </div>
            </div>
        </div>
        <div id="dClientes">
            <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pb-2 mb-3 border-bottom">
                <h1 class="h2">Clientes</h1>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div v-if="listaClientes.length > 0">
                        <table cellspacing="0" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                            <tr class="gridHeaderStyle">
                                <th class="centerHeaderText" scope="col">
                                    Nome
                                </th>
                                <th class="centerHeaderText" scope="col">
                                    Cidade
                                </th>
                                <th style="width: 40px;" scope="col"></th>
                                <th style="width: 40px;" scope="col"></th>
                            </tr>

                            <tr class="gridRowStyle" v-for="(item, index) in listaClientes" :key="index">
                                <td align="center">
                                    {{item.Nome}}
                                </td>
                                <td align="center">
                                    {{item.Cidade.Nome}}
                                </td>
                                <td align="center">
                                    <input type="image" title="Alterar" src="/Imagens/edit.png" v-on:click="abrirModalAlterarCliente(item.Id)">
                                </td>
                                <td align="center">
                                    <input type="image" title="Remover" src="/Imagens/cancela.jpg" v-on:click="removerCliente(item.Id)">
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-lg-4">
                    <br>
                    <button type="button" v-on:click="abrirModalNovoCliente()" class="btn bg-green-800 btn-block">Novo Cliente <i class="icon-file-empty position-right"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div id="divEstados" class="itemUsuarioMenu d-none">
        <div id="modal-estado" class="modal fade" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-lg-12 text-center">
                                <span v-if="!estado.id">Cadastrar novo estado:</span>
                                <span v-else>Alterar estado:</span>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-lg-3">
                                <span>Nome:</span>
                            </div>
                            <div class="col-lg-9">
                                <input type="text" id="estadoNome" v-model="estado.nome" class="form-control">
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-lg-3">
                                <span>UF:</span>
                            </div>
                            <div class="col-lg-9">
                                <input type="text" id="estadoUF" v-model="estado.uf" class="form-control" maxlength="2" style="text-transform: uppercase;">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btnVermelhoRedondo" style="height: 36px;" data-dismiss="modal">Cancelar</button>
                        <button type="button" v-on:click="salvarEstado()" :disabled="desabilitarBotoes" class="btn bg-green-800"><b><i class="icon-paperplane"></i></b><span class="ladda-label">Salvar</span></button>
                    </div>
                </div>
            </div>
        </div>
        <div id="dEstados">
            <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pb-2 mb-3 border-bottom">
                <h1 class="h2">Estados</h1>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div v-if="listaEstados.length > 0">
                        <table cellspacing="0" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                            <tr class="gridHeaderStyle">
                                <th class="centerHeaderText" scope="col">
                                    Nome
                                </th>
                                <th class="centerHeaderText" scope="col">
                                    UF
                                </th>
                                <th style="width: 40px;" scope="col"></th>
                                <th style="width: 40px;" scope="col"></th>
                            </tr>

                            <tr class="gridRowStyle" v-for="(item, index) in listaEstados" :key="index">
                                <td align="center">
                                    {{item.Nome}}
                                </td>
                                <td align="center">
                                    {{item.UF}}
                                </td>
                                <td align="center">
                                    <input type="image" title="Alterar" src="/Imagens/edit.png" v-on:click="abrirModalAlterarEstado(item.Id)">
                                </td>
                                <td align="center">
                                    <input type="image" title="Remover" src="/Imagens/cancela.jpg" v-on:click="removerEstado(item.Id)">
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-lg-4">
                    <br>
                    <button type="button" v-on:click="abrirModalNovoEstado()" class="btn bg-green-800 btn-block">Novo Estado <i class="icon-file-empty position-right"></i></button>
                </div>
            </div>
        </div>
    </div>
    <div id="divCidades" class="itemUsuarioMenu d-none">
        <div id="modal-cidade" class="modal fade" data-keyboard="false" data-backdrop="static">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row form-group">
                            <div class="col-lg-12 text-center">
                                <span v-if="!cidade.id">Cadastrar nova cidade:</span>
                                <span v-else>Alterar cidade:</span>
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-lg-3">
                                <span>Nome:</span>
                            </div>
                            <div class="col-lg-9">
                                <input type="text" id="cidadeNome" v-model="cidade.nome" class="form-control">
                            </div>
                        </div>
                        <div class="row form-group">
                            <div class="col-lg-3">
                                <span>Estado:</span>
                            </div>
                            <div class="col-lg-9">
                                <select class="form-control" v-model="cidade.estadoid" id="cidadeEstado">
                                    <option value="">Selecione</option>
                                    <option v-for="(itemestado,index) in listaEstados" :value="itemestado.Id">{{ itemestado.Nome }}</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btnVermelhoRedondo" style="height: 36px;" data-dismiss="modal">Cancelar</button>
                        <button type="button" v-on:click="salvarCidade()" :disabled="desabilitarBotoes" class="btn bg-green-800"><b><i class="icon-paperplane"></i></b><span class="ladda-label">Salvar</span></button>
                    </div>
                </div>
            </div>
        </div>
        <div id="dCidades">
            <div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pb-2 mb-3 border-bottom">
                <h1 class="h2">Cidades</h1>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div v-if="listaCidades.length > 0">
                        <table cellspacing="0" rules="all" border="1" style="width: 100%; border-collapse: collapse;">
                            <tr class="gridHeaderStyle">
                                <th class="centerHeaderText" scope="col">
                                    Nome
                                </th>
                                <th class="centerHeaderText" scope="col">
                                    Estado
                                </th>
                                <th style="width: 40px;" scope="col"></th>
                                <th style="width: 40px;" scope="col"></th>
                            </tr>

                            <tr class="gridRowStyle" v-for="(item, index) in listaCidades" :key="index">
                                <td align="center">
                                    {{item.Nome}}
                                </td>
                                <td align="center">
                                    {{item.Estado.UF}} - {{item.Estado.Nome}}
                                </td>
                                <td align="center">
                                    <input type="image" title="Alterar" src="/Imagens/edit.png" v-on:click="abrirModalAlterarCidade(item.Id)">
                                </td>
                                <td align="center">
                                    <input type="image" title="Remover" src="/Imagens/cancela.jpg" v-on:click="removerCidade(item.Id)">
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </div>
            <div class="row form-group">
                <div class="col-lg-4">
                    <br>
                    <button type="button" v-on:click="abrirModalNovoCidade()" class="btn bg-green-800 btn-block">Nova Cidade <i class="icon-file-empty position-right"></i></button>
                </div>
            </div>
        </div>
    </div>
</main>

@Section Scripts
    <script>
        var url = '@Url.RouteUrl("Default")';
    </script>
    <script>
        var app = new Vue({
            el: "#root",
            data() {
                return {
                    desabilitarBotoes: false,
                    listaClientes: [],
                    listaCidades: [],
                    listaEstados: [],
                    cliente: {
                        id: "",
                        nome: "",
                        cidadeid: ""
                    },
                    cidade: {
                        id: "",
                        nome: "",
                        estadoid: ""
                    },
                    estado: {
                        id: "",
                        nome: "",
                        uf: ""
                    },
                }
            },
            created: function () {
                this.carregarGrids();
            },
            methods: {
                carregarGrids() {
                    this.buscarTodosEstados();
                    this.buscarTodasCidades();
                    this.buscarTodosClientes();
                },
                abrirLoading() {
                    $("#ModalLoad").modal('show');
                },
                fecharLoading() {
                    setTimeout(
                        function () {
                            $("#ModalLoad").modal('hide');
                        }, 50);
                },

                //INICIO CLIENTE
                buscarTodosClientes() {
                    this.abrirLoading();
                    var result = $.get("/Cliente/BuscarTodosClientes");
                    result.done(function (response) {
                        app.fecharLoading();
                        app.listaClientes = response.lista;
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                    });
                },
                abrirModalNovoCliente() {
                    app.cliente.id = "";
                    app.cliente.nome = "";
                    app.cliente.cidadeid = "";
                    $("#clienteNome").removeClass("is-invalid");
                    $("#clienteCidade").removeClass("is-invalid");
                    $('#modal-cliente').modal();
                },
                abrirModalAlterarCliente(id) {
                    this.abrirLoading();
                    var result = $.get("/Cliente/BuscarClientePorId?id=" + id);
                    result.done(function (response) {
                        app.fecharLoading();
                        app.cliente.id = response.item.Id;
                        app.cliente.nome = response.item.Nome;
                        app.cliente.cidadeid = response.item.CidadeId;
                        $("#clienteNome").removeClass("is-invalid");
                        $("#clienteCidade").removeClass("is-invalid");
                        $('#modal-cliente').modal();
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                    });
                },
                validaCliente() {
                    var validou = true;
                    if (isNullOrWhitespace(app.cliente.nome)) {
                        validou = false;
                        $("#clienteNome").addClass("is-invalid");
                    } else {
                        $("#clienteNome").removeClass("is-invalid");
                    }

                    if (isNullOrWhitespace(app.cliente.cidadeid.toString())) {
                        validou = false;
                        $("#clienteCidade").addClass("is-invalid");
                    } else {
                        $("#clienteCidade").removeClass("is-invalid");
                    }

                    return validou;
                },
                salvarCliente() {
                    if (this.validaCliente()) {
                        app.desabilitarBotoes = true;
                        if (!app.cliente.id) {
                            var result = $.post("/Cliente/IncluirCliente", {
                                Nome: app.cliente.nome, CidadeId: app.cliente.cidadeid
                            });
                            result.done(function (response) {
                                app.desabilitarBotoes = false;
                                if (response.sucesso) {
                                    $("#modal-cliente").modal("toggle");
                                    swal({
                                        title: "Sucesso!",
                                        text: "Cliente cadastrado com sucesso!",
                                        type: "success"
                                    }, function () {
                                        app.carregarGrids();
                                    });
                                } else {
                                    swal({
                                        title: "Ops!",
                                        text: response.msg,
                                        type: "error"
                                    });
                                }                                
                            });
                            result.fail(function (data) {
                                app.desabilitarBotoes = false;
                                console.log(error);
                                swal({
                                    title: "Ops!",
                                    text: "Verifique se todos os campos foram preenchidos corretamente antes de prosseguir.",
                                    type: "error"
                                });
                            });
                        } else {
                            var result = $.post("/Cliente/AtualizarCliente", {
                                Id: app.cliente.id, Nome: app.cliente.nome, CidadeId: app.cliente.cidadeid
                            });
                            result.done(function (response) {
                                app.desabilitarBotoes = false;
                                if (response.sucesso) {
                                    $("#modal-cliente").modal("toggle");
                                    swal({
                                        title: "Sucesso!",
                                        text: "Cliente alterado com sucesso!",
                                        type: "success"
                                    }, function () {
                                        app.carregarGrids();
                                    });
                                } else {
                                    swal({
                                        title: "Ops!",
                                        text: response.msg,
                                        type: "error"
                                    });
                                }
                            });
                            result.fail(function (data) {
                                app.desabilitarBotoes = false;
                                console.log(error);
                                swal({
                                    title: "Ops!",
                                    text: "Verifique se todos os campos foram preenchidos corretamente antes de prosseguir.",
                                    type: "error"
                                });
                            });
                        }
                    }
                },
                removerCliente(id) {
                    this.abrirLoading();
                    var result = $.post("/Cliente/RemoverCliente", {
                        Id: id
                    });
                    result.done(function (response) {
                        app.fecharLoading();
                        if (response.sucesso) {
                            swal({
                                title: "Sucesso!",
                                text: "Cliente removido com sucesso!",
                                type: "success"
                            }, function () {
                                app.carregarGrids();
                            });
                        } else {
                            swal({
                                title: "Ops!",
                                text: response.msg,
                                type: "error"
                            });
                        }
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                        swal({
                            title: "Ops!",
                            text: "Ocorreu um erro.",
                            type: "error"
                        });
                    });
                },
                //FIM CLIENTE

                //INICIO ESTADO
                buscarTodosEstados() {
                    this.abrirLoading();
                    var result = $.get("/Estado/BuscarTodosEstados");
                    result.done(function (response) {
                        app.fecharLoading();
                        app.listaEstados = response.lista;
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                    });
                },
                abrirModalNovoEstado() {
                    app.estado.id = "";
                    app.estado.nome = "";
                    app.estado.uf = "";
                    $("#estadoNome").removeClass("is-invalid");
                    $("#estadoUF").removeClass("is-invalid");
                    $('#modal-estado').modal();
                },
                abrirModalAlterarEstado(id) {
                    this.abrirLoading();
                    var result = $.get("/Estado/BuscarEstadoPorId?id=" + id);
                    result.done(function (response) {
                        app.fecharLoading();
                        app.estado.id = response.item.Id;
                        app.estado.nome = response.item.Nome;
                        app.estado.uf = response.item.UF;
                        $("#estadoNome").removeClass("is-invalid");
                        $("#estadoUF").removeClass("is-invalid");
                        $('#modal-estado').modal();
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                    });
                },
                validaEstado() {
                    var validou = true;
                    if (isNullOrWhitespace(app.estado.nome)) {
                        validou = false;
                        $("#estadoNome").addClass("is-invalid");
                    } else {
                        $("#estadoNome").removeClass("is-invalid");
                    }

                    if (isNullOrWhitespace(app.estado.uf) || app.estado.uf.length != 2) {
                        validou = false;
                        $("#estadoUF").addClass("is-invalid");
                    } else {
                        $("#estadoUF").removeClass("is-invalid");
                    }

                    return validou;
                },
                salvarEstado() {
                    if (this.validaEstado()) {
                        app.desabilitarBotoes = true;
                        if (!app.estado.id) {
                            var result = $.post("/Estado/IncluirEstado", {
                                Nome: app.estado.nome, UF: app.estado.uf
                            });
                            result.done(function (response) {
                                app.desabilitarBotoes = false;
                                if (response.sucesso) {
                                    $("#modal-estado").modal("toggle");
                                    swal({
                                        title: "Sucesso!",
                                        text: "Estado cadastrado com sucesso!",
                                        type: "success"
                                    }, function () {
                                        app.carregarGrids();
                                    });
                                } else {
                                    swal({
                                        title: "Ops!",
                                        text: response.msg,
                                        type: "error"
                                    });
                                }                                
                            });
                            result.fail(function (data) {
                                app.desabilitarBotoes = false;
                                console.log(error);
                                swal({
                                    title: "Ops!",
                                    text: "Verifique se todos os campos foram preenchidos corretamente antes de prosseguir.",
                                    type: "error"
                                });
                            });
                        } else {
                            var result = $.post("/Estado/AtualizarEstado", {
                                Id: app.estado.id, Nome: app.estado.nome, UF: app.estado.uf
                            });
                            result.done(function (response) {
                                app.desabilitarBotoes = false;
                                if (response.sucesso) {
                                    $("#modal-estado").modal("toggle");
                                    swal({
                                        title: "Sucesso!",
                                        text: "Estado alterado com sucesso!",
                                        type: "success"
                                    }, function () {
                                        app.carregarGrids();
                                    });
                                } else {
                                    swal({
                                        title: "Ops!",
                                        text: response.msg,
                                        type: "error"
                                    });
                                }
                            });
                            result.fail(function (data) {
                                app.desabilitarBotoes = false;
                                console.log(error);
                                swal({
                                    title: "Ops!",
                                    text: "Verifique se todos os campos foram preenchidos corretamente antes de prosseguir.",
                                    type: "error"
                                });
                            });
                        }
                    }
                },
                removerEstado(id) {
                    this.abrirLoading();
                    var result = $.post("/Estado/RemoverEstado", {
                        Id: id
                    });
                    result.done(function (response) {
                        app.fecharLoading();
                        if (response.sucesso) {
                            swal({
                                title: "Sucesso!",
                                text: "Estado removido com sucesso!",
                                type: "success"
                            }, function () {
                                app.carregarGrids();
                            });
                        } else {
                            swal({
                                title: "Ops!",
                                text: response.msg,
                                type: "error"
                            });
                        }                        
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                        swal({
                            title: "Ops!",
                            text: "Ocorreu um erro.",
                            type: "error"
                        });
                    });
                },
                //FIM ESTADO

                //INICIO CIDADE
                buscarTodasCidades() {
                    this.abrirLoading();
                    var result = $.get("/Cidade/BuscarTodasCidades");
                    result.done(function (response) {
                        app.fecharLoading();
                        app.listaCidades = response.lista;
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                    });
                },
                abrirModalNovoCidade() {
                    app.cidade.id = "";
                    app.cidade.nome = "";
                    app.cidade.estadoid = "";
                    $("#cidadeNome").removeClass("is-invalid");
                    $("#cidadeEstado").removeClass("is-invalid");
                    $('#modal-cidade').modal();
                },
                abrirModalAlterarCidade(id) {
                    this.abrirLoading();
                    var result = $.get("/Cidade/BuscarCidadePorId?id=" + id);
                    result.done(function (response) {
                        app.fecharLoading();
                        app.cidade.id = response.item.Id;
                        app.cidade.nome = response.item.Nome;
                        app.cidade.estadoid = response.item.EstadoId;
                        $("#cidadeNome").removeClass("is-invalid");
                        $("#cidadeEstado").removeClass("is-invalid");
                        $('#modal-cidade').modal();
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                    });
                },
                validaCidade() {
                    var validou = true;
                    if (isNullOrWhitespace(app.cidade.nome)) {
                        validou = false;
                        $("#cidadeNome").addClass("is-invalid");
                    } else {
                        $("#cidadeNome").removeClass("is-invalid");
                    }

                    if (isNullOrWhitespace(app.cidade.estadoid.toString())) {
                        validou = false;
                        $("#cidadeEstado").addClass("is-invalid");
                    } else {
                        $("#cidadeEstado").removeClass("is-invalid");
                    }

                    return validou;
                },
                salvarCidade() {
                    if (this.validaCidade()) {
                        app.desabilitarBotoes = true;
                        if (!app.cidade.id) {
                            var result = $.post("/Cidade/IncluirCidade", {
                                Nome: app.cidade.nome, EstadoId: app.cidade.estadoid
                            });
                            result.done(function (response) {
                                app.desabilitarBotoes = false;
                                if (response.sucesso) {
                                    $("#modal-cidade").modal("toggle");
                                    swal({
                                        title: "Sucesso!",
                                        text: "Cidade cadastrado com sucesso!",
                                        type: "success"
                                    }, function () {
                                        app.carregarGrids();
                                    });
                                } else {
                                    swal({
                                        title: "Ops!",
                                        text: response.msg,
                                        type: "error"
                                    });
                                }                                
                            });
                            result.fail(function (data) {
                                app.desabilitarBotoes = false;
                                console.log(error);
                                swal({
                                    title: "Ops!",
                                    text: "Verifique se todos os campos foram preenchidos corretamente antes de prosseguir.",
                                    type: "error"
                                });
                            });
                        } else {
                            var result = $.post("/Cidade/AtualizarCidade", {
                                Id: app.cidade.id, Nome: app.cidade.nome, EstadoId: app.cidade.estadoid
                            });
                            result.done(function (response) {
                                app.desabilitarBotoes = false;
                                if (response.sucesso) {
                                    $("#modal-cidade").modal("toggle");
                                    swal({
                                        title: "Sucesso!",
                                        text: "Cidade alterado com sucesso!",
                                        type: "success"
                                    }, function () {
                                        app.carregarGrids();
                                    });
                                } else {
                                    swal({
                                        title: "Ops!",
                                        text: response.msg,
                                        type: "error"
                                    });
                                }
                            });
                            result.fail(function (data) {
                                app.desabilitarBotoes = false;
                                console.log(error);
                                swal({
                                    title: "Ops!",
                                    text: "Verifique se todos os campos foram preenchidos corretamente antes de prosseguir.",
                                    type: "error"
                                });
                            });
                        }
                    }
                },
                removerCidade(id) {
                    this.abrirLoading();
                    var result = $.post("/Cidade/RemoverCidade", {
                        Id: id
                    });
                    result.done(function (response) {
                        app.fecharLoading();
                        if (response.sucesso) {
                            swal({
                                title: "Sucesso!",
                                text: "Cidade removida com sucesso!",
                                type: "success"
                            }, function () {
                                app.carregarGrids();
                            });
                        } else {
                            swal({
                                title: "Ops!",
                                text: response.msg,
                                type: "error"
                            });
                        }                       
                    });
                    result.fail(function (data) {
                        app.fecharLoading();
                        console.log(error);
                        swal({
                            title: "Ops!",
                            text: "Ocorreu um erro.",
                            type: "error"
                        });
                    });
                },
                //FIM CIDADE
            },
        });
    </script>
End Section