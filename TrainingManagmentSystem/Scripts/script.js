$(document).ready(function () {

    $("#exp").change(function () {
        var start = new Date($('#end_date').val());
        if (start == 'Invalid Date') {
            start = Date.parse($('#end_date').val().toString().replace(/([0-9]+)\/([0-9]+)/, '$2/$1'));
            start = new Date(start);
        }
        var duration = $("#exp").val();
        var iNum = parseInt(duration);
        var day = ("0" + start.getDate()).slice(-2);
        var month = ("0" + (start.getMonth() + 1)).slice(-2);
        var year = start.getFullYear() + iNum;
        var today = (day) + "/" + (month) + "/" +  (year);

        $('#exp_date').val(today);
    });    
    

    $("#sector").select2({
        placeholder: "",
        theme: "classic",
        ajax: {
            url: "/Training/GetSectorList",
            dataType: "json",
            data: function (params) {
                return {
                    searchTerm: params.term
                };
            },
            processResults: function (data, params) {
                return {
                    results: data
                };
            }
        }
    });

    $("#subsector").select2();

    $("#EmployeeIDs").select2();

    $("#sector").on("change", function () {
        var sectors = $(this).val();

        if (sectors != null) {
            $.ajax({
                url: "/Training/GetSubSectorList?sectors=" + sectors,
                dataType: 'json',
                type: 'post',
                success: function (data) {
                    $("#subsector").empty();
                    $.each(data, function (index, row) {
                        $("#subsector").append("<option value='" + row.SubSectorID + "'>" + row.SubSectortype + "</option>")
                    });
                }
            });
        }
    });    

    $("#btnReset").click(function () {
        $("#sector").val(null).trigger('change');
    });

    $("#btnReset2").click(function () {
        $("#subsector").val(null).trigger('change');
    });

    $.validator.addMethod('date', function (value, element) {
        if (this.optional(element)) {
            return true;
        }
        var valid = true;
        try {
            $.datepicker.parseDate('dd/mm/yy', value);
        }
        catch (err) {
            valid = false;
        }
        return valid;
    });

    $('.datepicker').datepicker({ dateFormat: 'dd/mm/yy' });
});