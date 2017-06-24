function getPlayer(playerId) {
    var jsonToSend = JSON.stringify({ 'Id': playerId });
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: '/Player/PlayerDetails',
        data: jsonToSend,
        dataType: "html"
    }
    ).success(function (data) {
        $("#playerDetails").html(data);
    }).error(function (data) {
        alert("Did Not Work");
    });
}

$(".list-group-item").click(function (e) {
    $(".list-group-item").removeClass("active");
    $(this).addClass("active");
})

function createPlayer() {
    $.ajax({
        type: "POST",
        url: '/Player/CreatePlayerPartial',
        dataType: "html"
    }
    ).success(function (data) {
        $("#playerDetails").html(data);
    }).error(function (data) {
        alert("Did Not Work");
    });
}