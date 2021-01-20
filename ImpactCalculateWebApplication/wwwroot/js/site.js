// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function submitForm() {
    $("#inputForm").submit();
}

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

            SetEvenClassOnTable("#table");

            if (rowCount > 4) {
                $(".table").addClass("overflow-y");
            }
        })

        $("#AddRow").click(function () {
            let lastTrowInputs = $('#table tr:last').find(".gas-content");;

            let summ = 0;

            for (let i = 0; i < lastTrowInputs.length; i++) {
                let value = lastTrowInputs.eq(i).val().replace(',', '.');
                summ += parseFloat(value);
            }

            let summFixed = summ.toFixed(9) == 100;

            if (!summFixed) {
                lastTrowInputs.removeClass("is-valid");
                lastTrowInputs.addClass("is-invalid");
            } else {
                lastTrowInputs.removeClass("is-invalid");
                lastTrowInputs.addClass("is-valid");
            }

            BlockResultButton(!summFixed);
        })
    }

    inputsValidate();

    function inputsValidate() {

        for (let i = 0; i < $("#table tbody tr td input").length; i++) {
            let inputs = $("#table tbody tr td input").eq(i);
            let value = parseFloat(inputs.val().replace(',', '.'));

            if (value < 0) {
                inputs.addClass("invalid")
            }
        }

        $("#table tbody tr td input").keyup(function () {
            this.value = this.value.replace(/[^0-9\.,-]/g, '');

            let input = $(this);
            let value = parseFloat(input.val().replace(',', '.'));

            if (value < 0) {
                input.addClass("is-invalid");
                BlockResultButton(true);
            } else {
                input.removeClass("is-invalid");
                BlockResultButton(false);
            }
        })

        $(".table-modal tbody tr td input").keyup(function () {
            this.value = this.value.replace(/[^0-9\.,-]/g, '');
        })

        for (let i = 0; i < $(".gas-content").length; i++) {
            let gasContentInputs = $(".gas-content").eq(i).parent("td").parent("tr").find(".gas-content");

            let summ = 0;

            for (let i = 0; i < gasContentInputs.length; i++) {
                let value = gasContentInputs.eq(i).val().replace(',', '.');
                summ += parseFloat(value);
            }

            summFixed = summ.toFixed(9) == 100;

            if (!summFixed) {
                gasContentInputs.removeClass("is-valid");
                gasContentInputs.addClass("is-invalid");
            } else {
                gasContentInputs.removeClass("is-invalid");
                gasContentInputs.addClass("is-valid");
            }
           
            if (!$("#Calculate").prop("disabled")) {
                BlockResultButton(!summFixed);
            }
        }

        $(".gas-content").keyup(function () {

            let gasContentInputs = $(this).parent("td").parent("tr").find(".gas-content");

            let summ = 0;

            for (let i = 0; i < gasContentInputs.length; i++) {
                let value = gasContentInputs.eq(i).val().replace(',', '.');
                summ += parseFloat(value);
            }

            let summFixed = summ.toFixed(9) == 100;

            if (!summFixed) {
                gasContentInputs.removeClass("is-valid");
                gasContentInputs.addClass("is-invalid");
            } else {
                gasContentInputs.removeClass("is-invalid");
                gasContentInputs.addClass("is-valid");
            }

            BlockResultButton(!summFixed);
        })
    }

    function BlockResultButton(parameter) {
        $("#Calculate").prop("disabled", parameter);
    }


    SetEvenClassOnTable("#table");
    SetEvenClassOnTable("#gas-table");
    SetEvenClassOnTable("#device-table");
    SetEvenClassOnTable("#gas-device-difference-table");
    SetEvenClassOnTable("#heat-content-table");

    
    //SetEvenClassOnTable("#material-balance-coming-table");

    // Материальный баланс (приход)
    SetEvenClassOnTable("#material-balance-coming-table-hour");
    SetEvenClassOnTable("#material-balance-coming-table-tonn");
    SetEvenClassOnTable("#material-balance-coming-table-percent");

    // Материальный баланс (расход)
    SetEvenClassOnTable("#material-balance-consumption-table-hour");
    SetEvenClassOnTable("#material-balance-consumption-table-tonn");
    SetEvenClassOnTable("#material-balance-consumption-table-percent");

    SetEvenClassOnTable("#w-table");

    // Тепловой баланс (приход)
    SetEvenClassOnTable("#heat-balance-coming-table-hour");
    SetEvenClassOnTable("#heat-balance-coming-table-tonn");
    SetEvenClassOnTable("#heat-balance-coming-table-percent");


    // Материальный баланс (расход)
    SetEvenClassOnTable("#heat-balance-consumption-table-hour");
    SetEvenClassOnTable("#heat-balance-consumption-table-tonn");
    SetEvenClassOnTable("#heat-balance-consumption-table-percent");


    SetEvenClassOnTable("#oxides-percent-table");

    function SetEvenClassOnTable(table) {
        let rowCount = $(`${table} tbody tr`).length;

        for (let i = 0; i < rowCount; i++) {
            if (i % 12 == 0) {
                let rowSpanValue = (i / 12) + 1;

                if (rowSpanValue % 2 == 0) {
                    for (let j = 0; j < 12; j++) {
                        $(this).addClass("test");
                        $(`${table} tbody tr`).eq(i + j).addClass("even-row");
                    }
                }
            }
        }
    }

    //let rowCount = $(`${table} tbody tr`).length;

    //for (let i = 0; i < rowCount; i++) {
    //    if (i % 12 == 0) {
    //        let rowSpanValue = (i / 12) + 1;

    //        if (rowSpanValue % 2 == 0) {
    //            $(`${table} tbody tr td:first-child`).eq(i).addClass("even-row");
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

    //

    let kiloPerHour = true;

    $("#balanceConfigure").click(function () {

        percent = false;
        kiloPerHour = !kiloPerHour;

        $(".table-result").scrollTop(0);

        if (kiloPerHour) {
            $("#balanceConfigure").text("Показать на тонну");
            $(".material-balance-coming-heading").text("Материальный баланс (приход), кг/ч");
            $(".material-balance-consumption-heading").text("Материальный баланс (расход), кг/ч");

            $(".heat-balance-coming-heading").text("Тепловой баланс (приход), кДж");
            $(".heat-balance-consumption-heading").text("Тепловой баланс (расход), кДж");

            $(".table-balance-tonn").addClass("table-hide");
            $(".table-balance-hour").removeClass("table-hide");

            $(".table-balance-percent").addClass("table-hide");
        } else {
            $("#balanceConfigure").text("Показать в час");
            $(".material-balance-coming-heading").text("Материальный баланс (приход), кг/т");
            $(".material-balance-consumption-heading").text("Материальный баланс (расход), кг/т");

            $(".heat-balance-coming-heading").text("Тепловой баланс (приход), кДж/т");
            $(".heat-balance-consumption-heading").text("Тепловой баланс (расход), кДж/т");

            $(".table-balance-hour").addClass("table-hide");
            $(".table-balance-tonn").removeClass("table-hide");

            $(".table-balance-percent").addClass("table-hide");
        }
    });

    //

    let percent = false;

    $("#percent").click(function () {

        $(".table-result").scrollTop(0);

        percent = !percent;

        if (percent) {
            $(".material-balance-coming-heading").text("Материальный баланс (приход), %");
            $(".material-balance-consumption-heading").text("Материальный баланс (расход), %");

            $(".heat-balance-coming-heading").text("Тепловой баланс (приход), %");
            $(".heat-balance-consumption-heading").text("Тепловой баланс (расход), %");

            $(".table-balance-tonn").addClass("table-hide");
            $(".table-balance-hour").addClass("table-hide");

            // тут получаеца не надо "на час" надо просто у таблицы процента убрать hide окс (и получаеца что таблиц % не 2 а 1 на каждую жыжу)
            $(".table-balance-percent").removeClass("table-hide");
        } else {

            $(".table-balance-percent").addClass("table-hide");

            if (kiloPerHour) {
                //$("#balanceConfigure").text("Показать на тонну расплава");
                $(".material-balance-coming-heading").text("Материальный баланс (приход), кг/ч");
                $(".material-balance-consumption-heading").text("Материальный баланс (расход), кг/ч");

                $(".heat-balance-coming-heading").text("Тепловой баланс (приход), кг/ч");
                $(".heat-balance-consumption-heading").text("Тепловой баланс (расход), кг/ч");

                $(".table-balance-tonn").addClass("table-hide");
                $(".table-balance-hour").removeClass("table-hide");
            } else {
                //$("#balanceConfigure").text("Показать в час");
                $(".material-balance-coming-heading").text("Материальный баланс (приход), кг/т");
                $(".material-balance-consumption-heading").text("Материальный баланс (расход), кг/т");

                $(".heat-balance-coming-heading").text("Тепловой баланс (приход), кг/т");
                $(".heat-balance-consumption-heading").text("Тепловой баланс (расход), кг/т");

                $(".table-balance-hour").addClass("table-hide");
                $(".table-balance-tonn").removeClass("table-hide");
            }
        }
    });

})