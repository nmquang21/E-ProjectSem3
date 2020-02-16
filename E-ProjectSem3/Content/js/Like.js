jQuery(document).ready(function($) {
    //Like:
    $('.btn-like').click(function () {
        var id = $(this).data('id');
        var btn = $(this);
        $.ajax({
            url: `/Recipe/LikeAjax?id=${id}`,
            type: 'POST',
            success: function (responseData) {
                if (responseData.data == "NotLogIn") {
                    alert("Not Log In");
                } else {
                    btn.css('display', 'none');
                    btn.prev('.btn-unlike').css('display', 'contents');
                    btn.parent('.slide-button-i').children('.like-count').html(responseData.data);
                }
                console.log(responseData);
                console.log(btn.parent('.slide-button-i').children('.like-count'));
            }
        });
    });
    //Unlike:
    $('.btn-unlike').click(function () {
        var id = $(this).data('id');
        var btn = $(this);
        $.ajax({
            url: `/Recipe/UnLikeAjax?id=${id}`,
            type: 'POST',
            success: function (responseData) {
                if (responseData.data == "NotLogIn") {
                    alert("Not Log In");
                } else {
                    btn.css('display', 'none');
                    btn.next('.btn-like').css('display', 'contents');
                    btn.parent('.slide-button-i').children('.like-count').html(responseData.data);
                }
                console.log(responseData);
                console.log(btn.parent('.slide-button-i').children('.like-count'));
            }
        });
    });
})