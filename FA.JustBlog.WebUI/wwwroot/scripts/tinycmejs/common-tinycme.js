tinymce.init({
    selector: "textarea#menubar",
    branding: false,
    menubar: false,
    plugins: "lists code emoticons media image",
    toolbar: "bold italic underline strikethrough |" +
        "forecolor backcolor |" +
        "media image |" +
        "indent | bullist | emoticons",
    emoticons_append: {
        custom_mind_explode: {
            keywords: ["brain", "mind", "explode", "blown"],
            char: "🤯"
        }
    },
    media_url_resolver: function (data, resolve) {
        if (data.url.indexOf("YOUR_SPECIAL_VIDEO_URL") !== -1) {
            let embedHtml = `<iframe src="${data.url}" width="400" height="400" ></iframe>`;
            resolve({ html: embedHtml });
        } else {
            resolve({ html: "" });
        }
    }
});