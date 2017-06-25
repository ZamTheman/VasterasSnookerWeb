var allGames;
var allPlayers;
var playerDict;
var viewList = [];

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
        filterClosed();
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

function filterClosed() {

    updateGrid(0, 0, 0, 0);
    var html = "";
    html += "<div class='form-group clearfix'>";
    html += "<button class='btn btn-primary' id='saveGameBtnId' onclick='filterOpen()'><i class='fa fa-angle-double-down inButtonIcon' id='filterOpenIconId'></i> Filter</button>";
    html += "</div>";
    
    $('#filterGamesId').html(html);

    $('#filterOpenIconId').click(function () {
        filterOpen();
    })
}

function filterOpen() {
    var html = "";
    html += "<div class='row'>";

    html += "<div class='form-group col-sm-3 col-xs-6'>";
    html += "<button class='btn btn-primary' id='saveGameBtnId' onclick='filterClosed()'><i class='fa fa-angle-double-up inButtonIcon' id='filterOpenIconId'></i> Filter</button>";
    html += "</div>";

    html += "<div class='form-group col-sm-3 col-xs-6'>";
    html += "<label for='Spelare'>Med spelare</label>";
    html += "<select name='Spelare' id='playerFilterId' class='form-control filterChanged' selected='Välj spelare'>";
    html += "<option value='alla'>Alla</option>";
    for (var i = 0; i < allPlayers.length; i++) {
        html += "<option value='" + allPlayers[i].Id + "'>" + allPlayers[i].Name + "</option>";
    };
    html += "</select></div>";

    var todaysDate = getTodaysDateAsFormatedString();

    html += "<div class='form-group col-sm-3 col-xs-6'>";
    html += "<label for='Startdatum'>Startdatum</label>";
    html += "<input type='date' name='Startdatum' id='startDatumId' class='form-control filterChanged' value='2017-01-01' min='2017-01-01' max='" + todaysDate + "'>";
    html += "</div>";

    html += "<div class='form-group col-sm-3 col-xs-6'>";
    html += "<label for='Slutdatum'>Slutdatum</label>";
    html += "<input type='date' name='Slutdatum' id='slutDatumId' class='form-control filterChanged' value='" + todaysDate + "' min='2017-01-01' max='" + todaysDate + "'>";
    html += "</div>";
    
    html += "</div>";

    $('#filterGamesId').html(html);

    $('.filterChanged').change(function () {
        updateGrid(1, $('#playerFilterId').val(), $('#startDatumId').val(), $('#slutDatumId').val());
    })
}

function updateGrid(filterType, playerId, startDate, endDate) {
    viewList = allGames;
    switch (filterType) {
        case 0:
            break;
        case 1:
            viewList = filterByPlayerId(playerId, startDate, endDate);
            break;
        default:
            break;
    }
    
    viewList.sort(function (a, b) { return (a.Datum < b.Datum) ? 1 : ((b.Datm < a.Datum) ? -1 : 0); }); 

    var html = "";
    html += "<table class='table table-hover'>";
    html += "<thead><tr>";
    html += "<th>Datum</th>";
    html += "<th>Vinnare</th>";
    html += "<th>Spelare 1</th>";
    html += "<th>Spelare 2</th>";
    html += "<tbody>";
    for (var i = 0; i < viewList.length; i++) {
        var gameRowId = "gameRowId" + viewList[i].Id;
        html += "<tr id='" + gameRowId + "' class='selectableTableRow'>";
        html += "<td>" + new Date(parseInt(viewList[i].Datum.substr(6))).toLocaleString().slice(0,10) + "</td>";
        html += "<td>" + playerDict[viewList[i].Vinnare] + "</td>";
        html += "<td>" + playerDict[viewList[i].Spelare1] + "</td>";
        html += "<td>" + playerDict[viewList[i].Spelare2] + "</td>";
        html += "</tr>";
        var expandableRowId = "expandableGameRowId" + viewList[i].Id;
        html += "<tr id='" + expandableRowId + "' class='hidden expandableGameRow'><td colspan='4'><div class='inTableContent'>";
        html += "<h4>Högsta serie av " + playerDict[viewList[i].Spelare1] + ": " + viewList[i].HogstaSerieSpelare1 + "</h4>";
        html += "<h4>Högsta serie av " + playerDict[viewList[i].Spelare2] + ": " + viewList[i].HogstaSerieSpelare2 + "</h4>";
        html += "<table class='table'>";
        html += "<thead><tr>";
        html += "<th>Frame</th>";
        html += "<th>" + playerDict[viewList[i].Spelare1] + "</th>";
        html += "<th>" + playerDict[viewList[i].Spelare2] + "</th>";
        html += "<tbody>";
        var frameResultsAsArray = viewList[i].FrameResultat.split(';');
        var resultCounter = 0;
        for (var j = 0; j < viewList[i].AntalFrames; j++) {
            html += "<tr>";
            html += "<td>#" + (j+1) + "</td>";
            html += "<td>" + frameResultsAsArray[resultCounter] + "</td>";
            html += "<td>" + frameResultsAsArray[resultCounter + 1] + "</td>";
            html += "</tr>";
            resultCounter += 2;
        };
        html += "</tbody></table>";
        html += "<h4>Referat:</h4>";
        html += "<blockquote class='blockquote'>" + viewList[i].MatchReferat + "</blockquote>";
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

function filterByPlayerId(playerId, startDate, endDate) {
    var returnList = [];
    for (var i = 0; i < allGames.length; i++) {
        var gameDate = new Date(parseInt(allGames[i].Datum.substr(6))).toLocaleString().slice(0, 10)
        if (((playerId == allGames[i].Spelare1 || playerId == allGames[i].Spelare2) || playerId == "alla") && startDate <= gameDate && endDate >= gameDate) {
            returnList.push(allGames[i]);
        }
    }
    return returnList;
}

function getTodaysDateAsFormatedString() {
    var date = new Date();
    var month = date.getMonth() + 1;
    if (month < 10)
        month = "0" + month;
    var day = date.getDate();
    if (day < 10)
        day = "0" + day;
    return date.getFullYear() + "-" + month + "-" + day;
}