"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/status").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});

connection.on("PrescriptionUpdate", function () {
    window.location.reload();
});