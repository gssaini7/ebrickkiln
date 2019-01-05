$(function () {
    $.ajaxSetup({ cache: false });
    //$('.dynamicpopupcontainer').on('click', 'a.dynamicpopup', function (e) {
    //    e.preventDefault();
    //    //$('#myModal').modal('show');
    //    $('#myModalContent').load($(this).attr('href'), function () {
    //        $('#myModal').modal('show');
    //    });
    //    return false;
    //});

    $('.tablecontainer').on('click', 'a.dynamicpopupmodal', function (e) {
        e.preventDefault();
        $('#myModalContent').load($(this).attr('href'), function () {
            $('#myModal').modal('show');
        });
        return false;
    });

    $("a[data-modal]").on("click", function (e) {
        // hide dropdown if any (this is used wehen invoking modal from link in bootstrap dropdown )
        //$(e.target).closest('.btn-group').children('.dropdown-toggle').dropdown('toggle');

        $('#myModalContent').load(this.href, function () {
            $('#myModal').modal({
                /*backdrop: 'static',*/
                keyboard: true
            }, 'show');
            bindForm1(this, '#myModalContent', '#myModal');

        });
        return false;
    });

    //$('#myModal').on('hidden.bs.modal', function () {
        
    //});

    var globaldatatabel = null, globaldatatablePopup = null;

    var trigger = $('.hamburger'),
      overlay = $('.overlay'),
     isClosed = false;

    trigger.click(function () {
        hamburger_cross();
    });

    function hamburger_cross() {

        if (isClosed == true) {
            overlay.hide();
            trigger.removeClass('is-open');
            trigger.addClass('is-closed');
            isClosed = false;
        } else {
            overlay.show();
            trigger.removeClass('is-closed');
            trigger.addClass('is-open');
            isClosed = true;
        }
    }

    $('[data-toggle="offcanvas"]').click(function () {
        $('#wrapper').toggleClass('toggled');
    });

   
        
    
});


function filldatatable(dataobject, savepage) {
    var oTable = $('#myDatatable').DataTable(dataobject);
    globaldatatabel = oTable;
    if(savepage!=null)
        loadblankform(savepage);
    $('.tablecontainer').on('click', 'a.popup', function (e) {
        e.preventDefault();
        $('#myFormContent').load($(this).attr('href'), function () {
            bindForm(savepage);
            $("#collapseOne").addClass("in");

        });
        SelectRowOnEdit(this, globaldatatabel);
        return false;
    });

    //oTable.on('order.dt search.dt', function () {
    //    oTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();
}

function filldatatablepopup(dataobject, savepage) {
    var oTable = $('#popupDatatable').DataTable(dataobject);
    globaldatatablePopup = oTable;
    loadblankformpopup(savepage);
    $('.tablecontainerpopup').on('click', 'a.popup', function (e) {
        e.preventDefault();

        $('#myFormContentPopup').load($(this).attr('href'), function () {
            bindFormpopup(savepage);
            $("#collapseTwo").addClass("in");
        });
        SelectRowOnEdit(this, globaldatatablePopup);
        return false;
    });

    //oTable.on('order.dt search.dt', function () {
    //    oTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();
}

function bindForm(savepage) {
    $('form#baseForm').submit(function () {
        var $form = $(this);
        $.validator.unobtrusive.parse($form);
        $form.validate();
        if (!$form.valid()) {
            return false;
        }
        $.ajax({
            type: "POST",
            url: this.action,
            data: $(this).serialize(),
            success: function (data) {
                if (data.status) {
                    globaldatatabel.ajax.reload();
                    loadblankform(savepage);
                }
                else {
                    if (data.message != null)
                        alert(data.message);
                }
            },
            error: function (err) {
                alert(err.statusText);
            },
          
        });
        return false;
    });


}

function bindFormpopup(savepage) {
    $('form#popupForm').submit(function () {
        var $form = $(this);
        $.validator.unobtrusive.parse($form);
        $form.validate();
        if (!$form.valid()) {
            return false;
        }
        $.ajax({
            type: "POST",
            url: this.action,
            data: $(this).serialize(),
            success: function (data) {

                if (data.status) {
                    globaldatatablePopup.ajax.reload();
                    loadblankformpopup(savepage);
                }
            },
            error: function (err) {
                alert(err.statusText);
            },
           
        })
        return false;
    });
}

function loadblankform(savepage) {
    $('#myFormContent').load(savepage, function () {
        bindForm(savepage);
    });

    globaldatatabel.$('tr.selected').removeClass('selected');
}

function loadblankformpopup(savepage) {
    $('#myFormContentPopup').load(savepage, function () {
        bindFormpopup(savepage);
    });
    globaldatatablePopup.$('tr.selected').removeClass('selected');
}

function SelectRowOnEdit(selector, otablel) {
    var parenttr = $(selector).closest("tr");
    otablel.$('tr.selected').removeClass('selected');
    $(parenttr).addClass('selected');
}

function FillDD(url, val, ddli) {
    $.getJSON(url, function (result) {
        var ddl = $(ddli);
        ddl.empty();
        blankDdl(ddl);
        $.each(result.data, function (id, option) {
            ddl.append($('<option></option>').val(option.ddid).html(option.ddname));
        });

        var dbmodelval = val;
        ddl.val(dbmodelval).attr("selected", "selected");

        $(ddl).selectpicker({
            liveSearch: true,
            liveSearchPlaceholder: 'search...',
            liveSearchStyle: 'contains',
        });
        $(ddl).selectpicker('refresh');
    });
   
}

function FillDDProject(val) {
    $.getJSON('/Project/GetAll', function (result) {
        var ddl = $('#projectid');

        ddl.empty();
        blankDdl(ddl);
        $.each(result.data, function (id, option) {
            ddl.append($('<option></option>').val(option.projectid).html(option.projectname));
        });

        var dbmodelval = val;
        ddl.val(dbmodelval).attr("selected", "selected");

        $(ddl).selectpicker({
            liveSearch: true,
            liveSearchPlaceholder: 'search...',
            liveSearchStyle: 'contains',
        });
        $(ddl).selectpicker('refresh');
    });
}

function FillDDDepartment(val) {
    $.getJSON('/Department/GetAll', function (result) {
        var ddl = $('#departmentid');

        ddl.empty();
        blankDdl(ddl);
        $.each(result.data, function (id, option) {
            ddl.append($('<option></option>').val(option.departmentid).html(option.departmentname));
        });

        var dbmodelval = val;
        ddl.val(dbmodelval).attr("selected", "selected");

        $(ddl).selectpicker({
            liveSearch: true,
            liveSearchPlaceholder: 'search...',
            liveSearchStyle: 'contains',
        });
        $(ddl).selectpicker('refresh');
    });
}
function blankDdl(ddltobblank) {
    ddltobblank.html('');
    ddltobblank.append($('<option></option>').val('').html("--Select--"));
}

function bindForm1(dialog, modalcontent, modalname) {

    $('form', dialog).submit(function () {

        var $form = $(this);
        $.validator.unobtrusive.parse($form);
        $form.validate();
        if (!$form.valid()) {
            return false;
        }


        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                $(modalcontent).html(result);
            },
            error: function (e) {
            }
        });
        return false;
    });
}