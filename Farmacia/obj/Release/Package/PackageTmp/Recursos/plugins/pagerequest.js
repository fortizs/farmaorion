$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandlerPage);
    InicialJS();
    ConfigJS();
    ConfigAuxiliarJS();
});

function endRequestHandlerPage(sender, args) {
    InicialJS();
    ConfigJS();
    ConfigAuxiliarJS();
}

function InicialJS() {
    // Popover
    if ($('[data-popup="popover"]')[0]) {
        $('[data-popup="popover"]').popover();
    }

    // Tooltip
    if ($('[data-popup="tooltip"]')[0]) {
        $('[data-popup="tooltip"]').tooltip();
    }

    // Fancybox
    if ($('[data-popup="lightbox"]')[0]) {
        $('[data-popup="lightbox"]').fancybox({
            padding: 3
        });
    }
    
    // Calendario
    $(".daterangepicker").remove();

    // Calendario Combos
    if ($(".ui-com-fecha")[0]) {
        $('.ui-com-fecha').daterangepicker({
            autoApply: false,
            singleDatePicker: true,
            showDropdowns: true
        });
    }

    // Calendario
    if ($(".ui-com-fecha-simple")[0]) {
        $('.ui-com-fecha-simple').daterangepicker({
            autoApply: false,
            singleDatePicker: true
        });
    }
    
    // Monto
    if ($(".ui-com-monto")[0]) {
        $('.ui-com-monto').mask('000,000.00', { reverse: true });
    }

    // Set Number Value
    if ($(".ui-com-number")[0]) {
        $(".ui-com-number").click(function () {
            $(this).select();
        });
        $(".ui-com-number").change(function () {
            var $this = $(this);
            var val = parseInt($this.val());
            $this.val(!isNaN(val) ? val : '0');
        });
    }

    // Set Price Value
    if ($(".ui-com-price")[0]) {
        $(".ui-com-price").click(function () {
            $(this).select();
        });
        $(".ui-com-price").change(function () {
            var $this = $(this);
            var val = parseFloat($this.val()).toFixed(2);
            $this.val(!isNaN(val) ? val : '0.00');
        });
    }

    // Set Price Value
    if ($(".ui-com-price4")[0]) {
        $(".ui-com-price4").click(function () {
            $(this).select();
        });
        $(".ui-com-price4").change(function () {
            var $this = $(this);
            var val = parseFloat($this.val()).toFixed(4);
            $this.val(!isNaN(val) ? val : '0.0000');
        });
    }
}

function ConfigJS() {
}

function ConfigAuxiliarJS() {
}