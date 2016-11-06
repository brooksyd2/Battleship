// Write your Javascript code.
function fireTorpedo() {

    var position = {
        torpedoPosition: document.getElementById("position").value
    }

    fetch('/api/fire', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(position)
        })
        .then(function(response) { return response.json(); })
        .then(function(response) {
            dispatch(receiveAuthDetails({ access_token: response.access_token, organisationId: response.organisationId, userName: response.userName, refresh_token: response.refresh_token, role: response.role, expires: response['.expires'] }));
        })
        .catch(function(error) {
            dispatch(requestAuthFailed('An error occured'));
            dispatch(logoff());
        });


} 


function drawBoard() {
    var grid = '<div id="grid" class="grid">',
        cell_html = '',
        i = 0,
        j = 0;

    for (; i < 10; i++) {
        cell_html += '<div class="grid-cell"></div>';
    }

    for (; j < 10; j++) {
        grid += '<div class="grid-row">' + cell_html + '</div>';
    }

    grid += '</div>';

    return grid;
}

$('document').ready(function () {

    var canvas = document.getElementById("grid-container");

    var gridHtml = drawBoard();

    canvas.innerHTML = gridHtml;
});