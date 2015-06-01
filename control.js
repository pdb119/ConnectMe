var prof;
var gSearch;
var mes;
//test git
function profileLoad() {
    prof = new profileContent(profileUpdated);
    prof.downloadProfile();
    gSearch = new gameSearch(searchGamesReturn);
    gameSearchBtn.onclick = function () {
        gSearch.search(document.getElementById("input").value);
    }
}
function friendsLoad() {
    prof = new profileContent(profileUpdated);
    prof.downloadFriends();
}
function profileUpdated() {
    if(typeof currentPage != "undefined"){
        if (currentPage == "profile") {
            displayProfile(prof);
        } else if (currentPage == "friends") {
            displayFriendsList(prof.friends);
        }
    }
}
function messagingLoad() {
    prof = new profileContent(profileUpdated);
    prof.downloadProfile();
    mes = new messagingClient();
    mes.conversationUpdateFunction = conversationsReturn;
    mes.getConversations();
    mes.messageUpdateFunction = conversationReturn;

}
function conversationsReturn() {
    displayConversations(mes.conversations);
    var test = window.location.hash;
    if (window.location.hash) {
        var found = false;
        for (var i = 0; i < mes.conversations.length; i++) {
            for (var j = 0; j < mes.conversations[i].members.length;j++){
                if (mes.conversations[i].members.length == 2 && mes.conversations[i].members[j].profileId == window.location.hash.substring(1)) {
                    document.getElementById("conversations").style.display = "none";
                    document.getElementById("conversation").style.display = "block";
                    mes.getConversation(mes.conversations[i].id);
                    found = true;
                    j = mes.conversations[i].members.length;                    
                }
            }
            if (found) {
                i = mes.conversations.length;
            }
        }
        if (!found) {
            mes.newConversationReturnFunction = newConversationReturn;
            mes.newConversation(window.location.hash.substring(1));
        }
    }
}
function newConversationReturn(convId) {
    mes.getConversation(convId);
    document.getElementById("conversations").style.display = "none";
    document.getElementById("conversation").style.display = "block";
    mes.conversationUpdateFunction = secondConversationUpdate;
    mes.getConversations();
}
function conversations() {
    document.getElementById("conversation").style.display = "none";
    document.getElementById("conversations").style.display = "block";
}
function conversationClicked(id) {
    document.getElementById("conversations").style.display = "none";
    document.getElementById("conversation").style.display = "block";
    mes.getConversation(id);
}
function secondConversationUpdate() {
    displayConversations(mes, prof);
}
function conversationReturn() {
    //alert(prof);
    displayConversation(mes,prof);
}
function searchGamesReturn() {
    displaySearchGames(gSearch.gameList);
}
function addGameClicked(gameId) {
    prof.addGame(gameId);
}
function friendProfileClicked(friendId) {
    //will move later, here tempor.
    displayFriendProfile(prof.friends[friendId]);
    document.getElementById("friendsSection").style.display = "none";
    document.getElementById("friendProfileDiv").style.display = "block";

}
function closeFriendProfile() {
    document.getElementById("friendsSection").style.display = "block";
    document.getElementById("friendProfileDiv").style.display = "none";
}
function radarProfileClicked(profid) {
    var radarProfile = new profileContent(radarProfileReturn);
    radarProfile.downloadOtherProfile(profid);
}
function radarProfileReturn(prof) {
    displayFriendProfile(prof);
    document.getElementById("friendsSection").style.display = "none";
    document.getElementById("friendProfileDiv").style.display = "block";
}
function sendMessage() {
    mes.sendMessage(document.getElementById("textBox").value);
    document.getElementById("textBox").value = "";
}
function userMessageSearch() {
    mes.searchUsersReturn = userMessageSearchReturn;
}
function userMessageSearchReturn(users) {

}