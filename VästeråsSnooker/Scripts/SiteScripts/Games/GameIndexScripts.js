$(document).ready(
    $('#addGameBtnId').click(function () {
        $.ajax({
            type: "GET",
            url: '/Games/CreateGame',
            dataType: "html"
        }).success(function (data) {
            $("#CreateGameId").html(data);
            $('#addGameBtnId').addClass("disabled");
            })
    })
)