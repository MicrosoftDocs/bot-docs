$(function () {
    $('ul.header-nav').click(function (e) {
        $(e.currentTarget).toggleClass('expand');
    });
    $('.open-search').click(function (e) {
        $('.searchBox').toggleClass('expand');
    });
    $('.searchBox form').on('submit', function (e) {
        e.preventDefault();
        var $form = $(e.currentTarget);
        window.location.href = $form.attr('action') + '?s=' + encodeURIComponent($form.find('input').val());
        return false;
    });
});