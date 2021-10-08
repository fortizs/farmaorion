$(function() {
       
    $("a#mo" + $("#hfIDModulo").val()).parent('li').addClass("active");
    //$("a#mo" + $("#hfIDModulo").val()).addClass("moduloactivo");
    //$("a#sm" + $("#hfIDMenu").val()).addClass("menuactivo");
    $("a#sm" + $("#hfIDMenu").val()).parent('li').addClass("active");
    //$("a#sm" + $("#hfIDMenu").val()).parents('ul').addClass("collapse submenu list-unstyled show");

      
    /*
    $('.navigation a.moduloactivo').parent('li').each(function () {
        $(this).find('ul').show().find('li').find('a.menuactivo').addClass('active');
    });*/
});
 


function msgbox(tipo, titulo, mensaje) {
    alert(mensaje);
}

function msgnoti(tipo, mensaje) {
    alert(mensaje);
}

function ModalImpresion(capa, url) {
    $(capa + " iframe").attr("src", "");
    $(capa + " iframe").attr("src", url);
    $(capa).modal();
    return false;
}

function Mensaje(tipo, mensaje) {
    try {
        /*
          confirmation,
          error,
          warning,
          information
      */
        var conButtonColor = "";
        if (tipo == "confirmation") {
            tipo = "success";
            conButtonColor = "#66BB6A";
        } else if (tipo == "error") {
            conButtonColor = "#EF5350";
        } /*else if (tipo == "information") {
            tipo = "info";
            conButtonColor = "#2196F3";
        }*/
        else {
            tipo = "warning";
            conButtonColor = "#F07D00";
        }
        //swal('', mensaje, tipo);

        if (tipo == "confirmation") {
            const toast = swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 300000,
                padding: '2em'
            });
            new toast({
                type: 'success',
                title: mensaje,
                padding: '2em',
            })

            console.log("confi");
        }
        else {

            console.log("war");
            new swal({
                title: "",
                html: mensaje,
                text: mensaje,
                confirmButtonColor: conButtonColor,
                type: tipo
            });
        }
         
    }
    catch (ex) {
        console.log("error");
        alert(mensaje);
    }
}

function Notificacion(tipo, mensaje) {
    try {
        /*
            confirmation,
            error,
            warning,
            information
        */
        var conButtonColor = "";
        if (tipo == "confirmation") {
            tipo = "success";
            conButtonColor = "#66BB6A";
        } else if (tipo == "error") {
            conButtonColor = "#EF5350";
        } /*else if (tipo == "information") {
            tipo = "info";
            conButtonColor = "#2196F3";
        }*/
        else {
            tipo = "warning";
            conButtonColor = "#F07D00";
        }
        //swal('', mensaje, tipo);

        if (tipo == "success") {
            const toast = swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                padding: '2em'
            });
            new toast({
                type: 'success',
                title: mensaje,
                padding: '2em',
            })

            console.log("confi" + tipo);
        }
        else {

            console.log("war" + tipo);
            new swal({
                title: "",
                html: mensaje,
                text: mensaje,
                confirmButtonColor: conButtonColor,
                type: tipo
            });
        }

        //new toast({
        //    type: 'success',
        //    title: 'Signed in successfully',
        //    padding: '2em',
        //})

        //new swal({
        //    title: "",
        //    html: mensaje,
        //    text: mensaje,
        //    //confirmButtonColor: conButtonColor,
        //    type: tipo
        //});

        console.log("es una Notificacion");
    }
    catch (ex) {
        alert(mensaje);
    }
}

function Notificacion2(tipo, mensaje) {
    tipo = (tipo == "confirmation" ? "success" : tipo);
    tipo = (tipo == "information" ? "info" : tipo);
    try {
        var opts = {
            text: mensaje,
            delay: 3000,
            addclass: "stack-top-right bg-primary border-primary",
            history: false
        };
        switch (tipo) {
            case 'error':
                delay: 10000;
                opts.addclass = "stack-top-right bg-danger border-danger";
                opts.type = "error";
                break;
            case 'info':
                delay: 3000;
                opts.addclass = "stack-top-right bg-info border-info";
                opts.type = "info";
                break;
            case 'success':
                delay: 3000;
                opts.addclass = "stack-top-right bg-success border-success";
                opts.type = "success";
                break;
        }

        const toast = swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 300000,
            padding: '2em'
        });
        new toast({
            type: 'success',
            title: mensaje,
            padding: '2em',
        })
        //new PNotify(opts);
        console.log("111"); 
    }
    catch (ex) {
        console.log("222");
        alert(mensaje);
    }
}

var SMConfirm = false;
function Confirmacion(control, evt, msg1, msg2) {
    console.log("111");
    evt = (evt) ? evt : window.event;
    console.log("entrando01");
    
    //swal({
    //    title: 'Are you sure?',
    //    text: "You won't be able to revert this!",
    //    type: 'warning',
    //    showCancelButton: true,
    //    confirmButtonText: 'Delete',
    //    padding: '2em'
    //}).then(function (result) {
    //    if (result.value) {
    //        swal(
    //          'Deleted!',
    //          'Your file has been deleted.',
    //          'success'
    //        )
    //    }
    //})


    if (!SMConfirm) {
        new swal({
            title: msg1,
            text: msg2, 
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#1b55e2",
            confirmButtonText: "SI",
            cancelButtonText: "NO"
        },
         function (isConfirm) {
             console.log("1");
             if (isConfirm) {
                 console.log("2");
                 setTimeout(
                     function () {
                         SMConfirm = true;
                         control.click();
                         console.log("12");
                     },
                   300);
             }
         }).then(function (result) {
                 if (result.value) {
                     setTimeout(
                      function () {
                          SMConfirm = true;
                          control.click();
                          console.log("12");
                      },
                    300);
                 }
             })
        console.log("333344");
        (evt.preventDefault) ? evt.preventDefault() : evt.returnValue = false;
    } else {
        console.log("44");
        SMConfirm = false;
    }
}

function AbrirModal(pIDModal) {
    $('#' + pIDModal).modal({ backdrop: 'static', keyboard: false });
    return false;
}

function CerrarModal(pIDModal) {
    $('#' + pIDModal).modal('hide');
    return false;
}