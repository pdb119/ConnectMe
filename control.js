var prof;
var gSearch;
window.onload = function () {
    prof = new profileContent(profileUpdated);
    prof.downloadProfile();
    gSearch = new gameSearch();
}
function profileUpdated() {
    displayProfile(prof);
}
function searchGamesReturn() {
    displaySearchGames(gSearch.gameList);
}