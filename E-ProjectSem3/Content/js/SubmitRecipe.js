jQuery(document).ready(function ($) {
    if ($('#add-success').data('msg') == "Success") {
        showSuccessToast();
    };
    var index = 1;
    $('#btn-add-nutrition').click(function () {
        index++;
        var contentNutrition = `<tr class="acf-row acf-clone nutrition">
                                    <td class="acf-row-handle order">
                                        <span>${index}</span>
                                    </td>

                                    <td class="acf-field acf-field-text acf-field-558bba3a6bc23 -collapsed-target" data-name="nutrition_name" data-type="text">
                                        <div class="acf-input">
                                            <div class="acf-input-wrap">
                                                <input type="text" id="" name="listNutrition[${index-1}].Name}" />
                                            </div>
                                        </div>
                                    </td>

                                    <td class="acf-field acf-field-text acf-field-558bba486bc24" data-name="nutrition_value" data-type="text">
                                        <div class="acf-input">
                                            <div class="acf-input-wrap">
                                                <input type="text" id="" name="listNutrition[${index-1}].Value" />
                                            </div>
                                        </div>
                                    </td>

                                    <td class="acf-row-handle remove">
                                        <i class="fas fa-trash-alt delete-nutri" style="cursor: pointer"></i>
                                    </td>

                                </tr>`;
        $('.list-nutrition').prepend(contentNutrition);
    });

    var indexCT = 1;
    $('#btn-add-ingredient').click(function () {
        indexCT++;
        var contentCT = `<tr class="acf-row acf-clone ct">
                            <td class="acf-row-handle order" title="Drag to reorder">
                                <span>${indexCT}</span>
                            </td>
                            <td class="acf-field acf-field-text acf-field-5585317d9b50b">
                                <div class="acf-input">
                                    <div class="acf-input-wrap">
                                        <input type="text" id="" name="listIngredient[${indexCT - 1}].Name" />
                                    </div>
                                </div>
                            </td>

                            <td class="acf-field acf-field-text acf-field-558531f69b50c">
                                <div class="acf-input">
                                    <div class="acf-input-wrap">
                                        <input type="text" id="" name="listIngredient[${indexCT - 1}].Amount" />
                                    </div>
                                </div>
                            </td>
                            <td class="acf-row-handle remove">
                                <i class="fas fa-trash-alt delete-ct" style="cursor: pointer"></i>
                            </td>
                        </tr>`;
        $('.list-ct').prepend(contentCT);
    });

    var indexStep = 1;
    $('#btn-add-step').click(function () {
        indexStep++;
        var contentStep = `<tr class="acf-row step">
                                <td class="acf-row-handle order ui-sortable-handle">
                                    <input type="text" id="" name="listStep[${indexStep - 1}].Index" value="${indexStep}">
                                </td>

                                <td class="acf-fields">
                                    <div class="acf-field acf-field-textarea acf-field-5588b30c2df0d -collapsed-target" style="width: 50%" data-width="30" data-name="step_description" data-type="textarea">
                                        <div class="acf-label">
                                            <label for="step_title">Title</label>
                                        </div>
                                        <div class="acf-input">
                                            <input type="text" id="step_title" name="listStep[${indexStep - 1}].Title">
                                        </div>
                                    </div>
                                    <div class="acf-field acf-field-textarea acf-field-5588b30c2df0d -collapsed-target" style="width: 50%" data-width="30" data-name="step_description" data-type="textarea">
                                        <div class="acf-label">
                                            <label for="image-step">Image</label>
                                        </div>
                                        <div class="acf-input">
                                            <input type="text" id="image-step" name="listStep[${indexStep - 1}].ImagePath">
                                        </div>
                                    </div>

                                    <div class="acf-field acf-field-textarea acf-field-5588b30c2df0d -collapsed-target" data-name="step_description" data-type="textarea">
                                        <div class="acf-label">
                                            <label for="step_description">Description</label>
                                        </div>
                                        <div class="acf-input">
                                            <textarea id="step_description" name="listStep[${indexStep - 1}].Description" rows="4"></textarea>
                                        </div>
                                    </div>
                                </td>
                                <td class="acf-row-handle remove">
                                    <i class="fas fa-trash-alt delete-step" style="cursor: pointer"></i>
                                </td>

                            </tr>`;
        $('.list-step').prepend(contentStep);
    });



    $(document).on('click', '.delete-ct', function () {
        $(this).closest('.ct').remove();
    });
    $(document).on('click', '.delete-step', function () {
        $(this).closest('.step').remove();
    });
    $(document).on('click', '.delete-nutri', function () {
        $(this).closest('.nutrition').remove();});

    //Upload cloudinary:
    var myWidget = cloudinary.createUploadWidget({
            cloudName: 'dev20',
            uploadPreset: 'gfj9avei'
        },
        (error, result) => {
            if (!error && result && result.event === "success") {

                $('#post_image').val(result.info.url);
                var html = `<img src="${result.info.url}" id="image-recipe" alt="Alternate Text" style="width: 350px; heigth: auto;"/>`;
                $('.imagePreview').html(html);
                console.log(this)
            }
        }
    )

    document.getElementById("upload_widget").addEventListener("click",
        function () {
            
            myWidget.open();
            //this.parentElement.previousElementSibling.value = ;
            //this.parentElement.nextElementSibling;
        },
        false);



        //$('#upload_widget').cloudinary_upload_widget(
        //    {
        //        cloud_name: 'dev20',
        //        upload_preset: 'gfj9avei',
        //    },
        //    function (error, result) {
        //        if (!error && result && result.event === "success") {
        //            //$('#post_image').val(result.info.url);
        //            //var html = `<img src="${result.info.url}" id="image-recipe" alt="Alternate Text" style="width: 350px; heigth: auto;"/>`;
        //            //$('.imagePreview').html(html);
        //            console.log($(this))
        //        }
        //    });


    $('.upload_step').click(function () {
        var btn = $(this);
        console.log("Click")
        cloudinary.createUploadWidget({
            cloudName: 'dev20',
            uploadPreset: 'gfj9avei'
        },
            (error, result) => {
                if (!error && result && result.event === "success") {
                    btn.parent('.btn').next('.image-step').val(result.info.url);
                    var html = `<img src="${result.info.url}" id="image-recipe" alt="Alternate Text" style="width: 250px; height: 200px !important;"/>`;
                    btn.parent('.btn').prev('.image-preview-step').html(html);
                }
            }
        ).open();
    });




})

