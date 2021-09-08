
//#region All Input Validation is Here
//At key-up event, check the realtime input value against regex
//If regex is false attached "invalid class"
let userLoginInput = document.querySelectorAll('.userLoginInput');
userLoginInput.forEach(function (input) {

    input.addEventListener('keyup', function (e) {
        let nameAttributeValue = input.attributes.name.value;

        let isValid = Validate(input, RegExpDataValidationPatterns[nameAttributeValue]);

        //The technic is adding class to input to change the looks if regex is true or false
        if (isValid) {
            input.classList.remove('invalid');
        } else {
            input.classList.add('invalid');
        }

    })
})


//Check the input if empty or if it has "invalid" class
//Display appropriate comments
//This function will return true or false 
function IsLoginInputPassedValidation() {

    let hasPassedValidation = true;

    //Clear comments before validation
    const validationCommentsLogin = document.querySelectorAll('.validationCommentsLogin');
    validationCommentsLogin.forEach(function (smallElement) {
        smallElement.innerHTML = "";
    })

    //Start validation at input elements
    userLoginInput.forEach(function (input) {

        if (input.hasAttribute("required") && input.value.trim().length == 0) {
            //console.log(input.parentNode.document.querySelector('.validationCommentsLogin'))
            input.parentNode.querySelector('.validationCommentsLogin').innerHTML = `* ${inputNameReadableDisplay[input.attributes.name.value]} is required, please provide!`;
            hasPassedValidation = false;
        } else if (input.classList.contains('invalid')) {

            if (input.attributes.name.value == "EmailAddress") {
                input.parentNode.children[1].innerHTML = `* Email Address is not a valid format!`;
                hasPassedValidation = false;
            } else if (input.attributes.name.value == "IStillLoveYou") {
                input.parentNode.children[1].innerHTML = `* Password should between 8 to 20 characters!`;
                hasPassedValidation = false;
            }
        }
    })

    return hasPassedValidation;
}

//#endregion


// Close and open login users interface
function OpenAndCloseLoginUI() {
    let customerLoginDiv = document.querySelector('#homeWrapper0_customerLoginDiv');
    if (getComputedStyle(customerLoginDiv).display == 'none') {
        customerLoginDiv.style.display = 'block';
    } else {
        customerLoginDiv.style.display = 'none';
    }
}



//Create Account button at footer
document.querySelector('#custLogCreateAccntH3').addEventListener('click', function (e) {
    if (e.target == e.currentTarget) {
        OpenAndCloseLoginUI();
    }
})


//Login button at footer
document.querySelector('#custLogFooterLoginH3').addEventListener('click', function (e) {

    if (IsLoginInputPassedValidation()) {
        //Start fetch here
        //This is equivalent of anchor tag href links, but here you can incorporate paramerters and models
        let formData = new FormData(document.querySelector('#custLogContentDiv'));
        const options = {
            method: "POST",
            body: formData
        }
        let isJson = false;
        fetch("/UserDashboard/UserMainDashboard", options)

            .then(function (response) {

                if (response.ok) {

                    const contentType = response.headers.get('content-type');
                    if (contentType && contentType.indexOf('application/json') !== -1) {
                        isJson = true;
                        return response.json();

                    } else {

                        return response.text();

                    }
                }
            }).then(function (data) {

                const custLogEmailComment = document.querySelector('#custLogEmailComment');
                const custLogPasswordComment = document.querySelector('#custLogPasswordComment');
                const custLogEmailAddInput = document.querySelector('#custLogEmailAddInput');
                const custLogPasswordInput = document.querySelector('#custLogPasswordInput');
                const custLogEmailTopLabel = document.querySelector('#custLogEmailTopLabel');
                const custLogPasswordTopLabel = document.querySelector('#custLogPasswordTopLabel');
                const custLogNoMatchComment = document.querySelector('#custLogPassAndEmailNoMatchComment')

                //Reset login to normal view
                custLogEmailComment.innerHTML = "";
                custLogPasswordComment.innerHTML = "";
                custLogEmailAddInput.classList.remove('invalidEmail');
                custLogPasswordInput.classList.remove('invalidEmail');
                custLogEmailTopLabel.style.display = 'none';
                custLogPasswordTopLabel.style.display = 'none';
                custLogNoMatchComment.style.display = 'none'


                if (isJson) {

                    let numberCode = data.LoginStatusCode.LoginStatusNumberCode;
                    if (numberCode == 2) {

                        custLogEmailComment.textContent = '* Your Email Address is not registered!'
                        custLogEmailAddInput.classList.add('invalidEmail');
                        custLogEmailTopLabel.style.display = 'inline';

                    } else {

                        custLogNoMatchComment.style.display = 'flex';

                        custLogPasswordTopLabel.style.display = 'inline';
                        custLogPasswordInput.classList.add('invalidEmail');

                        custLogEmailTopLabel.style.display = 'inline';
                        custLogEmailAddInput.classList.add('invalidEmail');
                    }
                } else {
                    window.open("", "_self").document.write(data);
                }

            })



    }


})

