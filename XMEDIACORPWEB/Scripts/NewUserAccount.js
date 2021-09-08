//#region Adding Image Profile to New Account

//Important note: Canvass width and height should be assigned here, not in CSS, otherwise weird problem occur

let canvass = document.querySelector('#createAccountWrapper5_canvass');
let canvassImageFile; // This is the modified image from canvass
let imageElementX;
let imageElementY;
let imageContextWidth;
let imageContextHeight;
let imageElement;

let div = document.querySelector('#createAccountWrapper4_1_photoIdDiv');
let inputElementOfTypeFile = document.querySelector('#uploadFilesInput');

if (canvass != null) {
    console.log(canvass)
    canvass.addEventListener('dblclick', dbclick);
}

function dbclick() {
    inputElementOfTypeFile.click();
}

//This will validate and process selected file via input element of type file
inputElementOfTypeFile.addEventListener('change', function () {
    canvass.width = 200;
    canvass.height = 200;
    let ctx = canvass.getContext('2d');

    //Check if input has file
    if (this.files.length > 0) {

        //Check the file type
        if (this.files[0].type == 'image/jpeg' || this.files[0].type == 'image/png') {

            //This section will display image to screen
            imageElement = new Image();
            let fileReader = new FileReader();

            fileReader.onload = function () {
                //Assigned url to image element using results value of reader
                imageElement.src = fileReader.result
                imageElement.onload = function () {

                    //Get the image proportio by getting its natural dimension
                    let imageElementNaturalWidth = this.naturalWidth
                    let imageElementNaturalHeight = this.naturalHeight
                    let ratio = imageElementNaturalHeight / imageElementNaturalWidth;

                    //Assign image size to display and maintain height proportion
                    imageContextWidth = 500;
                    imageContextHeight = imageContextWidth * ratio

                    //Get X and Y value to center the image
                    imageElementX = canvass.width / 2 - imageContextWidth / 2
                    imageElementY = canvass.height / 2 - (imageContextWidth * ratio) / 2

                    ctx.drawImage(this, imageElementX, imageElementY, imageContextWidth, imageContextHeight);

                    canvass.toBlob(function (blob) {
                        canvassImageFile = new File([blob], `xmediaUserProfileImage${new Date().getTime()}`, { type: 'image/jpg' })
                    }, "image/jpg", .8)
                }
            }

            fileReader.readAsDataURL(this.files[0])


        } else {

            //File extension are invalid
            alert('Please select JPEG or PNG image')
        }

    } else {
        //No file selected
        alert('no file selected')
    }


    canvass.addEventListener('mousedown', function (e) {
        let originalMouseX = e.clientX;
        let originalMouseY = e.clientY;
        let newImageElementX;
        let newImageElementY;

        canvass.addEventListener('mousemove', mousemove)

        function mousemove(e) {
            ctx.clearRect(0, 0, this.width, this.height);
            let newMouseX = e.clientX;
            let newMouseY = e.clientY;

            let mouseDistanceMoveX = newMouseX - originalMouseX;
            let mouseDistanceMoveY = newMouseY - originalMouseY;

            newImageElementX = imageElementX + mouseDistanceMoveX;
            newImageElementY = imageElementY + mouseDistanceMoveY;

            ctx.drawImage(imageElement, newImageElementX, newImageElementY, imageContextWidth, imageContextHeight);
        }

        canvass.addEventListener('mouseup', function () {
            this.removeEventListener('mousemove', mousemove);
            imageElementX = newImageElementX;
            imageElementY = newImageElementY;
        })
    })

})

//#endregion

//#region Validate Data in New Account Field
const newAccountsInputs = document.querySelectorAll('.newAccountsInputs');

//Key-up event for all input, its validate while typing
newAccountsInputs.forEach(function (input) {
    if (input.attributes.name.value == "ConfirmPassword") {

    } else {
        input.addEventListener('keyup', function () {
            let nameAttributeValue = input.attributes.name.value;
            let isValid = Validate(input, RegExpDataValidationPatterns[nameAttributeValue]);

            //The technic is adding class to input to change the looks if regex is true or false
            if (isValid) {
                input.classList.remove('invalid');
            } else {
                input.classList.add('invalid');
            }
        })
    }

})

//Check if input element has class "invalid", this is the indication 
//that input does not pass validation.
//This boolean return function will be called to click event of  
//button "Proceed to Create Account" as part of validation.

