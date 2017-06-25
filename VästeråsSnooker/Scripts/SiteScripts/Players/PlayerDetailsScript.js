var allPlayers;
var playerDict;

$(document).ready(function () {
    $.ajax({
        type: 'post',
        url: '/Games/GetAllPlayers',
        dataType: 'json'
    }).success(function (data2) {
        allPlayers = data2;
        createPlayerDict();
        getLatestGames();
    }).error(function (data2) {
        alert("Could not get players");
    });
})

function getLatestGames() {
    $.ajax({
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ 'Id': $(".toGetId")[0].id, 'Name': 'NotUsed' }),
        url: '/Player/GetLatestGames',
        dataType: 'json'
    }).success(function (data) {
        createGamesTable(data, $(".toGetId")[0].id);
        getHighestBreaks();
    }).error(function (data2) {
        alert("Could not get games");
    });
}

function getHighestBreaks() {
    $.ajax({
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify({ 'Id': $(".toGetId")[0].id, 'Name': 'NotUsed' }),
        url: '/Player/GetHighestBreaks',
        dataType: 'json'
    }).success(function (data3) {
        createTopBreaksTable(data3, $(".toGetId")[0].id);
    }).error(function (data3) {
        alert("Could not get highest breaks");
    });
}

function createPlayerDict() {
    playerDict = {};
    for (var i = 0; i < allPlayers.length; i++) {
        playerDict[allPlayers[i].Id] = allPlayers[i].Name;
    }
}

function createTopBreaksTable(list, playerId) {
    var html = "";
    html += "<table class='table'>";
    html += "<thead><tr>";
    html += "<th>Datum</th>";
    html += "<th>Poäng</th>";
    html += "<tbody>";
    for (var i = 0; i < list.length; i++) {
        html += "<tr>";
        html += "<td>" + new Date(parseInt(list[i].Datum.substr(6))).toLocaleString().slice(0, 10) + "</td>";
        html += "<td>" + list[i].Serie + "</td>";
        html += "</tr>";
    }
    html += "</tbody></table>";
    $('#highestBreaksId').html(html);
}

function createGamesTable(list, playerId) {
    var html = "";
    html += "<table class='table'>";
    html += "<thead><tr>";
    html += "<th>Datum</th>";
    html += "<th>Resultat</th>";
    html += "<th>Moståndare</th>";
    html += "<th>Högsta serie</th>";
    html += "<tbody>";
    for (var i = 0; i < list.length; i++) {
        html += "<tr>";
        html += "<td>" + new Date(parseInt(list[i].Datum.substr(6))).toLocaleString().slice(0, 10) + "</td>";
        var result = list[i].Vinnare == playerId ? "Vinst" : "Förlust";
        html += "<td>" + result + "</td>";
        var opponent;
        var highestBreak;
        if (list[i].Spelare1 == playerId) {
            opponent = playerDict[list[i].Spelare2];
            highestBreak = list[i].HogstaSerieSpelare1;
        }
        else {
            opponent = playerDict[list[i].Spelare1];
            highestBreak = list[i].HogstaSerieSpelare2;
        }
        html += "<td>" + opponent + "</td>";
        html += "<td>" + highestBreak + "</td>";
        html += "</tr>";
    }
    html += "</tbody></table>";
    $('#latestGamesId').html(html);
}
