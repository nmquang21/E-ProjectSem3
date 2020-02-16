jQuery(document).ready(function ($) {
    $.ajax({
        url: '/Categories/GetCategoriesAjax',
        type: 'GET',
        success: function (responseData) {
            var html = '';
            for (var i = 0; i < responseData.data.length; i = i + 2) {
                html += `<tr>
                                <td>
                                    <div class="sci-media"><a href="/Home/Recipes?category=${responseData.data[i].Id}"><img src="${responseData.data[i].Icon}" alt="Chicken" /></a></div>
                                    <div class="sci-title">
                                        <h3><a href="/Home/Recipes?category=${responseData.data[i].Id}">${responseData.data[i].Name}</a></h3>
                                    </div>
                                </td>
                                <td>
                                    <div class="sci-media"><a href="/Home/Recipes?category=${responseData.data[i + 1].Id}"><img src="${responseData.data[i + 1].Icon}" alt="Chicken" /></a></div>
                                    <div class="sci-title">
                                        <h3><a href="/Home/Recipes?category=${responseData.data[i + 1].Id}">${responseData.data[i + 1].Name}</a></h3>
                                    </div>
                                </td>
                            </tr>`;
            }
            $('.category-ajax').html(html);
        }
    });
})