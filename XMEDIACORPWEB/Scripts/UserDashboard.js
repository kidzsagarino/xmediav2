
//#region My Account Menu @ Main Dashboard Including Sub-Menu 

//#region Click "My Account" Menu
let dashboardMenuMyAccount = document.querySelector('#custDashLeftPanelMenu_MyAccount');
dashboardMenuMyAccount.addEventListener('click', function (e) {

    //check first there is same dispaly
    let dashboardMainContent = document.querySelector('#customerDashboardWrapper2_content');

    if (dashboardMainContent.hasChildNodes()) { //Check if has childnodes

        //Check if childnodes are not the same
        if (!dashboardMainContent.childNodes[0].getAttribute('id') == 'userMyAccountMain_Wrapper0') {

            //Call display function
            DisplayMyAccountToDashboardContent();
        }

    } else {
        //Call display function
        DisplayMyAccountToDashboardContent();
    }

    function DisplayMyAccountToDashboardContent() {

        //Main content display
        fetch('/UserDashboard/UserMyAccountMain')
            .then(function (res) {
                console.log(res)
                return res.text();
            })
            .then(function (obj) {
                const docs = new DOMParser().parseFromString(obj, "text/html");
                const userProfMainWrapper0 = docs.querySelector('#userMyAccountMain_Wrapper0');
                const customerDashboardWrapper2content = document.querySelector('#customerDashboardWrapper2_content');

                if (!customerDashboardWrapper2content.hasChildNodes()) {
                    customerDashboardWrapper2content.appendChild(userProfMainWrapper0);
                } else if (customerDashboardWrapper2content.childNodes[0].getAttribute("name") != "personalInformation") {
                    customerDashboardWrapper2content.innerHTML = ""; // Clear content before adding
                    customerDashboardWrapper2content.appendChild(userProfMainWrapper0);
                }

                ClickMyAccountPersonal();
                ClickMyAccountAddress();

            })
            .catch(function (error) {
                alert(error)
            })
    }

});
//#endregion

//#region Personal info Sub-menu

function ClickMyAccountPersonal() {
    //This event listener must be inside this fetch, because element is available only after this call
    const userProfMainLeftPanelMenuItem = document.querySelector('#userMyAccountMain_PersonalInfo')
    userProfMainLeftPanelMenuItem.addEventListener('click', function () {
        const userProfMainContent = document.querySelector('#userMyAccountMain_Content');

        let formData = new FormData();
        const userID = document.querySelector('#customerDashboardWrapper0').getAttribute('data-id');

        formData.append("userID", userID)

        const options = {
            method: 'POST',
            body: formData
        }


        fetch('/UserDashboard/UserMyAccountPersonalInfo', options)
            .then(function (res) {
                return res.text();
            })
            .then(function (obj) {
                //Parse the return HMTL into document so you can manipulate before adding to DOM
                const parseDocument = new DOMParser().parseFromString(obj, "text/html")

                //Manipulate the html before attaching to DOM

                //Assign selected gender by getting the stored "data-GenderID" attribute at SELECT element
                //See cshtml model assignment of data-GenderID
                const select = parseDocument.querySelector('#userPersonalInfoGender');
                const genderID = select.getAttribute('data-GenderID');

                const selectOptions = parseDocument.querySelectorAll('.genderOptions');
                console.log(selectOptions)
                selectOptions.forEach(function (option) {
                    console.log(genderID, option.getAttribute('data-id'))
                    if (option.getAttribute('data-id') == genderID) {

                        option.setAttribute('SELECTED', true);
                    }
                })

                //This is the main wrapper, selected to be added to DOM together with its child component.
                const div = parseDocument.querySelector('.userProfwrapper');


                //Now add the "div" to DOM
                if (!userProfMainContent.hasChildNodes()) {
                    userProfMainContent.appendChild(div);

                } else {
                    userProfMainContent.innerHTML = "";
                    userProfMainContent.appendChild(div);
                    console.log('2')
                }

                //This function is for data modification
                //This should run here to ensure that DOM is available before calling any component
                PersonalInformation();

            })
            .catch(function (error) {
                alert(error)
            })
    })

}

