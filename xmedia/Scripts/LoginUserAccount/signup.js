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

        let form = this.closest('form');

        form.querySelectorAll('input').forEach((item) => {

            item.classList.remove('has-error');

            if (item.type == 'file') {
                item.closest('.pic-frame').removeAttribute('style');
            }

            if (!item.value) {

                if (item.type == 'file') {
                    item.closest('.pic-frame').setAttribute('style', 'border: 1px solid #FF0000');
                }
                else {
                    item.classList.add('has-error');
                }
            }
            else {
                save(form);
            }

        });

    });

    function save(form) {

        //check if password and confirm password matches

        let password = form.querySelector('input[name=Password]');
        let confirmPassword = form.querySelector('input[name=ConfirmPassword]');

        if (password.value == confirmPassword.value) {
            // proceed to saving

            let formData = new FormData();

            formData.append('FirstName', form.querySelector('input[name=Firstname]').value);
            formData.append('LastName', form.querySelector('input[name=Lastname]').value);
            formData.append('MobileNo', form.querySelector('input[name=MobileNo]').value);
            formData.append('LandlineNo', form.querySelector('input[name=LandlineNo]').value);
            formData.append('EmailAddress', form.querySelector('input[name=Email]').value);
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