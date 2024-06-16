"use strict"

var connection = new signalR.HubConnectionBuilder()
                            .withUrl("/messages")
                            .build();

//after declaring the connection to the endpoint of the hub
//add a event handler to handle the messages recieved
connection.on("RecieveMessage", function(message){
    //handle what to do with the message when client recieves the message
    var msg = message.replace("/&/g","&amp");
    var div = document.createElement("div");
    div.innerHTML = msg + "<hr/>";
    document.getElementById("messages").append(div);

});

connection.start().catch(function(err){
return console.error(err.toString());
});

//sending a message to the hub which in turn is sending the message to all
//the cilents connected to the hub
document.getElementById("sendButton").addEventListener("click",function(event){
    //send message to the HUB
    var message = document.getElementById("message").value;
    connection.invoke("SendMessageToClient",message).catch(function(err){
    return console.error(err.toString());
    });
    event.preventDefault();

});

//do dotnet run
//add messages and response in one tab
//open same url in another tab and add messages and see how all clients(tabs here)
//are recieving the messages in realtime