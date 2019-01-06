"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/status").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});

connection.on("PrescriptionUpdate", function (userId) {
    var hiddenId = document.getElementById("hiddenId").value;
    if (hiddenId == userId) {
        return fetch('https://localhost:5001/')
            .then(result => result.text())
            .then(result => document.getElementById("bodyForStuff").innerHTML = result)
    } else {
        return;
    }
});
