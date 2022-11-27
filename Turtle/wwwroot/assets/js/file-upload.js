(function ($) {
    'use strict';
    $(function () {
        $('.file-upload-browse').on('click', function () {
            var file = $(this).parent().parent().parent().find('.file-upload-default');
            file.trigger('click');
        });
        $('.file-upload-default').on('change', function () {
            $(this).parent().find('.form-control').val($(this).val().replace(/C:\\fakepath\\/i, ''));
            var src = URL.createObjectURL($(this)[0].files[0]);
            $(this)[0].previousElementSibling.children[0].src = src;
        });
    });
})(jQuery);