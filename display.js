function displayProfile(profile) {
    document.getElementById("profileNameSpan").innerHTML = profile.username;
    document.getElementById("profileAgeSpan").innerHTML = profile.age;
    document.getElementById("profileLocationSpan").innerHTML = profile.location;
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