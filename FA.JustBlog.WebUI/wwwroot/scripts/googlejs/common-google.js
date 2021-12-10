var OAUTH_URL = "https://accounts.google.com/o/oauth2/auth?";
var VALID_URL = "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token=";
var SCOPE = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email";
var CLIENT_ID = "";
var REDIRECT_URI = "https://localhost:44375/Account/GoogleLogin";
var TYPE = "token";
var _url = OAUTH_URL + "scope=" + SCOPE + "&client_id=" + CLIENT_ID + "&redirect_uri="
    + REDIRECT_URI + "&response_type=" + TYPE;
var acToken;
var tokenType;
var expiresIn;
var user;
var loggedIn = false;

function googleLogin() {
    var win = window.open(_url, "windowName", "width=800, height=600");
    var pollTimer = window.setInterval(function () {
        try {
            if (win.document.URL.indexOf(REDIRECT_URI) !== -1) {
                window.clearInterval(pollTimer);
                const url = win.document.URL;
                acToken = gup(url, "access_token");
                tokenType = gup(url, "token_type");
                expiresIn = gup(url, "expires_in");

                win.close();
                debugger;
                validateToken(acToken);
            }
        } catch (e) {

        }
    },
        500);
}

function gup(url, name) {
    const regexS = `[\\#&]${name}=([^&#]*)`;
    const regex = new RegExp(regexS);
    const results = regex.exec(url);
    if (results == null)
        return "";
    else
        return results[1];
}

function validateToken(token) {
    getUserInfo();
    $.ajax(
        {
            url: VALID_URL + token,
            data: null,
            success: function (responseText) {
            }
        });
}

function getUserInfo() {
    $.ajax({
        url: `https://www.googleapis.com/oauth2/v1/userinfo?access_token=${acToken}`,
        data: null,
        success: function (resp) {
            const user = resp;
            //console.log(user);
            getUser(user);
        },
        error: function (err) {
            console.log(err);
        }
    });
}

function getUser(user) {
    const data = {
        "Name": user.name,
        "Email": user.email,
        "Image": user.picture,
        "External": "Google"
    }

    $.ajax({
        url: "Account/GoogleLogin",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (res) {
            const normalizeResponse = res.toLowerCase();

            if (normalizeResponse === "signin".toLowerCase()) {
                window.location.href = "";
            }
            if (normalizeResponse === "register".toLowerCase()) {
                alert("Register Successful - Check Email For Passwords");
            }
        },
        error: function (err) {
            window.location.href = "/account/register";
            alert("Error 500");
            console.log(err);
        }
    });
}


