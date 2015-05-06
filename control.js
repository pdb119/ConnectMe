var prof;
var gSearch;
window.onload = function () {
    prof = new profileContent(profileUpdated);
    prof.downloadProfile();
    gSearch = new gameSearch(searchGamesReturn);
    gameSearchBtn.onclick = function () {
        gSearch.search("test");
    }
}
function profileUpdated() {
    displayProfile(prof);
}
function searchGamesReturn() {
    displaySearchGames(gSearch.gameList);
}
function addGameClicked(gameName) {
    prof.addGame(gameName);
}