function PersonalInformation() {
    //Click Edit Button 
    let UserMyAccountPersInfoEditH3 = document.querySelectorAll('.userMyAccountPersInfoEditH3')

    UserMyAccountPersInfoEditH3.forEach(function (EditButton) {
        EditButton.addEventListener('click', ClickEdit)
    })

    //This is needed for comparison if user input changes on data, use at save function
    let originalInputValue;

    function ClickEdit(e) {
        originalInputValue = e.target.parentNode.querySelector('.userMyAccountPersInfoInput').value;
        if (e.target.parentNode.querySelector('input')) {
            //Enable input element
            e.target.parentNode.querySelector('input').removeAttribute("disabled");
            SaveAndCancelBtn(e);

        } else {
            //Enable input element
            e.target.parentNode.querySelector('select').removeAttribute("disabled");
            SaveAndCancelBtn(e);
        }
        function SaveAndCancelBtn(e) {
            //Create and Add element on DOM "Save" and "Cancel"
            const div = document.createElement('div');
            div.setAttribute('class', 'userPersonalInfoSaveCancelDiv')

            const h3Cancel = document.createElement('h3');
            h3Cancel.setAttribute('class', 'userPersonalInfoCancel');
            h3Cancel.textContent = 'Cancel';
            h3Cancel.addEventListener('click', ClickCancelPersonalinfo);

            const h3Save = document.createElement('h3');
            h3Save.setAttribute('class', 'userPersonalInfoSave');
            h3Save.textContent = 'Save';
            h3Save.addEventListener('click', ClickSavePersonalInfo);

            div.appendChild(h3Save);
            div.appendChild(h3Cancel);

            const userMyAccountPersInfoInputContainer = e.target.parentNode.querySelector('.userMyAccountPersInfoInputContainer')
            if (userMyAccountPersInfoInputContainer.childElementCount == 1) {
                userMyAccountPersInfoInputContainer.appendChild(div);
            }

            //Hide the edit button
            e.target.style.display = 'none'

            //Change the color edit button
            UserMyAccountPersInfoEditH3.forEach(function (EditBtn) {
                //This will disable the click edit button for other edit
                EditBtn.style.color = '#C8C8C8';
                EditBtn.style.textDecoration = 'none'
                //Remove event listener to other edit button
                EditBtn.removeEventListener('click', ClickEdit)
            })
        }
    }

    //This will save changes on data at database 
    function ClickSavePersonalInfo(e) {

        const inputElement = e.target.closest('.userPersonalInfoMainContainerDiv').querySelector('.userMyAccountPersInfoInput');
        const newInputValue = inputElement.value;

        //Check first if value is same or different, if different proceed 
        if (originalInputValue != newInputValue) {
            let isAllDataValid = true; //For now its true, change this for validation

            let formData = new FormData();
            const userID = document.querySelector('#customerDashboardWrapper0').getAttribute('data-id');

            //console.log(inputElement.tagName)
            formData.append("UserID", userID)
            if (inputElement.tagName == "SELECT") {
                const genderID = inputElement[inputElement.selectedIndex].getAttribute('data-id')
                formData.append(inputElement.getAttribute('name'), genderID);

            } else {
                formData.append(inputElement.getAttribute('name'), inputElement.value);
                console.log(inputElement.value.toString(), inputElement.getAttribute('name'))
            }



            if (isAllDataValid) {

                const options = {
                    method: 'POST',
                    body: formData
                }

                fetch('/UserDashboard/SaveEditedPersonalInformation', options)
                    .then(function (resposnse) {
                        return resposnse.json();
                    }).then(function (data) {

                    })
            }
        }


        //Reset view back to original
        ResetPersonalInfoView(e)

        //Re attached the event listener
        UserMyAccountPersInfoEditH3.forEach(function (EditButton) {
            EditButton.addEventListener('click', ClickEdit)
        })
    }

    //This will cancel changes on data
    function ClickCancelPersonalinfo(e) {

        //Reset view back to original
        ResetPersonalInfoView(e)

        //Re attached the event listener
        UserMyAccountPersInfoEditH3.forEach(function (EditButton) {
            EditButton.addEventListener('click', ClickEdit)
        })

    }

    //Reset orginal view, its view before clicking "edit"
    function ResetPersonalInfoView(e) {

        //Show edit button
        const editElement = e.target.closest('.userPersonalInfoMainContainerDiv').querySelector('.userMyAccountPersInfoEditH3');
        editElement.style.display = 'flex';

        //Restore color of  
        const editButtons = document.querySelectorAll('.userMyAccountPersInfoEditH3');
        editButtons.forEach(function (editButton) {
            editButton.style.color = 'dodgerblue';
        })

        //Disable input or select element
        const inputElement = e.target.closest('.userPersonalInfoMainContainerDiv').querySelector('.userMyAccountPersInfoInput');
        inputElement.setAttribute('disabled', 'true');


        //Remove save and cancel button by removing its parent
        e.target.parentNode.remove();
    }
}

//#endregion

//#region Account Addresses Sub-menu
function ClickMyAccountAddress() {
    //This event listener must be inside this fetch, because element is available only after this call
    let userProfMainLeftPanelMenuItem = document.querySelector('#userMyAccountMain_Address')
    userProfMainLeftPanelMenuItem.addEventListener('click', function () {
        let userProfMainContent = document.querySelector('#userMyAccountMain_Content');

        fetch('/UserDashboard/UserMyAccountAddress')
            .then(function (res) {
                console.log(res)
                return res.text();
            })
            .then(function (obj) {
                let docs = new DOMParser().parseFromString(obj, "text/html")
                let div = docs.querySelector('.userProfwrapper');
                console.log(userProfMainContent.hasChildNodes())

                if (!userProfMainContent.hasChildNodes()) {
                    userProfMainContent.appendChild(div);
                } else {
                    userProfMainContent.innerHTML = "";
                    userProfMainContent.appendChild(div);
                }

                console.log(docs);
            })
            .catch(function (error) {
                alert(error)
            })
    })

}
//#endregion


//#endregion