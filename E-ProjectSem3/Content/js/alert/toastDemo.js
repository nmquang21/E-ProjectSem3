(function($) {
    showSuccessToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: `You are ${$('#show-message').data('success')} member.`,
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: {
                left: 20,
                top: 20
            }
        })
    };
    showSuccessAddRecipe = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Success',
            text: ' Recipe has been created.',
            showHideTransition: 'slide',
            icon: 'success',
            loaderBg: '#f96868',
            position: {
                left: 20,
                top: 20
            }
        })
    };
    showInfoToast = function () {
        'use strict';
        resetToastPosition();
        $.toast({
            heading: 'Hello',
            text: 'Your membership expired!',
            showHideTransition: 'slide',
            icon: 'info',
            loaderBg: '#46c35f',
            position: {
                left: 20,
                top: 20
            }
        })
    };
  showWarningToast = function() {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Warning',
      text: 'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
      showHideTransition: 'slide',
      icon: 'warning',
      loaderBg: '#57c7d4',
      position: 'top-right'
    })
  };
  showDangerToast = function() {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Danger',
      text: 'And these were just the basic demos! Scroll down to check further details on how to customize the output.',
      showHideTransition: 'slide',
      icon: 'error',
      loaderBg: '#f2a654',
      position: 'top-right'
    })
  };
  showToastPosition = function(position) {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Positioning',
      text: 'Specify the custom position object or use one of the predefined ones',
      position: String(position),
      icon: 'info',
      stack: false,
      loaderBg: '#f96868'
    })
  }
  showToastInCustomPosition = function() {
    'use strict';
    resetToastPosition();
    $.toast({
      heading: 'Custom positioning',
      text: 'Specify the custom position object or use one of the predefined ones',
      icon: 'info',
      position: {
        left: 120,
        top: 120
      },
      stack: false,
      loaderBg: '#f96868'
    })
  }
  resetToastPosition = function() {
    $('.jq-toast-wrap').removeClass('bottom-left bottom-right top-left top-right mid-center'); // to remove previous position class
    $(".jq-toast-wrap").css({
      "top": "",
      "left": "",
      "bottom": "",
      "right": ""
    }); //to remove previous position style
  }
})(jQuery);