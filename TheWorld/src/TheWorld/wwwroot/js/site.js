//var ele = document.getElementById("username");
//ele.innerHTML = "Mr. Dukane";

//site.js
(function () {

    //var ele = $("#username");
    //ele.text("Mr. Shailnedra");

    //var main = $("#main");
    //main.on("mouseenter", function () {
    //    main.style = "background-color: #100;";
    //});

    //main.onmousereave = function () {
    //    main.style = "";
    //};

    //var menuItems = $("ul.menu li a");

    //menuItems.on("click", function () {
    //    var me = $(this);
    //    alert(me.text());
    //});


    //JQuery
    //var $sidebarAndWrapper = $("#sidebar, #wrapper");

    //$("#sidebarToggle").on("click", function () {
    //    $sidebarAndWrapper.toggleClass("hide-sidebar");
    //    if($sidebarAndWrapper.hasClass("hide-sidebar")){
    //        $(this).text("Show Sidebar");
    //    }else{
    //        $(this).text("Hide Sidebar");
    //    }
    //});

    //font awesome
    var $sidebarAndWrapper = $("#sidebar, #wrapper");
    var $icon = $("#sidebarToggle i.fa");

    $("#sidebarToggle").on("click", function () {
        $sidebarAndWrapper.toggleClass("hide-sidebar");
        if($sidebarAndWrapper.hasClass("hide-sidebar")){
            $icon.removeClass("fa-angle-left");
            $icon.addClass("fa-angle-right");
        } else {
            $icon.addClass("fa-angle-left");
            $icon.removeClass("fa-angle-right");
        }
    });


})();