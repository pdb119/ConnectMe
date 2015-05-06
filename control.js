var prof;
var gSearch;
function profileLoad() {
    prof = new profileContent(profileUpdated);
    prof.downloadProfile();
    gSearch = new gameSearch(searchGamesReturn);
    gameSearchBtn.onclick = function () {
        gSearch.search("test");
    }
}
function friendsLoad() {
    prof = new profileContent(profileUpdated);
    prof.downloadFriends();
}
function profileUpdated() {
    if (currentPage == null || currentPage == "profile") {
        displayProfile(prof);
    } else if (currentPage == "friends") {
        displayFriendsList(prof.friends);
    }
}
function searchGamesReturn() {
    displaySearchGames(gSearch.gameList);
}
function addGameClicked(gameName) {
    prof.addGame(gameName);
}
function friendProfileClicked(friendId) {
    //will move later, here tempor.
    document.getElementById("profileNameSpan").innerHTML = prof.friends[friendId].username;
    document.getElementById("profileAgeSpan").innerHTML = prof.friends[friendId].age;
    document.getElementById("profileLocationSpan").innerHTML = prof.friends[friendId].location;
    var firstTemplate = document.getElementById("gameTemplate").cloneNode(true);
    document.getElementById("gamesDiv").innerHTML = "";
    document.getElementById("gamesDiv").appendChild(firstTemplate);
    var gamesArray = prof.friends[friendId].games;
    for (var i = 0; i < gamesArray.length; i++) {
        //alert(profile.games[i].name);
        var template = document.getElementById("gameTemplate").cloneNode(true);
        template.querySelector(".game").innerHTML = "<h1>" + gamesArray[i].name + "</h1>";
        template.setAttribute("id", gamesArray[i].name.replace(" ", "") + "GameDiv");
        template.style.display = "block";
        document.getElementById("gamesDiv").appendChild(template);
    }
    document.getElementById("friendsSection").style.display = "none";
    document.getElementById("friendProfileDiv").style.display = "block";

}
function closeFriendProfile() {
    document.getElementById("friendsSection").style.display = "block";
    document.getElementById("friendProfileDiv").style.display = "none";
}