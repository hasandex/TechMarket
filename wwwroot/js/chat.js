"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable the send button until connection is established.

connection.on("ReceiveMessage", function (userId, productName) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${userId} says ${productName}`;
});

connection.start().then(function () {
    console.log("hasan");
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var userId = document.getElementById("userInput").value;
    var productName = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", userId, productName).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});