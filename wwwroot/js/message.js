"use strict"

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/messages")
    .build();

//after declaring the connection to the endpoint of the hub
//add a event handler to handle the messages recieved
connection.on("RecieveMessage", function (message) {
    //handle what to do with the message when client recieves the message
    var msg = message.replace("/&/g", "&amp");
    var div = document.createElement("div");
    div.innerHTML = msg + "<hr/>";
    document.getElementById("messages").append(div);

});

connection.on("UserConnected", function (connectionId) {
    var groupElement = document.getElementById("group");
    var option = document.createElement("option");
    option.text = connectionId;
    option.value = connectionId;
    groupElement.add(option);

});

connection.on("UserDisconnected", function (connectionId) {
    var groupElement = document.getElementById("group");
    for (var i = 0; i < groupElement.length; i++) {
        if (groupElement.option[i].value == connectionId)
            groupElement.remove(i);
    }

});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

//sending a message to the hub which in turn is sending the message to all
//the cilents connected to the hub
document.getElementById("sendButton").addEventListener("click", function (event) {
    //send message to the HUB
    var message = document.getElementById("message").value;
    var groupElement = document.getElementById("group");
    var groupvalue = groupElement.options[groupElement.selectedIndex].value;
    if (groupvalue == "All" || groupvalue == "Myself") {
        var method = groupvalue == "All" ? "SendMessageToAll" : "SendMessageToCaller";

        connection.invoke(method, message).catch(function (err) {
            return console.error(err.toString());
        });

    }
    else if (groupvalue=="PrivateGroup"){
        console.log(groupvalue);
        connection.invoke("SendMessageToGroup","PrivateGroup", message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    else {
        connection.invoke("SendMessageToUser",groupvalue, message).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();

});

document.getElementById("joinGroup").addEventListener("click",function(event){
    console.log("join private group clicked");  
    connection.invoke("JoinGroup","PrivateGroup").catch(function (err) {
        return console.error(err.toString());
    });  
    event.preventDefault();

});

//do dotnet run
//add messages and response in one tab
//open same url in another tab and add messages and see how all clients(tabs here)
//are recieving the messages in realtime
//in browser developer tool we can check in the network transactions the negotiation between
//the client and the server about protocol to use and with response on settling for the 
//http upgrade websockets type communication