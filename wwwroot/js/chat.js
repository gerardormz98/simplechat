//---------------------------------------------------------
// SignalR logic
//---------------------------------------------------------
var connection = new signalR.HubConnectionBuilder().withUrl("/hubs/chat").build();

var currentUsername = $("#Username").val();
var currentChatRoom = $("#ChatRoom").val();

connection.start().then(function () {
    connection.invoke("JoinRoom", currentUsername, currentChatRoom);
});

connection.on("ReceiveMessage", function (username, message) {
    appendMessageToArea(username, message);
});

connection.on("UserEntered", function (username) {
    appendUserEnteredMessageToArea(username);
});

connection.on("UserLeft", function (username) {
    appendUserLeftMessageToArea(username);
});

//---------------------------------------------------------
// Page logic
//---------------------------------------------------------

var txtMessage = $("#txt-message");
var formSendMessage = $("#form-send-message");
var messagesArea = $("#messages-area");

formSendMessage.submit(function (e) {
    e.preventDefault();

    var message = txtMessage.val().trim();

    if (message.length > 0) {
        connection.invoke("SendMessage", message, currentChatRoom);
        appendMessageToArea(`${currentUsername} (me)`, message, true);
        txtMessage.val('');
    }

    txtMessage.focus();
});

function appendMessageToArea(username, message, isMine) {
    messagesArea.append(`
        <div class="message m-3 p-3 bg-white">
            <div class="message-user ${isMine ? "text-danger" : ""}">${username}</div>
            <div class="text-secondary">${message}</div>
        </div>
    `);

    messagesArea.scrollTop(messagesArea[0].scrollHeight);
}

function appendUserEnteredMessageToArea(username) {
    messagesArea.append(`
        <div class="m-3">
            <small class="text-secondary">> "<span class="message-user">${username}</span>" has joined the chat room. Say hi!</small>
        </div>
    `);

    messagesArea.scrollTop(messagesArea[0].scrollHeight);
}

function appendUserLeftMessageToArea(username) {
    messagesArea.append(`
        <div class="m-3">
            <small class="text-secondary">> "<span class="message-user">${username}</span>" has left the chat room :(</small>
        </div>
    `);

    messagesArea.scrollTop(messagesArea[0].scrollHeight);
}