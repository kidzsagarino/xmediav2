
fetch(AppGlobal.baseUrl + 'Forms/GetFormCommon/',
    {
        headers: {
            'Content-Type': 'application/json'
        },
    }
    )
    .then(data => data.json())
    .then(function (data) {
        console.log(data);
    })
    .catch(function (error) {
        console.log(error);
    })


const ErrorMessage = {
    ServerError: 'Server Error',
    FileExists: 'File Exists',
    DBError: 'Database Error'
};



let IsConfirmedAlertYesOrNo = function (parentElement, message, alertClassName) {

    const containerDiv = document.createElement('div');
    containerDiv.classList.add('alert-main-expandable-cont', 'jsAlertYesNoMainCont', alertClassName);
    const div0 = document.createElement('div');
    div0.classList.add('alert-cont-00');

    const alertHeader = document.createElement('div');
    alertHeader.classList.add('alert-cont-01', 'alert-cont-header');
    div0.appendChild(alertHeader);

    const alertContent = document.createElement('div');
    alertContent.classList.add('alert-cont-01', 'alert-cont-content');

    const alertContentParag = document.createElement('p');
    alertContentParag.classList.add('alert-paragraph')
    alertContentParag.innerText = message

    alertContent.appendChild(alertContentParag)
    div0.appendChild(alertContent);


    const alertFooter = document.createElement('div');
    alertFooter.classList.add('alert-cont-01', 'alert-cont-footer');

    const alertFooterBtnNo = document.createElement('button');
    alertFooterBtnNo.classList.add('alert-button', 'alert-button-no', 'jsProjectDeleteItemNo');
    alertFooterBtnNo.innerText = 'No';

    alertFooter.appendChild(alertFooterBtnNo)

    const alertFooterBtnYes = document.createElement('button');
    alertFooterBtnYes.classList.add('alert-button', 'alert-button-yes', 'jsProjectDeleteItemYes');
    alertFooterBtnYes.innerText = 'Yes';

    alertFooter.appendChild(alertFooterBtnYes)

    div0.appendChild(alertFooter);

    containerDiv.appendChild(div0);
    parentElement.appendChild(containerDiv);

    return new Promise(function (resolve, reject) {
        alertFooterBtnYes.addEventListener('click', function () {
            resolve('Yes');
            containerDiv.remove();
        })

        alertFooterBtnNo.addEventListener('click', function () {
            reject('No');
            containerDiv.remove();
        })
    })
}


IsConfirmedAlertYesOrNo(document.body, `Are you sure? \n This will be deleted permanently!`).then(function (resolve) {
    // delete database file
    const formData = new FormData();
    formData.append('ProjectLotID', parentContainer.querySelector('.jsProjectLotNo').getAttribute('data-id'));

    const options = {
        method: 'POST',
        body: formData
    }

    fetch('/Projects/ProjectLotDeleteData', options).then(function (res) {
        if (res.status == 200) {
            return res.json();
        }
        else {
            throw ErrorMessage.ServerError;
        }
    }).then(function (data) {
        if (data.StatusCodeNumber == 1) {
            parentContainer.remove();
            IsConfirmedAlertOk(document.body, 'Deleted Successfully!');
        }
        else if (data.StatusCodeNumber == 2) {
            throw ErrorMessage.FileExists;
        }
        else {
            //IsConfirmedAlertOk(document.body, data.ErrorMessage)
            throw data.ErrorMessage;
        }
    })
        .catch(function (error) {
            alert(error);
        })

}).catch(function () {
    // this where reject comes
})
