jQuery(document).ready(function($) {
    $.ajax({
        url: '/Recipe/GetPopularRecipesAjax',
        type: 'GET',
        success: function(responseData) {
            var html = '';
            for (var i = 0; i < responseData.data.length; i++) {
                html += `<li>
                            <a href="/Home/RecipeDetail/${responseData.data[i].Id}" class="tptn_link">
                                <img style ="width: 80px !important; height: 80px !important;" src="${
                    responseData.data[i].FeaturedImage}" class="tptn_thumb tptn_featured" alt="${
                    responseData.data[i].Title}" title="${responseData.data[i].Title
                    }" sizes="(max-width: 150px) 100vw, 150px" />
                            </a>
                            <span class="tptn_after_thumb">
                                <a href="/Home/RecipeDetail/${responseData.data[i].Id
                    }" class="tptn_link"><span class="tptn_title">${responseData.data[i].Title}</span></a>
                                <span class="tptn_excerpt"> Put your wine, shallot, herbs and peppercorns into a small&hellip;</span>
                            </span>
                        </li>`;
            }
            $('.popular-ajax-page').html(html);
        }
    })
})
