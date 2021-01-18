// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    //$(".row-configure-button").click(function () {


    //    let wHeight = $(window).height();
    //    let bHeight = $("body").height();

    //    if (bHeight > wHeight) {
    //        $(".head").addClass("test");
    //    } else {
    //        $(".head").removeClass("test");
    //    }
    //})

    GetSizeOfTable();
    ScrollTopOnAddRow();

    function GetSizeOfTable() {
        var rowCount = $('#table tr').length;

        console.log(rowCount);

        if (rowCount > 6) {
            $(".table").addClass("overflow-y");
        }
    }

    function ScrollTopOnAddRow() {

        $("#AddRow").click(function () {
            $("#table").scrollTop($("#table")[0].scrollHeight);
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