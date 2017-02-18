//js file
(function () {
    var ele = $("#username");
    ele.text("Luciano");

    var man = $("#main");

    man.on("mouseenter",function () {
        man.style = "background-color: #888;";
    });

    man.on("mouseleave",function () {
        man.style = "";
    });

    var menuItems = $("ul.menu li a");
    menuItems.on("click", function () {
        var me = $(this);
        alert(me.text());
    });

})();