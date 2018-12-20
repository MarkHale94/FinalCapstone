"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/status").build();

connection.start().catch(function (err) {
    return console.error(err.toString());
});

connection.on("PrescriptionUpdate", function (userId) {
    var hiddenId = document.getElementById("hiddenId").value;
    if (hiddenId == userId) {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == XMLHttpRequest.DONE) {
                if (xmlhttp.status == 200) {
                    document.getElementById("bodyForStuff").innerHTML = xmlhttp.responseText;
                }
                else if (xmlhttp.status == 400) {
                    alert('There was an error 400');
                }
                else {
                    console.log(xmlhttp.response)
                    alert('something else other than 200 was returned');
                }
            }
        };
        xmlhttp.open("GET", "https://localhost:5001/", true);
        xmlhttp.send();
    } else {
        return;
    }
});
