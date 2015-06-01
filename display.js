function displayProfile(profile) {
    document.getElementById("profileNameSpan").innerHTML = profile.username;
    document.getElementById("profileAgeSpan").innerHTML = profile.age;
    document.getElementById("profileLocationSpan").innerHTML = profile.location;
    var firstTemplate = document.getElementById("gameTemplate").cloneNode(true);
    document.getElementById("gamesDiv").innerHTML = "";
    document.getElementById("gamesDiv").appendChild(firstTemplate);
    for (var i = 0; i < profile.games.length; i++) {
        //alert(profile.games[i].name);
        var template = document.getElementById("gameTemplate").cloneNode(true);
        template.querySelector(".game").innerHTML = "<h1>"+profile.games[i].name+"</h1>";
        template.setAttribute("id", profile.games[i].name.replace(" ", "") + "GameDiv");
        template.style.display = "block";
        document.getElementById("gamesDiv").appendChild(template);
    }
    //document.getElementById("profileNameSpan").innerHTML = profile.username;
}
function displaySearchGames(games) {
    var firstTemplate = document.getElementById("gameResultTemplate").cloneNode(true);
    document.getElementById("gameResults").innerHTML = "";
    var ulTemplate = document.createElement("ul");
    ulTemplate.appendChild(firstTemplate);
    document.getElementById("gameResults").appendChild(ulTemplate);
    for (var i = 0; i < games.length; i++) {
        var template = document.getElementById("gameResultTemplate").cloneNode(true);
        template.querySelector(".gameResultTitle").innerHTML = games[i].name;
        template.setAttribute("id", games[i].name.replace(" ", "") + "GameResult");
        template.querySelector(".addGameButton").setAttribute("onclick", "addGameClicked(\"" + games[i].gameId + "\");");
        template.style.display = "block";
        document.getElementById("gameResults").querySelector("ul").appendChild(template);
    }
}
function displayFriendsList(friends) {
    var firstTemplate = document.getElementById("friendTemplate").cloneNode(true);
    document.getElementById("friendsSection").innerHTML = "";
    document.getElementById("friendsSection").appendChild(firstTemplate);
    for (var i = 0; i < friends.length; i++) {
        //alert(profile.games[i].name);
        var template = document.getElementById("friendTemplate").cloneNode(true);
        template.querySelector("#frname").innerHTML = friends[i].username;
        if (friends[i].games.length > 0) {
            template.querySelector("#game1").innerHTML = friends[i].games[0].name;
        }
        if (friends[i].games.length > 1) {
            template.querySelector("#game2").innerHTML = friends[i].games[1].name;
        }
        if (friends[i].games.length > 2) {
            template.querySelector("#game3").innerHTML = friends[i].games[2].name;
        }
        template.setAttribute("id", friends[i].username.replace(" ", "") + "FriendDiv");
        template.setAttribute("onclick", "friendProfileClicked(" + i + ");");
        template.style.display = "block";
        document.getElementById("friendsSection").appendChild(template);
    }
}
function displayUsersList(users) {
    var firstTemplate = document.getElementById("friendTemplate").cloneNode(true);
    document.getElementById("friendsSection").innerHTML = "";
    document.getElementById("friendsSection").appendChild(firstTemplate);
    for (var i = 0; i < users.length; i++) {
        var template = document.getElementById("friendTemplate").cloneNode(true);
        template.querySelector("#frname").innerHTML = users[i].userName;
        template.querySelector("#game1").innerHTML = users[i].games[0].name;
        template.querySelector("#game2").innerHTML = users[i].games[1].name;
        template.querySelector("#game3").innerHTML = users[i].games[2].name;
        template.querySelector(".messageLink").setAttribute("href", "conversation.html#" + users[i].userId);
        template.style.display = "block";
        template.setAttribute("id", users[i].userName.replace(" ", "") + "ListUser");
        template.setAttribute("onclick", "radarProfileClicked("+users[i].userId+");");
        document.getElementById("friendsSection").appendChild(template);        

        //users[i].distance;
        //users[i].userName;
    }
}
function drawRadar(users) {
    var c = document.getElementById("radarCanvas");
    var circleRads = new Array();
    for (var rad = 165; rad > 30; rad -= 35) {
        var circle = c.getContext("2d");
        circle.beginPath();
        circle.arc(175, 175, rad, 0, 2 * Math.PI);
        circle.lineWidth = 4;
        circle.strokeStyle = "#FFFFFF";
        circle.stroke();
        circleRads[circleRads.length] = rad;
    }
    circle.beginPath();
    circle.fillStyle = "#FFFFFF";
    circle.arc(175, 175, 20, 0, 2 * Math.PI);
    circle.fill();
    radarDotLocations = new Array();
    for (var i = 0; i < users.length; i++) {
        circle.fillStyle = "#000000";
        var dist = users[i].distance;
        var closestCircle = circleRads[0];
        var shortest = Math.abs(dist - circleRads[0]);
        for (var j = 1; j < circleRads.length; j++) {
            var calculatedShortPath = Math.min(shortest, Math.abs(dist - circleRads[j]));
            if (shortest > calculatedShortPath) {
                shortest = calculatedShortPath;
                closestCircle = circleRads[j];
            }
        }
        var circleRadians = Math.random() * 2 * Math.PI;
        var x = 175.0 + (Math.sin(circleRadians) * closestCircle);
        var y = 175.0 - (Math.cos(circleRadians) * closestCircle);
        var userDot = c.getContext("2d");
        userDot.beginPath();
        circle.arc(x, y, 10, 0, 2 * Math.PI);
        circle.fill();
        circle.font = "12px Arial";
        circle.fillStyle = "#003300";
        circle.fillText(users[i].userName, x + 12, y);
        radarDotLocations[i] = { "x": x, "y": y };
    }
}
    function displayConversations(cs) {
        var firstTemplate = document.getElementById("conversationTemplate").cloneNode(true);
        document.getElementById("conversations").innerHTML = "";
        document.getElementById("conversations").appendChild(firstTemplate);
        for (var i = 0; i < cs.length; i++) {
            var template = document.getElementById("conversationTemplate").cloneNode(true);
            template.querySelector("#name").innerHTML = cs[i].name;
            template.querySelector(".templateLink").setAttribute("onclick", "conversationClicked(" + cs[i].id + ");");
            template.setAttribute("id", cs[i].id + "ConversationLink");
            template.style.display = "block";
            document.getElementById("conversations").appendChild(template);
        }
    }
    function displayConversation(mesAge, profile) {
        var messages = mesAge.messages;
        var convArrayVal = -1;
        for (var i = 0; i < mesAge.conversations.length; i++) {
            if (mesAge.conversations[i].id == mesAge.currentConversation) {
                convArrayVal = i;
            }
        }
        var conversation = mesAge.conversations[convArrayVal];        
        var firstTemplate = document.getElementById("messageTemplate").cloneNode(true);
        var secondTemplate = document.getElementById("messageTemplateMe").cloneNode(true);
        document.getElementById("messagesDiv").innerHTML = "";
        document.getElementById("messagesDiv").appendChild(firstTemplate);
        document.getElementById("messagesDiv").appendChild(secondTemplate);        
        //alert(messages.length);
        for (var i = 0; i < messages.length; i++) {
            var template;
            //alert(messages[i].fromId);
            //alert(profile.id);
            if (messages[i].fromId == profile.id) {
                template = document.getElementById("messageTemplateMe").cloneNode(true);
            } else {
                template = document.getElementById("messageTemplate").cloneNode(true);
            }
            document.getElementById("conversationName").innerHTML = conversation.name;
            template.querySelector(".chatother").innerHTML = messages[i].message;            
            template.setAttribute("id", messages[i].id + "Message");
            template.style.display = "block";
            document.getElementById("messagesDiv").appendChild(template);
        }
    }

    function displayFriendProfile(prof) {
        document.getElementById("profileNameSpan").innerHTML = prof.username;
        document.getElementById("profileAgeSpan").innerHTML = prof.age;
        document.getElementById("profileLocationSpan").innerHTML = prof.location;
        var firstTemplate = document.getElementById("gameTemplate").cloneNode(true);
        document.getElementById("gamesDiv").innerHTML = "";
        document.getElementById("gamesDiv").appendChild(firstTemplate);
        var gamesArray = prof.games;
        for (var i = 0; i < gamesArray.length; i++) {
            //alert(profile.games[i].name);
            var template = document.getElementById("gameTemplate").cloneNode(true);
            template.querySelector(".game").innerHTML = "<h1>" + gamesArray[i].name + "</h1>";
            template.setAttribute("id", gamesArray[i].name.replace(" ", "") + "GameDiv");
            template.style.display = "block";
            document.getElementById("gamesDiv").appendChild(template);
        }
    }