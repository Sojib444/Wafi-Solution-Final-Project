$(document).ready(() => {
    debugger
    var l = abp.localization.getResource('Learn');
    var updateProfileModal = new abp.ModalManager(abp.appPath + 'Dashboard/Modals/UpdateProfileModal');
    var userId = $("#Input_UserId").val()

    $("#updateProfile").click(function (e) {
        debugger
        updateProfileModal.open({ id: userId });
    });
   
    updateProfileModal.onResult(function () {
    });
});