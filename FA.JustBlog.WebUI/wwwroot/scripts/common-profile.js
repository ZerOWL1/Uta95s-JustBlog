$("#form-p-pass").submit(function(e) {
    const newPass = $("#PasswordViewModel_NewPassword").val();
    const rePass = $("#PasswordViewModel_RePassword").val();
    const isValid = isUpper(newPass);

    if (newPass !== rePass || isValid === false) {
        alert("Check Passwords Requirements");
        e.preventDefault();
    }
});

function isUpper(str) {
    if (str.length >= 6) {
        const specialChar = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/;
        const isChar = specialChar.test(str);

        if (isChar) {
            const isCapital = /[a-z]/.test(str) && /[A-Z]/.test(str);
            return isCapital;
        } else {
            return false;
        }
    }
    return false;
}


$(".secu-discard").click(function (e) {
    $("#PasswordViewModel_OldPassword").val("");
    $("#PasswordViewModel_NewPassword").val("");
    $("#PasswordViewModel_RePassword").val("");
});

$(".acc-del").click(function (e) {
    const x = confirm("You sure want to disable your account?");
    if (x) {
        return true;
    } else {
        return false;
    }
});