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

            if (rowCount > 4) {
                $(".table").addClass("overflow-y");
            }
        })
    }

    //$("#AddRow").click(function () {

    //    //let block = document.querySelector(".table");


    //    $(".table").scrollTop($(".table")[0].scrollHeight);

    //    if ($(".table").height() > 400) {
    //        $(".table").addClass("overflow-y");
    //    }
    //})

    //$(window).scroll(function () {
    //    $(".head").css("height", 200 + "px");
    //})

})