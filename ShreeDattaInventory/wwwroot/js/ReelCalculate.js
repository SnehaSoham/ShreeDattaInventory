$(function () {
    $("#calReel").click(function () {
        var height = $("#height").val();
        var width = $("#width").val();
        var gsm = $("#gsm").val();
        var totPaper = ((height * width * gsm) / 1525);
        $("#totPaper").val(totPaper);
        $("#calReelText").css("visibility", "visible");
        $("#totPaper").css("visibility", "visible");
    })
});