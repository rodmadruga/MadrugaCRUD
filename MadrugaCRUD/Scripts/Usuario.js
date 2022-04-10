$(document).ready(function () {
    
});

function loadPartialView(view) {
    $("#divMenuUsuario .nav-link").removeClass("active");
    $(`#ItemMenu${view}`).addClass("active");
    $(".itemUsuarioMenu").removeClass("d-block").addClass("d-none");
    $(`#div${view}`).removeClass("d-none").addClass("d-block");
}
