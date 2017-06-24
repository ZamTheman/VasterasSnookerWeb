var player1Selected = false;
var player2Selected = false;
var player1;
var player2;

$("#spelare1SelectorId").change(function () {
    if (this.value != "") {
        player1Selected = true;
    }
    else {
        player1Selected = false;
    }
    if (player2Selected) {
        displayHighestScoresDiv();
    }
});

$("#spelare2SelectorId").change(function () {
    if (this.value != "") {
        player2Selected = true;
    }
    else {
        player2Selected = false;
    }
    if (player1Selected) {
        displayHighestScoresDiv();
    }
});

function displayHighestScoresDiv() {
    player1 = $("#spelare1SelectorId option:selected").text();
    player2 = $("#spelare2SelectorId option:selected").text();
    $("#hogstaSerie1Id").text("Högsta serie, " + player1);
    $("#hogstaSerie2Id").text("Högsta serie, " + player2);
    $("#gameResultRightDivId").css("visibility", "visible");
    $("#gameResultRightDivId").css("display", "block");
}

$("#antalFramesId").change(function () {
    $("#frameResultsDivId").html("");
    for (var i = 0; i < this.value; i++) {
        $("#frameResultsDivId").append("<div class='row top-buffer form-group'><div class='col-sm-2 margin-top-20'><h4>Frame " + (i + 1) + ": </h4></div><div class='col-sm-5'><label for='plr1Frm" + i + "Id'>Poäng, " + player1 + "</label><input class='form-control frameValuePicker' type='number' value='0' id='plr1Frm" + i + "Id'></input></div><div class='col-sm-5'><label for='plr2Frm" + i + "Id'>Poäng, " + player2 + "</label><input class='form-control frameValuePicker' type='number' value='0' id='plr2Frm" + i + "Id'></input></div></div>");
    }
    $("#frameResultsDivId").append("<div class='row top-buffer form-group'><label for='matchRefId'>Referat</label><textarea class='form-control' rows='5' id='matchRefId' placeholder='Var matchen värd en kort referat?'></textarea></div>");
    $("#frameResultsDivId").addClass("row top-buffer");
    addFrameValueEventHandler();
    $('#saveGameBtnId').removeClass("disabled");
    $('#saveGameBtnId').addClass("disabled");
})

function saveGame() {
    var Datum = $("#datePickerId").val();
    var Spelare1 = $("#spelare1SelectorId").val();
    var Spelare2 = $("#spelare2SelectorId").val();
    var HogstaSerieSpelare1 = $("#HogstaSerieSpelare1").val();
    var HogstaSerieSpelare2 = $("#HogstaSerieSpelare2").val();
    var AntalFrames = $("#antalFramesId").val();
    var FrameResultat = [];
    for (var i = 0; i < AntalFrames; i++) {
        FrameResultat.push($("#plr1Frm" + i + "Id").val());
        FrameResultat.push($("#plr2Frm" + i + "Id").val());
    }
    var MatchReferat = $("#matchRefId").val();

    var jsonToSend = JSON.stringify({
        'Datum': Datum,
        'Spelare1': Spelare1,
        'Spelare2': Spelare2,
        'HogstaSerieSpelare1': HogstaSerieSpelare1,
        'HogstaSerieSpelare2': HogstaSerieSpelare2,
        'AntalFrames': AntalFrames,
        'FrameResultat': FrameResultat,
        'MatchReferat': MatchReferat
    });

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: '/Games/SaveGame',
        data: jsonToSend,
        dataType: "html"
    }
    ).success(function (data) {
        $('#addGameBtnId').removeClass("disabled");
        $('#CreateGameId').html("<div class='panel panel-success'><div class='panel-heading'>Sparat</div><div class='panel-body'>Matchen sparad</div><div class='panel-body'><button class='btn btn-default' onclick='hideInfo()'>Dölj</button></div></div>");
    }).error(function (data) {
        $('#addGameBtnId').removeClass("disabled");
        $('#CreateGameId').html("<div class='panel panel-danger'><div class='panel-header'>Problem</div><div class='panel-body'>Något gick fel. Kontrollera värdena och försök igen</div><div class='panel-body'><button class='btn btn-default' onclick='hideInfo()'>Dölj</button></div></div>");
    });
}

function addFrameValueEventHandler() {
    $('.frameValuePicker').change(function () {
        var allFrameValue = $('.frameValuePicker');
        var allHaveValues = true;
        for (var i = 0; i < allFrameValue.length; i += 2) {
            if ((allFrameValue[i].value < 1 && allFrameValue[i + 1].value < 1)) {
                $('#saveGameBtnId').removeClass("disabled");
                $('#saveGameBtnId').addClass("disabled");
                return;
            }   
        }
        $('#saveGameBtnId').removeClass("disabled");
    })
}


function cancelGameCreation() {
    $('#addGameBtnId').removeClass("disabled");
    $('#CreateGameId').html("");
}

function hideInfo() {
    $('#addGameBtnId').removeClass("disabled");
    $('#CreateGameId').html("");
    getAllPlayers();
}


