var serviceCheck = function ($) {

    var panel = $('.panel'),
        panelBody = $('.panel-body'),
        success = $('#successTitle'),
        danger = $('#errorTitle'),
        btnStatus = $('#btnStatus');

    function init() {
        btnStatus.click(handleStatusBtnClick);
    }

    function handleStatusBtnClick() {
        panel.addClass('hidden');
        btnStatus.prop('disabled', true);
        $.get("service-status")
            .done(function(args) {
                if (args.status == 'ok')
                    showSuccess();
            })
            .fail(function(args) {
                showDanger();
                console.log(args);
            })
            .always(function() {
                btnStatus.prop('disabled', false);
            });
    }

    function showSuccess() {
        success.removeClass('hidden');
        panel.removeClass('hidden');
        panel.addClass('panel-success');
        danger.addClass('hidden');
    }

    function showDanger() {
        danger.removeClass('hidden');
        panel.removeClass('hidden');
        panel.addClass('panel-danger');
        success.addClass('hidden');
    }

    init();

}(jQuery);