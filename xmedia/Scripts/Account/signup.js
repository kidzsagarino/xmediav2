function previewProfileImage(event) {

    let parentEl = event.target.closest('label');

    let reader = new FileReader();
    reader.onload = function () {
        let imgTag = parentEl.querySelector('img');
        imgTag.setAttribute('style', 'width:200px; height: 200px');
        imgTag.src = reader.result;

        let defaultText = parentEl.querySelector('span');
        defaultText.style.display = 'none';
    };
    reader.readAsDataURL(event.target.files[0]);
}


function createAccount() {

    document.querySelector('.js-create-account').addEventListener('click', function (e) {

        let errors = 0;

        let form = this.closest('form');

        form.querySelectorAll('input[required]').forEach((item) => {

            item.classList.remove('has-error');

            if (item.type == 'file') {
                item.closest('.pic-frame').removeAttribute('style');
            }

            if (!item.value) {

                errors++;

                if (item.type == 'file') {
                    item.closest('.pic-frame').setAttribute('style', 'border: 1px solid #FF0000');
                }
                else {
                    item.classList.add('has-error');
                }
            }
            
        });

        if (errors === 0) {
            save(form);
        }

    });

    function save(form) {

        //check if password and confirm password matches

        let password = form.querySelector('input[name=Password]');
        let confirmPassword = form.querySelector('input[name=ConfirmPassword]');

        if (password.value == confirmPassword.value) {
            // proceed to saving

            let mainformData = new FormData();

            mainformData.append('LoginInfo.EmailAddress', form.querySelector('input[name=Email]').value);
            mainformData.append('LoginInfo.IStillLoveYou', form.querySelector('input[name=Password]').value);
            mainformData.append('PersonalInfo.FirstName', form.querySelector('input[name=Firstname]').value);
            mainformData.append('PersonalInfo.LastName', form.querySelector('input[name=Lastname]').value);
            mainformData.append('MobileNo', form.querySelector('input[name=MobileNo]').value);
            mainformData.append('LandlineNo', form.querySelector('input[name=LandlineNo]').value);
            mainformData.append('CompanyName', form.querySelector('input[name=CompanyName]').value);
            mainformData.append('PersonalInfo.File', form.querySelector('input[name=Photo]').files[0]);

            fetch(AppGlobal.baseUrl + 'LoginUserAccount/SignUp', {
                method: 'POST',
                body: mainformData
            })
            .then(response => response.json())
            .then(function (data) {

            })
            .catch(function (error) {
                console.log(error);
            });
        }
        else {
            password.classList.add('has-error');
            confirmPassword.classList.add('has-error');
        }
    }
}

window.onload = function () {
    createAccount();
}