$(function () {
    
    //$("form[name='userForm']").validate({
    $("#userForm").validate({
        rules: {

            primeironome: "required",
            sobrenome: "required",
            email: {
                required: true,
                email: true,
                maxlength: 254
            },
            tel: "required",
            cpf: "required",
            cep: "required",
            senha1: "required",
            senha2: {
                required: true,
                equalTo: "#senha1"
            }
        },

        messages: {
            primeironome: "Digite o seu nome",
            sobrenome: "Digite o seu sobrenome",
            email: {
                required: "Digite um e-mail",
                email: "Digite um e-mail válido"
            },
            tel: "Digite um número de telefone",
            cpf: "Digite o seu CPF",
            cep: "Digite o seu CEP",
            senha1: "Digite a sua senha",
            senha2: {
                required: "Confirme a sua senha",
                equalTo: "As senhas precisam ser iguais"
            }
        },

        submitHandler: function (form) {
            form.submit();
        }
    });
});