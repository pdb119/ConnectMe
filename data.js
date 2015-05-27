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
    var th = this;
    xmlHttp.onload = this.ajaxReturnFunction;
    ajaxDictionary[xmlHttp] = this;
    xmlHttp.open("POST", "data.asmx/" + method);
    xmlHttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlHttp.setRequestHeader("Content-length", vars.length);
    xmlHttp.setRequestHeader("Connection", "close");
    xmlHttp.send(vars);
};
ajax.prototype.ajaxReturnFunction = function () {
    var thisLoc = ajaxDictionary[this];
    delete ajaxDictionary[this];
    thisLoc.returnFunction(JSON.parse(this.responseText), thisLoc.thisObject);
};


var profileContent = function (updateFunction) {
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
    download.sendAjax("getProfile", {});    
};
profileContent.prototype.downloadProfileReturn = function (json,locA) {
    locA.username = json.username;
    locA.age = json.age;
    locA.picture = json.picture;
    locA.games = json.games;
    locA.location = json.location;
    locA.updateFunction();
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
    download.sendAjax("getFriends", {});
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
};
messagingClient.prototype.getConversations = function(){
    var convGet = new ajax();
    convGet.setReturnFunction(this.returnFunction, this);
    convGet.sendAjax("getConversations", "");
};
messagingClient.prototype.getConversation = function (convId) {
    var convGet = new ajax();
    convGet.setReturnFunction(this.returnFunction, this);
    convGet.sendAjax("getConversation", "conversationId="+convId);
};
messagingClient.prototype.getConversationsReturn = function (json, locA) {
    for (var i = 0; i < conversations.length; i++) {
        locA.conversations[locA.conversations.length] = { "id": json.conversations[i].id, "name": json.conversations[i].name };
    }
    
};
messagingClient.prototype.getConversationReturn = function (json, locA) {

};
messagingClient.prototype.getConversation = function(){

};
messagingClient.prototype.checkForMessages = function () {

};
