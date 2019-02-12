var host = 'localhost:5000';
var TOKEN;

document.getElementById("authButton").addEventListener("click", function (event) {
    var username = document.getElementById("login").value;

    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'http://' + host + '/token', true);

    xhr.setRequestHeader('Accept', 'application/json,text/plain,*/*');
    xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
    xhr.setRequestHeader('Cache-Control', 'no-cache');

    xhr.onreadystatechange = function () {
        if (xhr.readyState === 4 && xhr.status === 200) {
            TOKEN = xhr.response;

            hideAuth();
            getRooms();
            showRoomsForm();
        }
    };

    xhr.send('grant_type=password' + '&username=' + username);

    event.preventDefault();
});

function hideAuth() {
    document.getElementById("authform").style.display = "none";
}