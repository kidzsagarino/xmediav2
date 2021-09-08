//#region EmailStatusAndConfirmation

//At key-up event, check the realtime input value against regex
//If regex is false attached "invalidEmailForgotPass" class

//Validate against regex patterns, realtime during typing
const forgotPassEmailAddInput = document.querySelector('#forgotPassEmailAddInput');
forgotPassEmailAddInput.addEventListener('keyup', function () {
    //Name attribute will match to object property name of Regex object 
    const nameAttributeValue = forgotPassEmailAddInput.attributes.name.value;
    let isValid = Validate(forgotPassEmailAddInput, RegExpDataValidationPatterns[nameAttributeValue])
    if (isValid) {
        forgotPassEmailAddInput.classList.remove('invalidEmailForgotPass');
    } else {
        forgotPassEmailAddInput.classList.add('invalidEmailForgotPass');
    }
})


//This function will check if input has validation error, return true if OK, otherwise false
//This will be use when user will click "submit" button
function IsForgotEmailInputPassedValidation() {
    let hasPassedValidation = true;

    //Clear comments before validation
    const validationCommentSmall = document.querySelector('#forgotPassEmailComment');
    validationCommentSmall.innerHTML = "";

    const forgotPassEmailAddInput = document.querySelector('#forgotPassEmailAddInput');
    if (forgotPassEmailAddInput.hasAttribute("required") && forgotPassEmailAddInput.value.trim().length == 0) {
        validationCommentSmall.innerHTML = `* ${inputNameReadableDisplay[forgotPassEmailAddInput.attributes.name.value]} is required, please provide!`;
        hasPassedValidation = false;
    } else if (forgotPassEmailAddInput.classList.contains('invalidEmailForgotPass')) {
        validationCommentSmall.innerHTML = `* Email Address is not a valid format!`;
        hasPassedValidation = false;
    }
    return hasPassedValidation;
}


//Click submit button
document.querySelector('#forgotPassFooterSubmit').addEventListener('click', function () {
    if (IsForgotEmailInputPassedValidation()) {
        const emailadd = document.querySelector('#forgotPassEmailAddInput').value;
        console.log(emailadd);
        fetch('/ForgotPassword/EmailStatusAndConfirmation?' + new URLSearchParams({ emailAddress: emailadd }))
            .then(function (response) {
                return response.json();
            }).then(function (data) {
                if (data.StatusCodeNumber == 1) {
                    //Call another fetch to get successful view
                    fetch('/ForgotPassword/EmailToConfirmIsSuccessfullySent?' + new URLSearchParams({ _emailAdd: data.EmailAddress, _firstName: data.FirstName }))
                        .then(function (response) {
                            return response.text()
                        }).then(function (data) {
                            
                            const parseDocument = new DOMParser().parseFromString(data, "text/html");
                            document.querySelector('#contentWrapper0').innerHTML = "";
                            document.querySelector('#contentWrapper0').appendChild(parseDocument.querySelector('#forgotPassSuccess_Wrapper0'));
                            
                        })
                };
            })
    }
})

//#endregion

//#region InterfaceNewPasswordLoginAccount


//Validate against regex patterns, realtime during typing
const updateForgotPassInput = document.querySelectorAll('.updateForgotPassInput');

updateForgotPassInput.forEach(function (input) {
    updatePassPasswordInput.addEventListener('keyup', function () {
        console.log('keyup')
        //Name attribute will match to object property name of Regex object 
        const nameAttributeValue = forgotPassEmailAddInput.attributes.name.value;
        let isValid = Validate(forgotPassEmailAddInput, RegExpDataValidationPatterns[nameAttributeValue])
        if (isValid) {
            forgotPassEmailAddInput.classList.remove('invalidEmailForgotPass');
        } else {
            forgotPassEmailAddInput.classList.add('invalidEmailForgotPass');
        }
    })
})



//#endregion






