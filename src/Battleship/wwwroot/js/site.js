function fireTorpedo() {

    var positionElement = document.getElementById("position");
    $('#fireButton').prop('disabled', true);
    var position = {
        torpedoPosition: positionElement.value
    }
    positionElement.disabled = true;    

    fetch('/api/game', {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(position)
    })
    .then(handleErrors)
    .then(function(response) { return response.json(); })
    .then(function (response) {
        if (response.status === "End") {
            endGame();
        } else {
            drawBoard(response.displayBoard);
            setMessage(response.message);
            enableControls();
        }
    })
    .catch(function(error) {
        console.log("error", error);
        setMessage(error.message);
        enableControls();
    });

}

function enableControls() {
    var positionElement = document.getElementById("position");
    positionElement.disabled = false;
    positionElement.value = '';
    $('#fireButton').prop('disabled', false);
}

function endGame() {
    $('#grid-container').empty();
    $("#game-panel").addClass('end-game-panel').removeClass('game-panel');

    var canvas = document.getElementById("grid-container");
    canvas.innerHTML = '<span class="title-font">GAME OVER</span>';
}

function handleErrors(response) {
    if (!response.ok) {
        if (response.status >= 500) {
            throw Error('Something went wrong, please try again');
        } else {
            throw Error(response.statusText);
        }        
    }
    return response;
}


function setMessage(message) {
    var messageContainer = document.getElementById("message");
    messageContainer.innerHTML = '<span class="action-message-anim">' + message + '</span>';
}

function getInitialBoard() {
    fetch('/api/game', {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
    })
    .then(handleErrors)
    .then(function (response) { return response.json(); })
    .then(function (response) {
        drawBoard(response.displayBoard);
    })
    .catch(function (error) {
        console.log("error", error);
    });
}

function getCellIcon(cellStatus) {
    if (cellStatus == "Hit") {
        return 'fa-fire orange-icon'
    } else if (cellStatus == "Miss") {
        return 'fa-times red-icon'
    } else if (cellStatus == "Sink") {
        return 'fa-fire orange-icon'
    }
}

function drawBoard(cells) {
    var canvas = document.getElementById("grid-container");

    var gridHtml = '<div id="grid" class="grid">',
        cell_html = '',
        i = 0,
        j = 0;

    cells.map(function (cellRow) {
        var rowHtml = '';

        cellRow.map(function (cell) {
            var cellIcon = getCellIcon(cell);
            rowHtml += '<div class="grid-cell"><i class="fa ' + cellIcon + '" aria-hidden="true"></i></div>';
        })

        gridHtml += '<div class="grid-row">' + rowHtml + '</div>';
    })

    gridHtml += '</div>';
    canvas.innerHTML = gridHtml;
}

$('document').ready(function () {
    getInitialBoard();    
});