function IsAllInputPassedValidation() {

    let hasPassedValidation = true;

    //Clear comments before validation
    const validationCommentsForUserInput = document.querySelectorAll('.validationCommentsForUserInput');
    validationCommentsForUserInput.forEach(function (smallElement) {
        smallElement.innerHTML = "";
    })

    //Start validation at input elements
    newAccountsInputs.forEach(function (input) {

        if (input.hasAttribute("required") && input.value.trim().length == 0) {
            input.parentNode.children[1].innerHTML = `* ${inputNameReadableDisplay[input.attributes.name.value]} is required, please provide!`;
            hasPassedValidation = false;
        } else if (input.classList.contains('invalid')) {

            if (input.attributes.name.value == "EmailAddress") {
                input.parentNode.children[1].innerHTML = `* Email Address is not a valid format!`;
                hasPassedValidation = false;
            } else if (input.attributes.name.value == "IStillLoveYou") {
                input.parentNode.children[1].innerHTML = `* Password should between 8 to 20 characters!`;
                hasPassedValidation = false;
            } else if (input.attributes.name.value == "FirstName") {
                input.parentNode.children[1].innerHTML = `* First Name should contain only valid characters!`;
                hasPassedValidation = false;
            } else if (input.attributes.name.value == "LastName") {
                input.parentNode.children[1].innerHTML = `* Last Name should contain only valid characters!`;
                hasPassedValidation = false;
            }
            else if (input.attributes.name.value == "MobileNo") {
                input.parentNode.children[1].innerHTML = `* Mobile Number should atleast 11 digit numbers, parenthesis and plus sign is valid!`;
                hasPassedValidation = false;
            } else if (input.attributes.name.value == "LandlineNo") {
                input.parentNode.children[1].innerHTML = `* Landline Number should atleast 6 digit numbers, parenthesis and plus sign is valid!`;
                hasPassedValidation = false;
            }
        } else if (input.attributes.name.value == "ConfirmPassword") {
            const password = document.querySelector('#IStillLoveYou').value.trim();
            const confirmPassword = document.querySelector('#ConfirmPassword').value.trim();
            if (confirmPassword != password) {
                input.parentNode.children[1].innerHTML = `* Password do not match, please review!`;
                hasPassedValidation = false;
            }
        }
    })


    //Check if data privacy and terms of agreement is check
    const dataPrivacyCheckedBox = document.querySelector('#createAccntDataPrivacy');
    const termsAgreementCheckedBox = document.querySelector('#createAccntTermsAndAgreement');

    if (dataPrivacyCheckedBox.checked == false) {
        dataPrivacyCheckedBox.classList.add('invalidCheckedBox');
        hasPassedValidation = false;
    } else {
        dataPrivacyCheckedBox.classList.remove('invalidCheckedBox');
    }

    if (termsAgreementCheckedBox.checked == false) {
        termsAgreementCheckedBox.classList.add('invalidCheckedBox');
        hasPassedValidation = false;
    } else {
        termsAgreementCheckedBox.classList.remove('invalidCheckedBox');
    }


    return hasPassedValidation;
}

//#endregion

//#region Fetching Data and Forward to Server
let proceedCreateAccountBtn = document.querySelector('#proceedCreateAccount');

//This will forward formadata from view to controller and get responce back
proceedCreateAccountBtn.addEventListener('click', function () {
    //Check validation
    if (IsAllInputPassedValidation()) {
        let formData = new FormData(document.querySelector('#createAccountWrapper3_1'))

        formData.append('ImageFile', canvassImageFile)

        const options = {
            method: 'POST',
            body: formData
        }

        fetch('/NewUserAccount/CreateUsersAccount', options)
            .then(function (res) {
                
                return res.text();
                
            })
            .then(function (obj) {
                let doc = new DOMParser().parseFromString(obj, "text/html");

                if (doc.querySelector('#newAccntExistEmail_Wrapper0')) {

                    let div = doc.querySelector('#newAccntExistEmail_Wrapper0');
                    let createAccountWrapper0 = document.querySelector('#createAccountWrapper0');
                    createAccountWrapper0.appendChild(div);

                    let newAccntExistEmailYesBtn = document.querySelector('#newAccntExistEmailYesBtn');
                    console.log(newAccntExistEmailYesBtn);
                    newAccntExistEmailYesBtn.addEventListener('click', function () {
                        newAccntExistEmailYesBtn.closest('#newAccntExistEmail_Wrapper0').remove();
                    })

                } else {
                    document.querySelector('#contentWrapper0').innerHTML = obj
                }

            })
            .catch(function (error) {
                alert(error)
            })
    }

})

//#endregion


