function validaEmail(email) {
    
    er = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (!er.exec(email)) {
       // jQuery('#mensagen').html('Email inválido!');
        return false;
    } else {
        return true;
    }
}

$(document).ready(function () {
    $('#emailEsqueceu').keyup(function () {
        var $form = $(this).closest('form'),
            $group = $(this).closest('.input-group'),
            $addon = $group.find('.input-group-addon'),
            $icon = $addon.find('span'),
            state = false;

        state = validaEmail($('#emailEsqueceu').val());
        //if ($group.data('validate') == "email") {
        //    state = validaEmail($('#emailEsqueceu').val())
        //    console.log(state);
        //}

        if (state) {
            $addon.removeClass('danger');
            $addon.addClass('success');
            $icon.attr('class', 'glyphicon glyphicon-ok');
            $('#btnEnvioEmail').prop('disabled', false);
        } else {
            $addon.removeClass('success');
            $addon.addClass('danger');
            $icon.attr('class', 'glyphicon glyphicon-remove');
            $('#btnEnvioEmail').prop('disabled', true);
        }

        //if ($form.find('.input-group-addon.danger').length == 0) {
        //    $form.find('[type="button"]').prop('disabled', false);            
        //} else {            
        //    $form.find('[type="button"]').prop('disabled', true);
        //}
    });




});
$("#btnEnvioEmail").click(function () {

    
    if ($("#emailEsqueceu").val() != null || $("#emailEsqueceu").val() != "") {
        OpenLoading();
        $.ajax({
            type: 'GET',
            url: "/Esqueceu/EnvioEmail",
            dataType: 'json',
            data: { email: $("#emailEsqueceu").val() },

            success: function (resset) {
                CloeseLoading();
                $(location).attr('href', '/login');

            },
            error: function (ex) {
                alert('Ocorreu um erro no envio.' + ex);
            }
        });

    }
    else {
        $("#mensagen").html("Preencha o seu E-mail.");
    }

})
