//class for to create a server call
var ajax = function () {

};
ajax.prototype.setReturnFunction = function (returnFunction) {
    this.returnFunction = returnFunction;

};
ajax.prototype.sendAjax = function (method,vars) {
    var ajax = new XMLHttpRequest();
    ajax.onload = this.returnFunction;
    ajax.open("POST", "data.asmx/" + method);
    ajax.send(vars);
};
var profileContent = function () {

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
    download.setReturnFunction(this.downloadProfileReturn);
    download.sendAjax("getProfile", {});
};
profileContent.prototype.downloadProfileReturn = function () {
    var json = JSON.parse(this.responseText);
    alert(json.username);
};
profileContent.prototype.addGame = function(){

};
profileContent.prototype.getGame = function () {

};