//class for to create a server call
var ajaxDictionary = Array();
var ajax = function () {

};
ajax.prototype.setReturnFunction = function (returnFunction,thisObject) {
    this.returnFunction = returnFunction;
    this.thisObject = thisObject;
};
ajax.prototype.sendAjax = function (method,vars) {
    var xmlHttp = new XMLHttpRequest();    
    xmlHttp.onload = this.ajaxReturnFunction;
    ajaxDictionary[ajaxDictionary.length] = this;
    xmlHttp.open("POST", "data.asmx/" + method);
    xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlHttp.setRequestHeader("Content-length", vars.length);
    xmlHttp.setRequestHeader("Connection", "close");
    if (vars == "") {
        xmlHttp.send("ajaxid=" + (ajaxDictionary.length - 1));
    } else {        
        xmlHttp.send(vars+"&ajaxid=" + (ajaxDictionary.length - 1));
    }
    
};
ajax.prototype.ajaxReturnFunction = function () {
    var splits = this.responseText.split("{", 2);
    var indexof = this.responseText.indexOf("{");
    var thisLoc = ajaxDictionary[this.responseText.substring(0,indexof)];
    thisLoc.returnFunction(JSON.parse(this.responseText.substring(indexof)), thisLoc.thisObject);
};


var profileContent = function (updateFunction) {
    this.id = 0;
    this.username = "";
    this.age = 0;
    this.picture = "";
    this.location = "";    
    this.games = new Array();
    this.friends = new Array();
    this.updateFunction = updateFunction;
};
//vars = {"username":"string","age":int,"picture":"string"}
profileContent.prototype.updateProfile = function (vars) {
    this.username = vars["username"];
    this.age = vars["age"];
    this.picture = vars["picture"];
    this.uploadProfile();
};
profileContent.prototype.uploadProfile = function(){
    var upload = new ajax();
    upload.setReturnFunction(this.uploadProfileReturn, this);
    upload.sendAjax("putProfile", "username=" + this.username + ",age=" + age);
};
profileContent.prototype.uploadProfileReturn = function () {

};
//cookie will define which profile to download
profileContent.prototype.downloadProfile = function () {
    var download = new ajax();
    download.setReturnFunction(this.downloadProfileReturn,this);
    download.sendAjax("getProfile", "");
};
profileContent.prototype.downloadOtherProfile = function (profileId) {
    var download = new ajax();
    download.setReturnFunction(this.downloadProfileReturn, this);
    download.sendAjax("getOtherProfile", "profileId="+profileId);
};
profileContent.prototype.downloadProfileReturn = function (json,locA) {
    locA.username = json.username;
    locA.age = json.age;
    locA.picture = json.picture;
    locA.games = json.games;
    locA.location = json.location;
    locA.id = json.profileId;
    //alert(locA.id);
    locA.updateFunction(locA);
};
profileContent.prototype.addGame = function (gameId) {
    var addGameAjax = new ajax();
    addGameAjax.setReturnFunction(this.addGameReturn, this);
    addGameAjax.sendAjax("addGame", "gameId="+gameId);    
    //this.updateFunction();
};
profileContent.prototype.addGameReturn = function (json, locA) {
    locA.games[locA.games.length] = { "gameId": json.game.gameId, "name": json.game.name };
    locA.updateFunction();
};
profileContent.prototype.downloadFriends = function () {
    var download = new ajax();
    download.setReturnFunction(this.downloadFriendsReturn, this);
    download.sendAjax("getFriends", "");
};
profileContent.prototype.downloadFriendsReturn = function (json,locA) {
    locA.friends = json.friends;
    locA.updateFunction();
};
profileContent.prototype.addFriend = function (userId) {
    var serverFriendAdd = new ajax();
    download.setReturnFunction(this.addFriendReturn, this);
    download.sendAjax("addFriend", "profileId="+userId);
};
profileContent.prototype.addFriendReturn = function (json,locA) {
    locA.downloadFriends();
};
profileContent.prototype.getGame = function () {

};



