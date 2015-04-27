var prof;
window.onload = function () {
    prof = new profileContent(profileUpdated);
    prof.downloadProfile(); 
}
function profileUpdated() {
    displayProfile(prof);
}