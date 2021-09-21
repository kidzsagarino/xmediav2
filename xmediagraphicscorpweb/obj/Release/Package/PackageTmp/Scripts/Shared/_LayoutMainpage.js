function LayoutMainPage() {

    const nav01MenuButton = document.querySelectorAll('.js-nav01-button');

    nav01MenuButton.forEach(function (button) {
        button.addEventListener('mouseover', mouseover)
    })

    function mouseover(e) {
        //Disappear all display
        ResetAllMenuDisplayToNone();
        //Display the nextsibling menu
        e.target.nextElementSibling.style.display = 'flex';

        e.target.addEventListener('click', click)

    }

    function click(e) {
        //Disappear all display
        ResetAllMenuDisplayToNone();
        //Display the nextsibling menu
        e.target.nextElementSibling.style.display = 'flex';
        e.target.removeEventListener('mouseleave', mouseleave)
    }



    nav01MenuButton.forEach(function (button) {
        button.addEventListener('mouseleave', mouseleave)
    })

    function mouseleave() {
        //Disappear all display
        ResetAllMenuDisplayToNone();
    }

    function ResetAllMenuDisplayToNone() {
        document.querySelectorAll('.js-nav01-menuDisplay').forEach(function (div) {
            div.style.display = 'none';
        });
    }
}

LayoutMainPage();