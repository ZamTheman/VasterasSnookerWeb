var allGames;
var allPlayers;
var playerDict;

// Eventhandler for tablerows
$(document).ready(function () {
    getAllPlayers();
})

function getAllPlayers() {
    $.ajax({
        type: 'post',
        url: '/Games/GetAllPlayers',
        dataType: 'json'
    }).success(function (data2) {
        allPlayers = data2;
        createPlayerDict();
        getAllGames();
    }).error(function (data2) {
        alert("Did Not Work");
    });
}

function getAllGames() {
    $.ajax({
        type: 'post',
        url: '/Games/GetAllGames',
        dataType: 'json'
    }).success(function (data) {
        allGames = data;
        updateGrid(1);
    }).error(function (data) {
        alert("Did Not Work");
    });
}

function createPlayerDict() {
    playerDict = {};
    for (var i = 0; i < allPlayers.length; i++) {
        playerDict[allPlayers[i].Id] = allPlayers[i].Name;
    }
}

function updateGrid(filterId) {
    var html = "";
    html += "<table class='table table-hover'>";
    html += "<thead><tr>";
    html += "<th>Datum</th>";
    html += "<th>Vinnare</th>";
    html += "<th>Spelare 1</th>";
    html += "<th>Spelare 2</th>";
    html += "<tbody>";
    for (var i = 0; i < allGames.length; i++) {
        var gameRowId = "gameRowId" + allGames[i].Id;
        html += "<tr id='" + gameRowId + "' class='selectableTableRow'>";
        html += "<td>" + new Date(parseInt(allGames[i].Datum.substr(6))).toLocaleString().slice(0,10) + "</td>";
        html += "<td>" + playerDict[allGames[i].Vinnare] + "</td>";
        html += "<td>" + playerDict[allGames[i].Spelare1] + "</td>";
        html += "<td>" + playerDict[allGames[i].Spelare2] + "</td>";
        html += "</tr>";
        var expandableRowId = "expandableGameRowId" + allGames[i].Id;
        html += "<tr id='" + expandableRowId + "' class='hidden expandableGameRow'><td colspan='4'><div class='inTableContent'>";
        html += "<h4>Högsta serie av " + playerDict[allGames[i].Spelare1] + ": " + allGames[i].HogstaSerieSpelare1 + "</h4>";
        html += "<h4>Högsta serie av " + playerDict[allGames[i].Spelare2] + ": " + allGames[i].HogstaSerieSpelare2 + "</h4>";
        html += "<table class='table'>";
        html += "<thead><tr>";
        html += "<th>Frame</th>";
        html += "<th>" + playerDict[allGames[i].Spelare1] + "</th>";
        html += "<th>" + playerDict[allGames[i].Spelare2] + "</th>";
        html += "<tbody>";
        var frameResultsAsArray = allGames[i].FrameResultat.split(';');
        var resultCounter = 0;
        for (var j = 0; j < allGames[i].AntalFrames; j++) {
            html += "<tr>";
            html += "<td>#" + (j+1) + "</td>";
            html += "<td>" + frameResultsAsArray[resultCounter] + "</td>";
            html += "<td>" + frameResultsAsArray[resultCounter + 1] + "</td>";
            html += "</tr>";
            resultCounter += 2;
        };
        html += "</tbody></table>";
        html += "<h4>Referat:</h4>";
        html += "<blockquote class='blockquote'>" + allGames[i].MatchReferat + "</blockquote>";
        html += "</div></td></tr>";
    }
    html += "</tbody></table>";
    $('#allGamesId').html(html);

    $('.selectableTableRow').click(function (e) {
        gameSelected(e.currentTarget.id);
    })

    $('.expandableGameRow').click(function (e) {
        hideGame(e.currentTarget.id);
    })
}

function gameSelected(row) {
    console.log("#expandableG" + row.substr(1));
    $("#expandableG" + row.substr(1)).toggleClass("hidden");
}

function hideGame(row) {
    console.log("#"+row);
    $("#" + row).toggleClass("hidden");
}