
$(document).ready(function () {
    const buttons = $(".button");
    const cards = $(".card");

    function filter(category, items) {
        items.each(function () {
            const isItemFiltered = !$(this).hasClass(category);
            const isShowAll = category.toLowerCase() === "all";
            if (isItemFiltered && !isShowAll) {
                $(this).addClass("hide");
            } else {
                $(this).removeClass("hide");
            }
        });
    }

    buttons.on("click", function () {
        const currentCategory = $(this).data("filter");
        console.log(currentCategory);
        filter(currentCategory, cards);
    });
});
