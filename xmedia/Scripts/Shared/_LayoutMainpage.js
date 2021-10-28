﻿(function LayoutMainPage() {

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

})();


(function clickMobileMenu() {
    const menuContainer = document.querySelector('.js-mobile-burger-menu');

    let menu = document.querySelector('.js-nav01-divContainer-01-bot');

    menuContainer.addEventListener('click', function (e) {

        e.target.classList.toggle('active');
        menu.classList.toggle('active');

    });
})();


(function cartNotif() {

    let notifContainer = document.querySelector('.js-cart-notification-container');

    const notif = document.createElement('span');
    notif.setAttribute('class', 'notification-badge js-cart-notification');

    // get data in database

})();


//global variable for cartData to be saved!!

//if (!window.cartData) {
//    var cartData = new FormData();
//}

function saveOrder(data) {

    let url = AppGlobal.baseUrl + 'Orders/InsertUserFormOrders/';

    console.log(data);

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
        
    })
    .then(response => response.json())
    .then(data => {
        console.log(data);
    })
    .catch((error) => {
        console.log('Error: ', error);
    });
   
}


function getBase64Image(img) {
    const reader = new FileReader();

    reader.addEventListener("load", function () {
        //convert image file to base64 string
        return reader.result;
       // console.log(reader.result);
    }, false);

    if (img) {
        reader.readAsDataURL(img);
    }
}
