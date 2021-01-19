// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    GetSizeOfTable();
    AddNewShift();

    function GetSizeOfTable() {
        let rowCount = $('#table tbody tr').length;

        if (rowCount > 4) {
            $(".table").addClass("overflow-y");
        }
    }

    function AddNewShift() {
        $("#AddRow").click(function () {
            let rowCount = $('#table tbody tr').length;
            $("#table").scrollTop($("#table")[0].scrollHeight);
            SetEvenClassOnTable("#table tbody tr");

            if (rowCount > 4) {
                $(".table").addClass("overflow-y");
            }
        })
    }

    //for (let i = 0; i < $(".table").length; i++) {
    //    let tableID = "#" + $(".table").eq(i).attr("id");

    //    console.log(tableID);

    //    SetEvenClassOnTable(tableID);
    //}

    //for (let i = 0) {

    //}

    SetEvenClassOnTable("#table");
    SetEvenClassOnTable("#gas-table");
    SetEvenClassOnTable("#device-table");
    SetEvenClassOnTable("#gas-device-difference-table");
    SetEvenClassOnTable("#heat-content-table");
    SetEvenClassOnTable("#material-balance-coming-table");
    SetEvenClassOnTable("#material-balance-consumption-table");
    SetEvenClassOnTable("#w-table");
    SetEvenClassOnTable("#heat-balance-coming-table");
    SetEvenClassOnTable("#heat-balance-consumption-table");
    SetEvenClassOnTable("#heat-balance-consumption-table");
    SetEvenClassOnTable("#oxides-percent-table");

    function SetEvenClassOnTable(table) {
        let rowCount = $(`${table} tbody tr`).length;

        for (let i = 0; i < rowCount; i++) {
            if (i % 12 == 0) {
                let rowSpanValue = (i / 12) + 1;

                if (rowSpanValue % 2 == 0) {
                    $(`${table} tbody tr td:first-child`).eq(i).addClass("even-row");
                }
            }
        }
    }

    //let rowCount = $(`${table} tbody tr`).length;

    //for (let i = 0; i < rowCount; i++) {
    //    if (i % 12 == 0) {
    //        let rowSpanValue = (i / 12) + 1;

    //        if (rowSpanValue % 2 == 0) {
    //            for (let j = 0; j < 12; j++) {
    //                $(this).addClass("test");
    //                $(`${table} tbody tr`).eq(i + j).addClass("even-row");
    //            }
    //        }
    //    }
    //}


    $('.table-result').hover(function () {
        $('.table-result').addClass("no-scroll");
        $(this).removeClass("no-scroll");
        //console.log($(this).attr("id"));
    });

    $(".table-result").on('scroll', function () {
        let gavno = $(this).scrollTop();
        $(".table-result.no-scroll").scrollTop(gavno);       
    })

    let kiloPerHour = true;
    let percent = false;

    $("#materialBalance").click(function () {
        console.log("click")
        kiloPerHour = !kiloPerHour;

        if (kiloPerHour) {
            $("#materialBalance").text("Показать на тонну расплава");
            $(".material-balance-heading").text("Материальный баланс (приход), кг/ч");
            $("#material-balance-coming-table tbody tr.material-balance-tonn").css("display", "none");
            $("#material-balance-coming-table tbody tr.material-balance-hour").css("display", "table-row");
        } else {
            $("#materialBalance").text("Показать в час");
            $(".material-balance-heading").text("Материальный баланс (приход), кг/т");
            $("#material-balance-coming-table tbody tr.material-balance-hour").css("display", "none");
            $("#material-balance-coming-table tbody tr.material-balance-tonn").css("display", "table-row");
        }
    });

    $("#percent").click(function () {

        percent = !percent;
    });

})