var gameSearch = function (updateFunction) {
    this.gameList = new Array();
    this.updateFunction = updateFunction;
};
gameSearch.prototype.search = function (term) {
    //alert(term);
    var aj = new ajax();
    aj.setReturnFunction(this.returnFunction, this);
    aj.sendAjax("gameSearch", "searchTerm="+term);
};
gameSearch.prototype.returnFunction = function (json,locA) {
    for (var i = 0; i < json.games.length; i++) {
        locA.gameList[locA.gameList.length] = json.games[i];
    }
    locA.updateFunction();
};


var nearbyUsers = function (updateFunction) {
    this.updateFunction = updateFunction;
    //{"UserId":,"UserName":,"Distance":}
    this.nearbyUsers = new Array();
};
nearbyUsers.prototype.getUsersNearby = function () {
    var aj = new ajax();
    aj.setReturnFunction(this.returnFunction, this);
    aj.sendAjax("getNearby", "");
};
nearbyUsers.prototype.updateLocation = function(){
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(this.updateLocationReturn);
    } else {
        //location services off
    }
    if (document.getElementById("radarCanvasDiv").style.display == "none") {
        //do something
    }
};
nearbyUsers.prototype.updateLocationReturn = function (pos) {
    //alert(pos.coords.latitude);
}
nearbyUsers.prototype.returnFunction = function (json, locA) {
    locA.nearbyUsers = json.nearbyUsers;
    locA.updateFunction(json.nearbyUsers);
};
nearbyUsers.prototype.applyFilter = function () {

};

var messagingClient = function () {
    this.conversations = new Array();
    this.messages = new Array();
    this.currentConversation = -1;
};
messagingClient.prototype.getConversations = function(){
    var convGet = new ajax();
    convGet.setReturnFunction(this.getConversationsReturn, this);
    convGet.sendAjax("getConversations", "");
};
messagingClient.prototype.getConversation = function (convId) {
    this.messages = new Array();
    var convGet = new ajax();
    this.currentConversation = convId;
    convGet.setReturnFunction(this.getConversationReturn, this);
    convGet.sendAjax("getConversation", "conversationId="+convId);
};
messagingClient.prototype.getConversationsReturn = function (json, locA) {
    for (var i = 0; i < json.conversations.length; i++) {
        locA.conversations[locA.conversations.length] = { "id": json.conversations[i].id, "name": json.conversations[i].name };
    }
    locA.conversationUpdateFunction();
};
messagingClient.prototype.getConversationReturn = function (json, locA) {
    for (var i = 0; i < json.conversation.messages.length; i++) {
        var message = json.conversation.messages[i];
        locA.messages[locA.messages.length] = { "id": message.id, "fromId": message.from.profileId, "from": message.from.username, "message": message.message };
    }
    locA.messageUpdateFunction();
};
messagingClient.prototype.checkForMessages = function () {

};
messagingClient.prototype.sendMessage = function (message) {
    var sendAjax = new ajax();
    sendAjax.setReturnFunction(this.sendMessageReturn, this);
    sendAjax.sendAjax("sendMessage", "conversationId=" + this.currentConversation+"&message="+message);
};
messagingClient.prototype.sendMessageReturn = function (json, locA) {
    locA.getConversation(locA.currentConversation);
};
messagingClient.prototype.searchUsers = function(searchTerm){

};
messagingClient.prototype.searchUsersReturn = function(json,locA){
    locA.searchUsersReturn();
}
messagingClient.prototype.newConversation = function(otherUserId){
    var sendAjax = new ajax();
    sendAjax.setReturnFunction(this.sendMessageReturn, this);
    sendAjax.sendAjax("newConversation", "userId=" + this.otherUserId);
};
messagingClient.prototype.newConversationReturn = function (json, locA) {
    locA.newConversationReturnFunction(json.conversationId);
};
