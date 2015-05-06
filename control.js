var prof;
var gSearch;
window.onload = function () {
    prof = new profileContent(profileUpdated);
    prof.downloadProfile();
    gSearch = new gameSearch();
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