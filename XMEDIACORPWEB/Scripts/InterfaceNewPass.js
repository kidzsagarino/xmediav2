//#region InterfaceNewPasswordLoginAccount

//Validate against regex patterns, realtime during typing
const newPasswordForgotInput = document.querySelectorAll('.newPasswordForgotInput');
newPasswordForgotInput.forEach(function (input) {
    input.addEventListener('keyup', function () {
        //Name attribute will match to object property name of Regex object 
        const nameAttributeValue = input.attributes.name.value;
        let isValid = Validate(input, RegExpDataValidationPatterns[nameAttributeValue])
        if (isValid) {
            input.classList.remove('invalidEmailForgotPass');
        } else {
            input.classList.add('invalidEmailForgotPass');
        }
    })
})


//This function will check if input has validation error, return true if OK, otherwise false
//This will be use when user will click "submit" button
function IsUpdateForgotPassPassedValidation() {
    let hasPassedValidation = true;

    //Clear comments before validation
    const validationCommentSmall = document.querySelectorAll('.updatePassValidationComments');
    validationCommentSmall.forEach(function (small) {
        validationCommentSmall.innerHTML = "";
    })
    

    const newPasswordForgotInput = document.querySelectorAll('.newPasswordForgotInput');
    newPasswordForgotInput.forEach(function (input) {
        if (input.hasAttribute("required") && input.value.trim().length == 0) {
            input.parentNode.querySelector('.updatePassValidationComments').innerHTML = `* ${inputNameReadableDisplay[input.attributes.name.value]} is required, please provide!`;
            hasPassedValidation = false;
        } else if (input.classList.contains('invalidEmailForgotPass')) {
            input.parentNode.querySelector('.updatePassValidationComments').innerHTML = `* Email Address is not a valid format!`;
            hasPassedValidation = false;
        } else if (input.attributes.name == 'ConfirmPassword') {
            const newPass = document.querySelector('#updatePassPasswordInput').value.trim();
            if (input.value.trim() != newPass) {
                hasPassedValidation = false;
                input.parentNode.querySelector('.updatePassValidationComments').innerHTML = `* Password do not match, please check!`;
            }
        }
    })
    
    return hasPassedValidation;
}


//Click submit button
document.querySelector('#updatePassFooterSubmit').addEventListener('click', function () {
    if (IsUpdateForgotPassPassedValidation()) {
        const newPasswordInput = document.querySelector('#updatePassPasswordInput');
        
        let formData = new FormData();
        formData.append('EmailID', newPasswordInput.getAttribute('data-emailID'));
        formData.append('NewPassword', newPasswordInput.value.trim());

        const options = {
            method: 'POST',
            body:formData
        }
        console.log(newPasswordInput.getAttribute('data-emailID'))
        fetch('/ForgotPassword/AcceptNewPasswordLoginAccount',options)
            .then(function (response) {
                return response.json();
            }).then(function (data) {
                if (data.StatusCodeNumber == 1) {
                    const updatePassWrapper2_ContentForm = document.querySelector('#updatePassWrapper2_ContentForm');
                    const div = document.createElement('div');
                    div.classList.add('newPasswordSuccesWrap0')

                    const h3_0 = document.createElement('h3');
                    h3_0.classList.add('newPasswordSuccesh3');
                    h3_0.textContent = 'You have successully update your password!'

                    const h3_1 = document.createElement('h3');
                    h3_1.classList.add('newPasswordSuccesh3');
                    h3_1.textContent = 'You can now login to your account.'

                    div.appendChild(h3_0);
                    div.appendChild(h3_1);

                    updatePassWrapper2_ContentForm.innerHTML = "";
                    updatePassWrapper2_ContentForm.appendChild(div);

                    const updatePassWrapper2_Footer = document.querySelector('#updatePassWrapper2_Footer');
                    const div1 = document.createElement('div')
                    div1.classList.add('newPasswordSuccesWrapFooter')

                    const anchorTag = document.createElement('a');
                    anchorTag.textContent = 'Go to login'
                    anchorTag.classList.add('newPasswordSuccesAnchor')
                    anchorTag.href = 'https://localhost:44362/LoginUserAccount/ExistingUserLoginAccount'

                    div1.appendChild(anchorTag)

                    updatePassWrapper2_Footer.innerHTML=""
                    updatePassWrapper2_Footer.appendChild(div1);
                }
            })
    }
})

//#endregion