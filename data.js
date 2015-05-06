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
    xmlHttp.onload = this.returnFunction;
    ajaxDictionary[xmlHttp] = this.thisObject;
    xmlHttp.open("POST", "data.asmx/" + method);
    xmlHttp.send(vars);
};
var profileContent = function (updateFunction) {
    this.username = "";
    this.age = 0;
    this.picture = "";
    this.location = "";
    this.games = new Array();
    this.updateFunction = updateFunction;
};
//vars = {"username":"string","age":int,"picture":"string"}
profileContent.prototype.updateProfile = function (vars) {
    this.username = vars["username"];
    this.age = vars["age"];
    this.picture = vars["picture"];
};
//cookie will define which profile to download
profileContent.prototype.downloadProfile = function () {
    var download = new ajax();
    download.setReturnFunction(this.downloadProfileReturn,this);
    download.sendAjax("getProfile", {});
};
profileContent.prototype.downloadProfileReturn = function () {
    var json = JSON.parse(this.responseText);
    var locA = ajaxDictionary[this];
    delete ajaxDictionary[this];
    locA.username = json.username;
    locA.age = json.age;
    locA.picture = json.picture;
    locA.games = json.games;
    locA.location = json.location;
    locA.updateFunction();
};
profileContent.prototype.addGame = function(){

};
profileContent.prototype.getGame = function () {

};

var gameSearch = function (updateFunction) {
    this.gameList = new Array();
    this.updateFunction = updateFunction;
};
gameSearch.prototype.search = function(term){
    var aj = new ajax();
    aj.setReturnFunction(this.returnFunction, this);
    aj.sendAjax("gameSearch", { "searchTerm": term });
};
gameSearch.prototype.returnFunction = function () {
    var json = JSON.parse(this.responseText);
    var locA = ajaxDictionary[this];
    delete ajaxDictionary[this];
    for (var i = 0; i < json.games.length; i++) {
        locA.gameList[locA.gameList.length] = json.games[i];
    }
    locA.updateFunction();
};