
var loginFunc = function () {

    let loginForm = document.querySelector('.js-login-form');
    let errorMsg = document.createElement('p');
    errorMsg.setAttribute('class', 'error-message');

    let email = loginForm.querySelector('input[name=Email]');
    let password = loginForm.querySelector('input[name=Password]');

    email.classList.remove('has-error');
    password.classList.remove('has-error');

    if (email.value && password.value) {
        //proceed to login

        let formData = new FormData();

        formData.append('EmailAddress', email.value);
        formData.append('IStillLoveYou', password.value);

        fetch(AppGlobal.baseUrl + 'LoginUserAccount/UserLoginAccount', {
            method: 'POST',
            body: formData
        })
            .then(response => response.json())
            .then(function (data) {
                console.log(data);

                if (data.LoginStatusCode.LoginStatusNumberCode == 1) {

                    // check if there are pending cart

                    if (localStorage.getItem('Cart')) {
                        let jsonData = JSON.parse(localStorage.getItem('Cart'));

                        // if there are pending. then save !!
                    }
                }
                else {
                    // login failed

                    errorMsg.innerHTML = 'Login Failed';

                    loginForm.insertBefore(errorMsg, loginForm.querySelectorAll('.form-content')[0]);
                }

            })
            .catch(function (data) {
                console.log('Error');
            })

    }
    else {
        // error occured
        if (!email.value) {
            email.classList.add('has-error')

        }
        if (!password.value) {
            password.classList.add('has-error');
        }
    }
};

var eventListeners = function () {

    document.querySelector('.js-loginBtn').addEventListener('click', function (e) {

        loginFunc();

    });

    document.querySelector('.js-login-form').addEventListener('keydown', function (e) {

        if (e.keyCode == 13) {
            loginFunc();
        }

    });

}

window.onload = function () {
    eventListeners();
};