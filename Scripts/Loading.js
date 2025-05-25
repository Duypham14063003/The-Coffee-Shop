$(document).ajaxStart(function () {
    // Hiển thị hiệu ứng loading khi có yêu cầu AJAX
    $(".loading-overlay").fadeIn();
}).ajaxStop(function () {
    // Ẩn hiệu ứng loading khi yêu cầu AJAX kết thúc
    $(".loading-overlay").fadeOut();
});
