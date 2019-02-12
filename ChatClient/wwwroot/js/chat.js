//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
var roomId;

function buildConnection(someRoomId) {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("http://" + host + "/hubs/chat", { accessTokenFactory: () => TOKEN })
        .build();

    roomId = someRoomId;

    connection.on("ReceiveMessage", function (messageDto) {
        var li = document.createElement("li");
        li.textContent = messageDto;
        document.getElementById("messagesList").appendChild(li);
    });

    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
        connection.invoke("JoinGameChat", someRoomId);
    }).catch(err => console.error(err.toString()));

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var message = document.getElementById("messageInput").value;
        var dto = { roomId: roomId, body: message };

        connection.invoke("SendMessage", dto).catch(function (err) {
            console.error(err.toString());
        });
        event.preventDefault();
    });
}

function showChatForm() {
    document.getElementById("chatform").style.display = "block";
}

function hideChatForm() {
    document.getElementById("chatform").style.display = "none";
}