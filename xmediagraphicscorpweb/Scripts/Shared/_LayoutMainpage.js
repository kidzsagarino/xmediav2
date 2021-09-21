function LayoutMainPage() {

    const nav01MenuButton = document.querySelectorAll('.js-nav01-button');

    nav01MenuButton.forEach(function (button) {
        button.addEventListener('mouseover', mouseover)
    })

    function mouseover(e) {
        //Disappear all display
        ResetAllMenuDisplayToNone();
        //Display the nextsibling menu
        e.target.nextElementSibling.style.display = 'grid';
        HideMenuDisplayWhenMouseLeave();

    }

    nav01MenuButton.forEach(function (button) {
        button.addEventListener('click', click)
    })

    function click(e) {
        //Disappear all display
        ResetAllMenuDisplayToNone();
        //Display the nextsibling menu
        e.target.nextElementSibling.style.display = 'grid';
    }

    function ResetAllMenuDisplayToNone() {
        document.querySelectorAll('.js-nav01-menuDisplay').forEach(function (div) {
            div.style.display = 'none';
        });
    }

    //This must be envoke when menu display is shown, otherwise error
    //Envoke this at mousehover event of main menu, coz this time it is shown
    function HideMenuDisplayWhenMouseLeave() {
        const MainMenuDisplayDiv = document.querySelectorAll('.js-nav01-menuDisplay');
        MainMenuDisplayDiv.forEach(function (Div) {
            Div.addEventListener('mouseleave', mouseleave)
        })
        function mouseleave(e) {
            e.target.style.display = 'none'
        }
    }
    
}

LayoutMainPage();

