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
        template.querySelector(".gameResultTitle").innerHTML = games[i];
        template.setAttribute("id", games[i].replace(" ", "") + "GameResult");
        template.querySelector(".addGameButton").setAttribute("onclick", "addGameClicked(\"" + games[i] + "\");");
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
        template.querySelector("#game1").innerHTML = friends[i].games[0].name;
        template.querySelector("#game2").innerHTML = friends[i].games[1].name;
        template.querySelector("#game3").innerHTML = friends[i].games[2].name;
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
        template.style.display = "block";
        template.setAttribute("id", users[i].userName.replace(" ", "") + "ListUser");
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
        circle.strokeStyle= "#FFFFFF";
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
        var shortest = Math.abs(dist-circleRads[0]);
        for (var j = 1; j < circleRads.length; j++) {
            var calculatedShortPath = Math.min(shortest, Math.abs(dist - circleRads[j]));
            if (shortest > calculatedShortPath) {
                shortest = calculatedShortPath;
                closestCircle = circleRads[j];
            }
        }
        var circleRadians = Math.random() * 2 * Math.PI;
        var x = 175.0+(Math.sin(circleRadians) * closestCircle);
        var y = 175.0-(Math.cos(circleRadians) * closestCircle);
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